using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace ClientLab
{
    public class SistemaDAO
    {
        private readonly ConexaoBanco _conexaoBanco;

        public SistemaDAO()
        {
            _conexaoBanco = new ConexaoBanco();
        }

        //Pessoa Fisica
        public void InserirPF(Pessoa_Fisica cliente)
        {
            using (var conn = _conexaoBanco.ObterConexao())
            {
                string query = "INSERT INTO tb_cliente_pf (NOME_CLIENTE, ENDERECO, CPF, RG) VALUES (@nome, @end, @cpf, @rg)";
                using (var cmd = mySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@end", cliente.Endereco);
                    cmd.Parameters.AddWithValue("@cpf", cliente.CPF);
                    cmd.Parameters.AddWithValue("@rg", cliente.RG);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("[!] (Pessoa Física) Cadastrada com Sucesso!");
            Console.WriteLine("Aperte qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }

        // Pessoa Juridica
        public void InserirPJ(Pessoa_Juridica cliente)
        {
            using (var conn = _conexaoBanco.ObterConexao())
            {
                conn.Open();

                string query = "INSERT INTO tb_cliente_pj (NOME_CLIENTE, ENDERECO, CNPJ, IE) VALUES (@nome, @end, @cnpj, @ie)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@end", cliente.Endereco);
                    cmd.Parameters.AddWithValue("@cnpj", cliente.CNPJ);
                    cmd.Parameters.AddWithValue("@ie", cliente.IE);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("[!] (Pessoa Jurídica) Cadastrada com Sucesso!");
            Console.WriteLine("Aperte qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }

        // Registrar Venda
        public void RegistrarVenda(string tipo, )
        {
            using (var conn = _conexaoBanco.ObterConexao())
            {
                conn.Open();
            }
        }

        // Listar venda
        public List<string> ListarVendas()
        {
            List<string> lista = new List<string>();

            using (var conn = _conexaoBanco.ObterConexao())
            {
                conn.Open();

                string sql = @"
                SELECT 
                v.ID_VENDA,
                IFNULL(pf.NOME_CLIENTE, pj.NOME_CLIENTE) AS CLIENTE,
                IFNULL(pf.ID_CLIENTE, pj.ID_CLIENTE) AS ID_CLIENTE,
                v.VALOR_COMPRA,
                v.VALOR_IMPOSTO,
                v.VALOR_TOTAL,
                v.data_hora_venda
                FROM tb_vendas v
                LEFT JOIN tb_cliente_pf pf ON v.FK_CLIENTE_PF = pf.ID_CLIENTE
                LEFT JOIN tb_cliente_pj pj ON v.FK_CLIENTE_PJ = pj.ID_CLIENTE;";

                var cmd = new MySqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string linha =
                        $"ID: {reader["ID_VENDA"]} | Cliente: {reader["CLIENTE"]} | " +
                        $"Valor: {reader["VALOR_COMPRA"]} | Imposto: {reader["VALOR_IMPOSTO"]} | " +
                        $"Total: {reader["VALOR_TOTAL"]} | Data: {reader["data_hora_venda"]}";

                    lista.Add(linha);
                }
            }

            return lista;
        }

        // CSV
        public void GerarCSV()
        {
            using (var conn = _conexaoBanco.ObterConexao())
            {
                conn.Open();

                string sql = @"
                SELECT 
                    v.ID_VENDA,
                    IFNULL(pf.NOME_CLIENTE, pj.NOME_CLIENTE) AS CLIENTE,
                    v.VALOR_COMPRA,
                    v.VALOR_IMPOSTO,
                    v.VALOR_TOTAL,
                    v.data_hora_venda
                FROM tb_vendas v
                LEFT JOIN tb_cliente_pf pf ON v.FK_CLIENTE_PF = pf.ID_CLIENTE
                LEFT JOIN tb_cliente_pj pj ON v.FK_CLIENTE_PJ = pj.ID_CLIENTE";

                var cmd = new MySqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                using (StreamWriter sw = new StreamWriter("relatorio.csv"))
                {
                    sw.WriteLine("ID,CLIENTE,VALOR,IMPOSTO,TOTAL,DATA");

                    while (reader.Read())
                    {
                        sw.WriteLine(
                            $"{reader["ID_VENDA"]}," +
                            $"{reader["CLIENTE"]}," +
                            $"{reader["VALOR_COMPRA"]}," +
                            $"{reader["VALOR_IMPOSTO"]}," +
                            $"{reader["VALOR_TOTAL"]}," +
                            $"{reader["data_hora_venda"]}"
                        );
                    }
                }
            }
        }
    }
}