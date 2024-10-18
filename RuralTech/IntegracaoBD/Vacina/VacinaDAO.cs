using System;
using RuralTech.Database;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RuralTech.Helpers;
using RuralTech.Integracoes;

public class VacinaDAO
{
    private static Conexao _conn = new Conexao();

    public void Insert(Vacina obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO vacina_medicamento VALUES " +
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
            comando.CommandText = "SELECT nome_vac_med, diasCarencia_vac_med, estado_vac_med, quantidade_vac_med, unidadeEntrada_vac_med, unidadeSaida_vac_med, observacao_vac_med FROM vacina_medicamento";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Vacina vacina = new Vacina
                {
                    Nome = reader.GetString("nome_vac_med"),
                    DiasCarencia = reader.GetInt32("diasCarencia_vac_med"),
                    Estado = reader.GetString("estado_vac_med"),
                    Quantidade = reader.GetInt32("quantidade_vac_med"),
                    UnidadeEntrada = DAOHelper.GetString(reader, "unidadeEntrada_vac_med"),
                    UnidadeSaida = reader.GetString("unidadeSaida_vac_med"),
                    Observacao = reader.GetString("observacao_vac_med")
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


}