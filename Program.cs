using System.Text.Json;
using System.Text.Json.Serialization;
using FabricaDePersonajes;
using Api;

/*FabricaDePersonaje personaje1 = FabricaDePersonaje();

Console.WriteLine(personaje1.MostrarCaracteristicas());
*/

class Program
{
    static void Main()
    {
        FabricaDePersonaje personaje1 = new FabricaDePersonaje();
        Console.WriteLine(personaje1.MostrarCaracteristicas());
    }
}