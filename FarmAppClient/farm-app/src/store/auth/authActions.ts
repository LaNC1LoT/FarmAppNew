import { actionCreatorFactory } from "typescript-fsa";
import { ILoginParams, IAccount } from "../../api/dto/Auth";

const authActionCreator = actionCreatorFactory("auth");

export const login = authActionCreator.async<
  ILoginParams,
  IAccount | null,
  Error
>("LOGIN");
export const logout = authActionCreator("LOGOUT");

export const authActions = {
  login,
  logout,
};
