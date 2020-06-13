import { IRole } from "./Role";

export interface IUser {
  id: number;
  firstName: string;
  lastName: string;
  userName: string;
  role: IRole;
}
