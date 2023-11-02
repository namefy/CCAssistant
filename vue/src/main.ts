import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import { useThemeStore } from './stores/theme'
import { useLangStore } from './stores/lang'

import i18n from './plugins/i18n'

import { AuthService } from '@/scripts/AuthService'

const user = await AuthService.GetUser()

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.use(i18n)

app.mount('#app')

const themeStore = useThemeStore()
const langStore = useLangStore()

router.beforeEach(async (to, from) => {
  if (user?.expired) {
    await AuthService.Login()
    return false
  }
  if (to.path == '/') {
    return { name: 'home', params: { theme: 'light', lang: 'zh-cn' } }
  }
  if (to.params.theme != themeStore.param) {
    themeStore.change()
  }
  if (to.params.lang != langStore.param) {
    langStore.change()
  }
  window.$loading?.start()
  setTimeout(() => {
    window.$loading?.finish()
  }, 500)

  return true
})
