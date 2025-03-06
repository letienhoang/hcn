import { mapEnumToOptions } from '@abp/ng.core';

export enum MaterialType {
  Animal = 1,
  Plants = 2,
  Chemical = 3,
  Virtual = 4,
}

export const materialTypeOptions = mapEnumToOptions(MaterialType);
