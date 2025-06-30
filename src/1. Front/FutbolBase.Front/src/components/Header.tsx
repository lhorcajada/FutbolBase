import React from 'react';
import { AppBar, Toolbar, Typography } from '@mui/material';
import styles from './Header.module.css';

const Header: React.FC = () => {
	return (
		<AppBar position="static" className={styles.header}>
			<Toolbar>
				<Typography variant="h6" component="div" className={styles.title}>
					Futbol Base
				</Typography>
			</Toolbar>
		</AppBar>
	);
};

export default Header;
