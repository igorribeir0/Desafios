Public Class ComissaoService

    Public Function Calcular(
        vendas As IEnumerable(Of VendaDto)
    ) As List(Of ComissaoVendedorDto)

        If vendas Is Nothing Then
            Throw New ArgumentNullException(NameOf(vendas))
        End If

        Dim resultado As New Dictionary(Of String, ComissaoVendedorDto)(
            StringComparer.OrdinalIgnoreCase
        )

        For Each venda In vendas

            ValidarVenda(venda)

            Dim comissao As Decimal =
                CalcularComissao(venda.Valor)

            If Not resultado.ContainsKey(venda.Vendedor) Then

                resultado.Add(
                    venda.Vendedor,
                    New ComissaoVendedorDto With {
                        .Vendedor = venda.Vendedor,
                        .TotalVendas = 0D,
                        .TotalComissao = 0D
                    }
                )

            End If

            resultado(venda.Vendedor).TotalVendas += venda.Valor
            resultado(venda.Vendedor).TotalComissao += comissao

        Next

        Return resultado.Values.
            OrderByDescending(Function(x) x.TotalComissao).
            ToList()

    End Function


    Private Function CalcularComissao(valor As Decimal) As Decimal

        If valor < 100D Then
            Return 0D
        End If

        If valor < 500D Then
            Return valor * 0.01D
        End If

        Return valor * 0.05D

    End Function


    Private Sub ValidarVenda(venda As VendaDto)

        If venda Is Nothing Then
            Throw New ArgumentException(
                "A venda não pode ser nula."
            )
        End If

        If String.IsNullOrWhiteSpace(venda.Vendedor) Then
            Throw New ArgumentException(
                "O vendedor da venda não foi informado."
            )
        End If

        If venda.Valor < 0D Then
            Throw New ArgumentException(
                $"Valor de venda inválido para {venda.Vendedor}."
            )
        End If

    End Sub

End Class