import Oidc from 'oidc-client'
import { environment } from '@/environments/environment'

export class AuthService {
  private static readonly _mgr = new Oidc.UserManager(<any>environment.oAuthConfig)

  /**获取当前用户 */
  public static async GetUser(): Promise<Oidc.User | undefined> {
    const user = await this._mgr.getUser()
    if (user && !user?.expired) {
      return user
    } else {
      await this.Login()
    }
  }

  /**用户登录*/
  public static async Login(): Promise<void> {
    this._mgr.signinRedirect()
  }

  /**用户注销*/
  public static async Logout(): Promise<void> {
    this._mgr.signoutRedirect()
  }
}
