import axios from 'axios';

const API_BASE = '/api';

const apiClient = axios.create({
    baseURL: API_BASE,
    timeout: 10000,
    headers: {'Content-Type': 'application/json'}
});

//перехватчик для добавления токена
apiClient.interceptors.request.use(config => {
    const token = localStorage.getItem('token');
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
})

export const OrganizationApi = {
    getAll: () => apiClient.get('/organizations'),
    getById: (id) => apiClient.get(`/organizations/${id}`),
    create: (data) => apiClient.post('/organizations', data),
    update: (id, data) => apiClient.put(`/organizations/${id}`, data),
    delete: (id) => apiClient.delete(`/organizations/${id}`),
}