using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeClub.Models;

namespace ChallengeClub.Repositories
{
    public class MemberActivityRepository
    {
        public readonly IConfiguration configuration;

        public MemberActivityRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<MemberActivity> GetMemberActivities()
        {
            var connectionString = configuration.GetConnectionString("ChallengeClubDB");
            using (var connection = SqlConnectionFactory.GetSqlConnection(connectionString))
            {
                const string query = @"
                    SELECT a.*
                    FROM MemberActivity a
                ";

                return connection.Query<MemberActivity>(query);
            }
        }

        public MemberActivity GetMemberActivityById(string id)
        {
            var connectionString = configuration.GetConnectionString("ChallengeClubDB");
            using (var connection = SqlConnectionFactory.GetSqlConnection(connectionString))
            {
                const string query = @"
                    SELECT a.*
                    FROM MemberActivity a
                    WHERE a.MemberActivityId = @Id
                ";

                return connection.QuerySingleOrDefault<MemberActivity>(query, new { Id = id });
            }
        }


        public void CreateMemberActivity(int memberId, int activityId)
        {

            DateTime today = DateTime.Today;
            var connectionString = configuration.GetConnectionString("ChallengeClubDB");

            using (var connection = SqlConnectionFactory.GetSqlConnection(connectionString))
            {
                const string query = @"
                    INSERT INTO MemberActivity (MemberId, ActivityId, ActivityDate)
                    VALUES (@MemberId, @ActivityId, @ActivityDate)
                ";

                connection.Execute(query, new { MemberId = memberId, ActivityId = activityId, ActivityDate = today });
            }
        }

        public void DeleteMemberActivity(int memberActivityId)
        {
            var connectionString = configuration.GetConnectionString("ChallengeClubDB");
            using (var connection = SqlConnectionFactory.GetSqlConnection(connectionString))
            {
                const string query = @"
                    DELETE MemberActivity
                    WHERE MemberActivityId = @id;
                ";

                connection.Execute(query, new { Id = memberActivityId });
            }
        }

    }
}
