declare module "*.scss" {
  const content: Record<string, string>;
  export default content;
}
declare module "*.less" {
  const content: Record<string, string>;
  export default content;
}

const SERVER_HOST: string;
