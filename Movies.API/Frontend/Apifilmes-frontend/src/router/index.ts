import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LoginView from '../views/LoginView.vue'
import RegisterView from '../views/RegisterView.vue'
import MoviesView from '../views/MoviesView.vue'
import AdminView from '../views/AdminView.vue'
import store from '../store'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', component: HomeView },
    { path: '/login', component: LoginView },
    { path: '/cadastro', component: RegisterView },
    { path: '/filmes', component: MoviesView },
    {
      path: '/admin',
      component: AdminView,
      beforeEnter: () => {
        if (!store.getters.isAdmin) return '/filmes'
      },
    },
  ],
})

export default router
