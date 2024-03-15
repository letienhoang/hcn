import type { EntityDto } from '@abp/ng.core';
import type { BaseListFilterDto } from '../../models';

export interface CreateUpdateStoryDto {
  title?: string;
  slug?: string;
  code?: string;
  briefContent?: string;
  content?: string;
  pictures?: string;
  liked: number;
  viewCount: number;
  sortOrder: number;
  visibility: boolean;
  referenceSource?: string;
  keywordSEO?: string;
  descriptionSEO?: string;
  topicId?: string;
  coverPictureName?: string;
  coverPictureContent?: string;
  tags: string[];
}

export interface StoryDto {
  title?: string;
  slug?: string;
  code?: string;
  briefContent?: string;
  content?: string;
  thumbnailPicture?: string;
  pictures?: string;
  liked: number;
  viewCount: number;
  sortOrder: number;
  visibility: boolean;
  referenceSource?: string;
  keywordSEO?: string;
  descriptionSEO?: string;
  topicId?: string;
  id?: string;
}

export interface StoryInListDto extends EntityDto<string> {
  title?: string;
  code?: string;
  thumbnailPicture?: string;
  liked: number;
  viewCount: number;
  sortOrder: number;
  visibility: boolean;
  topicName?: string;
  topicSlug?: string;
}

export interface StoryListFilterDto extends BaseListFilterDto {
  topicId?: string;
}
