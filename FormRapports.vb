Imports System.Data.OleDb
Imports System.Windows.Forms
Imports System.IO

Public Class FormRapports
    Inherits Form

    Private comboSource As ComboBox
    Private comboAnnee As ComboBox
    Private comboLevel As ComboBox
    Private comboPaymentStatus As ComboBox
    Private WithEvents btnFilter As Button
    Private WithEvents btnExport As Button
    Private DataGridViewReport As DataGridView

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.comboSource = New ComboBox()
        Me.comboAnnee = New ComboBox()
        Me.comboLevel = New ComboBox()
        Me.btnFilter = New Button()
        Me.btnExport = New Button()
        Me.comboPaymentStatus = New ComboBox()
        Me.DataGridViewReport = New DataGridView()
        CType(Me.DataGridViewReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'comboSource
        '
        Me.comboSource.DropDownStyle = ComboBoxStyle.DropDownList
        Me.comboSource.Location = New Drawing.Point(12, 12)
        Me.comboSource.Size = New Drawing.Size(160, 23)
        Me.comboSource.Items.Add("Réinscriptions")
        Me.comboSource.Items.Add("Paiements")
        Me.comboSource.Items.Add("Etudiants")
        Me.comboSource.SelectedIndex = 0
        AddHandler Me.comboSource.SelectedIndexChanged, AddressOf Me.comboSource_SelectedIndexChanged
        '
        'comboAnnee
        '
        Me.comboAnnee.DropDownStyle = ComboBoxStyle.DropDownList
        Me.comboAnnee.Location = New Drawing.Point(180, 12)
        Me.comboAnnee.Size = New Drawing.Size(120, 23)
        AddHandler Me.comboAnnee.SelectedIndexChanged, AddressOf Me.btnFilter_Click

        ' comboLevel
        Me.comboLevel.DropDownStyle = ComboBoxStyle.DropDownList
        Me.comboLevel.Location = New Drawing.Point(310, 12)
        Me.comboLevel.Size = New Drawing.Size(140, 23)
        AddHandler Me.comboLevel.SelectedIndexChanged, AddressOf Me.btnFilter_Click
        Me.comboLevel.Items.Add("Tous les niveaux")
        Me.comboLevel.Items.AddRange(New Object(){"PREPA 1", "PREPA 2", "ING 1", "ING 2", "ING 3"})
        Me.comboLevel.SelectedIndex = 0
        '
        'btnFilter
        '
        Me.btnFilter.Location = New Drawing.Point(610, 10)
        Me.btnFilter.Size = New Drawing.Size(80, 26)
        Me.btnFilter.Text = "Filtrer"
        '
        'btnExport
        '
        Me.btnExport.Location = New Drawing.Point(700, 10)
        Me.btnExport.Size = New Drawing.Size(70, 26)
        Me.btnExport.Text = "Exporter CSV"

        ' comboPaymentStatus (for Paiements)
        Me.comboPaymentStatus.DropDownStyle = ComboBoxStyle.DropDownList
        Me.comboPaymentStatus.Location = New Drawing.Point(460, 12)
        Me.comboPaymentStatus.Size = New Drawing.Size(140, 23)
        Me.comboPaymentStatus.Items.AddRange(New Object(){"Tous", "Reçus", "En attente"})
        Me.comboPaymentStatus.SelectedIndex = 0
        AddHandler Me.comboPaymentStatus.SelectedIndexChanged, AddressOf Me.btnFilter_Click
        '
        'DataGridViewReport
        '
        Me.DataGridViewReport.Location = New Drawing.Point(12, 50)
        Me.DataGridViewReport.Size = New Drawing.Size(760, 400)
        Me.DataGridViewReport.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.DataGridViewReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewReport.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DataGridViewReport.ReadOnly = True
        Me.DataGridViewReport.AllowUserToAddRows = False
        Me.DataGridViewReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        '
        'FormRapports
        '
        Me.ClientSize = New Drawing.Size(784, 461)
        Me.Controls.Add(Me.comboSource)
        Me.Controls.Add(Me.comboAnnee)
        Me.Controls.Add(Me.comboLevel)
        Me.Controls.Add(Me.comboPaymentStatus)
        Me.Controls.Add(Me.btnFilter)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.DataGridViewReport)
        Me.Name = "FormRapports"
        Me.Text = "Rapports / Export"
        CType(Me.DataGridViewReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Private Sub FormRapports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim currentYear As Integer = DateTime.Now.Year
        Me.comboAnnee.Items.Clear()
        Me.comboAnnee.Items.Add(String.Format("{0}-{1}", currentYear, currentYear + 1))
        Me.comboAnnee.Items.Add(String.Format("{0}-{1}", currentYear - 1, currentYear))
        Me.comboAnnee.Items.Add(String.Format("{0}-{1}", currentYear + 1, currentYear + 2))
        Me.comboAnnee.SelectedIndex = 0

        ' ensure payment status and level are in sensible defaults
        If comboPaymentStatus.Items.Count = 0 Then
            comboPaymentStatus.Items.AddRange(New Object(){"Tous", "Reçus", "En attente"})
            comboPaymentStatus.SelectedIndex = 0
        End If
        If comboLevel.Items.Count = 0 Then
            comboLevel.Items.Add("Tous les niveaux")
            comboLevel.Items.AddRange(New Object(){"PREPA 1", "PREPA 2", "ING 1", "ING 2", "ING 3"})
            comboLevel.SelectedIndex = 0
        End If

        LoadReport()
    End Sub

    Private Sub LoadReport()
        Dim source As String = Convert.ToString(comboSource.SelectedItem)
        Try
            Using conn As New OleDbConnection(connString)
                conn.Open()
                Dim sql As String
                If source = "Paiements" Then
                    sql = "SELECT * FROM Paiements"
                ElseIf source = "Réinscriptions" Then
                    sql = "SELECT * FROM Reinscriptions"
                ElseIf source = "Etudiants" Then
                    sql = "SELECT * FROM Etudiants"
                Else
                    sql = "SELECT * FROM Reinscriptions"
                End If
                Dim adapter As New OleDbDataAdapter(sql, conn)
                Dim table As New DataTable()
                adapter.Fill(table)
                DataGridViewReport.DataSource = table
                ' Hide common id columns for UX
                HideIdColumns()
            End Using
        Catch ex As Exception
            DataGridViewReport.DataSource = New DataTable()
            HideIdColumns()
        End Try
    End Sub

    Private Sub comboSource_SelectedIndexChanged(sender As Object, e As EventArgs)
        ' Reload the report when the selected source changes
        LoadReport()
    End Sub

    Private Sub HideIdColumns()
        Try
            If DataGridViewReport.Columns Is Nothing Then Return
            Dim idCols = New String() {"IdReinscription", "IdPaiement", "IdUser", "IdEtudiant"}
            For Each c In idCols
                If DataGridViewReport.Columns.Contains(c) Then
                    DataGridViewReport.Columns(c).Visible = False
                End If
            Next
            ' Also hide password/hash related columns if present
            If DataGridViewReport.Columns.Contains("PasswordHash") Then
                DataGridViewReport.Columns("PasswordHash").Visible = False
            End If
            If DataGridViewReport.Columns.Contains("PasswordSalt") Then
                DataGridViewReport.Columns("PasswordSalt").Visible = False
            End If
        Catch
            ' ignore
        End Try
    End Sub

    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        ' Simple filtre basé sur l'année text contenue dans la colonne Annee (ou Date)
        Dim source As String = Convert.ToString(comboSource.SelectedItem)
        Dim year As String = Convert.ToString(comboAnnee.SelectedItem)
        If DataGridViewReport.DataSource Is Nothing Then LoadReport()
        Try
            ' DataSource may be a DataTable or a DataView (or BindingSource). Normalize to DataTable for schema checks.
            Dim table As DataTable = Nothing
            Dim src = DataGridViewReport.DataSource
            If TypeOf src Is DataTable Then
                table = CType(src, DataTable)
            ElseIf TypeOf src Is DataView Then
                table = CType(src, DataView).ToTable()
            ElseIf TypeOf src Is BindingSource Then
                Dim bs = CType(src, BindingSource)
                If TypeOf bs.DataSource Is DataTable Then
                    table = CType(bs.DataSource, DataTable)
                ElseIf TypeOf bs.DataSource Is DataView Then
                    table = CType(bs.DataSource, DataView).ToTable()
                End If
            Else
                ' fallback: try to build a DataTable from current rows
                Dim dt As New DataTable()
                Try
                    For Each col As DataGridViewColumn In DataGridViewReport.Columns
                        dt.Columns.Add(col.Name)
                    Next
                    For Each r As DataGridViewRow In DataGridViewReport.Rows
                        Dim dr = dt.NewRow()
                        For i As Integer = 0 To Math.Min(r.Cells.Count - 1, dt.Columns.Count - 1)
                            dr(i) = If(r.Cells(i).Value Is Nothing, DBNull.Value, r.Cells(i).Value)
                        Next
                        dt.Rows.Add(dr)
                    Next
                Catch
                End Try
                table = dt
            End If
            If source = "Etudiants" Then
                ' filter by level if requested
                If comboLevel.SelectedIndex > 0 AndAlso table.Columns.Contains("Niveau") Then
                    Dim lvl = comboLevel.SelectedItem.ToString()
                    Dim dv As New DataView(table)
                    dv.RowFilter = $"Niveau = '{lvl}'"
                    DataGridViewReport.DataSource = dv
                Else
                    ' fallback: keep table
                    DataGridViewReport.DataSource = table
                End If
            ElseIf source = "Paiements" Then
                ' if En attente -> show aggregated pending list
                Dim status = comboPaymentStatus.SelectedItem.ToString()
                Using conn As New OleDbConnection(connString)
                    conn.Open()
                    If status = "En attente" Then
                        Dim sql As String = "SELECT f.Matricule, e.Nom, e.Prenom, f.TypeFrais, f.Annee, f.MontantAttendu, Nz(SUM(p.Montant),0) AS TotalPaye, (f.MontantAttendu - Nz(SUM(p.Montant),0)) AS Solde FROM (FraisAttendus AS f LEFT JOIN Paiements AS p ON (p.Matricule=f.Matricule AND p.TypeFrais=f.TypeFrais AND p.Annee=f.Annee)) LEFT JOIN Etudiants AS e ON e.Matricule=f.Matricule GROUP BY f.Matricule, e.Nom, e.Prenom, f.TypeFrais, f.Annee, f.MontantAttendu HAVING f.MontantAttendu > Nz(SUM(p.Montant),0)"
                        Dim adapter As New OleDbDataAdapter(sql, conn)
                        Dim table2 As New DataTable()
                        adapter.Fill(table2)
                        DataGridViewReport.DataSource = table2
                    ElseIf status = "Reçus" Then
                        ' show payments (already loaded)
                        DataGridViewReport.DataSource = table
                    Else
                        DataGridViewReport.DataSource = table
                    End If
                End Using
            Else
                ' Reinscriptions or others: filter by year if possible
                If table.Columns.Contains("Annee") Then
                    Dim dv As New DataView(table)
                    dv.RowFilter = $"Annee = '{year}'"
                    DataGridViewReport.DataSource = dv
                ElseIf table.Columns.Contains("DateReinscription") Or table.Columns.Contains("DateReins") Then
                    Dim yearNum As Integer = DateTime.Now.Year
                    If Not String.IsNullOrEmpty(year) Then
                        Dim parts = year.Split("-"c)
                        Integer.TryParse(parts(0), yearNum)
                    End If
                    Dim dv As New DataView(table)
                    If table.Columns.Contains("DateReinscription") Then
                        dv.RowFilter = $"Year(DateReinscription) = {yearNum}"
                    Else
                        dv.RowFilter = $"Year(DateReins) = {yearNum}"
                    End If
                    DataGridViewReport.DataSource = dv
                End If
            End If
            HideIdColumns()
        Catch ex As Exception
            MessageBox.Show("Erreur lors du filtrage : " & ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        If DataGridViewReport.DataSource Is Nothing Then
            MessageBox.Show("Aucune donnée à exporter.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "CSV files (*.csv)|*.csv|Excel (CSV) (*.xlsx)|*.xlsx"
        sfd.FileName = "report.csv"
        If sfd.ShowDialog() <> DialogResult.OK Then Return

        Try
            Dim table As DataTable = CType(DataGridViewReport.DataSource, DataTable)
            ' Export as CSV regardless of extension (Excel can open CSV). Use semicolon as separator.
            Using sw As New StreamWriter(sfd.FileName, False, System.Text.Encoding.UTF8)
                ' header
                Dim headerCols As New List(Of String)
                For Each c As DataColumn In table.Columns
                    headerCols.Add(c.ColumnName)
                Next
                sw.WriteLine(String.Join(";", headerCols))

                For Each row As DataRow In table.Rows
                    Dim fields As New List(Of String)
                    For Each c As DataColumn In table.Columns
                        Dim v As String = ""
                        If Not row.IsNull(c) Then v = row(c).ToString()
                        ' escape semicolon and quotes using Chr(34) for " character
                        If v.Contains(";") Or v.Contains(Chr(34)) Then
                            v = Chr(34) & v.Replace(Chr(34), Chr(34) & Chr(34)) & Chr(34)
                        End If
                        fields.Add(v)
                    Next
                    sw.WriteLine(String.Join(";", fields))
                Next
            End Using
            MessageBox.Show("Export terminé.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Erreur d'export : " & ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
