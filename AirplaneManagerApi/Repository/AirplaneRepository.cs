using System;
using Microsoft.AspNetCore.Mvc;
using projekt.Model;
using Microsoft.Extensions.Configuration;
using projekt.Database;
using Npgsql;
using Dapper;

namespace projekt.Repository
{
    public class AirplaneRepository : IAirplaneRepository
    {
        private readonly DatabaseConfig dbConf;
        public AirplaneRepository(DatabaseConfig dbConf)
        {
            this.dbConf = dbConf;
        }

        public async Task<(string?, IEnumerable<IAirplane>?)> Get()
        {
            
            try
            {
                using (var conn = new NpgsqlConnection(dbConf.Name))
                {
                    return (null, await conn.QueryAsync<IAirplane>("SELECT \"Id\", \"Name\", \"Description\", \"Stars\", \"Img\" FROM \"Airplanes\" ORDER BY \"Id\""));
                }
            }
            catch (Exception ex)
            {
                if (ex is PostgresException) return (((PostgresException)ex).SqlState, null);
                return (ex.Message, null);
            }

            
        }

        public async Task<(string?, IAirplane?)> Get(Guid Id)
        {
            
            try
            {
                using (var conn = new NpgsqlConnection(dbConf.Name))
                {
                    var airplane = (await conn.QueryAsync<IAirplane>("SELECT \"Id\", \"Name\", \"Description\", \"Stars\", \"Img\" FROM \"Airplanes\" WHERE \"Id\"= @Id", new { Id })).FirstOrDefault();
                    return (null, airplane);
                }
            }
            catch (Exception ex)
            {
                if (ex is PostgresException) return (((PostgresException)ex).SqlState, null);
                return (ex.Message, null);
            }

            
        }

        public async Task<(string?, IEnumerable<IAirplane>?)> Search(string keyword)
        {

            try
            {
                using (var conn = new NpgsqlConnection(dbConf.Name))
                {
                    return (null, await conn.QueryAsync<IAirplane>($"SELECT \"Id\", \"Name\", \"Description\", \"Stars\", \"Img\" FROM \"Airplanes\" where to_tsvector(\"Airplanes\"::text) @@ to_tsquery('{keyword}')"));
                }
                    
            }
             
            catch (Exception ex)
            {
                if (ex is PostgresException) return (((PostgresException)ex).ToString(), null);
                return (ex.Message, null);
            }

         
        }

        public async Task<string?> Post(IAirplane airplane)
        {
            try
            {
                using (var conn = new NpgsqlConnection(dbConf.Name))
                {
                    await conn.ExecuteAsync("INSERT INTO \"Airplanes\" (\"Name\", \"Description\", \"Stars\", \"Img\") VALUES(@Name, @Description, @Stars, @Img)", airplane);
                    return null;
                }
            }
            catch (Exception ex)
            {
                if(ex is PostgresException) return ((PostgresException)ex).SqlState;
                return ex.Message;
            }
        }


        public async Task<string?> Update(IAirplane airplane)
        {
           
            try
            {
                using (var conn = new NpgsqlConnection(dbConf.Name))
                {
                    await conn.ExecuteAsync("UPDATE \"Airplanes\" SET \"Name\"=@Name, \"Description\"=@Description, \"Stars\"=@Stars, \"Img\"=@Img WHERE \"Id\"=@Id", airplane);
                    return null;
                }
                    
            }
            catch(Exception ex)
            {
                if (ex is PostgresException) return ((PostgresException)ex).SqlState;
                return ex.Message;
            }
            
        }

        public async Task<string?> Delete(Guid Id)
        {
            try
            {
                using (var conn = new NpgsqlConnection(dbConf.Name))
                {
                    await conn.ExecuteAsync("DELETE FROM \"Airplanes\" WHERE \"Id\"= @Id", new { Id = Id });
                    return null;
                }    
                
            }
            catch (Exception ex)
            {
                if (ex is PostgresException) return ((PostgresException)ex).SqlState;
                return ex.Message;
            }

        }

    }
}

