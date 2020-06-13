import React from "react";
import "./App.css";
import { SignIn } from "./modules/auth/SignIn";
import { ResponsiveDrawer } from "./modules/menu/ResponsiveDrawer";
import { BrowserRouter, Switch, Route, Redirect } from "react-router-dom";

const NotFoundRedirect = () => <Redirect to='/farm-app/' />

const App = () => {
  return (
    <BrowserRouter>
      <Switch>
        <Route exact path="/farm-app/auth/">
          <SignIn />
        </Route>
        <Route path="/farm-app/">
          <ResponsiveDrawer />
        </Route>
         <Route component={NotFoundRedirect} />
      </Switch>
    </BrowserRouter>
  );
}

export default App;
