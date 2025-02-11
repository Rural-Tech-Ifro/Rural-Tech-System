using System;
using RuralTech.Database;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RuralTech.Helpers;
using RuralTech.Integracoes;
using RuralTech.Telas;
using System.Windows;

public class CompraDAO
{
    private static Conexao _conn = new Conexao();

    public void Insert(Compra obj)
    {
        try
        {
            var comando = _conn.Query();

            // Inserir na tabela `compra`
            comando.CommandText = "INSERT INTO compra (codigo_com, formaPagamento_com, dataCompra_com, dataPagamento_com, quantidadeParcela, id_fun_fk) VALUES (@codigo, @formaPagamento, @dataCompra, @dataPagamento, @quantidadeParcelas, @idFuncionario);";
            comando.Parameters.AddWithValue("@codigo", obj.Codigo);
            comando.Parameters.AddWithValue("@formaPagamento", obj.FormaPagamento);
            comando.Parameters.AddWithValue("@dataCompra", obj.DataCompra);
            comando.Parameters.AddWithValue("@dataPagamento", obj.DataPagamento);
            comando.Parameters.AddWithValue("@quantidadeParcelas", obj.QuantidadeParcelas);
            comando.Parameters.AddWithValue("@idFuncionario", obj.Funcionario);

            // Executar o comando e capturar o ID gerado
            comando.ExecuteNonQuery();
            comando.CommandText = "SELECT LAST_INSERT_ID();";
            int idCompra = Convert.ToInt32(comando.ExecuteScalar());

            // Inserir na tabela `fornecedor_compra`
            var comandoFornecedor = _conn.Query();
            comandoFornecedor.CommandText = "INSERT INTO fornecedor_compra (id_for_fk, id_com_fk) VALUES (@idFornecedor, @idCompra);";
            comandoFornecedor.Parameters.AddWithValue("@idFornecedor", obj.Fornecedor);
            comandoFornecedor.Parameters.AddWithValue("@idCompra", idCompra);
            comandoFornecedor.ExecuteNonQuery();

            // Inserir na tabela `produto_compra`
            var comandoProduto = _conn.Query();
            comandoProduto.CommandText = "INSERT INTO produto_compra (id_prod_fk, id_com_fk) VALUES (@idProduto, @idCompra);";
            comandoProduto.Parameters.AddWithValue("@idProduto", obj.Produto);
            comandoProduto.Parameters.AddWithValue("@idCompra", idCompra);
            comandoProduto.ExecuteNonQuery();

            // Checar se os comandos foram executados corretamente
            if (idCompra == 0)
            {
                throw new Exception("Ocorreram erros ao salvar as informações da compra.");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<Compra> GetCompras()
    {
        List<Compra> compras = new List<Compra>();

        try
        {
            var comando = _conn.Query();
            comando.CommandText = "SELECT Compra.id_com, Compra.codigo_com, Compra.formaPagamento_com, Compra.dataCompra_com, Compra.dataPagamento_com, Compra.quantidadeParcela, Produto.nome_prod, Fornecedor.nome_for, Funcionario.nome_fun FROM Compra inner JOIN produto_compra ON (Compra.id_com = produto_compra.id_com_fk) inner JOIN Produto ON (Produto.id_prod = produto_compra.id_prod_fk) inner JOIN fornecedor_compra ON (Compra.id_com = fornecedor_compra.id_com_fk) inner JOIN Fornecedor ON (Fornecedor.id_for = fornecedor_compra.id_for_fk) inner join Funcionario on (Compra.id_fun_fk = Funcionario.id_fun);";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Compra compra = new Compra
                {
                    Id = DAOHelper.GetInt32(reader, "id_com"),
                    Codigo = DAOHelper.GetString(reader, "codigo_com"),
                    FormaPagamento = DAOHelper.GetString(reader, "formaPagamento_com"),
                    DataCompra = Convert.ToDateTime(DAOHelper.GetDateTime(reader, "dataCompra_com")),
                    DataPagamento = Convert.ToDateTime(DAOHelper.GetDateTime(reader, "dataPagamento_com")),
                    QuantidadeParcelas = DAOHelper.GetInt32(reader, "quantidadeParcela"),
                    Produto = DAOHelper.GetString(reader, "nome_prod"),
                    Fornecedor = DAOHelper.GetString(reader, "nome_for"),
                    Funcionario = DAOHelper.GetString(reader, "nome_fun"),
                };
                compras.Add(compra);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return compras;
    }

    public void Update(Compra obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = @"
        UPDATE compra 
        SET codigo_com = @codigo, formaPagamento_com = @formaPagamento, dataCompra_com = @dataCompra, 
            dataPagamento_com = @dataPagamento, quantidadeParcela = @quantidadeParcelas, id_fun_fk = @idFuncionario 
        WHERE id_com = @id;

        UPDATE Fornecedor_Compra 
        SET id_for_fk = @idFornecedor 
        WHERE id_com_fk = @id;

        UPDATE Produto_Compra 
        SET id_prod_fk = @idProduto 
        WHERE id_com_fk = @id;
    ";

            comando.Parameters.AddWithValue("@id", obj.Id);
            comando.Parameters.AddWithValue("@codigo", obj.Codigo);
            comando.Parameters.AddWithValue("@formaPagamento", obj.FormaPagamento);
            comando.Parameters.AddWithValue("@dataCompra", obj.DataCompra);
            comando.Parameters.AddWithValue("@dataPagamento", obj.DataPagamento);
            comando.Parameters.AddWithValue("@quantidadeParcelas", obj.QuantidadeParcelas);
            comando.Parameters.AddWithValue("@idFuncionario", obj.Funcionario);
            comando.Parameters.AddWithValue("@idFornecedor", obj.Fornecedor);
            comando.Parameters.AddWithValue("@idProduto", obj.Produto);

            var resultado = comando.ExecuteNonQuery();

            if (resultado == 0)
            {
                throw new Exception("Ocorreram erros ao atualizar as informações");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao atualizar a Compra: " + ex.Message, ex);
        }

    }

    public void Delete(Compra obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "DELETE FROM compra WHERE id_com = @id;";
            comando.CommandText = "DELETE FROM fornecedor_compra WHERE id_com_fk = @id;";
            comando.CommandText = "DELETE FROM produto_compra WHERE id_com_fk = @id;";

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

