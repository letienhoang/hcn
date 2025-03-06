import type { UnitType } from '../../hcn/units/unit-type.enum';
import type { EntityDto } from '@abp/ng.core';

export interface CreateUpdateUnitDto {
  name?: string;
  unitType: UnitType;
  briefContent?: string;
  visibility: boolean;
}

export interface UnitDto {
  name?: string;
  unitType: UnitType;
  briefContent?: string;
  visibility: boolean;
  id?: string;
}

export interface UnitInListDto extends EntityDto<string> {
  name?: string;
  unitType: UnitType;
  briefContent?: string;
  visibility: boolean;
}
