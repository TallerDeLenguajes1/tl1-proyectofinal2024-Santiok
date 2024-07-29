using FabricaDePersonajes;
using System;

namespace Combate
{
    public class Combates
    {
        //Declaro a mi jugador y al opnente.
        private FabricaDePersonaje jugador;
        private FabricaDePersonaje enemigo;

        //Metodo costructor para inicializar los valores de los perdonajes.
        public Combates(FabricaDePersonaje jugador, FabricaDePersonaje enemigo)
        {
            this.jugador = jugador;
            this.enemigo = enemigo;
        }

        //Metodo para realizar el combate.
        public void RealizarCombate()
        {
            //Declaro un avariable random para calcular la efectividad de los ataques.
            Random random = new Random();
            const int ConstanteAjuste = 500;

            //El combate se realiza mientras ambos personajes tengan salud > 0.
            while (jugador.Salud > 0 && enemigo.Salud > 0)
            {
                //Turno de mi jugador.
                int efectividadJugador = random.Next(1, 101);
                //Calculo el ataque de mi jugador.
                int ataqueJugador = jugador.Destreza * jugador.Fuerza * jugador.Nivel;
                //Calculo la defensa del enemigo.
                int defensaEnemigo = enemigo.Armadura * enemigo.Velocidad;

                //Calculo el daño total provocado.
                int dañoJugadorProvocado = ((ataqueJugador * efectividadJugador) - defensaEnemigo) / ConstanteAjuste;
                //Esto es para asegurar que el daño no sea negativo.
                dañoJugadorProvocado = Math.Max(0, dañoJugadorProvocado);

                //Redusco la salud del rival.
                enemigo.Salud -= dañoJugadorProvocado;

                //Utilizo esto para cambiar el color de las letras en la consola.
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"\n{jugador.Nombre} atacó a {enemigo.Nombre} y le provocó {dañoJugadorProvocado} puntos de daño.");
                Console.WriteLine($"{enemigo.Nombre} ahora tiene {enemigo.Salud} puntos de salud.\n");
                //Utilizo esto para restablecer el color de las letras en la consola.
                Console.ResetColor();

                //Verifico si mi enemigo sigue con salud > 0.
                if (enemigo.Salud <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{enemigo.Nombre} ha sido derrotado.\n");
                    Console.ResetColor();
                    break;
                }

                //Turno del enemigo.
                int efectividadEnemigo = random.Next(1, 101);
                //Ahora calculo el ataque del enemigo.
                int ataqueEnemigo = enemigo.Destreza * enemigo.Fuerza * enemigo.Nivel;
                //Ahora calculo la defensa de mi personaje.
                int defensaJugador = jugador.Armadura * jugador.Velocidad;

                //Calculo el daño total que le provocaron a mi jugasor.
                int dañoEnemigoProvocado = ((ataqueEnemigo * efectividadEnemigo) - defensaJugador) / ConstanteAjuste;
                //Esto es para asegurar que el daño no sea negativo.
                dañoEnemigoProvocado = Math.Max(0, dañoEnemigoProvocado);

                //Reduzco la salud de mi personaje.
                jugador.Salud -= dañoEnemigoProvocado;

                //Utilizo esto para cambiar el color de las letras en la consola.
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\n{enemigo.Nombre} atacó a {jugador.Nombre} y le provocó {dañoEnemigoProvocado} puntos de daño.");
                Console.WriteLine($"{jugador.Nombre} ahora tiene {jugador.Salud} puntos de salud.\n");
                //Utilizo esto para restablecer el color de las letras en la consola.
                Console.ResetColor();

                //Verifico si mi jugador sigue con vida.
                if (jugador.Salud <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{jugador.Nombre} ha sido derrotado.\n");
                    Console.ResetColor();
                    break;
                }
            }
        }
    }
}
