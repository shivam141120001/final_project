import { IAuthUser } from './auth-user';

export interface ILoginResponse {
  user: IAuthUser;
  token: string;
}
