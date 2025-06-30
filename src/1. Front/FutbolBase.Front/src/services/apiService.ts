import axios from 'axios';

// Configuración básica de Axios
const apiClient = axios.create({
	baseURL: 'https://api.example.com', // Cambiar por la URL base de tu API
	headers: {
		'Content-Type': 'application/json',
	},
});

// Funciones genéricas para métodos HTTP
export const apiService = {
	get: (url: string) => apiClient.get(url),
	post: (url: string, data: any) => apiClient.post(url, data),
	put: (url: string, data: any) => apiClient.put(url, data),
	delete: (url: string) => apiClient.delete(url),
};
