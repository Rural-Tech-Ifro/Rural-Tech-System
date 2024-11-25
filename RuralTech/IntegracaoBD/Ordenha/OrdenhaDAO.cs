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
    public void Update(Ordenha obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "UPDATE ordenha SET totalLitros_ord = @totalLitros, id_ani_fk = @animal, id_fun_fk = @funcionario WHERE id_ord = @id;";

            // Define os parâmetros para a atualização
            comando.Parameters.AddWithValue("@totalLitros", obj.TotalLitros);
            comando.Parameters.AddWithValue("@Animal", obj.Animal);
            comando.Parameters.AddWithValue("@funcionario", obj.Funcionario);



            foreach (Ordenha str in GetOrdenha())
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
            throw new Exception("Erro ao atualizar a Nome: " + ex.Message, ex);
        }
    }
    public void Delete(Ordenha obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "delete from ordenha where id_ord = @id;";

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