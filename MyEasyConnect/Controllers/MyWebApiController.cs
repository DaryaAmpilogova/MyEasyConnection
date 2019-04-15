using MyEasyConnect.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace MyEasyConnect.Controllers
{
    public class MyWebApiController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        // Submenú
        [HttpPost]
        public CurrentUserRS GetCurrentUser()
        {
            string sql = "SELECT NAME, SURNAMES, AVATAR FROM END_USER WHERE ID = 6";

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;

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
            string sql = "SELECT POINTS FROM END_USER WHERE ID = 6";

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
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
        public ReminderRS GetReminders()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            string sql = "SELECT DESCRIPTION, REMINDER_DATE, TITLE, NOTE, ADDRESS, PHONE_NUMBER FROM REMINDER WHERE END_USER = 6";

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;

                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        Reminder reminder = new Reminder();
                        dr.Read();

                        reminder.Description = dr.GetString(0);
                        reminder.ReminderDate = dr.GetDateTime(1).ToString("", CultureInfo.InvariantCulture);
                        reminder.Title = dr.GetString(2);
                        reminder.Note = dr.GetString(3);
                        reminder.Address = dr.GetString(4);
                        reminder.PhoneNumber = dr.GetString(5);

                        ReminderRS memory = new ReminderRS()
                        {
                            Memory = reminder
                        };
                        return memory;
                    }
                }
            }
        }

        //Mensages
        [HttpPost]
        public GetMessagesRS GetMessages()
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT SUBJECT, ");
            sql.Append("       CONTENT, ");
            sql.Append("       SEND_DATE, ");
            sql.Append("       NAME, ");
            sql.Append("       SURNAMES, ");
            sql.Append("       STATE ");
            sql.Append("  FROM END_USER_MESSAGE  E ");
            sql.Append("       JOIN MESSAGE M ON E.MESSAGE = M.ID ");
            sql.Append("       JOIN END_USER U ON U.ID = M.SENDER ");
            sql.Append(" WHERE ADDRESSEE = 6 ");
            sql.Append(" ORDER BY M.SEND_DATE DESC");

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
                        List<Message> mensajes = new List<Message>();
                        while (dr.Read())
                        {
                            Message message = new Message();
                            User user = new User();

                            message.Subject = dr.GetString(0);
                            message.Content = dr.GetString(1);
                            message.Date = dr.GetDateTime(2);
                            user.Name = dr.GetString(3);
                            user.Surnames = dr.GetString(4);
                            message.Sender = user;
                            message.State = dr.GetInt32(5);
                            mensajes.Add(message);

                        }
                        GetMessagesRS request = new GetMessagesRS();
                        request.Messages = mensajes;
                        return request;
                    }
                }
            }
        }

        //Circle of care
        [HttpPost]
        public GetCircleOfCareRS GetCircleOfCare()
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT NAME, SURNAMES, PROFESSION ");
            sql.Append("  FROM END_USER  U ");
            sql.Append("       JOIN END_USER_CIRCLE_OF_CARE EC ON USER_ID = U.ID ");
            sql.Append("       JOIN CIRCLE_OF_CARE C ON CIRCLE_OF_CARE_ID = C.ID ");
            sql.Append("       WHERE OWNER = 6 ");

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
                        List<User> users = new List<User>();
                        while (dr.Read())
                        {
                            User user = new User();
                            
                            user.Name = dr.GetString(0);
                            user.Surnames = dr.GetString(1);
                            user.Profession = dr.GetString(2);

                            users.Add(user);
                        }
                        GetCircleOfCareRS request = new GetCircleOfCareRS();
                        request.Users = users;
                        return request;
                    }
                }
            }
        }
    }
}