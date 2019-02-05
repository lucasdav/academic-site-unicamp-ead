using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

public partial class chat_lider : System.Web.UI.Page
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
        DefineGrupos();
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
    void DefineGrupos()
    {

        SqlConnection conLider = new SqlConnection(new ClsFuncoes().conexao);
        conLider.Open();
        string queryLider = "SELECT COUNT(*) FROM ALUNOS WHERE LIDER = 'L'";

        SqlCommand cmdLider = new SqlCommand(queryLider, conLider);
        string outputLider = cmdLider.ExecuteScalar().ToString();
        int totalLider = Convert.ToInt32(outputLider);
        conLider.Close();
        

        for (int i = 1; i < totalLider + 1; i++)
        {
            GridView grd = new GridView();
            Button btn = new Button();
            grd.ID = "GridView" + i.ToString();
            btn.ID = "btnIniciarChat" + i.ToString();
            grd.Attributes.Add("style", "margin:10px; background-color: whitesmoke;");
            grd.Attributes.Add("cellspacing", "5");
            grd.Attributes.Add("runat", "server");
            btn.Attributes.Add("style", "margin-top:20px;font-weight: bold;");
            btn.Attributes.Add("class", "btnContador btn btn-outline-success");
            btn.Text = "Chat com Líder - Grupo " + i.ToString();
            btn.OnClientClick = "raisePostBack";
            grd.DataSource = new ClsFuncoes().AbrirTabela("SELECT NOME, RA, EMAIL,LIDER FROM ALUNOS WHERE LIDER = 'L' AND GRUPO ='" + "Grupo " + i.ToString() + "'");
            grd.DataBind();

            SqlConnection con = new SqlConnection(new ClsFuncoes().conexao);
            con.Open();

            string query = "SELECT COUNT(*) FROM ALUNOS WHERE LIDER = 'L' AND GRUPO='" + "Grupo " + i.ToString() + "'";

            SqlCommand cmd = new SqlCommand(query, con);
            string output = cmd.ExecuteScalar().ToString();


            if (Convert.ToInt32(output) >= 1)
            {
                pnlResult.Controls.Add(btn);
                pnlResult.Controls.Add(grd);
            }
        }
    }

    protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
    {
        base.RaisePostBackEvent(source, eventArgument);

        if ((source as Button).Text.Contains("Chat com Líder - Grupo"))
        {
            historicoMensagem.Visible = false;
            SqlConnection conn = new SqlConnection(new ClsFuncoes().conexao);
            Label lblChat = new Label();
            lblChat.Attributes.Add("style", "font-weight: bolder; margin-top: 10px; margin-bottom: 10px;");
            Session["Grupo"] = (source as Button).Text.Replace("Chat com Líder - ", "");
            string agent = (source as Button).Text.Replace("Chat com Líder - ", "");
            conn.Open();
            string str = "SELECT NOME FROM ALUNOS WHERE LIDER = 'L' AND GRUPO ='" + agent + "'";
            SqlCommand cmd1 = new SqlCommand(str, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblChat.Text = "Chat com Líder - " + agent;
            pnlChat.Controls.Add(lblChat);
            DataList1.DataSource = ds;
            DataList1.DataBind();
            conn.Close();
            carregarChat.Visible = true;
        }

    }
    
	protected void btnEnviarMensagem_Click(object sender, EventArgs e)
	{
		historicoMensagem.Visible = true;
		DateTime myDateTime = DateTime.Now;
		string sqlFormattedDate = myDateTime.ToString("dd-MM-yyyy HH:mm:ss");
		string mensagem = txtMensagem.Value.ToString();

		SqlConnection conn = new SqlConnection(new ClsFuncoes().conexao);
		conn.Open();
		SqlCommand command = conn.CreateCommand();
		command.CommandText = "INSERT INTO CHAT VALUES(@nome, @grupo, @mensagem, @data)";
		
		command.Parameters.AddWithValue("@nome", Session["nome"].ToString());
		command.Parameters.AddWithValue("@grupo", Session["Grupo"] + "L");
		command.Parameters.AddWithValue("@mensagem", mensagem);
		command.Parameters.AddWithValue("@data", sqlFormattedDate);
		command.ExecuteNonQuery();

		conn.Close();
		txtMensagem.Value = "";
		receberHistóricoMensagem();
	}

	void receberHistóricoMensagem()
	{

		SqlConnection conn = new SqlConnection(new ClsFuncoes().conexao);
		conn.Open();
		string str = "SELECT SENDER,MENSAGEM, DATA_MENSAGEM FROM CHAT WHERE RECEIVER ='" + Session["Grupo"] + "L" + "' ORDER BY ID_MENSAGEM DESC";
		SqlCommand cmd1 = new SqlCommand(str, conn);
		SqlDataAdapter da = new SqlDataAdapter(cmd1);
		DataSet ds = new DataSet();
		da.Fill(ds);
		DataList2.DataSource = ds;
		DataList2.DataBind();
		conn.Close();

	}

    protected void direcionaControleGrupos(object sender, EventArgs e)
    {
        Response.Redirect("../controle-grupos/controle-grupos.aspx");
    }

    protected void direcionaCadastroProf(object sender, EventArgs e)
    {
        Response.Redirect("../portal-admin/portal-admin.aspx");
    }

    protected void direcionaHome(object sender, EventArgs e)
    {
        Response.Redirect("../home.aspx");
    }

    protected void btnSair_Click(object sender, EventArgs e)
    {
        Session.Remove("user");
        Response.Redirect("../home.aspx");
    }
    protected void direcionarPortalProfessor(object sender, EventArgs e)
    {
        Response.Redirect("../portal-professor/portal-professor.aspx");
    }
}