import type { CreateUpdateToolDto, ToolDto, ToolInListDto, ToolListFilterDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { TagInListDto } from '../tags/models';

@Injectable({
  providedIn: 'root',
})
export class ToolsService {
  apiName = 'Default';
  

  create = (input: CreateUpdateToolDto) =>
    this.restService.request<any, ToolDto>({
      method: 'POST',
      url: '/api/app/tools',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/tools/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/tools/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, ToolDto>({
      method: 'GET',
      url: `/api/app/tools/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<ToolDto>>({
      method: 'GET',
      url: '/api/app/tools',
      params: { maxResultCount: input.maxResultCount, skipCount: input.skipCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, ToolInListDto[]>({
      method: 'GET',
      url: '/api/app/tools/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: ToolListFilterDto) =>
    this.restService.request<any, PagedResultDto<ToolInListDto>>({
      method: 'GET',
      url: '/api/app/tools/filter',
      params: { categoryId: input.categoryId, keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getSuggestNewCode = () =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/tools/suggest-new-code',
    },
    { apiName: this.apiName });
  

  getThumbnailImage = (fileName: string) =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/tools/thumbnail-image',
      params: { fileName },
    },
    { apiName: this.apiName });
  

  getToolTag = (toolId: string) =>
    this.restService.request<any, TagInListDto[]>({
      method: 'GET',
      url: `/api/app/tools/tool-tag/${toolId}`,
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateToolDto) =>
    this.restService.request<any, ToolDto>({
      method: 'PUT',
      url: `/api/app/tools/${id}`,
      body: input,
    },
    { apiName: this.apiName });
  

  updateToolTag = (toolId: string, toolTagList: string[]) =>
    this.restService.request<any, ToolDto>({
      method: 'PUT',
      url: `/api/app/tools/tool-tag/${toolId}`,
      body: toolTagList,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
