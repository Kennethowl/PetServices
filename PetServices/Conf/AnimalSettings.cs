using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ConsoleTables;

public class AnimalSettings
{
    string path = Routes.routeAnimal();
    public List<Animal> ListadoDeAnimales { get; set; }

    public AnimalSettings()
    {
        ListadoDeAnimales = new List<Animal>();
    }

    private (int, string) digitarAnimales()
    {
        Console.Clear();
        Console.WriteLine("Registrar animales");
        Console.WriteLine("==================");
        Console.WriteLine("");
        Console.Write("Código de animal : ");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Tipo de animal   : ");
        string animal = Console.ReadLine();

        return (id, animal);
    }

    public List<Animal> ObtenerAnimales()
    {
        var animales = File.ReadAllText(path);
        
        ListadoDeAnimales = JsonConvert.DeserializeObject<List<Animal>>(animales);

        return ListadoDeAnimales;
    }

    private void GuardarAnimal()
    {
        Console.Clear();
        Console.WriteLine("Crear o actualizar animales");
        Console.WriteLine("");
        Console.Write("Id de animal o tipo de animal: ");
        string fetchanimal = Console.ReadLine();

        var animal = ListadoDeAnimales.Find(a => a.ID.ToString() == fetchanimal || a.Descripcion == fetchanimal);

        if (animal != null)
        {
            Console.Clear();
            Console.WriteLine($"Tipo de animal: {animal.Descripcion}");
            Console.ReadLine();

            Console.Write("\nTipo de animal: ");
            string nuevoTipoAnimal = Console.ReadLine();

            EliminarAnimal(animal);

            Animal nuevoAnimal = new Animal(animal.ID, nuevoTipoAnimal);
            ListadoDeAnimales.Add(nuevoAnimal);

            var animalesAgregados = JsonConvert.SerializeObject(ListadoDeAnimales);

            File.WriteAllText(path, animalesAgregados);

            Console.WriteLine("\nRegistro actualizado satisfactoriamente");
            Console.Read();
        }
        else
        {
            var (id, nombre) = digitarAnimales();

            Animal animalNuevo = new Animal(id, nombre);
            ListadoDeAnimales.Add(animalNuevo);

            var animalesAgregados = JsonConvert.SerializeObject(ListadoDeAnimales);

            File.WriteAllText(path, animalesAgregados);

            Console.WriteLine("\nRegistro agregado satisfactoriamente");
            Console.ReadLine();
        }
    }

    private void EliminarAnimal(Animal animal)
    {
        ListadoDeAnimales.Remove(animal);

        var animalesEliminados = JsonConvert.SerializeObject(ListadoDeAnimales);

        File.WriteAllText(path, animalesEliminados);
    }

    private void cargarAnimales()
    {
        var animales = ObtenerAnimales();

        var table = new ConsoleTable("Código", "Tipo de Animal");
    
        foreach (var animal in animales)
        {
            Console.Clear();
            table.AddRow(animal.ID, animal.Descripcion);
            table.Write();
        }
        Console.Read();
    }
    
    private void buscarRegistro()
    {
        Console.Clear();
        Console.Write("Id de animal o tipo de animal: ");
        string fetchanimal = Console.ReadLine();

        var animal = ListadoDeAnimales.Find(a => a.ID.ToString() == fetchanimal || a.Descripcion == fetchanimal);

        if (animal != null)
        {
            Console.WriteLine($"\nTipo de animal: {animal.Descripcion}");
            Console.ReadLine();

            Console.Write("\nDesea borrar el registro (s | n): ");
            string confirmacion = Console.ReadLine();

            if (confirmacion.ToLower() == "s")
            {
                EliminarAnimal(animal);

                Console.WriteLine("\nRegistro eliminado satisfactoriamente");
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("\nCódigo de animal no válido o no encontrado");
            Console.Read();
        }
    }

    public void menuPrincipal()
    {
        string opcion = "";

        menu: 
            Console.Clear();
            Console.WriteLine("Menu de Animales");
            Console.WriteLine("");
            Console.WriteLine("1 - Listar Animales");
            Console.WriteLine("2 - Crear / Actualizar Animales");
            Console.WriteLine("3 - Búsqueda de Animales");
            Console.WriteLine("0 - Salir del sistema");
            Console.WriteLine("");
            Console.Write("Seleccion un opcion: ");
            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    cargarAnimales();
                break;
                case "2":
                    GuardarAnimal();
                break;
                case "3":
                    buscarRegistro();
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