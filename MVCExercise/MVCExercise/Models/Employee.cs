using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCExercise.Models
{


    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string address { get; set; }

        public static Employee GetProduct()
        {
            Employee Emp = new Employee();
            return Emp;
        }
    }
}

