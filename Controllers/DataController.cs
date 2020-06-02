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
        public async Task<List<Data>> GetOutput(Data data)
        {
            var results = Task.Run(GetDataFromDb);
            Console.WriteLine(data.Name);
            return await results;
        }

        [HttpPost]
        public void CreateRubiksTid(Result result)
        {
            SendResultToDb(result);
        }

        public void SendResultToDb(Result result)
        {
            var connection = GetConnection();
            var sqlCode = "INSERT INTO times(time, name)" +
                               $"VALUES({result.Time.ToString(CultureInfo.InvariantCulture)}," +
                                    $"\"{result.Name}\")";
            connection.Execute(sqlCode);
        }

        public List<Data> GetDataFromDb()
        {
            var connection = GetConnection();
            var sqlCode = "SELECT * FROM times";
            return connection.Query<Data>(sqlCode).ToList();
        }

        public static MySqlConnection GetConnection()
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