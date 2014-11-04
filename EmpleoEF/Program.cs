using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpleoEF.Model;

namespace EmpleoEF
{
    class Program
    {
        static EmpleoEntities db = new EmpleoEntities();

        static void Main(string[] args)
        {
            int opcion = 0;
            do
            {
                Console.WriteLine("1.Dar de alta\n2.Buscar por id\n3.Buscar por salario\n4.Dar de baja\n5.Salir");
                Int32.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1:
                        Alta();
                        break;
                    case 2:
                        BuscarId();
                        break;
                    case 3:
                        BuscarSalario();
                        break;
                    case 4:
                        DarBaja();
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Opción incorrecta");
                        break;
                }

            } while (opcion != 5);

        }

        private static void Alta()
        {
            //DAR DE ALTA
            Console.WriteLine("Introduzca nombre");
            var newNombre = Console.ReadLine();

            Console.WriteLine("Introduzca dni");
            var newDni = Console.ReadLine();

            int newCargo;
            Console.WriteLine("Introduzca cargo");
            int.TryParse(Console.ReadLine(), out newCargo);

            int newsalario;
            Console.WriteLine("Introduzca salario");
            int.TryParse(Console.ReadLine(), out newsalario);

            var db = new EmpleoEntities();
            
            var empleado = new Empleado()
            {
                nombre = newNombre,
                dni = newDni,
                idCargo = newCargo,
                salario = newsalario
            };

            db.Empleado.Add(empleado);
            db.SaveChanges();

        }

        private static void BuscarId()
        {
            //BUSCAR POR ID
            int buscaId;
            Console.WriteLine("Introduzca id para buscar");
            int.TryParse(Console.ReadLine(), out buscaId);

            var empleado = db.Empleado.Find(buscaId);

            if (empleado==null)
            {
                Console.WriteLine("No existe empleado");
            }
            else
            {
                Console.WriteLine("El empleado con el id {0}, su nombre es {1}, su dni es {2}," +
                              "el id del cargo es {3} y su saliro es de {4}",
                empleado.id, empleado.nombre, empleado.dni, empleado.idCargo, empleado.salario);
            }
        }

        private static void BuscarSalario()
        {
            //BUSCAR POR SALARIO
            int buscaSalario;
            Console.WriteLine("Introduzca salario para buscar");
            int.TryParse(Console.ReadLine(), out buscaSalario);

            var empleados = db.Empleado.Where(o => o.salario == buscaSalario).OrderBy(o => o.salario);
            var contador = 0;
            foreach (var empleado in empleados)
            {
                if (empleado == null)
                {
                    Console.WriteLine("No existe empleado");
                }
                else
                {
                    contador++;
                    Console.WriteLine("{0}. El empleado con el id {1}, su nombre es {2}, su dni es {3}, " +
                            "el id del cargo es {4} y su saliro es de {5}",
                            contador, empleado.id, empleado.nombre, empleado.dni, empleado.idCargo, empleado.salario);
                }
              
            }

        }

        private static void DarBaja()
        {
            int introduceId;
            Console.WriteLine("Introduzca id para dar de baja");
            int.TryParse(Console.ReadLine(), out introduceId);

            var empleado = db.Empleado.Find(introduceId);

            if (empleado == null)
            {
                Console.WriteLine("No existe empleado");
            }
            else
            {
                db.Empleado.Remove(empleado);
                db.SaveChanges();
            }

        }
    }
}
