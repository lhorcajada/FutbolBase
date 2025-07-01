import { apiService } from "./apiService";

export const clubsService = {
	createClub: (data: { name: string; countryCode: string }) =>
		apiService.post('/api/catalog/club/create', data), // Endpoint para crear

	getClubs: () => apiService.get('/api/catalog/clubs'), // Endpoint para obtener clubes

	deleteClub: (id: string) => apiService.delete(`/api/catalog/club/${id}`), // Endpoint para eliminar

	updateClub: (id: string, data: { name: string; countryId: number }) =>
		apiService.put(`/api/catalog/club/${id}`, data), // Endpoint para actualizar
};
