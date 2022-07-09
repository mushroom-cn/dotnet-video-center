import { useClassName } from "@less/hooks";
import { Button, Select } from "antd";
import i18next from "i18next";
import { useCallback, useMemo } from "react";
import { useTranslation } from "react-i18next";

export function Language() {
  const { t, i18n } = useTranslation();
  const options = useMemo(
    () => [
      {
        id: "zh",
        label: t("中文简体"),
        value: "zh",
      },
      {
        id: "en",
        label: t("English"),
        value: "en",
      },
    ],
    [t]
  );
  const { clsPrefix } = useClassName();

  const handleChange = useCallback((v: string) => {
    i18next.changeLanguage(v);
  }, []);

  return (
    <Select
      className={`${clsPrefix}-language`}
      options={options}
      value={i18n.language}
      style={{ width: 120 }}
      onChange={handleChange}
    >
      <Button>{t("xx")}</Button>
    </Select>
  );
}
