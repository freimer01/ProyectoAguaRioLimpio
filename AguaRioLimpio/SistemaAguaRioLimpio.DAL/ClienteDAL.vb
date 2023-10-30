Imports SistemaAguaRioLimpio.Entities
Imports System.Data.SqlClient

Public Class ClienteDAL
    Inherits BaseDAL

    'METODOS CRUD 
    Public Shared Sub Create(cliente As ClienteEntity)
        'CREAMOS LA CONEXION A LA BD
        Using Conex As New SqlConnection(m_CadenaDeconexion)
            Conex.Open()

            'CREAMOS LA SENTENCIA SQL PARA AGREGAR UN REGISTRO
            Dim sql As String = "INSERT INTO Cliente (Nombre, Apellido, Cedula, Direccion, Telefono, Email, FechaRegistro," &
                           "Values(@IdCliente, @nombre, @apellido, @cedula, @direccion, @telefono, @email, @FechaRegistro," &
                               "WHERE ID = IdCliente"


            Dim cmd As New SqlCommand(sql, Conex)

            'AGREGAMOS EL PARAMETRO
            cmd.Parameters.AddWithValue("@nombre", cliente.Nombre)
            cmd.Parameters.AddWithValue("@apellido", cliente.Apellidos)
            cmd.Parameters.AddWithValue("@cedula", cliente.Cedula)
            cmd.Parameters.AddWithValue("@direccion", cliente.Direccion)
            cmd.Parameters.AddWithValue("@telefono", cliente.Telefono)
            cmd.Parameters.AddWithValue("@email", cliente.Email)
            cmd.Parameters.AddWithValue("@idCliente", cliente.IdCliente)
            cmd.Parameters.AddWithValue("@FechaRegistro", cliente.FechaRegistro)

            cliente.IdCliente = Convert.ToInt32(cmd.ExecuteScalar())

        End Using
    End Sub

    Private Shared Function m_CadenaDeconexion() As Object
        Throw New NotImplementedException()
    End Function

    Public Shared Function GetByID(Id As Integer) As ClienteEntity
        Dim cliente As ClienteEntity = Nothing

        Using conex As New SqlConnection(m_CadenaDeconexion)
            conex.Open()

            Dim sql As String = "SELECT * FROM Articulo Where ID=@IdCliente"
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("IdIdcliente", Id)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                cliente = ConvertToObjet(reader)

            End If

        End Using

        Return cliente
    End Function

    Public Shared Function GetByValor(Valor As String) As List(Of ClienteEntity)
        Dim list As New List(Of ClienteEntity)

        Using conex As New SqlConnection(m_CadenaDeconexion)
            conex.Open()

            Dim sql As String = "SELECT*FOM Cliente" &
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

    Public Shared Function GetAll() As List(Of ClienteEntity)
        Dim list As New List(Of ClienteEntity)

        Using conex As New SqlConnection(m_CadenaDeconexion)
            conex.Open()

            Dim sql As String = "SELECT*FOM Cliente ORDER BY Nombre"
            Dim cmd As New SqlCommand(sql, conex)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                list.Add(ConvertToObjet(reader))
            End While
        End Using
        Return list

    End Function

    Private Shared Function ConvertToObjet(reader As SqlDataReader) As ClienteEntity
        Throw New NotImplementedException()
    End Function

    Public Shared Sub Update(cliente As ClienteEntity)
        'CREAMOS LA CONEXION A LA BD
        Using Conex As New SqlConnection(m_CadenaDeconexion)
            Conex.Open()

            'CREAMOS LA SENTENCIA SQL PARA AGREGAR UN REGISTRO
            Dim sql As String = "UPDATE Cliente Set ID=@IdCliente,
                                                     Nombre=@Nombre,
                                                     Cedula=@Cedula,
                                                     Apellido=@Apellidos,
                                                     Direccion=@Direccion,
                                                     Telefono=@Telefono,
                                                     Email=@email,
                                                     FechaRegistro=@Fecharegistro,
                                                     WHERE ID=@IdCliente"

            Dim cmd As New SqlCommand(sql, Conex)

            'AGREGAMOS EL PARAMETRO
            cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre)
            cmd.Parameters.AddWithValue("@Apellido", cliente.Apellidos)
            cmd.Parameters.AddWithValue("@Cedula", cliente.Cedula)
            cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion)
            cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono)
            cmd.Parameters.AddWithValue("@Email", cliente.Email)
            cmd.Parameters.AddWithValue("@Idcliente", cliente.IdCliente)
            cmd.Parameters.AddWithValue("@FechaRegistro", cliente.FechaRegistro)

            cmd.ExecuteNonQuery()  'Ejecutar Comando

        End Using
    End Sub

    Public Shared Function Delete(Id As String) As Boolean
        Dim SeElimino As Boolean

        Using conex As New SqlConnection(m_CadenaDeconexion)
            conex.Open()

            Dim sql As String = "DELETE FROM Cliente WHERE ID=@IdCliente"
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("IdCliente", Id)

            SeElimino = cmd.ExecuteNonQuery() > 0

        End Using

        Return SeElimino

    End Function


End Class
