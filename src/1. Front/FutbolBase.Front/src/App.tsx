import React, { useState } from 'react';
import { SnackbarProvider } from 'notistack';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Menu from './components/Menu';
import Home from './pages/Home';
import Clubs from './pages/Clubs';
import Header from './components/Header';
import Footer from './components/Footer';
import styles from './App.module.css';
import CreateClub from './pages/CreateClub';

const App: React.FC = () => {
	const [isMenuOpen, setMenuOpen] = useState(false);

	const handleMenuToggle = () => {
		setMenuOpen(!isMenuOpen);
	};

	return (
		<SnackbarProvider maxSnack={3} autoHideDuration={3000} anchorOrigin={{ vertical: 'top', horizontal: 'right' }}>
			<Router>
				<div className={styles.container}>
					<Menu isOpen={isMenuOpen} onClose={() => setMenuOpen(false)} />
					<div className={styles.content}>
						<Header onMenuToggle={handleMenuToggle} />
						<main className={styles.main}>
							<Routes>
								<Route path="/" element={<Home />} />
								<Route path="/clubs" element={<Clubs />} />
								<Route path="/clubs/create" element={<CreateClub />} />
							</Routes>
						</main>
						<Footer />
					</div>
				</div>
			</Router>
		</SnackbarProvider>
	);
};

export default App;