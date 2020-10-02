using EMS.Models;
using EMS.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EMS.DataAccessLayer
{
    public class EmployeeADO : IEmployeeRepository
    {
        // Connection String
        public string connectionString = "Your Connection String Here";

        // Delete a particular employee in Database
        Result IEmployeeRepository.DeleteEmployee(int empid)
        {
            Result result = new Result()
            {
                Success = false,
                Message = "Employee Record Update Failed!"
            };

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("EMS_SP_DELETE_EMPLOYEE", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EMPLOYEEID", empid);

                    connection.Open();
                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
                    {
                        result.Success = true;
                        result.Message = "Employee Record Updated!";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        // Get a particular employee from database
        Employee IEmployeeRepository.GetEmployee(int empid)
        {
            Employee employee = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("EMS_SP_GET_PARTICULAR_EMPLOYEE", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EMPLOYEEID", empid);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        employee = new Employee()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["NAME"].ToString(),
                            Department = reader["DEPARTMENT"].ToString(),
                            City = reader["CITY"].ToString(),
                            Gender = reader["GENDER"].ToString()
                        };
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return employee;
        }

        // Get all active employees from database
        List<Employee> IEmployeeRepository.GetEmployees()
        {
            List<Employee> lstEmployees = new List<Employee>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("EMS_SP_GET_ALL_EMPLOYEES", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee empObj = new Employee()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["NAME"].ToString(),
                            Gender = reader["GENDER"].ToString(),
                            Department = reader["DEPARTMENT"].ToString(),
                            City = reader["CITY"].ToString()
                        };

                        lstEmployees.Add(empObj);
                        empObj = null;
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstEmployees;
        }

        // Save employee to Database
        Result IEmployeeRepository.SaveEmployee(Employee employee)
        {
            Result result = new Result()
            {
                Success = false,
                Message = "Failed!"
            };

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("EMS_SP_SAVE_EMPLOYEE", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NAME", employee.Name);
                    cmd.Parameters.AddWithValue("@CITY", employee.City);
                    cmd.Parameters.AddWithValue("@DEPARTMENT", employee.Department);
                    cmd.Parameters.AddWithValue("@GENDER", employee.Gender);

                    connection.Open();
                    int count = cmd.ExecuteNonQuery();
                    connection.Close();

                    if (count > 0)
                    {
                        result.Success = true;
                        result.Message = "Employee Created";
                    }

                };
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }

        // Update employee record in database
        Result IEmployeeRepository.UpdateEmployee(Employee employee)
        {
            Result result = new Result()
            {
                Success = false,
                Message = "Update Failed!"
            };

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("EMS_SP_UPDATE_EMPLOYEE", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EMPLOYEEID", employee.ID);
                    cmd.Parameters.AddWithValue("@NAME", employee.Name);
                    cmd.Parameters.AddWithValue("@GENDER", employee.Gender);
                    cmd.Parameters.AddWithValue("@DEPARTMENT", employee.Department);
                    cmd.Parameters.AddWithValue("@CITY", employee.City);

                    con.Open();
                    int count = cmd.ExecuteNonQuery();
                    con.Close();

                    if (count > 0)
                    {
                        result.Success = true;
                        result.Message = "Employee Record Updated!";
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }
    }
}
