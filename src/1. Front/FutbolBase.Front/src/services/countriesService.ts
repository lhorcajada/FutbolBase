import { apiService } from './apiService';

export const countriesService = {
	getCountries: () => apiService.get('/api/catalog/countries'), // Endpoint para obtener países
};
