using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class Factura
{
    string path = Routes.routeDetalleFactura();
    public int ID { get; set; }
    public string IDFactura { get; set; }
    public DateTime Fecha { get; set; }
    public Cliente Cliente { get; set; }
    public Usuario Usuario { get; set; }
    public double Subtotal { get; set; }
    public double Impuesto { get; set; }
    public double TotalImpuesto { get; set; }
    public double Total { get; set; }
    public double Recibo { get; set; } 
    public double Vuelto { get; set; }
    public List<DetalleFactura> DetalleFactura { get; set; }

    public Factura(int id, string idFactura, Cliente cliente, Usuario usuario)
    {
        ID = id;
        IDFactura = idFactura;
        Cliente = cliente;
        Usuario = usuario;
        Fecha = DateTime.Now;
        Impuesto = .15;
        DetalleFactura = new List<DetalleFactura>();
    }

    public List<DetalleFactura> ObtenerDetalleFactura()
    {
        var detalle = File.ReadAllText(path);

        DetalleFactura = JsonConvert.DeserializeObject<List<DetalleFactura>>(detalle);

        return DetalleFactura;
    }

    public void addDetalle(Servicio servicio)
    {
        int nuevoCodigo = DetalleFactura.Count + 1;

        DetalleFactura detalle = new DetalleFactura(nuevoCodigo, servicio);
        DetalleFactura.Add(detalle);

        var addDetalle = JsonConvert.SerializeObject(DetalleFactura);

        Subtotal += servicio.Precio;
        TotalImpuesto = Subtotal * Impuesto;
        Total = Subtotal + Total;

        File.WriteAllText(path, addDetalle);   
    }
}