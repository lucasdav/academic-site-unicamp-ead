<%@ Page Language="C#" AutoEventWireup="true" CodeFile="controle-grupos.aspx.cs" Inherits="controle_grupos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="pt-br">
<head runat="server">

	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
	<meta http-equiv="X-UA-Compatible" content="IE-edge" />

	<link rel="icon" href="../img/unicampLogo.png" />
	<link rel="stylesheet" href="../css/bootstrap.min.css" />
	<link rel="stylesheet" href="css/controle-grupos.css" />
	<link rel="home" href="../home.aspx"/>


	<script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
	<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js" integrity="sha384-b/U6ypiBEHpOf/4+1nzFpr53nxSS+GLCkfwBdFNTxtclqqenISfwAzpKaMNFNmj4" crossorigin="anonymous"></script>
	<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta/js/bootstrap.min.js" integrity="sha384-h0AbiXch4ZDo7tp9hKZ4TsHbi047NrKGLO3SEJAg45jXxnGIfYzk4Si90RDIqNm1" crossorigin="anonymous"></script>

	<script src="../jquery-3.3.1.js"></script>
	<script src="../js/bootstrap.min.js"></script>

	<title>Unicamp</title>
</head>

<body>    
	<form runat="server">
	<div class="container-fluid" style="padding-left:0px !important; padding-right:0px; margin-bottom: 0px !important; padding-bottom: 0px !important;">
		<div class="row div-header" style="margin-left:0px; margin-right: 0px;">
			<div class="col-md-6 div-logo">
				<div class="row">
					<img src="../img/unicampLogo.png" alt="Imagem da logo, da unicamp" class="img-logo"/>
					<h1>Portal Unicamp</h1>
				</div>
			</div>
			<div class="col-md-6 div-logo">
				<asp:label ID="lblNomeUsuario" style="float: right; font-weight: bold; margin-top: 35px;" runat="server" class="control-label"/>	
			</div>
		</div>			

		 <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <ul class="navbar-nav" style="width: 90%;">
                <li class="nav-item">
                    <a id="linkHome" runat="server" onserverclick="direcionaHome" class="nav-link" style="font-weight:bold; margin-right:20px;">
						<img style="width:20px; height:20px; margin-right: 10px;" src="../icons/svg/si-glyph-house.svg"/>
						Home</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" style="font-weight:bold; margin-right:20px;" id="linkPortalProfessor" runat="server" onserverclick="direcionarPortalProfessor">
						<img style="width:20px; height:20px; margin-right: 10px;" src="../icons/svg/si-glyph-person-man.svg"/>
						Portal do Professor</a>
                </li>              
				<li class="nav-item">
                    <a runat="server" id="linkCadastrarProf" class="nav-link" onserverclick="direcionaCadastroProf"  style="font-weight:bold; margin-right:20px;">
						<img style="width:20px; height:20px; margin-right: 10px;" src="../icons/svg/si-glyph-document-bullet-list.svg"/>
						Cadastrar Professores</a>
                </li>
				<li class="nav-item active">
                    <a runat="server" id="linkControleDeGrupos" href="#" class="nav-link" style="font-weight:bold; margin-right:20px;">
						<img style="width:20px; height:20px; margin-right: 10px;" src="../icons/svg/si-glyph-badge-name.svg"/>
						Controle de Grupos <span class="sr-only">(current)</span></a>
                </li>
                <li class="nav-item">
                    <a runat="server" id="linkChatLider" onserverclick="direcionaChatLider" class="nav-link" style="font-weight:bold; margin-right:20px;">
						<img style="width:20px; height:20px; margin-right: 10px;" src="../icons/svg/si-glyph-shield-star.svg"/>
						Chat com Líderes</a>
                </li>
            </ul>
			 <a runat="server" id="btnSair" class="nav-link" onserverclick="btnSair_Click"  style="font-weight:bold; float:right; color:red;">Sair
				 <img style="width:20px; height:20px; margin-right: 10px;" src="../icons/svg/si-glyph-sign-out.svg"/>
			 </a>
        </nav>

		<div class="row" style="display: flex;margin-left: 0px !important; margin-right: 0px !important; padding-bottom:140px">

			<div class="col-md-12">
				<br />
				<div class="row" style="display: flex;margin-left: 0px !important; margin-right: 0px !important">
						<div class="col-md-2">
							<label class="control-label">Total de Alunos</label>
							<asp:label ID="id_total_alunos" style="margin-left:5px;" runat="server" class="control-label"/>

							<div class="form-group">
								<label class="control-label" for="id-input-ra">Número de Grupos:</label>
								<asp:TextBox type="number" runat="server" class="form-control" id="id_input_numero" maxlength="4" 
									placeholder="0" />
							</div>

							<div class="form-group" id="btn-default">
									<asp:Button id="btnDefinirGrupos" runat="server" class="btn btn-default" style="background-color: #1d6daa; 
									color: #ffffff" Text="Aplicar" OnClick="btnDefinirGrupos_Click"/>
							</div>
							
							<div id="alertMaiorQZero" style="width: 300px;" runat="server" visible="false" class="alert alert-danger" role="alert">
							  o número de grupos deve ser maior que 0!
							</div>
							<div id="alertGrupos" style="width: 300px;" runat="server" visible="false" class="alert alert-danger" role="alert">
							  o número de grupos deve ser menor ou igual ao total de Alunos!
							</div>
						</div>

						<div class="col-md-10" style="max-width: max-content; margin-left: 10%;">

							<div id="alertGruposSalvos" style="margin-top: 55px;" runat="server" visible="false" class="alert alert-success" role="alert">
							  grupos salvos com sucesso!
							</div>	
													
							<asp:Panel ID="pnlResult" runat="server"></asp:Panel>

							<div class="form-group" style="float:right" runat="server" id="btn_salvar" visible="false">
								<asp:Button id="btnsalvar" runat="server" class="btn btn-default" style="background-color: #1d6daa; 
								color: #ffffff" Text="Salvar e Definir Líderes" OnClick="btnSalvar_Click"/>
							</div>

						</div>
				</div>
			</div>
		</div>
	</div>

	<footer class="page-footer font-small blue pt-4 mt-4" style="background: #070707; margin-top: 0px !important;">
		<div class="container-fluid text-center text-md-left">
			<div class="row">
				<div class="col-md-6 mt-md-0 mt-3">
					<h5 class="text-uppercase" style="color: #ffffff">Inovando Sempre</h5>
					<p style="color: #ffffff">Desenvolvido por Leonardo Kegel.</p>
				</div>
				<hr class="clearfix w-100 d-md-none pb-3"/>
			</div>
		</div>

		<div class="footer-copyright text-center py-3" style="color: #ffffff;">
			© 2018 Copyright:
            <a href="../home.aspx" style="color: #ffffff">unicamp.com</a>
		</div>

	</footer>
    </form>
</body>

</html>