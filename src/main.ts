import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import store from "./store";
import './assets/main.css';
import './assets/bundle.scss';
import './assets/scss/style.scss';
import axios from 'axios';
import VueAxios from 'vue-axios';
const app = createApp(App)
app.use(store);
app.use(router);
app.use(VueAxios, axios);
app.axios.defaults.baseURL = "https://localhost:7156/api";
app.mount('#app');
