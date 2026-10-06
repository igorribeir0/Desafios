Public Class JurosService

    Private Const JUROS_DIARIO As Decimal = 0.025D


    Public Function Calcular(
        valor As Decimal,
        dataVencimento As DateTime
    ) As CalculoJurosDto

        ValidarDados(
            valor,
            dataVencimento
        )


        Dim dataHoje As DateTime =
            DateTime.Today


        Dim diasAtraso As Integer = 0


        If dataHoje > dataVencimento.Date Then

            diasAtraso =
                (dataHoje - dataVencimento.Date).Days

        End If


        Dim valorJuros As Decimal =
            valor * JUROS_DIARIO * diasAtraso


        Dim valorAtualizado As Decimal =
            valor + valorJuros


        Return New CalculoJurosDto With {
            .ValorOriginal = valor,
            .DataVencimento = dataVencimento.Date,
            .DataCalculo = dataHoje,
            .DiasAtraso = diasAtraso,
            .PercentualJurosDiario = JUROS_DIARIO,
            .ValorJuros = valorJuros,
            .ValorAtualizado = valorAtualizado
        }

    End Function


    Private Sub ValidarDados(
        valor As Decimal,
        dataVencimento As DateTime
    )

        If valor <= 0D Then

            Throw New ArgumentException(
                "O valor deve ser maior que zero."
            )

        End If


        If dataVencimento = DateTime.MinValue Then

            Throw New ArgumentException(
                "A data de vencimento é obrigatória."
            )

        End If

    End Sub

End Class