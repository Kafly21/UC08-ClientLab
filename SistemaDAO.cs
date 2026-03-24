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
                string query = @"INSERT INTO tb_cliente_pf 
                (NOME_CLIENTE, ENDERECO, CPF, RG) 
                VALUES (@nome, @end, @cpf, @rg)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@end", cliente.Endereco);
                    cmd.Parameters.AddWithValue("@cpf", cliente.CPF);
                    cmd.Parameters.AddWithValue("@rg", cliente.RG);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("[!] Pessoa Física cadastrada!");
            Console.WriteLine("Aperte qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }

        // Pessoa Juridica
        public void InserirPJ(Pessoa_Juridica cliente)
        {
            using (var conn = _conexaoBanco.ObterConexao())
            {
                string query = @"INSERT INTO tb_cliente_pj 
                (NOME_CLIENTE, ENDERECO, CNPJ, IE) 
                VALUES (@nome, @end, @cnpj, @ie)";

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

            Console.WriteLine("[!] Pessoa Jurídica cadastrada!");
            Console.WriteLine("Aperte qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }

        // Registrar Venda
        public void RegistrarVenda(Cliente cliente, double valor)
        {
            double imposto = cliente.Pagar_Imposto(valor);
            double total = valor + imposto;

            using (var conn = _conexaoBanco.ObterConexao())
            {
                string query = @"
                INSERT INTO tb_vendas 
                (FK_CLIENTE_PF, FK_CLIENTE_PJ, VALOR_COMPRA, VALOR_IMPOSTO, VALOR_TOTAL)
                VALUES (@pf, @pj, @valor, @imposto, @total)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    if (cliente is Pessoa_Fisica)
                    {
                        cmd.Parameters.AddWithValue("@pf", cliente.ID);
                        cmd.Parameters.AddWithValue("@pj", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@pf", DBNull.Value);
                        cmd.Parameters.AddWithValue("@pj", cliente.ID);
                    }

                    cmd.Parameters.AddWithValue("@valor", valor);
                    cmd.Parameters.AddWithValue("@imposto", imposto);
                    cmd.Parameters.AddWithValue("@total", total);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("[!] Venda registrada!");

        }

        //Listagens de Vendas
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
                v.VALOR_COMPRA,
                v.VALOR_IMPOSTO,
                v.VALOR_TOTAL
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
                        $"Total: {reader["VALOR_TOTAL"]}";

                    lista.Add(linha);
                }
            }

            return lista;

        }

        // CSV
        public void GerarRelatorioCSV()
        {
            string path = "relatorio.csv";
            string conteudo = "ID;Cliente;Valor;Imposto;Total\n";

            using (var conn = _conexaoBanco.ObterConexao())
            {
                conn.Open();

                string sql = @"
                SELECT 
                v.ID_VENDA,
                IFNULL(pf.NOME_CLIENTE, pj.NOME_CLIENTE) AS CLIENTE,
                v.VALOR_COMPRA,
                v.VALOR_IMPOSTO,
                v.VALOR_TOTAL
                FROM tb_vendas v
                LEFT JOIN tb_cliente_pf pf ON v.FK_CLIENTE_PF = pf.ID_CLIENTE
                LEFT JOIN tb_cliente_pj pj ON v.FK_CLIENTE_PJ = pj.ID_CLIENTE;";

                var cmd = new MySqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    conteudo +=
                        $"{reader["ID_VENDA"]};" +
                        $"{reader["CLIENTE"]};" +
                        $"{reader["VALOR_COMPRA"]};" +
                        $"{reader["VALOR_IMPOSTO"]};" +
                        $"{reader["VALOR_TOTAL"]}\n";
                }
            }

            File.WriteAllText(path, conteudo);

            Console.WriteLine($"[!] Sucesso! Arquivo gerado: {Path.GetFullPath(path)}");
            Console.WriteLine("Aperte qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }
    }
}
