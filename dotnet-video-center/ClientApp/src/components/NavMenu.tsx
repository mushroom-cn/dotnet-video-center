import {
  CloudUploadOutlined,
  HomeOutlined,
  MailOutlined,
  SettingOutlined,
} from '@ant-design/icons';
import { Menu } from 'antd';
import { useTranslation } from 'react-i18next';
import { useTheme } from './hooks/useTheme';
import { LinkTo } from './LinkTo';
import './NavMenu.css';
export function NavMenu() {
  const { theme } = useTheme();
  const { t } = useTranslation();
  return (
    <>
      <Menu
        theme={theme as any}
        mode="horizontal"
        defaultSelectedKeys={['home']}
        items={[
          {
            key: 'home',
            icon: <HomeOutlined />,
            label: <LinkTo to="/">{t('主页')}</LinkTo>,
          },
          {
            key: 'upload',
            icon: <CloudUploadOutlined />,
            label: <LinkTo to="/upload">{t('上传')}</LinkTo>,
          },
          {
            key: 'setting',
            icon: <SettingOutlined />,
            label: <LinkTo to="/setting">{t('设置')}</LinkTo>,
          },
        ]}
      />
    </>
  );
}
