using System;
using RuralTech.Database;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RuralTech.Helpers;
using RuralTech.Integracoes;

public class UsuarioDAO
{
    private static Conexao _conn = new Conexao();

    public void Insert(Usuario obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "INSERT INTO usuario (nome_usu, email_usu, senha_usu) VALUES (@nome, @email, @senha);";


            comando.Parameters.AddWithValue("@nome", obj.Nome);
            comando.Parameters.AddWithValue("@senha", obj.Senha);
            comando.Parameters.AddWithValue("@email", obj.Email);
           

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

    public List<Usuario> GetUsuario()
    {
        List<Usuario> usuarios = new List<Usuario>();

        try
        {
            var comando = _conn.Query();
            comando.CommandText = "SELECT nome_usu, email_usu, senha_usu FROM usuario";

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Usuario usuario = new Usuario
                {
                    Nome = reader.GetString("nome_usu"),
                    Senha = reader.GetString("senha_usu"),
                    Email = reader.GetString("email_usu"),
                };
                usuarios.Add(usuario);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return usuarios;
    }
    public void Update(Usuario obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "UPDATE usuario SET nome_usu = @nome, email_usu = @email, senha_usu = @senha WHERE id_usu = @id;";

            // Define os parâmetros para a atualização
            comando.Parameters.AddWithValue("@nome", obj.Nome);
            comando.Parameters.AddWithValue("@email", obj.Email);
            comando.Parameters.AddWithValue("@senha", obj.Senha);



            foreach (Usuario str in GetUsuario())
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
            throw new Exception("Erro ao atualizar o usuario: " + ex.Message, ex);
        }
    }
    public void Delete(Usuario obj)
    {
        try
        {
            var comando = _conn.Query();

            comando.CommandText = "delete from usuario where id_usu = @id;";

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