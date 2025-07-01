import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import {
	CircularProgress,
	List,
	ListItem,
	ListItemText,
	IconButton,
	Typography,
	Box,
	Button,
} from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
import { clubsService } from '../services/clubsService';

interface Club {
	id: string;
	name: string;
	country: {
		id: number;
		name: string;
	};
}

const Clubs: React.FC = () => {
	const [clubs, setClubs] = useState<Club[]>([]);
	const [loading, setLoading] = useState<boolean>(true);
	const [error, setError] = useState<string | null>(null);
	const navigate = useNavigate();

	useEffect(() => {
		const fetchClubs = async () => {
			try {
				setLoading(true);
				const response = await clubsService.getClubs();
				setClubs(response.data);
			} catch (err) {
				setError('No se pudieron cargar los clubes. Inténtalo nuevamente.');
				console.error(err);
			} finally {
				setLoading(false);
			}
		};

		fetchClubs();
	}, []);

	const handleDelete = async (id: string) => {
		if (window.confirm('¿Estás seguro de que deseas eliminar este club?')) {
			try {
				await clubsService.deleteClub(id);
				setClubs((prevClubs) => prevClubs.filter((club) => club.id !== id));
			} catch (err) {
				console.error('Error al eliminar el club:', err);
				setError('No se pudo eliminar el club. Inténtalo nuevamente.');
			}
		}
	};

	if (loading) {
		return (
			<Box display="flex" justifyContent="center" alignItems="center" height="100vh">
				<CircularProgress />
			</Box>
		);
	}

	if (error) {
		return (
			<Box display="flex" justifyContent="center" alignItems="center" height="100vh">
				<Typography variant="h6" color="error">
					{error}
				</Typography>
			</Box>
		);
	}

	return (
		<Box sx={{ padding: 2 }}>
			<Typography variant="h4" gutterBottom>
				Lista de Clubs
			</Typography>


			<Box
				sx={{
					display: 'flex', // Estilo de flexbox
					justifyContent: 'space-between', // Espacio entre los botones
					gap: 2, // Espacio entre los botones (puedes ajustarlo)
					marginBottom: 2, // Margen inferior (opcional)
				}}
			>
				<Button
					variant="contained"
					color="primary"
					startIcon={<AddIcon />}
					onClick={() => navigate('/clubs/create')}
				>
					Crear Club
				</Button>

				<Button
					variant="outlined"
					color="secondary"
					onClick={() => navigate('/')}
				>
					Página principal
				</Button>
			</Box>
			<List>
				{clubs.map((club) => (
					<ListItem
						key={club.id}
						divider
						secondaryAction={
							<>
								{/* Botón de Editar */}
								<IconButton
									edge="end"
									aria-label="edit"
									onClick={() => console.log(`Editar club: ${club.id}`)}
								>
									<EditIcon />
								</IconButton>

								{/* Botón de Eliminar */}
								<IconButton
									edge="end"
									aria-label="delete"
									onClick={() => handleDelete(club.id)}
								>
									<DeleteIcon />
								</IconButton>
							</>
						}
					>
						<ListItemText
							primary={club.name}
							secondary={`País: ${club.country.name}`}
						/>
					</ListItem>
				))}
			</List>
		</Box>
	);
};

export default Clubs;
