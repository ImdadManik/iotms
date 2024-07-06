import { ABP, eLayoutType } from '@abp/ng.core';

export const ACCOUNT_BASE_ROUTES: ABP.Route[] = [
  {
    path: '/accounts',
    iconClass: 'fas fa-file-alt',
    name: '::Menu:Accounts',
    layout: eLayoutType.application,
    requiredPolicy: 'iotms.Accounts',
    breadcrumbText: '::Accounts',
  },
];
