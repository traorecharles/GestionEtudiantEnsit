Public Class FormMenuPrincipal
	Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = "Utilisateur : " & CurrentRole
        ' Adjust visible controls based on role
        Select Case CurrentRole
            Case "Administrateur"
                btn_paiement.Visible = True
                btn_user.Visible = True
                btn_rapport.Visible = True
            Case "Personnel"
                ' Personnel can manage payments but not access reports or user management
                btn_paiement.Visible = True
                btn_user.Visible = False
                btn_rapport.Visible = False
            Case Else
                ' Default: minimal permissions
                btn_paiement.Visible = False
                btn_user.Visible = False
                btn_rapport.Visible = False
        End Select
    End Sub

    Private Sub btn_etudiant_Click(sender As Object, e As EventArgs) Handles btn_etudiant.Click
        Try
            Me.Hide()
            Using frm As New FormEtudiant()
                frm.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show("Impossible d'ouvrir le formulaire des étudiants : " & ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Show()
        End Try
    End Sub

	Private Sub btn_reinscription_Click(sender As Object, e As EventArgs) Handles btn_reinscription.Click
		' Ouvre le formulaire de réinscription
        Try
            Me.Hide()
            Using frm As New FormReinscription()
                frm.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show("Impossible d'ouvrir le formulaire de réinscription : " & ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Show()
        End Try
	End Sub

	Private Sub btn_paiement_Click(sender As Object, e As EventArgs) Handles btn_paiement.Click
        Try
            Me.Hide()
            Using frm As New FormPaiement()
                frm.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show("Impossible d'ouvrir le formulaire de paiements : " & ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Show()
        End Try
	End Sub

	Private Sub btn_rapport_Click(sender As Object, e As EventArgs) Handles btn_rapport.Click
        Try
            Me.Hide()
            Using frm As New FormRapports()
                frm.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show("Impossible d'ouvrir le formulaire de rapports : " & ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Show()
        End Try
	End Sub

	Private Sub btn_user_Click(sender As Object, e As EventArgs) Handles btn_user.Click
        Try
            Me.Hide()
            Using frm As New FormUsers()
                frm.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show("Impossible d'ouvrir le formulaire de gestion des utilisateurs : " & ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Show()
        End Try
	End Sub

	Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
		' Déconnexion : fermer le menu pour revenir au login
		Try
			CurrentUser = ""
			CurrentRole = ""
			Me.Close()
		Catch ex As Exception
			MessageBox.Show("Erreur lors de la déconnexion : " & ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub
End Class