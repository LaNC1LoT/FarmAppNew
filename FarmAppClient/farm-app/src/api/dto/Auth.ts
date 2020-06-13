import { IUser } from "./User";

export interface ILoginParams {
  login: string;
  password: string;
  onSuccess?: () => void;
}

export interface IAccount extends IUser {
  token: string;
}
