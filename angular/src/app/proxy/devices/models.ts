import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface DeviceCreateDto {
  name: string;
  status: boolean;
  temp: boolean;
  ldr: boolean;
  pir: boolean;
  door: boolean;
  minTempAlert: number;
  tempAlertFreq: number;
  minLDRAlert: number;
  ldrAlertFreq: number;
  connection: boolean;
}

export interface DeviceDto extends FullAuditedEntityDto<string> {
  name: string;
  status: boolean;
  temp: boolean;
  ldr: boolean;
  pir: boolean;
  door: boolean;
  minTempAlert: number;
  tempAlertFreq: number;
  minLDRAlert: number;
  ldrAlertFreq: number;
  connection: boolean;
}

export interface DeviceUpdateDto {
  name: string;
  status: boolean;
  temp: boolean;
  ldr: boolean;
  pir: boolean;
  door: boolean;
  minTempAlert: number;
  tempAlertFreq: number;
  minLDRAlert: number;
  ldrAlertFreq: number;
  connection: boolean;
}

export interface GetDeviceListInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  accountId: string;
}

export interface GetDevicesInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
  status?: boolean;
  temp?: boolean;
  ldr?: boolean;
  pir?: boolean;
  door?: boolean;
  minTempAlertMin?: number;
  minTempAlertMax?: number;
  tempAlertFreqMin?: number;
  tempAlertFreqMax?: number;
  minLDRAlertMin?: number;
  minLDRAlertMax?: number;
  ldrAlertFreqMin?: number;
  ldrAlertFreqMax?: number;
  connection?: boolean;
  accountId: string;
}
