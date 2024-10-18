using System;
using RuralTech.Database;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RuralTech.Helpers;
using RuralTech.Integracoes;
using RuralTech.Telas;

public class EquipamentoDAO
{ 
    private static Conexao _conn = new Conexao();
    
    public void Insert(Equipamentos obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO equipamento (valor_equi, tipo_equi, nome_equi) VALUES (@nome, @tipo, @valor);";

            comando.Parameters.AddWithValue("@nome", obj.Nome);
            comando.Parameters.AddWithValue("@tipo", obj.Tipo);
            comando.Parameters.AddWithValue("@valor", obj.Valor);

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

    public List<Equipamentos> GetAnimal()
    {
        List<Equipamentos> equipamentos = new List<Equipamentos>();

        try
        {
            var comando = _conn.Query();
            comando.CommandText = "SELECT valor_equi, tipo_equi, nome_equi FROM equipamento";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Equipamentos equipamento = new Equipamentos
                {
                    Valor = reader.GetString("valor_equi"),
                    Tipo = reader.GetString("tipo_equi"),
                    Nome = reader.GetString("nome_equi"),
                 
                };
                equipamentos.Add(equipamento);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return equipamentos;
    }
}