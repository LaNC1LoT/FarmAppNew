import { ActionWithPayload } from "../auth/authState";
import * as types from "./regionActions"

export interface Regions {
    currentPage: number
    pageCount: number
    pageSize: number
    rowCount: number
    firstRowOnPage: number
    lastRowOnPage: number
    results: {
        id: number,
        parentId: 1,
        name: string,
        population: number,
        isDeleted: boolean
    }[]
};

export interface Region {
    isFetchRegion: boolean;
    errorRegion: null | string;
    regions: null | Regions
}

const initial: Region = {
    isFetchRegion: false,
    errorRegion: null,
    regions: null
};

export default (state: Region = initial, action: ActionWithPayload<any>) => {

    switch (action.type) {
        case types.GET_REGION_REQUEST:
            return {
                ...state,
                isFetchRegion: true,
                errorRegion: null,
            }
        case types.GET_REGION_RESPONCE:
            return {
                ...state,
                isFetchRegion: false,
                errorRegion: null,
                regions: action.payload
            }
        case types.GET_REGION_ERROR:
            return {
                ...state,
                isFetchRegion: false,
                errorRegion: action.payload
            }


        case "auth/LOGOUT":
            return initial
        default:
            return state
    }
}