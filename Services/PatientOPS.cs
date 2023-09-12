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
    public class PatientOPS
    {
        private MySqlConnection connection;
        private void MySqlConnection()
        {
            string ConnectionString = "Server=localhost; Port=3306; Database=therapymanagmentsystem; Uid=root; Pwd=Impetus@123;";
            connection = new MySqlConnection(ConnectionString);
        }
        
        
        public bool AddPatient(PatientModel patientmodel)
        {
            MySqlConnection();
            MySqlCommand cmd = new MySqlCommand("AddPatient", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new MySqlParameter("p_Name", patientmodel.Name));
            cmd.Parameters.Add(new MySqlParameter("p_DOB", patientmodel.DOB.ToString("yyyy-MM-dd")));
            cmd.Parameters.Add(new MySqlParameter("p_Gender", patientmodel.Gender));
            cmd.Parameters.Add(new MySqlParameter("p_Address", patientmodel.Address));
            cmd.Parameters.Add(new MySqlParameter("p_Phone", patientmodel.Phone));
            cmd.Parameters.Add(new MySqlParameter("p_Email", patientmodel.Email));
            cmd.Parameters.Add(new MySqlParameter("p_IsActive", "yes"));

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i >= 1)
                return true;
            else
                return false;

        }
        public bool DeletePatient(int id)
        {
            MySqlConnection();
            using MySqlCommand cmd = new MySqlCommand("DeletePatient", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_Patient_Id", id);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public List<PatientModel> GetPatients()
        {
            MySqlConnection();


            List<PatientModel> patientlist = new List<PatientModel>();

            using MySqlCommand cmd = new MySqlCommand("GetPatientDetails", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                patientlist.Add(
                    new PatientModel
                    {
                        Patient_Id = Convert.IsDBNull(reader["Patient_Id"]) ? 0 : Convert.ToInt32(reader["Patient_Id"]),
                        Name = Convert.IsDBNull(reader["Name"]) ? null : Convert.ToString(reader["Name"]),
                        DOB =  Convert.ToDateTime(reader["DOB"]),
                        Gender = Convert.IsDBNull(reader["Gender"]) ? null : Convert.ToString(reader["Gender"]),
                        Address = Convert.IsDBNull(reader["Address"]) ? null : Convert.ToString(reader["Address"]),
                        Phone = Convert.IsDBNull(reader["Phone"]) ? null : Convert.ToString(reader["Phone"]),
                        Email = Convert.IsDBNull(reader["Email"]) ? null : Convert.ToString(reader["Email"]),                     
                        IsActive = Convert.IsDBNull(reader["IsActive"]) ? null : Convert.ToString(reader["IsActive"])
                        
                       
                    });
            }

            connection.Close();
            return patientlist;
        }
        public PatientModel GetPatient(int id)
        {
            MySqlConnection();


            PatientModel patientmodel = new PatientModel();

            using MySqlCommand cmd = new MySqlCommand("GetPatient", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_Patient_Id", id));
            connection.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                patientmodel = new PatientModel
                {
                    Patient_Id = Convert.IsDBNull(reader["Patient_Id"]) ? 0 : Convert.ToInt32(reader["Patient_Id"]),
                    Name = Convert.IsDBNull(reader["Name"]) ? null : Convert.ToString(reader["Name"]),
                    DOB = Convert.ToDateTime(reader["DOB"]),
                    Gender = Convert.IsDBNull(reader["Gender"]) ? null : Convert.ToString(reader["Gender"]),
                    Address = Convert.IsDBNull(reader["Address"]) ? null : Convert.ToString(reader["Address"]),
                    Phone = Convert.IsDBNull(reader["Phone"]) ? null : Convert.ToString(reader["Phone"]),
                    Email = Convert.IsDBNull(reader["Email"]) ? null : Convert.ToString(reader["Email"]),
                    IsActive = Convert.IsDBNull(reader["IsActive"]) ? null : Convert.ToString(reader["IsActive"])
                };
            }

            connection.Close();
            return patientmodel;
        }
        public bool UpdatePatient(PatientModel patientmodel)
        {
            MySqlConnection();
            using MySqlCommand cmd = new MySqlCommand("UpdatePatientDetails", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new MySqlParameter("p_Patient_Id", patientmodel.Patient_Id));
            cmd.Parameters.Add(new MySqlParameter("p_Name", patientmodel.Name));
            cmd.Parameters.Add(new MySqlParameter("p_DOB", patientmodel.DOB));
            cmd.Parameters.Add(new MySqlParameter("p_Gender", patientmodel.Gender));
            cmd.Parameters.Add(new MySqlParameter("p_Address", patientmodel.Address));
            cmd.Parameters.Add(new MySqlParameter("p_Phone", patientmodel.Phone));
            cmd.Parameters.Add(new MySqlParameter("p_Email", patientmodel.Email));
            cmd.Parameters.Add(new MySqlParameter("p_IsActive", patientmodel.IsActive));
           
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }


        
        public List<TherapistModel> Search(TherapistModel therapistModel)
        {
            MySqlConnection();


            List<TherapistModel> therapistlist = new List<TherapistModel>();
            using MySqlCommand cmd = new MySqlCommand("Search", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new MySqlParameter("p_TherapistName", therapistModel.Name));
            

            connection.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                therapistlist.Add(
                    new TherapistModel
                    {
                        Therapist_Id = Convert.IsDBNull(reader["Therapist_Id"]) ? 0 : Convert.ToInt32(reader["Therapist_Id"]),
                        Name = Convert.IsDBNull(reader["Name"]) ? null : Convert.ToString(reader["Name"]),
                        
                        Phone = Convert.IsDBNull(reader["Phone"]) ? null : Convert.ToString(reader["Phone"]),
                        Email = Convert.IsDBNull(reader["Email"]) ? null : Convert.ToString(reader["Email"]),
                       
                        GeneralPracticeArea = Convert.IsDBNull(reader["GeneralPracticeArea"]) ? null : Convert.ToString(reader["GeneralPracticeArea"]),
                        SpecialityArea = Convert.IsDBNull(reader["SpecialityArea"]) ? null : Convert.ToString(reader["SpecialityArea"]),
                        

                    });
            }

            connection.Close();
            return therapistlist;
        }




    }

}
