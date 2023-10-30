Imports SistemaAguaRioLimpio.Entities
Imports System.Data.SqlClient
Imports System.Transactions

Public Class PedidoDAL
    Inherits BaseDAL

    Public Shadows Sub Create(pedido As PedidoEntity)
        'Inicializamos las Transacciones 
        Using scope As New TransactionScope
            Using m_CadenaDeconexion As New Object
                Using conex As New SqlConnection()
                    conex.Open()

                    '1. creamos el maestro del pedido
                    Dim sql As String = "INSERT INTO Pedido(IdCliente, Fecha, SubTotal, TotalDescuento," &
                        "TotalImpuesto, Total) Values(@idCliente, @Fecha, @subTotal,@totalDescuento," &
                    "@totalImpuesto, @total) SELECT SCOPE_IDENTITY()"

                    Dim cmd As New SqlCommand(sql, conex)
                    cmd.Parameters.AddWithValue("@idCliente", pedido.IdCliente)
                    cmd.Parameters.AddWithValue("@Fecha", pedido.Fecha)
                    cmd.Parameters.AddWithValue("@subTotal", pedido.TotalSubTotal)
                    cmd.Parameters.AddWithValue("@totalDescuento", pedido.TotalDescuento)
                    cmd.Parameters.AddWithValue("@totalImpuesto", pedido.TotalImpuesto)
                    cmd.Parameters.AddWithValue("@total", pedido.Total)
                    pedido.ID = Convert.ToInt32(cmd.ExecuteScalar())

                    '1. creamoslos detalles de pedido
                    Dim sqlDetallePedido As String = "INSERT INTO DetallePedido(IdPedido, IdArticulo, Cantidad," &
                        "Precio, SubTotal, Descuento, Impuesto, Total) " & "Values(@idPedido, @idArticulo, @cantidad,
                         @precio, @subTotal," & "@descuento, @impuesto, @total)" & "SELECT SCOPE_INDENTITY()"

                    Dim cmdDetalle As New SqlCommand(sqlDetallePedido, conex)
                    For Each detalle In pedido.Detalles
                        cmdDetalle.Parameters.Clear()

                        cmd.Parameters.AddWithValue("@idPedido", pedido.ID)
                        cmd.Parameters.AddWithValue("@idArticulo", detalle.IdArticulo)
                        cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad)
                        cmd.Parameters.AddWithValue("@precio", detalle.Precio)
                        cmd.Parameters.AddWithValue("@subTotal", detalle.SubTotal)
                        cmd.Parameters.AddWithValue("@descuento", detalle.Descuento)
                        cmd.Parameters.AddWithValue("@impuesto", detalle.Impuesto)
                        cmd.Parameters.AddWithValue("@total", detalle.Total)
                        detalle.ID = Convert.ToInt32(cmd.ExecuteScalar())
                    Next

                End Using

                scope.Complete()
            End Using
        End Using
    End Sub
    'Metodos Read
    Public Shared Function GetAll() As List(Of PedidoEntity)
        Dim lst As New List(Of PedidoEntity)

        Using Conex As New SqlConnection(m_CadenaConexion)
            Conex.Open()

            Dim sql As String = "SELECT* FRON Pedido ORDER BY Fecha DESC"
            Dim cmd As New SqlCommand(sql, Conex)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            'Leer cada fila devuelta por la BD y la conventimos en objetos
            While reader.Read()
                lst.Add(ConvertToObject(reader))
            End While
        End Using
        Return lst
    End Function

    Public Shadows Function GetById(id As Integer) As PedidoEntity
        Dim pedido As PedidoEntity = Nothing

        Using conex As New SqlConnection(m_CadenaDeconexion)
            conex.Open()

            Dim sql As String = "SELECT* FROM Pedido WHERE Id=@idPedido"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@idPedido", id)

            Dim reader As SqlDataReader = cmd.ExecuteReader()
            'leemos la unica fila que puede devolver esta sentencia y la convertimos en un objetos pedido
            If reader.Read() Then
                pedido = ConvertToObject(reader)

                'recuperamos todos los detalles del pedido de la BD 
                sql = "SELECT * FROM DetallePedido WHERE IdPedido=@idPedido"
                cmd = New SqlCommand(sql, conex)
                reader.Close()
                reader = cmd.ExecuteReader()

                While reader.Read()
                    Dim det As DetallePedidoEntity = ConvertToObjectDetalle(reader)

                    pedido.Detalles.Add(det)
                End While
            End If

        End Using

        Return pedido
    End Function

    Private Function m_CadenaDeconexion() As System.String
        Throw New NotImplementedException()
    End Function

    'metodo para convertir la fila de un DataReader en un objeto
    Private Shared Function ConvertToObject(reader As IDataReader) As PedidoEntity
        Dim opedido As New PedidoEntity
        opedido.ID = reader("ID")
        opedido.IdCliente = reader("IdCliente")
        opedido.Fecha = ("Fecha")

        Return opedido
    End Function

    Private Shared Function ConvertToObjectDetalle(reader As IDataReader) As DetallePedidoEntity
        Dim detalle As New DetallePedidoEntity

        detalle.ID = reader("ID")
        detalle.IdPedido = reader("idPedido")
        detalle.IdArticulo = reader("idArticulo")
        detalle.Cantidad = reader("cantidad")
        detalle.Descuento = reader("descuento")
        detalle.Precio = reader("precio")

        Return detalle

    End Function

    Public Sub Update(pedido As PedidoEntity)
        Throw New NotImplementedException()
    End Sub


End Class
