export * from "./v1/data-contracts";
import { Media } from "./v1/Media";
import { Setting } from "./v1/Setting";
import { Tag } from "./v1/Tag";
const baseOpt = { baseUrl: SERVER_HOST };
export default {
  media: new Media({ ...baseOpt }),
  tag: new Tag({ ...baseOpt }),
  setting: new Setting({ ...baseOpt }),
};
