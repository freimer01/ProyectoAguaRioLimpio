Imports SistemaAguaRioLimpio.Entities
Imports System.Data.SqlClient


Public Class CategoriaDAL

        Inherits BaseDAL

        'Metodo CRUD(Create, Read, Update, Delete)'
        Public Shared Sub Create(categoria As CategoriaEntity)
            'Creamos la conexion a la base de datos'

            Using conex As New SqlConnection(m_CadenaConexion)
                conex.Open()

                'Creamos la sentencia SQL para agregar un registro'

                Dim sql As String = "INSERT INTO Categoria(Nombre) Values (@Nombre) SELECT SCOPE_IDENTITY()"
                Dim cmd As New SqlCommand(sql, conex)

                'agregamos el parametro'

                cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre)
                categoria.IdCategoria = cmd.ExecuteScalar()

            End Using
        End Sub

        Public Shadows Sub Update(categoria As CategoriaEntity)
            'Creamos la conexion a la base de datos'

            Using conex As New SqlConnection(m_CadenaConexion)
                conex.Open()

                'Creamos la sentencia SQL para agregar un registro'

                Dim sql As String = "UPDATE Categoria Set Nombre = @Nombre" &
                                "WHERE ID =idCategoria"
                Dim cmd As New SqlCommand(sql, conex)

                'agregamos el parametro'

                cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre)
                cmd.Parameters.AddWithValue("@idCategoria", categoria.IdCategoria)
                cmd.ExecuteNonQuery()

            End Using
        End Sub

        Public Shared Function Delete(id As Integer) As Boolean
            Dim SeElimino As Boolean

            Using conex As New SqlConnection(m_CadenaConexion)
                conex.Open()

                Dim sql As String = "DELETE FROM Categoria Where ID =@IdCategoria"
                Dim cmd As New SqlCommand(sql, conex)
                cmd.Parameters.AddWithValue("@IdCategoria", id)

                SeElimino = cmd.ExecuteNonQuery() > 0

            End Using

            Return SeElimino


        End Function

        Public Shared Function GetByID(id As Integer) As CategoriaEntity
            Dim categoria As CategoriaEntity = Nothing

            Using conex As New SqlConnection(m_CadenaConexion)
                conex.Open()

                Dim sql As String = "SELECT * FROM Categoria Where ID=@idCategoria"
                Dim cmd As New SqlCommand(sql, conex)
                cmd.Parameters.AddWithValue("@idCategoria", id)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    categoria = ConvertToObject(reader)
                End If

            End Using

            Return categoria
        End Function

        Public Shared Function GetByValor(valor As String) As List(Of CategoriaEntity)
            Dim list As New List(Of CategoriaEntity)

            Using conex As New SqlConnection(m_CadenaConexion)
                conex.Open()

                Dim sql As String = "SELECT * FROM CATEGORIA " &
                                "WHERE Nombre Like '%' + @Valor + '%' ORDER BY Nombre"

                Dim cmd As New SqlCommand(sql, conex)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    list.Add(ConvertToObject(reader))
                End While
            End Using
            Return list
        End Function

        Public Shared Function GetAll() As List(Of CategoriaEntity)
            Dim list As New List(Of CategoriaEntity)

            Using conex As New SqlConnection(m_CadenaConexion)
                conex.Open()

                Dim sql As String = "SELECT * FROM Categoria ORDER BY Nombre"
                Dim cmd As New SqlCommand(sql, conex)
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    list.Add(ConvertToObject(reader))
                End While
            End Using

            Return list
        End Function

        Private Shared Function ConvertToObject(reader As IDataReader) As CategoriaEntity
            Dim categoria As New CategoriaEntity()

            categoria.IdCategoria = reader("IdCategoria")
            categoria.Nombre = reader("Nombre")

            Return categoria

        End Function

    End Class


