import { SkinOutlined, ToolOutlined } from "@ant-design/icons";
import { Tabs } from "antd";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { useClassName } from "@less/hooks";
import { AppearanceSettings } from "./AppearanceSettings";
import { SystemSettings } from "./SystemSettings";

export function Settings() {
  const { t } = useTranslation();
  const [activeKey, setActiveKey] = useState("System");
  const { clsPrefix } = useClassName();
  return (
    <Tabs
      activeKey={activeKey}
      onChange={setActiveKey}
      className={`${clsPrefix}-tabs`}
    >
      <Tabs.TabPane
        tab={
          <span>
            <ToolOutlined />
            {t("系统设置")}
          </span>
        }
        key="System"
      >
        <SystemSettings />
      </Tabs.TabPane>
      <Tabs.TabPane
        tab={
          <span>
            <SkinOutlined />
            {t("外观设置")}
          </span>
        }
        key="Appearance"
      >
        <AppearanceSettings />
      </Tabs.TabPane>
    </Tabs>
  );
}
