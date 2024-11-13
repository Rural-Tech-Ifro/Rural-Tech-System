using System;
using RuralTech.Database;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RuralTech.Helpers;
using RuralTech.Integracoes;

public class VacinaDAO
{
    private static Conexao _conn = new Conexao();

    public int Insert(Vacina obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO vacina VALUES " +
            "(null, @nome, @diasCarencia, @estado, @inscricaoEstadual, @quantidade, @unidadeEntrada, @unidadeSaida, @observacao)";


            comando.Parameters.AddWithValue("@nome", obj.Nome);
            comando.Parameters.AddWithValue("@diasCarencia", obj.DiasCarencia);
            comando.Parameters.AddWithValue("@estado", obj.Estado);
            comando.Parameters.AddWithValue("@inscricaoEstadual", obj.InscricaoEstadual);
            comando.Parameters.AddWithValue("@quantidade", obj.Quantidade);
            comando.Parameters.AddWithValue("@unidadeEntrada", obj.UnidadeEntrada);
            comando.Parameters.AddWithValue("@unidadeSaida", obj.UnidadeSaida);
            comando.Parameters.AddWithValue("@observacao", obj.Observacao);

            var resultado = comando.ExecuteNonQuery();

            if (resultado == 0)
            {
                throw new Exception("Ocorreram erros ao salvar as informações");
            }

            return (int) comando.LastInsertedId;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<Vacina> GetVacinas()
    {
        List<Vacina> vacinas = new List<Vacina>();

        try
        {
            var comando = _conn.Query();
<<<<<<< HEAD
            comando.CommandText = "SELECT id_vac, nome_vac, diasCarencia_vac, estado_vac, quantidade_vac, unidadeEntrada_vac, unidadeSaida_vac, observacao_vac FROM vacina";
=======
            comando.CommandText = "SELECT id_vac, nome_vac, diasCarencia_vac, estado_vac, inscricaoEstadual_vac, quantidade_vac, unidadeEntrada_vac, unidadeSaida_vac, observacao_vac FROM vacina";
>>>>>>> 54a4911379da197c93a63187fd6cf7741cd01d28

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Vacina vacina = new Vacina
                {
                    Nome = DAOHelper.GetString(reader, "nome_vac"),
                    DiasCarencia = DAOHelper.GetInt32(reader, "diasCarencia_vac"),
                    Estado = DAOHelper.GetString(reader, "estado_vac"),
<<<<<<< HEAD
=======
                    InscricaoEstadual = DAOHelper.GetString(reader, "inscricaoEstadual_vac"),
>>>>>>> 54a4911379da197c93a63187fd6cf7741cd01d28
                    Quantidade = DAOHelper.GetInt32(reader, "quantidade_vac"),
                    UnidadeEntrada = DAOHelper.GetString(reader, "unidadeEntrada_vac"),
                    UnidadeSaida = DAOHelper.GetString(reader, "unidadeSaida_vac"),
                    Observacao = DAOHelper.GetString(reader, "observacao_vac"),
                    Id = DAOHelper.GetInt32(reader, "id_vac"),
                };

                vacinas.Add(vacina);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return vacinas;
    }

    public void Update(Vacina obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "UPDATE vacina SET nome_vac = @nome, diasCarencia_vac = @dias, estado_vac = @estado, inscricaoEstadual_vac = @inscricao, quantidade_vac = @quantidade, unidadeEntrada_vac = @unidadeEntrada, unidadeSaida_vac = @unidadeSaida, observacao_vac = @observacao WHERE id_vac = @id;";

            // Define os parâmetros para a atualização
            comando.Parameters.AddWithValue("@nome", obj.Nome);
            comando.Parameters.AddWithValue("@dias", obj.DiasCarencia);
            comando.Parameters.AddWithValue("@estado", obj.Estado);
            comando.Parameters.AddWithValue("@inscricao", obj.InscricaoEstadual);
            comando.Parameters.AddWithValue("@quantidade", obj.Quantidade);
            comando.Parameters.AddWithValue("@unidadeEntrada", obj.UnidadeEntrada);
            comando.Parameters.AddWithValue("@unidadeSaida", obj.UnidadeSaida);
            comando.Parameters.AddWithValue("@observacao", obj.Observacao);

            foreach (Vacina str in GetVacinas())
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
            throw new Exception("Erro ao atualizar a vacina: " + ex.Message, ex);
        }
    }

    public void Delete(Vacina obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "delete from vacina where id_vac = @id;";

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