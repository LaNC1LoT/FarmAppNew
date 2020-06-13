import { IUser } from "./User";

export interface IAuthResponse extends IUser {
  token: string;
}
