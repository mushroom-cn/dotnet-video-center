import { IMedia } from "@api";
import AutoPlayer from "./AutoPalyer";
import EditMediaTag from "./EditMediaTag";
import { renderModal } from "./renderModal";

const editMediaTag = async function (media: IMedia) {
  return renderModal<void>(({ onClose, onOk }) => {
    return <EditMediaTag media={media} onOk={onOk} onClose={onClose} />;
  });
};
const runPlayer = async function (src: string) {
  return renderModal<void>(({ onClose }) => {
    return <AutoPlayer src={src} onClose={onClose} />;
  });
};
export default {
  editMediaTag,
  runPlayer,
};
