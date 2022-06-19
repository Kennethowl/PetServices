public class Cliente
{
    public int ID { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Correo { get; set; }
    public string Telefono { get; set; }
    public Mascota Mascota { get; set; }

    public Cliente(int id, string nombre, string apellido, string correo, string telefono, Mascota mascota)
    {
        ID = id;
        Nombre = nombre;
        Apellido = apellido;
        Correo = correo;
        Telefono = telefono;
        Mascota = mascota;
    }
}