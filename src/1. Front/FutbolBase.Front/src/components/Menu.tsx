import React from 'react';
import { Drawer, List, ListItem, ListItemText, ListItemButton } from '@mui/material';
import { useNavigate } from 'react-router-dom';

interface MenuProps {
	isOpen: boolean;
	onClose: () => void;
}

const Menu: React.FC<MenuProps> = ({ isOpen, onClose }) => {
	const navigate = useNavigate();

	const handleNavigation = (path: string) => {
		navigate(path);
		onClose();
	};
	return (
		<Drawer anchor="left" open={isOpen} onClose={onClose}>
			<List>
				<ListItem disablePadding>
					<ListItemButton>
						<ListItemText primary="Inicio" onClick={() => handleNavigation('/')} />
					</ListItemButton>
				</ListItem>
				<ListItem disablePadding>
					<ListItemButton>
						<ListItemText primary="Clubs" onClick={() => handleNavigation('/clubs')} />
					</ListItemButton>
				</ListItem>
				<ListItem disablePadding>
					<ListItemButton>
						<ListItemText primary="Contacto" onClick={onClose} />
					</ListItemButton>
				</ListItem>
			</List>
		</Drawer>
	);
};

export default Menu;
