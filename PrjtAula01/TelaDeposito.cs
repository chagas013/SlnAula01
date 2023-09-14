﻿using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrjtAula01
{
    public partial class TelaDeposito : Form
    {
        public TelaDeposito()
        {
            InitializeComponent();
        }

        private void lblVoltarDeposito_Click(object sender, EventArgs e)
        {


        }

        private void Extra_Load(object sender, EventArgs e)
        {

        }

        private void btConfirmarDeposito_Click(object sender, EventArgs e)
        {

                        
            try
            {

                if (Convert.ToInt32(txtValorDeposito.Text) <= 0 || decimal.TryParse(txtValorDeposito.Text, out decimal result) == false)
                {
                    throw new Exception("Insira um valor acima de R$ 0,00");
                }

                else if (txtValorDeposito.Text == string.Empty)
                {
                    throw new Exception("Digite algum valor!");
                }

                else
                {

                    Conta conta = new Conta();
                    conta.IdConta
                    conta.StatusConta = "ATIVA";
                    conta.TipoConta = "Corrente";
                    conta.Limite = UsuarioLogado.RendaMensal * 0.3m;
                    conta.Saldo = conta.Saldo + Convert.ToDecimal(txtValorDeposito.Text);

                    SqlConnection conexao =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["PrjtAula01.Properties.Settings.strConexao"].ToString());

                    
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "pu_conta";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conexao;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("IdConta", UsuarioLogado.IdConta);
                    cmd.Parameters.AddWithValue("statusConta", conta.StatusConta);
                    cmd.Parameters.AddWithValue("tipoConta", conta.TipoConta);
                    cmd.Parameters.AddWithValue("saldo", Convert.ToDecimal(txtValorDeposito.Text));
                    cmd.Parameters.AddWithValue("limite", conta.Limite);
                    cmd.Parameters.AddWithValue("senhaConta", UsuarioLogado.Senha);

                    conexao.Open();

                    cmd.ExecuteNonQuery();

                    conexao.Close();

                    UIclear.limparTelas(this);

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
