using System;
using Npgsql;

namespace envia.Models
{
    public class UsuarioData
    {
        private readonly string _connectionString;

        public UsuarioData(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public bool ValidarUsuario(string email, string password)
        {

            using var con = new NpgsqlConnection(_connectionString);
            var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM usuarios WHERE email = @e AND password = @p", con);
            cmd.Parameters.AddWithValue("@e", email);
            cmd.Parameters.AddWithValue("@p", password);
            con.Open();
            var count = (long)cmd.ExecuteScalar();
            return count == 1;
        }
    }
}

