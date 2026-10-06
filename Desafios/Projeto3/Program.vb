Module Program

    Sub Main()

        Try

            Dim valor As Decimal = 1000D

            Dim dataVencimento As DateTime =
                DateTime.Today.AddDays(-5)


            Dim service As New JurosService()


            Dim resultado =
                service.Calcular(
                    valor,
                    dataVencimento
                )


            Console.WriteLine(
                "CÁLCULO DE JUROS"
            )

            Console.WriteLine(
                New String("-"c, 50)
            )

            Console.WriteLine(
                $"Valor original: " &
                $"R$ {resultado.ValorOriginal:N2}"
            )

            Console.WriteLine(
                $"Data de vencimento: " &
                $"{resultado.DataVencimento:dd/MM/yyyy}"
            )

            Console.WriteLine(
                $"Data do cálculo: " &
                $"{resultado.DataCalculo:dd/MM/yyyy}"
            )

            Console.WriteLine(
                $"Dias em atraso: " &
                $"{resultado.DiasAtraso}"
            )

            Console.WriteLine(
                $"Juros diário: " &
                $"{resultado.PercentualJurosDiario:P2}"
            )

            Console.WriteLine(
                $"Valor dos juros: " &
                $"R$ {resultado.ValorJuros:N2}"
            )

            Console.WriteLine(
                $"Valor atualizado: " &
                $"R$ {resultado.ValorAtualizado:N2}"
            )


        Catch ex As Exception

            Console.WriteLine(
                $"Erro: {ex.Message}"
            )

        End Try


        Console.ReadLine()

    End Sub

End Module