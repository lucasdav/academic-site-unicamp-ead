using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class home : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		
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
			}
			else
			{
				inserirAlunos();
				id_verifica_dados.Visible = false;
				id_verifica_senha.Visible = false;
				alertSuccess.Visible = true;
			}
		}
		else
		{
			id_verifica_senha.Visible = false;
			id_verifica_dados.Visible = true;
			alertSuccess.Visible = false;
		}		
        
    }

	void inserirAlunos()
	{
		SqlConnection con = new SqlConnection(new ClsFuncoes().conexao);
		con.Open();

		string query = string.Format("INSERT INTO [ALUNOS](NOME,EMAIL,SENHA,RA) VALUES('" + id_input_nome.Text + "','" + id_input_email.Text + "','" + id_input_senha.Text + "','" + id_input_ra.Text + "')");

		SqlCommand cmd = new SqlCommand(query, con);
		cmd.ExecuteNonQuery();

		alertSuccess.Visible = true;

		id_input_email.Text = "";
		id_input_nome.Text = "";
		id_input_ra.Text = "";
		id_input_senha.Text = "";
		id_input_confirmar_senha.Text = "";
	}


    protected void direcionarPortalDocente(object sender, EventArgs e)
    {
        Response.Redirect("~/login-docente/login-docente.aspx");
    }

    protected void direcionarPortalAluno(object sender, EventArgs e)
    {
        Response.Redirect("~/login-aluno/login-aluno.aspx");
    }
}