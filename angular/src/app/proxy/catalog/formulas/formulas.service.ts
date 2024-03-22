import type { CreateUpdateFormulaDto, FormulaDto, FormulaInListDto, FormulaListFilterDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { TagInListDto } from '../tags/models';

@Injectable({
  providedIn: 'root',
})
export class FormulasService {
  apiName = 'Default';
  

  create = (input: CreateUpdateFormulaDto) =>
    this.restService.request<any, FormulaDto>({
      method: 'POST',
      url: '/api/app/formulas',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/formulas/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/formulas/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, FormulaDto>({
      method: 'GET',
      url: `/api/app/formulas/${id}`,
    },
    { apiName: this.apiName });
  

  getFormulaTag = (formulaId: string) =>
    this.restService.request<any, TagInListDto[]>({
      method: 'GET',
      url: `/api/app/formulas/formula-tag/${formulaId}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<FormulaDto>>({
      method: 'GET',
      url: '/api/app/formulas',
      params: { maxResultCount: input.maxResultCount, skipCount: input.skipCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, FormulaInListDto[]>({
      method: 'GET',
      url: '/api/app/formulas/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: FormulaListFilterDto) =>
    this.restService.request<any, PagedResultDto<FormulaInListDto>>({
      method: 'GET',
      url: '/api/app/formulas/filter',
      params: { categoryId: input.categoryId, keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getSuggestNewCode = () =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/formulas/suggest-new-code',
    },
    { apiName: this.apiName });
  

  getThumbnailImage = (fileName: string) =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/formulas/thumbnail-image',
      params: { fileName },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateFormulaDto) =>
    this.restService.request<any, FormulaDto>({
      method: 'PUT',
      url: `/api/app/formulas/${id}`,
      body: input,
    },
    { apiName: this.apiName });
  

  updateFormulaTag = (formulaId: string, formulaTagList: string[]) =>
    this.restService.request<any, FormulaDto>({
      method: 'PUT',
      url: `/api/app/formulas/formula-tag/${formulaId}`,
      body: formulaTagList,
    },
    { apiName: this.apiName });
  

  updateVisibility = (formulaId: string, visibility: boolean) =>
    this.restService.request<any, FormulaDto>({
      method: 'PUT',
      url: `/api/app/formulas/visibility/${formulaId}`,
      params: { visibility },
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
