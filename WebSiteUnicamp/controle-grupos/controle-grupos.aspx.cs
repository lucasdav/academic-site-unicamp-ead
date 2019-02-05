using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;

public partial class controle_grupos : System.Web.UI.Page
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

		ApresentaTotalAlunos();
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

	void ApresentaTotalAlunos()
	{
		SqlConnection con = new SqlConnection(new ClsFuncoes().conexao);
		con.Open();
		string query = "SELECT COUNT(*) FROM ALUNOS";

		SqlCommand cmd = new SqlCommand(query, con);
		string output = cmd.ExecuteScalar().ToString();
		id_total_alunos.Text = output;
	}

	void randomizarGrupos(int totalAlunos)
	{
		List<int> listNumbers = new List<int>();
		Random rnd = new Random();
		int number;
		SqlConnection con = new SqlConnection(new ClsFuncoes().conexao);
		int maxCount = totalAlunos + 1;
		for (int i = 1; i <= totalAlunos; i++) 
		{			
			do
			{
				number = rnd.Next(1, maxCount);
			} while (listNumbers.Contains(number));
			listNumbers.Add(number);

			con.Open();

			SqlCommand command = con.CreateCommand();

			command.CommandText = "UPDATE ALUNOS SET COUNT_RANDOM = @count_random WHERE ID_ALUNO = @id_aluno";

			command.Parameters.AddWithValue("@count_random", listNumbers[i-1]);
			command.Parameters.AddWithValue("@id_aluno", i);

			command.ExecuteNonQuery();
			con.Close();

		}
		
	}

	void limparChat()
	{
		SqlConnection con = new SqlConnection(new ClsFuncoes().conexao);
		con.Open();

		SqlCommand command = con.CreateCommand();

		command.CommandText = "DELETE FROM CHAT";
		

		command.ExecuteNonQuery();
		con.Close();
	}

	// Assign the people to groups.
	void separarGrupos(int numeroDeGrupos, int totalAlunos)
	{
		int num_people = totalAlunos;
		int num_groups = numeroDeGrupos;

		SqlConnection con = new SqlConnection(new ClsFuncoes().conexao);

		int group_num = 0;
		for (int i = 1; i <= num_people; i++)
		{

			con.Open();
			SqlCommand command = con.CreateCommand();

			command.CommandText = "UPDATE ALUNOS SET COUNT_GRUPO = @grupo WHERE COUNT_RANDOM = @count_random";

			command.Parameters.AddWithValue("@grupo", group_num+1);
			command.Parameters.AddWithValue("@count_random", i);
			group_num = ++group_num % num_groups;

			command.ExecuteNonQuery();
			con.Close();

		}
		
	}

	void DefineGrupos()
	{		
		int totalAlunos = Convert.ToInt32(id_total_alunos.Text);

		

		if (id_input_numero.Text != "0" && id_input_numero.Text != "" && id_input_numero.Text != null)
		{
			int numeroDeGrupos = Convert.ToInt32(id_input_numero.Text);
			alertMaiorQZero.Visible = false;
			double resultMedia = totalAlunos / numeroDeGrupos;

			if (numeroDeGrupos <= totalAlunos)
			{
				randomizarGrupos(totalAlunos);
				separarGrupos(numeroDeGrupos, totalAlunos);
				removerLider();
				alertGrupos.Visible = false;
							
				for (int i = 1; i <= numeroDeGrupos; i++)
				{
					GridView grd = new GridView();
					Label lbl = new Label();
					grd.ID = "GridView" + i.ToString();
					grd.Attributes.Add("style", "margin:10px; background-color: whitesmoke;");
					grd.Attributes.Add("cellspacing", "5");
					grd.Attributes.Add("runat", "server");
					lbl.Attributes.Add("style", "margin-top:20px;");
					lbl.ID = "lbl_grupo" + i.ToString();
					lbl.Text = "Grupo " + i.ToString();

					
					grd.DataSource = new ClsFuncoes().AbrirTabela("SELECT NOME, RA, EMAIL, LIDER FROM ALUNOS WHERE COUNT_GRUPO ='" + i + "'");
					grd.DataBind();
					

					pnlResult.Controls.Add(lbl);
					pnlResult.Controls.Add(grd);
				}
				btn_salvar.Visible = true;
			}
			else
			{
				alertGrupos.Visible = true;
				btn_salvar.Visible = false;
			}
		}
		else
		{
			alertMaiorQZero.Visible = true;
			btn_salvar.Visible = false;
		}
		
			
	}

			

	

	protected void direcionaCadastroProf(object sender, EventArgs e)
	{
		Response.Redirect("../portal-admin/portal-admin.aspx");
	}

	protected void direcionaHome(object sender, EventArgs e)
	{
		Response.Redirect("../home.aspx");
	}

	protected void direcionarPortalProfessor(object sender, EventArgs e)
	{
		Response.Redirect("../portal-professor/portal-professor.aspx");
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

	protected void btnDefinirGrupos_Click(object sender, EventArgs e)
	{
		alertGruposSalvos.Visible = false;
		DefineGrupos();
	}


	protected void btnSalvar_Click(object sender, EventArgs e)
	{
		int totalAlunos = Convert.ToInt32(id_total_alunos.Text);
		int numeroDeGrupos = Convert.ToInt32(id_input_numero.Text);

		double resultMedia = totalAlunos / numeroDeGrupos;

		SqlConnection con = new SqlConnection(new ClsFuncoes().conexao);
		
				
		for (int i = 1; i <= totalAlunos; i++)
		{

			con.Open();
			SqlCommand command = con.CreateCommand();

			command.CommandText = "UPDATE ALUNOS SET GRUPO = CONCAT('Grupo ', (SELECT COUNT_GRUPO FROM ALUNOS WHERE COUNT_RANDOM = @count_random)) WHERE COUNT_RANDOM = @count_random";

			command.Parameters.AddWithValue("@grupo", "Grupo ");
			command.Parameters.AddWithValue("@count_random", i);

			command.ExecuteNonQuery();
			con.Close();
						
			con.Close();
			
		}
		definirCapitao(numeroDeGrupos, totalAlunos, resultMedia);

		btn_salvar.Visible = false;
		alertGruposSalvos.Visible = true;
		
		limparChat();

	}

	void definirCapitao(int numeroDeGrupos, int totalAlunos, double resultMedia)
	{
		SqlConnection con = new SqlConnection(new ClsFuncoes().conexao);


		for (int i = 1; i <= numeroDeGrupos; i++)
		{

			if (numeroDeGrupos == 1)
			{
				con.Open();
				SqlCommand command = con.CreateCommand();

				command.CommandText = "UPDATE ALUNOS SET LIDER = @capitao WHERE COUNT_RANDOM = @mediaFinal";

				command.Parameters.AddWithValue("@capitao", "L");
				command.Parameters.AddWithValue("@mediaFinal", totalAlunos.ToString());

				command.ExecuteNonQuery();
			}
			else
			if (totalAlunos == numeroDeGrupos)
			{
				con.Open();
				SqlCommand command = con.CreateCommand();

				command.CommandText = "UPDATE ALUNOS SET LIDER = @capitao WHERE COUNT_RANDOM = @mediaFinal";

				command.Parameters.AddWithValue("@capitao", "L");
				command.Parameters.AddWithValue("@mediaFinal", i.ToString());

				command.ExecuteNonQuery();
			}
			else
			{
					con.Open();
					SqlCommand command = con.CreateCommand();

					command.CommandText = "UPDATE ALUNOS SET LIDER = @capitao WHERE COUNT_RANDOM = (SELECT MIN(COUNT_RANDOM) FROM ALUNOS WHERE GRUPO = @grupo)";

					command.Parameters.AddWithValue("@capitao", "L");
					command.Parameters.AddWithValue("@grupo", "Grupo " + i);

					command.ExecuteNonQuery();

			}

			con.Close();

		}
	}

	void removerLider()
	{
		SqlConnection con = new SqlConnection(new ClsFuncoes().conexao);
		con.Open();
		SqlCommand command = con.CreateCommand();

		command.CommandText = "UPDATE ALUNOS SET LIDER = @capitao WHERE COUNT_RANDOM > @mediaFinal";

		command.Parameters.AddWithValue("@capitao", "");
		command.Parameters.AddWithValue("@mediaFinal", "0");

		command.ExecuteNonQuery();
		con.Close();

		btn_salvar.Visible = false;
		
	}
}

