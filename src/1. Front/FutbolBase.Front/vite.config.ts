import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import dotenv from 'dotenv';
import fs from 'fs';
import path from 'path';

dotenv.config();
export default defineConfig({
	plugins: [
		react()
	],
	define: {
		'process.env': {
			VITE_LOCALES_SRC: process.env.VITE_LOCALES_SRC,
			VITE_WEB_SIGN_JS_SRC: process.env.VITE_WEB_SIGN_JS_SRC,
			VITE_API_BASE_URL: process.env.VITE_API_BASE_URL
		},
	},
	server: {
		https: {
			key: fs.readFileSync(path.resolve(__dirname, 'ssl/localhost-key.pem')),
			cert: fs.readFileSync(path.resolve(__dirname, 'ssl/localhost-cert.pem')),
		},
	},
})
