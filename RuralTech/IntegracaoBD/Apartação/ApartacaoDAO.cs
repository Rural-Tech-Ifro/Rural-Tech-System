using System;
using RuralTech.Database;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RuralTech.Helpers;
using RuralTech.Integracoes;
using RuralTech.Telas;
using System.Windows;

public class ApartacaoDAO
{
    private static Conexao _conn = new Conexao();

    public void Insert(Apartacao obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO apartacao (lote_apa, observacao_apa, situacao_apa, dataTransferencia_apa, id_ani_fk) VALUES (@lote, @observacao, @situacao, @dataTransferencia, @IdAnimal);";

            comando.Parameters.AddWithValue("@situacao", obj.Situacao);
            comando.Parameters.AddWithValue("@lote", obj.Lote);
            comando.Parameters.AddWithValue("@dataTransferencia", obj.DataTransferencia);
            comando.Parameters.AddWithValue("@observacao", obj.Observacao);
            comando.Parameters.AddWithValue("@IdAnimal", obj.Animal);

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

    public List<Apartacao> GetApartacoes()
    {
        List<Apartacao> apartacoes = new List<Apartacao>();

        try
        {
            var comando = _conn.Query();
            comando.CommandText = "SELECT Animal.brinco_ani, Apartacao.id_apa, Apartacao.situacao_apa, Apartacao.lote_apa, Apartacao.dataTransferencia_apa, Apartacao.observacao_apa FROM Animal inner join Apartacao on (Apartacao.id_ani_fk = Animal.id_ani);";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Apartacao apartacao = new Apartacao
                {
                    Id = reader.GetInt32("id_apa"),
                    Situacao = reader.GetString("situacao_apa"),
                    Lote = reader.GetString("lote_apa"),
                    DataTransferencia = reader.GetDateTime("dataTransferencia_apa"),
                    Observacao = reader.GetString("observacao_apa"),
                    Animal = reader.GetString("brinco_ani"),

                };
                apartacoes.Add(apartacao);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }


        return apartacoes;
    }

    public void Update(Apartacao obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "UPDATE apartacao SET lote_apa = @lote, observacao_apa = @observacao, situacao_apa = @situacao, dataTransferencia_apa = @dataTransferencia, id_ani_fk = @animal WHERE id_apa = @id;";

            // Define os parâmetros para a atualização
            comando.Parameters.AddWithValue("@situacao", obj.Situacao);
            comando.Parameters.AddWithValue("@lote", obj.Lote);
            comando.Parameters.AddWithValue("@dataTransferencia", obj.DataTransferencia);
            comando.Parameters.AddWithValue("@observacao", obj.Observacao);
            comando.Parameters.AddWithValue("@IdAnimal", obj.Animal);


            foreach (Apartacao str in GetApartacoes())
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

    public void Delete(Apartacao obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "delete from apartacao where id_apa = @id;";

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