using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace HamburgueriaApp
{
    public partial class FormClientes : Form
    {
        public FormClientes()
        {
            InitializeComponent();
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = Conexao.ObterConexao())
                {
                    conn.Open();

                    string sql = "SELECT * FROM clientes"; // troque pelo nome de uma tabela sua
                    using (var da = new NpgsqlDataAdapter(sql, conn))
                    {
                        var tabela = new DataTable();
                        da.Fill(tabela);
                        dgvDados.DataSource = tabela;

                        if (tabela.Rows.Count == 0)
                        {
                            MessageBox.Show("Nenhum cliente encontrado!");
                        }
                        else
                        {
                            MessageBox.Show("Dados carregados com sucesso!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
    }
}
