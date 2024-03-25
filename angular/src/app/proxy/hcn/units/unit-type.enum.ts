import { mapEnumToOptions } from '@abp/ng.core';

export enum UnitType {
  Measure = 1,
  Currency = 2,
  Organizational = 3,
}

export const unitTypeOptions = mapEnumToOptions(UnitType);
