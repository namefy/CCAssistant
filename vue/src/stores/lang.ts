import { ref } from 'vue'
import { defineStore } from 'pinia'
import { zhCN, dateZhCN, enUS, dateEnUS } from 'naive-ui'
import type { NLocale, NDateLocale } from 'naive-ui'

export const useLangStore = defineStore('lang', () => {
  const lang = ref<NLocale>(zhCN)
  const date = ref<NDateLocale>(dateZhCN)
  const param = ref('zh-cn')
  function change() {
    if (param.value == 'zh-cn') {
      lang.value = enUS
      date.value = dateEnUS
      param.value = 'en'
    } else {
      lang.value = zhCN
      date.value = dateZhCN
      param.value = 'zh-cn'
    }
  }

  return { lang, date, param, change }
})
