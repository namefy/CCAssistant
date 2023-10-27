import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import CustomerView from '@/views/Customer/CustomerView.vue'

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
    }
  ]
})

export default router
