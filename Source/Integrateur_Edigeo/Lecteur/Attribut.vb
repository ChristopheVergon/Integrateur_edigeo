Public Class Attribut

    Private mName As String
    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal value As String)
            mName = value
        End Set
    End Property


    Private mValeur As String
    Public Property Valeur() As String
        Get
            Return mValeur
        End Get
        Set(ByVal value As String)
            mValeur = value
        End Set
    End Property

End Class
