export function formatM3u8Url(m3u8Id: string, mediaType: string) {
  return `/static/m3u8/${m3u8Id}.${mediaType}`;
}
