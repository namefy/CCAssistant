import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import CustomerView from '@/views/customers/CustomerView.vue'
import UserView from '@/views/settings/UserView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/:lang/:theme',
      name: 'home',
      component: HomeView
    },
    {
      path:'',
      children: [
        {
          path: '/:lang/:theme/customer',
          name: 'customer',
          component: CustomerView
        }
      ]
    },
    {
      path:'',
      children: [
        {
          path: '/:lang/:theme/user',
          name: 'user',
          component: UserView
        }
      ]
    }
  ]
})

export default router
