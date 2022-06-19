public class DetalleFactura
{
    public int ID { get; set; }
    public Servicio Servicio { get; set; }
    public double Precio { get; set; }

    public DetalleFactura(int id, Servicio servicio)
    {
        ID = id;
        Servicio = servicio;
        Precio = servicio.Precio;
    }
}