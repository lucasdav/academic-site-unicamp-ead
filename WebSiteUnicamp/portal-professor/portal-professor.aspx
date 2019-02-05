﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="portal-professor.aspx.cs" Inherits="portal_professor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="pt-br">
<head runat="server">

	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
	<meta http-equiv="X-UA-Compatible" content="IE-edge" />
    <meta http-equiv="Content-Security-Policy" content="default-src *;style-src * 'unsafe-inline'; script-src * 'unsafe-inline' 'unsafe-eval'; 
        img-src * data: 'unsafe-inline'; connect-src * 'unsafe-inline'; frame-src *;"/>

	<link rel="icon" href="../img/unicampLogo.png" />
	<link rel="stylesheet" href="../css/bootstrap.min.css" />
	<link rel="stylesheet" href="css/portal-professor.css" />
	<link rel="home" href="../home.aspx"/>


	<script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
	<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js" integrity="sha384-b/U6ypiBEHpOf/4+1nzFpr53nxSS+GLCkfwBdFNTxtclqqenISfwAzpKaMNFNmj4" crossorigin="anonymous"></script>
	<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta/js/bootstrap.min.js" integrity="sha384-h0AbiXch4ZDo7tp9hKZ4TsHbi047NrKGLO3SEJAg45jXxnGIfYzk4Si90RDIqNm1" crossorigin="anonymous"></script>

	<script src="../jquery-3.3.1.js"></script>
	<script src="../js/bootstrap.min.js"></script>
	<script src="js/portal-professor.js"></script>

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
                <li class="nav-item active">
                    <a class="nav-link" href="#" style="font-weight:bold; margin-right:20px;">
						<img style="width:20px; height:20px; margin-right: 10px;" src="../icons/svg/si-glyph-person-man.svg"/>
						Portal do Professor<span class="sr-only">(current)</span></a>
                </li>              
				<li class="nav-item">
                    <a runat="server" id="linkCadastrarProf" class="nav-link" onserverclick="direcionaCadastroProf"  
                        style="font-weight:bold; margin-right:20px;">
						<img style="width:20px; height:20px; margin-right: 10px;" src="../icons/svg/si-glyph-document-bullet-list.svg"/>
						Cadastrar Professores</a>
                </li>
				<li class="nav-item">
                    <a runat="server" id="linkControleDeGrupos" onserverclick="direcionaControleGrupos" class="nav-link" 
                        style="font-weight:bold; margin-right:20px;">
						<img style="width:20px; height:20px; margin-right: 10px;" src="../icons/svg/si-glyph-badge-name.svg"/>
						Controle de Grupos</a>
                </li>
                <li class="nav-item">
                    <a runat="server" id="linkChatLider" onserverclick="direcionaChatLider" class="nav-link" 
                        style="font-weight:bold; margin-right:20px;">
						<img style="width:20px; height:20px; margin-right: 10px;" src="../icons/svg/si-glyph-shield-star.svg"/>
						Chat com Líderes</a>
                </li>
            </ul>
			  <a runat="server" id="btnSair" class="nav-link" onserverclick="btnSair_Click"  style="font-weight:bold; color:red; float:right;">Sair
				  <img style="width:20px; height:20px; margin-right: 10px;" src="../icons/svg/si-glyph-sign-out.svg"/>
			  </a>
			 
        </nav>

		<div class="row" style="display: flex;margin-left: 0px !important; margin-right: 0px !important; padding-bottom:20%">		

			<div class="col-md-5" style="margin-top: 15px;">
				<label class="control-label">Total de Alunos</label>
				<asp:label ID="id_total_alunos" style="margin-left:5px;" runat="server" class="control-label"/>	
											
				<asp:Panel ID="pnlResult" runat="server"></asp:Panel>
			</div>	
            	
			<div class="col-md-7" id="carregarChat" runat="server" visible="false" style="max-height: max-content; background-color: gainsboro; 
                padding-top: 20px; padding-bottom: 60px; border-bottom-left-radius: 10px;">
                <span id="spanNomeGrupo" style="font-weight: bolder; margin-top: 10px; margin-bottom: 10px;" runat="server"></span>
				<asp:Panel ID="pnlChat" runat="server"></asp:Panel>

				<div style="display: -webkit-inline-box;">
					<asp:DataList ID="DataList1" style="border-radius: 5px; padding: 20px; margin: 20px; background-color: ghostwhite;" 
                        runat="server" RepeatDirection="Vertical">
						<ItemTemplate>
							<div style="height:1px; background-color:deepskyblue"></div>	
							<asp:Label ID="LinkButton1" style="display: -webkit-inline-box; font-weight: bold;" class="nav-link" runat="server" 
								Text='<%# Bind("NOME") %>'></asp:Label>
							<img style="width:15px; height:15px; margin-right: 10px; position: relative; top: 14px; float: right;" 
                                src="../icons/svg/si-glyph-bubble-message-talk.svg"/>
						
							<div style="height:1px; background-color:deepskyblue"></div>	
						</ItemTemplate>
					</asp:DataList>

					<div style="position:relative; top: 20px;">
                        <iframe src="https://tokbox.com/embed/embed/ot-embed.js?embedId=c58ed827-fcde-452f-bf7c-ee4983d31f38&room=DEFAULT_ROOM&iframe=true" 
                            width="560px" height="315px" allow="microphone; camera" ></iframe>
						<textarea id="txtMensagem" runat="server" rows="4" cols="60" name="comment" style="resize:none; border-radius:5px; 
                        display: -webkit-box;" placeholder="Insira a mensagem aqui..."></textarea>
						<button id="btnEnviarMensagem" style="margin-top: -41px; float:right; font-weight:bold" class="btn btn-info" runat="server" 
                            onserverclick="btnEnviarMensagem_Click">
                            Enviar
                            <img style="width:20px; height:20px;" src="../icons/svg/si-glyph-bubble-message-text.svg"/>
                        </button>													
					</div>	
				</div>

                <div runat="server" style="display: block; margin-top: 40px;">

                     <div>
                        <asp:label runat="server">Selecionar Arquivo:  </asp:label> 
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </div>
                   
                    <div style="margin-top:15px"> 
                        <asp:Label runat="server">Nome do Arquivo:  </asp:Label>                    
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        <button runat="server" id="btnPDF" style="font-weight:bold" class="btn btn-outline-success"
                             onserverclick="btnPDF_Click"> Salvar
                            <img style="width:20px; height:20px; margin-right: 10px;" src="../icons/svg/si-glyph-floppy-disk.svg"/>
                        </button>
                    </div>

                    <div style="margin-top:10px;">
                        <asp:Label ID="label1" style="color:forestgreen" runat="server"> </asp:Label>
                        <asp:Label ID="label2" style="color:red" runat="server"> </asp:Label>
                    </div>

                    <div style="margin-top: 20px;">                        
                        <asp:GridView ID="GridViewPDF" runat="server" Width="700px">
                            <HeaderStyle BackColor="Silver" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument ='<%# Bind("FileName") %>' 
                                            Text='<%# Bind("FileName") %>' CommandName="Download" OnClick="LinkButton1_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                </div>

				<div id="historicoMensagem" visible="false" runat="server" style="display: block; margin-top: 20px;">                    
                   <p>Histórico de Mensagens</p>
					<asp:DataList ID="DataList2" style="margin-left: -2px;" runat="server" RepeatDirection="Vertical">
						<ItemTemplate>
							<asp:Label ID="LinkButton2" style="display: -webkit-inline-box; font-weight: bold;" class="nav-link" runat="server" 
									Text='<%# Bind("SENDER") %>'></asp:Label>		
							<asp:Label ID="LinkButton3" style="display: -webkit-inline-box; font-weight: bold;" class="nav-link" runat="server" 
									Text='<%# Bind("MENSAGEM") %>'></asp:Label>		
							<asp:Label ID="LinkButton4" style="display: -webkit-inline-box; font-weight: bold;" class="nav-link" runat="server" 
									Text='<%# Bind("DATA_MENSAGEM") %>'></asp:Label>
							<img style="width:15px; height:15px; margin-right: 10px; position: relative; top: 14px; float: right;" 
                                src="../icons/svg/si-glyph-bubble-message-talk.svg"/>				
							<div style="height:1px; background-color:deepskyblue"></div>
						</ItemTemplate>
					</asp:DataList>							
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
        </div>
    </form>
</body>
</html>