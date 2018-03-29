Public Class Formoption

    Private Sub Formoption_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBoxEdigeo.Text = nbprocedigeo
        TextBoxgroslot.Text = nbprocedigeo_gros
        TextBoxPostgis.Text = nbprocposgis
        TextBoxtampon.Text = tailletampon

    End Sub

    Private Sub Buttonvalide_Click(sender As Object, e As EventArgs) Handles Buttonvalide.Click
        If Val(TextBoxgroslot.Text) >= Val(TextBoxEdigeo.Text) Then
            MsgBox("Le nombre de processus dédiés au gros lot doit être inférieur au nombre total", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If TextBoxEdigeo.Text = "" Then Exit Sub
        If TextBoxgroslot.Text = "" Then Exit Sub
        If TextBoxPostgis.Text = "" Then Exit Sub
        If TextBoxtampon.Text = "" Then Exit Sub


        nbprocedigeo = Val(TextBoxEdigeo.Text)
        nbprocedigeo_gros = Val(TextBoxgroslot.Text)
        nbprocposgis = Val(TextBoxPostgis.Text)
        tailletampon = Val(TextBoxtampon.Text)


        Me.Close()


    End Sub

    Private Sub Buttonannule_Click(sender As Object, e As EventArgs) Handles Buttonannule.Click
        Me.Close()
    End Sub
End Class