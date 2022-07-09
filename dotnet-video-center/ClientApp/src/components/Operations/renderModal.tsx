import React from 'react';
import ReactDOM from 'react-dom/client';
type OperationResult<T> = {
  status: 'success' | 'close';
  data?: T;
};

type RenderParamFunc<T> = (p?: { data?: T }) => void;
type RenderFuncParam<T> = {
  onOk: RenderParamFunc<T>;
  onClose: RenderParamFunc<T>;
};

export function renderModal<T>(
  render: (param: RenderFuncParam<T>) => React.ReactElement
) {
  return new Promise<OperationResult<T>>((resolve) => {
    renderModalCore(({ onDestroy }) => {
      const onOk: RenderParamFunc<T> = (p) => {
        onDestroy();
        resolve({ status: 'success', data: p?.data });
      };
      const onClose: RenderParamFunc<T> = (p) => {
        onDestroy();
        resolve({ status: 'close', data: p?.data });
      };
      return render({ onOk, onClose });
    });
  });
}
function renderModalCore(
  render: ({ onDestroy }: { onDestroy: VoidFunction }) => React.ReactElement
) {
  const container = document.createDocumentFragment();
  const root = ReactDOM.createRoot(container);
  const onDestroy = () => {
    root.unmount();
  };
  root.render(render({ onDestroy }));
}
