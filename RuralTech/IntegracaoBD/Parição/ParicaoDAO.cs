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
    public void Update(Paricao obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "UPDATE paricao SET dataParto_par = @dataParto, sexo_par = @sexo, tipo_par = @tipo, lote_par = @lote, detalhamento_par = @detalhamento, situacao_par = @situacao WHERE id_par = @id;";

            // Define os parâmetros para a atualização
            comando.Parameters.AddWithValue("@apelido1", obj.NomeAtributo);



            foreach (Paricao str in GetParicao())
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
            throw new Exception("Erro ao atualizar a paricao: " + ex.Message, ex);
        }
    }
    public void Delete(Paricao obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "delete from paricao where id_par = @id;";

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