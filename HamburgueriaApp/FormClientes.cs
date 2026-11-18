
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace HamburgueriaApp
{
    public partial class FormClientes : Form
    {
        // Paleta
        private static readonly Color Marrom = Color.FromArgb(62, 39, 35);
        private static readonly Color Mostarda = Color.FromArgb(245, 166, 35);
        private static readonly Color Creme = Color.FromArgb(250, 246, 240);
        private static readonly Color LinhaAlternada = Color.FromArgb(255, 243, 224);

        private ToolStripStatusLabel lblStatus;

        // Nome da tabela mostrado no combo -> SQL
        private static readonly Dictionary<string, string> Consultas = new Dictionary<string, string>
        {
            { "Clientes",            "SELECT * FROM clientes ORDER BY id_clientes" },
            { "Produtos",            "SELECT * FROM produtos ORDER BY id_produtos" },
            { "Ingredientes",        "SELECT * FROM ingredientes ORDER BY id_ingredientes" },
            { "Pedidos",             "SELECT * FROM pedidos ORDER BY id_pedidos" },
            { "Itens do Pedido",     "SELECT * FROM itens_do_pedido ORDER BY id_item" },
            { "Pagamentos",          "SELECT * FROM pagamentos ORDER BY id_pagamentos" },
            { "Produto Ingrediente", "SELECT * FROM produto_ingrediente" }
        };

        // Nome da coluna no banco -> título amigável
        private static readonly Dictionary<string, string> Titulos = new Dictionary<string, string>
        {
            { "id_clientes", "Cód. Cliente" },
            { "id_produtos", "Cód. Produto" },
            { "id_ingredientes", "Cód. Ingrediente" },
            { "id_pedidos", "Cód. Pedido" },
            { "id_item", "Cód. Item" },
            { "id_pagamentos", "Cód. Pagamento" },
            { "nome", "Nome" },
            { "telefone", "Telefone" },
            { "preco", "Preço" },
            { "preco_unitario", "Preço unitário" },
            { "categoria", "Categoria" },
            { "disponivel", "Disponível" },
            { "quantidade", "Qtd." },
            { "quantidade_estoque", "Estoque" },
            { "quantidade_utilizada", "Qtd. utilizada" },
            { "validade", "Validade" },
            { "status", "Status" },
            { "data_pedidos", "Data do pedido" },
            { "tipo_de_entrega", "Tipo de entrega" },
            { "forma_pagamento", "Forma de pagamento" },
            { "status_pagamento", "Status do pagamento" },
            { "data_pagamento", "Data do pagamento" },
            { "valor_pago", "Valor pago" }
        };

        public FormClientes()
        {
            InitializeComponent();
        }

        private void FormClientes_Load(object sender, EventArgs e)
        {
            AplicarVisual();

            foreach (var nome in Consultas.Keys)
                cmbTabelas.Items.Add(nome);

            // Carrega automaticamente ao trocar a tabela
            cmbTabelas.SelectedIndexChanged += (s, ev) => btnCarregar_Click(s, ev);
            cmbTabelas.SelectedIndex = 0;
        }

        private void AplicarVisual()
        {
            Text = "TSI Hamburgueria";
            Font = new Font("Segoe UI", 10F);
            BackColor = Creme;
            MinimumSize = new Size(700, 450);

            // Cabeçalho (reaproveita o pnlTopo sem os campos não usados)
            pnlTopo.Controls.Clear();
            pnlTopo.Height = 60;
            pnlTopo.Dock = DockStyle.Top;
            pnlTopo.BackColor = Marrom;
            pnlTopo.Controls.Add(new Label
            {
                Text = "TSI Hamburgueria",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(16, 14)
            });

            // Barra de filtro
            var barra = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 52,
                Padding = new Padding(12, 10, 12, 0)
            };
            cmbTabelas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTabelas.Width = 200;
            btnCarregar.Text = "Atualizar";
            btnCarregar.Size = new Size(110, 28);
            btnCarregar.FlatStyle = FlatStyle.Flat;
            btnCarregar.FlatAppearance.BorderSize = 0;
            btnCarregar.BackColor = Mostarda;
            btnCarregar.ForeColor = Marrom;
            btnCarregar.Cursor = Cursors.Hand;
            barra.Controls.Add(cmbTabelas);
            barra.Controls.Add(btnCarregar);

            // Grid
            dgvDados.Dock = DockStyle.Fill;
            dgvDados.BorderStyle = BorderStyle.None;
            dgvDados.BackgroundColor = Creme;
            dgvDados.ReadOnly = true;
            dgvDados.AllowUserToAddRows = false;
            dgvDados.RowHeadersVisible = false;
            dgvDados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDados.EnableHeadersVisualStyles = false;
            dgvDados.ColumnHeadersDefaultCellStyle.BackColor = Marrom;
            dgvDados.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDados.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvDados.ColumnHeadersHeight = 34;
            dgvDados.AlternatingRowsDefaultCellStyle.BackColor = LinhaAlternada;
            dgvDados.DefaultCellStyle.SelectionBackColor = Mostarda;
            dgvDados.DefaultCellStyle.SelectionForeColor = Marrom;

            // Barra de status
            var status = new StatusStrip();
            lblStatus = new ToolStripStatusLabel("Pronto");
            status.Items.Add(lblStatus);

            // Ordem importa no Dock: o Fill entra primeiro na coleção
            Controls.Clear();
            Controls.Add(dgvDados);
            Controls.Add(barra);
            Controls.Add(pnlTopo);
            Controls.Add(status);
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            string sql;
            if (!Consultas.TryGetValue(cmbTabelas.Text, out sql))
            {
                lblStatus.Text = "Selecione uma tabela.";
                return;
            }

            CarregarDados(sql);
        }

        private void CarregarDados(string sql)
        {
            try
            {
                using (var conn = Conexao.ObterConexao())
                {
                    conn.Open();

                    using (var da = new NpgsqlDataAdapter(sql, conn))
                    {
                        var tabela = new DataTable();
                        da.Fill(tabela);
                        dgvDados.DataSource = tabela;
                        FormatarColunas();
                        lblStatus.Text = tabela.Rows.Count + " registro(s) em " + cmbTabelas.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Erro ao carregar dados.";
                MessageBox.Show("Erro ao carregar dados: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Títulos amigáveis, formatos de moeda/data e destaque de validade vencida
        private void FormatarColunas()
        {
            foreach (DataGridViewColumn col in dgvDados.Columns)
            {
                string nome = col.Name;

                string titulo;
                if (Titulos.TryGetValue(nome, out titulo))
                    col.HeaderText = titulo;

                switch (nome)
                {
                    case "preco":
                    case "preco_unitario":
                    case "valor_pago":
                        col.DefaultCellStyle.Format = "C2";
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;

                    case "data_pedidos":
                    case "data_pagamento":
                        col.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                        break;

                    case "validade":
                        col.DefaultCellStyle.Format = "dd/MM/yyyy";
                        break;

                    case "quantidade":
                    case "quantidade_estoque":
                    case "quantidade_utilizada":
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                }
            }

            DestacarValidadeVencida();
        }

        private void DestacarValidadeVencida()
        {
            if (!dgvDados.Columns.Contains("validade")) return;

            foreach (DataGridViewRow linha in dgvDados.Rows)
            {
                object valor = linha.Cells["validade"].Value;
                if (valor is DateTime && ((DateTime)valor).Date < DateTime.Today)
                {
                    linha.Cells["validade"].Style.ForeColor = Color.Firebrick;
                    linha.Cells["validade"].Style.Font = new Font(dgvDados.Font, FontStyle.Bold);
                }
            }
        }
    }
}
