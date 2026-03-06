export const getEnvVariable = (key: string, defaultValue: string = ''): string => {
    const value = import.meta.env[key];

    if (!value) {
        console.warn(`[Config Warning]: Environment variable ${key} is missing. Using default.`);
        return defaultValue;
    }

    return value.replace(/['"]+/g, '').trim();
};

export const config = {
    apiUrl: getEnvVariable('VITE_API_URL', 'http://localhost:5243'),
};
