using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using FabricaDePersonajes;
using HistorialJson;
using PersonajeJson;
using Combate;
using Api;

class Program
{
    static void Main()
    {
        string archivoPersonajes = "personajes.json";
        string archivoGanadores = "ganadores.json";
        PersonajesJson manejadorJson = new PersonajesJson();
        historialJson manejadorHistorial = new historialJson();
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
            /*
            //Muestro por pantalla los datos y características de los personajes cargados.
            foreach (var personaje in listaPersonajes)
            {
                Console.WriteLine(personaje.MostrarCaracteristicas());
                Console.WriteLine("\n--------------------\n");
            }
            */
        }

        //Menú principal
        bool continuar = true;
        while (continuar)
        { 
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(
            "╔══════════════════════════════╗\n" +
            "║          Menú principal      ║\n" +
            "╠══════════════════════════════╣\n" +
            "║ 1. Jugar                     ║\n" +
            "║ 2. Ver historial de ganadores║\n" +
            "║ 3. Salir                     ║\n" +
            "╚══════════════════════════════╝\n" +
            "Elige una opción: ");
            Console.ResetColor();
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Jugar(listaPersonajes, manejadorHistorial, archivoGanadores);
                    
                    //Genero otros 10 personajes nuevos para poder seguir jugando.
                    //Limpio la lista actual antes de generar nuevos personajes.
                    listaPersonajes.Clear(); 

                    for (int i = 0; i < 10; i++)
                    {
                        FabricaDePersonaje personaje = new FabricaDePersonaje();
                        listaPersonajes.Add(personaje);
                    }
                    manejadorJson.GuardarPersonajes(listaPersonajes, archivoPersonajes);

                    break;

                case "2":
                    VerHistorial(manejadorHistorial, archivoGanadores);
                    break;

                case "3":
                    continuar = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nGracias por jugar. ¡Hasta la próxima!\n");
                    Console.ResetColor();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nOpción no válida. Por favor, elige una opción del menú.\n");
                    Console.ResetColor();
                    break;
            }
        }
    }

    //Metodo para jugar.
    static void Jugar(List<FabricaDePersonaje> listaPersonajes, historialJson manejadorHistorial, string archivoGanadores)
{
    //Selecciono tres personajes aleatorios de la lista de parsonajes para que el usuario elija uno de ellos. 
    Random random = new Random();
    List<FabricaDePersonaje> seleccionados = listaPersonajes.OrderBy(x => random.Next()).Take(3).ToList();

    //Restablecer la salud de los personajes seleccionados antes de mostrarlos.
    foreach (var personaje in seleccionados)
    {
        personaje.Salud = 100;
    }

    //Se muestran los tres personajes disponibles que tiene el jugador para elegir.
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.WriteLine("\nSelecciona un personaje:\n");
    Console.ResetColor();
    for (int i = 0; i < seleccionados.Count; i++)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"{i + 1}. {seleccionados[i].MostrarCaracteristicas()}");
        Console.ResetColor();
    }

    //Control para que se elija una opcion correcta.
    FabricaDePersonaje jugador = null;
        while (jugador == null)
        {
            if (int.TryParse(Console.ReadLine(), out int eleccion) && eleccion >= 1 && eleccion <= 3)
            {
                jugador = seleccionados[eleccion - 1];
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nOpción no válida. Por favor, elige un número entre 1 y 3.\n");
                Console.ResetColor();
            }
        }

    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine($"Has elegido a {jugador.Nombre}.");
    Console.ResetColor();

    //Elimino a los tres personajes seleccionados anteriormente.
    listaPersonajes.RemoveAll(p => seleccionados.Contains(p));
    List<FabricaDePersonaje> participantesTorneo = listaPersonajes.OrderBy(x => random.Next()).Take(4).ToList();
    //Agrego al personaje sleccionado por el juagador.
    participantesTorneo.Add(jugador);

    //Arreglo con las rondas del torneo.
    string[] rondas = {"Cuartos de final", "Semifinales", "Final" };

    int rondaActual = 0;

    //Realizar los combates del torneo.
        foreach (var enemigo in participantesTorneo)
        {
            if (enemigo != jugador)
            {
                int precision = 1 + rondaActual*10;
                Combates combate = new Combates(jugador, enemigo);
                combate.RealizarCombate(precision);

                if (jugador.Salud <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Has sido derrotado. Fin del juego.");
                    Console.ResetColor();
                    return;
                }
                else
                {
                    //Mostrar mensaje de pasar de ronda si el jugador gana el combate.
                    if (rondaActual < rondas.Length)
                    {
                        jugador.Salud = 100 + rondaActual*5;
                        jugador.Potenciar(1); 
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"¡Felicidades! Has pasado a la ronda de {rondas[rondaActual]}.");
                        Console.ResetColor();
                        rondaActual++;
                    }
                }
            }
        }

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"\nEl ganador del torneo es {jugador.Nombre}.\n");
    Console.WriteLine($"\nFELICIDADES GANASTE EL TORNEO!!!!\n");
    Console.ResetColor();

    //Guardo al ganador en el archivo de ganadores.
    manejadorHistorial.GuardarGanador(jugador, archivoGanadores);
}


    //Metodo para mostrar el historial.
    static void VerHistorial(historialJson manejadorHistorial, string archivoGanadores)
    {
        //Leo la lista de gandores desde el archivo Json.
        List<FabricaDePersonaje> ganadores = manejadorHistorial.LeerGanadores(archivoGanadores);

        if (ganadores.Count == 0 || ganadores == null)
        {
            Console.WriteLine("\nNo hay ganadores registrados aún.\n");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nHistorial de ganadores:\n");
            Console.ResetColor();
            foreach (var ganador in ganadores)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(ganador.MostrarCaracteristicas());
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n--------------------\n");
            }
        }
    }
}
