import { Dispatch } from "react"
import { IAppState } from "../../core/mainReducer"
import { pharmacies } from "../../core/api"


export const GET_PHARMACIES_REQUEST = 'GET_PHARMACIES_REQUEST'
export const GET_PHARMACIES_RESPONCE = 'GET_PHARMACIES_RESPONCE'
export const GET_PHARMACIES_ERROR = 'GET_PHARMACIES_ERROR'

export const getPharmacies=()=>async(dispatch:Dispatch<any>,getState:()=>IAppState): Promise<void> =>{
    dispatch({type:GET_PHARMACIES_REQUEST})
    try{
        const token =getState().auth?.user?.token
        if(token){
           const response = await pharmacies(token)
           const responseJson = await response.json();
           console.log("response",response,responseJson)
           if(response.ok){
               dispatch({
                   type:GET_PHARMACIES_RESPONCE,
                   payload:responseJson
               })
           }
        }
    }

    catch(error){
        
    }
   
}