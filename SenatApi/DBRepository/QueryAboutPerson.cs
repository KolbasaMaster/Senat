using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace SenatApi
{
    public class QueryAboutPerson
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        string sqlPersonQuery = "SELECT ModelMembers.MemberId, COUNT(ModelMembers.IssueId) AS NumbersOfIssue, " +
            "COUNT (DISTINCT ModelIssues.MeetingId) AS NumbersOfMeeting FROM ModelMembers " +
            "INNER JOIN ModelIssues ON ModelMembers.IssueId = ModelIssues.Id " +
            "GROUP BY ModelMembers.MemberId";
        public void QueryAboutPersons()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlPersonQuery, connection);
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    Console.WriteLine("{0}\t\t\t\t{1}\t\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));
                    while (reader.Read())
                    {
                        object MemberId = reader["MemberId"];
                        object IssueCount = reader.GetValue(1);
                        object MeetingCount = reader.GetValue(2);
                        Console.WriteLine("{0},\t {1},\t\t\t {2}", MemberId, IssueCount, MeetingCount);
                    }
                }
                reader.Close();


            }
            Console.Read();
        }
    }
}
