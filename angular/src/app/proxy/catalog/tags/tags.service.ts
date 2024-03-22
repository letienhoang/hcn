import type { CreateUpdateTagDto, TagDto, TagInListDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BaseListFilterDto } from '../../models';

@Injectable({
  providedIn: 'root',
})
export class TagsService {
  apiName = 'Default';
  

  create = (input: CreateUpdateTagDto) =>
    this.restService.request<any, TagDto>({
      method: 'POST',
      url: '/api/app/tags',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/tags/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/tags/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, TagDto>({
      method: 'GET',
      url: `/api/app/tags/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<TagDto>>({
      method: 'GET',
      url: '/api/app/tags',
      params: { maxResultCount: input.maxResultCount, skipCount: input.skipCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, TagInListDto[]>({
      method: 'GET',
      url: '/api/app/tags/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: BaseListFilterDto) =>
    this.restService.request<any, PagedResultDto<TagInListDto>>({
      method: 'GET',
      url: '/api/app/tags/filter',
      params: { keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateTagDto) =>
    this.restService.request<any, TagDto>({
      method: 'PUT',
      url: `/api/app/tags/${id}`,
      body: input,
    },
    { apiName: this.apiName });
  

  updateVisibility = (tagId: string, visibility: boolean) =>
    this.restService.request<any, TagDto>({
      method: 'PUT',
      url: `/api/app/tags/visibility/${tagId}`,
      params: { visibility },
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
