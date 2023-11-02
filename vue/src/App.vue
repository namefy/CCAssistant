<script setup lang="ts">
import { RouterLink, RouterView, useRoute, useRouter } from 'vue-router'
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
  NDialogProvider
} from 'naive-ui'
import type { MenuOption } from 'naive-ui'
import { useThemeStore } from '@/stores/theme'
import { useLangStore } from '@/stores/lang'
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
    label: () => i18n.$t('menu:customer:customer'),
    icon: renderIcon(MenuIcon),
    key: 'menu:customer:customer',
    children: [
      {
        label: () =>
          h(
            RouterLink,
            {
              to: {
                name: 'customer'
              }
            },
            { default: () => i18n.$t('menu:customer:customerInfo') }
          ),
        key: 'customer',
        icon: renderIcon(PersonIcon)
      }
    ]
  },
  {
    label: () => i18n.$t('menu:setting:setting'),
    icon: renderIcon(SettingIcon),
    key: 'menu:setting:setting',
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
            { default: () => i18n.$t('menu:setting:user') }
          ),
        key: 'user',
        icon: renderIcon(UserIcon)
      }
    ]
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

function logout() {
  AuthService.Logout()
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
                <div class="m-head-center"></div>
                <div class="m-head-right">
                  <n-space justify="end">
                    <n-button quaternary @click="langChange">{{ $t('global:language') }}</n-button>
                    <n-button quaternary @click="themeChange">{{
                      $t('global:theme:' + themeStore.param)
                    }}</n-button>
                  </n-space>
                </div>
              </div>
            </n-layout-header>
            <n-layout-content bordered>
              <n-layout has-sider>
                <n-layout-sider
                  collapse-mode="width"
                  :collapsed-width="72"
                  :width="240"
                  show-trigger="bar"
                  bordered
                  style="height: 88vh"
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
                <n-layout-content style="height: 88vh">
                  <router-view> </router-view>
                </n-layout-content>
              </n-layout>
            </n-layout-content>
            <n-layout-footer style="height: 4vh">footer</n-layout-footer>
          </n-layout>
          <use-window-component />
        </n-loading-bar-provider>
      </n-message-provider>
    </n-dialog-provider>
  </n-config-provider>
</template>

<style scoped>
.g-header {
  height: 8vh;
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
  width: 200px;
  padding-right: var(--padding-right);
}
</style>
