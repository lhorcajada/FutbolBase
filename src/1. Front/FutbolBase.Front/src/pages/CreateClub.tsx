import React, { useEffect, useState } from 'react';
import { TextField, Button, Grid, Typography, Box } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { useSnackbar } from 'notistack';
import CountrySelector from '../components/CountrySelector';
import { clubsService } from '../services/clubsService';

import styles from './CreateClub.module.css';
import { countriesService } from '../services/countriesService';
import type { Country } from '../types/countryType';


const CreateClubPage: React.FC = () => {
	const [name, setName] = useState('');
	const [countryCode, setCountryCode] = useState('');
	const { enqueueSnackbar } = useSnackbar();
	const navigate = useNavigate();
	const [countries, setCountries] = useState<Country[]>([]);

	const handleSubmit = async (event: React.FormEvent) => {
		event.preventDefault();

		if (!name || !countryCode) {
			enqueueSnackbar('Todos los campos son obligatorios', { variant: 'warning' });
			return;
		}

		try {
			await clubsService.createClub({ name, countryCode });
			enqueueSnackbar('Club creado correctamente', { variant: 'success' });
			navigate('/clubs'); // Redirige a la lista de clubes
		} catch (error: any) {
			enqueueSnackbar(error.message || 'Error al crear el club', { variant: 'error' });
		}
	};

	useEffect(() => {
		const fetchCountries = async () => {
			try {
				const response = await countriesService.getCountries();
				setCountries(response.data);
			} catch (err) {
				console.error(err);
			}
		};

		fetchCountries();
	}, []);

	return (

		<Box className={styles.box}>

			<Typography variant="h4" gutterBottom>
				Crear Nuevo Club
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
						Crear Club
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

export default CreateClubPage;
