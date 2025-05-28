using DataBaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace DataBaseAPI.Repositories
{
	public class CarroRepository
	{
		SqlConnection conn;
		SqlCommand cmd = new SqlCommand();
        string connectionString = ConfigurationManager.ConnectionStrings["web-api"].ConnectionString;
        public CarroRepository()
		{
			conn = new SqlConnection(connectionString);
			cmd.Connection = conn;
		}

        public async Task<List<Models.Carro>> GetAllCarros()
        {
            List<Models.Carro> carros = new List<Carro>();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Id,Nome,Valor FROM Carro;";
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (await dr.ReadAsync())
                    {
                        Models.Carro carro = new Models.Carro();

                        carro.Id = (int)dr["Id"];
                        carro.Nome = (string)dr["Nome"];
                        carro.Valor = (decimal)dr["Valor"];

                        carros.Add(carro);
                    }
                }
            }

            return carros;
        }

        public async Task<Models.Carro> GetCarroById(int id)
        {
            Models.Carro carro = new Carro();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Id,Nome,Valor FROM Carro WHERE Id = @id;";
                    //cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        carro.Id = (int)dr["Id"];
                        carro.Nome = (string)dr["Nome"];
                        carro.Valor = (decimal)dr["Valor"];
                    }
                }

                return carro;
            }
        }
    
        public async Task<List<Models.Carro>> GetCarroByName(string nome)
        {
            List<Models.Carro> carros = new List<Carro>();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT * FROM Carro WHERE Nome LIKE '%' +  @nome + '%';";
                    cmd.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = nome;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            carros.Add(new Carro
                            {
                                Id = dr.GetInt32(dr.GetOrdinal("Id")),
                                Nome = dr.GetString(dr.GetOrdinal("Nome")),
                                Valor = dr.GetDecimal(dr.GetOrdinal("Valor"))
                            });
                        }
                    }

                    return carros;
                }
            }
        }
        
        public async Task<bool> PostCarro(Models.Carro carro)
        {
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "INSERT INTO Carro (Nome,Valor) VALUES (@nome,@valor);";

                    //cmd.Parameters.AddWithValue("@nome", carro.Nome);
                    //cmd.Parameters.AddWithValue("@valor", carro.Valor);

                    cmd.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = carro.Nome;
                    cmd.Parameters.Add(new SqlParameter("@valor", System.Data.SqlDbType.Decimal)).Value = carro.Valor;

                    int linhasCriadas = cmd.ExecuteNonQuery();
                    if (linhasCriadas > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    
        public async Task<bool> PutCarro(Models.Carro carro)
        {
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "UPDATE Carro SET Nome = @nome, Valor = @valor WHERE Id = @id;";

                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = carro.Id;
                    cmd.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = carro.Nome;
                    cmd.Parameters.Add(new SqlParameter("@valor", System.Data.SqlDbType.Decimal)).Value = carro.Valor;

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    if (linhasAfetadas > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    
        public async Task<bool> DeleteCarro(int id)
        {
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "DELETE FROM Carro WHERE Id = @id;";
                    //cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }
    }
}