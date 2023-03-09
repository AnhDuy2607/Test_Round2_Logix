import store from '@/store'
import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/Index.vue'
import LoginView from '../views/Login.vue'
import SignUpView from '../views/SignUp.vue'
import lodash from 'lodash';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      meta: { requiresAuth: true }
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
      path: '/signup',
      name: 'signup',
      component: SignUpView
    }
  ]
})

router.beforeEach(async (to, from) =>{
  const token = store.getters.getToken;
  if (lodash.isNil(token) || lodash.isEmpty(token)) {
    switch (to.name) {
      case 'home':
        return { path: '/login' }
    }
  }
  else {
    switch (to.name) {
      case 'login':
        return { path: '/' }
      case 'home':
        await store.dispatch('GetMovies', {
          pageIndex: 0
        });
        break
    }
  }
})

export default router
