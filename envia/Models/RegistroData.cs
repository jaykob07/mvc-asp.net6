using System;
using Npgsql;

namespace envia.Models
{
    public class RegistroData
    {
        private readonly string _connectionString;

        public RegistroData(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<Registro> GetAll()
        {
            var lista = new List<Registro>();
            using var con = new NpgsqlConnection(_connectionString);
            var cmd = new NpgsqlCommand("SELECT id, nombre, apellido, cargo, fecha_creacion FROM registros ORDER BY id DESC", con);

            con.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Registro
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Apellido = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Cargo = reader.IsDBNull(3) ? null : reader.GetString(3),
                    FechaCreacion = reader.GetDateTime(4)
                });

            }
            return lista;
        }

        public void Insert(Registro r)
        {
            using var con = new NpgsqlConnection(_connectionString);
            var cmd = new NpgsqlCommand("INSERT INTO registros (nombre, apellido, cargo, fecha_creacion) VALUES (@nom, @ape, @car, NOW())", con);
            cmd.Parameters.AddWithValue("@nom", r.Nombre ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ape", r.Apellido ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@car", r.Cargo ?? (object)DBNull.Value);

        }

        public Registro GetById(int id)
        {
            using var con = new NpgsqlConnection(_connectionString);
            var cmd = new NpgsqlCommand("SELECT id, nombre, apellido, cargo, fecha_creacion FROM registros ORDER BY id DESC", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Registro
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Apellido = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Cargo = reader.IsDBNull(3) ? null : reader.GetString(3),
                    FechaCreacion = reader.GetDateTime(4)
                };
            }
            return null!;
        }

        public void Update(Registro r)
        {
            using var con = new NpgsqlConnection(_connectionString);
            var cmd = new NpgsqlCommand("UPDATE registros SET nombre=@nom, apellido=@ape, cargo=@car WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@nom", r.Nombre ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ape", r.Apellido ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@car", r.Cargo ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@id", r.Id);

        }

        public void Delete(int id)
        {
            using var con = new NpgsqlConnection(_connectionString);
            var cmd = new NpgsqlCommand("DELETE FROM registros WHERE id = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}

