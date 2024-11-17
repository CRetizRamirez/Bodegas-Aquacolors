import { useEffect, useState, useContext } from "react";
import { AuthContext } from '../App';
import { useForm } from 'react-hook-form';
import { useLocation, useNavigate } from 'react-router-dom';
import {
    Modal,
    ModalBody,
    ModalFooter,
    ModalHeader,
    Button
} from "reactstrap";

function Encargado() {

    // La 3 y de la 15 a la 23 es para obtener el idUsuario y Usuario actual
    const navigate = useNavigate();
    const location = useLocation();
    useEffect(() => {
        if (!location.state || !location.state.usuario) {
            navigate('/login'); // O donde sea apropiado
        }
    }, [location, navigate]);
    const user = location.state?.usuario;
    const idUser = location.state?.idusuario;

    const { logout } = useContext(AuthContext); //Para el Logout

    const { register, handleSubmit, reset } = useForm();

    const [stock, setStock] = useState([]);
    const [stockCompleto, setStockCompleto] = useState([])
    const [modalAgregar, setModalAgregar] = useState(false);
    const [modalEliminar, setModalEliminar] = useState(false);
    const [modalEditarStock, setModalEditarStock] = useState(false);
    const [cantidad, setCantidad] = useState();
    const [bodegas, setBodegas] = useState([])
    const [buscar, setBuscar] = useState("")
    const [registroSeleccionado, setRegistroSeleccionado] = useState({
        idStock: "",
        clave: "",
        articulo: "",
        stock: "",
        bodega: "",
        ubicacion: "",
        accion: "",
        idUsuario: "",
        fecha: ""
    })

    const seleccionarRegistro = (item, caso) => {
        setRegistroSeleccionado(item);
        caso === "EditarStock" ? setModalEditarStock(true) : setModalEliminar(true);
    }

    const leerStockSP = async () => {
        try {
            const response = await fetch("/api/sp/mostrarstock");
            setStock(await response.json());
        }
        catch (error) {
            console.error('Error en leerStockSP', error);
        }
    }

    const leerStockCompletoSP = async () => {
        try {
            const response = await fetch("/api/sp/mostrarstockcompleto");
            setStockCompleto(await response.json());
        }
        catch (error) {
            console.error('Error en leerStockCompletoSP', error);
        }
    }

    const leerBodegasSP = async () => {
        try {
            const response = await fetch("/api/sp/mostrarbodegas");
            setBodegas(await response.json());
        } catch (error) {
            console.error('Error en leerBodegasSP', error);
        }
    }

    //Agregar Articulo
    const onSubmit = async (dat) => {
        const datos = {
            ...dat,
            idUsuario: idUser,
            accion: "Ingreso"
            // fecha: new Date().toISOString() // Da fecha y hora
            // fecha: new Date().toISOString().split('T')[0] // Para que de solo la fecha
        }
        try {
            const response = await fetch("/api/sp/ingresararticulo", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(datos)
            })
            reset();
            leerStockSP();
        } catch (error) {
            console.error('Error en onSubmit', error);
        }
    }

    const agregarStockSP = async () => {
        let nuevoStock = Number(registroSeleccionado.stock) + Number(cantidad)
        const datos = {
            idStock: registroSeleccionado.idStock,
            stock: nuevoStock,
            idUsuario: idUser,
            accion: "Ingreso",
            clave: "",
            articulo: "",
            ubicacion: ""
        }
        try {
            const response = await fetch("/api/sp/actualizarstock", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(datos)
            });
            if (response.ok) {
                setCantidad(0)
                leerStockCompletoSP()
                leerStockSP()
                setModalEditarStock(false)
            }
        }
        catch (error) {
            console.error('Error en el try de agregarStockSP', error);
        }
    }

    const retirarStockSP = async () => {
        if (Number(cantidad) > Number(registroSeleccionado.stock)) {
            alert("No puedes retirar mas de: " + registroSeleccionado.stock + " piezas")
            return
        }
        let nuevoStock = Number(registroSeleccionado.stock) - Number(cantidad)
        const datos = {
            idStock: registroSeleccionado.idStock,
            stock: nuevoStock,
            idUsuario: idUser,
            accion: "Retiro",
            clave: "",
            articulo: "",
            ubicacion: ""
        }
        try {
            const response = await fetch("/api/sp/actualizarstock", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(datos)
            });
            if (response.ok) {
                setCantidad(0)
                leerStockCompletoSP()
                leerStockSP()
                setModalEditarStock(false)
            }
        }
        catch (error) {
            console.error('Error en el try de retirarStockSP', error);
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
        leerStockSP();
        leerStockCompletoSP();
        leerBodegasSP();
    }, [])



    return (
        <div className="container mt-4">
            <h1>Bodegas Aquacolors</h1>
            <h3>Bienvenid@: {user}</h3>
            <button className="btn btn-warning m-3" onClick={() => setModalAgregar(true)}>Agregar Articulo Nuevo</button>
            <button
                className="btn btn-secondary m-3"
                onClick={() => {
                    logout();  // Función que limpie la autenticación
                    navigate('/login', { replace: true });
                }}
            >
                Salir
            </button>
            <table className="table">
                <thead>
                    <tr>
                        <th>Clave</th>
                        <th>Artículo</th>
                        <th>Stock</th>
                        <th>Bodega</th>
                        <th>Ubicación</th>
                        <th>Accion</th>
                        <th>Autor</th>
                        <th>Fecha</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        stock.map(item => (
                            <tr key={item.idStock}>
                                <td>{item.clave}</td>
                                <td>{item.articulo}</td>
                                <td>{item.stock}</td>
                                <td>{item.bodega}</td>
                                <td>{item.ubicacion}</td>
                                <td>{item.accion}</td>
                                <td>{item.usuario}</td>
                                <td>{item.fecha}</td>
                            </tr>
                        ))
                    }
                </tbody>
            </table>

            <input
                className="form-control mt-5"
                type="text"
                placeholder="🔎  Buscar ..."
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
                        <th>Accion</th>
                        <th>Autor</th>
                        <th>Ultima Accion</th>
                        <th>Editar Stock</th>
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
                                <td>{item.accion}</td>
                                <td>{item.usuario}</td>
                                <td>{item.fecha}</td>
                                <td>
                                    <button className="btn me-3" onClick={() => seleccionarRegistro(item, "EditarStock")}>✏</button>
                                </td>
                            </tr>
                        ))
                    }
                </tbody>
            </table>

            <Modal isOpen={modalAgregar}>
                <ModalHeader>
                    <div>
                        <h3>Agregar Stock</h3>
                    </div>
                </ModalHeader>
                <ModalBody>
                    <form onSubmit={handleSubmit(onSubmit)}>
                        <div className="row g-2 mb-2">
                            <div className="col-md">
                                <div className="form-floating">
                                    <input
                                        required
                                        type="text"
                                        className="form-control"
                                        id="inputClave"
                                        name="inputClave"
                                        placeholder=""
                                        {...register("clave")}
                                    />
                                    <label htmlFor="inputClave">Clave</label>
                                </div>
                            </div>
                            <div className="col-md">
                                <div className="form-floating">
                                    <input
                                        required
                                        type="text"
                                        className="form-control"
                                        id="inputArticulo"
                                        name="inputArticulo"
                                        placeholder=""
                                        {...register("articulo")}
                                    />
                                    <label htmlFor="inputArticulo">Articulo</label>
                                </div>
                            </div>
                        </div>
                        <div className="row g-2 mb-2">
                            <div className="col-md">
                                <div className="form-floating">
                                    <input
                                        required
                                        type="number"
                                        className="form-control"
                                        id="inputStock"
                                        name="inputStock"
                                        placeholder="name@example.com"
                                        min="0"
                                        {...register("stock")}
                                    />
                                    <label htmlFor="inputStock">Stock</label>
                                </div>
                            </div>
                            <div className="col-md">
                                <div className="form-floating">
                                    <select className="form-select"
                                        required
                                        id="inputBodega"
                                        name="inputBodega"
                                        aria-label="Floating label select example"
                                        {...register("idBodega")}>
                                        <option value=""></option>
                                        {
                                            bodegas.map(item => (
                                                <option key={item.idBodega} value={item.idBodega}>{item.bodega}</option>
                                            ))
                                        }
                                    </select>
                                    <label htmlFor="inputBodega">Bodega:</label>
                                </div>
                            </div>
                        </div>
                        <div className="row g-2 mb-2">
                            <div className="col-md">
                                <div className="form-floating">
                                    <input
                                        required
                                        type="text"
                                        className="form-control"
                                        id="inputUbicacion"
                                        name="inputUbicacion"
                                        placeholder=""
                                        {...register("ubicacion")}
                                    />
                                    <label htmlFor="inputUbicacion">Ubicacion</label>
                                </div>
                            </div>
                            <div className="col-md">

                            </div>
                        </div>
                        <div className="modal-footer">
                            <button type="submit" className="btn btn-primary">Ingresar Articulo</button>
                            <button type="button" className="btn btn-danger" onClick={() => setModalAgregar(false)}>Cerrar</button>
                        </div>
                    </form>
                </ModalBody>
            </Modal>

            
            <Modal isOpen={modalEditarStock}>
                <ModalHeader>
                    <div>
                        <h3>{registroSeleccionado.stock} piezas en existencia</h3>
                    </div>
                </ModalHeader>
                <ModalBody>
                    <div>
                        <p>{registroSeleccionado.articulo}</p>
                    </div>
                </ModalBody>
                <ModalFooter>
                    <input
                        className="form-control"
                        type="number"
                        min="0"
                        placeholder="Ingresa cantidad"
                        onChange={(e) => setCantidad(e.target.value)}
                    />

                    <div className="btn-group w-100" role="group">
                        <Button className="me-2" color="success" onClick={agregarStockSP} >Agregar  ➕</Button>
                        <Button className="mx-1" color="primary" onClick={retirarStockSP} >Retirar  ➖</Button>
                        <Button className="ms-2" color="danger" onClick={() => setModalEditarStock(false)} >Cancelar</Button>
                    </div>


                </ModalFooter>
            </Modal>
            

        </div>
    );
}

export default Encargado;