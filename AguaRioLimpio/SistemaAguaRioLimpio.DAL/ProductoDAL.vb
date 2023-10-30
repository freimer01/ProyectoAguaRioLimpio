Imports System.Data.SqlClient
Imports SistemaAguaRioLimpio.Entities

Public Class ProductoDAL

    Inherits BaseDAL

    'METODOS CRUD 
    Public Shared Sub Create(producto As ProductoEntity)
        'CREAMOS LA CONEXION A LA BD
        Using Conex As New SqlConnection(m_CadenaDeconexion)
            Conex.Open()

            'CREAMOS LA SENTENCIA SQL PARA AGREGAR UN REGISTRO
            Dim sql As String = "INSERT INTO Producto (IdCategoria, Nombre, Descripcion, Precio, StockProducto, CategoriaProducto)
                               Values(@IdCategoria, @Nombre, @Descripcion @Precio,@StockProducto, @CategoriaProducto" &
                               "SELECT SCOPE_IDENTITY()"

            Dim cmd As New SqlCommand(sql, Conex)

            'AGREGAMOS EL PARAMETRO
            cmd.Parameters.AddWithValue("@Nombre", producto.Nombre)
            cmd.Parameters.AddWithValue("@Descrpcion", producto.Descripcion)
            cmd.Parameters.AddWithValue("@Precio", producto.Precio)
            cmd.Parameters.AddWithValue("@StockProducto", producto.StockProducto)
            cmd.Parameters.AddWithValue("@Categoria", producto.CategoriaProducto)


        End Using
    End Sub

    Private Shared Function m_CadenaDeconexion() As String
        Throw New NotImplementedException()
    End Function

    Public Shared Function GetByID(Id As Integer) As ProductoEntity
        Dim producto As ProductoEntity = Nothing

        Using conex As New SqlConnection(m_CadenaDeconexion)
            conex.Open()

            Dim sql As String = "SELECT * FROM Articulo Where ID=@IdArticulo"
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("IdArticulo", Id)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                producto = ConvertToObjet(reader)

            End If

        End Using

        Return producto
    End Function

    Public Shared Function GetByValor(Valor As String) As List(Of ProductoEntity)
        Dim list As New List(Of ProductoEntity)

        Using conex As New SqlConnection(m_CadenaDeconexion)
            conex.Open()

            Dim sql As String = "SELECT*FOM Producto" &
                                "WHERE Nombre Like '%' + @valor '&' or Descripcion Like '&' + @valor + '&' ORDER BY Nombre"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@valor", Valor)

            Dim reader As SqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                list.Add(ConvertToObjet(reader))
            End While
        End Using
        Return list

    End Function

    Public Shared Function GetAll() As List(Of ProductoEntity)
        Dim list As New List(Of ProductoEntity)

        Using conex As New SqlConnection(m_CadenaDeconexion)
            conex.Open()

            Dim sql As String = "SELECT*FOM Articulo ORDER BY Nombre"
            Dim cmd As New SqlCommand(sql, conex)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                list.Add(ConvertToObjet(reader))
            End While
        End Using
        Return list

    End Function

    Private Shared Function ConvertToObjet(reader As SqlDataReader) As ProductoEntity
        Throw New NotImplementedException()
    End Function

    Public Shared Sub Update(producto As ProductoEntity)
        'CREAMOS LA CONEXION A LA BD
        Using Conex As New SqlConnection(m_CadenaDeconexion)
            Conex.Open()

            'CREAMOS LA SENTENCIA SQL PARA AGREGAR UN REGISTRO
            Dim sql As String = "UPDATE Producto Set ID=@ID,
                                                     Nombre=@Nombre,
                                                     Descripcion=@Descripcion,
                                                     PrecioC=@Precio,
                                                     PrecioVenta=@precioVenta,
                                                     StockProducto=@StockProducto WHERE ID=@ID"


            Dim cmd As New SqlCommand(sql, Conex)

            'AGREGAMOS EL PARAMETRO
            cmd.Parameters.AddWithValue("@Nombre", producto.Nombre)
            cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion)
            cmd.Parameters.AddWithValue("@Precio", producto.Precio)
            cmd.Parameters.AddWithValue("@StockProducto", producto.StockProducto)
            cmd.Parameters.AddWithValue("@ID", producto.ID)
            cmd.Parameters.AddWithValue("@IdCategoria", producto.ID)

            cmd.ExecuteNonQuery()  'Ejecutar Comando

        End Using
    End Sub

    Public Shared Function Delete(Id As String) As Boolean
        Dim SeElimino As Boolean

        Using conex As New SqlConnection(m_CadenaDeconexion)
            conex.Open()

            Dim sql As String = "DELETE FROM Producto WHERE ID=@ID"
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("ID", Id)

            SeElimino = cmd.ExecuteNonQuery() > 0

        End Using

        Return SeElimino

    End Function


End Class
