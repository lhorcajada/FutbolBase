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
	test: {
		globals: true,
		environment: 'jsdom',
		setupFiles: './vitest.setup.ts',
		include: ['src/**/*.test.ts', 'src/**/*.test.tsx'],
		reporters: ['default', 'junit'],
		outputFile: './test-results/TEST-results.xml',
		coverage: {
			provider: 'v8',
			reportsDirectory: './coverage/unit',
			reporter: ['text', 'lcov', 'html', 'cobertura'],
			exclude: [
				'src/test/**',
				'src/mocks/**',
				'src/types/**',
				'**/*.d.ts',
				'**/*.js',
				'src/index.tsx',
				'src/i18n/**',
				'**/*.config.ts',
				'src/stories/**',
				'.storybook/**',
				'config-tests/**',
				'dist/**',
				'src/e2e/**'
			],
		}
	},
	define: {
		'process.env': {
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
