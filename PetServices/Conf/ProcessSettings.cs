using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ConsoleTables;
using System.Linq;

public class ProccessSettings
{
    string path = Routes.routeFactura();
    public List<Factura> ListaDeFacturas { get; set; }
    MascotaSettings mascotaSettings;
    ClienteSettings clienteSettings;
    ServicioSettings servicioSettings;
    UsuarioSettings usuarioSettings;

    public ProccessSettings()
    {
        ListaDeFacturas = new List<Factura>();
        mascotaSettings = new MascotaSettings();
        clienteSettings = new ClienteSettings();
        servicioSettings = new ServicioSettings();
        usuarioSettings = new UsuarioSettings();
    }

    /*public List<Factura> ObtenerFacturas()
    {
        var facturas = File.ReadAllText(path);

        ListaDeFacturas = JsonConvert.DeserializeObject<List<Factura>>(facturas);

        return ListaDeFacturas;
    }*/

    public void realizarServicio(Usuario usuario)
    {
        var clientes = clienteSettings.ObtenerClientes();
        var mascotas = mascotaSettings.ObtenerMascotas();
        var servicios = servicioSettings.ObtenerServicio();

        Console.Clear();
        Console.WriteLine($"Realizar una compra, usuario: {usuario.User.ToUpper()}");
        Console.WriteLine("");
        Console.Write("Código del cliente: ");
        string idCliente = Console.ReadLine();

        var cliente = clientes.Find(c => c.ID.ToString() == idCliente);

        if (cliente != null)
        {
            Console.WriteLine($"\nNombre del cliente: {cliente.Nombre + " " + cliente.Apellido}");
            Console.WriteLine($"Nombre de la mascota: {cliente.Mascota.NombreMascota}");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("\nCódigo no encontrado o no válido");
            Console.ReadLine();
        }

        int nuevoCodigo = ListaDeFacturas.Count + 1;

        Factura factura = new Factura(nuevoCodigo, "FAC-000" + nuevoCodigo, cliente, usuario);
        ListaDeFacturas.Add(factura);

       /*" var addFactura = JsonConvert.SerializeObject(ListaDeFacturas);

        File.WriteAllText(path, addFactura);"*/

        Compra:
            Console.Write("\nCódigo del servicio: ");
            string idServicio = Console.ReadLine();

            var servicio = servicios.Find(s => s.ID.ToString() == idServicio);

            if (cliente != null)
            {
                Console.WriteLine($"\nNombre del Servicio: {servicio.NombreServicio}");
                Console.WriteLine($"Precio del Servicio: {servicio.Precio}");
                Console.ReadLine();
                factura.addDetalle(servicio);
            }
            else
            {
                Console.WriteLine("\nCódigo no encontrado o no válido");
                Console.ReadLine();
            }

        Console.Write("\nDesea continuar (s | n): ");
        string confirmacion = Console.ReadLine();

        if (confirmacion.ToLower() == "s")
        {
            goto Compra;   
        }

        Console.WriteLine($"\nSubtotal: {factura.Subtotal}");
        Console.WriteLine($"Impuesto 15%: {factura.TotalImpuesto}");
        Console.WriteLine($"Total: {factura.Total}");
        Console.WriteLine($"Fecha: {factura.Fecha}");
        Console.WriteLine($"Vendedor: {factura.Usuario.User}");
        Console.WriteLine($"Cliente: {factura.Cliente.Nombre}");
        Console.ReadLine();

        Console.Write("\nCantidad entrante: ");
        factura.Recibo = Convert.ToDouble(Console.ReadLine());

        factura.Vuelto = factura.Recibo - factura.Total;

        Console.WriteLine("\nVuelto: " + factura.Vuelto);
        Console.ReadLine();

        Console.Write("\nImprimir factura (s | n): ");
        string confirm = Console.ReadLine();

        if (confirm.ToLower() == "s")
            cargarFacturaEnBaseDeDatos(factura);
            Console.WriteLine("Factura impresa");
            Console.ReadLine();
    }

