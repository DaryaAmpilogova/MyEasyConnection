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

            string sql = "SELECT NAME, SURNAMES FROM END_USER WHERE ID = 6";

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
                        CurrentUser user = new CurrentUser();
                        dr.Read();

                        user.Name = dr.GetString(0);
                        user.Subname = dr.GetString(1);
                        user.Photo = "/images/senior.png";

                        CurrentUserRS request = new CurrentUserRS
                        {
                            UserRS = user
                        };

                        return request;

                    }
                }
            }

                
        }
    }
}
