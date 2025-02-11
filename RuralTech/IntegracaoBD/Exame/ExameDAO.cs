using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using RuralTech.Database;
using RuralTech.Helpers;

public class ExameDAO
{
    private static Conexao _conn = new Conexao();

    public void Insert(Exame obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO exame (tipo_exa, resultado_exa, data_exa, id_ani_fk) VALUES (@tipo, @resultado, @data, @idAnimal);";

            comando.Parameters.AddWithValue("@tipo", obj.Tipo);
            comando.Parameters.AddWithValue("@resultado", obj.Resultado);
            comando.Parameters.AddWithValue("@data", obj.Data);
            comando.Parameters.AddWithValue("@idAnimal", obj.Animal);


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
    public List<Exame> GetExames()
    {
        List<Exame> exames = new List<Exame>();

        try
        {
            var comando = _conn.Query();
            comando.CommandText = "SELECT id_exa, tipo_exa, resultado_exa, data_exa, id_ani_fk FROM exame;";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Exame exame = new Exame
                { 
                    Id = DAOHelper.GetInt32(reader, "id_exa"),
                    Tipo = DAOHelper.GetString(reader, "tipo_exa"),
                    Resultado = DAOHelper.GetString(reader, "resultado_exa"),
                    Data = Convert.ToDateTime(DAOHelper.GetDateTime(reader, "data_exa")),
                    Animal = DAOHelper.GetString(reader, "id_ani_fk"),


                };
                exames.Add(exame);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return exames;
    }
    public void Update(Exame obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "UPDATE exame SET tipo_exa = @tipo, resultado_exa = @resultado, data_exa = @data, id_ani_fk = @animal WHERE id_exa = @id;";

            // Define os parâmetros para a atualização
            comando.Parameters.AddWithValue("@tipo", obj.Tipo);
            comando.Parameters.AddWithValue("@resultado", obj.Resultado);
            comando.Parameters.AddWithValue("@data", obj.Data);
            comando.Parameters.AddWithValue("@animal", obj.Animal);



            foreach (Exame str in GetExames())
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
            throw new Exception("Erro ao atualizar o Exame: " + ex.Message, ex);
        }
    }
    public void Delete(Exame obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "delete from exame where id_exa = @id;";

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