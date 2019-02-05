using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net;

public partial class portal_aluno : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			if (Session["user"] != null)
			{
				receberNomeUsuario();
				lblNomeUsuario.Text = "Olá " + Session["nome"].ToString();
				verificaSeEhLider();
				verificaGrupo();
				CarregaAlunos();
				IniciarChat();
                LoadData();
            }
			else
			{
				Response.Redirect("../home.aspx");
			}
			
        }
	}

	void receberNomeUsuario()
	{

		string sql = "SELECT NOME FROM ALUNOS WHERE RA = @RA";
		using (SqlConnection con = new SqlConnection(new ClsFuncoes().conexao))
		using (var command = new SqlCommand(sql, con))
		{
			con.Open();
			command.Parameters.AddWithValue("@RA", Session["user"].ToString());
			Session["nome"] = (string)command.ExecuteScalar();
			con.Close();
		}

	}

	void verificaSeEhLider()
	{
		string sql = "SELECT LIDER FROM ALUNOS WHERE RA = @RA";
		using (SqlConnection con = new SqlConnection(new ClsFuncoes().conexao))
		using (var command = new SqlCommand(sql, con))
		{
			con.Open();
			command.Parameters.AddWithValue("@RA", Session["user"].ToString());
			Session["lider"] = (string)command.ExecuteScalar();
			con.Close();
		}
		if (Session["lider"].ToString().Contains("L"))
		{
			imgLider.Visible = true;
            linkChatProf.Visible = true;

        }
		else
		{
            linkChatProf.Visible = false;
            imgLider.Visible = false;
		}
			
	}

	void verificaGrupo()
	{
		string sql = "SELECT GRUPO FROM ALUNOS WHERE RA = @RA";
		using (SqlConnection con = new SqlConnection(new ClsFuncoes().conexao))
		using (var command = new SqlCommand(sql, con))
		{
			con.Open();
			command.Parameters.AddWithValue("@RA", Session["user"].ToString());
			Session["grupo"] = (string)command.ExecuteScalar();
			con.Close();
		}
		lblGrupo.Text = Session["grupo"].ToString();
	}

	void CarregaAlunos()
	{
		gvw_alunos.DataSource = new ClsFuncoes().AbrirTabela("SELECT NOME, RA, EMAIL,LIDER FROM ALUNOS WHERE GRUPO = '"+ Session["grupo"].ToString() + "'");
		gvw_alunos.DataBind();
	}

	void IniciarChat()
	{
			historicoMensagem.Visible = false;
			SqlConnection conn = new SqlConnection(new ClsFuncoes().conexao);
			Label lblChat = new Label();
			lblChat.Attributes.Add("style", "font-weight: bolder; margin-top: 10px; margin-bottom: 10px;");
			conn.Open();
			string str = "SELECT NOME FROM ALUNOS WHERE GRUPO ='" + Session["grupo"].ToString() + "'";
			SqlCommand cmd1 = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(cmd1);
			DataSet ds = new DataSet();
			da.Fill(ds);
			lblChat.Text = "Chat " + Session["grupo"].ToString();
			pnlChat.Controls.Add(lblChat);
			DataList1.DataSource = ds;
			DataList1.DataBind();
			conn.Close();
			carregarChat.Visible = true;		

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
		command.Parameters.AddWithValue("@grupo", Session["grupo"].ToString());
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
		string str = "SELECT SENDER,MENSAGEM, DATA_MENSAGEM FROM CHAT WHERE RECEIVER ='" + Session["grupo"].ToString() + "' ORDER BY ID_MENSAGEM DESC";
		SqlCommand cmd1 = new SqlCommand(str, conn);
		SqlDataAdapter da = new SqlDataAdapter(cmd1);
		DataSet ds = new DataSet();
		da.Fill(ds);
		DataList2.DataSource = ds;
		DataList2.DataBind();
		conn.Close();

	}

	protected void btnSair_Click(object sender, EventArgs e)
    {
        Session.Remove("user");
        Response.Redirect("../home.aspx");
    }

    protected void direcionaHome(object sender, EventArgs e)
    {
        Response.Redirect("../home.aspx");
    }

    protected void direcionaChatProf(object sender, EventArgs e)
    {
        Response.Redirect("../chat-prof/chat-prof.aspx");
    }

    void LoadData()
    {
        var st = from s in db.PDFFileTabs select s;
        GridViewPDF.DataSource = st;
        GridViewPDF.DataBind();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)((sender as Control)).NamingContainer).RowIndex;
        string filelocation = GridViewPDF.Rows[rowIndex].Cells[3].Text;
        string FilePath = Server.MapPath(filelocation);

        WebClient User = new WebClient();

        Byte[] FileBuffer = User.DownloadData(FilePath);

        if (FileBuffer != null)
        {
            Response.ContentType = "application/octect-stream";
            Response.AppendHeader("content-disposition", "filename=" + filelocation);
            Response.TransmitFile(FilePath);
            Response.End();

        }

    }

    DataClassesDataContext db = new DataClassesDataContext();

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string fname = FileUpload1.FileName;
        string flocation = "../pdf-file/";
        string pathstring = System.IO.Path.Combine(flocation, fname);

        if (!String.IsNullOrEmpty(fname))
        {
            var st = new PDFFileTab
            {
                FileName = TextBox1.Text,
                FileLocation = pathstring,
            };
            db.PDFFileTabs.InsertOnSubmit(st);
            db.SubmitChanges();
            FileUpload1.SaveAs(MapPath(pathstring));
            label1.Text = "Salvo com Sucesso!";
            LoadData();
        }
        else
        {
            label2.Text = "Insira um nome do documento acima!";
        }
    }
}