using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlTypes;
using System.Numerics;

namespace EmployeePayrol_DB
{
    class EmployeeModel
    {
        public int Id { get; set; }   
        public string name { get; set; }
        public decimal basic_pay { get; set; }
        public DateTime start_Date { get; set; }
        public char gender { get; set; }
        public string phoneNumber { get; set; }
        public string department { get; set; }
        public string address { get; set; }                              
        public double deduction { get; set; }
        public SqlSingle taxable { get; set; }
        public SqlSingle netpay { get; set; }
        public double income_tax { get; set; }
    }
}

