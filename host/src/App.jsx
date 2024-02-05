import React, { useEffect } from "react";

import "./index.css";
import { Navigate, Route, Routes, } from "react-router-dom";
import PrivateRoute from "./PrivateRoute";
import { getUserRole, isPersistedLocalStorageState } from "./helpers";
import TopBar from "./components/TopBar";
import { Provider } from "react-redux";
import store from "../shared/redux";
import NotificationComponent from "./components/NotificationComponent";
import OrderConfirmation from "./pages/OrderConfirmation";

const ProductCatalog = React.lazy(() => import('productCatalog/ProductCatalog'));
const ShoppingCart = React.lazy(() => import('shoppingCart/ShoppingCart'));
const Login = React.lazy(() => import('auth/Auth'));

export default function App() {
  useEffect(() => {
    const sessionUser = isPersistedLocalStorageState("user");
    if (sessionUser) {
      console.log(getUserRole(sessionUser.token));
      const tokenExpiry = new Date(sessionUser.tokenExpiry).getTime();
      const currentTime = new Date().getTime();
      if (tokenExpiry <= currentTime) {
        localStorage.removeItem("user");
        localStorage.removeItem("cart");
      }
    }
  }, []);

  return (
    <div className="app">
      <Provider store={store}>
        <TopBar />
        <NotificationComponent/>
        <React.Suspense fallback={<div>Loading...</div>}>
          <Routes>
            <Route path="/login/*" element={<Login />} />
            <Route
              path="/product-catalog/*"
              element={
                <PrivateRoute>
                  <ProductCatalog />
                </PrivateRoute>
              }
            />
            <Route
              path="/cart/*"
              element={
                <PrivateRoute>
                  <ShoppingCart />
                </PrivateRoute>
              }
            />
            <Route
              path="/order-confirmation"
              element={
                <PrivateRoute>
                  <OrderConfirmation/>
                </PrivateRoute>
              }
            />
            <Route path="*" element={<Navigate to="/product-catalog" />} />
          </Routes>
        </React.Suspense>
      </Provider>
    </div>
  );
};
