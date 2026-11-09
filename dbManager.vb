Imports System.Data.OleDb

Module dbManager
	Public connString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\GestionEtudiant.accdb;"
	Public CurrentUser As String = ""
	Public CurrentRole As String = ""
End Module