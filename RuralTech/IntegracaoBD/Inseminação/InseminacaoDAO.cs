using System;
using RuralTech.Database;

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
}