import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface AccountCreateDto {
  name: string;
  location: string;
  address: string;
  contact: string;
  email: string;
  web: string;
  rooms: number;
  status: boolean;
}

export interface AccountDto extends FullAuditedEntityDto<string> {
  name: string;
  location: string;
  address: string;
  contact: string;
  email: string;
  web: string;
  rooms: number;
  status: boolean;
  concurrencyStamp?: string;
}

export interface AccountExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
  location?: string;
  address?: string;
  contact?: string;
  email?: string;
  web?: string;
  roomsMin?: number;
  roomsMax?: number;
  status?: boolean;
}

export interface AccountUpdateDto {
  name: string;
  location: string;
  address: string;
  contact: string;
  email: string;
  web: string;
  rooms: number;
  status: boolean;
  concurrencyStamp?: string;
}

export interface GetAccountsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
  location?: string;
  address?: string;
  contact?: string;
  email?: string;
  web?: string;
  roomsMin?: number;
  roomsMax?: number;
  status?: boolean;
}
