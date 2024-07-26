using System.Text.Json;
using System.Text.Json.Serialization;
using FabricaDePersonajes;
using Api;
using HistorialJson;
using PersonajeJson;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string archivoPersonajes = "personajes.json";
        PersonajesJson manejadorJson = new PersonajesJson();
        List<FabricaDePersonaje> listaPersonajes = new List<FabricaDePersonaje>();

        //Verifico si el archivo de personajes existe y si tiene datos.
        if (manejadorJson.Existe(archivoPersonajes))
        {
            //Cargo los personajes desde el archivo existente.
            listaPersonajes = manejadorJson.LeerPersonajes(archivoPersonajes);
        }
        else
        {
            //Genero 10 personajes y los guardo en el archivo.
            for (int i = 0; i < 10; i++)
            {
                FabricaDePersonaje personaje = new FabricaDePersonaje();
                listaPersonajes.Add(personaje);
            }
            manejadorJson.GuardarPersonajes(listaPersonajes, archivoPersonajes);
        }

        //Muestro por pantalla los datos y características de los personajes cargados.
        foreach (var personaje in listaPersonajes)
        {
            Console.WriteLine(personaje.MostrarCaracteristicas());
            Console.WriteLine("\n--------------------\n");
        }
    }
}


    






    


    

