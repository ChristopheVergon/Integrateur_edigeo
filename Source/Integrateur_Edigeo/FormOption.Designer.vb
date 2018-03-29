<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Formoption
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxEdigeo = New System.Windows.Forms.TextBox()
        Me.TextBoxgroslot = New System.Windows.Forms.TextBox()
        Me.TextBoxPostgis = New System.Windows.Forms.TextBox()
        Me.TextBoxtampon = New System.Windows.Forms.TextBox()
        Me.Buttonvalide = New System.Windows.Forms.Button()
        Me.Buttonannule = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(169, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nombre total de processus Edigéo"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(193, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Nombre de processus Edigéo ""gros lot"""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 99)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(147, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Nombre de processus Postgis"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 146)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Taille du tampon"
        '
        'TextBoxEdigeo
        '
        Me.TextBoxEdigeo.Location = New System.Drawing.Point(255, 14)
        Me.TextBoxEdigeo.Name = "TextBoxEdigeo"
        Me.TextBoxEdigeo.Size = New System.Drawing.Size(47, 20)
        Me.TextBoxEdigeo.TabIndex = 4
        '
        'TextBoxgroslot
        '
        Me.TextBoxgroslot.Location = New System.Drawing.Point(255, 51)
        Me.TextBoxgroslot.Name = "TextBoxgroslot"
        Me.TextBoxgroslot.Size = New System.Drawing.Size(47, 20)
        Me.TextBoxgroslot.TabIndex = 5
        '
        'TextBoxPostgis
        '
        Me.TextBoxPostgis.Location = New System.Drawing.Point(255, 96)
        Me.TextBoxPostgis.Name = "TextBoxPostgis"
        Me.TextBoxPostgis.Size = New System.Drawing.Size(47, 20)
        Me.TextBoxPostgis.TabIndex = 6
        '
        'TextBoxtampon
        '
        Me.TextBoxtampon.Location = New System.Drawing.Point(255, 139)
        Me.TextBoxtampon.Name = "TextBoxtampon"
        Me.TextBoxtampon.Size = New System.Drawing.Size(47, 20)
        Me.TextBoxtampon.TabIndex = 7
        '
        'Buttonvalide
        '
        Me.Buttonvalide.Location = New System.Drawing.Point(364, 32)
        Me.Buttonvalide.Name = "Buttonvalide"
        Me.Buttonvalide.Size = New System.Drawing.Size(75, 23)
        Me.Buttonvalide.TabIndex = 8
        Me.Buttonvalide.Text = "Valider"
        Me.Buttonvalide.UseVisualStyleBackColor = True
        '
        'Buttonannule
        '
        Me.Buttonannule.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Buttonannule.Location = New System.Drawing.Point(364, 89)
        Me.Buttonannule.Name = "Buttonannule"
        Me.Buttonannule.Size = New System.Drawing.Size(75, 23)
        Me.Buttonannule.TabIndex = 9
        Me.Buttonannule.Text = "Annuler"
        Me.Buttonannule.UseVisualStyleBackColor = True
        '
        'Formoption
        '
        Me.AcceptButton = Me.Buttonvalide
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Buttonannule
        Me.ClientSize = New System.Drawing.Size(462, 179)
        Me.ControlBox = False
        Me.Controls.Add(Me.Buttonannule)
        Me.Controls.Add(Me.Buttonvalide)
        Me.Controls.Add(Me.TextBoxtampon)
        Me.Controls.Add(Me.TextBoxPostgis)
        Me.Controls.Add(Me.TextBoxgroslot)
        Me.Controls.Add(Me.TextBoxEdigeo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Formoption"
        Me.Text = "Options"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEdigeo As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxgroslot As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxPostgis As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxtampon As System.Windows.Forms.TextBox
    Friend WithEvents Buttonvalide As System.Windows.Forms.Button
    Friend WithEvents Buttonannule As System.Windows.Forms.Button
End Class
