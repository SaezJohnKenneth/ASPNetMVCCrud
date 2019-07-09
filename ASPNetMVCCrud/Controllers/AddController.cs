using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPNetMVCCrud.Models;
using MySql.Data.MySqlClient;

namespace ASPNetMVCCrud.Controllers
{
    public class AddController : Controller
    {
        // GET: Add
        private MySqlConnection con;
        private MySqlCommand cmd;
        private MySqlDataReader reader;
        private MySqlDataAdapter adapter;
        private MySqlTransaction transaction;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Employee employee)
        {
            if (employee.EmployeeName == null || employee.EmployeeAddress == null || employee.Position == null)
            {
                return View();
            }
            else
            {
                try
                {
                    con = new MySqlConnection(DatabaseUtility.ConnectionString);
                    con.Open();
                    transaction = con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                    cmd = new MySqlCommand("INSERT INTO employee(EmployeeName,EmployeeAddress,Position) VALUES('" + employee.EmployeeName + "','" + employee.EmployeeAddress + "','" + employee.Position + "')", con);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();

                    con.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.Message);
                    return View();
                }
                finally
                {
                    transaction.Dispose();
                }

                return RedirectToAction("Index", "EmployeeList");
            }
        }
    }
}