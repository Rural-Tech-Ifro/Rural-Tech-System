using System;
using RuralTech.Database;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RuralTech.Helpers;
using RuralTech.Integracoes;
using RuralTech.Telas;
using System.Windows;

public class FornecedorDAO
{
    private static Conexao _conn = new Conexao();

    public void Insert(Fornecedor obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO fornecedor (nome_for, celular_for, telefone_for, cnpjCpf_for, tipo_for, pais_for, estado_for, cidade_for, cep_for, numero_for, logradouro_for, email_for) VALUES (@nome, @celular, @telefone, @cnpjCpf, @tipo, @pais, @estado, @cidade, @cep, @numero, @logradouro, @email);";

            comando.Parameters.AddWithValue("@nome", obj.Nome);
            comando.Parameters.AddWithValue("@celular", obj.Celular);
            comando.Parameters.AddWithValue("@telefone", obj.Telefone);
            comando.Parameters.AddWithValue("@cnpjCpf", obj.CPFCNPJ);
            comando.Parameters.AddWithValue("@tipo", obj.Tipo);
            comando.Parameters.AddWithValue("@pais", obj.Pais);
            comando.Parameters.AddWithValue("@estado", obj.Estado);
            comando.Parameters.AddWithValue("@cidade", obj.Cidade);
            comando.Parameters.AddWithValue("@cep", obj.CEP);
            comando.Parameters.AddWithValue("@numero", obj.Numero);
            comando.Parameters.AddWithValue("@logradouro", obj.Logradouro);
            comando.Parameters.AddWithValue("@email", obj.Email);

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

    public List<Fornecedor> GetFornecedor()
    {
        List<Fornecedor> fornecedores = new List<Fornecedor>();

        try
        {
            var comando = _conn.Query();
            comando.CommandText = "SELECT id_for, nome_for, celular_for, telefone_for, cnpjCpf_for, tipo_for, pais_for, estado_for, cidade_for, cep_for, numero_for, logradouro_for, email_for FROM fornecedor";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Fornecedor fornecedor = new Fornecedor
                {
                    Id = reader.GetInt32("id_for"),
                    Nome = reader.GetString("nome_for"),
                    Celular = reader.GetString("celular_for"),
                    Telefone = reader.GetString("telefone_for"),
                    CPFCNPJ = reader.GetString("cnpjCpf_for"),
                    Tipo = DAOHelper.GetString(reader, "tipo_for"),
                    Pais = reader.GetString("pais_for"),
                    Estado = reader.GetString("estado_for"),
                    Cidade = reader.GetString("cidade_for"),
                    CEP = reader.GetString("cep_for"),
                    Numero = reader.GetString("numero_for"),
                    Logradouro = reader.GetString("logradouro_for"),
                    Email = reader.GetString("email_for"),

                };
                fornecedores.Add(fornecedor);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }


        return fornecedores;
    }
    public void Delete(Fornecedor obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "delete from fornecedor where id_for = @id;";

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