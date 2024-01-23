import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'HCN Admin',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:60000/',
    redirectUri: baseUrl,
    clientId: 'HCN_Admin',
    responseType: 'code',
    scope: 'offline_access HCN.Admin',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:50001',
      rootNamespace: 'HCN',
    },
  },
} as Environment;
