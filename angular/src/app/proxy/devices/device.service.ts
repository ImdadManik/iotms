import type {
  DeviceCreateDto,
  DeviceDto,
  DeviceUpdateDto,
  GetDeviceListInput,
  GetDevicesInput,
} from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { AppFileDescriptorDto, DownloadTokenResultDto, GetFileInput } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class DeviceService {
  apiName = 'Default';

  create = (input: DeviceCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DeviceDto>(
      {
        method: 'POST',
        url: '/api/app/devices',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/app/devices/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DeviceDto>(
      {
        method: 'GET',
        url: `/api/app/devices/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>(
      {
        method: 'GET',
        url: '/api/app/devices/download-token',
      },
      { apiName: this.apiName, ...config }
    );

  getFile = (input: GetFileInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/app/devices/file',
        params: { downloadToken: input.downloadToken, fileId: input.fileId },
      },
      { apiName: this.apiName, ...config }
    );

  getList = (input: GetDevicesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<DeviceDto>>(
      {
        method: 'GET',
        url: '/api/app/devices',
        params: {
          accountId: input.accountId,
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          name: input.name,
          status: input.status,
          temp: input.temp,
          ldr: input.ldr,
          pir: input.pir,
          door: input.door,
          minTempAlertMin: input.minTempAlertMin,
          minTempAlertMax: input.minTempAlertMax,
          tempAlertFreqMin: input.tempAlertFreqMin,
          tempAlertFreqMax: input.tempAlertFreqMax,
          minLDRAlertMin: input.minLDRAlertMin,
          minLDRAlertMax: input.minLDRAlertMax,
          ldrAlertFreqMin: input.ldrAlertFreqMin,
          ldrAlertFreqMax: input.ldrAlertFreqMax,
          connection: input.connection,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getListByAccountId = (input: GetDeviceListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<DeviceDto>>(
      {
        method: 'GET',
        url: '/api/app/devices/by-account',
        params: {
          accountId: input.accountId,
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
        },
      },
      { apiName: this.apiName, ...config }
    );

  update = (id: string, input: DeviceUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DeviceDto>(
      {
        method: 'PUT',
        url: `/api/app/devices/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  uploadFile = (input: FormData, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AppFileDescriptorDto>(
      {
        method: 'POST',
        url: '/api/app/devices/upload-file',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  constructor(private restService: RestService) {}
}
