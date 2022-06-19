using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ConsoleTables;

public class MascotaSettings
{
    string path = Routes.routeMascota();
    AnimalSettings animalSettings;
    public List<Mascota> ListadoDeMascotas { get; set; }

    public MascotaSettings()
    {
        ListadoDeMascotas = new List<Mascota>();
        animalSettings = new AnimalSettings();
    }

    private (int, string, string, Animal) digitarAnimales()
    {
        var animales = animalSettings.ObtenerAnimales();

        Console.Clear();
        Console.WriteLine("Registro de mascotas");
        Console.WriteLine("====================");
        Console.WriteLine("");
        Console.Write("Código de mascota: ");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Nombre de mascota: ");
        string nombre = Console.ReadLine();
        Console.Write("Raza de mascota  : ");
        string raza = Console.ReadLine();
        Console.Write("Código de animal : ");
        string idAnimal = Console.ReadLine();

        var animal = animales.Find(a => a.ID.ToString() == idAnimal);

        if (animal == null)
        {
            Console.WriteLine("\nCódigo no encontrado o no válido");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine($"\nTipo de animal: {animal.Descripcion}");
            Console.ReadLine();
        }

        return (id, nombre, raza, animal);
    }

    public List<Mascota> ObtenerMascotas()
    {
        var mascotas = File.ReadAllText(path);

        ListadoDeMascotas = JsonConvert.DeserializeObject<List<Mascota>>(mascotas);

        return ListadoDeMascotas;
    }

    private void BuscarMascotas()
    {
        Console.Clear();
        Console.WriteLine("Búsqueda de mascotas");
        Console.WriteLine("");
        Console.Write("Código de mascota: ");
        string fetchPet = Console.ReadLine();

        var mascota = ListadoDeMascotas.Find(m => m.ID.ToString() == fetchPet);

        if (mascota != null)
        {
            Console.WriteLine($"Mascota: {mascota.NombreMascota}");
            Console.WriteLine($"Raza: {mascota.Raza}");
            Console.WriteLine($"Tipo de animal: {mascota.Animal.Descripcion}");
            Console.ReadLine();

            Console.WriteLine("\nDesea eliminar registro de mascota (s | n): ");
            string confirmacion = Console.ReadLine();

            if (confirmacion.ToLower() == "s")
            {
                EliminarMascotas(mascota);
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

    private void crearMascotas()
    {
        var (id, nombre, raza, animal) = digitarAnimales();

        Mascota nuevaMascota = new Mascota(id, nombre, raza, animal);
        ListadoDeMascotas.Add(nuevaMascota);

        var addPets = JsonConvert.SerializeObject(ListadoDeMascotas);

        File.WriteAllText(path, addPets);

        Console.WriteLine("\nRegistro agregado satisfactoriamente");
        Console.ReadLine();
    }

    private void EliminarMascotas(Mascota mascota)
    {
        ListadoDeMascotas.Remove(mascota);

        var removePets = JsonConvert.SerializeObject(ListadoDeMascotas);

        File.WriteAllText(path, removePets);
    }

    private void cargarMascotas()
    {
        var mascotas = ObtenerMascotas();

        var table = new ConsoleTable("Código", "Nombre de la Mascota", "Raza de la Mascota", "Tipo de Animal");

        foreach (var mascota in mascotas)
        {
            Console.Clear();
            table.AddRow(mascota.ID, mascota.NombreMascota, mascota.Raza, mascota.Animal.Descripcion);
            table.Write();
        }
        Console.ReadLine();
    }

    public void menuPrincipal()
    {
        string opcion = "";

        menu: 
            Console.Clear();
            Console.WriteLine("Menu de Mascotas");
            Console.WriteLine("");
            Console.WriteLine("1 - Listar Mascotas");
            Console.WriteLine("2 - Búsqueda de Mascotas");
            Console.WriteLine("3 - Crear Registros de Mascotas");
            Console.WriteLine("0 - Salir del sistema");
            Console.WriteLine("");
            Console.Write("Seleccion un opcion: ");
            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    cargarMascotas();
                break;
                case "2":
                    BuscarMascotas();
                break;
                case "3":
                    crearMascotas();
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