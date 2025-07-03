import React, { useState } from 'react';
import { SnackbarProvider } from 'notistack';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Menu from './components/menu/Menu';
import Home from './pages/home/Home';
import Clubs from './pages/clubs/list/Clubs';
import Header from './components/header/Header';
import Footer from './components/footer/Footer';
import styles from './App.module.css';
import ClubForm from './pages/clubs/form/ClubForm';

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
								<Route path="/clubs/create" element={<ClubForm />} />
								<Route path="/clubs/edit/:id" element={<ClubForm isEdit={true} />} />
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