import { Aviso } from '@tracasa/justicia-components';
import React, { useState, useEffect } from 'react';

interface GlobalErrorHandlerProps {
	onErrorHandled?: () => void; // Callback que se ejecutará después de manejar el error
	children: React.ReactNode;
}

export const GlobalErrorHandler: React.FC<GlobalErrorHandlerProps> = ({ onErrorHandled, children }) => {
	const [error, setError] = useState<string | null>(null);

	useEffect(() => {
		// Capturar errores sincrónicos
		window.onerror = (error) => {
			setError(error instanceof Error ? error.message : 'Error desconocido');
		};

		// Capturar errores en promesas no manejadas
		window.onunhandledrejection = (event) => {
			setError(event.reason instanceof Error ? event.reason.message : 'Error desconocido');
		};

		return () => {
			// Limpiar los manejadores globales al desmontar el componente
			window.onerror = null;
			window.onunhandledrejection = null;
		};
	}, []);

	const handleCloseError = () => {
		setError(null);
		if (onErrorHandled) {
			onErrorHandled();
		}
	};

	return (
		<>
			{children}
			{error && (
				<Aviso
					message={error}
					type='error'
					onClose={() => {
						handleCloseError();
					}}
				/>
			)}
		</>
	);
};

