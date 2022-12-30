import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Social_Network',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44398/',
    redirectUri: baseUrl,
    clientId: 'Social_Network_App',
    responseType: 'code',
    scope: 'offline_access Social_Network',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44332',
      rootNamespace: 'ConnectVN.Social_Network',
    },
  },
} as Environment;
