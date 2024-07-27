using System.Text.Json;
using System.Text.Json.Serialization;
using FabricaDePersonajes;
using Api;
using HistorialJson;
using PersonajeJson;
using System.IO;
using System.Collections.Generic;
using torneo;
using Combate;

class Program
{
    
    static void Main()
    {
        string archivoPersonajes = "personajes.json";
        string archivoGanadores = "ganadores.json";
        PersonajesJson manejadorJson = new PersonajesJson();
        historialJson manejadorHistorial = new historialJson();
        List<FabricaDePersonaje> listaPersonajes = new List<FabricaDePersonaje>();

        // Verifico si el archivo de personajes existe y si tiene datos.
        if (manejadorJson.Existe(archivoPersonajes))
        {
            // Cargo los personajes desde el archivo existente.
            listaPersonajes = manejadorJson.LeerPersonajes(archivoPersonajes);
        }
        else
        {
            // Genero 10 personajes y los guardo en el archivo.
            for (int i = 0; i < 10; i++)
            {
                FabricaDePersonaje personaje = new FabricaDePersonaje();
                listaPersonajes.Add(personaje);
            }
            manejadorJson.GuardarPersonajes(listaPersonajes, archivoPersonajes);
        }

        // Menú principal
        bool continuar = true;
        while (continuar)
        {
            Console.WriteLine("Menú principal:");
            Console.WriteLine("1. Jugar");
            Console.WriteLine("2. Ver historial de ganadores");
            Console.WriteLine("3. Salir");
            Console.Write("Elige una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Jugar(listaPersonajes, manejadorHistorial, archivoGanadores);
                    break;

                case "2":
                    VerHistorial(manejadorHistorial, archivoGanadores);
                    break;

                case "3":
                    continuar = false;
                    Console.WriteLine("Gracias por jugar. ¡Hasta la próxima!");
                    break;

                default:
                    Console.WriteLine("Opción no válida. Por favor, elige una opción del menú.");
                    break;
            }
        }
    }

    static void Jugar(List<FabricaDePersonaje> listaPersonajes, historialJson manejadorHistorial, string archivoGanadores)
{
    Random random = new Random();
    List<FabricaDePersonaje> seleccionados = listaPersonajes.OrderBy(x => random.Next()).Take(3).ToList();

    Console.WriteLine("Selecciona un personaje:");
    for (int i = 0; i < seleccionados.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {seleccionados[i].MostrarCaracteristicas()}");
    }

    int eleccion = int.Parse(Console.ReadLine()) - 1;
    FabricaDePersonaje jugador = seleccionados[eleccion];

    Console.WriteLine($"Has elegido a {jugador.Nombre}.");

    // Inicio del torneo con los personajes restantes.
    listaPersonajes.RemoveAll(p => seleccionados.Contains(p));
    List<FabricaDePersonaje> participantesTorneo = listaPersonajes.OrderBy(x => random.Next()).Take(7).ToList();
    participantesTorneo.Add(jugador);

    // Realizar combates en el torneo
    foreach (var enemigo in participantesTorneo)
    {
        if (enemigo != jugador)
        {
            Combates combate = new Combates(jugador, enemigo);
            combate.RealizarCombate();

            if (jugador.Salud <= 0)
            {
                Console.WriteLine("Has sido derrotado. Fin del juego.");
                return;
            }
        }
    }

    Console.WriteLine($"El ganador del torneo es {jugador.Nombre}.");

    // Guardar al ganador en el archivo de ganadores.
    manejadorHistorial.GuardarGanador(jugador, archivoGanadores);
}


    static void VerHistorial(historialJson manejadorHistorial, string archivoGanadores)
    {
        List<FabricaDePersonaje> ganadores = manejadorHistorial.LeerGanadores(archivoGanadores);

        if (ganadores.Count == 0)
        {
            Console.WriteLine("No hay ganadores registrados aún.");
        }
        else
        {
            Console.WriteLine("Historial de ganadores:");
            foreach (var ganador in ganadores)
            {
                Console.WriteLine(ganador.MostrarCaracteristicas());
                Console.WriteLine("\n--------------------\n");
            }
        }
    }

}


    






    


    

