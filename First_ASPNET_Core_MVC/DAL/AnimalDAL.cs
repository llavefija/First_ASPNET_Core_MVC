using System;
using System.Collections.Generic;
using System.Data;
using First_ASPNET_Core_MVC.Models;
using Microsoft.Data.SqlClient;

namespace Animales.DAL
{
    public class AnimalDAL
    {
        private string connectionString = "";

        public List<Animal> GetAll()
        {
            List<Animal> animales = new List<Animal>();
            TipoAnimalDAL tipoAnimalDAL = new TipoAnimalDAL();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Animal";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Animal animal = new Animal
                    {
                        IdAnimal = Convert.ToInt32(reader["IdAnimal"]),
                        NombreAnimal = reader["NombreAnimal"].ToString(),
                        Raza = reader["Raza"]?.ToString(),
                        RIdTipoAnimal = Convert.ToInt32(reader["RIdTipoAnimal"]),
                        FechaNacimiento = reader["FechaNacimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["FechaNacimiento"]),
                        TipoDeAnimal = tipoAnimalDAL.GetById(Convert.ToInt32(reader["RIdTipoAnimal"]))
                    };
                    animales.Add(animal);
                }
            }

            return animales;
        }

        public Animal GetById(int id)
        {
            Animal animal = null;
            TipoAnimalDAL tipoAnimalDAL = new TipoAnimalDAL();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Animal WHERE IdAnimal = @IdAnimal";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@IdAnimal", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    animal = new Animal
                    {
                        IdAnimal = Convert.ToInt32(reader["IdAnimal"]),
                        NombreAnimal = reader["NombreAnimal"].ToString(),
                        Raza = reader["Raza"]?.ToString(),
                        RIdTipoAnimal = Convert.ToInt32(reader["RIdTipoAnimal"]),
                        FechaNacimiento = reader["FechaNacimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["FechaNacimiento"]),
                        TipoDeAnimal = tipoAnimalDAL.GetById(Convert.ToInt32(reader["RIdTipoAnimal"]))
                    };
                }
            }

            return animal;
        }

        public void Insert(Animal animal)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = "INSERT INTO Animal (NombreAnimal, Raza, RIdTipoAnimal, FechaNacimiento) VALUES (@NombreAnimal, @Raza, @RIdTipoAnimal, @FechaNacimiento)";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
                cmd.Parameters.AddWithValue("@Raza", animal.Raza ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
                cmd.Parameters.AddWithValue("@FechaNacimiento", animal.FechaNacimiento.HasValue ? (object)animal.FechaNacimiento.Value : DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Animal animal)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = "UPDATE Animal SET NombreAnimal = @NombreAnimal, Raza = @Raza, RIdTipoAnimal = @RIdTipoAnimal, FechaNacimiento = @FechaNacimiento WHERE IdAnimal = @IdAnimal";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
                cmd.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
                cmd.Parameters.AddWithValue("@Raza", animal.Raza ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
                cmd.Parameters.AddWithValue("@FechaNacimiento", animal.FechaNacimiento.HasValue ? (object)animal.FechaNacimiento.Value : DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@IdAnimal", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
