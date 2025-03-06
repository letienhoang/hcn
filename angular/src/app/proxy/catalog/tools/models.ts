import type { ToolType } from '../../hcn/tools/tool-type.enum';
import type { EntityDto } from '@abp/ng.core';
import type { BaseListFilterDto } from '../../models';

export interface CreateUpdateToolDto {
  name?: string;
  slug?: string;
  code?: string;
  categoryId?: string;
  toolType: ToolType;
  description?: string;
  pictures?: string;
  keywordSEO?: string;
  descriptionSEO?: string;
  parentId?: string;
  visibility: boolean;
  coverPictureName?: string;
  coverPictureContent?: string;
}

export interface ToolDto {
  name?: string;
  slug?: string;
  code?: string;
  categoryId?: string;
  toolType: ToolType;
  description?: string;
  thumbnailPicture?: string;
  pictures?: string;
  keywordSEO?: string;
  descriptionSEO?: string;
  parentId?: string;
  visibility: boolean;
  id?: string;
}

export interface ToolInListDto extends EntityDto<string> {
  name?: string;
  code?: string;
  thumbnailPicture?: string;
  visibility: boolean;
  categoryName?: string;
  categorySlug?: string;
}

export interface ToolListFilterDto extends BaseListFilterDto {
  categoryId?: string;
}
