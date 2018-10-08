

Public Class FormSRID

    Private Sub FormSRID_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ControlBox = False
        SRID3942.Checked = True
        Me.CenterToScreen()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles SRID3949.CheckedChanged

    End Sub

    Private Sub ButtonAnnuler_Click(sender As Object, e As EventArgs) Handles ButtonAnnuler.Click
        Me.Close()
    End Sub

    Private Sub buttonvalider_Click(sender As Object, e As EventArgs) Handles buttonvalider.Click
        Dim c As RadioButton

        For Each ct In GroupBox1.Controls

            If TypeOf (ct) Is RadioButton Then
                c = CType(ct, RadioButton)
                If c.Checked Then

                    SRID = CInt(c.Tag)

                End If
            End If


        Next
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub RadioButton1_CheckedChanged_1(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Dim A = DirectCast(sender, RadioButton)
        A.Tag = InputBox("Choississez la valeur du SRID voulue", "3942")
        A.Text = "SRID : " & A.Tag
    End Sub
End Class