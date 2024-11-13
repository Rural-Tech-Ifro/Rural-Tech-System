using System;
using RuralTech.Database;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RuralTech.Helpers;
using RuralTech.Integracoes;

public class MedicamentoDAO
{
    private static Conexao _conn = new Conexao();

    public int Insert(Medicamento obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO medicamento VALUES " +
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

            return (int)comando.LastInsertedId;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<Medicamento> GetMedicamentos()
    {
        List<Medicamento> medicamentos = new List<Medicamento>();

        try
        {
            var comando = _conn.Query();
            comando.CommandText = "SELECT id_med, nome_med, diasCarencia_med, estado_med, inscricaoEstadual_med, quantidade_med, unidadeEntrada_med, unidadeSaida_med, observacao_med FROM medicamento";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Medicamento medicamento = new Medicamento
                {
                    Nome = DAOHelper.GetString(reader, "nome_med"),
                    DiasCarencia = DAOHelper.GetInt32(reader, "diasCarencia_med"),
                    Estado = DAOHelper.GetString(reader, "estado_med"),
                    InscricaoEstadual = DAOHelper.GetString(reader, "inscricaoEstadual_med"),
                    Quantidade = DAOHelper.GetInt32(reader, "quantidade_med"),
                    UnidadeEntrada = DAOHelper.GetString(reader, "unidadeEntrada_med"),
                    UnidadeSaida = DAOHelper.GetString(reader, "unidadeSaida_med"),
                    Observacao = DAOHelper.GetString(reader, "observacao_med"),
                    Id = DAOHelper.GetInt32(reader, "id_med"),
                };

                medicamentos.Add(medicamento);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return medicamentos;
    }

    public void Update(Medicamento obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "UPDATE medicamento SET nome_med = @nome, diasCarencia_med = @dias, estado_med = @estado, inscricaoEstadual_med = @inscricao, quantidade_med = @quantidade, unidadeEntrada_med = @unidadeEntrada, unidadeSaida_med = @unidadeSaida, observacao_med = @observacao WHERE id_med = @id;";

            // Define os parâmetros para a atualização
            comando.Parameters.AddWithValue("@nome", obj.Nome);
            comando.Parameters.AddWithValue("@dias", obj.DiasCarencia);
            comando.Parameters.AddWithValue("@estado", obj.Estado);
            comando.Parameters.AddWithValue("@inscricao", obj.InscricaoEstadual);
            comando.Parameters.AddWithValue("@quantidade", obj.Quantidade);
            comando.Parameters.AddWithValue("@unidadeEntrada", obj.UnidadeEntrada);
            comando.Parameters.AddWithValue("@unidadeSaida", obj.UnidadeSaida);
            comando.Parameters.AddWithValue("@observacao", obj.Observacao);

            foreach (Medicamento str in GetMedicamentos())
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
            throw new Exception("Erro ao atualizar a Medicamento: " + ex.Message, ex);
        }
    }

    public void Delete(Medicamento obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "delete from medicamento where id_med = @id;";

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