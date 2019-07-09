using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using ASPNetMVCCrud.Models;
using System.Web.Configuration;

namespace ASPNetMVCCrud.Controllers
{
    public class EmployeeListController : Controller
    {
        // GET: EmployeeList
        private MySqlConnection con;
        private MySqlCommand cmd;
        private MySqlDataReader reader;
        private MySqlDataAdapter adapter;
        private MySqlTransaction transaction;
        public ActionResult Index()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                con = new MySqlConnection(DatabaseUtility.ConnectionString);
                con.Open();
                cmd = new MySqlCommand("SELECT * FROM employee",con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new Employee();
                    employee.ID = reader.GetInt32("ID");
                    employee.EmployeeName = reader.GetString("EmployeeName");
                    employee.EmployeeAddress = reader.GetString("EmployeeAddress");
                    employee.Position = reader.GetString("Position");
                    employees.Add(employee);
                }
                reader.Close();
                con.Close();
                return View(employees);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View();
            }
        }
        public ActionResult Delete(string id)
        {
            try
            {
                con = new MySqlConnection(DatabaseUtility.ConnectionString);
                con.Open();
                transaction = con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                cmd = new MySqlCommand("DELETE FROM employee WHERE ID = " + id, con);
                cmd.ExecuteNonQuery();
                transaction.Commit();

                con.Close();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex.Message);
            }
            finally
            {
                transaction.Dispose();
            }

            return RedirectToAction("Index", "EmployeeList");
        }
    }
}