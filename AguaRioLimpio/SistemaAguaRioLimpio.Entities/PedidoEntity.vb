Public Class PedidoEntity

    Sub New()
        Detalles = New List(Of DetallePedidoEntity)
    End Sub

    Public Property Detalles() As List(Of DetallePedidoEntity)

    Public Property ID() As Integer
    Public Property IdCliente() As Integer
    Public Property Fecha() As DateTime


    'Creamos propiedades Calculadas para los totales'
    'Utilizamos LINQ de Microsoft'

    Public ReadOnly Property TotalSubTotal() As Decimal
        Get
            Return (From d In Detalles Select d.SubTotal).Sum()
        End Get
    End Property

    Public ReadOnly Property TotalDescuento() As Decimal
        Get
            Return (From d In Detalles Select d.Descuento).Sum()
        End Get
    End Property

    Public ReadOnly Property TotalImpuesto() As Decimal
        Get
            Return (From d In Detalles Select d.Impuesto).Sum()
        End Get
    End Property

    Public ReadOnly Property Total() As Decimal
        Get
            Return TotalSubTotal - TotalDescuento + TotalImpuesto
        End Get
    End Property



End Class
