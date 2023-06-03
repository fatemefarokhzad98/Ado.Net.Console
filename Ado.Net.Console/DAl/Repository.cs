using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Ado.Net.Console.DAl
{
    internal class Repository
    {
        private const string ConnectionString = "Server=DESKTOP-CGR2LP5\\MSSQLSERVER2022;Initial Catalog=studentdb;Integrated Security=True;TrustServerCertificate=True";

      public void PritAllStudent()
        {
            using(SqlConnection con=new SqlConnection(ConnectionString))
            {
                try
                {

                    con.Open();
                    SqlCommand command = con.CreateCommand();
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = " select*from Students";
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        System.Console.WriteLine($"id:{reader["StudentId"]} name:{reader["Name"]} family:{reader["Family"]} age:{reader["age"] } Gender:{ reader["GenderId"]}");





                    }

                        con.Close();
                    } 
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    con.Close();
                }




            }




        }  
        public void AddStudent(string Name, string Family, int Age, int Genderid)
        {
            int Id = 0;
            using (SqlConnection cn= new SqlConnection(ConnectionString))
            {
                try
                {
                    cn.Open();
                    var query = "insert into Students (Name,Family,age,GenderId) values(@name,@family,@age,@genderid);select SCOPE_IDENTITY()";

                    using (SqlCommand command = new SqlCommand(query, cn))
                    {
                        command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = Name;
                        command.Parameters.Add("@family", System.Data.SqlDbType.NVarChar).Value = Family;
                        command.Parameters.Add("@age", System.Data.SqlDbType.Int).Value = Age;
                        command.Parameters.Add("@genderid", System.Data.SqlDbType.Int).Value = Genderid;
                        var result = command.ExecuteScalar();
                        Id = Int32.Parse(result.ToString());
                    }
                    cn.Close();
                    System.Console.WriteLine($"دانش اموزی با ایدی جدید بع شماره{Id} ");
                }
                catch (Exception ex )
                {
                    throw ex;
                }   
                finally
                {
                    cn.Close();
                }




            }
          
            




        }

        public List<Students> GetStudent()
        {
            List<Students> students = new List<Students>();
            
            using(SqlConnection cn=new SqlConnection(ConnectionString)){
                
               
                try
                {
                    cn.Open();
                    SqlCommand command = cn.CreateCommand();
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "select *from Students";
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Students st = new();
                        st.IdStudent = Int32.Parse(reader["StudentId"].ToString());
                        st.age= Int32.Parse(reader["age"].ToString());
                        st.GenderId= Int32.Parse(reader["GenserId"].ToString());
                        st.Name = reader["Name"].ToString();
                        st.Family = reader["Family"].ToString();
                        students.Add(st);

                    }
                    cn.Close();

                    return students;


                }
                catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cn.Close();
                }



                }




        }
        public List<Students> GetStudentsWhitDaper()
        {
            var query = "select *from Students";
            using(SqlConnection cn=new SqlConnection(ConnectionString))
            {
                var result = cn.Query<Students>(query).ToList();
                return result;


            }




        }



    }
}
