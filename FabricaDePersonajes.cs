using Api;
namespace FabricaDePersonajes
{
    public class FabricaDePersonajes 
    {
        //Caracteristicas del personaje.
        private int velocidad;
        private int destreza;
        private int fuerza;
        private int nivel;
        private int armadura;
        private int salud;
        //Datos del personaje.
        private string tipo;
        private string? nombre;
        private string? apodo;
        private DateTime fechaNac;
        private int edad;

        private string []apodos = { "El Valiente","El Sabio","El Rápido","El Fuerte","El Astuto","El Invencible","El Imparable","El Conquistador"};

        //Metodo constructor del personaje.
        public FabricaDePersonajes()
        {
            Random random = new Random();
            int velocidad = random.Next(1,11);
            int destreza = random.Next(1,6);
            int fuerza = random.Next(1,11);
            int nivel = random.Next(1,11);
            int armadura = random.Next(1,11);
            int salud = 100;
            string apodo = apodos[random.Next(0,6)];
            int edad = random.Next(0,301);
            DateTime fechaNac = CalcularFechaDeNacimiento(edad);
        }

        //Metodo para calcular la fecha de nacimiento.
        private DateTime CalcularFechaDeNacimiento(int edad)
        {
            int anioActual = DateTime.Now.Year;
            int anioNacimiento = anioActual - edad;
            Random random = new Random();

            return new DateTime(anioNacimiento, random.Next(1,13), random.Next(1,29));
        }





    }























}