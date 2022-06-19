using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ConsoleTables;

public class UsuarioSettings
{
    string path = Routes.routeUsuarios();
    public List<Usuario> ListaDeUsuarios { get; set; }

    public UsuarioSettings()
    {
        ListaDeUsuarios = new List<Usuario>();
    }

    public List<Usuario> ObtenerUsuarios()
    {
        var usuarios = File.ReadAllText(path);

        ListaDeUsuarios = JsonConvert.DeserializeObject<List<Usuario>>(usuarios);

        return ListaDeUsuarios;
    }

    private void crearUsuarios()
    {
        Console.Clear();
        Console.WriteLine("Registro de usuarios");
        Console.WriteLine("");
        Console.Write("Código de usuarios: ");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Usuario: ");
        string user = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();

        var passwordEncrypted = SeguridadConf.EncriptarPassword(password);

        Usuario usuario = new Usuario(id, user, passwordEncrypted);
        ListaDeUsuarios.Add(usuario);

        var addUser = JsonConvert.SerializeObject(ListaDeUsuarios);

        File.WriteAllText(path, addUser);

        Console.WriteLine("\nUsuario creado satisfactoriamente");
        Console.ReadLine();
    }

    private void cargarUsuarios()
    {
        var usuarios = ObtenerUsuarios();

        var table = new ConsoleTable("Código", "User", "Password");

        foreach (var item in usuarios)
        {
            Console.Clear();
            table.AddRow(item.ID, item.User, item.Password);
            table.Write();
        }
        Console.ReadLine();
    }

    public void menuPrincipal()
    {
        string opcion = "";

        menu: 
            Console.Clear();
            Console.WriteLine("Menu de Usuarios");
            Console.WriteLine("");
            Console.WriteLine("1 - Listar Usuarios");
            Console.WriteLine("2 - Crear usuarios nuevos");
            Console.WriteLine("0 - Salir del sistema");
            Console.WriteLine("");
            Console.Write("Seleccion un opcion: ");
            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    cargarUsuarios();
                break;
                case "2":
                    crearUsuarios();
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