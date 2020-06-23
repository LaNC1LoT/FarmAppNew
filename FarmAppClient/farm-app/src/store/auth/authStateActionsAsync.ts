import { ILoginParams } from "../../api/dto/Auth";
import { callApi } from "../apiActionsAsync";
import { authActions } from "./authActions";
import { BASE_URL } from "../../core/constants";
import { Dispatch } from "react";
import {regions} from "../../core/api";
import {GET_REGION_ERROR, GET_REGION_REQUEST, GET_REGION_RESPONCE} from "../region/regionActions";
import {IAppState} from "../../core/mainReducer";

export const callApiLogin = (params: ILoginParams, goToFrom: (result?:object|null) => void) =>
  callApi(
    params,
    { url:`api/Users/authenticate` , method: "POST", usePublicToken: true,headers:{
      "content-type":"application/json",
        'Access-Control-Allow-Origin': '*',
    } },
    authActions.login,
        (result?:object|null)=>goToFrom(result)
  );
