using System;
using System.Collections.Generic;

namespace AplicacionWeb.Models;

public partial class LugarEvento
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public int Capacidad { get; set; }

    public bool Deshabilitado { get; set; }
}