    private void cargarFactura()
    {
        var table = new ConsoleTable("ID", "ID Factura", "Fecha", "Cliente", "Usuario", "Subtotal", "Impuesto", "Total", "Recibe", "Vuelto");
        //var facturas = ObtenerFacturas();
        var facturas = ListaDeFacturas;
        foreach (var factura in facturas)
        {
            Console.Clear();
            table.AddRow(factura.ID, factura.IDFactura, factura.Fecha, factura.Cliente.Nombre + " " + factura.Cliente.Apellido, factura.Usuario.User, factura.Subtotal, factura.TotalImpuesto, factura.Total,factura.Recibo, factura.Vuelto);
            table.Write();
            var detalles = factura.ObtenerDetalleFactura();
        
            var table1 = new ConsoleTable("ID", "Detalle", "Precio", "Total");


            foreach (var detalle in detalles.OrderBy(c => c.ID))
            {
                var impuesto = detalle.Precio * .15;
                factura.Total = detalle.Precio + impuesto;

                table1.AddRow(detalle.ID, detalle.Servicio.NombreServicio, detalle.Precio, factura.Total);
                table1.Write();
            }
            Console.WriteLine("");
            Console.ReadLine();
        } 
    }

    private void cargarFacturaEnBaseDeDatos(Factura fileNameFactura)
    {
        var fileName = fileNameFactura.IDFactura;
        
        StreamWriter streamWriter = new StreamWriter(Routes.routeFactura() + fileName + ".txt");

        Console.Clear();
        streamWriter.WriteLine("ID" + " " + "ID Factura" + " " + "Fecha" + " " + "Cliente" + " " + "Usuario" + " " + "Subtotal" + " " + "Impuesto" + " " + "Total" + " " + "Recibe" + " " + "Vuelto");
        
        var facturas = ListaDeFacturas;

        foreach (var factura in facturas)
        {
            streamWriter.WriteLine(factura.ID.ToString() + " " + factura.IDFactura + " " + factura.Fecha + " " + factura.Cliente.Nombre + " " + factura.Cliente.Apellido + " " + factura.Usuario.User + " " + factura.Subtotal + " " + factura.TotalImpuesto + " " + factura.Total + " " + factura.Recibo + " " + factura.Vuelto);
        
            streamWriter.WriteLine("ID" + " " + "Detalle" + " " + "Precio" + " " + "Total");

            var detalles = factura.ObtenerDetalleFactura();

            foreach (var detalle in detalles.OrderBy(c => c.ID))
            {
                var impuesto = detalle.Precio * .15;
                factura.Total = detalle.Precio + impuesto;

                streamWriter.WriteLine(detalle.ID.ToString() + " " + detalle.Servicio.NombreServicio + " " + detalle.Precio + " " + factura.Total);
            }
        } 
        streamWriter.Close();
    }

    private void cargarVentas()
    {
        //"var facturas = ObtenerFacturas();"
        var facturas = ListaDeFacturas;
        foreach (var factura in facturas)
        {
            var detalles = factura.ObtenerDetalleFactura();
        
            var table1 = new ConsoleTable("ID", "Detalle", "Precio", "Total");


            foreach (var detalle in detalles.OrderBy(c => c.ID))
            {
                var impuesto = detalle.Precio * .15;
                factura.Total = detalle.Precio + impuesto;

                Console.Clear();
                table1.AddRow(detalle.ID, detalle.Servicio.NombreServicio, detalle.Precio, factura.Total);
                table1.Write();
            }
            Console.ReadLine();
        } 
    }

    public void menuPrincipal()
    {
        string opcion = "";

        menu: 
            Console.Clear();
            Console.WriteLine("Menu de Detalle");
            Console.WriteLine("");
            Console.WriteLine("1 - Listar Detalle de Servicios");
            Console.WriteLine("2 - Cargar Facturas");
            Console.WriteLine("0 - Salir del sistema");
            Console.WriteLine("");
            Console.Write("Seleccion un opcion: ");
            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    cargarVentas();
                break;
                case "2":
                    cargarFactura();
                break;
                default:
                break;
            }
        if (opcion != "0")
        {
            goto menu;
        }
    }
}