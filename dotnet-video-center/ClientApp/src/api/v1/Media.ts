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
  IMediaCreatePayload,
  IMediaDelete2Payload,
  IMediaIdsListParams,
  IMediaIdsUpdatePayload,
  IMediaTagCreatePayload,
  IMediaTagDeletePayload,
  IProblemDetails,
} from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Media<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Media
   * @name MediaDetail
   * @summary Get media description by media id.
   * @request GET:/v1/Media/{id}
   * @response `200` `IMedia` Returns the media description.
   * @response `404` `IProblemDetails` Media description not found.
   */
  mediaDetail = (id: string, params: RequestParams = {}) =>
    this.request<IMedia, IProblemDetails>({
      path: `/v1/Media/${id}`,
      method: "GET",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Media
   * @name MediaUpdate
   * @summary Modify media description.
   * @request PUT:/v1/Media/{id}
   * @response `200` `IMedia` Success
   */
  mediaUpdate = (id: string, data: IMedia, params: RequestParams = {}) =>
    this.request<IMedia, any>({
      path: `/v1/Media/${id}`,
      method: "PUT",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Media
   * @name MediaDelete
   * @summary Delete media description.
   * @request DELETE:/v1/Media/{id}
   * @response `200` `void` Success
   */
  mediaDelete = (id: string, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/v1/Media/${id}`,
      method: "DELETE",
      ...params,
    });
  /**
   * No description
   *
   * @tags Media
   * @name MediaIdsList
   * @summary Get media description collection by media ids.
   * @request GET:/v1/Media/ids
   * @response `200` `(IMedia)[]` Returns the media description.
   * @response `404` `void` Media description not found.
   */
  mediaIdsList = (query: IMediaIdsListParams, params: RequestParams = {}) =>
    this.request<IMedia[], void>({
      path: `/v1/Media/ids`,
      method: "GET",
      query: query,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Media
   * @name MediaIdsUpdate
   * @summary Batch modify media description.
   * @request PUT:/v1/Media/ids
   * @response `200` `(IMedia)[]` Success
   */
  mediaIdsUpdate = (data: IMediaIdsUpdatePayload, params: RequestParams = {}) =>
    this.request<IMedia[], any>({
      path: `/v1/Media/ids`,
      method: "PUT",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Media
   * @name MediaNameDetail
   * @summary Get media description collection by media names. support flex search
   * @request GET:/v1/Media/name/{name}
   * @response `200` `(IMedia)[]` Returns the media description.
   * @response `404` `void` Media description not found.
   */
  mediaNameDetail = (name: string, params: RequestParams = {}) =>
    this.request<IMedia[], void>({
      path: `/v1/Media/name/${name}`,
      method: "GET",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Media
   * @name MediaCreate
   * @summary Batch create media description.
   * @request POST:/v1/Media
   * @response `200` `(IMedia)[]` Success
   */
  mediaCreate = (data: IMediaCreatePayload, params: RequestParams = {}) =>
    this.request<IMedia[], any>({
      path: `/v1/Media`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Media
   * @name MediaDelete2
   * @summary Batch delete media description.
   * @request DELETE:/v1/Media
   * @originalName mediaDelete
   * @duplicate
   * @response `200` `void` Success
   */
  mediaDelete2 = (data: IMediaDelete2Payload, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/v1/Media`,
      method: "DELETE",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags Media
   * @name MediaTagDelete
   * @summary Unbind tag with media.
   * @request DELETE:/v1/Media/{mediaId}/tag/{tagId}
   * @response `200` `void` Success
   */
  mediaTagDelete = (mediaId: string, tagId: string, data: IMediaTagDeletePayload, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/v1/Media/${mediaId}/tag/${tagId}`,
      method: "DELETE",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags Media
   * @name MediaTagCreate
   * @summary Bind tag with media.
   * @request POST:/v1/Media/{mediaId}/tag/{tagId}
   * @response `200` `void` Success
   */
  mediaTagCreate = (mediaId: string, tagId: string, data: IMediaTagCreatePayload, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/v1/Media/${mediaId}/tag/${tagId}`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags Media
   * @name MediaConvertUpdate
   * @request PUT:/v1/Media/convert/{id}
   * @response `200` `string` Success
   */
  mediaConvertUpdate = (id: string, params: RequestParams = {}) =>
    this.request<string, any>({
      path: `/v1/Media/convert/${id}`,
      method: "PUT",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Media
   * @name MediaScanCreate
   * @request POST:/v1/Media/scan
   * @response `200` `void` Success
   */
  mediaScanCreate = (params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/v1/Media/scan`,
      method: "POST",
      ...params,
    });
}
