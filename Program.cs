﻿using System.Text.Json;
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
                    break;

                case "2":
                    VerHistorial(manejadorHistorial, archivoGanadores);
                    break;

                case "3":
                    continuar = false;
                    Console.WriteLine("\nGracias por jugar. ¡Hasta la próxima!\n");
                    break;

                default:
                    Console.WriteLine("\nOpción no válida. Por favor, elige una opción del menú.\n");
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

    //Se muestran los tres personajes disponibles que tiene el jugador para elegir.
    Console.WriteLine("Selecciona un personaje:");
    for (int i = 0; i < seleccionados.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {seleccionados[i].MostrarCaracteristicas()}");
    }

    //Guardo el jugador elegido.
    int eleccion = int.Parse(Console.ReadLine()) - 1;
    FabricaDePersonaje jugador = seleccionados[eleccion];

    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine($"Has elegido a {jugador.Nombre}.");
    Console.ResetColor();

    //Elimino a los tres personajes seleccionados anteriormente.
    listaPersonajes.RemoveAll(p => seleccionados.Contains(p));
    List<FabricaDePersonaje> participantesTorneo = listaPersonajes.OrderBy(x => random.Next()).Take(7).ToList();
    //Agrego al personaje sleccionado por el juagador.
    participantesTorneo.Add(jugador);

    //Arreglo con las rondas del torneo
    string[] rondas = {"Cuartos de final", "Semifinales", "Final" };

    int rondaActual = 0;

    //Realizar los combates del torneo.
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
            }else
            {
                //Mostrar mensaje de pasar de ronda si el jugador gana el combate.
                if (rondaActual < rondas.Length)
                {
                    //Utilizo esto para cambiar el color de las letras en la consola.
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"¡Felicidades! Has pasado a la ronda de {rondas[rondaActual]}.");
                    //Utilizo esto para restablecer el color de las letras en la consola.
                    Console.ResetColor();
                    rondaActual++;
                }
            }
        }
    }

    Console.WriteLine($"El ganador del torneo es {jugador.Nombre}.");

    //Guardo al ganador en el archivo de ganadores.
    manejadorHistorial.GuardarGanador(jugador, archivoGanadores);
}

    //Metodo para mostrar el historial.
    static void VerHistorial(historialJson manejadorHistorial, string archivoGanadores)
    {
        //Leo la lista de gandores desde el archivo Json.
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


    






    


    

