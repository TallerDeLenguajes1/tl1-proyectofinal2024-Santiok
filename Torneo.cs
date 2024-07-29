using System.Collections.Generic;
using System.Linq;
using System;
using FabricaDePersonajes;
using Combate;

namespace torneo
{
    public class Torneo
    {
        //Declaro una lista con todos los personajes que participaran del torneo.
        private List<FabricaDePersonaje> participantes;

        //Metodo constructor que tomo una lista de personajes y la asigna al campo de participantes de mi clase.
        public Torneo(List<FabricaDePersonaje> participantes)
        {
            this.participantes = participantes;
        }

        //Metodo para inicializar el torneo.
        public FabricaDePersonaje IniciarTorneo()
        {
            //El bucle se ejacuta mientras haya mas de un participante en la lista.
            while (participantes.Count > 1)
            {
                //En cada ronda creo una nueva lista con los personajes que siguen vivos.
                List<FabricaDePersonaje> ganadoresRonda = new List<FabricaDePersonaje>();

                for (int i = 0; i < participantes.Count; i += 2)
                {
                    //Realizo el combate entre dos personajes de la lista.
                    Combates combate = new Combates(participantes[i], participantes[i + 1]);
                    combate.RealizarCombate();

                    //Comparo la salud de los jugadores para ver quien gano.
                    if (participantes[i].Salud > participantes[i + 1].Salud)
                    {
                        ganadoresRonda.Add(participantes[i]);
                    }
                    else
                    {
                        ganadoresRonda.Add(participantes[i + 1]);
                    }
                }

                //Actualizo la lista con los gandores de cada ronda.
                participantes = ganadoresRonda;
            }

            //Retorno el unico jugador restante.
            return participantes.First();
        }
    }
}
