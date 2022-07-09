import { Button, Form, Radio, RadioChangeEvent } from "antd";
import { useForm } from "antd/lib/form/Form";
import { useCallback } from "react";
import { useTranslation } from "react-i18next";
import { useMount } from "react-use";
import { useTheme } from "../../components/hooks";
const formItemLayout = {
  labelCol: {
    xs: { span: 24 },
    sm: { span: 4 },
  },
  wrapperCol: {
    xs: { span: 24 },
    sm: { span: 20 },
  },
};
const formItemLayoutWithOutLabel = {
  wrapperCol: {
    xs: { span: 24, offset: 0 },
    sm: { span: 20, offset: 4 },
  },
};
export function AppearanceSettings() {
  const { t } = useTranslation();
  const { theme, setTheme } = useTheme();
  const [form] = useForm();
  useMount(() => {
    // init form
    form.setFieldsValue({ theme });
  });

  const onChange = (e: RadioChangeEvent) => {
    const value = e.target.value;
    setTheme(value === "dark" ? "dark" : "light");
  };

  const onFinishFunc = useCallback((values: any) => {
    console.debug(values);
  }, []);

  return (
    <Form form={form} onFinish={onFinishFunc} {...formItemLayoutWithOutLabel}>
      <Form.Item
        labelAlign="left"
        name="theme"
        label={t("主题")}
        {...formItemLayout}
      >
        <Radio.Group onChange={onChange} value={theme}>
          <Radio value={"light"}>{t("亮色")}</Radio>
          <Radio value={"dark"}>{t("深色")}</Radio>
        </Radio.Group>
      </Form.Item>
      <Form.Item>
        <Button type="primary" htmlType="submit">
          {t("确定")}
        </Button>
        <Button type="default">{t("取消")}</Button>
      </Form.Item>
    </Form>
  );
}
