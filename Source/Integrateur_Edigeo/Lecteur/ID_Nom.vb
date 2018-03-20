Public Class ID_Nom

    Private mIdentificateur As String
    Public Property Identificateur() As String
        Get
            Return mIdentificateur
        End Get
        Set(ByVal value As String)
            mIdentificateur = value
        End Set
    End Property


    Private mNom As String
    Public Property Nom() As String
        Get
            Return mNom
        End Get
        Set(ByVal value As String)
            mNom = value
        End Set
    End Property

End Class
