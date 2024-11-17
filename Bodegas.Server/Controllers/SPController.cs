using Bodegas.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bodegas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SPController(DbAquacolorsContext context) : ControllerBase
    {
        private readonly DbAquacolorsContext _context = context;

        [HttpGet("MostrarStock")]
        public IActionResult MostrarStock()
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand command = conexion.CreateCommand();
                conexion.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_MostrarStock";
                SqlDataReader reader = command.ExecuteReader();
                List<ModeloMostrarStock> mostrarStocks = [];
                while (reader.Read())
                {
                    ModeloMostrarStock mostrarStock = new()
                    {
                        IdStock = (int)reader["idStock"],
                        Clave = (string)reader["Clave"],
                        Articulo = (string)reader["Articulo"],
                        Stock = (int)reader["Stock"],
                        Bodega = (string)reader["Bodega"],
                        Ubicacion = (string)reader["Ubicacion"],
                        Accion = (string)reader["Accion"],
                        Usuario = (string)reader["Usuario"],
                        Fecha = (DateTime)reader["Fecha"]
                    };
                    mostrarStocks.Add(mostrarStock);
                }
                conexion.Close();
                return Ok(mostrarStocks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("MostrarStockCompleto")]
        public IActionResult MostrarStockCompleto()
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comand = conexion.CreateCommand();
                conexion.Open();
                comand.CommandType = System.Data.CommandType.StoredProcedure;
                comand.CommandText = "usp_MostrarStockCompleto";
                SqlDataReader reader = comand.ExecuteReader();
                List<ModeloMostrarStock> mostrarStockCompletos = [];
                while (reader.Read())
                {
                    ModeloMostrarStock mostrarStockCompleto = new()
                    {
                        IdStock = (int)reader["idStock"],
                        Clave = (string)reader["Clave"],
                        Articulo = (string)reader["Articulo"],
                        Stock = (int)reader["Stock"],
                        Bodega = (string)reader["Bodega"],
                        Ubicacion = (string)reader["Ubicacion"],
                        Accion = (string)reader["Accion"],
                        Usuario = (string)reader["Usuario"],
                        Fecha = (DateTime)reader["Fecha"]
                    };
                    mostrarStockCompletos.Add(mostrarStockCompleto);
                }
                conexion.Close();
                return Ok(mostrarStockCompletos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("MostrarBodegas")]
        public IActionResult MostrarBodegas()
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand command = conexion.CreateCommand();
                conexion.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "usp_MostrarBodegas";
                SqlDataReader reader = command.ExecuteReader();
                List<BodegasA> mostrarBodegas = [];
                while (reader.Read())
                {
                    BodegasA mostrarBodega = new()
                    {
                        IdBodega = (int)reader["idBodega"],
                        Bodega = (string)reader["Bodega"]
                    };
                    mostrarBodegas.Add(mostrarBodega);
                }
                conexion.Close();
                return Ok(mostrarBodegas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("IngresarArticulo")]
        public IActionResult IngresarArticulo(StockAqua gestor)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conexion.CreateCommand();
                conexion.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_IngresarArticulo";
                cmd.Parameters.Add("@Clave", System.Data.SqlDbType.VarChar).Value = gestor.Clave;
                cmd.Parameters.Add("@Articulo", System.Data.SqlDbType.VarChar).Value = gestor.Articulo;
                cmd.Parameters.Add("@Stock", System.Data.SqlDbType.Int).Value = gestor.Stock;
                cmd.Parameters.Add("@IdBodega", System.Data.SqlDbType.Int).Value = gestor.IdBodega;
                cmd.Parameters.Add("@Ubicacion", System.Data.SqlDbType.VarChar).Value = gestor.Ubicacion;
                cmd.Parameters.Add("@Accion", System.Data.SqlDbType.VarChar).Value = gestor.Accion;
                cmd.Parameters.Add("@IdUsuario", System.Data.SqlDbType.Int).Value = gestor.IdUsuario;
                cmd.ExecuteNonQuery();
                conexion.Close();
                return Ok();
            }
            catch (SqlException ex)
            {
                return StatusCode(500, $"Error en la bade de datos: {ex.Message}");
            }
            catch (Exception exception)
            {
                return StatusCode(500, $"Error en el servidor: {exception.Message}");
            }
        }

        [HttpPost("EliminarArticulo")]
        public IActionResult EliminarArticulo(StockAqua gestor)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conexion.CreateCommand();
                conexion.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_EliminarArticulo";
                cmd.Parameters.Add("@idStock", System.Data.SqlDbType.Int).Value = gestor.IdStock;
                cmd.ExecuteNonQuery();
                conexion.Close();
                return Ok();
            }
            catch (SqlException ex)
            {
                return StatusCode(500, $"Error en la base de datos: {ex.Message}");
            }
            catch (Exception exception)
            {
                return StatusCode(500, $"Error del servidor: {exception.Message}");
            }
        }

        [HttpPost("ActualizarStock")]
        public IActionResult ActualizarStock(StockAqua gestor)
        {
            try
            {
                using (var conexion = (SqlConnection)_context.Database.GetDbConnection())
                {
                    using (var comand = conexion.CreateCommand())
                    {
                        conexion.Open();
                        comand.CommandType = System.Data.CommandType.StoredProcedure;
                        comand.CommandText = "usp_ActualizarStock";
                        comand.Parameters.Add("@idStock", System.Data.SqlDbType.Int).Value = gestor.IdStock;
                        comand.Parameters.Add("@Stock", System.Data.SqlDbType.Int).Value = gestor.Stock;
                        comand.Parameters.Add("@Accion", System.Data.SqlDbType.VarChar).Value = gestor.Accion;
                        comand.Parameters.Add("@idUsuario", System.Data.SqlDbType.Int).Value = gestor.IdUsuario;
                        comand.ExecuteNonQuery();
                    }
                }
                return Ok();
            }
            catch (SqlException exception)
            {
                return StatusCode(500, $"Error en la DB: {exception.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        [HttpPost("AgregarUsuario")]
        public IActionResult AgregarUsuario(UsuariosA gestor)
        {
            try
            {
                using (var conexion = (SqlConnection)_context.Database.GetDbConnection())
                {
                    using (var comand = conexion.CreateCommand())
                    {
                        conexion.Open();
                        comand.CommandType = System.Data.CommandType.StoredProcedure;
                        comand.CommandText = "usp_AgregarUsuario";
                        comand.Parameters.Add("@Usuario", System.Data.SqlDbType.VarChar).Value = gestor.Usuario;
                        comand.Parameters.Add("@Correo", System.Data.SqlDbType.VarChar).Value = gestor.Correo;
                        comand.Parameters.Add("@Contrasena", System.Data.SqlDbType.VarChar).Value = gestor.Contrasena;
                        comand.Parameters.Add("@Rol", System.Data.SqlDbType.VarChar).Value = gestor.Rol;
                        comand.ExecuteNonQuery();
                    }
                }
                return Ok();
            }
            catch (SqlException ex)
            {
                return StatusCode(500, $"Error en la DB: {ex.Message}");
            }
            catch (Exception exception)
            {
                return StatusCode(500, $"Error en el servidor: {exception.Message}");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(UsuariosA gestor)
        {
            try
            {
                using (var conexion = (SqlConnection)_context.Database.GetDbConnection())
                {
                    using (var comand = conexion.CreateCommand())
                    {
                        conexion.Open();
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "usp_Login";
                        comand.Parameters.Add("@Correo", System.Data.SqlDbType.VarChar).Value = gestor.Correo;
                        comand.Parameters.Add("@Contrasena", System.Data.SqlDbType.VarChar).Value = gestor.Contrasena;

                        // Agregar parámetros de salida
                        var outputParameter = comand.Parameters.Add("@IsValid", SqlDbType.Bit);
                        outputParameter.Direction = ParameterDirection.Output;
                        var roleParameter = comand.Parameters.Add("@Rol", SqlDbType.VarChar, 50);
                        roleParameter.Direction = ParameterDirection.Output;
                        var userParameter = comand.Parameters.Add("@Usuario", SqlDbType.VarChar, 50);
                        userParameter.Direction = ParameterDirection.Output;
                        var idUserParameter = comand.Parameters.Add("@IdUser", SqlDbType.Int);
                        idUserParameter.Direction = ParameterDirection.Output;

                        comand.ExecuteNonQuery();

                        // Obtener los resultados
                        bool isValid = (bool)outputParameter.Value;
                        string? rol = roleParameter.Value.ToString();
                        string? usuario = userParameter.Value.ToString();
                        int? idUser = Convert.ToInt32(idUserParameter.Value);

                        if (isValid)
                        {
                            return Ok(new { message = "Login exitoso", rol, usuario, idUser });
                        }
                        else
                        {
                            return Unauthorized("Credenciales inválidas");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class ModeloMostrarStock
    {
        public int IdStock { get; set; }
        public string? Clave { get; set; }
        public string? Articulo { get; set; }
        public int Stock { get; set; }
        public string? Bodega { get; set; }
        public string? Ubicacion { get; set; }
        public string? Accion { get; set; }
        public string? Usuario { get; set; }
        public DateTime Fecha { get; set; }
    }
}
