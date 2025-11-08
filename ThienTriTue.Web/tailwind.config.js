// ThienTriTue.Web/tailwind.config.js
/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./Areas/**/*.{cshtml,js,html}",
    "./Views/**/*.{cshtml,js,html}",
    "./Pages/**/*.{cshtml,js,html}",
    "../ThienTriTue.Theme/Views/**/*.{cshtml,js,html}",
    "./wwwroot/js/**/*.js",
    "./wwwroot/html/**/*.html" 
  ],
  theme: {
    extend: {
      fontFamily: {
        'inter': ['Inter', 'sans-serif'],
      },
    },
  },
  plugins: [],
}