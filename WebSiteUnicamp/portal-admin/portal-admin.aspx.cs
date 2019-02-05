using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			if (Session["user"] != null)
			{
				receberNomeUsuario();
				lblNomeUsuario.Text = "Olá Prof. " + Session["nome"].ToString();
			}
			else
			{
				Response.Redirect("../home.aspx");
			}

		}

	}

	void receberNomeUsuario()
	{

		string sql = "SELECT NOME FROM PROFESSOR WHERE RA = @RA";
		using (SqlConnection con = new SqlConnection(new ClsFuncoes().conexao))
		using (var command = new SqlCommand(sql, con))
		{
			con.Open();
			command.Parameters.AddWithValue("@RA", Session["user"].ToString());
			Session["nome"] = (string)command.ExecuteScalar();
			con.Close();
		}

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
				inserirProfessores();
				id_verifica_dados.Visible = false;
				id_verifica_senha.Visible = false;
				alertSuccess.Visible = true;
			}
		}
		else
		{
			id_verifica_senha.Visible = false;
			alertSuccess.Visible = false;
			id_verifica_dados.Visible = true;
		}

		
	}

	void inserirProfessores()
	{
		SqlConnection con = new SqlConnection(new ClsFuncoes().conexao);
		con.Open();
		string query = string.Format("INSERT INTO [PROFESSOR](NOME,EMAIL,SENHA,RA) VALUES('" + id_input_nome.Text + "','" + id_input_email.Text + "','" + id_input_senha.Text + "','" + id_input_ra.Text + "')");

		SqlCommand cmd = new SqlCommand(query, con);
		cmd.ExecuteNonQuery();

		id_input_email.Text = "";
		id_input_nome.Text = "";
		id_input_ra.Text = "";
		id_input_senha.Text = "";
	}

	protected void direcionarPortalProfessor(object sender, EventArgs e)
    {
        Response.Redirect("../portal-professor/portal-professor.aspx");
    }
	
	protected void direcionaControleGrupos(object sender, EventArgs e)
	{
		Response.Redirect("../controle-grupos/controle-grupos.aspx");
	}

	protected void direcionarHome(object sender, EventArgs e)
    {
        Response.Redirect("../home.aspx");
    }
    protected void direcionaChatLider(object sender, EventArgs e)
    {
        Response.Redirect("../chat-lider/chat-lider.aspx");
    }

    protected void btnSair_Click(object sender, EventArgs e)
	{
		Session.Remove("user");
		Response.Redirect("../home.aspx");
	}

}