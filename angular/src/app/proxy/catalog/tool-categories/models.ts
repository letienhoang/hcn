import type { EntityDto } from '@abp/ng.core';

export interface CreateUpdateToolCategoryDto {
  name?: string;
  slug?: string;
  description?: string;
  keywordSEO?: string;
  descriptionSEO?: string;
  parentId?: string;
  visibility: boolean;
  coverPictureName?: string;
  coverPictureContent?: string;
}

export interface ToolCategoryDto {
  name?: string;
  slug?: string;
  coverPicture?: string;
  description?: string;
  keywordSEO?: string;
  descriptionSEO?: string;
  parentId?: string;
  visibility: boolean;
  id?: string;
}

export interface ToolCategoryInListDto extends EntityDto<string> {
  name?: string;
  slug?: string;
  coverPicture?: string;
  description?: string;
  visibility: boolean;
}
