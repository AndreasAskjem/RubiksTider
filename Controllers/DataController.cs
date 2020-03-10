using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RubiksTider.Model;
using System.Collections.Generic;
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
        public double CreateRubiksTid(Data rubikstid)
        {
            return rubikstid.Time = 34.45d;
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