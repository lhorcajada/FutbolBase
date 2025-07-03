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
import { clubsService } from '../../../services/clubsService';
import ConfirmDialog from '../../../components/dialog/confirmDialog';

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
	const [dialogOpen, setDialogOpen] = useState<boolean>(false);
	const [selectedClubId, setSelectedClubId] = useState<string | null>(null);
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
	const handleOpenDialog = (id: string) => {
		setSelectedClubId(id);
		setDialogOpen(true);
	};
	const handleCloseDialog = () => {
		setDialogOpen(false);
		setSelectedClubId(null);
	};
	const handleConfirmDelete = async () => {
		if (selectedClubId) {
			try {
				await clubsService.deleteClub(selectedClubId);
				setClubs((prevClubs) => prevClubs.filter((club) => club.id !== selectedClubId));
			} catch (err) {
				setError('No se pudo eliminar el club. Inténtalo nuevamente.');
			} finally {
				handleCloseDialog();
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
					onClick={() => navigate(`/clubs/create`)}
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
								<IconButton
									edge="end"
									aria-label="edit"
									onClick={() => navigate(`/clubs/edit/${club.id}`)}
								>
									<EditIcon />
								</IconButton>

								<IconButton
									edge="end"
									aria-label="delete"
									onClick={() => handleOpenDialog(club.id)}
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
			<ConfirmDialog
				open={dialogOpen}
				title="Confirmar eliminación"
				message="¿Estás seguro de que deseas eliminar este club? Esta acción no se puede deshacer."
				onConfirm={handleConfirmDelete}
				onCancel={handleCloseDialog}
				confirmText="Eliminar"
				cancelText="Cancelar"
			/>
		</Box>
	);
};

export default Clubs;
