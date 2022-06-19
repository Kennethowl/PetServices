using System;
using System.Text;
using System.Security.Cryptography;

public static class SeguridadConf
{
    public static string EncriptarPassword(string password)
    {
        var encriptado = String.Empty;

        byte[] encryted = System.Text.Encoding.Unicode.GetBytes(password);
        encriptado = Convert.ToBase64String(encryted);

        return encriptado;
    }
}