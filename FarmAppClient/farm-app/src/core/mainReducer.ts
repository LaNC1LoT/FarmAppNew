import { Action, combineReducers } from "redux";
import { ThunkAction, ThunkDispatch } from "redux-thunk";
import authReducer, { Auth } from "../store/auth/authState";
import pharmacyReducer, { Pharmacy } from "../store/pharmacy/parmacyState";
import regionReducer, { Region } from "../store/region/regionState";

export const createMainReducer = () =>
  combineReducers({
    auth: authReducer,
    pharmacy: pharmacyReducer,
    region: regionReducer
  });

export interface IAppState {
  auth: Auth,
  pharmacy: Pharmacy,
  region: Region
}
//extends ReturnType<ReturnType<typeof createMainReducer>> {}

export interface IAppDispatch extends ThunkDispatch<IAppState, Error, Action> { }

export interface IThunkAction
  extends ThunkAction<Promise<void>, IAppState, {}, Action> { }
