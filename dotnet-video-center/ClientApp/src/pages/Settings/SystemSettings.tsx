import {
  InfoCircleOutlined,
  MinusCircleOutlined,
  PlusOutlined,
} from "@ant-design/icons";
import { Button, Form, Input, Popover } from "antd";
import { useForm } from "antd/lib/form/Form";
import { useCallback } from "react";
import { useTranslation } from "react-i18next";
import { useMount } from "react-use";
import { useClassName } from "@less/hooks";
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
export function SystemSettings() {
  const { t } = useTranslation();
  const { clsPrefix } = useClassName();
  const [form] = useForm();
  useMount(() => {
    // init form
    form.setFieldsValue({ dir: [""] });
  });

  const onFinishFunc = useCallback((values: any) => {
    console.debug(values);
  }, []);

  return (
    <Form
      form={form}
      onFinish={onFinishFunc}
      {...formItemLayoutWithOutLabel}
      className={`${clsPrefix}-system-setting-form`}
    >
      <Form.List
        name="dir"
        rules={[
          {
            validator: async (_, names) => {
              if (names?.length < 1) {
                return Promise.reject(new Error(t("至少选择一项")));
              }
            },
          },
        ]}
      >
        {(fields, { add, remove }, { errors }) => (
          <>
            {fields.map((field, index) => (
              <Form.Item
                labelAlign="left"
                {...(index === 0 ? formItemLayout : formItemLayoutWithOutLabel)}
                label={index === 0 ? t("任务扫描根目录") : ""}
                required={false}
                key={field.key}
              >
                <Form.Item
                  {...field}
                  validateTrigger={["onChange", "onBlur"]}
                  rules={[
                    {
                      required: true,
                      whitespace: true,
                      message: "请输入目录或者删掉本项",
                    },
                  ]}
                  noStyle
                >
                  <Input
                    placeholder={t("请输入目录")}
                    style={{ width: "60%" }}
                  />
                </Form.Item>
                {fields.length > 1 ? (
                  <MinusCircleOutlined
                    className="dynamic-delete-button"
                    onClick={() => remove(field.name)}
                  />
                ) : null}
              </Form.Item>
            ))}
            <Form.Item>
              <Button
                type="dashed"
                onClick={() => add()}
                style={{ width: "60%" }}
                icon={<PlusOutlined />}
              >
                {t("添加目录")}
              </Button>
              <Form.ErrorList errors={errors} />
            </Form.Item>
          </>
        )}
      </Form.List>
      <Form.Item
        name="job"
        labelAlign="left"
        label={
          <>
            {t("定时任务扫描时间")}
            <Popover
              content={
                <>
                  <a title="" href="">
                    {t("参考文档")}
                  </a>
                </>
              }
            >
              <InfoCircleOutlined />
            </Popover>
          </>
        }
        {...formItemLayout}
      >
        <Input placeholder="请输入表达式" size="middle" />
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
