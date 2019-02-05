<%@ Page Language="C#" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="pt-br">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
	<meta http-equiv="X-UA-Compatible" content="IE-edge"/>

	<link rel="icon" href="img/unicampLogo.png" />
	<link rel="stylesheet" href="css/bootstrap.min.css" />
	<link rel="stylesheet" href="css/home-styles.css" />

	<script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
	<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js" integrity="sha384-b/U6ypiBEHpOf/4+1nzFpr53nxSS+GLCkfwBdFNTxtclqqenISfwAzpKaMNFNmj4" crossorigin="anonymous"></script>
	<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta/js/bootstrap.min.js" integrity="sha384-h0AbiXch4ZDo7tp9hKZ4TsHbi047NrKGLO3SEJAg45jXxnGIfYzk4Si90RDIqNm1" crossorigin="anonymous"></script>
	<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.8/jquery.mask.js"></script>

	<script src="jquery-3.3.1.js"></script>
	<script src="./js/bootstrap.min.js"></script>

	<title>Unicamp</title>
</head>
<body>
	<div class="container-fluid" style="padding-left:0px !important; padding-right:0px !important; margin-bottom: 0px !important; padding-bottom: 0px !important;">
		<div class="row div-header">
			<div class="col-6 div-logo">
				<div class="row">
					<img src="./img/unicampLogo.png" alt="Imagem da logo da Unicamp" class="img-logo"/>
				</div>
			</div>			
		</div>

        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <ul class="navbar-nav">
                <li class="nav-item active">
                    <a class="nav-link" href="#" style="font-weight:bold; margin-right:20px;">
						<img style="width:20px; height:20px; margin-right: 10px;" src="../icons/svg/si-glyph-house.svg"/>
						Home <span class="sr-only">(current)</span></a>
                </li>

                <li class="nav-item">
                    <a id="linkPortalDocente" runat="server" onserverclick="direcionarPortalDocente" class="nav-link" style="font-weight:bold; margin-right:20px;">
						<img style="width:20px; height:20px; margin-right: 10px;" src="../icons/svg/si-glyph-person-man.svg"/>
						Portal do Professor</a>
                </li>

                <li class="nav-item">
                    <a id="linkPortalAluno" runat="server" onserverclick="direcionarPortalAluno" class="nav-link" style="font-weight:bold; margin-right:20px;">
						<img style="width:20px; height:20px; margin-right: 10px;" src="../icons/svg/si-glyph-congratulation-hat.svg"/>
						Portal do Aluno
                    </a>
                </li>
            </ul>
        </nav>

		<div class="row" style="display: flex;margin-left: 0px !important; margin-right: 0px !important">
			<div class="col-xs-12 col-sm-6 col-md-6 jumbotron apresentacao">
				<h2 class="lead">A Universidade! </h2>
				<p>&nbsp; &nbsp; A Unicamp responde por 8% da pesquisa acadêmica no Brasil, 12% da pós-graduação nacional e mantém a liderança entre as universidades brasileiras no que diz respeito a patentes e ao número de artigos per capita publicados anualmente em revistas indexadas na base de dados ISI/WoS. A Universidade conta com aproximadamente 34 mil alunos matriculados em 66 cursos de graduação e 153 programas de pós-graduação.</p>
				<p>&nbsp; &nbsp; A média anual de teses e dissertações defendidas é de 2,1 mil e 99% de seus professores possuem título de doutor. Esse batalhão do ensino e pesquisa lidera o ranking nacional per capita de publicações científicas nas revistas internacionais catalogadas.Se a produção acadêmica for calculada pelo desempenho de cada pesquisador, a Unicamp é, atualmente, a mais produtiva universidade brasileira.</p>
				<p>&nbsp; &nbsp; Todos os anos, cerca de 800 doutores são formados, uma marca capaz de despertar admiração até mesmo em líderes de algumas universidades americanas e européias. E em cinco décadas, a Unicamp formou mais de 65 mil jovens profissionais em seus cursos de graduação. Além disso, milhares de profissionais formados na universidade atuam em empresas, governo e organizações sociais, contribuindo para o desenvolvimento econômico e social do país. Como pólo científico e cultural, a Universidade reuniu grandes nomes no meio acadêmico. Entre eles, Cesar Lattes, André Tosello, Gleb Wataghin, Vital Brasil, Giuseppe Cilento, Octávio Ianni, Almeida Prado e Bernardo Caro, entre muitos outros.</p>
			</div>
			<div class="col-xs-12 col-sm-6 col-md-6">
				<p style="margin-top: 30px">Alunos cadastre-se aqui para acesso ao portal!</p>
				<form class="form" runat="server">
					<div class="form-group">
						<label class="control-label col-md-6" for="id-input-nome">Nome</label>
						<div class="col-md-6">
							<asp:TextBox type="text" runat="server" class="form-control" id="id_input_nome" placeholder="insira seu nome" />
							<span class="s1">Preenchimento do nome é obrigatório!</span>
						</div>
					</div>
					<div class="form-group">
						<label class="control-label col-md-6" for="id-input-email">Email</label>
						<div class="col-md-6">
							<asp:TextBox type="text" runat="server" class="form-control" id="id_input_email" placeholder="insira seu email" />
							<span class="s1">Preenchimento do email é obrigatório!</span>
						</div>
					</div>
					<div class="form-group">
						<label class="control-label col-md-6" for="id-input-ra">RA</label>
						<div class="col-md-6">
							<asp:TextBox type="number" runat="server" class="form-control" id="id_input_ra" maxlength="4" placeholder="insira seu RA" />
							<span class="s1">Preenchimento do RA é obrigatório!</span>
						</div>
					</div>
					<div class="form-group">
						<label class="control-label col-md-6" for="id-input-senha">Criar Senha</label>
						<div class="col-md-6">
							<asp:TextBox type="password" runat="server" class="form-control" id="id_input_senha" placeholder="crie sua senha" />
							<span class="s1">Criação da senha é obrigatória!</span>
						</div>
					</div>
					<div class="form-group">
						<label class="control-label col-md-6" for="id_input_confirmar_senha">Confirmar Senha</label>
						<div class="col-md-6">
							<asp:TextBox type="password" runat="server" class="form-control" id="id_input_confirmar_senha" placeholder="confirme sua senha" />
							<span class="s1">Confirmação da senha é obrigatória!</span>
						</div>
					</div>
					<div id="id_verifica_senha" runat="server" visible="false" class="alert alert-danger" role="alert">
                      Senhas não conferem, insira novamente a senha e confirme-a!
                    </div>
					<div class="form-group" id="btn-default">
						<div class="col-md-offset-2 col-md-10">
							<asp:Button id="btnSalvar" runat="server" class="btn btn-default" style="background-color: #1d6daa; color: #ffffff" Text="Cadastrar" OnClick="btnSalvar_Click"/>
						</div>
					</div>
                    <div id="alertSuccess" runat="server" visible="false" class="alert alert-success" role="alert">
                      Seu perfil foi registrado com sucesso, acesse o menu Portal do Aluno e efetue o login!
                    </div>
					<div id="id_verifica_dados" runat="server" visible="false" class="alert alert-danger" role="alert">
                      Informe todos os dados acima para concluir o cadastro!
                    </div>
				</form>
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
                <a href="home.html" style="color: #ffffff">unicamp.com</a>
			</div>

		</footer>
	</div>

	<script src="js/home-js.js"></script>

</body>

</html>
