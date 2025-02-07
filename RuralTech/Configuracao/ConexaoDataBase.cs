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

        private static string port = "3360";

        private static string user = "root";

<<<<<<< HEAD
        private static string password = "200254";
=======
        private static string password = "root";
>>>>>>> 95534470f5da23413a483206fb1a993d57187f99

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
