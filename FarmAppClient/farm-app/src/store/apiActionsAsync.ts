import { AsyncActionCreators } from "typescript-fsa";
import { IThunkAction } from "../core/mainReducer";
import { baseFetch, IHttpMethods } from "../api/BaseFetch";

export interface IFetchParams {
  url: string;
  method?: IHttpMethods;
  usePublicToken?: boolean;
  headers?: { [key: string]: string };
}
export const callApi = <P, R>(
  params: P,
  { url, method = "GET", usePublicToken, headers }: IFetchParams,
  actions: AsyncActionCreators<P, R | null, Error>,
  onSuccess?: (result?: R | null) => void
): IThunkAction => {
  return async (dispatch, getState) => {
    dispatch(actions.started(params));
    try {
      let token = process.env.REACT_APP_PUBLIC_TOKEN || "";
      if (!usePublicToken) {
        const account = getState().auth.user;
        token = account ? account.token : "";
      }
      const { status, message, result: result } = await baseFetch<P, R>(
        url,
        params,
        method,
        token,
        headers
      );

      if (status >= 400) {
        console.log("message", message);
        console.log("message", message);
        const error: any = result;

        dispatch(
          actions.failed({
            params,
            error: { name: status.toString(), message: error ? error : "" },
          })
        );
      } else {
        dispatch(actions.done({ params, result }));
        if (onSuccess != null) {
          onSuccess(result);
        }
      }
    } catch (error) {
      dispatch(actions.failed({ params, error }));
    }
  };
};
