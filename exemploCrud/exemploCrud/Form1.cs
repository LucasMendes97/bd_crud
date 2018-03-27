using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace exemploCrud
{
    public partial class Form1 : Form
    {
        ExemploBasic n;
        public Form1()
        {
            InitializeComponent();
            n = new ExemploBasic(this);
            n.CarregarTabela();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Insere dados no combo estado
            cmbEstado.Items.Add("SP");
            cmbEstado.Items.Add("RJ");

            //Insere dados no combo cidade
            cmbCidade.Items.Add("Osasco");
            cmbCidade.Items.Add("Petrópolis");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            n.InserirDados();
            n.CarregarTabela();
            n.LimparControles();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            n.DeletaDados();
            n.CarregarTabela();
            n.LimparControles();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            n.BuscarDados();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            n.AtualizarDados();
            n.CarregarTabela();
            n.LimparControles();
        }

    }
}

