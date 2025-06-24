using DataBaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace DataBaseAPI.Repositories
{
	public class FuncionarioRepository
	{
        SqlConnection conn;
        SqlCommand cmd = new SqlCommand();
        string connectionString = ConfigurationManager.ConnectionStrings["web-api"].ConnectionString;

        public FuncionarioRepository()
        {
            conn = new SqlConnection(connectionString);
            cmd.Connection = conn;
        }
        public async Task<List<Models.Funcionario>> GetAllFuncionarios()
		{
            List<Models.Funcionario> funcionarios = new List<Funcionario>();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Codigo,CodigoDepartamento,PrimeiroNome,SegundoNome,UltimoNome,DataNascimento,CPF,RG,Endereco,CEP,Cidade,Fone,Funcao,Salario FROM Funcionario;";
                    SqlDataReader dr = cmd.ExecuteReader();


                    while (dr.Read())
                    {


                        funcionarios.Add(new Models.Funcionario
                        {
                            Codigo = dr.GetInt32(dr.GetOrdinal("Codigo")),

                            CodigoDepartamento = dr.GetInt32(dr.GetOrdinal("CodigoDepartamento")),

                            PrimeiroNome = dr.GetString(dr.GetOrdinal("PrimeiroNome")),

                            SegundoNome = dr.IsDBNull(dr.GetOrdinal("SegundoNome")) ? null : dr.GetString(dr.GetOrdinal("SegundoNome")),

                            UltimoNome = dr.GetString(dr.GetOrdinal("UltimoNome")),

                            DataNascimento = dr.GetDateTime(dr.GetOrdinal("DataNascimento")),

                            CPF = dr.GetString(dr.GetOrdinal("CPF")),

                            RG = dr.GetString(dr.GetOrdinal("RG")),

                            Endereco = dr.GetString(dr.GetOrdinal("Endereco")),

                            CEP = dr.GetString(dr.GetOrdinal("CEP")),

                            Cidade = dr.GetString(dr.GetOrdinal("Cidade")),

                            Fone = dr.IsDBNull(dr.GetOrdinal("Fone")) ? null : dr.GetString(dr.GetOrdinal("Fone")),

                            Funcao = dr.GetString(dr.GetOrdinal("Funcao")),

                            Salario = dr.GetDecimal(dr.GetOrdinal("Salario"))
                        });
                    }

                    return funcionarios;
                }
            } 
        }
	
        public async Task<Models.Funcionario> GetFuncionarioById(int id)
        {
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Codigo,CodigoDepartamento,PrimeiroNome,SegundoNome,UltimoNome,DataNascimento,CPF,RG,Endereco,CEP,Cidade,Fone,Funcao,Salario FROM Funcionario WHERE Codigo = @Id;";
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        Models.Funcionario func = new Models.Funcionario()
                        {
                            Codigo = dr.GetInt32(dr.GetOrdinal("Codigo")),
                            CodigoDepartamento = dr.GetInt32(dr.GetOrdinal("CodigoDepartamento")),
                            PrimeiroNome = dr.GetString(dr.GetOrdinal("PrimeiroNome")),
                            SegundoNome = dr.IsDBNull(dr.GetOrdinal("SegundoNome")) ? null : dr.GetString(dr.GetOrdinal("SegundoNome")),
                            UltimoNome = dr.GetString(dr.GetOrdinal("UltimoNome")),
                            DataNascimento = dr.GetDateTime(dr.GetOrdinal("DataNascimento")),
                            CPF = dr.GetString(dr.GetOrdinal("CPF")),
                            RG = dr.GetString(dr.GetOrdinal("RG")),
                            Endereco = dr.GetString(dr.GetOrdinal("Endereco")),
                            CEP = dr.GetString(dr.GetOrdinal("CEP")),
                            Cidade = dr.GetString(dr.GetOrdinal("Cidade")),
                            Fone = dr.IsDBNull(dr.GetOrdinal("Fone")) ? null : dr.GetString(dr.GetOrdinal("Fone")),
                            Funcao = dr.GetString(dr.GetOrdinal("Funcao")),
                            Salario = dr.GetDecimal(dr.GetOrdinal("Salario"))
                        };

                        return func;
                    }

                    return null;
                }
            }
        }
    
        public async Task<List<Models.Funcionario>> GetFuncionarioByName(string nome)
        {
            List<Models.Funcionario> funcionarios = new List<Funcionario>();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Codigo,CodigoDepartamento,PrimeiroNome,SegundoNome,UltimoNome,DataNascimento,CPF,RG,Endereco,CEP,Cidade,Fone,Funcao,Salario FROM Funcionario WHERE PrimeiroNome LIKE '%' + @nome + '%';";
                    cmd.Parameters.AddWithValue("@nome", nome);
                    SqlDataReader dr = cmd.ExecuteReader();
                   
                    while (dr.Read())
                    {
                        funcionarios.Add(new Models.Funcionario
                        {
                            Codigo = dr.GetInt32(dr.GetOrdinal("Codigo")),
                            CodigoDepartamento = dr.GetInt32(dr.GetOrdinal("CodigoDepartamento")),
                            PrimeiroNome = dr.GetString(dr.GetOrdinal("PrimeiroNome")),
                            SegundoNome = dr.IsDBNull(dr.GetOrdinal("SegundoNome")) ? null : dr.GetString(dr.GetOrdinal("SegundoNome")),
                            UltimoNome = dr.GetString(dr.GetOrdinal("UltimoNome")),
                            DataNascimento = dr.GetDateTime(dr.GetOrdinal("DataNascimento")),
                            CPF = dr.GetString(dr.GetOrdinal("CPF")),
                            RG = dr.GetString(dr.GetOrdinal("RG")),
                            Endereco = dr.GetString(dr.GetOrdinal("Endereco")),
                            CEP = dr.GetString(dr.GetOrdinal("CEP")),
                            Cidade = dr.GetString(dr.GetOrdinal("Cidade")),
                            Fone = dr.IsDBNull(dr.GetOrdinal("Fone")) ? null : dr.GetString(dr.GetOrdinal("Fone")),
                            Funcao = dr.GetString(dr.GetOrdinal("Funcao")),
                            Salario = dr.GetDecimal(dr.GetOrdinal("Salario"))
                        });

                        return funcionarios;
                    }

                    return null;
                }
            }
        }
        public async Task<bool> CreateFuncionario(Models.Funcionario funcionario)
        {
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText =

                    "INSERT INTO Funcionario (CodigoDepartamento,PrimeiroNome,SegundoNome,UltimoNome,DataNascimento,CPF,RG,Endereco,CEP,Cidade,Fone,Funcao,Salario) " +
                    "VALUES (@CodigoDepartamento,@PrimeiroNome,@SegundoNome,@UltimoNome,@DataNascimento,@CPF,@RG,@Endereco,@CEP,@Cidade,@Fone,@Funcao,@Salario);";

                    cmd.Parameters.AddWithValue("@CodigoDepartamento", funcionario.CodigoDepartamento);
                    cmd.Parameters.AddWithValue("@PrimeiroNome", funcionario.PrimeiroNome);
                    cmd.Parameters.AddWithValue("@SegundoNome", funcionario.SegundoNome);
                    cmd.Parameters.AddWithValue("@UltimoNome", funcionario.UltimoNome);
                    cmd.Parameters.AddWithValue("@DataNascimento", funcionario.DataNascimento);
                    cmd.Parameters.AddWithValue("@CPF", funcionario.CPF);
                    cmd.Parameters.AddWithValue("@RG", funcionario.RG);
                    cmd.Parameters.AddWithValue("@Endereco", funcionario.Endereco);
                    cmd.Parameters.AddWithValue("@CEP", funcionario.CEP);
                    cmd.Parameters.AddWithValue("@Cidade", funcionario.Cidade);
                    cmd.Parameters.AddWithValue("@Fone", funcionario.Fone);
                    cmd.Parameters.AddWithValue("@Funcao", funcionario.Funcao);
                    cmd.Parameters.AddWithValue("@Salario", funcionario.Salario);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }
    
        public async Task<bool> UpdateFuncionario(Models.Funcionario funcionario)
        {
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "UPDATE Funcionario SET " +
                         "CodigoDepartamento = @CodigoDepartamento,PrimeiroNome = @PrimeiroNome,SegundoNome = @SegundoNome,UltimoNome = @UltimoNome,DataNascimento = @DataNascimento,CPF = @CPF,RG = @RG,Endereco = @Endereco,CEP = @CEP,Cidade = @Cidade,Fone = @Fone,Funcao = @Funcao, Salario = @Salario";

                    cmd.Parameters.AddWithValue("@CodigoDepartamento", funcionario.CodigoDepartamento);
                    cmd.Parameters.AddWithValue("@PrimeiroNome", funcionario.PrimeiroNome);
                    cmd.Parameters.AddWithValue("@SegundoNome", funcionario.SegundoNome);
                    cmd.Parameters.AddWithValue("@UltimoNome", funcionario.UltimoNome);
                    cmd.Parameters.AddWithValue("@DataNascimento", funcionario.DataNascimento);
                    cmd.Parameters.AddWithValue("@CPF", funcionario.CPF);
                    cmd.Parameters.AddWithValue("@RG", funcionario.RG);
                    cmd.Parameters.AddWithValue("@Endereco", funcionario.Endereco);
                    cmd.Parameters.AddWithValue("@CEP", funcionario.CEP);
                    cmd.Parameters.AddWithValue("@Cidade", funcionario.Cidade);
                    cmd.Parameters.AddWithValue("@Fone", funcionario.Fone);
                    cmd.Parameters.AddWithValue("@Funcao", funcionario.Funcao);
                    cmd.Parameters.AddWithValue("@Salario", funcionario.Salario);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }
    
        public async Task<bool> DeleteFuncionario(int id)
        {
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "DELETE Funcionario WHERE Codigo = @Id;";
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