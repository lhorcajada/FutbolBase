import { apiService } from './apiService';

export const countriesService = {
	getCountries: () => apiService.get('/countries'), // Endpoint para obtener países
};
