import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { GlobalStyle } from './GlobalStyle';
import ShoppingCart from './pages/ShoppingCart';

export default function App() {
  return (
    <div className="app">
      <Routes>
        <Route index element={<ShoppingCart />} />
      </Routes>
      <GlobalStyle/>
    </div>
  );
}