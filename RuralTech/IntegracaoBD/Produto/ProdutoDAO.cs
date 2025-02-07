using System;
using RuralTech.Database;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RuralTech.Helpers;
using RuralTech.Integracoes;

public class ProdutoDAO
{
    private static Conexao _conn = new Conexao();

    public void Insert(Produto obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO Produto (precoCusto_prod, unidadeSaida_prod, unidadeEntrada_prod, precoVenda_prod, quantidade_prod, observacao_prod, nome_prod, dataVencimento_prod) VALUES (@precoCusto, @unidadeSaida, @unidadeEntrada, @precoVenda, @quantidade, @observacao, @nome, @dataVencimento);";

            comando.Parameters.AddWithValue("@precoCusto", obj.PrecoCusto);
            comando.Parameters.AddWithValue("@unidadeSaida", obj.UnidadeSaida);
            comando.Parameters.AddWithValue("@unidadeEntrada", obj.UnidadeEntrada);
            comando.Parameters.AddWithValue("@precoVenda", obj.PrecoVenda);
            comando.Parameters.AddWithValue("@quantidade", obj.Quantidade);
            comando.Parameters.AddWithValue("@observacao", obj.Observacao);
            comando.Parameters.AddWithValue("@nome", obj.Nome);
            comando.Parameters.AddWithValue("@dataVencimento", obj.DataVencimento);

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

    public List<Produto> GetProduto()
    {
        List<Produto> produtos = new List<Produto>();

        try
        {
            var comando = _conn.Query();
            comando.CommandText = "SELECT id_prod, precoCusto_prod, unidadeSaida_prod, unidadeEntrada_prod, precoVenda_prod, quantidade_prod, observacao_prod, nome_prod, dataVencimento_prod FROM Produto;";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Produto produto = new Produto
                {
                    Id = DAOHelper.GetInt32(reader, "id_prod"),
                    PrecoCusto = DAOHelper.GetDouble(reader, "precoCusto_prod"),
                    UnidadeSaida = DAOHelper.GetString(reader, "unidadeSaida_prod"),
                    UnidadeEntrada = DAOHelper.GetString(reader, "unidadeEntrada_prod"),
                    PrecoVenda = DAOHelper.GetDouble(reader, "precoVenda_prod"),
                    Quantidade = DAOHelper.GetInt32(reader, "quantidade_prod"),
                    Observacao = DAOHelper.GetString(reader, "observacao_prod"),
                    Nome = DAOHelper.GetString(reader, "nome_prod"),
                    DataVencimento = Convert.ToDateTime(DAOHelper.GetDateTime(reader, "dataVencimento_prod"))
                };
                produtos.Add(produto);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return produtos;
    }

    
    public void Delete(Produto obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "delete from produto where id_prod = @id;";

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