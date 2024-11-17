using System;
using System.Collections.Generic;

namespace Bodegas.Server.Models;

public partial class StockAqua
{
    public int IdStock { get; set; }

    public string Clave { get; set; } = null!;

    public string Articulo { get; set; } = null!;

    public int Stock { get; set; }

    public int IdBodega { get; set; }

    public string Ubicacion { get; set; } = null!;

    public string Accion { get; set; } = null!;

    public int IdUsuario { get; set; }

    public DateTime? Fecha { get; set; }
}
