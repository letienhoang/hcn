import type { CreateUpdateMaterialCategoryDto, MaterialCategoryDto, MaterialCategoryInListDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BaseListFilterDto } from '../../models';

@Injectable({
  providedIn: 'root',
})
export class MaterialCategoriesService {
  apiName = 'Default';
  

  create = (input: CreateUpdateMaterialCategoryDto) =>
    this.restService.request<any, MaterialCategoryDto>({
      method: 'POST',
      url: '/api/app/material-categories',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/material-categories/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/material-categories/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, MaterialCategoryDto>({
      method: 'GET',
      url: `/api/app/material-categories/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<MaterialCategoryDto>>({
      method: 'GET',
      url: '/api/app/material-categories',
      params: { maxResultCount: input.maxResultCount, skipCount: input.skipCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, MaterialCategoryInListDto[]>({
      method: 'GET',
      url: '/api/app/material-categories/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: BaseListFilterDto) =>
    this.restService.request<any, PagedResultDto<MaterialCategoryInListDto>>({
      method: 'GET',
      url: '/api/app/material-categories/filter',
      params: { keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getThumbnailImage = (fileName: string) =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/material-categories/thumbnail-image',
      params: { fileName },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateMaterialCategoryDto) =>
    this.restService.request<any, MaterialCategoryDto>({
      method: 'PUT',
      url: `/api/app/material-categories/${id}`,
      body: input,
    },
    { apiName: this.apiName });
  

  updateVisibility = (formulaCategoryId: string, visibility: boolean) =>
    this.restService.request<any, MaterialCategoryDto>({
      method: 'PUT',
      url: `/api/app/material-categories/visibility/${formulaCategoryId}`,
      params: { visibility },
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
