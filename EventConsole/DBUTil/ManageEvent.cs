using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TCPServer;

namespace EventConsole.DBUTil
{
    public class ManageEvent
    {
        private const string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Event;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string GET_ALL_SQL = "select * from Event";
        public IList<BaseClass> HentAlle()
        {
            IList<BaseClass> events = new List<BaseClass>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GET_ALL_SQL, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        events.Add(ReadNextEvent(reader));
                    }
                }
            }
            return events;

        }
        private const string INSERT_SQL = "insert into Event(EventConsoles) values (@EventConsoles)";
        public bool Opret(BaseClass sensor)
        {
            bool OK = true;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(INSERT_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@EventConsoles", sensor.EventConsoles);

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        OK = rows == 1;
                    }
                    catch (Exception ex)
                    {
                        OK = false;
                    }
                }
            }
            return OK;
        }
        private BaseClass ReadNextEvent(SqlDataReader reader)
        {
            
            BaseClass bc = new BaseClass();

            bc.EventConsoles = reader.GetString(0);
            
            return bc;
        }
    }
}
