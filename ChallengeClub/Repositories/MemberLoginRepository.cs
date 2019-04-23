using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeClub.Models;
namespace ChallengeClub.Repositories
{
    public class MemberLoginRepository
    { 
        public readonly IConfiguration configuration;
           public MemberLoginRepository(IConfiguration configuration)
            {
                this.configuration = configuration;
            }
            public IEnumerable<MemberAttendance> GetAttendanceByDate()
        {
            var connectionString = configuration.GetConnectionString("ChallengeClubDB");
            using (var connection = SqlConnectionFactory.GetSqlConnection(connectionString))
            {
                //DateTime date = DateTime.Now.Date; 
                //string loginDate = date.ToString("MM-dd-yyyy");

                var loginDate = "04-18-2019";

                

                const string query = @"
                    SELECT 
                        m.MemberId,
                        a.MemberLoginId,
                        a.LoginDate,
                        m.Name
                    FROM MemberLogin a
                    JOIN Member m on a.MemberId = m.MemberId
                    WHERE a.LoginDate = @LoginDate
                ";

                return connection.Query<MemberAttendance>(query, new { LoginDate = loginDate });
            }
        }
    }
}
