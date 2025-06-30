import React, { useState } from 'react';
import {
	Drawer,
	ListItem,
	ListItemButton,
	ListItemText,
	Toolbar,
	IconButton,
	Divider,
} from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import { Link } from 'react-router-dom';
import styles from './Menu.module.css';


const Menu: React.FC = () => {
	const [mobileOpen, setMobileOpen] = useState(false);

	const handleDrawerToggle = () => {
		setMobileOpen(!mobileOpen);
	};

	const drawer = (
		<div>
			<Toolbar />
			<Divider />
			<ListItem>
				<ListItemButton component={Link} to="/">
					<ListItemText primary="Inicio" />
				</ListItemButton>
			</ListItem>
			<ListItem>
				<ListItemButton component={Link} to="/clubs">
					<ListItemText primary="Clubes" />
				</ListItemButton>
			</ListItem>
			<ListItem>
				<ListItemButton component={Link} to="/about">
					<ListItemText primary="Acerca de" />
				</ListItemButton>
			</ListItem>

		</div>
	);

	return (
		<>
			{/* Ícono para abrir el menú en dispositivos pequeños */}
			<IconButton
				color="inherit"
				aria-label="open drawer"
				edge="start"
				onClick={handleDrawerToggle}
				sx={{ display: { sm: 'none' } }} /* Solo visible en pantallas pequeñas */
			>
				<MenuIcon />
			</IconButton>

			{/* Drawer para pantallas pequeñas */}
			<Drawer
				variant="temporary"
				open={mobileOpen}
				onClose={handleDrawerToggle}
				ModalProps={{
					keepMounted: true, // Mejora el rendimiento en dispositivos móviles
				}}
				classes={{
					paper: styles.drawerPaper,
				}}
			>
				{drawer}
			</Drawer>

			{/* Drawer permanente para pantallas grandes */}
			<Drawer
				variant="permanent"
				open
				classes={{
					paper: styles.drawerPaper,
				}}
				sx={{
					display: { xs: 'none', sm: 'block' }, // Oculto en pantallas pequeñas
				}}
			>
				{drawer}
			</Drawer>
		</>
	);
};

export default Menu;
