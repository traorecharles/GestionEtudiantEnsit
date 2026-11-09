Imports System.Data.OleDb
Imports System.Windows.Forms

Public Class FormUsers
    Inherits Form

    Private lblUsername As Label
    Private txtUsername As TextBox
    Private lblPassword As Label
    Private txtPassword As TextBox
    Private lblRole As Label
    Private comboRole As ComboBox
    Private WithEvents btnAddUser As Button
    Private WithEvents btnDeleteUser As Button
    Private DataGridViewUsers As DataGridView

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.lblUsername = New Label()
        Me.txtUsername = New TextBox()
        Me.lblPassword = New Label()
        Me.txtPassword = New TextBox()
        Me.lblRole = New Label()
        Me.comboRole = New ComboBox()
        Me.btnAddUser = New Button()
        Me.DataGridViewUsers = New DataGridView()
        CType(Me.DataGridViewUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Location = New Drawing.Point(12, 15)
        Me.lblUsername.Text = "Username :"
        '
        'txtUsername
        '
        Me.txtUsername.Location = New Drawing.Point(12, 35)
        Me.txtUsername.Size = New Drawing.Size(180, 23)
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New Drawing.Point(200, 15)
        Me.lblPassword.Text = "Password :"
        Me.txtPassword.Location = New Drawing.Point(200, 35)
        Me.txtPassword.Size = New Drawing.Size(160, 23)
        Me.txtPassword.PasswordChar = "*"c
        '
        'lblRole
        '
        Me.lblRole.AutoSize = True
        Me.lblRole.Location = New Drawing.Point(380, 15)
        Me.lblRole.Text = "Role :"
        Me.comboRole.Location = New Drawing.Point(380, 35)
        Me.comboRole.Size = New Drawing.Size(140, 23)
        Me.comboRole.DropDownStyle = ComboBoxStyle.DropDownList
        Me.comboRole.Items.AddRange(New Object() {"Administrateur", "Personnel"})
        Me.comboRole.SelectedIndex = 1
        '
        'btnAddUser
        '
        Me.btnAddUser.Location = New Drawing.Point(540, 33)
        Me.btnAddUser.Size = New Drawing.Size(90, 26)
        Me.btnAddUser.Text = "Ajouter"
        '
        'btnDeleteUser
        '
        Me.btnDeleteUser = New Button()
        Me.btnDeleteUser.Location = New Drawing.Point(640, 33)
        Me.btnDeleteUser.Size = New Drawing.Size(90, 26)
        Me.btnDeleteUser.Text = "Supprimer"
        '
        'DataGridViewUsers
        '
        Me.DataGridViewUsers.Location = New Drawing.Point(12, 75)
        Me.DataGridViewUsers.Size = New Drawing.Size(760, 370)
        Me.DataGridViewUsers.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.DataGridViewUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewUsers.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DataGridViewUsers.ReadOnly = True
        Me.DataGridViewUsers.AllowUserToAddRows = False
        Me.DataGridViewUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        '
        'FormUsers
        '
        Me.ClientSize = New Drawing.Size(784, 461)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.lblRole)
        Me.Controls.Add(Me.comboRole)
        Me.Controls.Add(Me.btnAddUser)
        Me.Controls.Add(Me.btnDeleteUser)
        Me.Controls.Add(Me.DataGridViewUsers)
        Me.Name = "FormUsers"
        Me.Text = "Gestion des Utilisateurs/Personnels"
        CType(Me.DataGridViewUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Private Sub FormUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ChargerUsers()
    End Sub

    Private Sub ChargerUsers()
        Try
            Using conn As New OleDbConnection(connString)
                conn.Open()
                Dim sql As String = "SELECT * FROM Utilisateurs"
                Dim adapter As New OleDbDataAdapter(sql, conn)
                Dim table As New DataTable()
                adapter.Fill(table)
                DataGridViewUsers.DataSource = table
                ' Hide IdUser column in the grid for UX
                If DataGridViewUsers.Columns.Contains("IdUser") Then
                    DataGridViewUsers.Columns("IdUser").Visible = False
                End If
            End Using
        Catch ex As Exception
            Dim empty As New DataTable()
            empty.Columns.Add("IdUser")
            empty.Columns.Add("Username")
            empty.Columns.Add("Password")
            empty.Columns.Add("Role")
            DataGridViewUsers.DataSource = empty
            If DataGridViewUsers.Columns.Contains("IdUser") Then
                DataGridViewUsers.Columns("IdUser").Visible = False
            End If
        End Try
    End Sub

    Private Sub btnDeleteUser_Click(sender As Object, e As EventArgs) Handles btnDeleteUser.Click
        If DataGridViewUsers.CurrentRow Is Nothing Then
            MessageBox.Show("Veuillez sélectionner un utilisateur à supprimer.", "Aucune sélection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim idObj = DataGridViewUsers.CurrentRow.Cells("IdUser").Value
        If idObj Is Nothing OrElse IsDBNull(idObj) Then
            MessageBox.Show("Identifiant utilisateur invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim usernameObj = DataGridViewUsers.CurrentRow.Cells("Username").Value
        Dim username As String = If(usernameObj Is Nothing OrElse IsDBNull(usernameObj), String.Empty, usernameObj.ToString())

        If Not String.IsNullOrEmpty(CurrentUser) AndAlso String.Equals(CurrentUser, username, StringComparison.OrdinalIgnoreCase) Then
            MessageBox.Show("Vous ne pouvez pas supprimer l'utilisateur actuellement connecté.", "Action interdite", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim id As Integer
        Try
            id = CInt(idObj)
        Catch ex As Exception
            MessageBox.Show("Impossible de récupérer l'identifiant utilisateur.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        Dim confirmation = MessageBox.Show($"Voulez-vous vraiment supprimer l'utilisateur '{username}' (ID: {id}) ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.No Then Return

        Using conn As New OleDbConnection(connString)
            conn.Open()
            Dim sql As String = "DELETE FROM Utilisateurs WHERE IdUser=?"
            Dim cmd As New OleDbCommand(sql, conn)
            cmd.Parameters.Add("IdUser", OleDbType.Integer).Value = id
            Dim rows As Integer = cmd.ExecuteNonQuery()
            If rows > 0 Then
                MessageBox.Show("Utilisateur supprimé.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Aucun utilisateur supprimé. Vérifiez l'ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using

        ChargerUsers()
    End Sub

    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text
        Dim role As String = Convert.ToString(comboRole.SelectedItem)

        If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Veuillez renseigner username et password.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using conn As New OleDbConnection(connString)
            conn.Open()
            Try
                Dim checkCmd As New OleDbCommand("SELECT COUNT(*) FROM Utilisateurs", conn)
                checkCmd.ExecuteScalar()
            Catch ex As Exception
                Dim createSql As String = "CREATE TABLE Utilisateurs (IdUser COUNTER PRIMARY KEY, Username TEXT(50), [Password] TEXT(255), [Role] TEXT(50))"
                Dim createCmd As New OleDbCommand(createSql, conn)
                createCmd.ExecuteNonQuery()
            End Try

            Dim insertSql As String = "INSERT INTO Utilisateurs (Username, [Password], [Role]) VALUES (?, ?, ?)"
            Dim insertCmd As New OleDbCommand(insertSql, conn)
            insertCmd.Parameters.Add("Username", OleDbType.VarChar).Value = username
            insertCmd.Parameters.Add("Password", OleDbType.VarChar).Value = password
            insertCmd.Parameters.Add("Role", OleDbType.VarChar).Value = role

            Dim rowsAffected As Integer = insertCmd.ExecuteNonQuery()
            If rowsAffected > 0 Then
                MessageBox.Show("Utilisateur ajouté.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtUsername.Clear()
                txtPassword.Clear()
            Else
                MessageBox.Show("Échec de l'ajout.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Using

        ChargerUsers()
    End Sub
End Class
