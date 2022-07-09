import { notification as baseNotification } from 'antd';

type NotificationType = 'success' | 'info' | 'warning' | 'error';

const openNotificationWithIcon = (
  type: NotificationType,
  { title, description }: { title: string; description: string }
) => {
  baseNotification[type]({
    message: title,
    description,
  });
};

export default {
  success: (opt: { title: string; description: string }) => {
    openNotificationWithIcon('success', opt);
  },
  info: (opt: { title: string; description: string }) => {
    openNotificationWithIcon('info', opt);
  },
  warning: (opt: { title: string; description: string }) => {
    openNotificationWithIcon('warning', opt);
  },
  error: (opt: { title: string; description: string }) => {
    openNotificationWithIcon('error', opt);
  },
};
