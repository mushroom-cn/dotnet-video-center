/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

import { IDeleteTagPayload, IGetTagPayload, IPostTagPayload, IPutTagPayload, ITag } from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Tag<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Tag
   * @name TagIdDetail
   * @summary Get Tag info by tag id
   * @request GET:/v1/Tag/id/{id}
   * @response `200` `ITag` Success
   */
  tagIdDetail = (id: string, params: RequestParams = {}) =>
    this.request<ITag, any>({
      path: `/v1/Tag/id/${id}`,
      method: "GET",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Tag
   * @name GetTag
   * @summary Get Tag info collection by a group of tag id
   * @request GET:/v1/Tag
   * @response `200` `(ITag)[]` Success
   */
  getTag = (data: IGetTagPayload, params: RequestParams = {}) =>
    this.request<ITag[], any>({
      path: `/v1/Tag`,
      method: "GET",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Tag
   * @name PutTag
   * @summary Modify Tag
   * @request PUT:/v1/Tag
   * @response `200` `(ITag)[]` Success
   */
  putTag = (data: IPutTagPayload, params: RequestParams = {}) =>
    this.request<ITag[], any>({
      path: `/v1/Tag`,
      method: "PUT",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Tag
   * @name PostTag
   * @summary Create Tag
   * @request POST:/v1/Tag
   * @response `200` `(ITag)[]` Success
   */
  postTag = (data: IPostTagPayload, params: RequestParams = {}) =>
    this.request<ITag[], any>({
      path: `/v1/Tag`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Tag
   * @name DeleteTag
   * @summary Delete Tag by a group od id
   * @request DELETE:/v1/Tag
   * @response `200` `void` Success
   */
  deleteTag = (data: IDeleteTagPayload, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/v1/Tag`,
      method: "DELETE",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags Tag
   * @name TagNameDetail
   * @summary Get Tag info collection by tag name
   * @request GET:/v1/Tag/name/{name}
   * @response `200` `(ITag)[]` Success
   */
  tagNameDetail = (name: string, params: RequestParams = {}) =>
    this.request<ITag[], any>({
      path: `/v1/Tag/name/${name}`,
      method: "GET",
      format: "json",
      ...params,
    });
}
