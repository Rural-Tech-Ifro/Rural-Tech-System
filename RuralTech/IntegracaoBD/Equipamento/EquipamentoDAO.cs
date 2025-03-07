﻿using System;
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

            comando.CommandText = "INSERT INTO equipamento (valor_equi, tipo_equi, nome_equi, descricao_equi, id_pro_fk) VALUES (@valor, @tipo, @nome, @descricao, @idPropriedade);";

            comando.Parameters.AddWithValue("@nome", obj.Nome);
            comando.Parameters.AddWithValue("@tipo", obj.Tipo);
            comando.Parameters.AddWithValue("@valor", obj.Valor);
            comando.Parameters.AddWithValue("@descricao", obj.Descricao);
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

    public List<Equipamento> GetEquipamento()
    {
        List<Equipamento> equipamentos = new List<Equipamento>();

        try
        {
            var comando = _conn.Query();
            comando.CommandText = "SELECT Propriedade.nome_pro, Equipamento.id_equi, Equipamento.valor_equi, Equipamento.tipo_equi, Equipamento.nome_equi, Equipamento.descricao_equi FROM Propriedade inner join Equipamento on (Equipamento.id_pro_fk = Propriedade.id_pro);";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Equipamento equipamento = new Equipamento
                {
                    Id = reader.GetInt32("id_equi"),
                    Valor = Convert.ToDouble(reader.GetDouble("valor_equi")),
                    Tipo = reader.GetString("tipo_equi"),
                    Nome = reader.GetString("nome_equi"),
                    Descricao = reader.GetString("descricao_equi"),
                    Propriedade = reader.GetString("nome_pro"),

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
    public void Update(Equipamento obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "UPDATE equipamento SET valor_equi = @valor, descricao_equi = @descricao, tipo_equi = @tipo, nome_equi = @nome, id_pro_fk = @propriedade WHERE id_equi = @id;";

            // Define os parâmetros para a atualização
            comando.Parameters.AddWithValue("@valor", obj.Valor);
            comando.Parameters.AddWithValue("@descricao", obj.Descricao);
            comando.Parameters.AddWithValue("@tipo", obj.Tipo);
            comando.Parameters.AddWithValue("@nome", obj.Nome);
            comando.Parameters.AddWithValue("@propriedade", obj.Propriedade);



            foreach (Equipamento str in GetEquipamento())
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
            throw new Exception("Erro ao atualizar o Equipamento: " + ex.Message, ex);
        }
    }
    public void Delete(Equipamento obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "delete from equipamento where id_equi = @id;";

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