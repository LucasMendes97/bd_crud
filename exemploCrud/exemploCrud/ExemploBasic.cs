using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace exemploCrud
{
    class ExemploBasic
    {
        Timer tempo = new Timer();
        private Form1 n;
        private MySqlDataAdapter daCountry;
        private DataSet dsCountry;

        public ExemploBasic(Form1 forma)
        {
            this.n = forma;

            tempo.Interval = 1000;
            tempo.Start();
            tempo.Tick += Tempo_Tick;
        }

        private void Tempo_Tick(object sender, EventArgs e)
        {           
            n.hora.Text = DateTime.Now.Hour.ToString()+":"+DateTime.Now.Minute.ToString();
        }

        //Limpa os dados do controle
        public void LimparControles()
        {
            n.cmbCidade.ResetText();
            n.cmbEstado.ResetText();
            n.txtCode.Clear();
            n.txtEndereco.Clear();
            n.txtNome.Clear();
        }
        //Carrega ou atualiza componente tabela
        public void CarregarTabela()
        {
            try
            {
                string connStr = "server=localhost;port=3307;User Id=root;database=bd_crud; password=usbw";
                MySqlConnection conn = new MySqlConnection(connStr);

                string sql = ("SELECT cd_dados,nm_nome,sg_estado, ds_endereco FROM tb_dados");
                daCountry = new MySqlDataAdapter(sql, conn);
                MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);

                dsCountry = new DataSet();
                daCountry.Fill(dsCountry, "tb_dados");
                n.dataGridView1.DataSource = dsCountry;
                n.dataGridView1.DataMember = "tb_dados";
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Insere dados na tabela
        public void InserirDados()
        {
            try
            {

                //Cria conexão
                MySqlConnection objcon = new MySqlConnection("server=localhost;port=3307;User Id=root;database=bd_crud; password=usbw");
                //Abre o bando de dados
                objcon.Open();
                //Comando slq para inserir dados na tabela
                MySqlCommand objCmd = new MySqlCommand("insert into tb_dados(cd_dados,nm_nome,sg_estado,nm_cidade,ds_endereco) value (null, ?, ?, ?, ?)", objcon);
                objCmd.Parameters.Add("@nm_nome", MySqlDbType.VarChar, 60).Value = n.txtNome.Text;
                objCmd.Parameters.Add("@sg_estado", MySqlDbType.VarChar, 2).Value = n.cmbEstado.SelectedItem.ToString();
                objCmd.Parameters.Add("@nm_cidade", MySqlDbType.VarChar, 20).Value = n.cmbCidade.SelectedItem.ToString();
                objCmd.Parameters.Add("@ds_endereco", MySqlDbType.VarChar, 100).Value = n.txtEndereco.Text;

                //comando para executar query
                objCmd.ExecuteNonQuery();

                //Fecha o banco de dados
                objcon.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }
        //Deleta dados da tabela
        public void DeletaDados()
        {
            try
            {
                //Abre o banco
                MySqlConnection objcon = new MySqlConnection("server=localhost;port=3307;User Id=root;database=bd_crud; password=usbw");
                objcon.Open();

                //comandos mysql
                MySqlCommand objCmd = new MySqlCommand("DELETE FROM tb_dados WHERE cd_dados =?", objcon);
                objCmd.Parameters.Clear();
                objCmd.Parameters.Add("@cd_dados", MySqlDbType.Int32).Value = n.txtCode.Text;

                //Executa comando
                objCmd.CommandType = CommandType.Text;
                objCmd.ExecuteNonQuery();

                //fecha conexão
                objcon.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }
        //Busca dados
        public void BuscarDados()
        {
            try
            {
                //passa a string de conexão
                MySqlConnection objcon = new MySqlConnection("server=localhost;port=3307;User Id=root;database=bd_crud; password=usbw");
                objcon.Open();

                //comandos mysql
                MySqlCommand objCmd = new MySqlCommand("select nm_nome, sg_estado, nm_cidade, ds_endereco from tb_dados where cd_dados = ?", objcon);
                objCmd.Parameters.Clear();
                objCmd.Parameters.Add("@cd_dados", MySqlDbType.Int32).Value = n.txtCode.Text;

                //Executa comando
                objCmd.CommandType = CommandType.Text;

                //Recebe conteudo que vem do banco
                MySqlDataReader dr = objCmd.ExecuteReader();
                dr.Read();
                n.txtNome.Text = dr.GetString(0);
                n.cmbEstado.Text = dr.GetString(1);
                n.cmbCidade.Text = dr.GetString(2);
                n.txtEndereco.Text = dr.GetString(3);

                //fecha conexão
                objcon.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }

        }
        //Atualiza Dados
        public void AtualizarDados()
        {
            try
            {
                //passa a string de conexão
                MySqlConnection objcon = new MySqlConnection("server=localhost;port=3307;User Id=root;database=bd_crud; password=usbw");
                objcon.Open();
                MySqlCommand objCmd = new MySqlCommand("update tb_dados set nm_nome =?, sg_estado =?, nm_cidade = ?, ds_endereco = ? where cd_dados =?", objcon);
                objCmd.Parameters.Clear();

                objCmd.Parameters.Add("@nm_nome", MySqlDbType.VarChar, 60).Value = n.txtNome.Text;
                objCmd.Parameters.Add("@sg_estado", MySqlDbType.VarChar, 2).Value = n.cmbEstado.SelectedItem;
                objCmd.Parameters.Add("@nm_cidade", MySqlDbType.VarChar, 20).Value = n.cmbCidade.SelectedItem;
                objCmd.Parameters.Add("@ds_endereco", MySqlDbType.VarChar, 100).Value = n.txtEndereco.Text;
                objCmd.Parameters.Add("cd_dados", MySqlDbType.Int32).Value = n.txtCode.Text;

                //executando comando
                objCmd.CommandType = CommandType.Text;
                objCmd.ExecuteNonQuery();

                //fecha conexão
                objcon.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }
    }
}
