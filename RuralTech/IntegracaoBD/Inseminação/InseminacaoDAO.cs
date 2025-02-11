using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using RuralTech.Database;
using RuralTech.Helpers;

public class InseminacaoDAO
{
    private static Conexao _conn = new Conexao();

    public void Insert(Inseminacao obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO inseminacao (tipo_ins, observacao_ins, data_ins, id_ani_fk, id_fun_fk) VALUES (@tipo, @observação, @data, @idAnimal, @idFuncionário);";

            comando.Parameters.AddWithValue("@tipo", obj.Tipo);
            comando.Parameters.AddWithValue("@observação", obj.Observacao);
            comando.Parameters.AddWithValue("@data", obj.Data);
            comando.Parameters.AddWithValue("@idAnimal", obj.Animal);
            comando.Parameters.AddWithValue("@idFuncionário", obj.Funcionario);


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
    public List<Inseminacao> GetInseminacoes()
    {
        List<Inseminacao> inseminacoes = new List<Inseminacao>();

        try
        {
            var comando = _conn.Query();
            comando.CommandText = "SELECT inseminacao.id_ins, inseminacao.tipo_ins, inseminacao.observacao_ins, inseminacao.data_ins, animal.brinco_ani, funcionario.nome_fun from inseminacao inner JOIN Animal ON (inseminacao.id_ani_fk = animal.id_ani) inner JOIN Funcionario ON (inseminacao.id_fun_fk = funcionario.id_fun);";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Inseminacao inseminacao = new Inseminacao
                {
                    Id = DAOHelper.GetInt32(reader, "id_ins"),
                    Tipo = DAOHelper.GetString(reader, "tipo_ins"),
                    Observacao = DAOHelper.GetString(reader, "observacao_ins"),
                    Data = Convert.ToDateTime(DAOHelper.GetDateTime(reader, "data_ins")),
                    Animal = DAOHelper.GetString(reader, "brinco_ani"),
                    Funcionario = DAOHelper.GetString(reader, "nome_fun"),


                };
                inseminacoes.Add(inseminacao);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return inseminacoes;
    }
    public void Update(Inseminacao obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "UPDATE inseminacao SET tipo_ins = @tipo, observacao_ins = @observacao, data_ins = @data, id_ani_fk = @animal, id_fun_fk = @funcionario WHERE id_ins = @id;";

            // Define os parâmetros para a atualização
            comando.Parameters.AddWithValue("@tipo", obj.Tipo);
            comando.Parameters.AddWithValue("@observacao", obj.Observacao);
            comando.Parameters.AddWithValue("@data", obj.Data);
            comando.Parameters.AddWithValue("@Animal", obj.Animal);
            comando.Parameters.AddWithValue("@funcionario", obj.Funcionario);



            foreach (Inseminacao str in GetInseminacoes())
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
            throw new Exception("Erro ao atualizar a inseminacao: " + ex.Message, ex);
        }
    }
    public void Delete(Inseminacao obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "delete from inseminacao where id_ins = @id;";

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