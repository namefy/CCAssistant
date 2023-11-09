<script setup lang="ts">
import CLayout from '@/components/CLayout.vue'
</script>
<template>
  <c-layout>
    <template #search> search </template>
    <template #tool> tool </template>
    <template #table><div style="height: 100vh;">  table </div></template>
  </c-layout>
</template>
