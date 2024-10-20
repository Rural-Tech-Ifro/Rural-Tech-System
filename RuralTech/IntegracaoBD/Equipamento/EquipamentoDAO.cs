using System;
using RuralTech.Database;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RuralTech.Helpers;
using RuralTech.Integracoes;
using RuralTech.Telas;
using System.Windows;

public class EquipamentoDAO
{ 
    private static Conexao _conn = new Conexao();
    
    public void Insert(Equipamento obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO equipamento (valor_equi, tipo_equi, nome_equi, descricao_equi) VALUES (@valor, @tipo, @nome, @descricao);";

            comando.Parameters.AddWithValue("@nome", obj.Nome);
            comando.Parameters.AddWithValue("@tipo", obj.Tipo);
            comando.Parameters.AddWithValue("@valor", obj.Valor);
            comando.Parameters.AddWithValue("@descricao", obj.Descricao);

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

    public List<Equipamento> GetEquipamento()
    {
        List<Equipamento> equipamentos = new List<Equipamento>();

        try
        {
            var comando = _conn.Query();
            comando.CommandText = "SELECT valor_equi, tipo_equi, nome_equi, descricao_equi FROM equipamento";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Equipamento equipamento = new Equipamento
                {
                    Valor = Convert.ToDouble(reader.GetDouble("valor_equi")),
                    Tipo = reader.GetString("tipo_equi"),
                    Nome = reader.GetString("nome_equi"),
                    Descricao = reader.GetString("descricao_equi"),

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