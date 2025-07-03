import React from 'react';
import { Box, Typography } from '@mui/material';
import styles from './Footer.module.css';

const Footer: React.FC = () => {
	return (
		<Box className={styles.footer}>
			<Typography variant="body2">© 2025 Futbol Base</Typography>
		</Box>
	);
};

export default Footer;
