<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormEtudiant
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
		Label1 = New Label()
		Label2 = New Label()
		Label3 = New Label()
		Label4 = New Label()
		Label5 = New Label()
		Label6 = New Label()
		TextBox1 = New TextBox()
		TextBox2 = New TextBox()
		TextBox3 = New TextBox()
		TextBox5 = New TextBox()
		ComboBox1 = New ComboBox()
		Label7 = New Label()
		Label8 = New Label()
		Label9 = New Label()
		TextBox6 = New TextBox()
		TextBox7 = New TextBox()
		TextBox8 = New TextBox()
		DateTimePicker1 = New DateTimePicker()
		DataGridView1 = New DataGridView()
		Button1 = New Button()
		Button2 = New Button()
		Button3 = New Button()
		PictureBox1 = New PictureBox()
		Button4 = New Button()
		Label10 = New Label()
		ComboBox2 = New ComboBox()
		CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
		CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
		SuspendLayout()
		' 
		' Label1
		' 
		Label1.AutoSize = True
		Label1.Location = New Point(18, 9)
		Label1.Name = "Label1"
		Label1.Size = New Size(66, 15)
		Label1.TabIndex = 0
		Label1.Text = "Matricule : "
		' 
		' Label2
		' 
		Label2.AutoSize = True
		Label2.Location = New Point(18, 53)
		Label2.Name = "Label2"
		Label2.Size = New Size(40, 15)
		Label2.TabIndex = 1
		Label2.Text = "Nom :"
		' 
		' Label3
		' 
		Label3.AutoSize = True
		Label3.Location = New Point(18, 94)
		Label3.Name = "Label3"
		Label3.Size = New Size(63, 15)
		Label3.TabIndex = 2
		Label3.Text = "Prénoms : "
		' 
		' Label4
		' 
		Label4.AutoSize = True
		Label4.Location = New Point(18, 135)
		Label4.Name = "Label4"
		Label4.Size = New Size(107, 15)
		Label4.TabIndex = 3
		Label4.Text = "Date de naissance :"
		' 
		' Label5
		' 
		Label5.AutoSize = True
		Label5.Location = New Point(18, 179)
		Label5.Name = "Label5"
		Label5.Size = New Size(105, 15)
		Label5.TabIndex = 4
		Label5.Text = "Lieu de naissance :"
		' 
		' Label6
		' 
		Label6.AutoSize = True
		Label6.Location = New Point(18, 225)
		Label6.Name = "Label6"
		Label6.Size = New Size(36, 15)
		Label6.TabIndex = 5
		Label6.Text = "Sexe :"
		' 
		' TextBox1
		' 
		TextBox1.Location = New Point(18, 27)
		TextBox1.Name = "TextBox1"
		TextBox1.Size = New Size(228, 23)
		TextBox1.TabIndex = 6
		' 
		' TextBox2
		' 
		TextBox2.Location = New Point(18, 68)
		TextBox2.Name = "TextBox2"
		TextBox2.Size = New Size(228, 23)
		TextBox2.TabIndex = 7
		' 
		' TextBox3
		' 
		TextBox3.Location = New Point(18, 109)
		TextBox3.Name = "TextBox3"
		TextBox3.Size = New Size(228, 23)
		TextBox3.TabIndex = 8
		' 
		' TextBox5
		' 
		TextBox5.Location = New Point(18, 197)
		TextBox5.Name = "TextBox5"
		TextBox5.Size = New Size(228, 23)
		TextBox5.TabIndex = 10
		' 
		' ComboBox1
		' 
		ComboBox1.FormattingEnabled = True
		ComboBox1.Location = New Point(18, 243)
		ComboBox1.Name = "ComboBox1"
		ComboBox1.Size = New Size(121, 23)
		ComboBox1.TabIndex = 11
		' 
		' Label7
		' 
		Label7.AutoSize = True
		Label7.Location = New Point(300, 9)
		Label7.Name = "Label7"
		Label7.Size = New Size(31, 15)
		Label7.TabIndex = 12
		Label7.Text = "Tel : "
		' 
		' Label8
		' 
		Label8.AutoSize = True
		Label8.Location = New Point(300, 53)
		Label8.Name = "Label8"
		Label8.Size = New Size(45, 15)
		Label8.TabIndex = 13
		Label8.Text = "Email : "
		' 
		' Label9
		' 
		Label9.AutoSize = True
		Label9.Location = New Point(300, 94)
		Label9.Name = "Label9"
		Label9.Size = New Size(57, 15)
		Label9.TabIndex = 14
		Label9.Text = "Adresse : "
		' 
		' TextBox6
		' 
		TextBox6.Location = New Point(300, 27)
		TextBox6.Name = "TextBox6"
		TextBox6.Size = New Size(229, 23)
		TextBox6.TabIndex = 15
		' 
		' TextBox7
		' 
		TextBox7.Location = New Point(300, 68)
		TextBox7.Name = "TextBox7"
		TextBox7.Size = New Size(229, 23)
		TextBox7.TabIndex = 16
		' 
		' TextBox8
		' 
		TextBox8.Location = New Point(300, 112)
		TextBox8.Name = "TextBox8"
		TextBox8.Size = New Size(229, 23)
		TextBox8.TabIndex = 17
		' 
		' DateTimePicker1
		' 
		DateTimePicker1.Location = New Point(18, 153)
		DateTimePicker1.Name = "DateTimePicker1"
		DateTimePicker1.Size = New Size(181, 23)
		DateTimePicker1.TabIndex = 18
		' 
		' DataGridView1
		' 
		DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
		DataGridView1.Location = New Point(3, 331)
		DataGridView1.Name = "DataGridView1"
		DataGridView1.Size = New Size(796, 175)
		DataGridView1.TabIndex = 19
		' 
		' Button1
		' 
		Button1.Location = New Point(474, 279)
		Button1.Name = "Button1"
		Button1.Size = New Size(105, 46)
		Button1.TabIndex = 20
		Button1.Text = "Ajouter"
		Button1.UseVisualStyleBackColor = True
		' 
		' Button2
		' 
		Button2.Location = New Point(585, 279)
		Button2.Name = "Button2"
		Button2.Size = New Size(100, 46)
		Button2.TabIndex = 21
		Button2.Text = "Modifier"
		Button2.UseVisualStyleBackColor = True
		' 
		' Button3
		' 
		Button3.Location = New Point(691, 279)
		Button3.Name = "Button3"
		Button3.Size = New Size(97, 46)
		Button3.TabIndex = 22
		Button3.Text = "Supprimer"
		Button3.UseVisualStyleBackColor = True
		' 
		' PictureBox1
		' 
		PictureBox1.BackColor = SystemColors.ControlDark
		PictureBox1.Location = New Point(564, 27)
		PictureBox1.Name = "PictureBox1"
		PictureBox1.Size = New Size(167, 149)
		PictureBox1.TabIndex = 23
		PictureBox1.TabStop = False
		' 
		' Button4
		' 
		Button4.Location = New Point(602, 197)
		Button4.Name = "Button4"
		Button4.Size = New Size(97, 31)
		Button4.TabIndex = 24
		Button4.Text = "Choisir Photo"
		Button4.UseVisualStyleBackColor = True
		' 
		' Label10
		' 
		Label10.AutoSize = True
		Label10.Location = New Point(300, 138)
		Label10.Name = "Label10"
		Label10.Size = New Size(50, 15)
		Label10.TabIndex = 25
		Label10.Text = "Niveau :"
		' 
		' ComboBox2
		' 
		ComboBox2.FormattingEnabled = True
		ComboBox2.Location = New Point(300, 156)
		ComboBox2.Name = "ComboBox2"
		ComboBox2.Size = New Size(129, 23)
		ComboBox2.TabIndex = 26
		' 
		' FormEtudiant
		' 
		AutoScaleDimensions = New SizeF(7F, 15F)
		AutoScaleMode = AutoScaleMode.Font
		ClientSize = New Size(800, 509)
		Controls.Add(ComboBox2)
		Controls.Add(Label10)
		Controls.Add(Button4)
		Controls.Add(PictureBox1)
		Controls.Add(Button3)
		Controls.Add(Button2)
		Controls.Add(Button1)
		Controls.Add(DataGridView1)
		Controls.Add(DateTimePicker1)
		Controls.Add(TextBox8)
		Controls.Add(TextBox7)
		Controls.Add(TextBox6)
		Controls.Add(Label9)
		Controls.Add(Label8)
		Controls.Add(Label7)
		Controls.Add(ComboBox1)
		Controls.Add(TextBox5)
		Controls.Add(TextBox3)
		Controls.Add(TextBox2)
		Controls.Add(TextBox1)
		Controls.Add(Label6)
		Controls.Add(Label5)
		Controls.Add(Label4)
		Controls.Add(Label3)
		Controls.Add(Label2)
		Controls.Add(Label1)
		Name = "FormEtudiant"
		Text = "FormEtudiant"
		CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
		CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
		ResumeLayout(False)
		PerformLayout()
	End Sub

	Friend WithEvents Label1 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents Label3 As Label
	Friend WithEvents Label4 As Label
	Friend WithEvents Label5 As Label
	Friend WithEvents Label6 As Label
	Friend WithEvents TextBox1 As TextBox
	Friend WithEvents TextBox2 As TextBox
	Friend WithEvents TextBox3 As TextBox
	Friend WithEvents TextBox5 As TextBox
	Friend WithEvents ComboBox1 As ComboBox
	Friend WithEvents Label7 As Label
	Friend WithEvents Label8 As Label
	Friend WithEvents Label9 As Label
	Friend WithEvents TextBox6 As TextBox
	Friend WithEvents TextBox7 As TextBox
	Friend WithEvents TextBox8 As TextBox
	Friend WithEvents DateTimePicker1 As DateTimePicker
	Friend WithEvents DataGridView1 As DataGridView
	Friend WithEvents Button1 As Button
	Friend WithEvents Button2 As Button
	Friend WithEvents Button3 As Button
	Friend WithEvents PictureBox1 As PictureBox
	Friend WithEvents Button4 As Button
	Friend WithEvents Label10 As Label
	Friend WithEvents ComboBox2 As ComboBox
End Class
