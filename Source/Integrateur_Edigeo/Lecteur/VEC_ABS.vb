Public MustInherit Class VEC_ABS
    Private mIdentificateur As String
    Public Property Identificateur() As String
        Get
            Return mIdentificateur
        End Get
        Set(ByVal value As String)
            mIdentificateur = value
        End Set
    End Property

    MustOverride Sub affecte(ByVal ZL As ListeZone, ByVal cur As Integer)


End Class
