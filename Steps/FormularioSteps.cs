using Reqnroll;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Desafio_Amcom.Pages;
using NUnit.Framework;
using System;

namespace Desafio_Amcom.Steps
{
    [Binding]
    public class FormularioUnificadoSteps
    {
        private IWebDriver? _driver;
        private FormularioPage? _formularioPage;

        // =================================================================
        // HOOKS (GANCHOS DE EXECUÇÃO)
        // =================================================================

        [BeforeScenario]
        public void InicializarNavegador()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _formularioPage = new FormularioPage(_driver);
        }

        [AfterScenario]
        public void FecharNavegador()
        {
            // Pausa estratégica para verificação visual do último estado antes do encerramento
            System.Threading.Thread.Sleep(1500);
            _driver?.Quit();
            _driver?.Dispose();
        }

        // =================================================================
        // PASSOS COMPARTILHADOS (GHERKIN GLOBAIS)
        // =================================================================

        [Given(@"que eu acessei a página de formulários")]
        public void GivenQueEuAcesseiAPaginaDeFormularios()
        {
            _formularioPage?.Navegar("https://imagens.amcom.com.br/HTML/testeQA_1_1.html"); 
            System.Threading.Thread.Sleep(1500); // Aguarda renderização inicial da página
        }

        // =================================================================
        // PASSOS DO FORMULÁRIO 1
        // =================================================================

        [When(@"preencho o nome ""(.*)"", sobrenome ""(.*)"" e telefone ""(.*)""")]
        public void WhenPreenchoONomeSobrenomeETelefone(string nome, string sobrenome, string telefone)
        {
            _formularioPage?.PreencherFormulario1(nome, sobrenome, telefone);
            System.Threading.Thread.Sleep(1500); // Pausa para validação visual dos dados inseridos
        }

        [When(@"clico no botão ""Enviar"" do Formulário 1")]
        public void WhenClicoNoBotaoEnviarDoFormulario1()
        {
            _formularioPage?.ClicarEnviarForm1();
            System.Threading.Thread.Sleep(1500); // Pausa para captura do comportamento pós-clique
        }

        [Then(@"o modal deve ser exibido com a mensagem ""(.*)""")]
        public void ThenOModalDeveSerExibidoComAMensagem(string messageEsperada)
        {
            Assert.That(_formularioPage!.ModalEstaVisivel(), Is.True, "O modal de sucesso deveria estar visível.");
            string textoReal = _formularioPage.ObterTextoDoModal();
            Assert.That(textoReal, Is.EqualTo(messageEsperada), "A mensagem do modal está incorreta.");
            System.Threading.Thread.Sleep(1500); // Tempo para leitura do conteúdo do modal
        }

        [When(@"clico no botão de fechar do modal")]
        public void WhenClicoNoBotaoDeFecharDoModal()
        {
            _formularioPage?.FecharModal();
            System.Threading.Thread.Sleep(1500); // Pausa para observação do fechamento do componente
        }

        [Then(@"o modal deve desaparecer da tela")]
        public void ThenOModalDeveDesaparecerDaTela()
        {
            Assert.That(_formularioPage!.ModalEstaVisivel(), Is.False, "O modal não deveria mais estar visível na tela.");
        }

        [When(@"não preencho nenhum campo do Formulário 1")]
        public void WhenNaoPreenchoNenhumCampoDoFormulario1()
        {
            System.Threading.Thread.Sleep(1500); // Validação de campos em estado inicial (vazios)
        }

        [Then(@"o modal com a mensagem de sucesso não deve ser exibido")]
        public void ThenOModalComAMensagemDeSucessoNaoDeveSerExibido()
        {
            Assert.That(_formularioPage!.ModalEstaVisivel(), Is.False, "Bug encontrado: O modal foi exibido mesmo com os campos vazios ou inválidos!");
        }

        // =================================================================
        // PASSOS DO FORMULÁRIO 2
        // =================================================================

        [When(@"seleciono a cor ""(.*)"" no combo box do Formulário 2")]
        public void WhenSelecionoACorNoComboBoxDoFormulario2(string corIngles)
        {
            _formularioPage?.SelecionarCorPorValor(corIngles);
            System.Threading.Thread.Sleep(1500); // Tempo para transição e aplicação da nova cor de fundo
        }

