Imports SistemaAguaRioLimpio.DAL
Imports SistemaAguaRioLimpio.Entities

Public Class CategoriaBLL

    Public Shared Sub Save(categoria As CategoriaEntity)
        If categoria.IdCategoria = 0 Then
            'Es una nueva categoria'
            CategoriaDAL.Create()

        Else
            'Es una actualizacion de una categoria existente '
        End If
    End Sub

End Class
