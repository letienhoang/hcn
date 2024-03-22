import type { Level } from '../../hcn/formulas/level.enum';
import type { FormulaType } from '../../hcn/formulas/formula-type.enum';
import type { EntityDto } from '@abp/ng.core';
import type { BaseListFilterDto } from '../../models';

export interface CreateUpdateFormulaDto {
  name?: string;
  slug?: string;
  code?: string;
  categoryId?: string;
  level: Level;
  formulaType: FormulaType;
  executionTime: number;
  briefContent?: string;
  description?: string;
  liked: number;
  viewCount: number;
  sortOrder: number;
  visibility: boolean;
  videoUrl?: string;
  referenceSource?: string;
  keywordSEO?: string;
  descriptionSEO?: string;
  coverPictureName?: string;
  coverPictureContent?: string;
  parentId?: string;
}

export interface FormulaDto {
  name?: string;
  slug?: string;
  code?: string;
  categoryId?: string;
  level: Level;
  formulaType: FormulaType;
  executionTime: number;
  thumbnailPicture?: string;
  briefContent?: string;
  description?: string;
  liked: number;
  viewCount: number;
  sortOrder: number;
  visibility: boolean;
  videoUrl?: string;
  referenceSource?: string;
  keywordSEO?: string;
  descriptionSEO?: string;
  id?: string;
  parentId?: string;
}

export interface FormulaInListDto extends EntityDto<string> {
  name?: string;
  code?: string;
  thumbnailPicture?: string;
  liked: number;
  viewCount: number;
  sortOrder: number;
  visibility: boolean;
  categoryName?: string;
  categorySlug?: string;
}

export interface FormulaListFilterDto extends BaseListFilterDto {
  categoryId?: string;
}
