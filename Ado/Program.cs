using System;

namespace EmployeePayrol_DB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("************Welcome To EmployeeDataBase************");
            EmployeeRepo Repo = new EmployeeRepo();
            Repo.CheckDBConnection();
            Repo.GetAllEmployee();
        }
    }
}
