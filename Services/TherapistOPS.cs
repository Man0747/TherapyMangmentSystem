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
using System.Globalization;
using System.Collections;

namespace TherapyMangmentSystem.Services

{
    public class TherapistOPS
    {
        private MySqlConnection connection;
        private void MySqlConnection()
        {
            string ConnectionString = "Server=localhost; Port=3306; Database=therapymanagmentsystem; Uid=root; Pwd=Impetus@123;";
            connection = new MySqlConnection(ConnectionString);
        }
        public bool AddTherapist(TherapistModel therapistnmodel)
        {
            MySqlConnection();
            MySqlCommand cmd = new MySqlCommand("AddTherapist", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new MySqlParameter("p_Name", therapistnmodel.Name));
            cmd.Parameters.Add(new MySqlParameter("p_BusinessAddress", therapistnmodel.BusinessAddress));
            cmd.Parameters.Add(new MySqlParameter("p_Phone", therapistnmodel.Phone));
            cmd.Parameters.Add(new MySqlParameter("p_Email", therapistnmodel.Email));
            cmd.Parameters.Add(new MySqlParameter("p_ClinicName", therapistnmodel.ClinicName));
            cmd.Parameters.Add(new MySqlParameter("p_IsActive", "yes"));
            cmd.Parameters.Add(new MySqlParameter("p_GeneralPracticeArea", therapistnmodel.GeneralPracticeArea));
            cmd.Parameters.Add(new MySqlParameter("p_SpecialityArea", therapistnmodel.SpecialityArea));
            cmd.Parameters.Add(new MySqlParameter("p_Certifications", therapistnmodel.Certifications));

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i >= 1)
                return true;
            else
                return false;

        }
        public bool DeleteTherapist(int id)
        {
            MySqlConnection();
            using MySqlCommand cmd = new MySqlCommand("DeleteTherapist", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_Therapist_Id", id);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public List<TherapistLoginModel> GetTherapists()
        {
            MySqlConnection();


            List<TherapistLoginModel> therapistlist = new List<TherapistLoginModel>();

            using MySqlCommand cmd = new MySqlCommand("GetTherapistDetails", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                therapistlist.Add(
                    new TherapistLoginModel
                    {
                        Therapist_Id = Convert.IsDBNull(reader["Therapist_Id"]) ? 0 : Convert.ToInt32(reader["Therapist_Id"]),
                        Name = Convert.IsDBNull(reader["Name"]) ? null : Convert.ToString(reader["Name"]),
                        BusinessAddress = Convert.IsDBNull(reader["BusinessAddress"]) ? null : Convert.ToString(reader["BusinessAddress"]),
                        Phone = Convert.IsDBNull(reader["Phone"]) ? null : Convert.ToString(reader["Phone"]),
                        Email = Convert.IsDBNull(reader["Email"]) ? null : Convert.ToString(reader["Email"]),
                        ClinicName = Convert.IsDBNull(reader["ClinicName"]) ? null : Convert.ToString(reader["ClinicName"]),
                        IsActive = Convert.IsDBNull(reader["IsActive"]) ? null : Convert.ToString(reader["IsActive"]),
                        GeneralPracticeArea = Convert.IsDBNull(reader["GeneralPracticeArea"]) ? null : Convert.ToString(reader["GeneralPracticeArea"]),
                        SpecialityArea = Convert.IsDBNull(reader["SpecialityArea"]) ? null : Convert.ToString(reader["SpecialityArea"]),
                        Certifications = Convert.IsDBNull(reader["Certifications"]) ? null : Convert.ToString(reader["Certifications"]),
                        Password = Convert.IsDBNull(reader["Password"]) ? null : Convert.ToString(reader["Password"])

                    });
            }

            connection.Close();
            return therapistlist;
        }
        public TherapistLoginModel GetTherapist(int id)
        {
            MySqlConnection();


            TherapistLoginModel therapistloginmodel = new TherapistLoginModel();

            using MySqlCommand cmd = new MySqlCommand("GetTherapist", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_Therapist_Id", id));
            connection.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                therapistloginmodel = new TherapistLoginModel
                {
                    Therapist_Id = Convert.IsDBNull(reader["Therapist_Id"]) ? 0 : Convert.ToInt32(reader["Therapist_Id"]),
                    Name = Convert.IsDBNull(reader["Name"]) ? null : Convert.ToString(reader["Name"]),
                    BusinessAddress = Convert.IsDBNull(reader["BusinessAddress"]) ? null : Convert.ToString(reader["BusinessAddress"]),
                    Phone = Convert.IsDBNull(reader["Phone"]) ? null : Convert.ToString(reader["Phone"]),
                    Email = Convert.IsDBNull(reader["Email"]) ? null : Convert.ToString(reader["Email"]),
                    ClinicName = Convert.IsDBNull(reader["ClinicName"]) ? null : Convert.ToString(reader["ClinicName"]),
                    IsActive = Convert.IsDBNull(reader["IsActive"]) ? null : Convert.ToString(reader["IsActive"]),
                    GeneralPracticeArea = Convert.IsDBNull(reader["GeneralPracticeArea"]) ? null : Convert.ToString(reader["GeneralPracticeArea"]),
                    SpecialityArea = Convert.IsDBNull(reader["SpecialityArea"]) ? null : Convert.ToString(reader["SpecialityArea"]),
                    Certifications = Convert.IsDBNull(reader["Certifications"]) ? null : Convert.ToString(reader["Certifications"]),
                    Password = Convert.IsDBNull(reader["Password"]) ? null : Convert.ToString(reader["Password"])
                };
            }

            connection.Close();
            return therapistloginmodel;
        }
        public bool UpdateTherapist(TherapistLoginModel therapistloginmodel)
        {
            MySqlConnection();
            using MySqlCommand cmd = new MySqlCommand("UpdateTherapistDetails", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new MySqlParameter("p_Therapist_Id", therapistloginmodel.Therapist_Id));
            cmd.Parameters.Add(new MySqlParameter("p_Name", therapistloginmodel.Name));
            cmd.Parameters.Add(new MySqlParameter("p_BusinessAddress", therapistloginmodel.BusinessAddress));
            cmd.Parameters.Add(new MySqlParameter("p_Phone", therapistloginmodel.Phone));
            cmd.Parameters.Add(new MySqlParameter("p_Email", therapistloginmodel.Email));
            cmd.Parameters.Add(new MySqlParameter("p_ClinicName", therapistloginmodel.ClinicName));
            cmd.Parameters.Add(new MySqlParameter("p_IsActive", therapistloginmodel.IsActive));
            cmd.Parameters.Add(new MySqlParameter("p_GeneralPracticeArea", therapistloginmodel.GeneralPracticeArea));
            cmd.Parameters.Add(new MySqlParameter("p_SpecialityArea", therapistloginmodel.SpecialityArea));
            cmd.Parameters.Add(new MySqlParameter("p_Certifications", therapistloginmodel.Certifications));
            cmd.Parameters.Add(new MySqlParameter("p_Password", therapistloginmodel.Password));

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }



        public List<TherapistSpecialityModel> AddSpecialityInListBox()
        {
            MySqlConnection();


            List<TherapistSpecialityModel> therapistspecialitymodel = new List<TherapistSpecialityModel>();

            using MySqlCommand cmd = new MySqlCommand("GetTherapistSpeciallity", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                therapistspecialitymodel.Add(
                    new TherapistSpecialityModel
                    {
                        Speciallity_Id = Convert.IsDBNull(reader["Speciallity_Id"]) ? 0 : Convert.ToInt32(reader["Speciallity_Id"]),
                        Speciallity_Name = Convert.IsDBNull(reader["Speciallity_Name"]) ? null : Convert.ToString(reader["Speciallity_Name"]),

                    });
            }

            connection.Close();
            return therapistspecialitymodel;
        }
        public List<TherapistPracticeModel> AddPracticeInListBox()
        {
            MySqlConnection();


            List<TherapistPracticeModel> therapistpracticemodel = new List<TherapistPracticeModel>();

            using MySqlCommand cmd = new MySqlCommand("GetTherapistPractice", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                therapistpracticemodel.Add(
                    new TherapistPracticeModel
                    {
                        Practice_Id = Convert.IsDBNull(reader["Practice_Id"]) ? 0 : Convert.ToInt32(reader["Practice_Id"]),
                        Practice_Name = Convert.IsDBNull(reader["Practice_Name"]) ? null : Convert.ToString(reader["Practice_Name"]),

                    });
            }

            connection.Close();
            return therapistpracticemodel;
        }

        public bool AddSchedule(SlotDataModel slotDataModel)
        {
            MySqlConnection();
            MySqlCommand cmd = new MySqlCommand("AddSchedule", connection);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Clear();


            //cmd.Parameters.Add(new MySqlParameter("p_Schedule_Id", scheduleviewmodel.Schedule_Id));
            cmd.Parameters.Add(new MySqlParameter("p_Date", slotDataModel.Date.ToString("yyyy-MM-dd")));

            cmd.Parameters.Add(new MySqlParameter("p_Slot", slotDataModel.Slot));
            cmd.Parameters.Add(new MySqlParameter("p_Therapist_Id", slotDataModel.Therapist_Id));
            cmd.Parameters.Add(new MySqlParameter("p_Isholiday", "false"));

            int k = 0;

            connection.Open();

            //int i = cmd.ExecuteNonQuery();

            using MySqlDataReader reader = cmd.ExecuteReader();

            int Schedule_Id = 0; // Initialize the variable outside the loop

            while (reader.Read())
            {
                Schedule_Id = Convert.IsDBNull(reader["v_Schedule_Id"]) ? 0 : Convert.ToInt32(reader["v_Schedule_Id"]);
            }


            connection.Close();


            MySqlCommand cmd1 = new MySqlCommand("AddScheduleSlots", connection);
            cmd1.CommandType = CommandType.StoredProcedure;

            for (int j = 0; j < slotDataModel.Slots.Count; j++)
            {

                string FromTime = slotDataModel.Slots[j].Split("-")[0].Trim();
                string ToTime = slotDataModel.Slots[j].Split("-")[1].Trim();


                if (DateTime.Parse(FromTime) >= DateTime.Parse("06:00 AM") &&
                    DateTime.Parse(ToTime) <= DateTime.Parse("12:00 PM"))
                {
                    slotDataModel.Shift = "morning";
                }
                else if (DateTime.Parse(FromTime) >= DateTime.Parse("12:00 PM") &&
                    DateTime.Parse(ToTime) <= DateTime.Parse("06:00 PM"))
                {
                    slotDataModel.Shift = "afternoon";
                }
                else if (DateTime.Parse(FromTime) >= DateTime.Parse("06:00 PM") &&
                    DateTime.Parse(ToTime) <= DateTime.Parse("10:45 PM"))
                {
                    slotDataModel.Shift = "evening";
                }


                cmd1.Parameters.Clear();

                cmd1.Parameters.Add(new MySqlParameter("p_FromTime", FromTime));
                cmd1.Parameters.Add(new MySqlParameter("p_ToTime", ToTime));
                cmd1.Parameters.Add(new MySqlParameter("p_Day", slotDataModel.Date.DayOfWeek));
                cmd1.Parameters.Add(new MySqlParameter("p_Shift", slotDataModel.Shift));
                cmd1.Parameters.Add(new MySqlParameter("p_Schedule_Id", Schedule_Id));

                connection.Open();
                k = cmd1.ExecuteNonQuery();
                connection.Close();
            }
            if (k >= 1)
                return true;
            else
                return false;

        }


        public bool IsScheduleExist(DateTime scheduleDate)
        {
            MySqlConnection();
            MySqlCommand cmd = new MySqlCommand("IsScheduleExist", connection);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Clear();

            cmd.Parameters.Add(new MySqlParameter("p_Date", scheduleDate.Date.ToString("yyyy-MM-dd")));


            connection.Open();

            //int i = cmd.ExecuteNonQuery();

            using MySqlDataReader reader = cmd.ExecuteReader();


            DateTime DBDate = DateTime.Now;
            while (reader.Read())
            {
                DBDate = Convert.ToDateTime(reader["Date"]);
            }

            connection.Close();


            if (scheduleDate == DBDate)
            {
                return true;
            }

            else
            {

                return false;
            }
        }

        public List<String> GetSlots(DateTime scheduleDate, int TherapistId)
        {

            MySqlConnection();
            MySqlCommand cmd = new MySqlCommand("GetSlots", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Clear();

            cmd.Parameters.Add(new MySqlParameter("p_Date", scheduleDate.Date.ToString("yyyy-MM-dd")));
            cmd.Parameters.Add(new MySqlParameter("p_Therapist_Id", TherapistId));

            List<String> Slots = new List<String>();

            connection.Open();

            using MySqlDataReader reader = cmd.ExecuteReader();


            TimeSpan DB_FromTime;
            TimeSpan DB_ToTime;

            String FinalTime;
            while (reader.Read())

            {
                DB_FromTime = (TimeSpan)reader["FromTime"];

                DateTime FromTime = DateTime.Today.Add(DB_FromTime);
                string displayFromTime = FromTime.ToString("hh:mm tt");

                DB_ToTime = (TimeSpan)reader["ToTime"];

                DateTime ToTime = DateTime.Today.Add(DB_ToTime);
                string displayToTime = ToTime.ToString("hh:mm tt");

                FinalTime = displayFromTime + " - " + displayToTime;
                Slots.Add(FinalTime);
            }

            return Slots;
        }
        //public bool AddScheduleSlots(SlotDataModel slotDataModel)
        //{
        //    MySqlConnection();
        //    MySqlCommand cmd = new MySqlCommand("AddScheduleSlots", connection);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    int i = 0;
        //    for (int j = 0; j < slotDataModel.Slots.Count; j++)
        //    {

        //        string FromTime = slotDataModel.Slots[j].Split("-")[0].Trim();
        //        string ToTime = slotDataModel.Slots[j].Split("-")[1].Trim();


        //        if (DateTime.Parse(FromTime) >= DateTime.Parse("06:00 AM") &&
        //            DateTime.Parse(ToTime) <= DateTime.Parse("12:00 PM"))
        //        {
        //            slotDataModel.Shift = "morning";
        //        }
        //        else if (DateTime.Parse(FromTime) >= DateTime.Parse("12:00 PM") &&
        //            DateTime.Parse(ToTime) <= DateTime.Parse("06:00 PM"))
        //        {
        //            slotDataModel.Shift = "afternoon";
        //        }
        //        else if (DateTime.Parse(FromTime) >= DateTime.Parse("06:00 PM") &&
        //            DateTime.Parse(ToTime) <= DateTime.Parse("12:00 AM"))
        //        {
        //            slotDataModel.Shift = "evening";
        //        }


        //        cmd.Parameters.Clear();

        //        cmd.Parameters.Add(new MySqlParameter("p_FromTime", FromTime));
        //        cmd.Parameters.Add(new MySqlParameter("p_ToTime", ToTime));
        //        cmd.Parameters.Add(new MySqlParameter("p_Day", slotDataModel.Date.DayOfWeek));
        //        cmd.Parameters.Add(new MySqlParameter("p_Shift", slotDataModel.Shift));


        //        connection.Open();
        //        i = cmd.ExecuteNonQuery();
        //        connection.Close();
        //    }
        //    if (i >= 1)
        //        return true;
        //    else
        //        return false;

        //}
        //public List<ScheduleViewModel> GetScheduleTherapist()
        //{
        //    MySqlConnection();


        //    List<ScheduleViewModel> schedulevievlist = new List<ScheduleViewModel>();

        //    using MySqlCommand cmd = new MySqlCommand("GetScheduleTherapist", connection);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    connection.Open();
        //    using MySqlDataReader reader = cmd.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        schedulevievlist.Add(
        //            new ScheduleViewModel
        //            {
        //                Therapist_Id = Convert.IsDBNull(reader["Therapist_Id"]) ? 0 : Convert.ToInt32(reader["Therapist_Id"]),
        //                Schedule_Id = Convert.IsDBNull(reader["Schedule_Id"]) ? 0 : Convert.ToInt32(reader["Schedule_Id"]),
        //                Date= DateTime.ParseExact(reader["Date"].ToString(), "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
        //                Slot = Convert.IsDBNull(reader["Slot"]) ? 0 : Convert.ToInt32(reader["Slot"]),
        //                Isholiday = Convert.IsDBNull(reader["Isholiday"]) ? null : Convert.ToString(reader["Isholiday"]),
        //                FromTime = DateTime.ParseExact(reader["FromTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
        //                ToTime = DateTime.ParseExact(reader["ToTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
        //                Day = Convert.IsDBNull(reader["Day"]) ? null : Convert.ToString(reader["Day"]),
        //                Shift = Convert.IsDBNull(reader["Shift"]) ? null : Convert.ToString(reader["Shift"])
        //            });
        //    }

        //    connection.Close();
        //    return schedulevievlist;
        //}
    }
}
