using System;
using RuralTech.Database;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RuralTech.Helpers;
using RuralTech.Integracoes;

public class PatrimonioDAO
{
    private static Conexao _conn = new Conexao();

    public void Insert(Patrimonio obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO Patrimonio (nome_pat, valor_pat, tipo_pat, descricao_pat, id_pro_fk) VALUES (@nome, @valor, @tipo, @descricao, @idPropriedade);";

            comando.Parameters.AddWithValue("@nome", obj.Nome);
            comando.Parameters.AddWithValue("@valor", obj.Valor);
            comando.Parameters.AddWithValue("@tipo", obj.Tipo);
            comando.Parameters.AddWithValue("@Descricao", obj.Descricao);
            comando.Parameters.AddWithValue("@idPropriedade", obj.Propriedade);


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

    public List<Patrimonio> GetPatrimonio()
    {
        List<Patrimonio> patrimonios = new List<Patrimonio>();

        try
        {
            var comando = _conn.Query();
            comando.CommandText = "SELECT Propriedade.nome_pro, Patrimonio.id_pat, Patrimonio.nome_pat, Patrimonio.valor_pat, Patrimonio.tipo_pat, Patrimonio.descricao_pat FROM Propriedade inner join Patrimonio on (Patrimonio.id_pro_fk = Propriedade.id_pro);";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Patrimonio patrimonio = new Patrimonio
                {
                    Id = reader.GetInt32("id_pat"),
                    Valor = reader.GetDouble("valor_pat"),
                    Descricao = reader.GetString("descricao_pat"),
                    Nome = reader.GetString("nome_pat"),
                    Tipo = reader.GetString("tipo_pat"),
                    Propriedade = reader.GetString("nome_pro"),
                };
                patrimonios.Add(patrimonio);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return patrimonios;
    }
    public void Delete(Patrimonio obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "delete from patrimonio where id_pat = @id;";

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