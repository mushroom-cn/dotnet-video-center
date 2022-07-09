import { InboxOutlined } from '@ant-design/icons';
import Dragger from 'antd/lib/upload/Dragger';
import { useTranslation } from 'react-i18next';

export function DragUpload() {
  const { t } = useTranslation();
  return (
    <Dragger multiple showUploadList height={500}>
      <p className="ant-upload-drag-icon">
        <InboxOutlined />
      </p>
      <p className="ant-upload-text">{t('若拽本地文件即刻上传。')}</p>
      <p className="ant-upload-hint">{t('支持批量上传')}</p>
    </Dragger>
  );
}
