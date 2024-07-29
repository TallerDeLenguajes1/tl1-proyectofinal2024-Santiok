using System.Text.Json.Serialization;
using System.Collections.Generic;
using FabricaDePersonajes;
using System.Text.Json;
using System.IO;
using Api;

namespace HistorialJson
{
    public class historialJson
    {
        //Metodo para guardar al ganador.
        public string GuardarGanador(FabricaDePersonaje ganador, string listaDeGanadores)
        {
            if (!File.Exists(listaDeGanadores))
            {
                List<FabricaDePersonaje> lista = new List<FabricaDePersonaje>();
                lista.Add(ganador);

                var IdentacionJson = new JsonSerializerOptions {WriteIndented = true};
                string nuevaListaGanadores = JsonSerializer.Serialize(lista, IdentacionJson);
                File.WriteAllText(listaDeGanadores, nuevaListaGanadores);

                return listaDeGanadores;

            }else
            {
                string listaJson = File.ReadAllText(listaDeGanadores);
                List<FabricaDePersonaje> lista = JsonSerializer.Deserialize<List<FabricaDePersonaje>>(listaJson);
                lista.Add(ganador);

                var IdentacionJson = new JsonSerializerOptions {WriteIndented = true};
                string nuevaListaGanadores = JsonSerializer.Serialize(lista, IdentacionJson);
                File.WriteAllText(listaDeGanadores, nuevaListaGanadores);

                return listaDeGanadores;
            }
        }

        //Metodo para leer los ganadores.
        public List<FabricaDePersonaje> LeerGanadores(string ganadoresGuardados)
        {
            if (!File.Exists(ganadoresGuardados))
            {
                return new List<FabricaDePersonaje>();
            }else
            {
                string ganadoresJson = File.ReadAllText(ganadoresGuardados);

                List<FabricaDePersonaje> ganadores = JsonSerializer.Deserialize<List<FabricaDePersonaje>>(ganadoresJson);

                return ganadores;
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