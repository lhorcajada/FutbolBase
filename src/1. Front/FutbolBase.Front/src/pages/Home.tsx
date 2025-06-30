import React from 'react';
import Menu from '../components/Menu';
import styles from './Home.module.css';

const Home: React.FC = () => {
	return (
		<>
			<Menu />
			<main className={styles.main}>
				<h1>Bienvenido a Futbol Base</h1>
			</main>

		</>
	);
};

export default Home;
