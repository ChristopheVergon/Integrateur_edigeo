Public Class Lecteur


    Private mTHF As LotEDIGEO
    Public ReadOnly Property THF() As LotEDIGEO
        Get
            Return mTHF
        End Get
        
    End Property


    Public Sub New(ByVal NomFichier As String, f As FormIntegration)

        mTHF = New LotEDIGEO(NomFichier, f)
    End Sub


End Class
