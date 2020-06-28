import { ActionWithPayload } from "../auth/authState";
import * as types from "./parmacyActions"

export interface Pharmacy {
    isFetchFarmacy: boolean;
    errorFarmacy: null | string;
    farmacy: null | object;
}

const initial: Pharmacy = {
    isFetchFarmacy: false,
    errorFarmacy: null,
    farmacy: null
};

export default (state: Pharmacy = initial, action: ActionWithPayload<any>) => {

    switch (action.type) {
        case types.GET_PHARMACIES_REQUEST:
            return {
                ...state,
                isFetchFarmacy: true,
                errorFarmacy: null,
            }
        case types.GET_PHARMACIES_RESPONCE:
            return {
                ...state,
                isFetchFarmacy: false,
                errorFarmacy: null,
                farmacy: action.payload
            }
        case types.GET_PHARMACIES_ERROR:
            return {
                ...state,
                isFetchFarmacy: false,
                errorFarmacy: action.payload
            }

        case "auth/LOGOUT":
            return initial
        default:
            return state
    }
}