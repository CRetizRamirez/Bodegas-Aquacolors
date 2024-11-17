using System;
using System.Collections.Generic;

namespace Bodegas.Server.Models;

public partial class UsuariosA
{
    public int IdUsuario { get; set; }

    public string Usuario { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string Rol { get; set; } = null!;
}
