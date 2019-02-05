using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsFuncoes
/// </summary>
public class ClsFuncoes
{
	public string conexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Davanso\documents\visual studio 2015\WebSites\WebSiteUnicamp\App_Data\DbUnicamp.mdf; Integrated Security=True";
	
	/// <summary>
	/// Método para retornar um Dataset conforme sql passado por parâmetro.
	/// </summary>
	/// <param name="sqltxt">Comando sql para carregar dataset</param>
	/// <returns></returns>
	
	public DataSet AbrirTabela(string query)
	{
		SqlConnection con = new SqlConnection(conexao);
		con.Open();
		SqlDataAdapter adp = new SqlDataAdapter(query, con);
		DataSet dst = new DataSet();
		adp.Fill(dst);
		return dst;
	}
	    
}