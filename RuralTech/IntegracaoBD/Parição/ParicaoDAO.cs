using System;
using RuralTech.Database;

public class ParicaoDAO
{
    private static Conexao _conn = new Conexao();

    public void Insert(Paricao obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO paricao (dataParto_par, sexo_par, tipo_par, lote_par, detalhamento_par, situacao_par) VALUES (@DataParto, @Sexo, @Tipo, @Lote, @Detalhamento, @Situacao);";

            comando.Parameters.AddWithValue("@DataParto", obj.DataParto);
            comando.Parameters.AddWithValue("@Sexo", obj.Sexo);
            comando.Parameters.AddWithValue("@Tipo", obj.Tipo);
            comando.Parameters.AddWithValue("@Lote", obj.Lote);
            comando.Parameters.AddWithValue("@Detalhamento", obj.Detalhamento);
            comando.Parameters.AddWithValue("@Situacao", obj.Situacao);


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