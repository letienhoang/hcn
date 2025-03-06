import { mapEnumToOptions } from '@abp/ng.core';

export enum FormulaType {
  Simple = 1,
  Complex = 2,
  Elaborate = 3,
  Variant = 4,
}

export const formulaTypeOptions = mapEnumToOptions(FormulaType);
