import type { CreateUpdateStoryDto, StoryDto, StoryInListDto, StoryListFilterDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { TagInListDto } from '../tags/models';

@Injectable({
  providedIn: 'root',
})
export class StoriesService {
  apiName = 'Default';
  

  create = (input: CreateUpdateStoryDto) =>
    this.restService.request<any, StoryDto>({
      method: 'POST',
      url: '/api/app/stories',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/stories/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/stories/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, StoryDto>({
      method: 'GET',
      url: `/api/app/stories/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<StoryDto>>({
      method: 'GET',
      url: '/api/app/stories',
      params: { maxResultCount: input.maxResultCount, skipCount: input.skipCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, StoryInListDto[]>({
      method: 'GET',
      url: '/api/app/stories/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: StoryListFilterDto) =>
    this.restService.request<any, PagedResultDto<StoryInListDto>>({
      method: 'GET',
      url: '/api/app/stories/filter',
      params: { topicId: input.topicId, keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getStoryTag = (storyId: string) =>
    this.restService.request<any, TagInListDto[]>({
      method: 'GET',
      url: `/api/app/stories/story-tag/${storyId}`,
    },
    { apiName: this.apiName });
  

  getSuggestNewCode = () =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/stories/suggest-new-code',
    },
    { apiName: this.apiName });
  

  getThumbnailImage = (fileName: string) =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/stories/thumbnail-image',
      params: { fileName },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateStoryDto) =>
    this.restService.request<any, StoryDto>({
      method: 'PUT',
      url: `/api/app/stories/${id}`,
      body: input,
    },
    { apiName: this.apiName });
  

  updateStoryTag = (storyId: string, storyTagList: string[]) =>
    this.restService.request<any, StoryDto>({
      method: 'PUT',
      url: `/api/app/stories/story-tag/${storyId}`,
      body: storyTagList,
    },
    { apiName: this.apiName });
  

  updateVisibility = (storyId: string, visibility: boolean) =>
    this.restService.request<any, StoryDto>({
      method: 'PUT',
      url: `/api/app/stories/visibility/${storyId}`,
      params: { visibility },
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
