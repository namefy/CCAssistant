import { ref } from 'vue'
import { defineStore } from 'pinia'

export const useSizeStore = defineStore('size', () => {
  const size = ref<'small' | 'medium' | 'large'>('medium')
  function change(input: 'small' | 'medium' | 'large') {
    size.value = input
  }

  return { size, change }
})
