import { Dispatch } from "react"
import { IAppState } from "../../core/mainReducer"
import { regions } from "../../core/api"


export const GET_REGION_REQUEST = 'GET_REGION_REQUEST'
export const GET_REGION_RESPONCE = 'GET_REGION_RESPONCE'
export const GET_REGION_ERROR = 'GET_REGION_ERROR'

export const getRegions = () => async (dispatch: Dispatch<any>, getState: () => IAppState): Promise<void> => {
    dispatch({ type: GET_REGION_REQUEST })
    try {
        const token = getState().auth?.user?.token
        if (token) {
            const response = await regions(token)
            const responseJson = await response.json();
            console.log("response", response, responseJson)
            if (response.ok) {
                dispatch({
                    type: GET_REGION_RESPONCE,
                    payload: responseJson
                })
            } else {
                dispatch({
                    type: GET_REGION_ERROR,
                    payload: responseJson
                })
            }
        }
    }

    catch (error) {
        dispatch({
            type: GET_REGION_ERROR,
            payload: error
        })
    }
}