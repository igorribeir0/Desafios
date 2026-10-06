Imports System.Text.Json

Module Program

    Sub Main()

        Dim json As String = "SEU JSON AQUI"

        Try

            '1. Desserializa
            Dim dados As VendasDto =
                JsonSerializer.Deserialize(Of VendasDto)(
                    json,
                    New JsonSerializerOptions With {
                        .PropertyNameCaseInsensitive = True
                    }
                )

            If dados Is Nothing OrElse dados.Vendas Is Nothing Then
                Throw New Exception(
                    "Nenhuma venda foi encontrada."
                )
            End If


            '2. Executa regra de negócio
            Dim service As New ComissaoService()

            Dim resultado =
                service.Calcular(dados.Vendas)


            '3. Apresenta resultado
            Console.WriteLine("RELATÓRIO DE COMISSÕES")
            Console.WriteLine(New String("-"c, 60))

            For Each vendedor In resultado

                Console.WriteLine(
                    $"Vendedor: {vendedor.Vendedor}"
                )

                Console.WriteLine(
                    $"Total de vendas: R$ {vendedor.TotalVendas:N2}"
                )

                Console.WriteLine(
                    $"Comissão: R$ {vendedor.TotalComissao:N2}"
                )

                Console.WriteLine(New String("-"c, 60))

            Next

        Catch ex As Exception

            Console.WriteLine(
                $"Erro: {ex.Message}"
            )

        End Try

        Console.ReadLine()

    End Sub

End Module