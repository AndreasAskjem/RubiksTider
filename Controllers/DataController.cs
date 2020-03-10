using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RubiksTider.Model;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;


namespace RubiksTider.Controllers
{
    [Route("api/Data")]
    [ApiController]
    public class DataController : ControllerBase
    {


        [HttpGet]
        public async Task<List<Data>> GetOutput()
        {
            var results = Task.Run(GetDataFromDb);
            return await results;
        }

        [HttpPost]
        public void CreateRubiksTid(Result result)
        {
            //double result = 25.11;
            SendResultToDb(result);

            //return result;
        }

        private void SendResultToDb(Result result)
        {
            var connection = GetConnection();
            var sqlCode = "INSERT INTO times(Time)" +
                               $"VALUES({result.Time.ToString(CultureInfo.InvariantCulture)})";
            connection.Execute(sqlCode);
        }

        public List<Data> GetDataFromDb()
        {
            var connection = GetConnection();
            var sqlCode = "SELECT * FROM times";
            return connection.Query<Data>(sqlCode).ToList();
        }

        private static MySqlConnection GetConnection()
        {
            var connectionString =
                "Server=localhost;" +
                "Database=rubikstimes;" +
                "User=Andreas;" +
                "Port=3300;" +
                "Password=1234;";

            return new MySqlConnection(connectionString);
        }
    }
}