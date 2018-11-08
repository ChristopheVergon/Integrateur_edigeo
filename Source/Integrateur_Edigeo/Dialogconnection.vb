Imports System.Windows.Forms
Imports Npgsql
Public Class Dialogconnection

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        cnngen = New NpgsqlConnection
        mConnectionB = New NpgsqlConnectionStringBuilder

        affecteconnection()
        cnngen.ConnectionString = mConnectionB.ConnectionString
        connectionstringB = New NpgsqlConnectionStringBuilder
        connectionstringB = mConnectionB
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private mConnectionB As NpgsqlConnectionStringBuilder


    Private Sub affecteconnection()
        mConnectionB.Host = Hote.Text
        mConnectionB.Port = PortTCP.Text
        mConnectionB.UserName = Utilisateur.Text
        mConnectionB.Password = Motdepasse.Text
        mConnectionB.Database = Nombase.Text
        databasename = Nombase.Text
        mConnectionB.Database = databasename
        SchemaName = Nomschema.Text

        My.Settings.Host = Hote.Text
        My.Settings.PortTcp = PortTCP.Text
        My.Settings.Utilisateur = Utilisateur.Text
        My.Settings.Nombase = Nombase.Text
        My.Settings.NomSchema = Nomschema.Text
        mConnectionB.CommandTimeout = 1200
        mConnectionB.Timeout = 1024
        mConnectionB.ConnectionLifeTime = Int32.MaxValue
        mConnectionB.Pooling = True
        mConnectionB.MaxPoolSize = 100
    End Sub

    Private Sub Dialogconnection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim cnn As New NpgsqlConnectionStringBuilder
        Hote.Text = My.Settings.Host
        PortTCP.Text = My.Settings.PortTcp
        Utilisateur.Text = My.Settings.Utilisateur
        Nombase.Text = My.Settings.Nombase
        Nomschema.Text = My.Settings.NomSchema
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        cnngen = New NpgsqlConnection
        mConnectionB = New NpgsqlConnectionStringBuilder

        affecteconnection()
        cnngen.ConnectionString = mConnectionB.ConnectionString

        Try
            cnngen.Open()
            MsgBox("connexion réussie")
            cnngen.Close()
        Catch ex As Exception
            MsgBox("connection avortée   " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
End Class
