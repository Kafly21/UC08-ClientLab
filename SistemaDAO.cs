using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlCliente;
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
        public void InserirPF(Pessoa_Fisica pf)
        {
            using (var conn = _conexaoBanco.Conectar())
            {
                conn.Open();

                string sql = "INSERT INTO tb_cliente_pf (NOME_CLIENTE, ENDERECO, CPF, RG) VALUES (@nome, @end, @cpf, @rg)";
                var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nome", pf.Nome);
                cmd.Parameters.AddWithValue("@end", pf.Endereco);
                cmd.Parameters.AddWithValue("@cpf", pf.CPF);
                cmd.Parameters.AddWithValue("@rg", pf.RG);

                cmd.ExecuteNonQuery();
            }
        }

        // Pessoa Juridica
        public void InserirPJ(Pessoa_Juridica pj)
        {
            using (var conn = _conexaoBanco.Conectar())
            {
                conn.Open();

                string sql = "INSERT INTO tb_cliente_pj (NOME_CLIENTE, ENDERECO, CNPJ, IE) VALUES (@nome, @end, @cnpj, @ie)";
                var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nome", pj.Nome);
                cmd.Parameters.AddWithValue("@end", pj.Endereco);
                cmd.Parameters.AddWithValue("@cnpj", pj.CNPJ);
                cmd.Parameters.AddWithValue("@ie", pj.IE);

                cmd.ExecuteNonQuery();
            }
        }

        // Registrar Venda
        public void RegistrarVenda(Cliente cliente, string tipo)
        {
            using (var conn = _conexaoBanco.Conectar())
            {
                conn.Open();

                string sql = @"INSERT INTO tb_vendas 
                (FK_CLIENTE_PF, FK_CLIENTE_PJ, VALOR_COMPRA, VALOR_IMPOSTO, VALOR_TOTAL, data_hora_venda)
                VALUES (@pf, @pj, @valor, @imposto, @total, NOW())";

                var cmd = new MySqlCommand(sql, conn);

                if (tipo == "f")
                {
                    cmd.Parameters.AddWithValue("@pf", cliente.ID);
                    cmd.Parameters.AddWithValue("@pj", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pf", DBNull.Value);
                    cmd.Parameters.AddWithValue("@pj", cliente.ID);
                }

                cmd.Parameters.AddWithValue("@valor", cliente.Valor_Compra);
                cmd.Parameters.AddWithValue("@imposto", cliente.Valor_Imposto);
                cmd.Parameters.AddWithValue("@total", cliente.Total_Pagar);

                cmd.ExecuteNonQuery();
            }
        }

        // Listar venda
        public List<string> ListarVendas()
        {
            List<string> lista = new List<string>();

            using (var conn = _conexaoBanco.Conectar())
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
                LEFT JOIN tb_cliente_pj pj ON v.FK_CLIENTE_PJ = pj.ID_CLIENTE;

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
            using (var conn = _conexaoBanco.Conectar())
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
