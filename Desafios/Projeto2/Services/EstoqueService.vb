Public Class EstoqueService

    Private ReadOnly _produtos As List(Of ProdutoEstoqueDto)

    Public Sub New(produtos As List(Of ProdutoEstoqueDto))

        If produtos Is Nothing Then
            Throw New ArgumentNullException(NameOf(produtos))
        End If

        _produtos = produtos

    End Sub


    Public Function Movimentar(
        movimentacao As MovimentacaoEstoqueDto
    ) As ResultadoMovimentacaoDto

        ValidarMovimentacao(movimentacao)

        Dim produto As ProdutoEstoqueDto =
            ObterProduto(movimentacao.CodigoProduto)

        Dim estoqueAnterior As Integer =
            produto.Estoque


        Dim tipo As String =
            movimentacao.Descricao.Trim().ToLower()


        If tipo = "entrada" Then

            produto.Estoque += movimentacao.Quantidade


        ElseIf tipo = "saida" OrElse tipo = "saída" Then

            If movimentacao.Quantidade > produto.Estoque Then

                Throw New InvalidOperationException(
                    "Estoque insuficiente para realizar a saída."
                )

            End If

            produto.Estoque -= movimentacao.Quantidade


        Else

            Throw New ArgumentException(
                "Tipo de movimentação inválido. " &
                "Utilize Entrada ou Saída."
            )

        End If


        Return New ResultadoMovimentacaoDto With {
            .IdMovimentacao = movimentacao.Id,
            .CodigoProduto = produto.CodigoProduto,
            .DescricaoProduto = produto.DescricaoProduto,
            .TipoMovimentacao = movimentacao.Descricao,
            .EstoqueAnterior = estoqueAnterior,
            .QuantidadeMovimentada = movimentacao.Quantidade,
            .EstoqueAtual = produto.Estoque
        }

    End Function


    Private Function ObterProduto(
        codigoProduto As Integer
    ) As ProdutoEstoqueDto

        Dim produto =
            _produtos.FirstOrDefault(
                Function(x) x.CodigoProduto = codigoProduto
            )

        If produto Is Nothing Then

            Throw New KeyNotFoundException(
                $"Produto {codigoProduto} não encontrado."
            )

        End If

        Return produto

    End Function


    Private Sub ValidarMovimentacao(
        movimentacao As MovimentacaoEstoqueDto
    )

        If movimentacao Is Nothing Then

            Throw New ArgumentException(
                "A movimentação não pode ser nula."
            )

        End If


        If movimentacao.Id <= 0 Then

            Throw New ArgumentException(
                "O identificador da movimentação deve ser maior que zero."
            )

        End If


        If movimentacao.CodigoProduto <= 0 Then

            Throw New ArgumentException(
                "O código do produto é obrigatório."
            )

        End If


        If movimentacao.Quantidade <= 0 Then

            Throw New ArgumentException(
                "A quantidade deve ser maior que zero."
            )

        End If


        If String.IsNullOrWhiteSpace(
            movimentacao.Descricao
        ) Then

            Throw New ArgumentException(
                "A descrição da movimentação é obrigatória."
            )

        End If

    End Sub

End Class