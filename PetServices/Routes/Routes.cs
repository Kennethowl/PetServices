using System;

public class Routes
{
    static public string routeAnimal()
    {
        var PathAnimales = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\PetServices\DatabasePetServices\animales.json";
        
        return PathAnimales;
    }

    static public string routeMascota()
    {
        var PathMascota = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\PetServices\DatabasePetServices\mascotas.json";
        
        return PathMascota;
    }

    static public string routeCliente()
    {
        var PathClientes = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\PetServices\DatabasePetServices\clientes.json";
        
        return PathClientes;
    }

    static public string routeServicio()
    {
        var PathServices = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\PetServices\DatabasePetServices\servicios.json";
        
        return PathServices;
    }

    static public string routeDetalleFactura()
    {
        var pathDetail = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\PetServices\DatabasePetServices\detalles.json";
        
        return pathDetail;
    }

    static public string routeFactura()
    {
        var pathFactura = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\PetServices\DatabasePetServices\Facturas\";
        
        return pathFactura;
    }

    static public string routeUsuarios()
    {
        var pathUsuario = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\PetServices\DatabasePetServices\usuarios.json";
        
        return pathUsuario;
    }
}