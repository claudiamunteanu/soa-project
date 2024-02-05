import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Login from './pages/Login'
import { GlobalStyle } from './GlobalStyle';

export default function App() {
  return (
    <div className="app">
      <Routes>
        <Route index element={<Login />} />
      </Routes>
      <GlobalStyle />
    </div>
  );
}