# language: pt-br
Funcionalidade: Testes de Validação do Formulário 1

  @CenarioFeliz
  Cenário: Enviar o formulário 1 com sucesso preenchendo todos os campos obrigatórios
    Dado que eu acessei a página de formulários
    Quando preencho o nome "João", sobrenome "Silva" e telefone "11999998888"
    E clico no botão "Enviar" do Formulário 1
    Então o modal deve ser exibido com a mensagem "Enviado com sucesso!"
    Quando clico no botão de fechar do modal
    Então o modal deve desaparecer da tela

  @BugConhecido
  Cenário: Tentar enviar o formulário sem preencher os campos obrigatórios
    Dado que eu acessei a página de formulários
    Quando não preencho nenhum campo do Formulário 1
    E clico no botão "Enviar" do Formulário 1
    Então o modal com a mensagem de sucesso não deve ser exibido

  @ValidacaoCampos @BugConhecido
  Esquema do Cenário: Validar restrições de caracteres nos campos de nome e sobrenome
    Dado que eu acessei a página de formulários
    Quando preencho o nome "<Nome>", sobrenome "<Sobrenome>" e telefone "11999998888"
    E clico no botão "Enviar" do Formulário 1
    Então o modal com a mensagem de sucesso não deve ser exibido

    Exemplos:
      | Nome     | Sobrenome |
      | Raul123  | Silv4     |
      | J@oão    | M_endes   |
      | 99999    | 88888     |

  @ValidacaoCampos @BugConhecido
  Esquema do Cenário: Validar formatos inválidos e letras no campo telefone
    Dado que eu acessei a página de formulários
    Quando preencho o nome "Raul", sobrenome "Silveira" e telefone "<Telefone>"
    E clico no botão "Enviar" do Formulário 1
    Então o modal com a mensagem de sucesso não deve ser exibido

    Exemplos:
      | Telefone          |
      | OnzeNoveOitoOito  |
      | 119abcdefff       |
      | (11)9999999999999 |
      | 123               |

  @ValidacaoVisual @BugConhecido
  Cenário: Validar alertas visuais de campos obrigatórios ao perder o foco
    Dado que eu acessei a página de formulários
    Quando eu clico no campo de nome e mudo para o campo de sobrenome sem preencher
    Então o campo de nome deve exibir um alerta visual de erro ou borda vermelha