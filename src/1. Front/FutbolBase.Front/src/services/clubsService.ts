import { apiService } from './apiService';

export const clubsService = {
	createClub: (data: { name: string; countryId: number }) =>
		apiService.post('/clubs', data), // Endpoint para crear un club

	getClubs: () => apiService.get('/clubs'), // Endpoint para obtener clubes (futuro)
};
