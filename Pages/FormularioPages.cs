using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Desafio_Amcom.Pages
{
    public class FormularioPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public FormularioPage(IWebDriver driver)
        {
            _driver = driver;
            // Configuração do Wait Explícito para sincronismo de elementos assíncronos (Modais)
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5)); 
        }

        // =================================================================
        // ELEMENTOS - FORMULÁRIO 1
        // =================================================================
        private IWebElement InputNome => _driver.FindElement(By.Id("nome"));
        private IWebElement InputSobrenome => _driver.FindElement(By.Id("sobrenome"));
        private IWebElement InputTelefone => _driver.FindElement(By.Id("telefone"));
        private IWebElement BotaoEnviarForm1 => _driver.FindElement(By.XPath("//*[text()='Enviar']"));
        
        // Elementos dinâmicos do Modal gerenciados via Explicit Wait
        private IWebElement ObterModal() => _wait.Until(drv => drv.FindElement(By.ClassName("modal-content"))); 
        private IWebElement ObterTextoModal() => _wait.Until(drv => drv.FindElement(By.XPath("//div[@class='modal-content']/p")));
        private IWebElement BotaoFecharModal => _driver.FindElement(By.ClassName("close"));

        // =================================================================
        // ELEMENTOS - FORMULÁRIO 2
        // =================================================================
        private IWebElement ComboCores => _driver.FindElement(By.Id("cor"));
        private IWebElement ContainerForm2 => _driver.FindElement(By.XPath("//h2[text()='Formulario 2']/.."));

        // =================================================================
        // ELEMENTOS - FORMULÁRIO 3
        // =================================================================
        private IWebElement BotaoHorario => _driver.FindElement(By.Id("horario"));
        private IWebElement TextoMensagemHorario => _driver.FindElement(By.Id("mensagem"));


        // =================================================================
        // MÉTODOS E AÇÕES GLOBAIS
        // =================================================================
        public void Navegar(string url) => _driver.Navigate().GoToUrl(url);

        // -----------------------------------------------------------------
        // AÇÕES DO FORMULÁRIO 1
        // -----------------------------------------------------------------
        public void PreencherFormulario1(string nome, string sobrenome, string telefone)
        {
            InputNome.Clear();
            InputNome.SendKeys(nome);
            InputSobrenome.Clear();
            InputSobrenome.SendKeys(sobrenome);
            InputTelefone.Clear();
            InputTelefone.SendKeys(telefone);
        }
        
        public void ClicarEnviarForm1() => BotaoEnviarForm1.Click();
        
        public string ObterTextoDoModal() => ObterTextoModal().Text;
        
        public void FecharModal() 
        {
            // Pausa estratégica necessária para a conclusão da animação visual do modal
            System.Threading.Thread.Sleep(500);
            BotaoFecharModal.Click();
        }
        
        public bool ModalEstaVisivel()
        {
            try 
            { 
                return ObterModal().Displayed; 
            }
            catch (WebDriverTimeoutException) 
            { 
                return false; 
            }
            catch (NoSuchElementException) 
            { 
                return false; 
            }
        }

        // -----------------------------------------------------------------
        // AÇÕES DO FORMULÁRIO 2
        // -----------------------------------------------------------------
        public void SelecionarCorPorValor(string valorCor)
        {
            var select = new SelectElement(ComboCores);
            select.SelectByValue(valorCor);
        }
        
        public string ObterCorDeFundoForm2() => ContainerForm2.GetCssValue("background-color");

        // -----------------------------------------------------------------
        // AÇÕES DO FORMULÁRIO 3
        // -----------------------------------------------------------------
        public void ClicarVerHorario() => BotaoHorario.Click();
        
        public string ObterTextoSaudacao() => TextoMensagemHorario.Text;
    }
}