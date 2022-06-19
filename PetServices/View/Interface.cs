using System;

public class MenuPrincipal
{
    AnimalSettings animalSettings;
    MascotaSettings mascotaSettings;
    ClienteSettings clienteSettings;
    ServicioSettings servicioSettings;
    UsuarioSettings usuarioSettings;
    ProccessSettings proccessSettings;

    public MenuPrincipal()
    {
        animalSettings = new AnimalSettings();
        mascotaSettings = new MascotaSettings();
        clienteSettings = new ClienteSettings();
        servicioSettings = new ServicioSettings();
        usuarioSettings = new UsuarioSettings();
        proccessSettings = new ProccessSettings();
    }

    public void menuPrincipal(Usuario usuario)
    {
        string opcion = "";

        Console.Title = "Pet Services" + " " + usuario.User;
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Pet Services");
            Console.WriteLine("============");
            Console.WriteLine("");
            Console.WriteLine("1 - Animales");
            Console.WriteLine("2 - Mascotas");
            Console.WriteLine("3 - Clientes");
            Console.WriteLine("4 - Servicios");
            Console.WriteLine("5 - Usuarios");
            Console.WriteLine("6 - Realizar Servicios");
            Console.WriteLine("7 - Reporte de Servicios");
            Console.WriteLine("0 - Salir del sistema");
            Console.WriteLine("");
            Console.Write("Seleccione una opción: ");
            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                animalSettings.menuPrincipal();
                break;
                case "2":
                mascotaSettings.menuPrincipal();
                break;
                case "3":
                clienteSettings.menuPrincipal();
                break;
                case "4":
                servicioSettings.menuPrincipal();
                break;
                case "5":
                usuarioSettings.menuPrincipal();
                break;
                case "6":
                proccessSettings.realizarServicio(usuario);
                break;
                case "7":
                proccessSettings.menuPrincipal();
                break;
                default:
                break;
            }

            if (opcion == "0")
            {
                break;
            }
        }
    }
}