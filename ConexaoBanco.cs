using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ClientLab
{
    public class ConexaoBanco
    {
        private readonly string stringConexao = "Server=localhost;Database=clientlab;Uid=root;Pwd=root;";
    
        public MySqlConnection ObterConexao()
        {
            return new MySqlConnection(stringConexao);
        }
    }
}