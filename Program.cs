using System;
using System.IO; // lo agrego el para el file
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad3
{
    class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine("MENU PRINCIPAL");
                Console.WriteLine("--------------");

                Console.WriteLine("1-Alta");
                Console.WriteLine("2-Modificar");
                Console.WriteLine("3-Baja");
                Console.WriteLine("4-Buscar");
                Console.WriteLine("9-SALIR");

                Console.Write("Ingrese una opcion y presione [ENTER]: ");
                var opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        Alta();
                        break;

                    case "2":
                        Modificar();
                        break;

                    case "3":
                        Baja();
                        break;

                    case "4":
                        Buscar();
                        break;

                    case "9":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("No ha ingresado una opción del menu.");
                        break;
                }
            } while (!salir);
        }
  

        private static void Buscar()
        {
            var cuenta = Plan.Seleccionar();
            
            if (cuenta != null)
            {
                cuenta.Mostrar();
            }
        }

        private static void Alta()
        {
            var cuenta = Cuenta.IngresarNueva();
            Plan.Agregar(cuenta);
        }

        private static void Baja()
        {
            var cuenta = Plan.Seleccionar();

            if (cuenta == null)
            {
                return;
            }

            cuenta.Mostrar();
            
            
            Console.WriteLine($"Se dispone a dar de baja a la cuenta: {cuenta.TituloEntrada}. ¿Esta usted seguro? (S/N)");
            var key = Console.ReadKey(intercept: true);
            if (key.Key == ConsoleKey.S)
            {
                Plan.Baja(cuenta);
            }            
        }
        
        private static void Modificar()
        {
            var cuenta = Plan.Seleccionar();
            if (cuenta == null)
            {
                return;
            }
            cuenta.Mostrar();
            cuenta.Modificar();
        }

    }
}

