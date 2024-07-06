import type {
  AccountCreateDto,
  AccountDto,
  AccountExcelDownloadDto,
  AccountUpdateDto,
  GetAccountsInput,
} from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { AppFileDescriptorDto, DownloadTokenResultDto, GetFileInput } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  apiName = 'Default';

  create = (input: AccountCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AccountDto>(
      {
        method: 'POST',
        url: '/api/app/accounts',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/app/accounts/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AccountDto>(
      {
        method: 'GET',
        url: `/api/app/accounts/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>(
      {
        method: 'GET',
        url: '/api/app/accounts/download-token',
      },
      { apiName: this.apiName, ...config }
    );

  getFile = (input: GetFileInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/app/accounts/file',
        params: { downloadToken: input.downloadToken, fileId: input.fileId },
      },
      { apiName: this.apiName, ...config }
    );

  getList = (input: GetAccountsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<AccountDto>>(
      {
        method: 'GET',
        url: '/api/app/accounts',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          name: input.name,
          location: input.location,
          address: input.address,
          contact: input.contact,
          email: input.email,
          web: input.web,
          roomsMin: input.roomsMin,
          roomsMax: input.roomsMax,
          status: input.status,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getListAsExcelFile = (input: AccountExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/app/accounts/as-excel-file',
        params: {
          downloadToken: input.downloadToken,
          filterText: input.filterText,
          name: input.name,
          location: input.location,
          address: input.address,
          contact: input.contact,
          email: input.email,
          web: input.web,
          roomsMin: input.roomsMin,
          roomsMax: input.roomsMax,
          status: input.status,
        },
      },
      { apiName: this.apiName, ...config }
    );

  update = (id: string, input: AccountUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AccountDto>(
      {
        method: 'PUT',
        url: `/api/app/accounts/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  uploadFile = (input: FormData, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AppFileDescriptorDto>(
      {
        method: 'POST',
        url: '/api/app/accounts/upload-file',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  constructor(private restService: RestService) {}
}
