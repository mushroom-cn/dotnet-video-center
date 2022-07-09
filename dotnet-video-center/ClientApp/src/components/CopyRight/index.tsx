import { GithubOutlined, MailOutlined } from '@ant-design/icons';
import { Popover } from 'antd';
import { useTranslation } from 'react-i18next';

export function CopyRight() {
  const { t } = useTranslation();
  return (
    <>
      ©2022
      {t('由YoungMushroom创建')}
      <MailOutlined />
      <Popover content={t('跳转到GitHub')}>
        <a
          target="_blank"
          href="https://github.com/mushroom-cn/dotnet-video-center"
        >
          <GithubOutlined />
        </a>
      </Popover>
    </>
  );
}
