import React, { useContext } from 'react';
import { AuthContext } from '../App.jsx';
import { useState } from "react";
import { useNavigate } from "react-router-dom";

function Login() {

    const { login } = useContext(AuthContext);
    const navigate = useNavigate();

    const [correo, setCorreo] = useState("");
    const [contrasena, setContrasena] = useState("");

    const handleCorreoChange = (value) => {
        setCorreo(value);
    }

    const handleContrasenaChange = (value) => {
        setContrasena(value)
    }

    const handleLoginSP = async () => {
        const data = {
            Correo: correo,
            Contrasena: contrasena,
            Rol: "",  // No sirve, pero lo pide el Postman para funcionar
            Usuario: ""  // No sirve, pero lo pide el Postman para funcionar
        };
        const url = ("/api/sp/login");
        try {
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })
            if (response.ok) {
                const result = await response.json();
                if (result.rol == "Gerente") {
                    navigate("/gerente", { state: { usuario: result.usuario, idusuario: result.idUser } });
                } else if (result.rol == "Encargado") {
                    navigate("/encargado", { state: { usuario: result.usuario, idusuario:result.idUser } })
                } else if (result.rol == "Vendedor") {
                    navigate("/vendedor", { state: { usuario: result.usuario, idusuario: result.idUser } })
                }
                login();
            } else {
                alert("Credenciales invalidas")
            }
        }
        catch (error) {
            console.error("Error en el handleLoginSP", error);
        }
    }


    return (
        <div className="contenedor">
            <section className=" text-center text-lg-start">
                <div className="card mb-3 bg-light">
                    <div className="row g-0 d-flex align-items-center">
                        <div className="col-lg-4 d-none d-lg-flex">
                            <img
                                src="https://alt72.com.ar/wp-content/uploads/2014/09/login.png"
                                alt="Usuario"
                                className="w-100"
                            />
                        </div>
                        <div className="col-lg-8">
                            <div className="card-body py-5 px-md-5">
                                <h3 className="mb-4">Stock Bodegas Aquacolors</h3>
                                <form>
                                    <div className="form-outline mb-4">
                                        <input
                                            placeholder="Correo"
                                            type="text"
                                            id="correo"
                                            className="form-control"
                                            onChange={(e) => handleCorreoChange(e.target.value)}
                                        />
                                    </div>
                                    <div className="form-outline mb-4">
                                        <input
                                            placeholder="Contraseña"
                                            type="password"
                                            id="contrasena"
                                            className="form-control"
                                            onChange={(e) => handleContrasenaChange(e.target.value)}
                                        />
                                    </div>
                                    <button
                                        type="button"
                                        className="btn btn-success btn-block mb-4"
                                        onClick={() => handleLoginSP()}
                                    >
                                        Ingresar
                                    </button>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    );
}

export default Login;