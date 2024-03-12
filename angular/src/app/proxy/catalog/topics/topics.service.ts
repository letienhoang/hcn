import type { CreateUpdateTopicDto, TopicDto, TopicInListDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BaseListFilterDto } from '../../models';

@Injectable({
  providedIn: 'root',
})
export class TopicsService {
  apiName = 'Default';
  

  create = (input: CreateUpdateTopicDto) =>
    this.restService.request<any, TopicDto>({
      method: 'POST',
      url: '/api/app/topics',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/topics/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/topics/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, TopicDto>({
      method: 'GET',
      url: `/api/app/topics/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<TopicDto>>({
      method: 'GET',
      url: '/api/app/topics',
      params: { maxResultCount: input.maxResultCount, skipCount: input.skipCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, TopicInListDto[]>({
      method: 'GET',
      url: '/api/app/topics/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: BaseListFilterDto) =>
    this.restService.request<any, PagedResultDto<TopicInListDto>>({
      method: 'GET',
      url: '/api/app/topics/filter',
      params: { keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getSuggestNewCode = () =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/topics/suggest-new-code',
    },
    { apiName: this.apiName });
  

  getThumbnailImage = (fileName: string) =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/topics/thumbnail-image',
      params: { fileName },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateTopicDto) =>
    this.restService.request<any, TopicDto>({
      method: 'PUT',
      url: `/api/app/topics/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
