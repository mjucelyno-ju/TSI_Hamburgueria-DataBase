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
    public partial class Form1 : Form
    {
        public Form1()
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
