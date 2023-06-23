using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp3.Models;

namespace WpfApp3.Repository
{
    public class Repo
    {
        SqlConnection conn;
        string cs = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
        DataSet set = new DataSet();

        public Repo()
        {
            conn = new SqlConnection();

            using (conn = new SqlConnection())
            {
                var da = new SqlDataAdapter();
                conn.ConnectionString = cs;
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Authors", conn);

                da.SelectCommand = command;
                da.Fill(set, "AuthorsSet");
            }
        }

        public DataSet GetAll()
        {
            return set;
        }

        public void Insert(int id, string firstName, string lastName)
        {
            using (conn = new SqlConnection())
            {
                var command = new SqlCommand("INSERT INTO Authors(Id,FirstName,LastName) VALUES(@id,@firstName,@lastName)", conn);
                conn.ConnectionString = cs;
                conn.Open();

                command.Parameters.Add(new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@id",
                    Value = id
                });

                command.Parameters.Add(new SqlParameter
                {
                    SqlDbType = SqlDbType.NVarChar,
                    ParameterName = "@firstName",
                    Value = firstName
                });
                
                command.Parameters.Add(new SqlParameter
                {
                    SqlDbType = SqlDbType.NVarChar,
                    ParameterName = "@lastName",
                    Value = lastName
                });

                var da = new SqlDataAdapter();
                da.InsertCommand= command;
                da.InsertCommand.ExecuteNonQuery();
                da.Update(set, "AuthorsSet");
                set.Clear();

                da = new SqlDataAdapter("SELECT * FROM Authors", conn);

                da.Fill(set, "AuthorsSet");


            }
        }
    }
}
