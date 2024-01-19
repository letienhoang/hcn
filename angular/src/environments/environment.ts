import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'HCN',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44382/',
    redirectUri: baseUrl,
    clientId: 'HCN_App',
    responseType: 'code',
    scope: 'offline_access HCN',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44361',
      rootNamespace: 'HCN',
    },
  },
} as Environment;
