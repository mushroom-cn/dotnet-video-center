import api, { IMedia } from "@api";
import { Form, Modal, Select } from "antd";
import { useCallback, useState } from "react";
import { useTranslation } from "react-i18next";
import { useAsyncRetry } from "react-use";
import Notification from "../../../components/Notification";
type EditMediaTagProps = {
  media: IMedia;
  onOk?: VoidFunction;
  onClose?: VoidFunction;
};
const EditMediaTag = ({
  media,
  onOk: onOkFunc,
  onClose: onCloseFunc,
}: EditMediaTagProps) => {
  const [visible, setVisible] = useState(true);
  const { t } = useTranslation("myNamespace");
  const [form] = Form.useForm<{ tagId: string; mediaId: string }[]>();

  const { value = [], loading } = useAsyncRetry(async () => {
    if (media?.id) {
      return [];
    }
    const { data } = await api.tag.getTag([]);
    return data;
  }, []);

  const onClose = () => {
    setVisible(false);
    onCloseFunc?.();
  };
  const onFinish = useCallback(
    (values: { tagId: string; mediaId: string }[]) => {
      api.media
        .mediaCreate([])
        .then(() => {
          onOkFunc?.();
        })
        .catch((e) => {
          Notification.error(e.message);
        });
    },
    [onOkFunc, media]
  );

  const handleOk = () => {
    form.submit();
  };

  const handleChange = useCallback(
    (value: string[]) => {
      form.setFieldsValue(value.map((tagId) => ({ tagId, mediaId: media.id })));
    },
    [form, media]
  );

  return (
    <Modal
      title={t("添加标签")}
      visible={visible}
      onOk={handleOk}
      onCancel={onClose}
      destroyOnClose
      closable
    >
      <Form form={form} autoComplete="off" onFinish={onFinish}>
        <Form.Item label={t("名称")}>{media?.name}</Form.Item>
        <Form.Item label={t("标签")}>
          <Select
            mode="tags"
            placeholder={t("请选择标签")}
            value={media?.mediaTags
              ?.map(({ tag }) => tag?.name || "")
              ?.filter(Boolean)}
            onChange={handleChange}
            style={{ width: "100%" }}
            options={value.map(({ id, name: label }) => ({
              id,
              value: id,
              label,
            }))}
            loading={loading}
          ></Select>
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default EditMediaTag;
