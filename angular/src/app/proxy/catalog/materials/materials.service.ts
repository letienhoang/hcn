import type { CreateUpdateMaterialDto, MaterialDto, MaterialInListDto, MaterialListFilterDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { TagInListDto } from '../tags/models';

@Injectable({
  providedIn: 'root',
})
export class MaterialsService {
  apiName = 'Default';
  

  create = (input: CreateUpdateMaterialDto) =>
    this.restService.request<any, MaterialDto>({
      method: 'POST',
      url: '/api/app/materials',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/materials/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/materials/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, MaterialDto>({
      method: 'GET',
      url: `/api/app/materials/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<MaterialDto>>({
      method: 'GET',
      url: '/api/app/materials',
      params: { maxResultCount: input.maxResultCount, skipCount: input.skipCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, MaterialInListDto[]>({
      method: 'GET',
      url: '/api/app/materials/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: MaterialListFilterDto) =>
    this.restService.request<any, PagedResultDto<MaterialInListDto>>({
      method: 'GET',
      url: '/api/app/materials/filter',
      params: { categoryId: input.categoryId, keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getMaterialTag = (materialId: string) =>
    this.restService.request<any, TagInListDto[]>({
      method: 'GET',
      url: `/api/app/materials/material-tag/${materialId}`,
    },
    { apiName: this.apiName });
  

  getSuggestNewCode = () =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/materials/suggest-new-code',
    },
    { apiName: this.apiName });
  

  getThumbnailImage = (fileName: string) =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/materials/thumbnail-image',
      params: { fileName },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateMaterialDto) =>
    this.restService.request<any, MaterialDto>({
      method: 'PUT',
      url: `/api/app/materials/${id}`,
      body: input,
    },
    { apiName: this.apiName });
  

  updateMaterialTag = (materialId: string, materialTagList: string[]) =>
    this.restService.request<any, MaterialDto>({
      method: 'PUT',
      url: `/api/app/materials/material-tag/${materialId}`,
      body: materialTagList,
    },
    { apiName: this.apiName });
  

  updateVisibility = (materialId: string, visibility: boolean) =>
    this.restService.request<any, MaterialDto>({
      method: 'PUT',
      url: `/api/app/materials/visibility/${materialId}`,
      params: { visibility },
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
