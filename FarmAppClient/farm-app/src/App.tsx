import React, {useEffect} from "react";
import "./App.css";
import SignIn from "./modules/auth/SignIn";
import SignUp from "./modules/auth/SignUp";
import ResponsiveDrawer from "./modules/menu/ResponsiveDrawer";
import {BrowserRouter, Switch, Route, Redirect, useLocation, useHistory} from "react-router-dom";
import {logout, restoreAuth} from "./store/auth/authActions";
import {useDispatch} from "react-redux";

const NotFoundRedirect = () => {
  const rememberMe = localStorage.getItem('auth')
  if (rememberMe !== null) {
    return <Redirect to='/farm-app/main/'/>
  } else {
    return <Redirect to='/farm-app/auth/'/>
  }
}

const App = () => {
  const dispatch = useDispatch();
  const history = useHistory();
  let rememberMe = localStorage.getItem('auth')
  useEffect(() => {
    rememberMe = localStorage.getItem('auth')
    console.log("rememberMe", rememberMe)
    if (rememberMe !== null) {
      const response = JSON.parse(rememberMe)
      dispatch(restoreAuth(response))
    } else {
      localStorage.clear();
      dispatch(logout())
      history.push('/auth/')
    }
  }, [])

  return (
    <BrowserRouter>
      <Switch>
        <Route exact path="/farm-app/auth/">
          <SignIn/>
        </Route>
        <Route exact path="/farm-app/reg/">
          <SignUp/>
        </Route>
            <Route path="/farm-app/">
              <ResponsiveDrawer/>
            </Route>

        <Route component={NotFoundRedirect}/>
      </Switch>

    </BrowserRouter>
  );
}

export default App;
