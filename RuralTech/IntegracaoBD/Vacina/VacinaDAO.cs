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
}