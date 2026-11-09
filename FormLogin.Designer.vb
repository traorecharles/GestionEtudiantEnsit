<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Label1 = New Label()
		Label2 = New Label()
		Label3 = New Label()
		txt_user = New TextBox()
		txt_password = New TextBox()
		btn_connect = New Button()
		btn_quitter = New Button()
		SuspendLayout()
		' 
		' Label1
		' 
		Label1.Location = New Point(174, 46)
		Label1.Name = "Label1"
		Label1.Size = New Size(414, 22)
		Label1.TabIndex = 0
		Label1.Text = "Authentification - Gestion Scolaire"
		Label1.TextAlign = ContentAlignment.TopCenter
		' 
		' Label2
		' 
		Label2.AutoSize = True
		Label2.Location = New Point(174, 129)
		Label2.Name = "Label2"
		Label2.Size = New Size(106, 15)
		Label2.TabIndex = 1
		Label2.Text = "Nom d'Utilisateur :"
		' 
		' Label3
		' 
		Label3.AutoSize = True
		Label3.Location = New Point(174, 204)
		Label3.Name = "Label3"
		Label3.Size = New Size(83, 15)
		Label3.TabIndex = 2
		Label3.Text = "Mot de passe :"
		' 
		' txt_user
		' 
		txt_user.Location = New Point(174, 160)
		txt_user.Name = "txt_user"
		txt_user.Size = New Size(422, 23)
		txt_user.TabIndex = 3
		' 
		' txt_password
		' 
		txt_password.Location = New Point(174, 234)
		txt_password.Name = "txt_password"
		txt_password.PasswordChar = "*"c
		txt_password.Size = New Size(422, 23)
		txt_password.TabIndex = 4
		' 
		' btn_connect
		' 
		btn_connect.Location = New Point(245, 299)
		btn_connect.Name = "btn_connect"
		btn_connect.Size = New Size(113, 47)
		btn_connect.TabIndex = 5
		btn_connect.Text = "Se connecter"
		btn_connect.UseVisualStyleBackColor = True
		' 
		' btn_quitter
		' 
		btn_quitter.Location = New Point(389, 299)
		btn_quitter.Name = "btn_quitter"
		btn_quitter.Size = New Size(111, 47)
		btn_quitter.TabIndex = 6
		btn_quitter.Text = "Quitter"
		btn_quitter.UseVisualStyleBackColor = True
		'
		' lblHints
		'
		lblHints = New Label()
		lblHints.AutoSize = False
		lblHints.Location = New Point(12, 400)
		lblHints.Name = "lblHints"
		lblHints.Size = New Size(520, 40)
		lblHints.TabIndex = 7
		lblHints.Text = "Personnel: Nom Utilisateur = test, Mot de passe = test" & vbCrLf & _
				"Administrateur: Nom Utilisateur = admin, Mot de passe = admin"
		lblHints.ForeColor = Drawing.Color.Gray
		lblHints.TextAlign = ContentAlignment.MiddleLeft
		' 
		' FormLogin
		' 
		AutoScaleDimensions = New SizeF(7F, 15F)
		AutoScaleMode = AutoScaleMode.Font
		ClientSize = New Size(800, 450)
		Controls.Add(btn_quitter)
		Controls.Add(lblHints)
		Controls.Add(btn_connect)
		Controls.Add(txt_password)
		Controls.Add(txt_user)
		Controls.Add(Label3)
		Controls.Add(Label2)
		Controls.Add(Label1)
		Name = "FormLogin"
		Text = "FormLogin"
		ResumeLayout(False)
		PerformLayout()
	End Sub

	Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txt_user As TextBox
    Friend WithEvents txt_password As TextBox
    Friend WithEvents btn_connect As Button
    Friend WithEvents btn_quitter As Button
	Friend WithEvents lblHints As Label

End Class
