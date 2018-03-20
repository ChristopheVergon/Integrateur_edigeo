Public Class ListeDescripteurReference
    Inherits System.Collections.Generic.List(Of Descripteur_Reference)

    Private mIDCherche As String

    Public Function ChercheID(ByVal id As String) As Descripteur_Reference
        mIDCherche = id
        Return Me.Find(AddressOf mChercheID)
    End Function

    Public Function ChercheTouteID(ByVal id As String) As System.Collections.Generic.List(Of Descripteur_Reference)
        mIDCherche = id
        Return Me.FindAll(AddressOf mChercheID)
    End Function

    Private Function mChercheID(ByVal des As Descripteur_Reference) As Boolean
        Return mIDCherche = des._ID
    End Function
End Class
