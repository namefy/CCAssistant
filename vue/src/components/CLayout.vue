<script setup lang="ts">
import { NLayout, NLayoutHeader, NLayoutContent, NSpace } from 'naive-ui'
import { useSizeStore } from '@/stores'

const sizeStore = useSizeStore()
</script>
<template>
  <n-layout
    embedded
    style="
      min-height: var(--content-height);
      padding: var(--padding-top) var(--padding-right) var(--padding-bottom) var(--padding-left);
    "
  >
    <n-space vertical :size="sizeStore.size">
      <n-layout-header
        style="
          padding: var(--padding-top) var(--padding-right) var(--padding-bottom) var(--padding-left);
        "
      >
        <slot name="search"></slot>
      </n-layout-header>
      <n-layout>
        <n-space vertical :size="sizeStore.size">
          <n-layout-header
            style="
              padding: var(--padding-top) var(--padding-right) var(--padding-bottom)
                var(--padding-left);
            "
          >
            <slot name="tool"></slot>
          </n-layout-header>
          <n-layout
            has-sider
            style="
              padding: var(--padding-top) var(--padding-right) var(--padding-bottom)
                var(--padding-left);
            "
          >
            <n-layout-content :native-scrollbar="false">
              <slot name="table"></slot>
            </n-layout-content>
          </n-layout>
        </n-space>
      </n-layout>
    </n-space>
  </n-layout>
</template>
