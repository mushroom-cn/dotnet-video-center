import { Empty as BaseEmpty } from 'antd';
type EmptyProps = {};
export function Empty(props: EmptyProps) {
  return <BaseEmpty image={BaseEmpty.PRESENTED_IMAGE_SIMPLE} />;
}
