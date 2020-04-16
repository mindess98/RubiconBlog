using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using RubiconBloggingApi.Models;

namespace RubiconBloggingApi.Repositories
{
    public class DapperTagRepository : ITagRepository
    {
        private IConfiguration configuration;
        public DapperTagRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public List<Tag> GetTags()
        {
            using (IDbConnection connection = new SqlConnection(GetDefaultConnectionString()))
            {
                string sql = @"SELECT * FROM Tags";

                var tags = connection.Query<Tag>(sql);

                return tags.ToList();
            }
        }

        private string GetDefaultConnectionString()
        {
            return configuration.GetConnectionString("DefaultConnection");
        }
    }
}
