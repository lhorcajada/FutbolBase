import React, { useEffect, useState } from 'react';
import { TextField, Button, Grid, Typography, Box } from '@mui/material';
import { useNavigate, useParams } from 'react-router-dom';
import { useSnackbar } from 'notistack';
import CountrySelector from '../../../components/countrySelector/CountrySelector';
import { clubsService } from '../../../services/clubsService';
import { countriesService } from '../../../services/countriesService';
import type { Country } from '../../../types/countryType';

import styles from './ClubForm.module.css';

const ClubForm: React.FC<{ isEdit?: boolean }> = ({ isEdit = false }) => {
	const { id } = useParams<{ id: string }>(); // Obtiene el id para edición
	const [name, setName] = useState('');
	const [countryCode, setCountryCode] = useState('');
	const [countries, setCountries] = useState<Country[]>([]);
	const { enqueueSnackbar } = useSnackbar();
	const navigate = useNavigate();

	useEffect(() => {
		const fetchCountries = async () => {
			try {
				const response = await countriesService.getCountries();
				setCountries(response.data);
			} catch (err) {
				console.error(err);
			}
		};

		const fetchClub = async () => {
			if (isEdit && id) {
				try {
					const response = await clubsService.getClub(id);
					setName(response.data.name);
					setCountryCode(response.data.country.code);
				} catch (err) {
					enqueueSnackbar('Error al cargar los datos del club', { variant: 'error' });
				}
			}
		};

		fetchCountries();
		if (isEdit) fetchClub();
	}, [isEdit, id, enqueueSnackbar]);

	const handleSubmit = async (event: React.FormEvent) => {
		event.preventDefault();

		if (!name || !countryCode) {
			enqueueSnackbar('Todos los campos son obligatorios', { variant: 'warning' });
			return;
		}

		try {
			if (isEdit && id) {
				await clubsService.updateClub(id, { clubName: name, countryCode });
				enqueueSnackbar('Club actualizado correctamente', { variant: 'success' });
			} else {
				await clubsService.createClub({ name, countryCode });
				enqueueSnackbar('Club creado correctamente', { variant: 'success' });
			}

			navigate('/clubs');
		} catch (error: any) {
			enqueueSnackbar(error.message || 'Error al guardar el club', { variant: 'error' });
		}
	};

	return (
		<Box className={styles.box}>
			<Typography variant="h4" gutterBottom>
				{isEdit ? 'Editar Club' : 'Crear Nuevo Club'}
			</Typography>
			<Grid container spacing={2} component="form" onSubmit={handleSubmit}>
				<Grid size={12}>
					<TextField
						label="Nombre del Club"
						value={name}
						onChange={(e) => setName(e.target.value)}
						inputProps={{ maxLength: 200 }}
						required
						fullWidth
						variant="outlined"
					/>
				</Grid>

				<Grid size={12}>
					<CountrySelector
						value={countryCode}
						onChange={(code) => setCountryCode(code)}
						options={countries}
					/>
				</Grid>

				<Grid size={12}>
					<Button type="submit" variant="contained" color="primary" fullWidth>
						{isEdit ? 'Actualizar Club' : 'Crear Club'}
					</Button>
				</Grid>

				<Grid size={12}>
					<Button
						variant="outlined"
						color="secondary"
						fullWidth
						onClick={() => navigate('/clubs')}
					>
						Volver a la Lista de Clubs
					</Button>
				</Grid>
			</Grid>
		</Box>
	);
};

export default ClubForm;
