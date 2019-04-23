using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeClub.Models;

namespace ChallengeClub.Repositories
{
    public class ActivityDefinitionRepositories
    {
        public readonly IConfiguration configuration;
        public ActivityDefinitionRepositories(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void AddActivityDefinition(string name, int hours, string description)
        {
            var connectionString = configuration.GetConnectionString("ChallengeClubDB");
            using (var connection = SqlConnectionFactory.GetSqlConnection(connectionString))
            {

                const string query = @"
                    INSERT INTO ActivityDefinition(Name,Hours,Description)
                    VALUES(@Name,@Hours,@Description)
                ";

                connection.Execute(query, new { Name = name, Hours = hours, Description = description });
            }
        }

        public IEnumerable<EmployeeActivityDefinition> GetActivityDefinition()
        {
            var connectionString = configuration.GetConnectionString("ChallengeClubDB");
            using (var connection = SqlConnectionFactory.GetSqlConnection(connectionString))
            {
                const string query = @"
                    SELECT a.*
                    FROM ActivityDefinition a
                ";

                return connection.Query<EmployeeActivityDefinition>(query);
            }
        }

        public IEnumerable<EmployeeActivityDefinition> GetActivityDefinitionByName(string name)
        {
            var connectionString = configuration.GetConnectionString("ChallengeClubDB");
            using (var connection = SqlConnectionFactory.GetSqlConnection(connectionString))
            {
                const string query = @"
                    SELECT a.*
                    FROM ActivityDefinition a
                    WHERE a.Name = @Name
                ";

                return connection.Query<EmployeeActivityDefinition>(query, new { Name = name });
            }
        }
    }
}

