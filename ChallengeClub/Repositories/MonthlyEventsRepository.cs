using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeClub.Models;
namespace ChallengeClub.Repositories
{
    public class MonthlyEventsRepository
    {
        public readonly IConfiguration configuration;
        public MonthlyEventsRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void AddMonthlyEvent(string title, string date, string description)
        {
            var connectionString = configuration.GetConnectionString("ChallengeClubDB");
            using (var connection = SqlConnectionFactory.GetSqlConnection(connectionString))
            {
                const string query = @"
                    INSERT INTO MonthlyEvents(Title,Date,Description)
                    VALUES(@Title,@Date,@Description)
                ";

                connection.Execute(query, new { Title = title, Date = date, Description = description });
            }
        }
        public IEnumerable<MonthlyEvents> GetMonthlyEvents()
        {
            var connectionString = configuration.GetConnectionString("ChallengeClubDB");
            using (var connection = SqlConnectionFactory.GetSqlConnection(connectionString))
            {
               

                const string query = @"
                    SELECT a.*
                    FROM MonthlyEvents a
                    ORDER BY Date

                ";

                return connection.Query<MonthlyEvents>(query);
            }
        }

        public void DeleteEventById(int eventId)
        {
            var connectionString = configuration.GetConnectionString("ChallengeClubDB");
            using (var connection = SqlConnectionFactory.GetSqlConnection(connectionString))
            {
                const string query = @"
                    DELETE FROM MonthlyEvents
                    WHERE EventId = @EventId
                ";

                connection.Execute(query, new { EventId = eventId });
            }
        }
    }
}