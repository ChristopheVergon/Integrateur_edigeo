Public Class Echange_object


    Private mrep As String
    Public ReadOnly Property rep() As String
        Get
            Return mrep
        End Get

    End Property

    Private mcouche As System.Collections.Generic.List(Of LayerEDIGEO)
    Public ReadOnly Property Couches() As System.Collections.Generic.List(Of LayerEDIGEO)
        Get
            Return mcouche
        End Get
        
    End Property

    Private mNomLot As String
    Public ReadOnly Property NomLot() As String
        Get
            Return mNomLot
        End Get

    End Property

    Private mPoly As ObjetEDIGEO_SURF
    Public ReadOnly Property Poly() As ObjetEDIGEO_SURF
        Get
            Return mPoly
        End Get

    End Property
    Public Function FindCouche(ByVal nomcouche As String) As LayerEDIGEO
        For Each C In mcouche
            If C.LayerName = nomcouche Then
                Return C
            End If
        Next
        Return Nothing
    End Function
    Public Sub New(MaListe As System.Collections.Generic.List(Of LayerEDIGEO), noml As String, repertoire As String)
        mrep = repertoire
        mcouche = New System.Collections.Generic.List(Of LayerEDIGEO)
        mcouche = MaListe
        mNomLot = noml

        Dim la As LayerEDIGEO
        la = FindCouche("SECTION_id")

        mPoly = New ObjetEDIGEO_SURF
        mPoly = CType(la.DictionaryObj(0), ObjetEDIGEO_SURF)

    End Sub

End Class
