import { Link } from 'react-router-dom';
type LinkToProps = {
  to: string;
  useSearchParam?: boolean;
  children?: React.ReactNode;
};
export function LinkTo({ to, useSearchParam = true, children }: LinkToProps) {
  return (
    <Link
      to={[to, useSearchParam ? location.search : ''].filter(Boolean).join('')}
    >
      {children}
    </Link>
  );
}
