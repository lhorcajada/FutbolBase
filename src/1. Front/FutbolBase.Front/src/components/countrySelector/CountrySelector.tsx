import React from 'react';
import { Autocomplete, TextField } from '@mui/material';
import type { Country } from '../../types/countryType';

interface CountrySelectorProps {
	value: string;
	onChange: (value: string) => void;
	options: Country[];
}

const CountrySelector: React.FC<CountrySelectorProps> = ({ value, onChange, options }) => {
	const selectedOption = options.find((option) => option.code === value);
	return (
		<Autocomplete
			freeSolo
			options={options}
			getOptionLabel={(option) => typeof option === 'string' ? option : option.name}
			value={selectedOption || null}
			onChange={(_event, newValue) =>
				onChange(
					typeof newValue === 'string'
						? newValue
						: newValue
							? newValue.code
							: ''
				)
			}
			renderInput={(params) => (
				<TextField
					{...params}
					label="Selecciona o escribe un país"
					variant="outlined"
					fullWidth
				/>
			)}
		/>
	);
};

export default CountrySelector;
