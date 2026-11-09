Imports System.Data.OleDb
Imports System.Windows.Forms

Public Class FormReinscription
    Inherits Form

    Private lblMatricule As Label
    Private txtMatricule As TextBox
    Private WithEvents btnSearch As Button
    Private lblNomLabel As Label
    Private lblPrenomLabel As Label
    Private lblNom As Label
    Private lblPrenom As Label
    Private comboAnnee As ComboBox
    Private comboNiveau As ComboBox
    Private dtpDate As DateTimePicker
    Private WithEvents btnReinscrire As Button
    Private DataGridViewReinscriptions As DataGridView

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Function TableExists(conn As OleDbConnection, tableName As String) As Boolean
        Try
            Dim schema = conn.GetSchema("Tables")
            For Each row As DataRow In schema.Rows
                Dim name As String = Convert.ToString(row("TABLE_NAME"))
                If String.Compare(name, tableName, True) = 0 Then
                    Return True
                End If
            Next
        Catch
            ' ignore and fallback
        End Try
        Return False
    End Function

    Private Sub InitializeComponent()
        Me.lblMatricule = New Label()
        Me.txtMatricule = New TextBox()
        Me.btnSearch = New Button()
        Me.lblNomLabel = New Label()
        Me.lblPrenomLabel = New Label()
        Me.lblNom = New Label()
        Me.lblPrenom = New Label()
        Me.comboAnnee = New ComboBox()
        Me.comboNiveau = New ComboBox()
        Me.dtpDate = New DateTimePicker()
        Me.btnReinscrire = New Button()
        Me.DataGridViewReinscriptions = New DataGridView()
        CType(Me.DataGridViewReinscriptions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        ' lblMatricule
        '
        Me.lblMatricule.AutoSize = True
        Me.lblMatricule.Location = New Drawing.Point(12, 15)
        Me.lblMatricule.Name = "lblMatricule"
        Me.lblMatricule.Size = New Drawing.Size(60, 15)
        Me.lblMatricule.Text = "Matricule :"
        '
        ' txtMatricule
        '
        Me.txtMatricule.Location = New Drawing.Point(12, 35)
        Me.txtMatricule.Name = "txtMatricule"
        Me.txtMatricule.Size = New Drawing.Size(180, 23)
        '
        ' btnSearch
        '
        Me.btnSearch.Location = New Drawing.Point(200, 33)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New Drawing.Size(90, 26)
        Me.btnSearch.Text = "Rechercher"
        '
        ' lblNomLabel
        '
        Me.lblNomLabel.AutoSize = True
        Me.lblNomLabel.Location = New Drawing.Point(12, 70)
        Me.lblNomLabel.Text = "Nom :"
        '
        ' lblNom
        '
        Me.lblNom.AutoSize = True
        Me.lblNom.Location = New Drawing.Point(60, 70)
        Me.lblNom.Text = "-"
        '
        ' lblPrenomLabel
        '
        Me.lblPrenomLabel.AutoSize = True
        Me.lblPrenomLabel.Location = New Drawing.Point(12, 95)
        Me.lblPrenomLabel.Text = "Prénom :"
        '
        ' lblPrenom
        '
        Me.lblPrenom.AutoSize = True
        Me.lblPrenom.Location = New Drawing.Point(70, 95)
        Me.lblPrenom.Text = "-"
        '
        ' comboAnnee
        '
        Me.comboAnnee.DropDownStyle = ComboBoxStyle.DropDownList
        Me.comboAnnee.Location = New Drawing.Point(12, 125)
        Me.comboAnnee.Size = New Drawing.Size(140, 23)

        ' comboNiveau
        '
        Me.comboNiveau.DropDownStyle = ComboBoxStyle.DropDownList
        Me.comboNiveau.Location = New Drawing.Point(12, 155)
        Me.comboNiveau.Size = New Drawing.Size(200, 23)
        '
        ' dtpDate
        '
        Me.dtpDate.Location = New Drawing.Point(230, 125)
        Me.dtpDate.Size = New Drawing.Size(130, 23)
        Me.dtpDate.Format = DateTimePickerFormat.Short
        '
        ' btnReinscrire
        '
        Me.btnReinscrire.Location = New Drawing.Point(380, 122)
        Me.btnReinscrire.Size = New Drawing.Size(110, 27)
        Me.btnReinscrire.Text = "Réinscrire"
        '
        ' DataGridViewReinscriptions
        '
        Me.DataGridViewReinscriptions.Location = New Drawing.Point(12, 195)
        Me.DataGridViewReinscriptions.Size = New Drawing.Size(760, 250)
        Me.DataGridViewReinscriptions.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.DataGridViewReinscriptions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewReinscriptions.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DataGridViewReinscriptions.BorderStyle = BorderStyle.FixedSingle
        Me.DataGridViewReinscriptions.EnableHeadersVisualStyles = True
        Me.DataGridViewReinscriptions.AllowUserToAddRows = False
        Me.DataGridViewReinscriptions.ReadOnly = True
        Me.DataGridViewReinscriptions.RowHeadersVisible = False
        Me.DataGridViewReinscriptions.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        '
        ' FormReinscription
        '
        Me.ClientSize = New Drawing.Size(784, 461)
        Me.Controls.Add(Me.lblMatricule)
        Me.Controls.Add(Me.txtMatricule)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.lblNomLabel)
        Me.Controls.Add(Me.lblPrenomLabel)
        Me.Controls.Add(Me.lblNom)
        Me.Controls.Add(Me.lblPrenom)
        Me.Controls.Add(Me.comboAnnee)
        Me.Controls.Add(Me.comboNiveau)
        Me.Controls.Add(Me.dtpDate)
        Me.Controls.Add(Me.btnReinscrire)
        Me.Controls.Add(Me.DataGridViewReinscriptions)
        Me.Name = "FormReinscription"
        Me.Text = "Gestion des Réinscriptions"
        CType(Me.DataGridViewReinscriptions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private Sub FormReinscription_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Remplir les années (exemples) : on peut améliorer selon le contexte
        Dim currentYear As Integer = DateTime.Now.Year
        Me.comboAnnee.Items.Clear()
        Me.comboAnnee.Items.Add(String.Format("{0}-{1}", currentYear, currentYear + 1))
        Me.comboAnnee.Items.Add(String.Format("{0}-{1}", currentYear - 1, currentYear))
        Me.comboAnnee.Items.Add(String.Format("{0}-{1}", currentYear + 1, currentYear + 2))
        Me.comboAnnee.SelectedIndex = 0

        ' Remplir niveaux (conformes au reste de l'application)
        Me.comboNiveau.Items.Clear()
        Me.comboNiveau.Items.AddRange(New Object(){"PREPA 1", "PREPA 2", "ING 1", "ING 2", "ING 3"})
        Me.comboNiveau.SelectedIndex = 0

        ChargerReinscriptions()
    End Sub

    Private Sub ChargerReinscriptions()
        Try
            Using conn As New OleDbConnection(connString)
                conn.Open()
                Dim sql As String = "SELECT * FROM Reinscriptions"
                Dim adapter As New OleDbDataAdapter(sql, conn)
                Dim table As New DataTable()
                adapter.Fill(table)
                DataGridViewReinscriptions.DataSource = table
                If DataGridViewReinscriptions.Columns.Contains("IdReinscription") Then
                    DataGridViewReinscriptions.Columns("IdReinscription").Visible = False
                End If
            End Using
        Catch ex As Exception
            ' Si la table n'existe pas ou erreur, on initialise la grille avec un DataTable vide
            Dim empty As New DataTable()
            empty.Columns.Add("IdReinscription")
            empty.Columns.Add("Matricule")
            empty.Columns.Add("Nom")
            empty.Columns.Add("Prenom")
            empty.Columns.Add("Annee")
            empty.Columns.Add("Niveau")
            empty.Columns.Add("DateReinscription", GetType(Date))
            DataGridViewReinscriptions.DataSource = empty
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim matricule As String = txtMatricule.Text.Trim()
        If String.IsNullOrWhiteSpace(matricule) Then
            MessageBox.Show("Veuillez entrer un matricule.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using conn As New OleDbConnection(connString)
            conn.Open()
            Dim sql As String = "SELECT Nom, Prenom, Niveau FROM Etudiants WHERE Matricule=?"
            Dim cmd As New OleDbCommand(sql, conn)
            cmd.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
            Using reader = cmd.ExecuteReader()
                If reader.Read() Then
                    lblNom.Text = If(IsDBNull(reader("Nom")), "", reader("Nom").ToString())
                    lblPrenom.Text = If(IsDBNull(reader("Prenom")), "", reader("Prenom").ToString())
                    ' Try to set current niveau into comboNiveau
                    Try
                        If Not IsDBNull(reader("Niveau")) Then
                            Dim niv = reader("Niveau").ToString()
                            If comboNiveau.Items.Contains(niv) Then
                                comboNiveau.SelectedItem = niv
                            Else
                                ' if format differs, attempt to map
                                comboNiveau.SelectedIndex = 0
                            End If
                        End If
                    Catch
                        ' ignore
                    End Try
                Else
                    MessageBox.Show("Aucun étudiant trouvé pour ce matricule.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    lblNom.Text = "-"
                    lblPrenom.Text = "-"
                End If
            End Using
        End Using
    End Sub

    Private Sub btnReinscrire_Click(sender As Object, e As EventArgs) Handles btnReinscrire.Click
        Dim matricule As String = txtMatricule.Text.Trim()
        If String.IsNullOrWhiteSpace(matricule) Then
            MessageBox.Show("Matricule invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If lblNom.Text = "-" OrElse lblPrenom.Text = "-" Then
            MessageBox.Show("Veuillez rechercher et sélectionner un étudiant avant de réinscrire.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim annee As String = Convert.ToString(comboAnnee.SelectedItem)
        Dim dateReins As Date = dtpDate.Value.Date
        Dim newNiveau As String = Convert.ToString(comboNiveau.SelectedItem)

        Dim confirmation = MessageBox.Show($"Confirmer la réinscription de {lblNom.Text} {lblPrenom.Text} (Matricule: {matricule}) pour l'année {annee} ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.No Then Return

        Using conn As New OleDbConnection(connString)
            conn.Open()
            ' Vérifier l'existence de la table Reinscriptions et la créer si nécessaire
            If Not TableExists(conn, "Reinscriptions") Then
                Dim createSql As String = "CREATE TABLE Reinscriptions (IdReinscription COUNTER PRIMARY KEY, Matricule TEXT(50), Nom TEXT(100), Prenom TEXT(100), Annee TEXT(20), Niveau TEXT(50), DateReinscription DATETIME)"
                Dim createCmd As New OleDbCommand(createSql, conn)
                createCmd.ExecuteNonQuery()
            End If

            ' Prevent duplicate reinscription for same year
            Dim dupCmd As New OleDbCommand("SELECT COUNT(*) FROM Reinscriptions WHERE Matricule=? AND Annee=?", conn)
            dupCmd.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
            dupCmd.Parameters.Add("Annee", OleDbType.VarChar).Value = annee
            Dim dupCount As Integer = Convert.ToInt32(dupCmd.ExecuteScalar())
            If dupCount > 0 Then
                MessageBox.Show("Réinscription déjà effectuée pour cet étudiant pour l'année sélectionnée.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Insérer la réinscription
            Dim insertSql As String = "INSERT INTO Reinscriptions (Matricule, Nom, Prenom, Annee, Niveau, DateReinscription) VALUES (?, ?, ?, ?, ?, ?)"
            Dim insertCmd As New OleDbCommand(insertSql, conn)
            insertCmd.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
            insertCmd.Parameters.Add("Nom", OleDbType.VarChar).Value = lblNom.Text
            insertCmd.Parameters.Add("Prenom", OleDbType.VarChar).Value = lblPrenom.Text
            insertCmd.Parameters.Add("Annee", OleDbType.VarChar).Value = annee
            insertCmd.Parameters.Add("Niveau", OleDbType.VarChar).Value = newNiveau
            insertCmd.Parameters.Add("DateReinscription", OleDbType.Date).Value = dateReins

            Dim rowsAffected As Integer = insertCmd.ExecuteNonQuery()
            If rowsAffected > 0 Then
                ' update student's current level
                Try
                    Dim upd As New OleDbCommand("UPDATE Etudiants SET Niveau=? WHERE Matricule=?", conn)
                    upd.Parameters.Add("Niveau", OleDbType.VarChar).Value = newNiveau
                    upd.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
                    upd.ExecuteNonQuery()
                Catch
                End Try

                MessageBox.Show("Réinscription enregistrée et niveau mis à jour.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Échec de l'enregistrement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Using

        ChargerReinscriptions()
    End Sub

    Private Sub FormReinscription_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ' Assurez un chargement initial
        ChargerReinscriptions()
    End Sub
End Class
