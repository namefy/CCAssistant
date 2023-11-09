import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import CustomerView from '@/views/customers/CustomerView.vue'
import UserView from '@/views/settings/UserView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      redirect: { name: 'home', params: { theme: 'light', lang: 'zh-cn' } }
    },
    {
      path: '/:lang/:theme/home',
      name: 'home',
      component: HomeView
    },
    {
      path: '/:lang/:theme/customers',
      children: [
        {
          path: '/:lang/:theme/customers/customerInfo',
          name: 'customerInfo',
          component: CustomerView
        }
      ]
    },
    {
      path: '/:lang/:theme/settings',
      children: [
        {
          path: '/:lang/:theme/settings/user',
          name: 'user',
          component: UserView
        }
      ]
    }
  ]
})

export default router
