Imports System.Data.OleDb
Public Class FormLogin
    Private Sub btn_connect_Click(sender As Object, e As EventArgs) Handles btn_connect.Click
        ' 1. Vérification des champs vides [cite: 58]
        If txt_user.Text = "" Or txt_password.Text = "" Then
            MessageBox.Show("Veuillez remplir tous les champs.", "Champs obligatoires", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' 2. Utilisation de la connexion définie dans le module dbManager
        Using conn As New OleDbConnection(connString)
            Try
                conn.Open()
                ' Requête pour vérifier le login et récupérer le rôle [cite: 51, 52]
                Dim sql As String = "SELECT Role FROM Utilisateurs WHERE Username=? AND Password=?"
                Dim cmd As New OleDbCommand(sql, conn)

                cmd.Parameters.AddWithValue("Username", txt_user.Text)
                cmd.Parameters.AddWithValue("Password", txt_password.Text)

                Dim result = cmd.ExecuteScalar()

                If result IsNot Nothing Then
                    ' Connexion réussie : on stocke les infos dans le module [cite: 52]
                    CurrentUser = txt_user.Text
                    CurrentRole = result.ToString()

                    MessageBox.Show("Bienvenue, vous êtes connecté en tant que " & CurrentRole, "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Ouvrir le Menu Principal en remplaçant cette fenêtre
                    Me.Hide()
                    Using frm As New FormMenuPrincipal()
                        frm.ShowDialog()
                    End Using
                    ' lorsque le menu est fermé, réafficher le login
                    Me.Show()

                Else
                    MessageBox.Show("Identifiants incorrects.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Catch ex As Exception
                MessageBox.Show("Erreur base de données : " & ex.Message)
            End Try
        End Using
    End Sub
    Private Sub btn_quitter_Click(sender As Object, e As EventArgs) Handles btn_quitter.Click
        Application.Exit()
    End Sub
End Class
