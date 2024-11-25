using System;
using RuralTech.Database;

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
}