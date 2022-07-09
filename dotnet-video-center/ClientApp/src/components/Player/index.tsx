import { useEffect, useMemo, useRef } from "react";
import videojs, { VideoJsPlayer, VideoJsPlayerOptions } from "video.js";
import ReactResizeDetector, { useResizeDetector } from "react-resize-detector";

type VideoJSProps = {
  src: string;
  onReady?: (player: VideoJsPlayer) => void;
};
export const VideoJS = (props: VideoJSProps) => {
  const { onReady, src } = props;
  const options = useMemo<VideoJsPlayerOptions>(
    () => ({
      controls: true,
      autoplay: true,
      preload: "auto",
      responsive: true,
      playbackRates: [0.5, 1, 1.25, 1.5, 2],
      sources: [
        {
          src,
          type: "application/x-mpegURL",
        },
      ],
      fullscreen: {},
    }),
    [src]
  );
  const {
    width,
    height,
    ref: videoRef,
  } = useResizeDetector<HTMLVideoElement>();
  const playerRef = useRef<VideoJsPlayer | null>(null);
  useEffect(() => {
    const videoElement = videoRef.current;
    if (videoElement) {
      if (!playerRef.current) {
        const player = videojs(videoElement, options, () => {
          onReady?.(player);
        });
        player.usingPlugin("m3u8-parser");
        playerRef.current = player;
      }
    }

    return () => {
      playerRef.current?.dispose();
      playerRef.current = null;
    };
  }, [options, videoRef, onReady]);

  return (
    <div data-vjs-player style={{ width: `${width}px`, height: `${height}px` }}>
      <video ref={videoRef} className="video-js vjs-big-play-centered"></video>
    </div>
  );
};

export default VideoJS;
