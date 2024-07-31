using System.Text.Json.Serialization;
using System.Collections.Generic;
using FabricaDePersonajes;
using System.Text.Json;
using System.IO;
using Api;

namespace FabricaDePersonajes
{
    public class FabricaDePersonaje 
    {
        //Características del personaje.
        public int Velocidad { get; private set; }
        public int Destreza { get; private set; }
        public int Fuerza { get; private set; }
        public int Nivel { get; private set; }
        public int Armadura { get; private set; }
        public int Salud { get; set; }
        //Datos del personaje.
        public string? Tipo { get; private set; }
        public string? Nombre { get; private set; }
        public string? Apodo { get; private set; }
        public DateTime FechaNac { get; private set; }
        public int Edad { get; private set; }

        //Metodo para potenciar al personaje.
        public void Potenciar(int valor)
        {
            this.Velocidad = Velocidad + valor;
            this.Destreza = Destreza + valor;
            this.Fuerza = Fuerza + valor;
            this.Nivel = Nivel + valor;
            this.Armadura = Armadura + valor;
        }

        private string []nombres = {"Artemis","Freya","Aurelio","Cleo","Hercules","Atticus","Demeter","Ares"};
        private string []apodos = {"El Valiente","El Sabio","El Rápido","El Fuerte","El Astuto","El Invencible","El Imparable","El Conquistador"};

        private static readonly HttpClient client = new HttpClient();

        //Metodo constructor del personaje.
        public FabricaDePersonaje()
        {
            Random random = new Random();
            this.Velocidad = random.Next(1,11);
            this.Destreza = random.Next(1,6);
            this.Fuerza = random.Next(1,11);
            this.Nivel = random.Next(1,11);
            this.Armadura = random.Next(1,11);
            this.Salud = 100;
            this.Nombre = nombres[random.Next(0,nombres.Length)];
            this.Apodo = apodos[random.Next(0,apodos.Length)];
            this.Edad = random.Next(0,301);
            this.FechaNac = CalcularFechaDeNacimiento(Edad);

            //Obtengo y asigno el tipo de personaje.
            Tipo = GetTipoAsync().GetAwaiter().GetResult();
        }

        //Metodo para calcular la fecha de nacimiento.
        private DateTime CalcularFechaDeNacimiento(int edad)
        {
            int anioActual = DateTime.Now.Year;
            int anioNacimiento = anioActual - edad;
            Random random = new Random();

            return new DateTime(anioNacimiento, random.Next(1,13), random.Next(1,29));
        }

        //Metodo para obtener el tipo.
        private static async Task<string> GetTipoAsync()
        {
            var url = "https://www.dnd5eapi.co/api/classes/";

            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            //DeserealizO la respuesta.
             var root = JsonSerializer.Deserialize<Root>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            //Elijo un tipo de personaje aleatoriamente.
            Random random = new Random();
            int index = random.Next(root.results.Count);

            return root.results[index].name;
        }

        //Método para mostrar todas las características del personaje.
        public string MostrarCaracteristicas()
        {
            return $"Nombre: {Nombre}\n" +
                   $"Apodo: {Apodo}\n" +
                   $"Tipo: {Tipo}\n" +
                   $"Fecha de Nacimiento: {FechaNac.ToShortDateString()}\n" +
                   $"Edad: {Edad}\n" +
                   $"Velocidad: {Velocidad}\n" +
                   $"Destreza: {Destreza}\n" +
                   $"Fuerza: {Fuerza}\n" +
                   $"Nivel: {Nivel}\n" +
                   $"Armadura: {Armadura}\n" +
                   $"Salud: {Salud}\n";
        }
    }
}