import { Breadcrumb as BaseBreadcrumb } from 'antd';
import { t } from 'i18next';
import { useLocation } from 'react-router-dom';
import { LinkTo } from '../LinkTo';

const breadcrumbNameMap: Record<string, string> = {
  '/': t('主页'),
  '/upload': t('上传'),
  '/setting': t('设置'),
  '/setting/system': t('设置'),
  '/setting/appearance': t('设置'),
};

export const Breadcrumb = () => {
  const location = useLocation();
  const pathSnippets = location.pathname.split('/').filter((i) => i);
  const extraBreadcrumbItems = pathSnippets.map((_, index) => {
    const url = `/${pathSnippets.slice(0, index + 1).join('/')}`;
    return (
      <BaseBreadcrumb.Item key={url}>
        <LinkTo to={url}>{breadcrumbNameMap[url]}</LinkTo>
      </BaseBreadcrumb.Item>
    );
  });

  return <BaseBreadcrumb>{extraBreadcrumbItems}</BaseBreadcrumb>;
};
