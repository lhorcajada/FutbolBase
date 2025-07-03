import React from 'react';
import { Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Button } from '@mui/material';

interface ConfirmDialogProps {
	open: boolean;
	title?: string;
	message?: string;
	onConfirm: () => void;
	onCancel: () => void;
	confirmText?: string;
	cancelText?: string;
}

const ConfirmDialog: React.FC<ConfirmDialogProps> = ({
	open,
	title = 'Confirmación',
	message = '¿Estás seguro de realizar esta acción?',
	onConfirm,
	onCancel,
	confirmText = 'Aceptar',
	cancelText = 'Cancelar',
}) => {
	return (
		<Dialog open={open} onClose={onCancel}>
			<DialogTitle>{title}</DialogTitle>
			<DialogContent>
				<DialogContentText>{message}</DialogContentText>
			</DialogContent>
			<DialogActions>
				<Button onClick={onCancel} color="secondary">
					{cancelText}
				</Button>
				<Button onClick={onConfirm} color="primary" variant="contained">
					{confirmText}
				</Button>
			</DialogActions>
		</Dialog>
	);
};

export default ConfirmDialog;
