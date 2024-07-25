using System.Text.Json;
using System.Text.Json.Serialization;
using FabricaDePersonajes;
using Api;

namespace PersonajeJson
{
    public class PersonajesJson
    {
        //Metodo para guardar personajes.
        public void GuardarPersonajes(List<FabricaDePersonaje> personajes, string nombreArchivo)
        {
            var IdentacionJson = new JsonSerializerOptions {WriteIndented = true};
            string personajesJson = JsonSerializer.Serialize(personajes, IdentacionJson);
            File.WriteAllText(personajesGuardados, personajesJson);
        }

        //Metodo para leer el personaje.
        public List<FabricaDePersonaje> LeerPersonajes(string personajesGuardados)
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

        //Metodo para preguntar si existe el archivo.
        public bool Existe(string personajesGuardados)
        {
            if (File.Exists(personajesGuardados) && new FileInfo(nombreArchivo).Length > 0)
            {
                return true;
            } else
            {
                return false;
            }
        }

    }
}