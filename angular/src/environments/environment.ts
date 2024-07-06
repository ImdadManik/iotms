import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44354/',
  redirectUri: baseUrl,
  clientId: 'iotms_App',
  responseType: 'code',
  scope: 'offline_access iotms',
  requireHttps: true,
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'iotms',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44349',
      rootNamespace: 'iotms',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
