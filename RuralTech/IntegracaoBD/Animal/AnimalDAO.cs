using System;
using RuralTech.Database;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RuralTech.Helpers;
using RuralTech.Integracoes;

public class AnimalDAO
{
    private static Conexao _conn = new Conexao();

    public void Insert(Animals obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO animal (brinco_ani, sexo_ani, raca_ani, classificacao_ani, origem_ani) VALUES (@nome, @sexo, @raca, @classificacao, @origem);";

            comando.Parameters.AddWithValue("@nome", obj.Brinco);
            comando.Parameters.AddWithValue("@sexo", obj.Sexo);
            comando.Parameters.AddWithValue("@raca", obj.Raca);
            comando.Parameters.AddWithValue("@classificacao", obj.Classificacao);
            comando.Parameters.AddWithValue("@origem", obj.Origem);


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

    public List<Animals> GetAnimal()
    {
        List<Animals> animais = new List<Animals>();

        try
        {
            var comando = _conn.Query();
            comando.CommandText = "SELECT id_ani, brinco_ani, sexo_ani, raca_ani, classificacao_ani, origem_ani FROM animal";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Animals animal = new Animals
                {
                    Id = reader.GetInt32("id_ani"),
                    Brinco = reader.GetString("brinco_ani"),
                    Sexo = reader.GetString("sexo_ani"),
                    Raca = reader.GetString("raca_ani"),
                    Classificacao = reader.GetString("classificacao_ani"),
                    Origem = reader.GetString("origem_ani"),
                };
                animais.Add(animal);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return animais;
    }
    public void Delete(Animals obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "delete from animal where id_ani = @id;";

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