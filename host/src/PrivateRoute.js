import React from "react";
import { Navigate } from 'react-router-dom';

const PrivateRoute = ({children}) => {
    const user = localStorage.getItem('user');
    const isAuthenticated = user != null;
    return isAuthenticated ? children : <Navigate to="/login" />;
};

export default PrivateRoute;