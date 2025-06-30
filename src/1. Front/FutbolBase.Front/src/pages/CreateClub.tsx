import React, { useState, useEffect } from 'react';
import {
	Box,
	Button,
	TextField,
	Dialog,
	DialogActions,
	DialogContent,
	DialogTitle,
	MenuItem,
} from '@mui/material';
import { useSnackbar } from 'notistack';
import { countriesService } from '../services/countriesService';
import { clubsService } from '../services/clubsService';
import styles from './CreateClub.module.css';

interface CreateClubProps {
	open: boolean;
	handleClose: () => void;
}

const CreateClub: React.FC<CreateClubProps> = ({ open, handleClose }) => {
	const [name, setName] = useState('');
	const [countryId, setCountryId] = useState<number | null>(null);
	const [countries, setCountries] = useState([]);
	const { enqueueSnackbar } = useSnackbar();

	useEffect(() => {
		if (open) {
			countriesService.getCountries().then((response) => setCountries(response.data));
		}
	}, [open]);

	const handleCreateClub = () => {
		if (!name || !countryId) {
			enqueueSnackbar('Por favor, complete todos los campos.', { variant: 'warning' });
			return;
		}

		clubsService.createClub({ name, countryId })
			.then(() => {
				enqueueSnackbar('Club creado exitosamente.', { variant: 'success' });
				handleClose();
				setName('');
				setCountryId(null);
			})
			.catch(() => {
				enqueueSnackbar('Ocurrió un error al crear el club.', { variant: 'error' });
			});
	};

	return (
		<Dialog open={open} onClose={handleClose} fullWidth>
			<DialogTitle>Crear Club</DialogTitle>
			<DialogContent>
				<Box className={styles.form}>
					<TextField
						label="Nombre del Club"
						variant="outlined"
						fullWidth
						value={name}
						onChange={(e) => setName(e.target.value)}
					/>
					<TextField
						select
						label="País"
						variant="outlined"
						fullWidth
						value={countryId || ''}
						onChange={(e) => setCountryId(Number(e.target.value))}
					>
						{countries.map((country: any) => (
							<MenuItem key={country.Id} value={country.Id}>
								{country.Name}
							</MenuItem>
						))}
					</TextField>
				</Box>
			</DialogContent>
			<DialogActions>
				<Button onClick={handleClose}>Cancelar</Button>
				<Button onClick={handleCreateClub}>Crear</Button>
			</DialogActions>
		</Dialog>
	);
};

export default CreateClub;
