using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ConsoleTables;

public class ClienteSettings
{
    string path = Routes.routeCliente();
    MascotaSettings mascotaSettings;
    public List<Cliente> ListaDeClientes { get; set; }

    public ClienteSettings()
    {
        ListaDeClientes = new List<Cliente>();
        mascotaSettings = new MascotaSettings();
    }

    private (int, string, string, string, string, Mascota) digitarClientes()
    {
        var table = new ConsoleTable("Código", "Nombre de Mascota", "Raza", "Tipo de Animal");
        var mascotas = mascotaSettings.ObtenerMascotas();

        Console.Clear();
        Console.WriteLine("Crear registro de clientes");
        Console.WriteLine("");
        Console.Write("Código de cliente: ");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Nombre del cliente: ");
        string nombre = Console.ReadLine();
        Console.Write("Apellido del cliente: ");
        string apellido = Console.ReadLine();
        Console.Write("Correo del cliente: ");
        string correo = Console.ReadLine();
        Console.Write("Telefono del cliente: ");
        string telefono = Console.ReadLine();
        Console.Write("Digite codigo de mascota: ");
        string fetchPet = Console.ReadLine();

        var mascota = mascotas.Find(m => m.ID.ToString() == fetchPet);

        if (mascota != null)
        {
            Console.WriteLine("");
            table.AddRow(mascota.ID, mascota.NombreMascota, mascota.Raza, mascota.Animal.Descripcion);
            table.Write();
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("\nCódigo no encontrado o no válido");
            Console.ReadLine();
        }

        return (id, nombre, apellido, correo, telefono, mascota);
    }

    public List<Cliente> ObtenerClientes()
    {
        var clientes = File.ReadAllText(path);

        ListaDeClientes = JsonConvert.DeserializeObject<List<Cliente>>(clientes);
        
        return ListaDeClientes;
    }
    
    private void crearClientes()
    {
        var (id, nombre, apellido, correo, telefono, mascota) = digitarClientes();

        Cliente cliente = new Cliente(id, nombre, apellido, correo, telefono, mascota);
        ListaDeClientes.Add(cliente);

        var addCliente = JsonConvert.SerializeObject(ListaDeClientes);

        File.WriteAllText(path, addCliente);

        Console.WriteLine("\nRegistro agregado satisfactoriamente");
        Console.ReadLine();
    }

    private void buscarClientes()
    {
        Console.Clear();
        Console.WriteLine("Búsqueda de clientes");
        Console.WriteLine("");
        Console.Write("Código de cliente: ");
        string fetchClient = Console.ReadLine();

        var cliente = ListaDeClientes.Find(c => c.ID.ToString() == fetchClient);

        if (cliente != null)
        {
            Console.WriteLine($"Nombre del cliente: {cliente.Nombre + " " + cliente.Apellido}");
            Console.WriteLine($"Telefono: {cliente.Telefono}");
            Console.WriteLine($"Nombre de mascota: {cliente.Mascota.NombreMascota}");
            Console.ReadLine();

            Console.Write("\nDesea eliminar registro de cliente (s | n): ");
            string confirmacion = Console.ReadLine();

            if (confirmacion.ToLower() == "s")
            {
                EliminarClientes(cliente);
                Console.WriteLine("\nRegistro eliminado satisfactoriamente");
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("Código no encontrado o no válido");
            Console.ReadLine();
        }
    }

    private void EliminarClientes(Cliente cliente)
    {
        ListaDeClientes.Remove(cliente);

        var clientes = JsonConvert.SerializeObject(ListaDeClientes);

        File.WriteAllText(path, clientes);
    }

    private void cargarClientes()
    {
        var clientes = ObtenerClientes();
        
        var table = new ConsoleTable("Código", "Nombre", "Apellido", "Correo", "Telefono", "Nombre de Mascota");

        foreach (var cliente in clientes)
        {
            Console.Clear();
            table.AddRow(cliente.ID, cliente.Nombre, cliente.Apellido, cliente.Correo, cliente.Telefono, cliente.Mascota.NombreMascota);
            table.Write();
        }
        Console.ReadLine();
    }

    public void menuPrincipal()
    {
        string opcion = "";

        menu: 
            Console.Clear();
            Console.WriteLine("Menu de Clientes");
            Console.WriteLine("");
            Console.WriteLine("1 - Listar Clientes");
            Console.WriteLine("2 - Búsqueda de Clientes");
            Console.WriteLine("3 - Crear nuevo cliente");
            Console.WriteLine("0 - Salir del sistema");
            Console.WriteLine("");
            Console.Write("Seleccion un opcion: ");
            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    cargarClientes();
                break;
                case "2":
                    buscarClientes();
                break;
                case "3":
                    crearClientes();
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