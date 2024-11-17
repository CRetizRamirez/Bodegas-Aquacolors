import './App.css';
import Login from './Components/Login';
import Encargado from './Components/Encargado';
import Gerente from './Components/Gerente';
import Vendedor from './Components/Vendedor'
import React, { useState } from 'react';
import ProtectedRoutes from './Components/ProtectedRoutes.jsx';

import { Routes, Route } from 'react-router-dom';

export const AuthContext = React.createContext(null);

function App() {

    const [isAuthenticated, setIsAuthenticated] = useState(false);

    const login = () => setIsAuthenticated(true);
    const logout = () => setIsAuthenticated(false);

    return (
        <div>
            <AuthContext.Provider value={{ isAuthenticated, login, logout }}>
                <Routes>
                    <Route path='/' element={<Login />} />
                    <Route path='*' element={<Login />} />
                    <Route element={<ProtectedRoutes isAuthenticated={isAuthenticated}/>}>
                        <Route path='/gerente' element={<Gerente />} />
                        <Route path='/encargado' element={<Encargado />} />
                        <Route path='/vendedor' element={<Vendedor />} />
                    </Route>
                </Routes>
            </AuthContext.Provider>
        </div>
    );
}

export default App;