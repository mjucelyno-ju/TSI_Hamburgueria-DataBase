namespace HamburgueriaApp
{
    partial class FormClientes
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvDados = new System.Windows.Forms.DataGridView();
            this.btnCarregar = new System.Windows.Forms.Button();
            this.pnlTopo = new System.Windows.Forms.Panel();
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).BeginInit();
            this.pnlTopo.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDados
            // 
            this.dgvDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDados.Location = new System.Drawing.Point(265, 248);
            this.dgvDados.Name = "dgvDados";
            this.dgvDados.Size = new System.Drawing.Size(357, 190);
            this.dgvDados.TabIndex = 0;
            // 
            // btnCarregar
            // 
            this.btnCarregar.Location = new System.Drawing.Point(405, 202);
            this.btnCarregar.Name = "btnCarregar";
            this.btnCarregar.Size = new System.Drawing.Size(75, 23);
            this.btnCarregar.TabIndex = 1;
            this.btnCarregar.Text = "Carregar";
            this.btnCarregar.UseVisualStyleBackColor = true;
            this.btnCarregar.Click += new System.EventHandler(this.btnCarregar_Click);
            // 
            // pnlTopo
            // 
            this.pnlTopo.Controls.Add(this.txtTelefone);
            this.pnlTopo.Controls.Add(this.lblTelefone);
            this.pnlTopo.Controls.Add(this.txtNome);
            this.pnlTopo.Controls.Add(this.lblNome);
            this.pnlTopo.Controls.Add(this.txtId);
            this.pnlTopo.Controls.Add(this.lblId);
            this.pnlTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopo.Location = new System.Drawing.Point(0, 0);
            this.pnlTopo.Name = "pnlTopo";
            this.pnlTopo.Size = new System.Drawing.Size(800, 180);
            this.pnlTopo.TabIndex = 2;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(218, 71);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(40, 13);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "Código";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(191, 99);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(100, 20);
            this.txtId.TabIndex = 1;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(365, 71);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(35, 13);
            this.lblNome.TabIndex = 2;
            this.lblNome.Text = "Nome";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(344, 99);
            this.txtNome.MaxLength = 100;
            this.txtNome.Name = "txtNome";
            this.txtNome.ReadOnly = true;
            this.txtNome.Size = new System.Drawing.Size(100, 20);
            this.txtNome.TabIndex = 3;
            // 
            // lblTelefone
            // 
            this.lblTelefone.AutoSize = true;
            this.lblTelefone.Location = new System.Drawing.Point(513, 71);
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.Size = new System.Drawing.Size(49, 13);
            this.lblTelefone.TabIndex = 4;
            this.lblTelefone.Text = "Telefone";
            // 
            // txtTelefone
            // 
            this.txtTelefone.Location = new System.Drawing.Point(494, 99);
            this.txtTelefone.MaxLength = 20;
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.ReadOnly = true;
            this.txtTelefone.Size = new System.Drawing.Size(100, 20);
            this.txtTelefone.TabIndex = 5;
            // 
            // FormClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlTopo);
            this.Controls.Add(this.btnCarregar);
            this.Controls.Add(this.dgvDados);
            this.Name = "FormClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clientes";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).EndInit();
            this.pnlTopo.ResumeLayout(false);
            this.pnlTopo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDados;
        private System.Windows.Forms.Button btnCarregar;
        private System.Windows.Forms.Panel pnlTopo;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.Label lblTelefone;
    }
}

