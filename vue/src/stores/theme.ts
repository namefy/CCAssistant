import { ref } from 'vue'
import { defineStore } from 'pinia'
import { darkTheme } from 'naive-ui'
import type { GlobalTheme } from 'naive-ui'

export const useThemeStore = defineStore('theme', () => {
  const theme = ref<GlobalTheme | null>(null)
  const param = ref('light')
  function change() {
    theme.value = theme.value == null ? darkTheme : null
    param.value = param.value == 'light' ? 'dark' : 'light'
  }

  return { theme, param, change }
})
