import { Modal } from "antd";
import { useCallback, useState } from "react";
import VideoJS from "../../../components/Player";
type AutoPlayerProps = { src: string; onClose?: VoidFunction };
const AutoPlayer = ({ src, onClose }: AutoPlayerProps) => {
  const [visible, setVisible] = useState(true);
  const onCloseFunc = useCallback(() => {
    setVisible(false);
    onClose?.();
  }, [onClose]);
  return (
    <Modal
      onCancel={onCloseFunc}
      visible={visible}
      destroyOnClose
      closable
      footer={null}
    >
      <VideoJS src={src}></VideoJS>
    </Modal>
  );
};

export default AutoPlayer;
