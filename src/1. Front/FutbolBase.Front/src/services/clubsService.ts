import { apiService } from "./apiService";

export const clubsService = {
	createClub: (data: { name: string; countryCode: string }) =>
		apiService.post('/api/catalog/club', data),
	deleteClub: (id: string) => apiService.delete(`/api/catalog/club/${id}`),
	updateClub: (id: string, data: { clubName: string; countryCode: string }) =>
		apiService.put(`/api/catalog/club/${id}`, data),
	getClubs: () => apiService.get('/api/catalog/clubs'),
	getClub: (id: string) => apiService.get(`/api/catalog/club/${id}`),
};
