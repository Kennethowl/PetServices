public class Animal
{
    public int ID { get; set; }
    public string Descripcion { get; set; }

    public Animal(int id, string descripcion)
    {
        ID = id;
        Descripcion = descripcion;
    }
}