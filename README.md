Amcom QA Automation Challenge
Suíte de testes automatizados desenvolvida em C# utilizando Selenium WebDriver e a abordagem de BDD (Behavior-Driven Development) com o framework Reqnroll. O objetivo deste projeto é validar o comportamento, as regras de negócio e a experiência de usuário (UI/UX) das telas propostas no desafio técnico da Amcom.

Tecnologias e Frameworks Utilizados
Linguagem: C# (.NET 8.0 / .NET 9.0)
Framework de Testes: NUnit (Engine de asserções)
Automação de Web: Selenium WebDriver (Chrome Driver)
Abordagem BDD: Reqnroll (Sucessor moderno do SpecFlow para ecossistemas .NET atuais)
Padrão de Arquitetura: Page Object Pattern (POM)
Estrutura do Projeto
O projeto foi arquitetado seguindo rigorosamente as boas práticas de engenharia de software para testes, separando a especificação viva do código de execução:

├── Features/
│   ├── Formulario1.feature               # Cenários BDD do Formulário 1 e Modal
│   └── Formulario2.feature               # Cenários BDD do Formulário 2 (Cores) e Ortografia
│   └── Formulario3.feature               # Cenários BDD do Formulário 3 Saudação
├── Pages/
│   └── FormularioPage.cs                 # Localizadores (Page Objects) e interações com a UI
├── Steps/
│   └── FormularioSteps.cs                # Amarração dos passos Gherkin com as ações da Page
└── amcom-qa-challenge.csproj             # Arquivo de configuração do projeto .NET


## Estratégia de Testes & Decisões de Arquitetura

1. **Page Object Pattern (POM):** Toda a inteligência de busca de seletores e sincronismo do Selenium foi centralizada na `FormularioPage`. Isso garante alta manutenibilidade (se um ID mudar no site, altera-se apenas em um lugar).
2. **Gerenciamento de Esperas (Explicit Waits):** Elementos assíncronos e modais dinâmicos utilizam `WebDriverWait` com condições baseadas em eventos (`_wait.Until`), eliminando testes instáveis (*flaky tests*).
3. **Pausas Estratégicas (Slow Motion):** Foram aplicadas pausas de execução de forma cirúrgica para fins de auditoria visual, permitindo acompanhar o preenchimento de campos e transições de tela durante a avaliação.
4. **Tratamento de Eventos (Blur/Loss of Focus):** Criamos cenários que simulam o comportamento real do usuário, focando e desfocando de campos obrigatórios para validar os gatilhos de validação do sistema.

---

##  Relatório de Execução & Bugs Mapeados (Resultados Esperados)

Ao rodar a suíte de testes com o comando `dotnet test`, o resultado reportará **17 testes executados, sendo 5 bem-sucedidos e 12 falhas**. 

> **Nota de Engenharia de Qualidade:** O volume elevado de falhas é o **resultado esperado e projetado** para esta suíte. Conforme orientado nas instruções do desafio (*"Fique a vontade para criar cenários de teste, inclusive os que possivelmente vão falhar"*), a automação foi desenhada como uma ferramenta de diagnóstico para evidenciar comportamentos inesperados do software testado.

### Principais Bugs Evidenciados pelas Falhas (Asserts):
* **Formulário 1 (Falta de Validação):** O modal de sucesso é disparado mesmo quando o formulário é submetido completamente em branco ou com dados inválidos.
* **Formulário 1 (Feedback Visual):** O sistema aceita a perda de foco de inputs obrigatórios sem aplicar nenhuma classe de erro (`invalid/error`) ou feedback visual imediato (borda vermelha) para orientar o usuário.
* **Formulário 2 (Inconsistência Visual):** A cor de fundo aplicada na página diverge drasticamente dos padrões RGB universais esperados para as strings selecionadas (ex: selecionar "blue" e o fundo renderizar um tom claro/ciano fora do padrão).
* **Qualidade de UI/UX (Erros Ortográficos):** Foi implementado um *Esquema de Cenário* dinâmico que valida a escrita dos cabeçalhos dos blocos. A automação acusa falha ortográfica nos três blocos da página, uma vez que a interface exibe a palavra de forma incorreta (**"Formulario"** sem acento), ferindo a norma culta padrão (**"Formulário"**).

---

## Como Executar o Projeto Localmente

### Pré-requisitos:
* [.NET SDK](https://dotnet.microsoft.com/download) instalado (Versão 8.0 ou superior).
* Navegador **Google Chrome** instalado na máquina.

### Passo a Passo:

1. Clone o repositório para sua máquina local:
   ```bash
   git clone (https://github.com/rauulsilveira/amcom-qa-challenge)

   ---

## BÔNUS: Demonstração Prática em Playwright (TypeScript)

 Sabendo que o Playwright é a principal ferramenta utilizada no dia a dia da Amcom, criei a branch paralela `feature/playwright-typescript-bonus` contendo um script completo em TypeScript (`amcom-challenge.spec.ts`).

* **Onde encontrar:** O arquivo está localizado na raiz desta branch.
* **Abordagem rápida:** Para garantir a agilidade na entrega do desafio, este script de bônus foi escrito sem o uso de Page Objects (POM), concentrando os 17 cenários mapeados de forma direta e funcional em um arquivo único.
* **Fidelidade:** Ele cobre rigorosamente os mesmos caminhos felizes, edge cases e mapeamentos de bugs do projeto original em C#.

---