        [Then(@"o fundo do Formulário 2 deve mudar para a cor ""(.*)""")]
        public void ThenOFundoDoFormulario2DeveMudarParaACor(string rgbEsperado)
        {
            string rgbReal = _formularioPage!.ObterCorDeFundoForm2();

            // Ajuste do canal Alpha retornado pelo Chrome para compatibilidade com o formato RGB do Gherkin
            if (rgbReal.StartsWith("rgba") && rgbReal.EndsWith(", 1)"))
            {
                rgbReal = rgbReal.Replace("rgba", "rgb").Replace(", 1)", ")");
            }

            Assert.That(rgbReal, Is.EqualTo(rgbEsperado), 
                $"A cor de fundo do Formulário 2 deveria ser {rgbEsperado}, mas o navegador renderizou {rgbReal}.");
        }

        // =================================================================
        // PASSOS DO FORMULÁRIO 3
        // =================================================================

        [When(@"clico no botão ""Ver horario"" do Formulário 3")]
        public void WhenClicoNoBotaoVerHorarioDoFormulario3()
        {
            _formularioPage?.ClicarVerHorario();
            System.Threading.Thread.Sleep(1500); // Tempo para exibição da mensagem dinâmica de horário
        }

        [Then(@"o sistema deve exibir a saudação correta baseada na hora atual do computador")]
        public void ThenOSistemaDeveExibirASaudacaoCorretaBaseadaNaHoraAtualDoComputador()
        {
            int horaAtual = DateTime.Now.Hour;
            string saudacaoEsperada;

            if (horaAtual < 12)
                saudacaoEsperada = "Bom dia!";
            else if (horaAtual < 18)
                saudacaoEsperada = "Boa tarde!";
            else
                saudacaoEsperada = "Boa noite!";

            string saudacaoReal = _formularioPage!.ObterTextoSaudacao();
            Assert.That(saudacaoReal, Is.EqualTo(saudacaoEsperada), 
                $"A saudação deveria ser '{saudacaoEsperada}' para o horário de {horaAtual}h, mas o site exibiu '{saudacaoReal}'.");
        }

        // =================================================================
        // PASSOS DE INTERAÇÕES VISUAIS E CASOS DE BORDA (EDGE CASES)
        // =================================================================

        [When(@"eu clico no campo de nome e mudo para o campo de sobrenome sem preencher")]
        public void WhenEuClicoNoCampoDeNomeEMudoParaOCampoDeSobrenomeSemPreencher()
        {
            var inputNome = _driver?.FindElement(By.Id("nome"));
            var inputSobrenome = _driver?.FindElement(By.Id("sobrenome"));

            inputNome?.Click();
            System.Threading.Thread.Sleep(800);  // Simulação de foco temporário no campo
            inputSobrenome?.Click(); 
            System.Threading.Thread.Sleep(1500); // Aguarda o disparo do evento 'blur' (perda de foco)
        }

        [Then(@"o campo de nome deve exibir um alerta visual de erro ou borda vermelha")]
        public void ThenOCampoDeNomeDeveExibirUmAlertaVisualDeErroOuBordaVermelha()
        {
            var inputNome = _driver?.FindElement(By.Id("nome"));
            Assert.That(inputNome, Is.Not.Null, "Não foi possível localizar o campo de nome.");

            string classeCss = inputNome!.GetAttribute("class") ?? "";
            string estiloBorda = inputNome!.GetCssValue("border-color") ?? "";
            string pseudoElemento = inputNome!.GetCssValue("box-shadow") ?? "";

            bool temMarcacaoErro = classeCss.Contains("error") || 
                                   classeCss.Contains("invalid") || 
                                   estiloBorda.Contains("255, 0, 0") || 
                                   pseudoElemento.Contains("255, 0, 0");

            Assert.That(temMarcacaoErro, Is.True, 
                "Bug encontrado: O campo perdeu o foco sem preenchimento, mas a página não aplicou nenhum feedback visual (borda vermelha ou classe de erro)!");
        }

        // =================================================================
        // PASSOS DE VALIDAÇÃO ORTOGRÁFICA E ACENTUAÇÃO
        // =================================================================

        [Then(@"o título do bloco (.*) deve exibir a grafia correta ""(.*)"" com acento")]
        public void ThenOTituloDoBlocoDeveExibirAGrafiaCorretaComAcento(int posicao, string textoEsperadoComAcento)
        {
            // Captura o elemento H2 de forma dinâmica com base na posição da tabela de exemplos
            var tituloForm = _driver?.FindElement(By.XPath($"//h2[{posicao}]")); 
            string textoRealDoSite = tituloForm?.Text ?? "";

            Assert.That(textoRealDoSite, Is.EqualTo(textoEsperadoComAcento), 
                $"Erro ortográfico na UI: O bloco {posicao} exibe '{textoRealDoSite}', mas o correto de acordo com a norma padrão é '{textoEsperadoComAcento}'.");
        }
    }
}