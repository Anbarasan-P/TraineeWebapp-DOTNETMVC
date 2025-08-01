﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TraineeWebapp_DOTNETMVC.Models
{
    public class TraineeDAL
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<Trainee> GetAllTrainees()
        {
            List<Trainee> list = new List<Trainee>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("usp_GetAllTrainees", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Trainee
                        {
                            TraineeID = Convert.ToInt32(reader["TraineeID"]),
                            Name = reader["Name"].ToString().Trim(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Department = reader["Department"].ToString(),
                            JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                            Gender = reader["Gender"].ToString(),
                            Photo = reader["Photo"] as byte[]
                        });
                    }
                }
            }
            return list;
        }

        public Admin ValidateAdmin(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Admins WHERE Email=@Email AND Password=@Password", connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Admin
                        {
                            AdminID = Convert.ToInt32(reader["AdminID"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        public Trainee ValidateTrainee(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Trainees WHERE Email=@Email AND Password=@Password", connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Trainee
                        {
                            TraineeID = Convert.ToInt32(reader["TraineeID"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        public void InsertTrainee(Trainee trainee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("usp_InsertTrainee", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", trainee.Name);
                command.Parameters.AddWithValue("@Email", trainee.Email);
                command.Parameters.AddWithValue("@PhoneNumber", trainee.PhoneNumber);
                command.Parameters.AddWithValue("@Department", trainee.Department);
                command.Parameters.AddWithValue("@JoiningDate", trainee.JoiningDate);
                command.Parameters.AddWithValue("@Gender", trainee.Gender);
                if (trainee.Photo != null)
                    command.Parameters.Add("@Photo", SqlDbType.VarBinary).Value = trainee.Photo;
                else
                    command.Parameters.Add("@Photo", SqlDbType.VarBinary).Value = DBNull.Value;
                command.Parameters.AddWithValue("@Password", trainee.Password); // plain text for now

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Trainee GetTraineeById(int id)
        {
            Trainee trainee = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("usp_GetTraineeById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TraineeID", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        trainee = new Trainee
                        {
                            TraineeID = Convert.ToInt32(reader["TraineeID"]),
                            Name = reader["Name"].ToString().Trim(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Department = reader["Department"].ToString(),
                            JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                            Gender = reader["Gender"].ToString(),
                            Photo = reader["Photo"] as byte[]
                        };
                    }
                }
            }
            return trainee;
        }

        public void UpdateTrainee(Trainee trainee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("usp_UpdateTrainee", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TraineeID", trainee.TraineeID);
                command.Parameters.AddWithValue("@Name", trainee.Name);
                command.Parameters.AddWithValue("@Email", trainee.Email);
                command.Parameters.AddWithValue("@PhoneNumber", trainee.PhoneNumber);
                command.Parameters.AddWithValue("@Department", trainee.Department);
                command.Parameters.AddWithValue("@JoiningDate", trainee.JoiningDate);
                command.Parameters.AddWithValue("@Gender", trainee.Gender);
                command.Parameters.AddWithValue("@Photo", trainee.Photo ?? (object)DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteTrainee(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("usp_DeleteTrainee", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TraineeID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
