Public Class ObjetEDIGEO
    'celle du fichier vecteur
    Private mID_Objet As String
    Public Property ID_Objet() As String
        Get
            Return mID_Objet
        End Get
        Set(ByVal value As String)
            mID_Objet = value
        End Set
    End Property


    'Nom générique de l'objet : parcelle, lieudit etc ...
    Private mNameSCD As String
    Public Property NameSCD() As String
        Get
            Return mNameSCD
        End Get
        Set(ByVal value As String)
            mNameSCD = value
        End Set
    End Property
    

    Private mNomLot As String
    Public Property NomLot() As String
        Get
            Return mNomLot
        End Get
        Set(ByVal value As String)
            mNomLot = value
        End Set
    End Property

    Private mNature As NatureObjetSCD
    Public Property Nature() As NatureObjetSCD
        Get
            Return mNature
        End Get
        Set(ByVal value As NatureObjetSCD)
            mNature = value
        End Set
    End Property


    Private mListeAttribut As System.Collections.Generic.List(Of Attribut)
    Public Property ListeAttribut() As System.Collections.Generic.List(Of Attribut)
        Get
            Return mListeAttribut
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of Attribut))
            mListeAttribut = value
        End Set
    End Property

    'Attribut de VEC_OBJ à l'origine
    Private mNbAttribut As Integer
    Public Property NbAttribut() As Integer
        Get
            Return mNbAttribut
        End Get
        Set(ByVal value As Integer)
            mNbAttribut = value
            
        End Set
    End Property


    Private mSCDAttribut As ListeDescripteurReference
    Public Property SCDAttribut() As ListeDescripteurReference
        Get
            Return mSCDAttribut
        End Get
        Set(ByVal value As ListeDescripteurReference)
            mSCDAttribut = value
        End Set
    End Property

    Private mValeurAtt As System.Collections.Generic.List(Of String)
    Public Property ValeurAtt() As System.Collections.Generic.List(Of String)
        Get
            Return mValeurAtt
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            mValeurAtt = value
        End Set
    End Property


    Private mEstAssocieA As System.Collections.Generic.List(Of Association)
    Public Property EstAssocieA() As System.Collections.Generic.List(Of Association)
        Get
            Return mEstAssocieA
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of Association))
            mEstAssocieA = value
        End Set
    End Property

    Public Sub New()
        mEstAssocieA = New System.Collections.Generic.List(Of Association)
    End Sub

    Public Function OrdreTEX_id(ByVal SCDAttIndex As Integer) As Integer
        Select Case mSCDAttribut(SCDAttIndex)._ID

            Case "TEX_id"
                Return 1
            Case "TEX2_id"
                Return 2
            Case "TEX3_id"
                Return 3
            Case "TEX4_id"
                Return 4
            Case "TEX5_id"
                Return 5
            Case "TEX6_id"
                Return 6
            Case "TEX7_id"
                Return 7
            Case "TEX8_id"
                Return 8
            Case "TEX9_id"
                Return 9
            Case "TEX10_id"
                Return 10


        End Select


    End Function

    Public Function IndexOfAttribut(ByVal ref As String) As Integer

        

        If mSCDAttribut Is Nothing Then
            Return -1
        End If
        For i = 0 To mSCDAttribut.Count - 1
            If mSCDAttribut(i)._ID = ref Then
                Return i
            End If
        Next

        Return -1

            

    End Function
End Class
