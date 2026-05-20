# language: pt-br
Funcionalidade: Testes de Saudação Dinâmica do Formulário 3

  @SaudacaoHorario
  Cenário: Validar exibição da saudação baseada no horário do sistema
    Dado que eu acessei a página de formulários
    Quando clico no botão "Ver horario" do Formulário 3
    Então o sistema deve exibir a saudação correta baseada na hora atual do computador