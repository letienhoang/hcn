import type { MaterialType } from '../../hcn/materials/material-type.enum';
import type { EntityDto } from '@abp/ng.core';
import type { BaseListFilterDto } from '../../models';

export interface CreateUpdateMaterialDto {
  name?: string;
  slug?: string;
  code?: string;
  categoryId?: string;
  materialType: MaterialType;
  description?: string;
  pictures?: string;
  keywordSEO?: string;
  descriptionSEO?: string;
  parentId?: string;
  visibility: boolean;
  coverPictureName?: string;
  coverPictureContent?: string;
}

export interface MaterialDto {
  name?: string;
  slug?: string;
  code?: string;
  categoryId?: string;
  materialType: MaterialType;
  description?: string;
  thumbnailPicture?: string;
  pictures?: string;
  keywordSEO?: string;
  descriptionSEO?: string;
  parentId?: string;
  visibility: boolean;
  id?: string;
}

export interface MaterialInListDto extends EntityDto<string> {
  name?: string;
  code?: string;
  thumbnailPicture?: string;
  visibility: boolean;
  categoryName?: string;
  categorySlug?: string;
}

export interface MaterialListFilterDto extends BaseListFilterDto {
  categoryId?: string;
}
