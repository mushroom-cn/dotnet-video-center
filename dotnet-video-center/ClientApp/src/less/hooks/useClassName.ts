import styles from "../../less/vars.less";
export const CLS_PREFIX = styles.clsPrefix;
export function useClassName() {
  return { clsPrefix: CLS_PREFIX };
}
