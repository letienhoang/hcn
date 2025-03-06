import type { EntityDto } from '@abp/ng.core';

export interface CreateUpdateTagDto {
  name?: string;
  slug?: string;
  visibility: boolean;
}

export interface TagDto {
  name?: string;
  slug?: string;
  visibility: boolean;
  id?: string;
}

export interface TagInListDto extends EntityDto<string> {
  name?: string;
  slug?: string;
  visibility: boolean;
}
