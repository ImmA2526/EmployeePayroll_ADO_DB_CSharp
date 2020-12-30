using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace EmployeePayrol_DB
{
    class EmployeeRepo
    {
        public static string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = EmployeePayroll; Integrated Security = True";
        SqlConnection Connection = new SqlConnection(connectionstring);

        /// <summary>
        /// UC 1 Checks the database connection.
        /// </summary>
        public void CheckDBConnection()
        {
            try
            {
                this.Connection.Open();
                Console.WriteLine("Connection Success");
                this.Connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            //finally
            //{
            //    this.Connection.Close();
            //}
        }

        /// <summary>
        /// UC 2 UC 4 Retrive  all employee.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void GetAllEmployee()
        {
            try
            {
                EmployeeModel Model = new EmployeeModel();
                using (this.Connection)
                {
                    string query = @"Select * from EmployeePayroll;";
                    SqlCommand CMD = new SqlCommand(query, this.Connection);
                    this.Connection.Open();
                    SqlDataReader reader = CMD.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Model.Id = reader.GetInt32(0);
                            Model.name = reader.GetString(1);
                            Model.basic_pay = reader.GetDecimal(2);
                            Model.start_Date = reader.GetDateTime(3);
                            Model.gender = Convert.ToChar(reader.GetString(4));
                            Model.phoneNumber = reader.GetString(5);
                            Model.department = reader.GetString(6);
                            Model.address = reader.GetString(7);
                            Model.deduction = reader.GetDouble(8);
                            Model.taxable = reader.GetSqlSingle(9);
                            Model.netpay = reader.GetSqlSingle(10);
                            Model.income_tax = reader.GetDouble(11);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", Model.Id, Model.name, Model.basic_pay, Model.start_Date, Model.gender, Model.phoneNumber, Model.department, Model.address, Model.deduction, Model.taxable, Model.netpay, Model.income_tax);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    reader.Close();
                    this.Connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.Connection.Close();
            }
        }

        /// <summary>
        /// UC 3 Insert Record.
        /// </summary>
        /// <exception cref="Exception"></exception>

        public bool AddRecord(EmployeeModel Model)
        {
            try
            {
                using (this.Connection)
                {
                    SqlCommand CMD = new SqlCommand("SpAddEmployeePayroll", this.Connection);
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.AddWithValue("@Id", Model.Id);
                    CMD.Parameters.AddWithValue("@name", Model.name);
                    CMD.Parameters.AddWithValue("@basic_pay", Model.basic_pay);
                    CMD.Parameters.AddWithValue("@start_Date", Model.start_Date);
                    CMD.Parameters.AddWithValue("@gender", Model.gender);
                    CMD.Parameters.AddWithValue("@phoneNumber", Model.phoneNumber);
                    CMD.Parameters.AddWithValue("@department", Model.department);
                    CMD.Parameters.AddWithValue("@address", Model.address);
                    CMD.Parameters.AddWithValue("@deduction", Model.deduction);
                    CMD.Parameters.AddWithValue("@taxable", Model.taxable);
                    CMD.Parameters.AddWithValue("@netpay", Model.netpay);
                    CMD.Parameters.AddWithValue("@income_tax", Model.income_tax);
                    this.Connection.Open();
                    var result = CMD.ExecuteNonQuery();
                    this.Connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.Connection.Close();
            }
        }

        /// <summary>
        /// UC 5 View Particuar Employee
        /// </summary>
        public void GetPerticularEmployeeData()
        {
            try
            {
                EmployeeModel Model = new EmployeeModel();
                using (this.Connection)
                {
                    //Get Basic Pay for Perticular Employee
                    string viewQuery = @"SELECT basic_pay  from EmployeePayroll WHERE name = 'Abhilasha'; ";
                    SqlCommand CMD = new SqlCommand(viewQuery, this.Connection);
                    this.Connection.Open();
                    SqlDataReader reader1 = CMD.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        while (reader1.Read())
                        {
                            Model.basic_pay = reader1.GetDecimal(0);
                        }
                        Console.WriteLine("Basic Pay for Abhilasha is : {0}", Model.basic_pay);
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    reader1.Close();
                    this.Connection.Close();
                    //Get List of Employee who Joined between perticular range of date
                    string ViewQuery = @"SELECT * FROM EmployeePayroll WHERE start_Date BETWEEN CAST('2010-05-02' as date) AND GETDATE(); ";
                    SqlCommand CMD2 = new SqlCommand(ViewQuery, this.Connection);
                    this.Connection.Open();
                    SqlDataReader reader = CMD2.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Console.WriteLine("Following is list of Employee who joined between Date: 2017-01-30 And 2020-12-29");
                        Console.WriteLine("\n");
                        while (reader.Read())
                        { 
                        Model.Id = reader.GetInt32(0);
                        Model.name = reader.GetString(1);
                        Model.basic_pay = reader.GetDecimal(2);
                        Model.start_Date = reader.GetDateTime(3);
                        Model.gender = Convert.ToChar(reader.GetString(4));
                        Model.phoneNumber = reader.GetString(5);
                        Model.department = reader.GetString(6);
                        Model.address = reader.GetString(7);
                        Model.deduction = reader.GetDouble(8);
                        Model.taxable = reader.GetSqlSingle(9);
                        Model.netpay = reader.GetSqlSingle(10);
                        Model.income_tax = reader.GetDouble(11);
                        Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", Model.Id, Model.name, Model.basic_pay, Model.start_Date, Model.gender, Model.phoneNumber, Model.department, Model.address, Model.deduction, Model.taxable, Model.netpay, Model.income_tax);
                    }
                    Console.WriteLine("\n");
                        
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    reader.Close();
                    this.Connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.Connection.Close();
            }
        }
    }
}