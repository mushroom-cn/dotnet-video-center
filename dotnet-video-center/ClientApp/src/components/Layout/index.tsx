import { GithubOutlined, MailOutlined } from "@ant-design/icons";
import { Button, Layout as BaseLayout, Popover } from "antd";
import { Content, Footer, Header } from "antd/lib/layout/layout";
import React from "react";
import { useTranslation } from "react-i18next";
import { Breadcrumb } from "../Breadcrumb";
import { CopyRight } from "../CopyRight";
import { useClassName } from "@less/hooks";
import { Language } from "../Language";
import { NavMenu } from "../NavMenu";
import "./layout.less";
type LayoutProps = {
  children?: React.ReactNode;
};
export function Layout({ children }: LayoutProps) {
  const { t } = useTranslation();
  const { clsPrefix } = useClassName();
  return (
    <BaseLayout className={`${clsPrefix}-layout ${clsPrefix}-app`}>
      <Header className={`${clsPrefix}-layout-header`}>
        <NavMenu />
      </Header>
      <Content className={`${clsPrefix}-layout-content`}>
        <Breadcrumb />
        {children}
      </Content>
      <Footer
        style={{ textAlign: "center" }}
        className={`${clsPrefix}-layout-footer`}
      >
        <Language />
        <CopyRight />
      </Footer>
    </BaseLayout>
  );
}
