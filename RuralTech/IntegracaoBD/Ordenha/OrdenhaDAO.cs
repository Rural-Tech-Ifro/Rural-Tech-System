using System;
using RuralTech.Database;

public class OrdenhaDAO
{
    private static Conexao _conn = new Conexao();

    public void Insert(Ordenha obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO ordenha (totalLitros_ord, id_ani_fk, id_fun_fk) VALUES (@TotalLitros, @idAnimal, @idFuncionário);";

            comando.Parameters.AddWithValue("@TotalLitros", obj.TotalLitros);
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