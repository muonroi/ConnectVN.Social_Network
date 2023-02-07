const ENV = {
    dev: {
        apiUrl: 'http://localhost:44332',
        oAuthConfig: {
            issuer: 'http://localhost:44398',
            clientId: 'Social_Network_App',
            clientSecret: '1q2w3e*',
            scope: 'offline_access Social_Network',
        },
        localization: {
            defaultResourceName: 'Social_Network',
        },
    },
    prod: {
        apiUrl: 'http://localhost:44332',
        oAuthConfig: {
            issuer: 'http://localhost:44398',
            clientId: 'Social_Network_App',
            clientSecret: '1q2w3e*',
            scope: 'offline_access Social_Network',
        },
        localization: {
            defaultResourceName: 'Social_Network',
        },
    },
};

export const getEnvVars = () => {
    // eslint-disable-next-line no-undef
    return __DEV__ ? ENV.dev : ENV.prod;
};