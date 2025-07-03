import { apiService } from './apiService';

export const countriesService = {
	getCountries: () => apiService.get('/api/catalog/countries'),
	getCountry: (id: number) => apiService.get(`/api/catalog/country/${id}`),
};
