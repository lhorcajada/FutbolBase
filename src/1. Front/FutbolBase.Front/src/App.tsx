import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { SnackbarProvider } from 'notistack';
import Home from './pages/Home';
import Clubs from './pages/Clubs';
import Footer from './components/Footer';
import Menu from './components/Menu';
import styles from './App.module.css';

const App: React.FC = () => {
	return (
		<SnackbarProvider maxSnack={3} autoHideDuration={3000} anchorOrigin={{ vertical: 'top', horizontal: 'right' }}>
			<Router>
				<div className={styles.container}>
					<Menu />
					<div className={styles.content}>
						<main className={styles.main}>
							<Routes>
								<Route path="/" element={<Home />} />
								<Route path="/clubs" element={<Clubs />} />
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
