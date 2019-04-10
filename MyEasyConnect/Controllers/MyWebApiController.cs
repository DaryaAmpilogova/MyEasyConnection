using MyEasyConnect.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyEasyConnect.Controllers
{
    public class MyWebApiController : ApiController
    {
        [HttpPost]
        public CurrentUserRS GetCurrentUser()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            string sql = "SELECT NAME, SURNAMES, AVATAR FROM END_USER WHERE ID = 6";

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql.ToString();

                    cmd.CommandType = CommandType.Text;

                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        User user = new User();
                        dr.Read();

                        user.Name = dr.GetString(0);
                        user.Surnames = dr.GetString(1);
                        user.Avatar = (dr.IsDBNull(2)) ? " " : dr.GetString(2);

                        CurrentUserRS request = new CurrentUserRS
                        {
                            UserRS = user
                        };
                        return request;
                    }
                }
            }                
        }

        // Agafar els punts del usuari
        [HttpPost]
        public UserPointsRS GetPoints()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            string sql = "SELECT POINTS FROM END_USER WHERE ID = 6";

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql.ToString();
                    cmd.CommandType = CommandType.Text;

                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        User punctuation = new User();
                        dr.Read();

                        punctuation.Points = dr.GetInt32(0);

                        UserPointsRS point = new UserPointsRS
                        {
                            PointsRS = punctuation
                        };
                        return point;
                    }                    
                }
            }            
        }
    }
}