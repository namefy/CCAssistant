import { type App } from 'vue'
import en from './en'
import zhCN from './zh-CN'
import { useLangStore } from '@/stores/lang'

const t = (key: string) => {
  const langStore = useLangStore()
  key = `${langStore.param}:${key}`
  return key.split(':').reduce((o, i) => {
    if (o) return o[i]
  }, options)
}

const options: any = {
  en: en,
  'zh-cn': zhCN,
  $t: t
}

export default {
  install: (app: App<Element>) => {
    app.config.globalProperties.$t = t
    app.provide('i18n', options)
  }
}
