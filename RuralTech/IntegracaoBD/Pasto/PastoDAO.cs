using System;
using RuralTech.Database;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RuralTech.Helpers;
using RuralTech.Integracoes;

public class PastoDAO
{
    private static Conexao _conn = new Conexao();

    public void Insert(Pasto obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO Pasto (limete_pas, descricao_pas, tipo_pas, tamanho_pas, id_pro_fk) VALUES (@limite, @descricao, @tipo, @tamanho, @diPropriedade);";

            comando.Parameters.AddWithValue("@limite", obj.Limite);
            comando.Parameters.AddWithValue("@descricao", obj.Descricao);
            comando.Parameters.AddWithValue("@tipo", obj.TipoPastagem);
            comando.Parameters.AddWithValue("@tamanho", obj.Tamanho);
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

    public List<Pasto> GetPasto()
    {
        List<Pasto> pastos = new List<Pasto>();

        try
        {
            var comando = _conn.Query();
            comando.CommandText = "SELECT Propriedade.nome_pro, Pasto.limete_pas, Pasto.descricao_pas, Pasto.tipo_pas, Pasto.tamanho_pas FROM Propriedade inner join Pasto on (Pasto.id_pro_fk = Propriedade.id_pro);";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Pasto pasto = new Pasto
                {
                    Id = reader.GetInt32("id_ani"),
                    Limite = reader.GetInt32("limite_pas"),
                    Descricao = reader.GetString("descricao_pas"),
                    TipoPastagem = reader.GetString("tipo_pas"),
                    Tamanho = reader.GetInt32("tamanho_pas"),
                    Propriedade = reader.GetString("nome_pro"),
                };
                pastos.Add(pasto);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return pastos;
    }
}