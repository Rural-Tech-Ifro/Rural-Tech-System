using System;
using RuralTech.Database;

public class ExameDAO
{
    private static Conexao _conn = new Conexao();

    public void Insert(Exame obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO exame (tipo_exa, resultado_exa, data_exa, id_ani_fk) VALUES (@tipo, @resultado, @data, @idAnimal);";

            comando.Parameters.AddWithValue("@tipo", obj.Tipo);
            comando.Parameters.AddWithValue("@resultado", obj.Resultado);
            comando.Parameters.AddWithValue("@data", obj.Data);
            comando.Parameters.AddWithValue("@idAnimal", obj.Animal);


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