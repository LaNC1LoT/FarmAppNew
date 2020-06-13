import { IRole } from "./Role";
import { IApiMethod } from "./ApiMethod";

export interface IApiMethodRole {
    id: number;
    apiMethod: IApiMethod;
    role: IRole;
    isDeleted: boolean;
}