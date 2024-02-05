import React from 'react';
import { Routes, Route } from 'react-router-dom';
import ProductCatalog from './pages/ProductCatalog';
import { GlobalStyle } from './GlobalStyle';

export default function App() {
  return (
    <div className="app">
      <Routes>
        <Route index element={<ProductCatalog />} />
      </Routes>
      <GlobalStyle/>
    </div>
  );
}