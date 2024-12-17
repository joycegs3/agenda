import './assets/main.css';

import { createApp } from 'vue';
import App from './App.vue';

import router from './router';

import Aura from '@primevue/themes/aura';  // Theme
import 'primeicons/primeicons.css';  // Icons

import PrimeVue from 'primevue/config';

const app = createApp(App);


app.use(PrimeVue, {
    theme: {
        preset: Aura,
        // unstyled: 'true'
    }
});

app.use(router);

app.mount('#app');
