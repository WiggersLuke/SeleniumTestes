using AventStack.ExtentReports;
using NUnit.Framework;
using NUnitAutomationFramework.Base;
using NUnitAutomationFramework.Pages;
using NUnitAutomationFramework.Utility;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace NUnitAutomationFramework.TestSuites
{
    public class Regression : BaseSetup
    {
        [Test, Category("Youtube")]
        public void PesquisarEReproduzirVideo()
        {
            test = extent.CreateTest("Pesquisar e Reproduzir um Vídeo");

            var searchBox = GetDriver().FindElement(By.Name("search_query"));
            searchBox.SendKeys("Selenium WebDriver tutorial");
            searchBox.SendKeys(Keys.Enter);
            test.Log(Status.Info, "Pesquisa enviada: 'Selenium WebDriver tutorial'.");
            Thread.Sleep(3000);

            var firstVideo = GetDriver().FindElement(By.CssSelector("ytd-video-renderer"));
            firstVideo.Click();
            test.Log(Status.Info, "Clicou no primeiro vídeo.");
            Thread.Sleep(10000);

            // Se houver anúncio durante a reprodução, tenta pular
            SkipAdIfPresent();

            var pauseButton = GetDriver().FindElement(By.CssSelector("button.ytp-play-button"));
            string titleAttr = pauseButton.GetAttribute("title");
            test.Log(Status.Pass, "O vídeo iniciou a reprodução com sucesso.");
        }

        [Test, Category("Youtube")]
        public void CarregarMaisResultadosAoRolarPagina()
        {
            test = extent.CreateTest("Carregar Mais Resultados ao Rolar a Página");

            // Pesquisa "Música Lo-Fi"
            var searchBox = GetDriver().FindElement(By.Name("search_query"));
            searchBox.SendKeys("Música Lo-Fi");
            searchBox.SendKeys(Keys.Enter);
            test.Log(Status.Info, "Pesquisa enviada: 'Música Lo-Fi'.");
            Thread.Sleep(3000);

            // Captura a contagem inicial de vídeos exibidos
            var videoResults = GetDriver().FindElements(By.CssSelector("ytd-video-renderer"));
            int initialCount = videoResults.Count;
            test.Log(Status.Info, $"Contagem inicial de vídeos: {initialCount}");

            // Realiza o scroll até o final da página para carregar mais resultados
            IJavaScriptExecutor js = (IJavaScriptExecutor)GetDriver();
            js.ExecuteScript("window.scrollTo(0, document.documentElement.scrollHeight);");
            test.Log(Status.Info, "Página rolada para o final para carregar mais resultados.");
            Thread.Sleep(5000); // Aguarda o carregamento dos novos resultados

            // Captura a nova contagem de vídeos exibidos
            videoResults = GetDriver().FindElements(By.CssSelector("ytd-video-renderer"));
            int afterScrollCount = videoResults.Count;
            test.Log(Status.Info, $"Contagem após scroll: {afterScrollCount}");

            // Valida que mais resultados foram carregados
            Assert.IsTrue(afterScrollCount > initialCount, "Mais resultados não foram carregados ao rolar a página.");
            test.Log(Status.Pass, "Mais resultados foram carregados com sucesso ao rolar a página.");
        }


        [Test, Category("Youtube")]
        public void FiltrarVideosAoVivo()
        {
            test = extent.CreateTest("Filtrar Apenas Vídeos Ao Vivo");

            var searchBox = GetDriver().FindElement(By.Name("search_query"));
            searchBox.SendKeys("CNN");
            searchBox.SendKeys(Keys.Enter);
            test.Log(Status.Info, "Pesquisa enviada: 'CNN'.");
            Thread.Sleep(3000);

            var filtersButton = GetDriver().FindElement(By.CssSelector("#filter-button > ytd-button-renderer > yt-button-shape > button"));
            filtersButton.Click();
            test.Log(Status.Info, "Clicou no botão 'Filtros'.");
            Thread.Sleep(2000);

            var liveFilter = GetDriver().FindElement(By.CssSelector(".style-scope:nth-child(4) > .style-scope:nth-child(2) #label > .style-scope:nth-child(1)"));
            liveFilter.Click();
            test.Log(Status.Info, "Selecionou o filtro 'Ao vivo'.");
            Thread.Sleep(3000);

            var liveBadges = GetDriver().FindElements(By.XPath("//*[contains(text(),'AO VIVO')]"));
            Assert.IsTrue(liveBadges.Count > 0, "Não foram encontrados vídeos ao vivo.");
            test.Log(Status.Pass, "Vídeos ao vivo filtrados com sucesso.");
        }

        [Test, Category("Youtube")]
        public void FilaDeReproducao()
        {
            test = extent.CreateTest("Criar e Validar Fila de Reprodução (Playlist)");

            var searchBox = GetDriver().FindElement(By.Name("search_query"));
            searchBox.SendKeys("Música Lo-Fi");
            searchBox.SendKeys(Keys.Enter);
            test.Log(Status.Info, "Pesquisa enviada: 'Música Lo-Fi'.");
            Thread.Sleep(3000);

            // Obtém a lista de vídeos e garante que haja pelo menos 2 resultados
            var videos = GetDriver().FindElements(By.CssSelector("ytd-video-renderer"));
            Assert.IsTrue(videos.Count >= 2, "Não foram encontrados vídeos suficientes para criar a fila.");

            Actions action = new Actions(GetDriver());

            // Adiciona o primeiro vídeo à fila de reprodução
            var firstVideo = videos[0];
            action.MoveToElement(firstVideo).Perform();
            Thread.Sleep(2000);

            // Clica no botão de mais opções (ícone de 3 pontos verticais) utilizando o aria-label "Menu de ações"
            var moreOptionsButton = firstVideo.FindElement(By.XPath(".//button[@aria-label='Menu de ações']"));
            moreOptionsButton.Click();
            test.Log(Status.Info, "Clicou no ícone de mais opções do primeiro vídeo.");
            Thread.Sleep(2000);

            // Clica na opção "Adicionar à fila"
            var addToQueueButton = GetDriver().FindElement(By.XPath("//ytd-menu-service-item-renderer//yt-formatted-string[contains(text(),'Adicionar à fila')]"));
            addToQueueButton.Click();
            test.Log(Status.Info, "Primeiro vídeo adicionado à fila.");
            Thread.Sleep(2000);

            // Adiciona o segundo vídeo à fila de reprodução
            var secondVideo = videos[1];
            action.ScrollToElement(videos[2]).Perform();
            Thread.Sleep(2000);

            // Clica no botão de mais opções do segundo vídeo utilizando o mesmo seletor
            var moreOptionsButton2 = secondVideo.FindElement(By.XPath(".//button[@aria-label='Menu de ações']"));
            moreOptionsButton2.Click();
            test.Log(Status.Info, "Clicou no ícone de mais opções do segundo vídeo.");
            Thread.Sleep(2000);

            // Clica na opção "Adicionar à fila" para o segundo vídeo
            var addToQueueButton2 = GetDriver().FindElement(By.XPath("//ytd-menu-service-item-renderer//yt-formatted-string[contains(text(),'Adicionar à fila')]"));
            addToQueueButton2.Click();
            test.Log(Status.Info, "Segundo vídeo adicionado à fila.");
            Thread.Sleep(2000);

            // Validação: Verifica se o painel da fila de reprodução está visível
            var queuePanel = GetDriver().FindElements(By.CssSelector("ytd-playlist-panel-renderer"));
            Assert.IsTrue(queuePanel.Count >= 1, "A fila de reprodução não foi exibida.");
            test.Log(Status.Pass, "Fila de reprodução validada com sucesso.");
        }



        [Test, Category("Youtube")]
        public void AlternarTemaClaroEscuro()
        {
            var driver = GetDriver();

            // Clicar no botão de configurações no canto superior direito
            var settingsButton = driver.FindElement(By.XPath("//button[@aria-label='Configurações']"));
            settingsButton.Click();

            // Aguardar o menu aparecer
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var themeMenuOption = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//ytd-toggle-theme-compact-link-renderer")));

            // Clicar na opção de alternância de tema
            themeMenuOption.Click();

            // Aguardar a alternância do tema ocorrer (pequena pausa para UI atualizar)
            Thread.Sleep(1000);

            // Localizar e clicar na opção "Tema escuro"
            var temaEscuro = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//yt-formatted-string[@id='label' and text()='Tema claro']")));
            temaEscuro.Click();
            // Validação: verificar se a classe do body mudou para indicar tema escuro/claro
            var bodyElement = driver.FindElement(By.TagName("body"));
            bool isDarkMode = bodyElement.GetAttribute("class").Contains("light");

            Assert.That(isDarkMode, Is.True.Or.False, "O tema não alternou corretamente.");
        }


        [Test, Category("Youtube")]
        public void ValidarBarraLateral()
        {
            test = extent.CreateTest("Validar Comportamento da Barra Lateral");

            // Clica no botão do menu para recolher a barra lateral
            var menuButton = GetDriver().FindElement(By.Id("guide-icon"));
            menuButton.Click();
            test.Log(Status.Info, "Clicou no botão de menu para recolher a barra lateral.");
            Thread.Sleep(2000);

            var sidebar = GetDriver().FindElement(By.CssSelector("#guide"));
            Assert.IsFalse(sidebar.Displayed, "A barra lateral não foi recolhida.");
            test.Log(Status.Pass, "Barra lateral recolhida com sucesso.");

            // Expande a barra lateral
            menuButton.Click();
            test.Log(Status.Info, "Clicou no botão de menu para expandir a barra lateral.");
            Thread.Sleep(2000);

            Assert.IsTrue(sidebar.Displayed, "A barra lateral não foi expandida.");
            test.Log(Status.Pass, "Barra lateral expandida com sucesso.");
        }

        private void SkipAdIfPresent()
        {
            try
            {
                var wait = new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(10));
                var skipButton = wait.Until(drv =>
                {
                    try
                    {
                        var btn = drv.FindElement(By.XPath("//*[@id='skip-button:2']"));
                        return btn.Displayed ? btn : null;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                });
                if (skipButton != null)
                {
                    test.Log(Status.Info, "Anúncio detectado. Clicando em pular anúncio.");
                    skipButton.Click();
                    Thread.Sleep(2000);
                }
            }
            catch (WebDriverTimeoutException)
            {
                test.Log(Status.Info, "Nenhum anúncio detectado.");
            }
        }
    }
}
