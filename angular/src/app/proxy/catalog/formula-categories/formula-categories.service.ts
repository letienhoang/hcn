import type { CreateUpdateFormulaCategoryDto, FormulaCategoryDto, FormulaCategoryInListDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BaseListFilterDto } from '../../models';

@Injectable({
  providedIn: 'root',
})
export class FormulaCategoriesService {
  apiName = 'Default';
  

  create = (input: CreateUpdateFormulaCategoryDto) =>
    this.restService.request<any, FormulaCategoryDto>({
      method: 'POST',
      url: '/api/app/formula-categories',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/formula-categories/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/formula-categories/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, FormulaCategoryDto>({
      method: 'GET',
      url: `/api/app/formula-categories/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<FormulaCategoryDto>>({
      method: 'GET',
      url: '/api/app/formula-categories',
      params: { maxResultCount: input.maxResultCount, skipCount: input.skipCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, FormulaCategoryInListDto[]>({
      method: 'GET',
      url: '/api/app/formula-categories/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: BaseListFilterDto) =>
    this.restService.request<any, PagedResultDto<FormulaCategoryInListDto>>({
      method: 'GET',
      url: '/api/app/formula-categories/filter',
      params: { keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getThumbnailImage = (fileName: string) =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/formula-categories/thumbnail-image',
      params: { fileName },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateFormulaCategoryDto) =>
    this.restService.request<any, FormulaCategoryDto>({
      method: 'PUT',
      url: `/api/app/formula-categories/${id}`,
      body: input,
    },
    { apiName: this.apiName });
  

  updateVisibility = (formulaCategoryId: string, visibility: boolean) =>
    this.restService.request<any, FormulaCategoryDto>({
      method: 'PUT',
      url: `/api/app/formula-categories/visibility/${formulaCategoryId}`,
      params: { visibility },
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
