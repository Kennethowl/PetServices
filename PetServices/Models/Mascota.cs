public class Mascota
{
    public int ID { get; set; }
    public string NombreMascota { get; set; }
    public string Raza { get; set; }
    public Animal Animal { get; set; }

    public Mascota(int id, string nombreMascota, string raza, Animal animal)
    {
        ID = id;
        NombreMascota = nombreMascota;
        Raza = raza;
        Animal = animal;
    }
}