using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPNetMVCCrud.Models
{
    public class Employee
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string EmployeeAddress { get; set; }

        [Required]
        [Display(Name = "Position")]
        public string Position { get; set; }
    }
}