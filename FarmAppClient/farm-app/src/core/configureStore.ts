import { createBrowserHistory } from "history";
import {Action, applyMiddleware, compose, createStore, Dispatch, MiddlewareAPI} from 'redux'
import thunk from "redux-thunk";
import { composeWithDevTools } from "redux-devtools-extension";
import { IAppState, createMainReducer } from "../core/mainReducer";
import { persistReducer, persistStore } from "redux-persist";
import storage from "redux-persist/lib/storage";
export const history = createBrowserHistory();

const persistWhitelist: Array<keyof IAppState> = ["auth"];
const persistConfig = {
  key: "root",
  storage,
  whitelist: ["auth"],
};

const composeEnhancers =
  process.env.NODE_ENV === "production"
    ? compose
    : composeWithDevTools({
        shouldHotReload: false,
      });





export function configureStore() {
  const customMiddleWare = (store: MiddlewareAPI<any, any>) => (next: Dispatch,
    ) => async (action: Action) => {

    next(action);
};
  const middleware = [thunk,customMiddleWare];
  const enhancer = composeEnhancers(applyMiddleware(...middleware));
  const persistedReducer = persistReducer(persistConfig, createMainReducer());
  const store = createStore(persistedReducer, enhancer);
  // const store: Cards<IAppState, Action> = createStore(persistedReducer, enhancer);
  //@ts-ignore
  // const persistor = persistStore(store);
  // persistor.purge();
  return { store };
}
