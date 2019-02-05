using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login_docente : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		
	}
	
    protected void botaoEntrar_Click(object sender, EventArgs e)
    {
		if (input_ra_login.Text != "" && id_input_senha_login.Text != "")
		{
			SqlConnection con = new SqlConnection(new ClsFuncoes().conexao);
			con.Open();
			string query = "SELECT COUNT(*) FROM PROFESSOR WHERE RA='" + input_ra_login.Text + "' AND SENHA = '" + id_input_senha_login.Text + "' ";

			SqlCommand cmd = new SqlCommand(query, con);
			string output = cmd.ExecuteScalar().ToString();

			if (output == "1")
			{
				Session["user"] = input_ra_login.Text;
				Response.Redirect("~/portal-professor/portal-professor.aspx");
			}
			else
			{
				id_alerta_campos_vazio.Visible = false;
				id_alerta_dados.Visible = true;
			}
		}
		else
		{
			id_alerta_campos_vazio.Visible = true;
			id_alerta_dados.Visible = false;
		}

	}

    protected void direcionarHome(object sender, EventArgs e)
    {
        Response.Redirect("../home.aspx");
    }

    protected void direcionarPortaAluno(object sender, EventArgs e)
    {
        Response.Redirect("../login-aluno/login-aluno.aspx");
    }

	protected void recuperarSenha(object sender, EventArgs e)
	{
		divRecuperarSenha.Visible = true;
	}

	protected void btnSalvar_Click(object sender, EventArgs e)
	{
		if (id_input_nome.Text != "" && id_input_ra.Text != "" && id_input_email.Text != "" &&
			id_input_senha.Text != "" && id_input_confirmar_senha.Text != "")
		{
			if (id_input_senha.Text != id_input_confirmar_senha.Text)
			{
				id_verifica_senha.Visible = true;
				alertSuccess.Visible = false;
				id_verifica_dados.Visible = false;
				id_dados_incorretos.Visible = false;
			}
			else
			{
				VerificaDados();				
			}
		}
		else
		{
			id_verifica_senha.Visible = false;
			id_verifica_dados.Visible = true;
			id_dados_incorretos.Visible = false;
			alertSuccess.Visible = false;
		}

	}

	void recuperarSenha()
	{
		SqlConnection con = new SqlConnection(new ClsFuncoes().conexao);
		con.Open();
		SqlCommand command = con.CreateCommand();

		command.CommandText = "UPDATE PROFESSOR SET SENHA = @senha WHERE NOME = @nome AND EMAIL = @email AND RA = @ra";

		command.Parameters.AddWithValue("@nome", id_input_nome.Text);
		command.Parameters.AddWithValue("@email", id_input_email.Text);
		command.Parameters.AddWithValue("@ra", id_input_ra.Text);
		command.Parameters.AddWithValue("@senha", id_input_senha.Text);

		command.ExecuteNonQuery();

		id_verifica_dados.Visible = false;
		id_verifica_senha.Visible = false;
		id_dados_incorretos.Visible = false;
		alertSuccess.Visible = true;
		
		id_input_email.Text = "";
		id_input_nome.Text = "";
		id_input_ra.Text = "";
		id_input_senha.Text = "";
		id_input_confirmar_senha.Text = "";
	}

	void VerificaDados()
	{

		SqlConnection con = new SqlConnection(new ClsFuncoes().conexao);
		con.Open();
		SqlCommand command = con.CreateCommand();

		command.CommandText = "SELECT COUNT(*) FROM PROFESSOR WHERE NOME = @nome AND EMAIL = @email AND RA = @ra";

		command.Parameters.AddWithValue("@nome", id_input_nome.Text);
		command.Parameters.AddWithValue("@email", id_input_email.Text);
		command.Parameters.AddWithValue("@ra", id_input_ra.Text);

		command.ExecuteNonQuery();
		string output = command.ExecuteScalar().ToString();

		if (output == "1")
		{
			recuperarSenha();
			id_dados_incorretos.Visible = false;
			id_verifica_dados.Visible = false;
		}
		else
		{
			alertSuccess.Visible = false;
			id_verifica_dados.Visible = false;
			id_dados_incorretos.Visible = true;
		}

	}

}