using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; 

namespace RuralTech.Database
{
    internal class Conexao
    {
        private static string host = "localhost";

        private static string port = "3306";

        private static string user = "root";

<<<<<<< HEAD
        private static string password = "root";
=======
        private static string password = "Anjo 123";
>>>>>>> 54a4911379da197c93a63187fd6cf7741cd01d28

        private static string dbname = "RuralTech";

        private static MySqlConnection connection;

        private static MySqlCommand command;

        public Conexao()
        {
            try
            {
                connection = new MySqlConnection($"server={host};database={dbname};port={port};user={user};password={password}");
                connection.Open();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MySqlCommand Query()
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                return command;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
