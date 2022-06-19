using System;

public class Enter
{
    MenuPrincipal main = new MenuPrincipal();
    UsuarioSettings UsuarioSettings = new UsuarioSettings();
 
    public void ingresarAlSistema()
    {
        int intentos = 0;
        
        Console.Title = "Pet Services";

        for (int i = 0; i < 3; i++)
        {
            Console.Clear();
            Console.WriteLine("Write EXIT for logout of the system\n");
            Console.Write("User: ");
            string user = Console.ReadLine();
            /*Console.Write("Password: ");
            string password = Console.ReadLine();*/

            var comprobar = comprobarPassword(user/*, password*/);

            if (user.ToLower() == "exit")
            {
                Console.WriteLine("\nSaliendo del Sistema");
                Console.ReadKey();
                break;
            }
            
            if (comprobar)
            {
                break;
            }

            if (!comprobar)
            {
                Console.WriteLine($"\nIntente nuevamente, intento {i + 1} de 3: ");
                Console.ReadKey();
            }

            intentos++;
        }
    }

    private bool comprobarPassword(string user/*, string password*/)
    {
        var usuarios = UsuarioSettings.ObtenerUsuarios();
        
        var usuario = usuarios.Find(u => u.User == user /*&& u.Password == password*/);

        if (usuario != null)
        {
            main.menuPrincipal(usuario);
            return true;
        }

        return false;
    }
}