﻿using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using RuralTech.Database;
using RuralTech.Helpers;

public class TransporteDAO
{
    private static Conexao _conn = new Conexao();

    public void Insert(Transporte obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO transporte (cpf_tra, cnpj_tra, nome_tra, inscricaoEstadual_tra, estado_tra, cidade_tra, bairro_tra, rua_tra, cep_tra) VALUES (@Cpf, @Cnpj, @Nome, @InscricaoEstadual, @Estado, @Cidade, @Bairro, @Rua, @Cep);";

            comando.Parameters.AddWithValue("@Cpf", obj.Cpf);
            comando.Parameters.AddWithValue("@Cnpj", obj.Cnpj);
            comando.Parameters.AddWithValue("@Nome", obj.Nome);
            comando.Parameters.AddWithValue("@InscricaoEstadual", obj.InscricaoEstadual);
            comando.Parameters.AddWithValue("@Estado", obj.Estado);
            comando.Parameters.AddWithValue("@Cidade", obj.Cidade);
            comando.Parameters.AddWithValue("@Bairro", obj.Bairro);
            comando.Parameters.AddWithValue("@Rua", obj.Rua);
            comando.Parameters.AddWithValue("@Cep", obj.Cep);


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
    public List<Transporte> GetTransportes()
    {
        List<Transporte> transportes = new List<Transporte>();

        try
        {
            var comando = _conn.Query();
            comando.CommandText = "SELECT id_tra, cpf_tra, cnpj_tra, nome_tra, inscricaoEstadual_tra, estado_tra, cidade_tra, bairro_tra, rua_tra, cep_tra FROM trsnaporte;";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Transporte transporte = new Transporte
                {
                    Id = DAOHelper.GetInt32(reader, "id_tra"),
                    Cpf = DAOHelper.GetString(reader, "cpf_tra"),
                    Cnpj = DAOHelper.GetString(reader, "cnpj_tra"),
                    Nome = DAOHelper.GetString(reader, "nome_tra"),
                    InscricaoEstadual = DAOHelper.GetString(reader, "inscricaoEstadual_tra"),
                    Estado = DAOHelper.GetString(reader, "estado_tra"),
                    Cidade = DAOHelper.GetString(reader, "cidade_tra"),
                    Bairro = DAOHelper.GetString(reader, "bairro_tra"),
                    Rua = DAOHelper.GetString(reader, "rua_tra"),
                    Cep = DAOHelper.GetString(reader, "cep_tra"),

                };
                transportes.Add(transporte);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return transportes;
    }
    public void Update(Transporte obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "UPDATE transporte SET cpf_tra = @cpf, cnpj_tra = @cnpj, nome_tra = @nome, inscricaoEstadual_tra = @inscricao, estado_tra = @estado, cidade_tra = @cidade, bairro_tra = @bairro, rua_tra = @rua, cep_tra = @cep WHERE id_tra = @id;";

            // Define os parâmetros para a atualização
            comando.Parameters.AddWithValue("@cpf", obj.Cpf);
            comando.Parameters.AddWithValue("@cnpj", obj.Cnpj);
            comando.Parameters.AddWithValue("@nome", obj.Nome);
            comando.Parameters.AddWithValue("@inscricao", obj.InscricaoEstadual);
            comando.Parameters.AddWithValue("@estado", obj.Estado);
            comando.Parameters.AddWithValue("@cidade", obj.Cidade);
            comando.Parameters.AddWithValue("@bairro", obj.Bairro);
            comando.Parameters.AddWithValue("@rua", obj.Rua);
            comando.Parameters.AddWithValue("@cep", obj.Cep);



            foreach (Transporte str in GetTransportes())
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
            throw new Exception("Erro ao atualizar o transporte: " + ex.Message, ex);
        }
    }
    public void Delete(Transporte obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "delete from transporte where id_tra = @id;";

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