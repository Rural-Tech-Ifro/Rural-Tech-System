using System;
using RuralTech.Database;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RuralTech.Helpers;
using RuralTech.Integracoes;
using RuralTech.Telas;
using System.Windows;

public class FuncionarioDAO
{
    private static Conexao _conn = new Conexao();

    public void Insert(Funcionario obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO Funcionario (nome_fun, email_fun, telefone_fun, numero_fun, salario_fun, celular_fun, logradouro_fun, pais_fun, estado_fun, cidade_fun, cep_fun, cpf_fun, dataNascimento_fun, dataPagamento_fun, dataAdmissao_fun) VALUES (@nome, @email, @telefone, @numero, @salario, @celular, @logradouro, @pais, @estado, @cidade, @cep, @cpf, @dataNascimento, @dataPagamento, @dataAdmissao);";

            comando.Parameters.AddWithValue("@nome", obj.Nome);
            comando.Parameters.AddWithValue("@email", obj.Email);
            comando.Parameters.AddWithValue("@telefone", obj.Telefone);
            comando.Parameters.AddWithValue("@numero", obj.Numero);
            comando.Parameters.AddWithValue("@salario", obj.Salario);
            comando.Parameters.AddWithValue("@celular", obj.Celular);
            comando.Parameters.AddWithValue("@logradouro", obj.Logradouro);
            comando.Parameters.AddWithValue("@pais", obj.Pais);
            comando.Parameters.AddWithValue("@estado", obj.Estado);
            comando.Parameters.AddWithValue("@cidade", obj.Cidade);
            comando.Parameters.AddWithValue("@cep", obj.CEP);
            comando.Parameters.AddWithValue("@cpf", obj.CPF);
            comando.Parameters.AddWithValue("@dataNascimento", obj.DataNascimento);
            comando.Parameters.AddWithValue("@dataPagamento", obj.DataPagamento);
            comando.Parameters.AddWithValue("@dataAdmissao", obj.DataAdmissao);

            var resultado = comando.ExecuteNonQuery();

            if (resultado == 0)
            {
                throw new Exception("Ocorreram erros ao salvar as informações");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<Funcionario> GetFuncionario()
    {
        List<Funcionario> funcionarios = new List<Funcionario>();

        try
        {
            var comando = _conn.Query();
            comando.CommandText = "SELECT id_fun, nome_fun, email_fun, telefone_fun, numero_fun, salario_fun, celular_fun, logradouro_fun, pais_fun, estado_fun, cidade_fun, cep_fun, cpf_fun, dataNascimento_fun, dataPagamento_fun, dataAdmissao_fun from funcionario;";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Funcionario Funcionario = new Funcionario
                {
                    Id = reader.GetInt32("id_fun"),
                    Nome = reader.GetString("nome_fun"),
                    Email = reader.GetString("email_fun"),
                    Telefone = reader.GetString("telefone_fun"),
                    Numero = reader.GetString("numero_fun"),
                    Celular = reader.GetString("celular_fun"),
                    Logradouro = reader.GetString("logradouro_fun"),
                    Pais = reader.GetString("pais_fun"),
                    Estado = reader.GetString("estado_fun"),
                    Salario = reader.GetDouble("salario_fun"),
                    Cidade = reader.GetString("cidade_fun"),
                    CEP = reader.GetString("cep_fun"),
                    CPF = reader.GetString("cpf_fun"),
                    DataNascimento = reader.GetDateTime("dataNascimento_fun"),
                    DataPagamento = reader.GetDateTime("dataPagamento_fun"),
                    DataAdmissao = reader.GetDateTime("dataAdmissao_fun"),
                };
                funcionarios.Add(Funcionario);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }


        return funcionarios;
    }
    public void Delete(Funcionario obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "delete from nomeTabela where id_fun = @id;";

            comando.Parameters.AddWithValue("@id", obj.Id);

            var resultado = comando.ExecuteNonQuery();

            if (resultado == 0)
            {
                throw new Exception("Ocorreram erros ao salvar as informações.");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}