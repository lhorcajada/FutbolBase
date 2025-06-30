import React, { useState } from 'react';
import CreateClub from './CreateClub';
import styles from './Clubs.module.css';

const Clubs: React.FC = () => {
	const [createClubOpen, setCreateClubOpen] = useState(false);

	const handleOpenCreateClub = () => setCreateClubOpen(true);
	const handleCloseCreateClub = () => setCreateClubOpen(false);

	return (
		<>
			<main className={styles.main}>
				<h1>Clubes</h1>
				<button className={styles.button} onClick={handleOpenCreateClub}>Crear Club</button>
			</main>
			<CreateClub open={createClubOpen} handleClose={handleCloseCreateClub} />
		</>
	);
};

export default Clubs;
