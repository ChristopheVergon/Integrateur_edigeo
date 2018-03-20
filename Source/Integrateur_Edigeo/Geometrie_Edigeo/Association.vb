Public Class Association

    Private mRefSCD As Descripteur_Reference
    Public Property RefSCD() As Descripteur_Reference
        Get
            Return mRefSCD
        End Get
        Set(ByVal value As Descripteur_Reference)
            mRefSCD = value
        End Set
    End Property


    Private mListeObj As System.Collections.Generic.List(Of ObjetEDIGEO)
    Public Property ListeObj() As System.Collections.Generic.List(Of ObjetEDIGEO)
        Get
            Return mListeObj
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of ObjetEDIGEO))
            mListeObj = value
        End Set
    End Property

    Public Sub New()
        mListeObj = New System.Collections.Generic.List(Of ObjetEDIGEO)

    End Sub
End Class
