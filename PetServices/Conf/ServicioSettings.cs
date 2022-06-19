using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ConsoleTables;

public class ServicioSettings
{
    string path = Routes.routeServicio();
    public List<Servicio> ListadoDeServicios { get; set; }

    public ServicioSettings()
    {
        ListadoDeServicios = new List<Servicio>();
    }

    private (int, string, double) digitarServicios()
    {
        Console.Clear();
        Console.WriteLine("Registrar servicios en el sistema");
        Console.WriteLine("");
        Console.Write("Código del servicio     : ");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Descripción del servicio: ");
        string servicio = Console.ReadLine();
        Console.Write("Precio del servicio     : ");
        double precio = Convert.ToDouble(Console.ReadLine());

        return(id, servicio, precio);
    }

    public List<Servicio> ObtenerServicio()
    {
        var servicios = File.ReadAllText(path);

        ListadoDeServicios = JsonConvert.DeserializeObject<List<Servicio>>(servicios);

        return ListadoDeServicios;
    }

    private void crearServicios()
    {
        var (id, nombreServicio, precio) = digitarServicios();

        var servicio = new Servicio(id, nombreServicio, precio, true);
        ListadoDeServicios.Add(servicio);

        var addServicio = JsonConvert.SerializeObject(ListadoDeServicios);

        File.WriteAllText(path, addServicio);

        Console.WriteLine("\nRegistro agregado satisfactoriamente");
        Console.ReadLine();
    }

    private void cargarServicios()
    {
        var servicios = ObtenerServicio();

        var table = new ConsoleTable("Código", "Servicio", "Precio", "Estado", "Fecha de Creación");

        foreach (var servicio in servicios)
        {
            Console.Clear();
            table.AddRow(servicio.ID, servicio.NombreServicio, servicio.Precio, servicio.Estado, servicio.Fecha);
            table.Write();
        }
        Console.ReadLine();
    }

    private void buscarServicio()
    {
        var table = new ConsoleTable("Código", "Servicio", "Precio", "Estado", "Fecha de Creación");

        Console.Clear();
        Console.WriteLine("Búsqueda de servicios");
        Console.WriteLine("");
        Console.Write("Código de servicio: ");
        string idServicio = Console.ReadLine();

        var servicio = ListadoDeServicios.Find(s => s.ID.ToString() == idServicio);

        if (servicio != null)
        {
            Console.WriteLine("");
            table.AddRow(servicio.ID, servicio.NombreServicio, servicio.Precio, servicio.Estado, servicio.Fecha);
            table.Write();
            Console.ReadLine();

            Console.Write("\nDesea eliminar registro de servicio (s | n): ");
            string confirmacion = Console.ReadLine();

            if (confirmacion.ToLower() == "s")
            {
                EliminarServicio(servicio);
                Console.WriteLine("\nRegistro eliminado satisfactoriamente");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Código no encontrado o no válido");
                Console.ReadLine();
            }
        }

    }

    private void EliminarServicio(Servicio servicio)
    {
        ListadoDeServicios.Remove(servicio);

        var removeService = JsonConvert.SerializeObject(ListadoDeServicios);

        File.WriteAllText(path, removeService);
    }

    public void menuPrincipal()
    {
        string opcion = "";

        menu: 
            Console.Clear();
            Console.WriteLine("Menu de Servicios");
            Console.WriteLine("");
            Console.WriteLine("1 - Listar Servicios");
            Console.WriteLine("2 - Búsqueda de Servicios");
            Console.WriteLine("3 - Crear nuevo Servicio");
            Console.WriteLine("0 - Salir del sistema");
            Console.WriteLine("");
            Console.Write("Seleccion un opcion: ");
            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    cargarServicios();
                break;
                case "2":
                    buscarServicio();
                break;
                case "3":
                    crearServicios();
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