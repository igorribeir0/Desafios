Imports System.Text.Json

Module Program

    Sub Main()

        Dim json As String = "
        {
            ""estoque"": [
                {
                    ""codigoProduto"": 101,
                    ""descricaoProduto"": ""Caneta Azul"",
                    ""estoque"": 150
                },
                {
                    ""codigoProduto"": 102,
                    ""descricaoProduto"": ""Caderno Universitário"",
                    ""estoque"": 75
                },
                {
                    ""codigoProduto"": 103,
                    ""descricaoProduto"": ""Borracha Branca"",
                    ""estoque"": 200
                },
                {
                    ""codigoProduto"": 104,
                    ""descricaoProduto"": ""Lápis Preto HB"",
                    ""estoque"": 320
                },
                {
                    ""codigoProduto"": 105,
                    ""descricaoProduto"": ""Marcador de Texto Amarelo"",
                    ""estoque"": 90
                }
            ]
        }"


        Try

            'Desserializa o JSON
            Dim dados As EstoqueDto =
                JsonSerializer.Deserialize(Of EstoqueDto)(
                    json,
                    New JsonSerializerOptions With {
                        .PropertyNameCaseInsensitive = True
                    }
                )


            If dados Is Nothing OrElse
               dados.Estoque Is Nothing Then

                Throw New Exception(
                    "Nenhum produto encontrado no estoque."
                )

            End If


            'Cria o serviço
            Dim service As New EstoqueService(
                dados.Estoque
            )


            'Cria uma movimentação
            Dim movimentacao As New MovimentacaoEstoqueDto With {
                .Id = 1,
                .CodigoProduto = 101,
                .Descricao = "Entrada",
                .Quantidade = 50
            }


            'Executa a movimentação
            Dim resultado =
                service.Movimentar(movimentacao)


            'Exibe o resultado
            Console.WriteLine(
                "MOVIMENTAÇÃO DE ESTOQUE"
            )

            Console.WriteLine(
                New String("-"c, 50)
            )

            Console.WriteLine(
                $"ID: {resultado.IdMovimentacao}"
            )

            Console.WriteLine(
                $"Produto: {resultado.CodigoProduto} - " &
                $"{resultado.DescricaoProduto}"
            )

            Console.WriteLine(
                $"Tipo: {resultado.TipoMovimentacao}"
            )

            Console.WriteLine(
                $"Estoque anterior: " &
                $"{resultado.EstoqueAnterior}"
            )

            Console.WriteLine(
                $"Quantidade movimentada: " &
                $"{resultado.QuantidadeMovimentada}"
            )

            Console.WriteLine(
                $"Estoque atual: " &
                $"{resultado.EstoqueAtual}"
            )


        Catch ex As Exception

            Console.WriteLine(
                $"Erro: {ex.Message}"
            )

        End Try


        Console.ReadLine()

    End Sub

End Module