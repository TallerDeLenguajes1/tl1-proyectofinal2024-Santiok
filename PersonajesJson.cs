using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using FabricaDePersonajes;
using Api;

namespace PersonajeJson
{
    public class PersonajesJson
    {
        //Metodo para guardar personajes.
        public void GuardarPersonajes(List<FabricaDePersonaje> personajes, string personajesGuardados)
        {
            try
            {
                var IdentacionJson = new JsonSerializerOptions {WriteIndented = true};
                string personajesJson = JsonSerializer.Serialize(personajes, IdentacionJson);
                File.WriteAllText(personajesGuardados, personajesJson);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error al guardar el personaje: {ex.Message}");
            }
        }

        //Metodo para leer el personaje.
        public List<FabricaDePersonaje> LeerPersonajes(string personajesGuardados)
        {
            try
            {
                if (!File.Exists(personajesGuardados))
                {
                return new List<FabricaDePersonaje>();
                }else
                {
                string personajesJson = File.ReadAllText(personajesGuardados);

                List<FabricaDePersonaje> personajes = JsonSerializer.Deserialize<List<FabricaDePersonaje>>(personajesJson);

                return personajes;

                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error al leer personajes: {ex.Message}");
                return new List<FabricaDePersonaje>();
            }
        }

        //Metodo para preguntar si existe el archivo.
        public bool Existe(string personajesGuardados)
        {
            if (File.Exists(personajesGuardados) && new FileInfo(personajesGuardados).Length > 0)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}