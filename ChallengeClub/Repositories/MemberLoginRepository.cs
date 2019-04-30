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
            DateTime today = DateTime.Today;
            var connectionString = configuration.GetConnectionString("ChallengeClubDB");
            using (var connection = SqlConnectionFactory.GetSqlConnection(connectionString))
            {

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

                return connection.Query<MemberAttendance>(query, new { LoginDate = today });
            }
        }

        public MemberAttendance GetAttendanceById(int memberId)
        {
            DateTime today = DateTime.Today;
            var connectionString = configuration.GetConnectionString("ChallengeClubDB");
            using (var connection = SqlConnectionFactory.GetSqlConnection(connectionString))
            {

                const string query = @"
                    SELECT 
                       a.*
                    FROM MemberLogin a
                    WHERE a.LoginDate = @LoginDate AND a.MemberId = @MemberId
                    ";
                return connection.QuerySingleOrDefault<MemberAttendance>(query, new { LoginDate = today, MemberId = memberId });
            }
        }
    }
}

