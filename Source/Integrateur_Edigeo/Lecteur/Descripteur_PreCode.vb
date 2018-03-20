Public Class Descripteur_PreCode

    Private mValeur As String
    Public Property Valeur() As String
        Get
            Return mValeur
        End Get
        Set(ByVal value As String)
            mValeur = value
        End Set
    End Property


    Private mDescription As String
    Public Property Description() As String
        Get
            Return mDescription
        End Get
        Set(ByVal value As String)
            mDescription = value
        End Set
    End Property

End Class
