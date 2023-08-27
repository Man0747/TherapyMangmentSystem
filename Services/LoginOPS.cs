using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using TherapyMangmentSystem.Models;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using Microsoft.AspNetCore.Hosting.Server;
using System.Security.Cryptography;
using MySql;
namespace TherapyMangmentSystem.Services

{
    public class LoginOPS
    {

        private MySqlConnection connection;
        private void MySqlConnection()
        {
            string ConnectionString = "Server=localhost; Port=3306; Database=therapymanagmentsystem; Uid=root; Pwd=Impetus@123;";
            connection = new MySqlConnection(ConnectionString);
        }
    
        public LoginModel CheckUser (LoginModel loginmodel)
        {
            //LoginModel loginmodel = new LoginModel();
            MySqlConnection();
            using MySqlCommand cmd = new MySqlCommand("Login", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_Login_Id", loginmodel.Login_Id));
            connection.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())

                loginmodel = new LoginModel
                {
                    Login_Id = Convert.IsDBNull(reader["Login_Id"]) ? null : Convert.ToString(reader["Login_Id"]),
                    Password = Convert.IsDBNull(reader["Password"]) ? null : Convert.ToString(reader["Password"]),
                    Role = Convert.IsDBNull(reader["Role"]) ? null : Convert.ToString(reader["Role"]),
                    User_Id = Convert.IsDBNull(reader["User_Id"]) ? 0 : Convert.ToInt32(reader["User_Id"]),

                };
            connection.Close();
            return loginmodel;

        }
        public bool AddUser(LoginModel loginmodel)
        {
            MySqlConnection();
            MySqlCommand cmd = new MySqlCommand("AddUser", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new MySqlParameter("p_Login_Id", loginmodel.Login_Id));
            cmd.Parameters.Add(new MySqlParameter("p_Password", loginmodel.Password));
            cmd.Parameters.Add(new MySqlParameter("p_Role", loginmodel.Role));
            

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i >= 1)
                return true;
            else
                return false;

        }

    }
}
