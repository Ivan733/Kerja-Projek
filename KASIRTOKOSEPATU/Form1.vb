Imports System.Data.Odbc

Public Class LoginForm
    Dim connectionString As String = "DSN=db kasir" ' Ganti dengan DSN yang sesuai dengan database MySQL Anda

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles BtnLogin.Click
        Dim username As String = TxtUsername.Text.Trim()
        Dim password As String = TxtPassword.Text.Trim()

        If ValidateLogin(username, password) Then
            MessageBox.Show("Login berhasil!")
            ' Alihkan ke halaman Home setelah login berhasil
            Dim Home As New Home()
            Me.Hide() ' Sembunyikan formulir login
            Home.Show() ' Tampilkan halaman Home
        Else
            MessageBox.Show("Login berhasil!")
            ' Alihkan ke halaman Home setelah login berhasil
            Dim Home As New Home()
            Me.Hide() ' Sembunyikan formulir login
            Home.Show() ' Tampilkan halaman Home
        End If
    End Sub

    Private Function ValidateLogin(username As String, password As String) As Boolean
        Using connection As New OdbcConnection(connectionString)
            connection.Open()

            Dim query As String = "select *from tbkasir WHERE id_kasir = ? AND password = ?"
            Using cmd As New OdbcCommand(query, connection)
                cmd.Parameters.AddWithValue("@id_kasir", username)
                cmd.Parameters.AddWithValue("@password", password)

                Dim result As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return result > 0
            End Using
        End Using
    End Function

    Private Sub TxtPassword_Click(sender As Object, e As EventArgs) Handles TxtPassword.Click

    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        End
    End Sub
End Class
