import { ILoginParams } from "../../api/dto/Auth";
import { callApi } from "../apiActionsAsync";
import { authActions } from "./authActions";

export const callApiLogin = (params: ILoginParams, goToFrom: () => void) =>
  callApi(
    params,
    { url: "Users/authenticate", method: "POST", usePublicToken: true },
    authActions.login,
    goToFrom
  );
