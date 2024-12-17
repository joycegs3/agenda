import { createMemoryHistory, createRouter } from 'vue-router'

import HomeView from '../views/HomeView.vue';
import RegisterView from '@/components/UserRegister.vue';
import DashboardView from '@/views/DashboardView.vue';

const routes = [
  { path: '/',
    name: 'home',
    component: HomeView 
  },
  {
    path: '/dashboard',
    name: 'dashboard',
    component: DashboardView,
  },
  {
    path: '/register',
    name: 'register',
    component: RegisterView,
  },
]

const router = createRouter({
  history: createMemoryHistory(),
  routes,
})

export default router