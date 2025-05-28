using DataBaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DataBaseAPI.Repositories
{
	public class PessoaRepository
	{

        SqlConnection conn;
        SqlCommand cmd = new SqlCommand();
        string connectionString = ConfigurationManager.ConnectionStrings["web-api"].ConnectionString;
		
		public PessoaRepository()
		{
            conn = new SqlConnection(connectionString);
            cmd.Connection = conn;
        }
        public async Task<List<Models.Pessoa>> GetAllPersons()
		{
            List<Models.Pessoa> pessoas = new List<Pessoa>();

			using (conn)
			{
				await conn.OpenAsync();
				using (cmd)
				{
                    cmd.CommandText = "SELECT Id,Nome,Idade FROM Pessoa;";
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        pessoas.Add(new Models.Pessoa
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("Id")),
                            Nome = dr.GetString(dr.GetOrdinal("Nome")),
                            Idade = dr.GetInt32(dr.GetOrdinal("Idade"))

                        });
                    }

                    return pessoas;
                }

			}
		}
	
        public async Task<Models.Pessoa> GetPessoaById(int id)
        {
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Id,Nome,Idade FROM Pessoa WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        return (new Models.Pessoa
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("Id")),
                            Nome = dr.GetString(dr.GetOrdinal("Nome")),
                            Idade = dr.GetInt32(dr.GetOrdinal("Idade"))
                        });
                    }

                    return null;
                }
            }
        }
    
        public async Task<bool> CreatePessoa(Models.Pessoa pessoa)
        {
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "INSERT INTO Pessoa (Nome,Idade) VALUES (@Nome,@Idade);";
                    cmd.Parameters.AddWithValue("@Nome", pessoa.Nome);
                    cmd.Parameters.AddWithValue("@Idade", pessoa.Idade);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }
    
        public async Task<bool> UpdatePessoa(int id,Models.Pessoa pessoa)
        {
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "UPDATE Pessoa SET Nome = @Nome, Idade = @idade WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Nome", pessoa.Nome);
                    cmd.Parameters.AddWithValue("@Idade", pessoa.Idade);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }
    
        public async Task<bool> DeletePessoa(int id)
        {
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "DELETE FROM Pessoa WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@Id", id);

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