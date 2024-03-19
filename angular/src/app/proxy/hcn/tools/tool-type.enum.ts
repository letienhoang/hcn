import { mapEnumToOptions } from '@abp/ng.core';

export enum ToolType {
  Utensils = 1,
  Kitchenware = 2,
  ChemicalInstruments = 3,
  Learning = 4,
  Crafts = 5,
  Virtual = 6,
}

export const toolTypeOptions = mapEnumToOptions(ToolType);
