import type { CreateUpdateUnitDto, UnitDto, UnitInListDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BaseListFilterDto } from '../../models';

@Injectable({
  providedIn: 'root',
})
export class UnitsService {
  apiName = 'Default';
  

  create = (input: CreateUpdateUnitDto) =>
    this.restService.request<any, UnitDto>({
      method: 'POST',
      url: '/api/app/units',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/units/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/units/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, UnitDto>({
      method: 'GET',
      url: `/api/app/units/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<UnitDto>>({
      method: 'GET',
      url: '/api/app/units',
      params: { maxResultCount: input.maxResultCount, skipCount: input.skipCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, UnitInListDto[]>({
      method: 'GET',
      url: '/api/app/units/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: BaseListFilterDto) =>
    this.restService.request<any, PagedResultDto<UnitInListDto>>({
      method: 'GET',
      url: '/api/app/units/filter',
      params: { keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateUnitDto) =>
    this.restService.request<any, UnitDto>({
      method: 'PUT',
      url: `/api/app/units/${id}`,
      body: input,
    },
    { apiName: this.apiName });
  

  updateVisibility = (unitId: string, visibility: boolean) =>
    this.restService.request<any, UnitDto>({
      method: 'PUT',
      url: `/api/app/units/visibility/${unitId}`,
      params: { visibility },
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
