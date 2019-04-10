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

        // SECCIÓ PUNTS
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

        // SECCIÓ CALENDARI

        [HttpPost]
        public Reminder GetReminders()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            string sql = "SELECT NOTE, TITLE, REMINDER_DATE, DESCRIPTION FROM REMINDER WHERE END_USER = 6";

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
                        Reminder reminder = new Reminder();
                        dr.Read();

                        reminder.Note = dr.GetString(0);
                        reminder.Title = dr.GetString(1);
                        reminder.ReminderDate = dr.GetDateTime(2);
                        reminder.Description = dr.GetString(3);

                        return reminder;
                    }
                }
            }
        }
    }
}