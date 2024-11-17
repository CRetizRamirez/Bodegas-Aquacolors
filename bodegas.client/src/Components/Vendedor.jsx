import { useEffect, useState, useContext } from "react";
import { useLocation, useNavigate } from 'react-router-dom';
import { AuthContext } from '../App';

function Vendedor() {

    // La 2 y de la 7 a la 14 es para obtener el Usuario actual
    const navigate = useNavigate();
    const location = useLocation();
    useEffect(() => {
        if (!location.state || !location.state.usuario) {
            navigate('/login'); // O donde sea apropiado
        }
    }, [location, navigate]);
    const user = location.state?.usuario

    const { logout } = useContext(AuthContext); //Para el Logout

    const [buscar, setBuscar] = useState("");
    const [stockCompleto, setStockCompleto] = useState([]);

    const leerStockCompletoSP = async () => {
        try {
            const response = await fetch("/api/sp/mostrarstockcompleto");
            setStockCompleto(await response.json());
        }
        catch (error) {
            console.error('Error en leerStockCompletoSP', error);
        }
    }

    // Codigo para el buscador
    const search = (e) => {
        setBuscar(e.target.value)
    }
    const results = buscar
        ? stockCompleto.filter((dato) =>
            dato.articulo.toLowerCase().includes(buscar.toLowerCase())
        )
        : [];

    useEffect(() => {
        leerStockCompletoSP();
    },[])

    return (
        <div className="container mt-5">
            <h2>Bodegas Aquacolors</h2>
            <h3>Bienvenid@: {user} </h3>
            <button
                className="btn btn-secondary m-3"
                onClick={() => {
                    logout();  // Función que limpie la autenticación
                    navigate('/login', { replace: true });
                }}
            >
                Salir
            </button>
            <input
                className="form-control mt-4"
                type="text"
                placeholder="🔎  Buscar artículo ..."
                onChange={search}
            />

            <table className="table">
                <thead>
                    <tr>
                        <th>Clave</th>
                        <th>Artículo</th>
                        <th>Stock</th>
                        <th>Bodega</th>
                        <th>Ubicación</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        results.map(item => (
                            <tr key={item.idStock}>
                                <td>{item.clave}</td>
                                <td>{item.articulo}</td>
                                <td>{item.stock}</td>
                                <td>{item.bodega}</td>
                                <td>{item.ubicacion}</td>
                            </tr>
                        ))
                    }
                </tbody>
            </table>
        </div>
    );
}

export default Vendedor;