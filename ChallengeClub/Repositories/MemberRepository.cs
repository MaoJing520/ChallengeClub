﻿using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ChallengeClub.Repositories
{
    public class Member
    {
        public string MemberNumber { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }
    }

    public class MemberRepository
    {
        public readonly IConfiguration configuration;
        public MemberRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public List<Member> GetMembers()
        {
            throw new System.NotImplementedException();
        }

        public Member GetMemberById(string memberNumber)
        {
            var connectionString = configuration.GetConnectionString("ChallengeClubDB");
            using (var connection = SqlConnectionFactory.GetSqlConnection(connectionString))
            {
                const string query = @"
                    SELECT 
<<<<<<< HEAD
                        MemberNumber,
                        Name,
                        IconPath
                    FROM dbo.Member
                    WHERE MemberNumber = @memberNumber";

                var member = connection.QuerySingleOrDefault<Member>(query, new { MemberNumber = memberNumber});
=======
                        m.MemberNumber,
                        m.Name
                    FROM dbo.Member m
                    WHERE m.MemberNumber = @MemberNumber";

                var member = connection.QuerySingleOrDefault<Member>(query, new { MemberNumber = memberId });
>>>>>>> 3b54639247ddc161625153995426b83f33240982

                return member;
            }
        }
    }
}
