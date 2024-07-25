using System.Text.Json;
using System.Text.Json.Serialization;
using FabricaDePersonajes;
using Api;

namespace HistorialJson
{
    public class HistorialJson
    {
        //Metodo para guardar al ganador.
        public string GuardarGanador(FabricaDePersonaje ganador, string listaDeGanadores)
        {
            if (!File.Exists(listaDeGanadores))
            {
                List<FabricaDePersonaje> lista = new List<FabricaDePersonaje>();
                lista.add(ganador);

                var IdentacionJson = new JsonSerializerOptions {WriteIndented = true};
                string nuevaListaGanadores = JsonSerializer.Serialize(lista, IdentacionJson);
                File.WriteAllText(nuevosGanadores, nuevaListaGanadores);

                return nuevosGanadores;

            }else
            {
                string listaJson = File.ReadAllText(listaDeGanadores);
                List<FabricaDePersonaje> lista = JsonSerializer.Deserialize<List<FabricaDePersonaje>>(listaJson);
                lista.add(ganador);

                var IdentacionJson = new JsonSerializerOptions {WriteIndented = true};
                string nuevaListaGanadores = JsonSerializer.Serialize(lista, IdentacionJson);
                File.WriteAllText(nuevosGanadores, nuevaListaGanadores);

                return nuevosGanadores;
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

                List<FabricaDePersonaje> ganadores = JsonSerializer.Deserialize<List<FabricaDePersonaje>>(ganaddoresJson);

                return ganadores;
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