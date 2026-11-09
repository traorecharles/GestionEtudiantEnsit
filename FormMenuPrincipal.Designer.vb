<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMenuPrincipal
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

	'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
	'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
	'Ne la modifiez pas à l'aide de l'éditeur de code.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		btn_etudiant = New Button()
		btn_reinscription = New Button()
		btn_paiement = New Button()
		btn_rapport = New Button()
		btn_user = New Button()
		Label1 = New Label()
		Button6 = New Button()
		tlMain = New TableLayoutPanel()
		headerPanel = New Panel()
		tlButtons = New FlowLayoutPanel()
		tlMain.SuspendLayout()
		headerPanel.SuspendLayout()
		tlButtons.SuspendLayout()
		SuspendLayout()
		' 
		' btn_etudiant
		' 
		btn_etudiant.Location = New Point(3, 13)
		btn_etudiant.Name = "btn_etudiant"
		btn_etudiant.Size = New Size(384, 39)
		btn_etudiant.TabIndex = 0
		btn_etudiant.Text = "Gestion des Etudiants"
		btn_etudiant.UseVisualStyleBackColor = True
		' 
		' btn_reinscription
		' 
		btn_reinscription.Location = New Point(3, 58)
		btn_reinscription.Name = "btn_reinscription"
		btn_reinscription.Size = New Size(384, 39)
		btn_reinscription.TabIndex = 1
		btn_reinscription.Text = "Gestion des Réinscriptions"
		btn_reinscription.UseVisualStyleBackColor = True
		' 
		' btn_paiement
		' 
		btn_paiement.Location = New Point(3, 103)
		btn_paiement.Name = "btn_paiement"
		btn_paiement.Size = New Size(384, 39)
		btn_paiement.TabIndex = 2
		btn_paiement.Text = "Gestion des Paiements"
		btn_paiement.UseVisualStyleBackColor = True
		' 
		' btn_rapport
		' 
		btn_rapport.Location = New Point(3, 148)
		btn_rapport.Name = "btn_rapport"
		btn_rapport.Size = New Size(384, 39)
		btn_rapport.TabIndex = 3
		btn_rapport.Text = "Rapports/Export"
		btn_rapport.UseVisualStyleBackColor = True
		' 
		' btn_user
		' 
		btn_user.Location = New Point(3, 193)
		btn_user.Name = "btn_user"
		btn_user.Size = New Size(384, 39)
		btn_user.TabIndex = 4
		btn_user.Text = "Gestion des Utilisateurs/Personnels"
		btn_user.UseVisualStyleBackColor = True
		' 
		' Label1
		' 
		Label1.AutoSize = True
		Label1.Dock = DockStyle.Left
		Label1.Location = New Point(0, 0)
		Label1.Name = "Label1"
		Label1.Padding = New Padding(10, 8, 0, 0)
		Label1.Size = New Size(51, 23)
		Label1.TabIndex = 5
		Label1.Text = "Label1"
		' 
		' Button6
		' 
		Button6.Anchor = AnchorStyles.Top Or AnchorStyles.Right
		Button6.Location = New Point(594, 0)
		Button6.Margin = New Padding(0, 8, 10, 0)
		Button6.Name = "Button6"
		Button6.Size = New Size(136, 28)
		Button6.TabIndex = 6
		Button6.Text = "Déconnexion"
		Button6.UseVisualStyleBackColor = True
		' 
		' tlMain
		' 
		tlMain.ColumnCount = 1
		tlMain.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20.0F))
		tlMain.Controls.Add(headerPanel, 0, 0)
		tlMain.Controls.Add(tlButtons, 0, 1)
		tlMain.Dock = DockStyle.Fill
		tlMain.Location = New Point(0, 0)
		tlMain.Name = "tlMain"
		tlMain.RowCount = 3
		tlMain.RowStyles.Add(New RowStyle())
		tlMain.RowStyles.Add(New RowStyle())
		tlMain.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
		tlMain.Size = New Size(800, 450)
		tlMain.TabIndex = 0
		' 
		' headerPanel
		' 
		headerPanel.Controls.Add(Label1)
		headerPanel.Controls.Add(Button6)
		headerPanel.Dock = DockStyle.Fill
		headerPanel.Location = New Point(3, 3)
		headerPanel.Name = "headerPanel"
		headerPanel.Size = New Size(794, 100)
		headerPanel.TabIndex = 0
		' 
		' tlButtons
		' 
		tlButtons.Anchor = AnchorStyles.None
		tlButtons.AutoSize = True
		tlButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink
		tlButtons.Controls.Add(btn_etudiant)
		tlButtons.Controls.Add(btn_reinscription)
		tlButtons.Controls.Add(btn_paiement)
		tlButtons.Controls.Add(btn_rapport)
		tlButtons.Controls.Add(btn_user)
		tlButtons.FlowDirection = FlowDirection.TopDown
		tlButtons.Location = New Point(205, 109)
		tlButtons.Name = "tlButtons"
		tlButtons.Padding = New Padding(0, 10, 0, 10)
		tlButtons.Size = New Size(390, 245)
		tlButtons.TabIndex = 1
		' 
		' FormMenuPrincipal
		' 
		AutoScaleDimensions = New SizeF(7.0F, 15.0F)
		AutoScaleMode = AutoScaleMode.Font
		ClientSize = New Size(800, 450)
		Controls.Add(tlMain)
		Name = "FormMenuPrincipal"
		Text = "FormMenuPrincipal"
		tlMain.ResumeLayout(False)
		tlMain.PerformLayout()
		headerPanel.ResumeLayout(False)
		headerPanel.PerformLayout()
		tlButtons.ResumeLayout(False)
		ResumeLayout(False)
	End Sub

	Friend WithEvents btn_etudiant As Button
	Friend WithEvents btn_paiement As Button
    Friend WithEvents btn_rapport As Button
    Friend WithEvents btn_reinscription As Button
    Friend WithEvents btn_user As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Button6 As Button
    Friend WithEvents tlMain As TableLayoutPanel
    Friend WithEvents headerPanel As Panel
    Friend WithEvents tlButtons As FlowLayoutPanel
End Class
