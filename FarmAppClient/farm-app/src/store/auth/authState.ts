import { reducerWithInitialState } from "typescript-fsa-reducers";
import { login, logout } from "./authActions";
import { IAccount } from "../../api/dto/Auth";

interface State {
  loadState: boolean;
  error: Error | null;
  account: IAccount | null;
}

const initial: State = {
  account: null,
  error: null,
  loadState: false,
};

export const authReducer = reducerWithInitialState(initial)
  .case(login.started, (state) => ({
    ...state,
    loadState: true,
    error: null,
  }))
  .case(login.done, (state, { result }) => ({
    ...state,
    user: result,
    loadState: false,
    error: null,
  }))
  .case(login.failed, (state, { error }) => ({
    ...state,
    loadState: false,
    error,
  }))
  .case(logout, () => initial);
