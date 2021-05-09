using System;
using System.IO; // lo agrego el para el file
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad3
{
    class Cuenta
    {
        public int CODIGO { get; set; } // { si no le pongo el set, unicamente se puede escribir 1 vez por un constructor. } 
        public string Nombre { get; set; } // { q pasa cuando leo y cuando escribo esa propiedad } 
        public string Tipo { get; set; }

        public string TituloEntrada
        {
            get
            {
                return $"{Nombre}, {Tipo} - CODIGO: {CODIGO}"; 
            }
        }

        public Cuenta() { }
        public Cuenta(string linea)
        {
            var datos = linea.Split('|');
            CODIGO = int.Parse(datos[0]);
            Nombre = datos[1];
            Tipo = datos[2];
        }

        public Cuenta (int codigo)
        {
            CODIGO = codigo;
        }
        public string ObtenerLineaDatos()
        {
            return $"{CODIGO}|{Nombre}|{Tipo}";
        }

        public static Cuenta IngresarNueva()
        {
            var cuenta = new Cuenta();

            cuenta.CODIGO = IngresarCODIGO();
            cuenta.Nombre = Ingreso("Ingrese el nombre de la cuenta: ");
            cuenta.Tipo = IngresoTipo("Ingrese el tipo de cuenta: ");


            return cuenta;
        }
        

        public void Modificar()
        {
            Console.WriteLine($"Nombre: {Nombre} - Presione M para modificar / cualquier tecla para seguir: ");
            var tecla = Console.ReadKey(intercept: true);
            if (tecla.Key == ConsoleKey.M)
            {
                Nombre = Ingreso("Ingrese el nuevo nombre: ");
            }

            Plan.Grabar();
        }

        public void Mostrar()
        {
            Console.WriteLine();
            Console.WriteLine($"CODIGO: {CODIGO}");
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Tipo: {Tipo}");
            Console.WriteLine();
        }

        public static Cuenta CrearModeloBusqueda()
        {
            var modelo = new Cuenta();
            bool ok = false;

            do
            {
                modelo.CODIGO = IngresarCODIGO2(obligatorio: false);
                modelo.Nombre = Ingreso("Ingrese el nombre,", obligatorio: false);
                // modelo.Tipo = Ingreso("Ingrese el tipo,", obligatorio: false);

                if (modelo.CODIGO == 0 && modelo.Nombre == "")
                {
                    ok = false;
                    return null;
                }
                else
                {
                    ok = true;
                    return modelo;
                }

            } while (!ok);
        }

        private static int IngresarCODIGO(bool obligatorio = true)
        {
            var titulo = "Ingrese el CODIGO de la cuenta (Entero positivo): ";

            if (!obligatorio)
            {
                titulo += "o presione [Enter] para continuar: ";
            }

            do
            {
                Console.Write(titulo);

                var ingreso = Console.ReadLine(); // readline devuelve un string, hay que pasarlo a int

                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    return 0;
                }

                if (!int.TryParse(ingreso, out var codigo))
                {
                    Console.WriteLine("No ha ingresado un CODIGO valido. Ingrese nuevamente: ");
                    continue; // reinicia el ciclo
                }

                if (codigo < 1)
                {
                    Console.WriteLine("Debe ser un numero entero positivo. Ingrese nuevamente: ");
                    continue; // reinicia el ciclo
                }

                
                if (Plan.Existe(codigo))
                {
                    Console.WriteLine("El CODIGO indicado ya existe en el Plan de Cuentas. Ingrese nuevamente: ");
                    continue; // reinicia el ciclo
                }
                
                return codigo;

            } while (true);
        }

        private static int IngresarCODIGO2(bool obligatorio = true)
        {
            var titulo = "Ingrese el CODIGO de la cuenta (Entero positivo): ";

            if (!obligatorio)
            {
                titulo += "o presione [Enter] para continuar: ";
            }

            do
            {
                Console.Write(titulo);

                var ingreso = Console.ReadLine(); // readline devuelve un string, hay que pasarlo a int

                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    return 0;
                }

                if (!int.TryParse(ingreso, out var codigo))
                {
                    Console.WriteLine("No ha ingresado un CODIGO valido. Ingrese nuevamente: ");
                    continue; // reinicia el ciclo
                }

                if (codigo < 1)
                {
                    Console.WriteLine("Debe ser un numero entero positivo. Ingrese nuevamente: ");
                    continue; // reinicia el ciclo
                }

                return codigo;

            } while (true);
        }

        public bool CoincideCon(Cuenta modelo)
        {
            if (modelo is null)
            {
                return false;
            }
            else
            {
                if (modelo.CODIGO != 0 && CODIGO != modelo.CODIGO)
                {
                    return false;
                }

                if (!string.IsNullOrWhiteSpace(modelo.Nombre) && !Nombre.Equals(modelo.Nombre, StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }

                if (!string.IsNullOrWhiteSpace(modelo.Tipo) && !Tipo.Equals(modelo.Tipo, StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }

                return true;
            }
            
        }

        private static string Ingreso(string titulo, bool obligatorio = true)
        {
            string ingreso;

            do
            {
                if (!obligatorio)
                {
                    titulo += " o presione [Enter] para continuar: ";
                }

                Console.Write(titulo);
                ingreso = Console.ReadLine();

                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    return null;
                }

                if (obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("Debe ingresar un valor.");
                    continue;
                }

                if (ingreso.Any(Char.IsDigit))
                {
                    Console.WriteLine("El valor ingresado no debe contener numeros.");
                    continue;
                }

                break;

            } while (true);

            return ingreso;
        }

        private static string IngresoTipo(string titulo, bool obligatorio = true)
        {
            string ingreso;

            do
            {
                if (!obligatorio)
                {
                    titulo += " o presione [Enter] para continuar: ";
                }

                Console.Write(titulo);
                ingreso = Console.ReadLine();

                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    return null;
                }

                if (obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("Debe ingresar un valor.");
                    continue;
                }

                if (ingreso.Any(Char.IsDigit))
                {
                    Console.WriteLine("El valor ingresado no debe contener numeros.");
                    continue;
                }

                if (ingreso != "Pasivo" && ingreso != "Activo" && ingreso != "PatrimonioNeto")
                {
                    Console.WriteLine("El valor ingresado debe ser de un tipo valido (Activo - Pasivo - PatrimonioNeto).");
                    continue;
                }

                break;

            } while (true);

            return ingreso;
        }
    }
}
