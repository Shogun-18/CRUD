using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPessoa3
{
    public partial class FrmPessoa : Form
    {
        public FrmPessoa()
        {
            InitializeComponent();
        }

        private void FrmPessoa_Load(object sender, EventArgs e)
        {
            Pessoa pessoa = new Pessoa();
            List<Pessoa> pessoas = pessoa.listapessoas();
            dgvPessoa.DataSource = pessoas;
            btnAtualizar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Deseja realmente sair do aplicativo?","Fechar",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNome.Text == "" && txtCidade.Text == "" && txtEndereco.Text == "")
                {
                    MessageBox.Show("Por favor, preencha o formulário para inserir!");
                    this.txtNome.Focus();
                }
                else
                {
                    Pessoa pessoa = new Pessoa();
                    if (pessoa.RegistroRepetido(txtNome.Text,txtCelular.Text,txtEmail.Text) != false)
                    {
                        MessageBox.Show("Este cliente já está em nossa base de dados!");
                        txtNome.Text = "";
                        txtCidade.Text = "";
                        txtEndereco.Text = "";
                        txtCelular.Text = "";
                        txtDataNascimento.Text = "";
                        txtEmail.Text = "";
                        this.txtNome.Focus();
                    }
                    else
                    {
                        pessoa.Inserir(txtNome.Text, txtCidade.Text, txtEndereco.Text, txtDataNascimento.Text,txtCelular.Text, txtEmail.Text);
                        MessageBox.Show("Cliente cadastraddo com sucesso!");
                        List<Pessoa> pessoas = pessoa.listapessoas();
                        dgvPessoa.DataSource = pessoas;
                        txtNome.Text = "";
                        txtCidade.Text = "";
                        txtEndereco.Text = "";
                        txtCelular.Text = "";
                        txtDataNascimento.Text = "";
                        txtEmail.Text = "";
                        this.txtNome.Focus();
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Por favor digite um ID!");
                this.txtId.Focus();
            }
            else
            {
                int Id = Convert.ToInt32(txtId.Text.Trim());
                Pessoa pessoa = new Pessoa();
                pessoa.Localizar(Id);
                txtNome.Text = pessoa.nome;
                txtCidade.Text = pessoa.cidade;
                txtEndereco.Text = pessoa.endereco;
                txtCelular.Text = pessoa.celular;
                txtDataNascimento.Text = pessoa.data_nascimento;
                txtEmail.Text = pessoa.email;
                btnAtualizar.Enabled = true;
                btnExcluir.Enabled = true;
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(txtId.Text.Trim());
            Pessoa pessoa = new Pessoa();
            pessoa.Excluir(Id);
            MessageBox.Show("Cliente apagado com sucesso!");
            List<Pessoa> pessoas = pessoa.listapessoas();
            dgvPessoa.DataSource = pessoas;
            txtId.Text = "";
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtEmail.Text = "";
            txtDataNascimento.Text = "";
            txtCidade.Text = "";
            txtCelular.Text = "";
            this.txtNome.Focus();
            btnAtualizar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(txtId.Text.Trim());
            Pessoa pessoa = new Pessoa();
            pessoa.Atualizar(Id,txtNome.Text,txtCidade.Text,txtEndereco.Text,txtDataNascimento.Text,txtEmail.Text,txtCelular.Text);
            MessageBox.Show("Cliente atualizado com sucesso!");
            List<Pessoa> pessoas = pessoa.listapessoas();
            dgvPessoa.DataSource = pessoas;
            txtId.Text = "";
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtEmail.Text = "";
            txtDataNascimento.Text = "";
            txtCidade.Text = "";
            txtCelular.Text = "";
            this.txtNome.Focus();
            btnAtualizar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void dgvPessoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvPessoa.Rows[e.RowIndex];
                row.Selected = true;
                txtId.Text = row.Cells[0].Value.ToString();
                txtNome.Text = row.Cells[1].Value.ToString();
                txtCidade.Text = row.Cells[2].Value.ToString();
                txtEndereco.Text = row.Cells[3].Value.ToString();
                txtDataNascimento.Text = row.Cells[4].Value.ToString();
                txtCelular.Text = row.Cells[5].Value.ToString();
                txtEmail.Text = row.Cells[6].Value.ToString();
            }
            btnAtualizar.Enabled = true;
            btnExcluir.Enabled = true;
        }
    }
}
