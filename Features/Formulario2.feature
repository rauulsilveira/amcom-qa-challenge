# language: pt-br
Funcionalidade: Testes de Alteração de Cor do Formulário 2

  @CoresFormulario2
  Esquema do Cenário: Validar a mudança da cor de fundo do Formulário 2 ao selecionar uma cor
    Dado que eu acessei a página de formulários
    Quando seleciono a cor "<Cor>" no combo box do Formulário 2
    Então o fundo do Formulário 2 deve mudar para a cor "<RGB>"

    Exemplos:
      | Cor    | RGB                |
      | blue   | rgb(173, 216, 230) |
      | yellow | rgb(247, 220, 111) |
      | red    | rgb(255, 0, 0)     |

 @ValidacaoOrtografica @BugConhecido
  Esquema do Cenário: Validar a ortografia correta dos títulos dos formulários
    Dado que eu acessei a página de formulários
    Então o título do bloco <Posicao> deve exibir a grafia correta "<TextoEsperado>" com acento

    Exemplos:
      | Posicao | TextoEsperado |
      | 1       | Formulário 1  |
      | 2       | Formulário 2  |
      | 3       | Formulário 3  |