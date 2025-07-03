import { useSnackbar } from 'notistack';
import React, { useEffect } from 'react';

interface GlobalErrorHandlerProps {
	onErrorHandled?: () => void; // Callback que se ejecutará después de manejar el error
	children: React.ReactNode;
}

export const GlobalErrorHandler: React.FC<GlobalErrorHandlerProps> = ({ children }) => {
	const { enqueueSnackbar } = useSnackbar();
	useEffect(() => {
		// Capturar errores sincrónicos
		window.onerror = (error) => {
			enqueueSnackbar(error instanceof Error ? error.message : 'Error desconocido', { variant: 'error' });
		};

		// Capturar errores en promesas no manejadas
		window.onunhandledrejection = (event) => {
			enqueueSnackbar(event.reason instanceof Error ? event.reason.message : 'Error desconocido', { variant: 'error' });
		};

		return () => {
			// Limpiar los manejadores globales al desmontar el componente
			window.onerror = null;
			window.onunhandledrejection = null;
		};
	}, [enqueueSnackbar]);

	return (
		<>
			{children}

		</>
	);
};

