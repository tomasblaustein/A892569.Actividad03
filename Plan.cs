using System;
using System.IO; // lo agrego el para el file
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad3
{
    static class Plan
    {
        private static readonly Dictionary<int, Cuenta> entradas;
        private static readonly Dictionary<int, Cuenta> entradasMayor;

        const string nombreArchivo = @"C:\Users\tomi\Desktop\PlanDeCuentas.txt";
        const string nombreArchivoMayor = @"C:\Users\tomi\Desktop\Mayor.txt";
        static Plan()
        { 
            entradas = new Dictionary<int, Cuenta>();
            entradasMayor = new Dictionary<int, Cuenta>();

            if (File.Exists(nombreArchivo))
            {
                using (var reader = new StreamReader(nombreArchivo))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();

                        if (linea == "Codigo|Nombre|Tipo")
                        {
                            continue;
                        }
                        else
                        {
                            var cuenta = new Cuenta(linea);
                            entradas.Add(cuenta.CODIGO, cuenta);
                        }
                    }
                }
            }
            if (File.Exists(nombreArchivoMayor))
            {
                using (var reader = new StreamReader(nombreArchivoMayor))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();

                        if (linea == "CodigoCuenta|Fecha|Debe|Haber")
                        {
                            continue;
                        }

                        var datos = linea.Split('|');
                        int codigo = int.Parse(datos[0]);
                        var cuenta = new Cuenta(codigo);
                        entradasMayor.Add(cuenta.CODIGO, cuenta);
                    }
                }
            }
        }

        public static void Agregar(Cuenta cuenta)
        {
            entradas.Add(cuenta.CODIGO, cuenta);
            Grabar();
        }

        public static Cuenta Seleccionar()
        {
            var modelo = Cuenta.CrearModeloBusqueda();

            foreach (var cuenta in entradas.Values)
            {
                if (cuenta.CoincideCon(modelo))
                {
                    return cuenta;
                }
            }

            Console.WriteLine("No se ha encontrado una cuenta que coincida con los criterios indicados.");
            return null;

        }
        
        public static void Baja (Cuenta cuenta)
        {
            if (ExisteMayor(cuenta.CODIGO))
            {
                Console.WriteLine("No se puede eliminar la cuenta ya que tiene saldos en el Mayor.");
            }
            else
            {
                entradas.Remove(cuenta.CODIGO);
                Console.WriteLine($"La cuenta: {cuenta.TituloEntrada}, ha sido dado de baja.");
                Grabar();
            }
        }

        public static bool Existe(int codigo)
        {
            return entradas.ContainsKey(codigo);
        }

        public static bool ExisteMayor(int codigo)
        {
            return entradasMayor.ContainsKey(codigo);
        }

        public static void Grabar()
        {
            using (var writer = new StreamWriter(nombreArchivo, append: false))
            {
                string encabezado = "Codigo|Nombre|Tipo";
                writer.WriteLine(encabezado);
                foreach (var cuenta in entradas.Values)
                {
                    var linea = cuenta.ObtenerLineaDatos();
                    writer.WriteLine(linea);
                }
            }
        }
    }
}
