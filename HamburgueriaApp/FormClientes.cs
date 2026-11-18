
using System;
using System.Data;
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

        // Evento do botão Carregar
        private void btnCarregar_Click(object sender, EventArgs e)
        {
            string sql = "";

            switch (cmbTabelas.Text)
            {
                case "Clientes":
                    sql = "SELECT * FROM clientes";
                    break;

                case "Produtos":
                    sql = "SELECT * FROM produtos";
                    break;

                case "Ingredientes":
                    sql = "SELECT * FROM ingredientes";
                    break;

                case "Pedidos":
                    sql = "SELECT * FROM pedidos";
                    break;

                case "Itens do Pedido":
                    sql = "SELECT * FROM itens_do_pedido";
                    break;

                case "Pagamentos":
                    sql = "SELECT * FROM pagamentos";
                    break;

                case "Produto Ingrediente":
                    sql = "SELECT * FROM produto_ingrediente";
                    break;

                default:
                    MessageBox.Show("Selecione uma tabela!");
                    return;
            }

            // Chama o método para carregar os dados
            CarregarDados(sql);
        }

        // Método responsável por consultar o banco
        private void CarregarDados(string sql)
        {
            try
            {
                using (var conn = Conexao.ObterConexao())
                {
                    conn.Open();

                    using (var da = new NpgsqlDataAdapter(sql, conn))
                    {
                        DataTable tabela = new DataTable();

                        da.Fill(tabela);

                        dgvDados.DataSource = tabela;
                    }
                }

                MessageBox.Show("Dados carregados com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        // Evento executado quando o formulário abre
        private void FormClientes_Load(object sender, EventArgs e)
        {
            cmbTabelas.Items.Add("Clientes");
            cmbTabelas.Items.Add("Produtos");
            cmbTabelas.Items.Add("Ingredientes");
            cmbTabelas.Items.Add("Pedidos");
            cmbTabelas.Items.Add("Itens do Pedido");
            cmbTabelas.Items.Add("Pagamentos");
            cmbTabelas.Items.Add("Produto Ingrediente");

            cmbTabelas.SelectedIndex = 0;
        }
    }
}
