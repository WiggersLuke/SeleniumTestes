# Testes Automatizados - YouTube

Abaixo, segue o link para o documento contendo todos os cenários que foram desenhados, usando os pontos abordados durante a entrevista.
https://docs.google.com/document/d/1RjhQ40dNlpJ0GI9FbjKRtK1kUbk5Z-qTFNJYKZtghYg/edit?usp=sharing

Este repositório contém a implementação de testes automatizados para o YouTube, utilizando a abordagem **Behavior-Driven Development (BDD)** com a tecnologia **Selenium WebDriver** e **NUnit**.

Optei por usar um template pronto que integra algumas ferramentas comuns no uso de selenium, para mostrar também a adaptabilidade dos casos desenhados em qualquer ambiente pronto que use o selenium.

Variei um pouco de caso em caso no uso de métodos e comentários do código. 

Só fiz o uso de IA para correções ortográficas e algumas questões de formatação do documento.

## Cenários de Teste

Foram elaborados os seguintes cenários de teste:

1. **Pesquisar e Reproduzir Vídeo**  
   - Descrição: O teste realiza uma busca por um vídeo no YouTube e verifica se o vídeo foi reproduzido corretamente após a seleção.

2. **Filtrar Vídeos ao Vivo**  
   - Descrição: Este teste valida a funcionalidade de filtragem de vídeos ao vivo no YouTube, garantindo que apenas vídeos ao vivo apareçam nos resultados.

3. **Fila de Reprodução**  
   - Descrição: O teste verifica a funcionalidade da fila de reprodução, incluindo a adição de vídeos à fila e a reprodução contínua de vídeos.

4. **Carregar Mais Resultados ao Rolar a Página**  
   - Descrição: Verifica se o YouTube carrega mais vídeos automaticamente ao rolar para baixo na página de resultados de pesquisa.

5. **Validar Barra Lateral**  
   - Descrição: Este teste garante que a barra lateral do YouTube (com sugestões e outros links) seja carregada corretamente.

6. **Alternar Tema Claro/Escuro**  
   - Descrição: Este teste valida a alternância entre os temas claro e escuro no YouTube, garantindo que a mudança de tema seja aplicada corretamente.

## Como Configurar o Projeto Localmente

### Pré-requisitos
- Baixar o arquivo zip na página de releases do repositório.

https://github.com/WiggersLuke/SeleniumTestes/releases/tag/TesteSelenium

### Passos para a execução
- Extrair a pasta TesteWiggers na raiz do diretório C
- Dentro da pasta existe um bat chamado 001 Executar Testes, basta executa-lo e aguardar a execução.
