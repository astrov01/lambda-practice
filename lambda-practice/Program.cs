using lamda_practice.Data;
using System;
using System.Globalization;
using System.Linq;

namespace lambda_practice
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var ctx = new DatabaseContext())
            {

                Console.WriteLine("All the employees:");

                var employees = ctx.Employees.ToList();
                foreach (var employee in employees)
                {
                    Console.WriteLine(employee.FirstName);
                }

                //1. Listar todos los empleados cuyo departamento tenga una sede en Chihuahua
                var chihuahuaEmployees = ctx.Employees.Where(cities => cities.City.Name == "Chihuahua").ToList();

                foreach (var employee in chihuahuaEmployees)
                {
                    Console.WriteLine(employee.FirstName);
                }

                //2. Listar todos los departamentos y el numero de empleados que pertenezcan a cada departamento.

                var departments = ctx.Departments.ToList();
                foreach(var department in departments)
                {
                    var departmentEmployees = ctx.Employees.Where(Department => department.Id.Equals(department.Id));
                    Console.WriteLine($"The department {department.Name} has a total of {departmentEmployees.Count()} employees.");
                }

                //3. Listar todos los empleados remotos. Estos son los empleados cuya ciudad no se encuentre entre las sedes de su departamento.

                var remoteEmployees = ctx.Employees.Where(employee => employee.Department.Cities.Any(department => department.Name != employee.City.Name));

                foreach(var employee in remoteEmployees)
                {
                    Console.WriteLine($"The employee {employee.FirstName} is remote");
                }

                //4. Listar todos los empleados cuyo aniversario de contratación sea el próximo mes.

                var Anniversary = ctx.Employees.Where(employee => employee.HireDate.Month == (DateTime.Now.Month + 1));

                foreach (var employee in Anniversary)
                {
                    Console.WriteLine($"{employee.FirstName} has an anniversary next month.");
                }

                //Listar los 12 meses del año y el numero de empleados contratados por cada mes.
                for (int x = 1; x <= 12; x++)
                {
                    var cont = ctx.Employees
                        .Where(a => a.HireDate.Month == x)
                        .Count();
                    Console.WriteLine(x + " " + cont);
                }

            }


            Console.Read();
        }
    }
}
