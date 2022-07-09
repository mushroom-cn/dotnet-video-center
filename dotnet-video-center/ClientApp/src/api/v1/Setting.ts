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

import {
  IMedia,
  IProblemDetails,
  ISettingCreateCreatePayload,
  ISettingDelete2Payload,
  ISettingIdsListParams,
  ISettingIdsUpdatePayload,
  ISettingTagCreatePayload,
  ISettingTagDeletePayload,
} from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Setting<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Setting
   * @name SettingDetail
   * @summary Get media description by media id.
   * @request GET:/v1/Setting/{id}
   * @response `200` `IMedia` Returns the media description.
   * @response `404` `IProblemDetails` Media description not found.
   */
  settingDetail = (id: string, params: RequestParams = {}) =>
    this.request<IMedia, IProblemDetails>({
      path: `/v1/Setting/${id}`,
      method: "GET",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Setting
   * @name SettingUpdate
   * @summary Modify media description.
   * @request PUT:/v1/Setting/{id}
   * @response `200` `IMedia` Success
   */
  settingUpdate = (id: string, data: IMedia, params: RequestParams = {}) =>
    this.request<IMedia, any>({
      path: `/v1/Setting/${id}`,
      method: "PUT",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Setting
   * @name SettingDelete
   * @summary Delete media description.
   * @request DELETE:/v1/Setting/{id}
   * @response `200` `void` Success
   */
  settingDelete = (id: string, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/v1/Setting/${id}`,
      method: "DELETE",
      ...params,
    });
  /**
   * No description
   *
   * @tags Setting
   * @name SettingIdsList
   * @summary Get media description collection by media ids.
   * @request GET:/v1/Setting/ids
   * @response `200` `(IMedia)[]` Returns the media description.
   * @response `404` `void` Media description not found.
   */
  settingIdsList = (query: ISettingIdsListParams, params: RequestParams = {}) =>
    this.request<IMedia[], void>({
      path: `/v1/Setting/ids`,
      method: "GET",
      query: query,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Setting
   * @name SettingIdsUpdate
   * @summary Batch modify media description.
   * @request PUT:/v1/Setting/ids
   * @response `200` `(IMedia)[]` Success
   */
  settingIdsUpdate = (data: ISettingIdsUpdatePayload, params: RequestParams = {}) =>
    this.request<IMedia[], any>({
      path: `/v1/Setting/ids`,
      method: "PUT",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Setting
   * @name SettingNameDetail
   * @summary Get media description collection by media names. support flex search
   * @request GET:/v1/Setting/name/{name}
   * @response `200` `(IMedia)[]` Returns the media description.
   * @response `404` `void` Media description not found.
   */
  settingNameDetail = (name: string, params: RequestParams = {}) =>
    this.request<IMedia[], void>({
      path: `/v1/Setting/name/${name}`,
      method: "GET",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Setting
   * @name SettingCreateCreate
   * @summary Batch create media description.
   * @request POST:/v1/Setting/create
   * @response `200` `(IMedia)[]` Success
   */
  settingCreateCreate = (data: ISettingCreateCreatePayload, params: RequestParams = {}) =>
    this.request<IMedia[], any>({
      path: `/v1/Setting/create`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Setting
   * @name SettingDelete2
   * @summary Batch delete media description.
   * @request DELETE:/v1/Setting
   * @originalName settingDelete
   * @duplicate
   * @response `200` `void` Success
   */
  settingDelete2 = (data: ISettingDelete2Payload, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/v1/Setting`,
      method: "DELETE",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags Setting
   * @name SettingTagDelete
   * @summary Unbind tag with media.
   * @request DELETE:/v1/Setting/{mediaId}/tag/{tagId}
   * @response `200` `void` Success
   */
  settingTagDelete = (mediaId: string, tagId: string, data: ISettingTagDeletePayload, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/v1/Setting/${mediaId}/tag/${tagId}`,
      method: "DELETE",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags Setting
   * @name SettingTagCreate
   * @summary Bind tag with media.
   * @request POST:/v1/Setting/{mediaId}/tag/{tagId}
   * @response `200` `void` Success
   */
  settingTagCreate = (mediaId: string, tagId: string, data: ISettingTagCreatePayload, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/v1/Setting/${mediaId}/tag/${tagId}`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      ...params,
    });
}
