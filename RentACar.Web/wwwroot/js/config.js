const API_URL = process.env.API_URL;

if (API_URL === undefined) {
    API_URL = "https://localhost:7027";
}

export const API_URL
