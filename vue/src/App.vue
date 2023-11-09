<script setup lang="ts">
import { RouterLink, RouterView, useRoute, useRouter, type RouteLocationMatched } from 'vue-router'
import {
  NConfigProvider,
  NLayout,
  NLayoutSider,
  NLayoutHeader,
  NLayoutContent,
  NLayoutFooter,
  NLoadingBarProvider,
  NMenu,
  NIcon,
  NSpace,
  NButton,
  NImage,
  NMessageProvider,
  NDialogProvider,
  NBreadcrumb,
  NBreadcrumbItem,
  NDropdown
} from 'naive-ui'
import type { MenuOption, DropdownOption } from 'naive-ui'
import { useLangStore, useSizeStore, useThemeStore } from '@/stores'
import { ref, h, type Component, inject } from 'vue'
import {
  PersonOutline as PersonIcon,
  MenuOutline as MenuIcon,
  HomeOutline as HomeIcon,
  SettingsOutline as SettingIcon,
  PersonOutline as UserIcon
} from '@vicons/ionicons5'
import UseWindowComponent from '@/components/UseWindowComponent.vue'
import type { I18N } from '@/plugins/i18n'
import { AuthService } from '@/scripts/AuthService'

function renderIcon(icon: Component) {
  return () => h(NIcon, null, { default: () => h(icon) })
}

const i18n: I18N = <I18N>inject('i18n')
const route = useRoute()
const router = useRouter()
const themeStore = useThemeStore()
const langStore = useLangStore()
const sizeStore = useSizeStore()
const collapsed = ref(false)
const activeKey = ref<string | null>(null)

const menuOptions: MenuOption[] = [
  {
    label: () =>
      h(
        RouterLink,
        {
          to: {
            name: 'home',
            params: {
              lang: langStore.param,
              theme: themeStore.param
            }
          }
        },
        { default: () => i18n.$t('menu:home') }
      ),
    key: 'home',
    icon: renderIcon(HomeIcon)
  },
  {
    key: 'divider',
    type: 'divider'
  },
  {
    label: () => i18n.$t('menu:customers:customers'),
    icon: renderIcon(MenuIcon),
    key: 'menu:customers:customers',
    children: [
      {
        label: () =>
          h(
            RouterLink,
            {
              to: {
                name: 'customerInfo'
              }
            },
            { default: () => i18n.$t('menu:customers:customerInfo') }
          ),
        key: 'customer',
        icon: renderIcon(PersonIcon)
      }
    ]
  },
  {
    label: () => i18n.$t('menu:settings:settings'),
    icon: renderIcon(SettingIcon),
    key: 'menu:settings:settings',
    children: [
      {
        label: () =>
          h(
            RouterLink,
            {
              to: {
                name: 'user'
              }
            },
            { default: () => i18n.$t('menu:settings:user') }
          ),
        key: 'user',
        icon: renderIcon(UserIcon)
      }
    ]
  }
]
const sizeOptions: DropdownOption[] = [
  {
    label: () => i18n.$t('global:size:small'),
    key: 'small',
    disabled: sizeStore.size == 'small'
  },
  {
    label: () => i18n.$t('global:size:medium'),
    key: 'medium',
    disabled: sizeStore.size == 'medium'
  },
  {
    label: () => i18n.$t('global:size:large'),
    key: 'large',
    disabled: sizeStore.size == 'large'
  }
]

function langChange() {
  langStore.change()
  route.params.lang = langStore.param
  router.replace({ name: route.name ?? '', params: route.params, force: true })
}

function themeChange() {
  themeStore.change()
  route.params.theme = themeStore.param
  router.replace({ name: route.name ?? '', params: route.params, force: true })
}

function sizeChange(key: 'small' | 'medium' | 'large') {
  sizeStore.change(key)
  sizeOptions.forEach((o) => {
    o.disabled = key == o.key
  })
}

function logout() {
  AuthService.Logout()
}

function breadcrumb(routeMatch: RouteLocationMatched, isLast: boolean) {
  const tmp = routeMatch.path.split('/').slice(3)
  if (!isLast) {
    tmp.push(tmp[tmp.length - 1])
  }
  const key = tmp.join(':')
  return i18n.$t(`menu:${key}`)
}
</script>

<template>
  <n-config-provider
    :locale="langStore.lang"
    :date-locale="langStore.date"
    :theme="themeStore.theme"
  >
    <n-dialog-provider>
      <n-message-provider>
        <n-loading-bar-provider>
          <n-layout style="height: 100vh">
            <n-layout-header class="g-header">
              <div class="m-head">
                <div class="m-head-left">
                  <n-image width="64" src="/doraemon.ico" preview-disabled></n-image>
                </div>
                <div class="m-head-center">
                  <n-breadcrumb>
                    <n-breadcrumb-item v-for="(r, i) in route.matched">
                      {{ breadcrumb(r, i == route.matched.length - 1) }}
                    </n-breadcrumb-item>
                  </n-breadcrumb>
                </div>
                <div class="m-head-right">
                  <n-space justify="end" :size="sizeStore.size">
                    <n-button quaternary @click="langChange" :size="sizeStore.size">{{
                      $t('global:language')
                    }}</n-button>
                    <n-button quaternary @click="themeChange" :size="sizeStore.size">{{
                      $t('global:theme:' + themeStore.param)
                    }}</n-button>
                    <n-dropdown
                      trigger="hover"
                      :options="sizeOptions"
                      @select="sizeChange"
                      :size="sizeStore.size"
                    >
                      <n-button quaternary :size="sizeStore.size">{{
                        $t('global:size:' + sizeStore.size)
                      }}</n-button>
                    </n-dropdown>
                  </n-space>
                </div>
              </div>
            </n-layout-header>
            <n-layout has-sider>
              <n-layout-sider
                collapse-mode="width"
                :collapsed-width="72"
                :width="240"
                show-trigger="bar"
                bordered
                style="height: var(--content-height)"
                @collapse="collapsed = true"
                @expand="collapsed = false"
              >
                <n-menu
                  v-model:value="activeKey"
                  :collapsed="collapsed"
                  :collapsed-width="72"
                  :collapsed-icon-size="20"
                  :options="menuOptions"
                >
                </n-menu>
              </n-layout-sider>
              <n-layout-content style="height: var(--content-height)" :native-scrollbar="false">
                <router-view v-slot="{ Component, route }">
                  <transition name="slide-fade" mode="out-in">
                    <div :key="route.path">
                      <component :is="Component" />
                    </div>
                  </transition>
                </router-view>
              </n-layout-content>
            </n-layout>
            <n-layout-footer style="height: var(--footer-height)" bordered>footer</n-layout-footer>
          </n-layout>
          <use-window-component />
        </n-loading-bar-provider>
      </n-message-provider>
    </n-dialog-provider>
  </n-config-provider>
</template>

<style scoped>
.g-header {
  height: var(--head-height);
  border-bottom: 1px solid var(--n-border-color);
}

.m-head {
  display: flex;
  justify-content: space-between;
  align-items: center;
  height: 100%;
}
.m-head-left {
  width: 240px;
  text-align: center;
}
.m-head-center {
  flex: 1;
}
.m-head-right {
  width: 300px;
  padding-right: var(--padding-right);
}
</style>
