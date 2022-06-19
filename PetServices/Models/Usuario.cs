public class Usuario
{
    public int ID { get; set; }
    public string User { get; set; }
    public string Password { get; set; }

    public Usuario(int id, string user, string password)
    {
        ID = id;
        User = user;
        Password = password;
    }
}