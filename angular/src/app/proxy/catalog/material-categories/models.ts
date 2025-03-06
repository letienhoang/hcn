import type { EntityDto } from '@abp/ng.core';

export interface CreateUpdateMaterialCategoryDto {
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

export interface MaterialCategoryDto {
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

export interface MaterialCategoryInListDto extends EntityDto<string> {
  name?: string;
  slug?: string;
  coverPicture?: string;
  description?: string;
  visibility: boolean;
}
