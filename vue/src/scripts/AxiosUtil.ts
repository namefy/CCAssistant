import axios, { type AxiosError, type AxiosInstance, type AxiosRequestConfig } from 'axios'
import { AuthService } from './AuthService'
import { Convert } from './Convert'
import { type I18N } from '@/plugins/i18n'
import { inject } from 'vue'

export interface IAxiosResult {
  Success: boolean
  Data: any
}

export class AxiosUtil {
  private _instance: AxiosInstance
  private _i18n: I18N

  constructor(baseUrl: string) {
    const instance = axios.create()
    instance.defaults.baseURL = baseUrl
    this._instance = instance
    this._i18n = <I18N>inject('i18n')
  }

  public async Request(
    controller: string,
    funcName: string,
    params?: any,
    data?: any,
    showMsg: boolean = false,
    prex: string = '/api/app'
  ): Promise<IAxiosResult> {
    const user = await AuthService.GetUser()
    if (user == undefined || user?.expired) {
      await AuthService.Login()
      return <IAxiosResult>{}
    }
    if (this._instance.defaults.headers.common['Authorization'] == undefined) {
      this._instance.defaults.headers.common[
        'Authorization'
      ] = `${user?.token_type} ${user?.access_token}`
    }
    const param = this.GetUrlByFuncName(funcName)
    const url = `${prex}/${Convert.KebabCase(controller)}${param.action}`

    const config: AxiosRequestConfig = {
      url: url,
      method: param.method,
      params: params,
      data: data,
      headers: {
        'Accept-Language': localStorage.getItem('locale'),
        'Cache-Control': 'no-cache'
      }
    }
    try {
      window.$loading?.start()
      const response = await this._instance.request(config)
      if (showMsg) {
        window.$message.success(this._i18n.$t('tip:success'))
      }
      return {
        Success: true,
        Data: response.data
      }
    } catch (e) {
      const err = e as AxiosError
      console.error(err)
      const message = (<any>err.response?.data)?.error?.message ?? err.message
      window.$message.error(message)
      return {
        Success: false,
        Data: err.response?.data
      }
    } finally {
      window.$loading?.finish()
    }
  }

  private GetUrlByFuncName(funcName: string) {
    const temps = funcName.split('_')
    const method = this.MapMethod(temps[0])
    let action = ''
    for (let i = 1; i < temps.length; i++) {
      let name = temps[i]
      name = Convert.KebabCase(name)
      action += `/${name}`
    }
    return { method, action }
  }

  private MapMethod(method: string): string {
    method = method.toLowerCase()
    switch (method) {
      case 'get':
      case 'getlist':
        return 'get'
      case 'create':
      case 'post':
        return 'post'
      case 'update':
      case 'put':
        return 'put'
      case 'delete':
        return 'delete'
    }
    return method
  }
}
