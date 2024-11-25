using System;
using RuralTech.Database;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RuralTech.Helpers;
using RuralTech.Integracoes;
using RuralTech.Telas;
using System.Windows;

public class PropriedadeDAO
{
    private static Conexao _conn = new Conexao();

    public void Insert(Propriedade obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO propriedade (nome_pro, proprietario_pro, logradouro_pro, cep_pro, bairro_pro, complemento_pro, tamanho_pro) VALUES (@nome, @proprietario, @logradouro, @cep, @bairro, @complemento, @tamanho);";

            comando.Parameters.AddWithValue("@nome", obj.NomePropriedade);
            comando.Parameters.AddWithValue("@proprietario", obj.NomeProprietario);
            comando.Parameters.AddWithValue("@logradouro", obj.Logradouro);
            comando.Parameters.AddWithValue("@cep", obj.CEP);
            comando.Parameters.AddWithValue("@bairro", obj.Bairro);
            comando.Parameters.AddWithValue("@complemento", obj.Complemento);
            comando.Parameters.AddWithValue("@tamanho", obj.Tamanho);

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

    public List<Propriedade> GetPropriedade()
    {
        List<Propriedade> propriedades = new List<Propriedade>();

        try
        {
            var comando = _conn.Query();
            comando.CommandText = "SELECT id_pro, nome_pro, proprietario_pro, logradouro_pro, cep_pro, bairro_pro, complemento_pro, tamanho_pro FROM propriedade";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Propriedade propriedade = new Propriedade
                {
                    Id = reader.GetInt32("id_pro"),
                    NomePropriedade = reader.GetString("nome_pro"),
                    NomeProprietario = reader.GetString("proprietario_pro"),
                    Logradouro = reader.GetString("logradouro_pro"),
                    CEP = reader.GetString("cep_pro"),
                    Bairro = reader.GetString("bairro_pro"),
                    Complemento = reader.GetString("complemento_pro"),
                    Tamanho = reader.GetInt32("tamanho_pro"),
                };
                propriedades.Add(propriedade);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }


        return propriedades;
    }
    public void Update(Propriedade obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "UPDATE propriedade SET nome_pro = @nome, proprietario_pro = @proprietario, logradouro_pro = @logradouro, cep_pro = @cep, bairro_pro = @bairro, complemento_pro = @complemento, tamanho_pro = @tamanho, null, id_usu_fk = @usuario WHERE id_pro = @id;";

            // Define os parâmetros para a atualização
            comando.Parameters.AddWithValue("@nome", obj.NomePropriedade);
            comando.Parameters.AddWithValue("@proprietario", obj.NomeProprietario);
            comando.Parameters.AddWithValue("@logradouro", obj.Logradouro);
            comando.Parameters.AddWithValue("@cep", obj.CEP);
            comando.Parameters.AddWithValue("@bairro", obj.Bairro);
            comando.Parameters.AddWithValue("@complemento", obj.Complemento);
            comando.Parameters.AddWithValue("@tamanho", obj.Tamanho);
            comando.Parameters.AddWithValue("@usuario", obj.Usuario);



            foreach (Propriedade str in GetPropriedade())
            {
                if (str.Id == obj.Id)
                {
                    comando.Parameters.AddWithValue("@id", str.Id);
                }
            }

            // Executa o comando e verifica o resultado
            var resultado = comando.ExecuteNonQuery();

            if (resultado == 0)
            {
                throw new Exception("Ocorreram erros ao atualizar as informações");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao atualizar a propriedade: " + ex.Message, ex);
        }
    }
    public void Delete(Propriedade obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "delete from propriedade where id_pro = @id;";

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