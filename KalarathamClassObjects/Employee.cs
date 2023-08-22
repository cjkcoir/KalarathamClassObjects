using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace KalarathamClassObjects
{
    internal class Employee
    {
        // declaring variable as per Employee table in the database
        public int employeeId { get; set; }
        public string employeeName { get; set; }

        public string Gender { get; set; }

        public int salary { get; set; }

        // declare sqlconnection variable
        public SqlConnection con;

        // declare sqlcommand variable
        public SqlCommand cmd;

        // creating empty constructor
        public Employee()
        {
            con=new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=kalaratham;Integrated Security=True");
        }


        // constructor with variables
        public Employee(int employeeId, string employeeName, string gender, int salary)
        {
            con=new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=kalaratham;Integrated Security=True");
            this.employeeId = employeeId;
            this.employeeName = employeeName;
            Gender = gender;
            this.salary = salary;
        }


        // method to insert a data into db
        public void InsertEmployee(   ) 
        {
            try
            {
                con.Open();
                string sql = $"insert into employee values({employeeId},'{employeeName}','{Gender}',{salary})";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {

                Console.WriteLine( e.ToString());
            }

            finally 
            
            { 
                con.Close();
            
            }
            
            
        
        }

        // method to update salary
        public void UpdateSalary(int id, int salary)
        {
            con.Open();
            string sql = $"update employee set salary ={salary} where id = {id}";
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        // method to delete Employee
        public void DeleteEmployee(int id)
        {
            con.Open();
            string sql = $"delete from employee  where id = {id}";
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        // method to retrieve datas from the table


        public List<Employee> GetEmployees() 
        
        {
            List<Employee> emplist = new List<Employee>();

            con.Open() ;
            string sql = "select * from employee";
            cmd= new SqlCommand(sql, con);

            SqlDataReader dr= cmd.ExecuteReader();
            while(dr.Read())
            {
                Employee employ = new Employee();
                employ.employeeId = int.Parse(dr[0].ToString());
                employ.employeeName= dr[1].ToString();
                employ.Gender= dr[2].ToString();
                employ.salary = int.Parse(dr[3].ToString());
                emplist.Add(employ);

            }


            return emplist;
        
        



        
        }


        static void Main(string[] args)
        {

            Employee e2 = new Employee() { employeeId = 23,employeeName="Roshini",Gender="female",salary=23500 };
            e2.InsertEmployee();

        }
    }
}
