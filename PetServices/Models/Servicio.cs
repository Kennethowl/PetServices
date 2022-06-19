using System;

public class Servicio
{
    public int ID { get; set; }
    public string NombreServicio { get; set; }
    public double Precio { get; set; }
    public bool Estado { get; set; }
    public DateTime Fecha { get; set; }

    public Servicio(int id, string nombreServicio, double precio, bool estado)
    {
        ID = id;
        NombreServicio = nombreServicio;
        Precio = precio;
        Estado = estado;
        Fecha = DateTime.Now;
    }
}