Imports System.Data.OleDb
Imports System.Windows.Forms

Public Class FormPaiement
    Inherits Form

    Private lblMatricule As Label
    Private txtMatricule As TextBox
    Private WithEvents btnSearch As Button
    Private lblNom As Label
    Private lblPrenom As Label
    Private lblNomLabel As Label
    Private lblPrenomLabel As Label
    Private lblMontant As Label
    Private txtMontant As TextBox
    Private dtpDate As DateTimePicker
    Private WithEvents btnPayer As Button
    Private WithEvents btnEdit As Button
    Private WithEvents btnDelete As Button
    Private WithEvents comboType As ComboBox
    Private WithEvents btnSetExpected As Button
    Private WithEvents btnShowPending As Button
    Private WithEvents DataGridViewPaiements As DataGridView

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

    Private Function ColumnExists(conn As OleDbConnection, tableName As String, columnName As String) As Boolean
        Try
            Dim cmd As New OleDbCommand($"SELECT TOP 1 [{columnName}] FROM [{tableName}]", conn)
            ' ExecuteReader just to see if column exists
            Using rdr = cmd.ExecuteReader()
                Return True
            End Using
        Catch
            Return False
        End Try
    End Function

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub DataGridViewPaiements_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridViewPaiements.SelectionChanged
        If DataGridViewPaiements.CurrentRow Is Nothing Then Return
        Try
            Dim row = DataGridViewPaiements.CurrentRow
            Dim idObj = row.Cells("IdPaiement").Value
            Dim matObj = row.Cells("Matricule").Value
            Dim nomObj = row.Cells("Nom").Value
            Dim prenomObj = row.Cells("Prenom").Value
            Dim montantObj = row.Cells("Montant").Value
            Dim dateObj = row.Cells("DatePaiement").Value

            txtMatricule.Text = If(matObj Is Nothing OrElse IsDBNull(matObj), "", matObj.ToString())
            lblNom.Text = If(nomObj Is Nothing OrElse IsDBNull(nomObj), "-", nomObj.ToString())
            lblPrenom.Text = If(prenomObj Is Nothing OrElse IsDBNull(prenomObj), "-", prenomObj.ToString())
            txtMontant.Text = If(montantObj Is Nothing OrElse IsDBNull(montantObj), "", Convert.ToString(montantObj))
            If Not (dateObj Is Nothing OrElse IsDBNull(dateObj)) Then
                Dim dt As DateTime
                If DateTime.TryParse(Convert.ToString(dateObj), dt) Then
                    dtpDate.Value = dt
                End If
            End If
        Catch
            ' ignore selection parsing errors
        End Try
    End Sub

    Private Sub InitializeComponent()
        Me.lblMatricule = New Label()
        Me.txtMatricule = New TextBox()
        Me.btnSearch = New Button()
        Me.lblNomLabel = New Label()
        Me.lblPrenomLabel = New Label()
        Me.lblNom = New Label()
        Me.lblPrenom = New Label()
        Me.lblMontant = New Label()
        Me.txtMontant = New TextBox()
        Me.dtpDate = New DateTimePicker()
        Me.btnPayer = New Button()
        Me.DataGridViewPaiements = New DataGridView()
        CType(Me.DataGridViewPaiements, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblMatricule
        '
        Me.lblMatricule.AutoSize = True
        Me.lblMatricule.Location = New Drawing.Point(12, 15)
        Me.lblMatricule.Text = "Matricule :"
        '
        'txtMatricule
        '
        Me.txtMatricule.Location = New Drawing.Point(12, 35)
        Me.txtMatricule.Size = New Drawing.Size(160, 23)
        '
        'btnSearch
        '
        Me.btnSearch.Location = New Drawing.Point(180, 33)
        Me.btnSearch.Size = New Drawing.Size(90, 26)
        Me.btnSearch.Text = "Rechercher"
        '
        'lblNomLabel/nom
        '
        Me.lblNomLabel.AutoSize = True
        Me.lblNomLabel.Location = New Drawing.Point(12, 70)
        Me.lblNomLabel.Text = "Nom :"
        Me.lblNom.AutoSize = True
        Me.lblNom.Location = New Drawing.Point(60, 70)
        Me.lblNom.Text = "-"
        '
        'lblPrenomLabel/prenom
        '
        Me.lblPrenomLabel.AutoSize = True
        Me.lblPrenomLabel.Location = New Drawing.Point(12, 95)
        Me.lblPrenomLabel.Text = "Prénom :"
        Me.lblPrenom.AutoSize = True
        Me.lblPrenom.Location = New Drawing.Point(70, 95)
        Me.lblPrenom.Text = "-"
        '
        'lblMontant & txtMontant
        '
        Me.lblMontant.AutoSize = True
        Me.lblMontant.Location = New Drawing.Point(12, 125)
        Me.lblMontant.Text = "Montant :"
        Me.txtMontant.Location = New Drawing.Point(80, 122)
        Me.txtMontant.Size = New Drawing.Size(100, 23)
        '
        ' comboType
        '
        Me.comboType = New ComboBox()
        ' place type selector near the search controls on the top row
        Me.comboType.Location = New Drawing.Point(300, 35)
        Me.comboType.Size = New Drawing.Size(140, 23)
        Me.comboType.DropDownStyle = ComboBoxStyle.DropDownList
        Me.comboType.Items.AddRange(New Object() {"Inscription", "Materiel", "Examen", "Autre"})
        Me.comboType.SelectedIndex = 0
        '
        ' btnSetExpected
        '
        Me.btnSetExpected = New Button()
        Me.btnSetExpected.Location = New Drawing.Point(450, 33)
        Me.btnSetExpected.Size = New Drawing.Size(100, 26)
        Me.btnSetExpected.Text = "Définir attendu"
        '
        ' btnShowPending
        '
        Me.btnShowPending = New Button()
        Me.btnShowPending.Location = New Drawing.Point(560, 33)
        Me.btnShowPending.Size = New Drawing.Size(100, 26)
        Me.btnShowPending.Text = "Voir impayés"
        '
        'dtpDate
        '
        Me.dtpDate.Location = New Drawing.Point(200, 122)
        Me.dtpDate.Format = DateTimePickerFormat.Short
        Me.dtpDate.Size = New Drawing.Size(110, 23)
        '
        'btnPayer
        '
        Me.btnPayer.Location = New Drawing.Point(320, 120)
        Me.btnPayer.Size = New Drawing.Size(90, 27)
        Me.btnPayer.Text = "Payer"
        '
        'btnEdit
        '
        Me.btnEdit = New Button()
        Me.btnEdit.Location = New Drawing.Point(420, 120)
        Me.btnEdit.Size = New Drawing.Size(90, 27)
        Me.btnEdit.Text = "Modifier"
        '
        'btnDelete
        '
        Me.btnDelete = New Button()
        Me.btnDelete.Location = New Drawing.Point(520, 120)
        Me.btnDelete.Size = New Drawing.Size(90, 27)
        Me.btnDelete.Text = "Supprimer"
        '
        'DataGridViewPaiements
        '
        Me.DataGridViewPaiements.Location = New Drawing.Point(12, 165)
        Me.DataGridViewPaiements.Size = New Drawing.Size(760, 280)
        Me.DataGridViewPaiements.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.DataGridViewPaiements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewPaiements.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DataGridViewPaiements.BorderStyle = BorderStyle.FixedSingle
        Me.DataGridViewPaiements.AllowUserToAddRows = False
        Me.DataGridViewPaiements.ReadOnly = True
        Me.DataGridViewPaiements.RowHeadersVisible = False
        Me.DataGridViewPaiements.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        '
        'FormPaiement
        '
        Me.ClientSize = New Drawing.Size(784, 461)
        Me.Controls.Add(Me.lblMatricule)
        Me.Controls.Add(Me.txtMatricule)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.lblNomLabel)
        Me.Controls.Add(Me.lblNom)
        Me.Controls.Add(Me.lblPrenomLabel)
        Me.Controls.Add(Me.lblPrenom)
        Me.Controls.Add(Me.comboType)
        Me.Controls.Add(Me.btnSetExpected)
        Me.Controls.Add(Me.btnShowPending)
        Me.Controls.Add(Me.lblMontant)
        Me.Controls.Add(Me.txtMontant)
        Me.Controls.Add(Me.dtpDate)
        Me.Controls.Add(Me.btnPayer)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.DataGridViewPaiements)
        Me.Name = "FormPaiement"
        Me.Text = "Gestion des Paiements"
        CType(Me.DataGridViewPaiements, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Private Sub FormPaiement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' ensure comboType has values (in case InitializeComponent didn't run items)
        If comboType.Items.Count = 0 Then
            comboType.Items.AddRange(New Object() {"Inscription", "Materiel", "Examen", "Autre"})
            comboType.SelectedIndex = 0
        End If
        ChargerPaiements()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        ' Modifier le paiement sélectionné
        If DataGridViewPaiements.CurrentRow Is Nothing Then
            MessageBox.Show("Veuillez sélectionner un paiement à modifier.", "Aucune sélection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim idObj = DataGridViewPaiements.CurrentRow.Cells("IdPaiement").Value
        If idObj Is Nothing OrElse IsDBNull(idObj) Then
            MessageBox.Show("Identifiant paiement invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim montant As Double
        If Not Double.TryParse(txtMontant.Text.Trim(), montant) Then
            MessageBox.Show("Montant invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim matricule As String = txtMatricule.Text.Trim()
        If String.IsNullOrWhiteSpace(matricule) Then
            MessageBox.Show("Matricule invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim datePaiement As Date = dtpDate.Value.Date

        Dim id As Integer
        Try
            id = CInt(idObj)
        Catch ex As Exception
            MessageBox.Show("Impossible de récupérer l'identifiant du paiement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        Using conn As New OleDbConnection(connString)
            conn.Open()
            Dim updateSql As String = "UPDATE Paiements SET Matricule=?, Nom=?, Prenom=?, Montant=?, DatePaiement=? WHERE IdPaiement=?"
            Dim cmd As New OleDbCommand(updateSql, conn)
            cmd.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
            cmd.Parameters.Add("Nom", OleDbType.VarChar).Value = lblNom.Text
            cmd.Parameters.Add("Prenom", OleDbType.VarChar).Value = lblPrenom.Text
            cmd.Parameters.Add("Montant", OleDbType.Double).Value = montant
            cmd.Parameters.Add("DatePaiement", OleDbType.Date).Value = datePaiement
            cmd.Parameters.Add("IdPaiement", OleDbType.Integer).Value = id

            Dim rows As Integer = cmd.ExecuteNonQuery()
            If rows > 0 Then
                MessageBox.Show("Paiement modifié.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Aucune modification effectuée.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using

        ChargerPaiements()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If DataGridViewPaiements.CurrentRow Is Nothing Then
            MessageBox.Show("Veuillez sélectionner un paiement à supprimer.", "Aucune sélection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim idObj = DataGridViewPaiements.CurrentRow.Cells("IdPaiement").Value
        If idObj Is Nothing OrElse IsDBNull(idObj) Then
            MessageBox.Show("Identifiant paiement invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim id As Integer
        Try
            id = CInt(idObj)
        Catch ex As Exception
            MessageBox.Show("Impossible de récupérer l'identifiant du paiement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        Dim confirmation = MessageBox.Show($"Voulez-vous vraiment supprimer le paiement ID {id} ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.No Then Return

        Using conn As New OleDbConnection(connString)
            conn.Open()
            Dim sql As String = "DELETE FROM Paiements WHERE IdPaiement=?"
            Dim cmd As New OleDbCommand(sql, conn)
            cmd.Parameters.Add("IdPaiement", OleDbType.Integer).Value = id
            Dim rows As Integer = cmd.ExecuteNonQuery()
            If rows > 0 Then
                MessageBox.Show("Paiement supprimé.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Aucun paiement supprimé. Vérifiez l'ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using

        ChargerPaiements()
    End Sub

    Private Sub ChargerPaiements()
        Try
            Using conn As New OleDbConnection(connString)
                conn.Open()
                Dim sql As String = "SELECT * FROM Paiements"
                Dim adapter As New OleDbDataAdapter(sql, conn)
                Dim table As New DataTable()
                adapter.Fill(table)
                DataGridViewPaiements.DataSource = table
                If DataGridViewPaiements.Columns.Contains("IdPaiement") Then
                    DataGridViewPaiements.Columns("IdPaiement").Visible = False
                End If
            End Using
        Catch ex As Exception
            Dim empty As New DataTable()
            empty.Columns.Add("IdPaiement")
            empty.Columns.Add("Matricule")
            empty.Columns.Add("Nom")
            empty.Columns.Add("Prenom")
            empty.Columns.Add("Montant", GetType(Double))
            empty.Columns.Add("DatePaiement", GetType(Date))
            empty.Columns.Add("TypeFrais")
            empty.Columns.Add("Annee")
            DataGridViewPaiements.DataSource = empty
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
            Dim sql As String = "SELECT Nom, Prenom FROM Etudiants WHERE Matricule=?"
            Dim cmd As New OleDbCommand(sql, conn)
            cmd.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
            Using reader = cmd.ExecuteReader()
                If reader.Read() Then
                    lblNom.Text = If(IsDBNull(reader("Nom")), "", reader("Nom").ToString())
                    lblPrenom.Text = If(IsDBNull(reader("Prenom")), "", reader("Prenom").ToString())
                Else
                    MessageBox.Show("Aucun étudiant trouvé pour ce matricule.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    lblNom.Text = "-"
                    lblPrenom.Text = "-"
                End If
            End Using
        End Using
    End Sub

    Private Sub btnPayer_Click(sender As Object, e As EventArgs) Handles btnPayer.Click
        Dim matricule As String = txtMatricule.Text.Trim()
        If String.IsNullOrWhiteSpace(matricule) Then
            MessageBox.Show("Matricule invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim montant As Double
        If Not Double.TryParse(txtMontant.Text.Trim(), montant) Then
            MessageBox.Show("Montant invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim datePaiement As Date = dtpDate.Value.Date

        Using conn As New OleDbConnection(connString)
            conn.Open()
            ' Create table only if it does not exist
            If Not TableExists(conn, "Paiements") Then
                Dim createSql As String = "CREATE TABLE Paiements (IdPaiement COUNTER PRIMARY KEY, Matricule TEXT(50), Nom TEXT(100), Prenom TEXT(100), Montant DOUBLE, DatePaiement DATETIME, TypeDeFrais TEXT(50), Annee TEXT(20), MontantAttendu DOUBLE, ResteApayer DOUBLE)"
                Dim createCmd As New OleDbCommand(createSql, conn)
                createCmd.ExecuteNonQuery()
            End If

            ' Ensure columns exist in case the table was created by an older version
            Try
                Dim alter1 As New OleDbCommand("ALTER TABLE Paiements ADD COLUMN TypeDeFrais TEXT(50)", conn)
                alter1.ExecuteNonQuery()
            Catch ex As Exception
                ' ignore if column exists or cannot be added
            End Try
            Try
                Dim alter2 As New OleDbCommand("ALTER TABLE Paiements ADD COLUMN MontantAttendu DOUBLE", conn)
                alter2.ExecuteNonQuery()
            Catch ex As Exception
                ' ignore
            End Try
            Try
                Dim alter3 As New OleDbCommand("ALTER TABLE Paiements ADD COLUMN ResteApayer DOUBLE", conn)
                alter3.ExecuteNonQuery()
            Catch ex As Exception
                ' ignore
            End Try

            Dim annee As String = String.Format("{0}-{1}", datePaiement.Year, datePaiement.Year + 1)
            ' Determine which optional columns exist and build the INSERT dynamically
            Dim cols As New List(Of String) From {"Matricule", "Nom", "Prenom", "Montant", "DatePaiement"}
            Dim paramNames As New List(Of String) From {"Matricule", "Nom", "Prenom", "Montant", "DatePaiement"}

            ' TypeDeFrais might be named TypeDeFrais or TypeFrais in the DB
            Dim typeCol As String = If(ColumnExists(conn, "Paiements", "TypeDeFrais"), "TypeDeFrais", If(ColumnExists(conn, "Paiements", "TypeFrais"), "TypeFrais", Nothing))
            If typeCol IsNot Nothing Then
                cols.Add(typeCol)
                paramNames.Add(typeCol)
            End If

            Dim anneeCol As String = If(ColumnExists(conn, "Paiements", "Annee"), "Annee", Nothing)
            If anneeCol IsNot Nothing Then
                cols.Add(anneeCol)
                paramNames.Add(anneeCol)
            End If

            Dim montAttCol As String = If(ColumnExists(conn, "Paiements", "MontantAttendu"), "MontantAttendu", If(ColumnExists(conn, "Paiements", "MontantAtt"), "MontantAtt", Nothing))
            If montAttCol IsNot Nothing Then
                cols.Add(montAttCol)
                paramNames.Add(montAttCol)
            End If

            Dim resteCol As String = If(ColumnExists(conn, "Paiements", "ResteApayer"), "ResteApayer", If(ColumnExists(conn, "Paiements", "ResteApaye"), "ResteApaye", Nothing))
            If resteCol IsNot Nothing Then
                cols.Add(resteCol)
                paramNames.Add(resteCol)
            End If

            Dim insertSql As String = String.Format("INSERT INTO Paiements ({0}) VALUES ({1})", String.Join(", ", cols), String.Join(", ", Enumerable.Repeat("?", cols.Count)))
            Dim insertCmd As New OleDbCommand(insertSql, conn)
            ' Add mandatory parameters
            insertCmd.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
            insertCmd.Parameters.Add("Nom", OleDbType.VarChar).Value = lblNom.Text
            insertCmd.Parameters.Add("Prenom", OleDbType.VarChar).Value = lblPrenom.Text
            insertCmd.Parameters.Add("Montant", OleDbType.Double).Value = montant
            insertCmd.Parameters.Add("DatePaiement", OleDbType.Date).Value = datePaiement
            ' Add optional params in same order as cols
            If typeCol IsNot Nothing Then insertCmd.Parameters.Add(typeCol, OleDbType.VarChar).Value = comboType.SelectedItem.ToString()
            If anneeCol IsNot Nothing Then insertCmd.Parameters.Add(anneeCol, OleDbType.VarChar).Value = annee
            If montAttCol IsNot Nothing Then
                ' try to get expected amount from FraisAttendus
                Dim expected As Double = 0
                Try
                    Dim cmdGet As New OleDbCommand("SELECT MontantAttendu FROM FraisAttendus WHERE Matricule=? AND TypeFrais=? AND Annee=?", conn)
                    cmdGet.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
                    cmdGet.Parameters.Add("TypeFrais", OleDbType.VarChar).Value = If(typeCol IsNot Nothing, comboType.SelectedItem.ToString(), "")
                    cmdGet.Parameters.Add("Annee", OleDbType.VarChar).Value = annee
                    Dim obj = cmdGet.ExecuteScalar()
                    If obj IsNot Nothing AndAlso Not IsDBNull(obj) Then expected = Convert.ToDouble(obj)
                Catch
                    expected = 0
                End Try
                insertCmd.Parameters.Add(montAttCol, OleDbType.Double).Value = expected
            End If
            If resteCol IsNot Nothing Then
                ' compute tentative reste: MontantAttendu (if known) - (existing payments + this one)
                Dim totalPaye As Double = 0
                Try
                    ' build sum SQL depending on which type column exists
                    Dim hasTypeDe = ColumnExists(conn, "Paiements", "TypeDeFrais")
                    Dim hasType = ColumnExists(conn, "Paiements", "TypeFrais")
                    Dim typeCondition As String = ""
                    If hasTypeDe AndAlso hasType Then
                        typeCondition = "(TypeDeFrais=? OR TypeFrais=?) AND "
                    ElseIf hasTypeDe Then
                        typeCondition = "TypeDeFrais=? AND "
                    ElseIf hasType Then
                        typeCondition = "TypeFrais=? AND "
                    End If
                    Dim sumSql As String = "SELECT Nz(SUM(Montant),0) FROM Paiements WHERE Matricule=? AND " & typeCondition & "Annee=?"
                    Dim sumCmd As New OleDbCommand(sumSql, conn)
                    sumCmd.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
                    If hasTypeDe AndAlso hasType Then
                        sumCmd.Parameters.Add("Type1", OleDbType.VarChar).Value = comboType.SelectedItem.ToString()
                        sumCmd.Parameters.Add("Type2", OleDbType.VarChar).Value = comboType.SelectedItem.ToString()
                    ElseIf hasTypeDe Then
                        sumCmd.Parameters.Add("Type", OleDbType.VarChar).Value = comboType.SelectedItem.ToString()
                    ElseIf hasType Then
                        sumCmd.Parameters.Add("Type", OleDbType.VarChar).Value = comboType.SelectedItem.ToString()
                    End If
                    sumCmd.Parameters.Add("Annee", OleDbType.VarChar).Value = annee
                    Dim s = sumCmd.ExecuteScalar()
                    If s IsNot Nothing AndAlso Not IsDBNull(s) Then totalPaye = Convert.ToDouble(s)
                Catch
                    totalPaye = montant
                End Try
                Dim expectedVal As Double = 0
                If montAttCol IsNot Nothing Then
                    ' expected value was added to parameters earlier; retrieve it
                    expectedVal = If(insertCmd.Parameters(montAttCol) IsNot Nothing, Convert.ToDouble(insertCmd.Parameters(montAttCol).Value), 0)
                End If
                Dim reste As Double = expectedVal - (totalPaye + montant)
                insertCmd.Parameters.Add(resteCol, OleDbType.Double).Value = reste
            End If

            Dim rowsAffected As Integer = insertCmd.ExecuteNonQuery()
            If rowsAffected > 0 Then
                ' After inserting, update MontantAttendu and ResteApayer for this Matricule/Type/Annee
                Dim montantAttendu As Double = 0
                Try
                    Dim cmdGet As New OleDbCommand("SELECT MontantAttendu FROM FraisAttendus WHERE Matricule=? AND TypeFrais=? AND Annee=?", conn)
                    cmdGet.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
                    cmdGet.Parameters.Add("TypeFrais", OleDbType.VarChar).Value = comboType.SelectedItem.ToString()
                    cmdGet.Parameters.Add("Annee", OleDbType.VarChar).Value = annee
                    Dim obj = cmdGet.ExecuteScalar()
                    If obj IsNot Nothing AndAlso Not IsDBNull(obj) Then montantAttendu = Convert.ToDouble(obj)
                Catch
                    montantAttendu = 0
                End Try

                ' compute total paid for this combination
                Dim totalPaye As Double = 0
                Try
                    ' compute total paid for this combination using actual column name(s)
                    Dim hasTypeDe2 = ColumnExists(conn, "Paiements", "TypeDeFrais")
                    Dim hasType2 = ColumnExists(conn, "Paiements", "TypeFrais")
                    Dim sumSql2 As String
                    If hasTypeDe2 AndAlso hasType2 Then
                        sumSql2 = "SELECT Nz(SUM(Montant),0) FROM Paiements WHERE Matricule=? AND (TypeDeFrais=? OR TypeFrais=?) AND Annee=?"
                    ElseIf hasTypeDe2 Then
                        sumSql2 = "SELECT Nz(SUM(Montant),0) FROM Paiements WHERE Matricule=? AND TypeDeFrais=? AND Annee=?"
                    ElseIf hasType2 Then
                        sumSql2 = "SELECT Nz(SUM(Montant),0) FROM Paiements WHERE Matricule=? AND TypeFrais=? AND Annee=?"
                    Else
                        sumSql2 = "SELECT Nz(SUM(Montant),0) FROM Paiements WHERE Matricule=? AND Annee=?"
                    End If
                    Dim sumCmd As New OleDbCommand(sumSql2, conn)
                    sumCmd.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
                    If hasTypeDe2 AndAlso hasType2 Then
                        sumCmd.Parameters.Add("Type1", OleDbType.VarChar).Value = comboType.SelectedItem.ToString()
                        sumCmd.Parameters.Add("Type2", OleDbType.VarChar).Value = comboType.SelectedItem.ToString()
                        sumCmd.Parameters.Add("Annee", OleDbType.VarChar).Value = annee
                    ElseIf hasTypeDe2 OrElse hasType2 Then
                        sumCmd.Parameters.Add("Type", OleDbType.VarChar).Value = comboType.SelectedItem.ToString()
                        sumCmd.Parameters.Add("Annee", OleDbType.VarChar).Value = annee
                    Else
                        sumCmd.Parameters.Add("Annee", OleDbType.VarChar).Value = annee
                    End If
                    Dim s = sumCmd.ExecuteScalar()
                    If s IsNot Nothing AndAlso Not IsDBNull(s) Then totalPaye = Convert.ToDouble(s)
                Catch
                    totalPaye = montant
                End Try

                Dim reste As Double = montantAttendu - totalPaye

                Try
                    Dim upd As New OleDbCommand("UPDATE Paiements SET MontantAttendu=?, ResteApayer=? WHERE Matricule=? AND TypeDeFrais=? AND Annee=?", conn)
                    upd.Parameters.Add("MontantAttendu", OleDbType.Double).Value = montantAttendu
                    upd.Parameters.Add("ResteApayer", OleDbType.Double).Value = reste
                    upd.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
                    upd.Parameters.Add("TypeDeFrais", OleDbType.VarChar).Value = comboType.SelectedItem.ToString()
                    upd.Parameters.Add("Annee", OleDbType.VarChar).Value = annee
                    upd.ExecuteNonQuery()
                Catch
                    ' ignore update errors
                End Try

                MessageBox.Show("Paiement enregistré.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Échec de l'enregistrement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Using

        ChargerPaiements()
    End Sub

    Private Sub FormPaiement_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ChargerPaiements()
    End Sub

    Private Sub btnSetExpected_Click(sender As Object, e As EventArgs) Handles btnSetExpected.Click
        Dim matricule As String = txtMatricule.Text.Trim()
        If String.IsNullOrWhiteSpace(matricule) Then
            MessageBox.Show("Veuillez entrer un matricule pour définir un montant attendu.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim typeFrais As String = comboType.SelectedItem.ToString()
        Dim annee As String = String.Format("{0}-{1}", dtpDate.Value.Year, dtpDate.Value.Year + 1)

        Dim input = Microsoft.VisualBasic.Interaction.InputBox($"Montant attendu pour {typeFrais} ({annee}) pour l'étudiant {matricule} :", "Montant attendu", "0")
        Dim montantAttendu As Double
        If Not Double.TryParse(input, montantAttendu) Then
            MessageBox.Show("Montant invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using conn As New OleDbConnection(connString)
            conn.Open()
            ' create table if not exists
            Try
                Dim checkCmd As New OleDbCommand("SELECT COUNT(*) FROM FraisAttendus", conn)
                checkCmd.ExecuteScalar()
            Catch ex As Exception
                Dim createSql As String = "CREATE TABLE FraisAttendus (IdFrais COUNTER PRIMARY KEY, Matricule TEXT(50), TypeFrais TEXT(50), Annee TEXT(20), MontantAttendu DOUBLE)"
                Dim createCmd As New OleDbCommand(createSql, conn)
                createCmd.ExecuteNonQuery()
            End Try

            ' insert or update
            Dim upsertSql As String = "SELECT COUNT(*) FROM FraisAttendus WHERE Matricule=? AND TypeFrais=? AND Annee=?"
            Dim upsertCmd As New OleDbCommand(upsertSql, conn)
            upsertCmd.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
            upsertCmd.Parameters.Add("TypeFrais", OleDbType.VarChar).Value = typeFrais
            upsertCmd.Parameters.Add("Annee", OleDbType.VarChar).Value = annee
            Dim count As Integer = Convert.ToInt32(upsertCmd.ExecuteScalar())
            If count > 0 Then
                Dim updateSql As String = "UPDATE FraisAttendus SET MontantAttendu=? WHERE Matricule=? AND TypeFrais=? AND Annee=?"
                Dim cmd As New OleDbCommand(updateSql, conn)
                cmd.Parameters.Add("MontantAttendu", OleDbType.Double).Value = montantAttendu
                cmd.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
                cmd.Parameters.Add("TypeFrais", OleDbType.VarChar).Value = typeFrais
                cmd.Parameters.Add("Annee", OleDbType.VarChar).Value = annee
                cmd.ExecuteNonQuery()
            Else
                Dim insertSql As String = "INSERT INTO FraisAttendus (Matricule, TypeFrais, Annee, MontantAttendu) VALUES (?, ?, ?, ?)"
                Dim cmd As New OleDbCommand(insertSql, conn)
                cmd.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
                cmd.Parameters.Add("TypeFrais", OleDbType.VarChar).Value = typeFrais
                cmd.Parameters.Add("Annee", OleDbType.VarChar).Value = annee
                cmd.Parameters.Add("MontantAttendu", OleDbType.Double).Value = montantAttendu
                cmd.ExecuteNonQuery()
            End If

            ' Update existing Paiements rows so MontantAttendu and ResteApayer reflect this new expected amount
            Try
                Dim montAttCol As String = If(ColumnExists(conn, "Paiements", "MontantAttendu"), "MontantAttendu", If(ColumnExists(conn, "Paiements", "MontantAtt"), "MontantAtt", Nothing))
                Dim resteCol As String = If(ColumnExists(conn, "Paiements", "ResteApayer"), "ResteApayer", If(ColumnExists(conn, "Paiements", "ResteApaye"), "ResteApaye", Nothing))
                Dim typeCol As String = If(ColumnExists(conn, "Paiements", "TypeDeFrais"), "TypeDeFrais", If(ColumnExists(conn, "Paiements", "TypeFrais"), "TypeFrais", Nothing))
                Dim anneeCol As String = If(ColumnExists(conn, "Paiements", "Annee"), "Annee", Nothing)

                ' compute total already paid for this student/type/annee
                Dim totalPaye As Double = 0
                Try
                    Dim sumCmd As New OleDbCommand("SELECT Nz(SUM(Montant),0) FROM Paiements WHERE Matricule=?" & If(typeCol IsNot Nothing, " AND (TypeDeFrais=? OR TypeFrais=?)", "") & If(anneeCol IsNot Nothing, " AND Annee=?", ""), conn)
                    sumCmd.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
                    If typeCol IsNot Nothing Then
                        sumCmd.Parameters.Add("Type1", OleDbType.VarChar).Value = typeFrais
                        sumCmd.Parameters.Add("Type2", OleDbType.VarChar).Value = typeFrais
                    End If
                    If anneeCol IsNot Nothing Then sumCmd.Parameters.Add("Annee", OleDbType.VarChar).Value = annee
                    Dim s = sumCmd.ExecuteScalar()
                    If s IsNot Nothing AndAlso Not IsDBNull(s) Then totalPaye = Convert.ToDouble(s)
                Catch
                    totalPaye = 0
                End Try

                Dim reste As Double = montantAttendu - totalPaye

                If montAttCol IsNot Nothing Or resteCol IsNot Nothing Then
                    Dim updateCols As New List(Of String)
                    If montAttCol IsNot Nothing Then updateCols.Add(montAttCol & "=?")
                    If resteCol IsNot Nothing Then updateCols.Add(resteCol & "=?")
                    Dim updSql As String = String.Format("UPDATE Paiements SET {0} WHERE Matricule=?" & If(typeCol IsNot Nothing, " AND (TypeDeFrais=? OR TypeFrais=?)", "") & If(anneeCol IsNot Nothing, " AND Annee=?", ""), String.Join(", ", updateCols))
                    Dim updCmd As New OleDbCommand(updSql, conn)
                    If montAttCol IsNot Nothing Then updCmd.Parameters.Add("MontantAtt", OleDbType.Double).Value = montantAttendu
                    If resteCol IsNot Nothing Then updCmd.Parameters.Add("ResteAp", OleDbType.Double).Value = reste
                    updCmd.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
                    If typeCol IsNot Nothing Then
                        updCmd.Parameters.Add("Type1", OleDbType.VarChar).Value = typeFrais
                        updCmd.Parameters.Add("Type2", OleDbType.VarChar).Value = typeFrais
                    End If
                    If anneeCol IsNot Nothing Then updCmd.Parameters.Add("Annee", OleDbType.VarChar).Value = annee
                    updCmd.ExecuteNonQuery()
                End If
            Catch
                ' ignore update errors
            End Try
        End Using

        MessageBox.Show("Montant attendu enregistré.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnShowPending_Click(sender As Object, e As EventArgs) Handles btnShowPending.Click
        ' If a matricule is selected or entered, show popup details for that student & type
        Dim matricule As String = txtMatricule.Text.Trim()
        If String.IsNullOrWhiteSpace(matricule) AndAlso DataGridViewPaiements.CurrentRow IsNot Nothing Then
            Dim mobj = DataGridViewPaiements.CurrentRow.Cells("Matricule").Value
            If mobj IsNot Nothing Then matricule = mobj.ToString()
        End If

        If String.IsNullOrWhiteSpace(matricule) Then
            ' Show list of all impayés
            Using conn As New OleDbConnection(connString)
                conn.Open()
                Dim sql As String = "SELECT f.Matricule, e.Nom, e.Prenom, f.TypeFrais, f.Annee, f.MontantAttendu, Nz(SUM(p.Montant),0) AS TotalPaye, (f.MontantAttendu - Nz(SUM(p.Montant),0)) AS Solde " & _
                                    "FROM (FraisAttendus AS f LEFT JOIN Paiements AS p ON (p.Matricule=f.Matricule AND p.TypeFrais=f.TypeFrais AND p.Annee=f.Annee)) LEFT JOIN Etudiants AS e ON e.Matricule=f.Matricule " & _
                                    "GROUP BY f.Matricule, e.Nom, e.Prenom, f.TypeFrais, f.Annee, f.MontantAttendu " & _
                                    "HAVING f.MontantAttendu > Nz(SUM(p.Montant),0)"
                Dim adapter As New OleDbDataAdapter(sql, conn)
                Dim table As New DataTable()
                adapter.Fill(table)
                DataGridViewPaiements.DataSource = table
                If DataGridViewPaiements.Columns.Contains("IdPaiement") Then
                    DataGridViewPaiements.Columns("IdPaiement").Visible = False
                End If
            End Using
            Return
        End If

        Dim typeFrais As String = If(comboType.SelectedItem IsNot Nothing, comboType.SelectedItem.ToString(), String.Empty)
        Dim annee As String = String.Format("{0}-{1}", dtpDate.Value.Year, dtpDate.Value.Year + 1)

        Using conn As New OleDbConnection(connString)
            conn.Open()
            ' total already paid for this student/type/annee
            Dim totalPaye As Double = 0
            Try
                Dim sumSql As String = "SELECT Nz(SUM(Montant),0) FROM Paiements WHERE Matricule=?"
                If ColumnExists(conn, "Paiements", "TypeDeFrais") Or ColumnExists(conn, "Paiements", "TypeFrais") Then
                    sumSql = "SELECT Nz(SUM(Montant),0) FROM Paiements WHERE Matricule=? AND (TypeDeFrais=? OR TypeFrais=?)"
                End If
                If ColumnExists(conn, "Paiements", "Annee") Then sumSql &= " AND Annee=?"
                Dim sumCmd As New OleDbCommand(sumSql, conn)
                sumCmd.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
                If sumSql.Contains("TypeDeFrais") Or sumSql.Contains("TypeFrais") Then
                    sumCmd.Parameters.Add("Type1", OleDbType.VarChar).Value = typeFrais
                    sumCmd.Parameters.Add("Type2", OleDbType.VarChar).Value = typeFrais
                End If
                If sumSql.Contains("Annee=") Then sumCmd.Parameters.Add("Annee", OleDbType.VarChar).Value = annee
                Dim s = sumCmd.ExecuteScalar()
                If s IsNot Nothing AndAlso Not IsDBNull(s) Then totalPaye = Convert.ToDouble(s)
            Catch
                totalPaye = 0
            End Try

            ' expected amount
            Dim montantAttendu As Double = 0
            Try
                Dim cmdGet As New OleDbCommand("SELECT MontantAttendu FROM FraisAttendus WHERE Matricule=? AND TypeFrais=? AND Annee=?", conn)
                cmdGet.Parameters.Add("Matricule", OleDbType.VarChar).Value = matricule
                cmdGet.Parameters.Add("TypeFrais", OleDbType.VarChar).Value = typeFrais
                cmdGet.Parameters.Add("Annee", OleDbType.VarChar).Value = annee
                Dim o = cmdGet.ExecuteScalar()
                If o IsNot Nothing AndAlso Not IsDBNull(o) Then montantAttendu = Convert.ToDouble(o)
            Catch
                montantAttendu = 0
            End Try

            Dim reste As Double = montantAttendu - totalPaye

            ' Show pop-up with details
            ' Format numbers as FCFA without euro symbol
            Dim fmtPaid As String = totalPaye.ToString("N0") & " FCFA"
            Dim fmtExpected As String = montantAttendu.ToString("N0") & " FCFA"
            Dim fmtReste As String = reste.ToString("N0") & " FCFA"
            Dim msg As String = $"Montant déjà payé : {fmtPaid}{Environment.NewLine}Montant attendu : {fmtExpected}{Environment.NewLine}Reste à payer : {fmtReste}" & Environment.NewLine
            MessageBox.Show(msg, "Détails des impayés", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Using
    End Sub
End Class
