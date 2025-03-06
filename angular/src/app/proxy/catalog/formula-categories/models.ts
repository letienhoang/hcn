import type { EntityDto } from '@abp/ng.core';

export interface CreateUpdateFormulaCategoryDto {
  name?: string;
  slug?: string;
  description?: string;
  visibility: boolean;
  keywordSEO?: string;
  descriptionSEO?: string;
  parentId?: string;
  coverPictureName?: string;
  coverPictureContent?: string;
}

export interface FormulaCategoryDto {
  name?: string;
  slug?: string;
  coverPicture?: string;
  description?: string;
  visibility: boolean;
  keywordSEO?: string;
  descriptionSEO?: string;
  parentId?: string;
  id?: string;
}

export interface FormulaCategoryInListDto extends EntityDto<string> {
  name?: string;
  slug?: string;
  coverPicture?: string;
  description?: string;
  visibility: boolean;
}
