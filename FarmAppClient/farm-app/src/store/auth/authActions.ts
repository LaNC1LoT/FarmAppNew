import {actionCreatorFactory} from "typescript-fsa";
import {ILoginParams, IAccount} from "../../api/dto/Auth";
import {authRequest, regRequest} from "../../api/BaseFetch";
import {Dispatch} from "redux";
import {IAppState} from "../../core/mainReducer";
import {User} from "./authState"
import {GET_REGION_ERROR, GET_REGION_REQUEST, GET_REGION_RESPONCE} from "../region/regionActions";
import {regions} from "../../core/api";
import {dispatch} from "../../index";


const authActionCreator = actionCreatorFactory("auth");

export const login = authActionCreator.async<ILoginParams,
    User | null,
    Error>("LOGIN");


export const logout = authActionCreator("LOGOUT");

export const authActions = {
    login,
    logout,
};

export const REGISTRATION_REQUEST = 'REGISTRATION_REQUEST'
export const REGISTRATION_RESPONSE = 'REGISTRATION_RESPONSE'
export const REGISTRATION_ERROR = 'REGISTRATION_ERROR'

export const registration = ({login ,password ,firstName, lastName}:{login: string,password: string,firstName: string, lastName: string}) => async (dispatch: Dispatch<any>, getState: () => IAppState): Promise<void> => {
    dispatch({type: REGISTRATION_REQUEST})
    try {
      const response = await regRequest(login,password,firstName,lastName)
            if (response.status === 200) {
                dispatch({
                    type: REGISTRATION_RESPONSE,
                })
            } else {
                dispatch({
                    type: REGISTRATION_ERROR,
                    payload: response.status
                })
            }
    } catch (error) {
        dispatch({
            type: REGISTRATION_ERROR,
            payload: error
        })
    }
}


export const RESTORE_AUTH = 'RESTORE_AUTH'
export const restoreAuth = (result: object | null) => async (dispatch: Dispatch<any>) => {
    dispatch({
        type: RESTORE_AUTH,
        payload: result
    })
}


