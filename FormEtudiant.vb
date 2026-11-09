Imports System.Data.OleDb

Public Class FormEtudiant
	Dim cheminPhoto As String = ""
	Private idSelectionne As Integer = 0

	Private Sub ChargerEtudiants()
		Using conn As New OleDbConnection(connString)
			conn.Open()

			Dim sql As String = "
            SELECT IdEtudiant,
				   Matricule, 
                   Nom, 
                   Prenom,
				   Niveau,
                   DateNaissance, 
                   LieuNaissance, 
                   Sexe, 
                   Telephone, 
                   Email, 
                   Adresse,
				   Photo
            FROM Etudiants"

			Dim adapter As New OleDbDataAdapter(sql, conn)
			Dim table As New DataTable()
			adapter.Fill(table)

			DataGridView1.DataSource = table
		End Using
	End Sub


	Private Sub choixSexe()
		ComboBox1.Items.Add("Masculin")
		ComboBox1.Items.Add("Féminin")
	End Sub

	Private Sub choixNiveau()
		ComboBox2.Items.Add("PREPA 1")
		ComboBox2.Items.Add("PREPA 2")
		ComboBox2.Items.Add("ING 1")
		ComboBox2.Items.Add("ING 2")
		ComboBox2.Items.Add("ING 3")
	End Sub

	Private Sub FormEtudiant_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		ChargerEtudiants()
		choixSexe()
		choixNiveau()
	End Sub


	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		' Vérification des champs obligatoires
		If TextBox1.Text.Trim() = "" Or
			TextBox2.Text.Trim() = "" Or
			TextBox3.Text.Trim() = "" Or
			TextBox5.Text.Trim() = "" Or
			ComboBox1.Text.Trim() = "" Or
			ComboBox2.Text.Trim() = "" Or
			TextBox6.Text.Trim() = "" Or
			TextBox7.Text.Trim() = "" Or
			TextBox8.Text.Trim() = "" Then

			MessageBox.Show("Veuillez remplir tous les champs avant d'ajouter l'étudiant.",
					"Champs obligatoires",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning)
			Exit Sub
		End If

		Using conn As New OleDbConnection(connString)
			conn.Open()

			Dim sql As String = "INSERT INTO Etudiants (Matricule, Nom, Prenom, Niveau, DateNaissance, LieuNaissance, Sexe, Telephone, Email, Adresse, Photo) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
			Dim cmd As New OleDbCommand(sql, conn)

			cmd.Parameters.Add("Matricule", OleDbType.VarChar).Value = TextBox1.Text
			cmd.Parameters.Add("Nom", OleDbType.VarChar).Value = TextBox2.Text
			cmd.Parameters.Add("Prenom", OleDbType.VarChar).Value = TextBox3.Text
			cmd.Parameters.Add("Niveau", OleDbType.VarChar).Value = ComboBox2.Text
			cmd.Parameters.Add("DateNaissance", OleDbType.Date).Value = DateTimePicker1.Value.Date
			cmd.Parameters.Add("LieuNaissance", OleDbType.VarChar).Value = TextBox5.Text
			cmd.Parameters.Add("Sexe", OleDbType.VarChar).Value = ComboBox1.Text
			cmd.Parameters.Add("Telephone", OleDbType.VarChar).Value = TextBox6.Text
			cmd.Parameters.Add("Email", OleDbType.VarChar).Value = TextBox7.Text
			cmd.Parameters.Add("Adresse", OleDbType.VarChar).Value = TextBox8.Text
			cmd.Parameters.Add("Photo", OleDbType.VarChar).Value = cheminPhoto



			cmd.ExecuteNonQuery()

			MessageBox.Show("Étudiant ajouté avec succès.")
			ChargerEtudiants()
		End Using

		' Vider les TextBox
		TextBox1.Clear()
		TextBox2.Clear()
		TextBox3.Clear()
		TextBox5.Clear()
		TextBox6.Clear()
		TextBox7.Clear()
		TextBox8.Clear()

		' Réinitialiser la ComboBox
		ComboBox1.SelectedIndex = -1
		ComboBox2.SelectedIndex = -1
		' Réinitialiser le DateTimePicker à aujourd'hui ou à la date par défaut
		DateTimePicker1.Value = DateTime.Now

		' Réinitialiser le chemin de la photo si nécessaire
		cheminPhoto = ""
	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
		' Vérifie s'il y a une ligne sélectionnée
		If DataGridView1.CurrentRow Is Nothing Then
			MessageBox.Show("Veuillez sélectionner un étudiant à supprimer.",
					"Aucune sélection",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning)
			Exit Sub
		End If

		' Vérifie que la cellule Matricule n'est pas vide
		If IsDBNull(DataGridView1.CurrentRow.Cells("Matricule").Value) OrElse String.IsNullOrWhiteSpace(Convert.ToString(DataGridView1.CurrentRow.Cells("Matricule").Value)) Then
			MessageBox.Show("Le matricule est invalide.",
					"Erreur",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error)
			Exit Sub
		End If

		' Récupération du matricule (sécurisée)
		Dim matriculeObj = DataGridView1.CurrentRow.Cells("Matricule").Value
		Dim matricule As String = If(matriculeObj Is Nothing OrElse IsDBNull(matriculeObj), String.Empty, Convert.ToString(matriculeObj).Trim())

		If String.IsNullOrWhiteSpace(matricule) Then
			MessageBox.Show("Le matricule est invalide ou vide. Suppression annulée.",
				"Erreur",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error)
			Exit Sub
		End If

		' Demande de confirmation (affiche le matricule supprimé)
		Dim confirmation As DialogResult = MessageBox.Show(
			$"Voulez-vous vraiment supprimer l'étudiant avec le matricule '{matricule}' ?",
			"Confirmation",
			MessageBoxButtons.YesNo,
			MessageBoxIcon.Question)

		If confirmation = DialogResult.No Then Exit Sub

		' Suppression
		Using conn As New OleDbConnection(connString)
			conn.Open()

			Dim sql As String = "DELETE FROM Etudiants WHERE Matricule=?"
			Dim cmd As New OleDbCommand(sql, conn)
			cmd.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule

			Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
			If rowsAffected = 0 Then
				MessageBox.Show("Aucune ligne supprimée. Vérifiez que le matricule existe en base de données.",
					"Information",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information)
			Else
				MessageBox.Show("Étudiant supprimé avec succès.",
					"Succès",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information)
			End If
		End Using

		ChargerEtudiants()
	End Sub

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		Dim openFile As New OpenFileDialog()
		openFile.Filter = "Images (*.jpg;*.png)|*.jpg;*.png"

		If openFile.ShowDialog() = DialogResult.OK Then

			' Dossier destination dans le projet
			Dim dossier As String = Application.StartupPath & "\PhotosEtudiants\"

			' Création du dossier si inexistant
			If Not IO.Directory.Exists(dossier) Then
				IO.Directory.CreateDirectory(dossier)
			End If

			' Nouveau nom unique basé sur le matricule + date
			Dim nouveauNom As String = TextBox1.Text & "_" & DateTime.Now.Ticks & ".jpg"
			Dim cheminDestination As String = dossier & nouveauNom

			' Chargement image originale
			Dim imgOriginal As Image = Image.FromFile(openFile.FileName)

			' 🔥 Redimension automatique (150x150)
			Dim imgRedimensionnee As New Bitmap(150, 150)
			Using g As Graphics = Graphics.FromImage(imgRedimensionnee)
				g.DrawImage(imgOriginal, 0, 0, 150, 150)
			End Using

			' Sauvegarde image redimensionnée
			imgRedimensionnee.Save(cheminDestination, Imaging.ImageFormat.Jpeg)

			' Libération mémoire
			imgOriginal.Dispose()
			imgRedimensionnee.Dispose()

			' Stocker chemin relatif
			cheminPhoto = "PhotosEtudiants\" & nouveauNom

			' Affichage dans PictureBox
			PictureBox1.Image = Image.FromFile(cheminDestination)
			PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage

		End If
	End Sub

	'Afficher la photo de l'étudiant sélectionné dans le DataGridView
	Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged

		If DataGridView1.CurrentRow Is Nothing Then Exit Sub
		If IsDBNull(DataGridView1.CurrentRow.Cells("Photo").Value) Then Exit Sub

		Dim cheminRelatif As String = DataGridView1.CurrentRow.Cells("Photo").Value.ToString()
		Dim cheminComplet As String = Application.StartupPath & "\" & cheminRelatif

		If IO.File.Exists(cheminComplet) Then
			PictureBox1.Image = Image.FromFile(cheminComplet)
			PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
		Else
			PictureBox1.Image = Nothing
		End If



	End Sub

	' Lorsque je clique sur une cellule du DataGridView
	Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

		If e.RowIndex >= 0 Then

			Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

			' On stocke l’ID (clé primaire)
			idSelectionne = Convert.ToInt32(row.Cells("IdEtudiant").Value)

			' Remplissage des champs
			TextBox1.Text = row.Cells("Matricule").Value.ToString()
			TextBox2.Text = row.Cells("Nom").Value.ToString()
			TextBox3.Text = row.Cells("Prenom").Value.ToString()
			ComboBox2.Text = row.Cells("Niveau").Value.ToString()
			DateTimePicker1.Value = Convert.ToDateTime(row.Cells("DateNaissance").Value)
			TextBox5.Text = row.Cells("LieuNaissance").Value.ToString()
			ComboBox1.Text = row.Cells("Sexe").Value.ToString()
			TextBox6.Text = row.Cells("Telephone").Value.ToString()
			TextBox7.Text = row.Cells("Email").Value.ToString()
			TextBox8.Text = row.Cells("Adresse").Value.ToString()

			' Photo
			cheminPhoto = row.Cells("Photo").Value.ToString()

			If cheminPhoto <> "" AndAlso IO.File.Exists(cheminPhoto) Then
				PictureBox1.Image = Image.FromFile(cheminPhoto)
			Else
				PictureBox1.Image = Nothing
			End If

		End If

	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

		If DataGridView1.CurrentRow Is Nothing Or idSelectionne = 0 Then
			MessageBox.Show("Veuillez sélectionner un étudiant dans la liste avant de modifier.",
						"Aucune sélection",
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning)
			Exit Sub
		End If

		Using conn As New OleDbConnection(connString)
			conn.Open()

			Dim sql As String = "UPDATE Etudiants SET Matricule=?, Nom=?, Prenom=?, Niveau=?, DateNaissance=?, LieuNaissance=?, Sexe=?, Telephone=?, Email=?, Adresse=?, Photo=? WHERE IdEtudiant=?"

			Dim cmd As New OleDbCommand(sql, conn)

			cmd.Parameters.Add("Matricule", OleDbType.VarChar).Value = TextBox1.Text
			cmd.Parameters.Add("Nom", OleDbType.VarChar).Value = TextBox2.Text
			cmd.Parameters.Add("Prenom", OleDbType.VarChar).Value = TextBox3.Text
			cmd.Parameters.Add("Niveau", OleDbType.VarChar).Value = ComboBox2.Text
			cmd.Parameters.Add("DateNaissance", OleDbType.Date).Value = DateTimePicker1.Value.Date
			cmd.Parameters.Add("LieuNaissance", OleDbType.VarChar).Value = TextBox5.Text
			cmd.Parameters.Add("Sexe", OleDbType.VarChar).Value = ComboBox1.Text
			cmd.Parameters.Add("Telephone", OleDbType.VarChar).Value = TextBox6.Text
			cmd.Parameters.Add("Email", OleDbType.VarChar).Value = TextBox7.Text
			cmd.Parameters.Add("Adresse", OleDbType.VarChar).Value = TextBox8.Text
			cmd.Parameters.Add("Photo", OleDbType.VarChar).Value = cheminPhoto

			cmd.Parameters.Add("IdEtudiant", OleDbType.Integer).Value = idSelectionne

			cmd.ExecuteNonQuery()

		End Using

		MessageBox.Show("Étudiant modifié avec succès.")

		' 🔥 Recharge le DataGridView
		ChargerEtudiants()

		' 🔄 Optionnel : réinitialiser la sélection
		idSelectionne = 0

	End Sub

	Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

	End Sub
End Class