Public Class ListeZone

    Private mListeZ As System.Collections.Generic.List(Of ZoneEdigeo)
    Public ReadOnly Property ListeZ() As System.Collections.Generic.List(Of ZoneEdigeo)
        Get
            Return mListeZ
        End Get
        
    End Property


    Public Sub New()
        mListeZ = New System.Collections.Generic.List(Of ZoneEdigeo)
    End Sub

    Private mZ1cherche As String

    Public Function ChercheZ1(ByVal Z1 As String) As ZoneEdigeo
        mZ1cherche = Z1
        Return mListeZ.Find(AddressOf mChZ1)
    End Function

    Public Function ChercheToutZ1(ByVal z1 As String) As System.Collections.Generic.List(Of ZoneEdigeo)
        mZ1cherche = z1
        Return mListeZ.FindAll(AddressOf mChZ1)
    End Function

    Private Function mChZ1(ByVal Z As ZoneEdigeo) As Boolean
        Return Z.Z1 = mZ1cherche
    End Function

    Private mZ2cherche As String

    Public Function ChercheZ2(ByVal Z2 As String) As ZoneEdigeo
        mZ2cherche = Z2
        Return mListeZ.Find(AddressOf mChZ2)
    End Function
    Public Function ChercheToutZ2(ByVal z2 As String) As System.Collections.Generic.List(Of ZoneEdigeo)
        mZ2cherche = z2
        Return mListeZ.FindAll(AddressOf mChZ2)
    End Function

    Private Function mChZ2(ByVal Z As ZoneEdigeo) As Boolean
        Return Z.Z2 = mZ2cherche
    End Function

    Private mZ3cherche As String

    Public Function ChercheZ3(ByVal Z3 As String) As ZoneEdigeo
        mZ3cherche = Z3
        Return mListeZ.Find(AddressOf mChZ3)
    End Function
    Public Function ChercheToutZ3(ByVal z3 As String) As System.Collections.Generic.List(Of ZoneEdigeo)
        mZ3cherche = z3
        Return mListeZ.FindAll(AddressOf mChZ3)
    End Function
    Private Function mChZ3(ByVal Z As ZoneEdigeo) As Boolean
        Return Z.Z3 = mZ3cherche
    End Function

    Private mZ4cherche As String

    Public Function ChercheZ4(ByVal Z4 As String) As ZoneEdigeo
        mZ4cherche = Z4
        Return mListeZ.Find(AddressOf mChZ4)
    End Function
    Public Function ChercheToutZ4(ByVal z4 As String) As System.Collections.Generic.List(Of ZoneEdigeo)
        mZ4cherche = z4
        Return mListeZ.FindAll(AddressOf mChZ4)
    End Function

    Private Function mChZ4(ByVal Z As ZoneEdigeo) As Boolean
        Return Z.Z4 = mZ4cherche
    End Function

    Private mZ6cherche As String

    Public Function ChercheZ6(ByVal Z6 As String) As ZoneEdigeo
        mZ6cherche = Z6
        Return mListeZ.Find(AddressOf mChZ6)
    End Function
    Public Function ChercheToutZ6(ByVal z6 As String) As System.Collections.Generic.List(Of ZoneEdigeo)
        mZ6cherche = z6
        Return mListeZ.FindAll(AddressOf mChZ6)
    End Function

    Private Function mChZ6(ByVal Z As ZoneEdigeo) As Boolean
        Return Z.Z6 = mZ6cherche
    End Function
End Class
