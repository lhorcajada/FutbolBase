import React from 'react';
import { AppBar, Toolbar, Typography, IconButton } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import styles from './Header.module.css';

interface HeaderProps {
	onMenuToggle: () => void;
}

const Header: React.FC<HeaderProps> = ({ onMenuToggle }) => {
	return (
		<AppBar position="fixed" className={styles.header}>
			<Toolbar>
				<IconButton
					edge="start"
					color="inherit"
					aria-label="menu"
					onClick={onMenuToggle}
					className={styles.menuButton}
				>
					<MenuIcon />
				</IconButton>
				<Typography variant="h6" component="div" className={styles.title}>
					Futbol Base
				</Typography>
			</Toolbar>
		</AppBar>
	);
};

export default Header;
