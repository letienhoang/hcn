import { mapEnumToOptions } from '@abp/ng.core';

export enum Level {
  Beginner = 1,
  Elementary = 2,
  Intermendiate = 3,
  UpperIntermendiate = 4,
  Advanced = 5,
  Proficient = 6,
}

export const levelOptions = mapEnumToOptions(Level);
