import React from "react";
import { Route, Navigate } from "react-router-dom";
import { isAuthenticated } from "./AuthGuard";

function AdminRoute({ element }) {
  return isAuthenticated() ? element : <Navigate to="/login" />;
}

export default AdminRoute;
