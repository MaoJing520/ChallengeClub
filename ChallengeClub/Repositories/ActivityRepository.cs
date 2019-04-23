using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeClub.Models;

namespace ChallengeClub.Repositories
{
    public class ActivityRepository
    {
        public readonly IConfiguration configuration;

        public ActivityRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<Activity> GetActivities()
        {
            DateTime today = DateTime.Today;
            var connectionString = configuration.GetConnectionString("ChallengeClubDB");
            using (var connection = SqlConnectionFactory.GetSqlConnection(connectionString))
            {
                const string query = @"
                    SELECT a.*
                    FROM Activity a
                    WHERE a.ActivityDate = @ActivityDate
                ";

                return connection.Query<Activity>(query, new { ActivityDate = today });
            }
        }


        public Activity GetActivityById(string id)
        {
            var connectionString = configuration.GetConnectionString("ChallengeClubDB");
            using (var connection = SqlConnectionFactory.GetSqlConnection(connectionString))
            {
                const string query = @"
                    SELECT a.*
                    FROM Activity a
                    WHERE a.ActivityId = @Id
                ";

                return connection.QuerySingleOrDefault<Activity>(query, new { Id = id });
            }
        }

        public void AddActivity(string name, int hours)
        {
            var connectionString = configuration.GetConnectionString("ChallengeClubDB");
            using (var connection = SqlConnectionFactory.GetSqlConnection(connectionString))
            {
                DateTime date = DateTime.Now.Date;
                var description = "x";
                var imagePath = "x";
                const string query = @"
                    INSERT INTO Activity(Name,Duration,ImagePath,Description,Date)
                    VALUES(@Name,@Duration,@ImagePath,@Description,@Date)
                ";

                connection.Execute(query, new { Name = name, Duration = hours, ImagePath = imagePath, Description = description, Date = date });
            }
        }
    }
}
    

