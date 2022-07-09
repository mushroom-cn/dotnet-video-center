import api from "@api";
import { useClassName } from "@less/hooks";
import { Button, List } from "antd";
import ButtonGroup from "antd/lib/button/button-group";
import { ListGridType } from "antd/lib/list";
import React, { useCallback, useMemo } from "react";
import { useTranslation } from "react-i18next";
import { useAsyncRetry } from "react-use";
import { Video } from "./Video";
import utilStyle from "@less/utils/index.less";

export function MediaList() {
  const { t } = useTranslation();
  const { value, loading, error, retry } = useAsyncRetry(async () => {
    const { data } = await api.media.mediaIdsList({ ids: [] });
    return data;
  });
  const gridProps: ListGridType = {
    gutter: 16,
    xs: 2,
    sm: 3,
    md: 4,
    lg: 5,
    xl: 6,
    xxl: 7,
  };
  const { clsPrefix } = useClassName();
  const onRefresh = useCallback(
    (e: React.MouseEvent<HTMLButtonElement>) => {
      retry();
      e.stopPropagation();
    },
    [retry]
  );
  const onReScan = useCallback(
    (e: React.MouseEvent<HTMLButtonElement>) => {
      e.stopPropagation();
      api.media.mediaScanCreate();
    },
    [retry]
  );

  const btnGroup = useMemo(
    () => [
      {
        key: "refresh",
        label: t("刷新"),
        type: "primary",
        onClick: onRefresh,
      },
      {
        key: "rescan",
        label: t("重新扫描"),
        onClick: onReScan,
      },
    ],
    [onReScan, onRefresh]
  );
  return (
    <>
      <ButtonGroup className={utilStyle[`${clsPrefix}-margin-y-normal`]}>
        {btnGroup.map(({ type, label, onClick, key }) => (
          <Button key={key} type={(type as any) || "default"} onClick={onClick}>
            {label}
          </Button>
        ))}
      </ButtonGroup>
      <List
        className={`${clsPrefix}-media-list`}
        grid={gridProps}
        dataSource={value ?? []}
        rowKey={"id"}
        renderItem={(item) => (
          <List.Item key={item.id}>
            <Video media={item} loading={loading} onRefresh={retry} />
          </List.Item>
        )}
      />
    </>
  );
}
