import { render, screen, fireEvent } from '@testing-library/react';
import { describe, it, expect, vi } from 'vitest';
import { MemoryRouter } from 'react-router-dom';
import Menu from './Menu';
import '@testing-library/jest-dom';

describe('Menu Component', () => {
	const mockOnClose = vi.fn();

	it('renderiza correctamente las opciones del menú', () => {
		render(
			<MemoryRouter>
				<Menu isOpen={true} onClose={mockOnClose} />
			</MemoryRouter>
		);

		expect(screen.getByText('Inicio')).toBeInTheDocument();
		expect(screen.getByText('Clubs')).toBeInTheDocument();
		expect(screen.getByText('Contacto')).toBeInTheDocument();
	});

	it('llama a onClose al hacer clic en "Contacto"', () => {
		render(
			<MemoryRouter>
				<Menu isOpen={true} onClose={mockOnClose} />
			</MemoryRouter>
		);

		fireEvent.click(screen.getByText('Contacto'));
		expect(mockOnClose).toHaveBeenCalled();
	});


});
