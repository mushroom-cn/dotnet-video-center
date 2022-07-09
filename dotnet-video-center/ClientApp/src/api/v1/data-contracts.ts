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

export interface IMedia {
  /** @format uuid */
  id?: string;
  name: string;
  path: string;
  mediaType: string;

  /** @format date-time */
  createTime?: string;

  /** @format date-time */
  lastModifyTime?: string;
  description?: string | null;
  mediaTags?: IMediaTag[] | null;
  iconUrl: string;
  url?: string | null;
}

export interface IMediaTag {
  /** @format date-time */
  publicationDate?: string;

  /** @format uuid */
  tagId: string;
  tag?: ITag;

  /** @format uuid */
  mediaId: string;
  media?: IMedia;
}

export interface IProblemDetails {
  type?: string | null;
  title?: string | null;

  /** @format int32 */
  status?: number | null;
  detail?: string | null;
  instance?: string | null;
}

export interface ITag {
  /** @format uuid */
  id?: string;
  name: string;

  /** @format date-time */
  createTime?: string;

  /** @format date-time */
  lastModifyTime?: string;
  mediaTags?: IMediaTag[] | null;
}

export interface IMediaIdsListParams {
  /** A group of media id */
  ids?: string[];
}

export type IMediaIdsUpdatePayload = IMedia[];

export type IMediaCreatePayload = IMedia[];

export type IMediaDelete2Payload = string[];

export type IMediaTagDeletePayload = string[];

export type IMediaTagCreatePayload = string[];

export interface ISettingIdsListParams {
  /** A group of media id */
  ids?: string[];
}

export type ISettingIdsUpdatePayload = IMedia[];

export type ISettingCreateCreatePayload = IMedia[];

export type ISettingDelete2Payload = string[];

export type ISettingTagDeletePayload = string[];

export type ISettingTagCreatePayload = string[];

export type IGetTagPayload = string[];

export type IPutTagPayload = ITag[];

export type IPostTagPayload = ITag[];

export type IDeleteTagPayload = string[];
