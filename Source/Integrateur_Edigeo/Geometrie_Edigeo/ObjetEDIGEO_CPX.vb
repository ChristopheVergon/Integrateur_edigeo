Public Class ObjetEDIGEO_CPX
    Inherits ObjetEDIGEO


    Private mListeOBJ As System.Collections.Generic.List(Of ObjetEDIGEO)
    Public Property ListeOBJ() As System.Collections.Generic.List(Of ObjetEDIGEO)
        Get
            Return mListeOBJ
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of ObjetEDIGEO))
            mListeOBJ = value
        End Set
    End Property

    Public Sub New()
        MyBase.Nature = NatureObjetSCD.CPX
    End Sub

End Class
