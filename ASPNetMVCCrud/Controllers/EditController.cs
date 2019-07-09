using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPNetMVCCrud.Models;
using MySql.Data.MySqlClient;

namespace ASPNetMVCCrud.Controllers
{
    public class EditController : Controller
    {
        // GET: Edit
        private MySqlConnection con;
        private MySqlCommand cmd;
        private MySqlDataReader reader;
        private MySqlDataAdapter adapter;
        private MySqlTransaction transaction;

        [HttpGet]
        public ActionResult Index(string id)
        {
            Employee employee = new Employee();

            if(id == null)
            {
                return RedirectToAction("Index", "EmployeeList");
            }

            try
            {
                con = new MySqlConnection(DatabaseUtility.ConnectionString);
                con.Open();
                cmd = new MySqlCommand("SELECT * FROM employee WHERE ID = " + id, con);
                reader = cmd.ExecuteReader();
                reader.Read();
                employee.ID = reader.GetInt32("ID");
                employee.EmployeeName = reader.GetString("EmployeeName");
                employee.EmployeeAddress = reader.GetString("EmployeeAddress");
                employee.Position = reader.GetString("Position");
                reader.Close();
                return View(employee);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
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

                    cmd = new MySqlCommand("UPDATE employee SET EmployeeName = '" + employee.EmployeeName + "',EmployeeAddress = '" + employee.EmployeeAddress + "',Position = '" + employee.Position + "' WHERE ID  = " + employee.ID, con);
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