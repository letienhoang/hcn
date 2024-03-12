import type { EntityDto } from '@abp/ng.core';

export interface CreateUpdateTopicDto {
  name?: string;
  slug?: string;
  code?: string;
  description?: string;
  keywordSEO?: string;
  descriptionSEO?: string;
  parentId?: string;
  visibility: boolean;
  coverPictureName?: string;
  coverPictureContent?: string;
}

export interface TopicDto {
  name?: string;
  slug?: string;
  code?: string;
  coverPicture?: string;
  description?: string;
  keywordSEO?: string;
  descriptionSEO?: string;
  parentId?: string;
  visibility: boolean;
  id?: string;
}

export interface TopicInListDto extends EntityDto<string> {
  name?: string;
  slug?: string;
  code?: string;
  coverPicture?: string;
  visibility: boolean;
}
