Public Class FACE
    Inherits Geometrie

    Private mListeArc As System.Collections.Generic.List(Of ARC)
    Public Property ListeARC() As System.Collections.Generic.List(Of ARC)
        Get
            Return mListeArc
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of ARC))
            mListeArc = value
        End Set
    End Property
    Private mNbAttribut As Integer
    Public Property NbAttribut() As Integer
        Get
            Return mNbAttribut
        End Get
        Set(ByVal value As Integer)
            mNbAttribut = value
            If value > 0 Then
                mSCDAttribut = New ListeDescripteurReference

                mValeurAtt = New System.Collections.Generic.List(Of String)
            End If
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
    Public Sub New()
        mListeArc = New System.Collections.Generic.List(Of ARC)
    End Sub
End Class
