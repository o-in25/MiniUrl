/** @type {import('tailwindcss').Config} */
export default {
  darkMode: true, // Disables dark mode
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",  // Ensure Tailwind scans your components
    "./node_modules/flowbite-react/**/*.{js,jsx,ts,tsx}" // Add Flowbite React components
  ],
  theme: {
    extend: {},  // You can extend Tailwind styles here if needed
  },
  plugins: [require("flowbite/plugin")], // Enable Flowbite plugin
};