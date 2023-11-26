Imports System.Data.Odbc
Public Class Barang
    Dim con As OdbcConnection
    Dim dr As OdbcDataReader
    Dim da As OdbcDataAdapter
    Dim ds As DataSet
    Dim dt As DataTable
    Dim cmd As OdbcCommand
    Sub koneksi()
        con = New OdbcConnection
        con.ConnectionString = "dsn=db kasir"
        con.Open()

    End Sub
    Sub simpan()
        koneksi()

        Dim sql As String = "INSERT INTO tbbarang VALUES (?, ?, ?, ?, ?)"
        cmd = New OdbcCommand(sql, con)

        ' Assuming TextBox1 to TextBox5 are TextBox controls in your form
        cmd.Parameters.AddWithValue("@param1", TextBox1.Text)
        cmd.Parameters.AddWithValue("@param2", TextBox2.Text)
        cmd.Parameters.AddWithValue("@param3", TextBox3.Text)
        cmd.Parameters.AddWithValue("@param4", TextBox4.Text)
        cmd.Parameters.AddWithValue("@param5", TextBox5.Text)

        Try
            cmd.ExecuteNonQuery()
            MsgBox("Menyimpan data BERHASIL", vbInformation, "INFORMASI")
        Catch ex As Exception
            MsgBox("Menyimpan data GAGAL", vbInformation, "PERINGATAN")
        End Try
    End Sub

    Sub tampil()
        DataGridView1.Rows.Clear()
        Try
            koneksi()
            da = New OdbcDataAdapter("select *from tbbarang order by id_brg asc", con)
            dt = New DataTable
            da.Fill(dt)
            For Each row In dt.Rows
                DataGridView1.Rows.Add(row(0), row(1), row(2), row(3), row(4))
            Next
            dt.Rows.Clear()
        Catch ex As Exception
            MsgBox("Menampilkan data GAGAL")
        End Try
    End Sub

    Private Sub Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampil()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        simpan()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        End
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim a As String = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
        If a = "" Then
            MsgBox("Data Barang yang dihapus belum DIPILIH")
        Else
            If (MessageBox.Show("Anda yakin menghapus data dengan id_brg=" & a &
        "...?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) =
        Windows.Forms.DialogResult.OK) Then
                koneksi()
                cmd = New OdbcCommand("DELETE FROM tbbarang WHERE id_brg='" & a & "'", con)
                Try
                    cmd.ExecuteNonQuery()
                    MsgBox("Menghapus data BERHASIL", vbInformation, "INFORMASI")
                    tampil()
                Catch ex As Exception
                    MsgBox("Error: " & ex.Message, vbExclamation, "ERROR")
                Finally
                    con.Close()
                End Try
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox5.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Focus()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Hide()
        Home.Show()
    End Sub

    Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Hide()
        Transaksi.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        Kasir.Show()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Panggil metode simpan untuk melakukan update
        Update()
    End Sub

    Sub Update()
        koneksi()

        ' Memeriksa apakah TextBox1 tidak kosong
        If Not String.IsNullOrEmpty(TextBox1.Text) Then
            ' Memeriksa apakah data dengan id_brg yang diinginkan sudah ada
            If CekDataExist(TextBox1.Text) Then
                ' Data sudah ada, maka lakukan update
                Dim sql As String = "UPDATE tbbarang SET nama_brg='" & TextBox2.Text & "', harga='" & TextBox3.Text & "', ukuran='" & TextBox4.Text & "', stok='" & TextBox5.Text & "' WHERE id_brg='" & TextBox1.Text & "'"
                cmd = New OdbcCommand(sql, con)

                Try
                    cmd.ExecuteNonQuery()
                    MsgBox("Mengupdate data BERHASIL", vbInformation, "INFORMASI")
                    tampil()
                Catch ex As Exception
                    MsgBox("Error: " & ex.Message, vbExclamation, "ERROR")
                End Try
            Else
                ' Data tidak ditemukan, tampilkan pesan kesalahan
                MsgBox("Data dengan id_brg '" & TextBox1.Text & "' tidak ditemukan", vbExclamation, "PERINGATAN")
            End If
        Else
            ' TextBox1 kosong, tampilkan pesan kesalahan
            MsgBox("Id_brg tidak boleh kosong", vbExclamation, "PERINGATAN")
        End If
    End Sub

    ' Fungsi untuk memeriksa apakah data dengan id_brg sudah ada
    Function CekDataExist(id_brg As String) As Boolean
        Dim sql As String = "SELECT COUNT(*) FROM tbbarang WHERE id_brg='" & id_brg & "'"
        cmd = New OdbcCommand(sql, con)

        ' Menggunakan ExecuteScalar untuk mendapatkan jumlah baris yang sesuai dengan id_brg
        Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

        ' Jika jumlah baris lebih dari 0, data sudah ada
        Return count > 0
    End Function

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        tampil()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        tampil()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class