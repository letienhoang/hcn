import type { CreateUpdateToolCategoryDto, ToolCategoryDto, ToolCategoryInListDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BaseListFilterDto } from '../../models';

@Injectable({
  providedIn: 'root',
})
export class ToolCategoriesService {
  apiName = 'Default';
  

  create = (input: CreateUpdateToolCategoryDto) =>
    this.restService.request<any, ToolCategoryDto>({
      method: 'POST',
      url: '/api/app/tool-categories',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/tool-categories/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/tool-categories/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, ToolCategoryDto>({
      method: 'GET',
      url: `/api/app/tool-categories/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<ToolCategoryDto>>({
      method: 'GET',
      url: '/api/app/tool-categories',
      params: { maxResultCount: input.maxResultCount, skipCount: input.skipCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, ToolCategoryInListDto[]>({
      method: 'GET',
      url: '/api/app/tool-categories/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: BaseListFilterDto) =>
    this.restService.request<any, PagedResultDto<ToolCategoryInListDto>>({
      method: 'GET',
      url: '/api/app/tool-categories/filter',
      params: { keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getThumbnailImage = (fileName: string) =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/tool-categories/thumbnail-image',
      params: { fileName },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateToolCategoryDto) =>
    this.restService.request<any, ToolCategoryDto>({
      method: 'PUT',
      url: `/api/app/tool-categories/${id}`,
      body: input,
    },
    { apiName: this.apiName });
  

  updateVisibility = (toolCategoryId: string, visibility: boolean) =>
    this.restService.request<any, ToolCategoryDto>({
      method: 'PUT',
      url: `/api/app/tool-categories/visibility/${toolCategoryId}`,
      params: { visibility },
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
