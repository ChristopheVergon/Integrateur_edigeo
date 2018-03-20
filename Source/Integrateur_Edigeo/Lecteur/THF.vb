
Public Class THF_Liste
    Private m_col As System.Collections.Generic.List(Of THF)



    Public Property _Col() As System.Collections.Generic.List(Of THF)
        Get
            Return m_col
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of THF))
            m_col = value
        End Set
    End Property

    Private Sub Sort()
        m_col.Sort(New Comparison(Of THF)(Function(x As THF, y As THF) x.Taille.CompareTo(y.Taille)))
    End Sub

    Public Sub New(rf As System.Collections.ObjectModel.ReadOnlyCollection(Of String))

        m_col = New System.Collections.Generic.List(Of THF)


        For i = 0 To rf.Count - 1
            Dim t As New THF(rf(i))
            m_col.Add(t)
        Next

        Sort()

    End Sub
End Class

Public Class THF
    Private mChemin As String
    Public Property Chemin() As String
        Get
            Return mChemin
        End Get
        Set(ByVal value As String)
            mChemin = value
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

    Private mTaille As Integer
    Public Property Taille() As Integer
        Get
            Return mTaille
        End Get
        Set(ByVal value As Integer)
            mTaille = value
        End Set
    End Property

    Public Sub New(RF As String)


        Dim f As New System.IO.FileInfo(RF)

        mChemin = f.DirectoryName
        mNom = f.Name

        Dim s As String = "E" & mNom.Substring(3, 5) & "S1.vec"
        s = mChemin & "\" & s
        Dim f2 As New System.IO.FileInfo(s)
        'mTaille = f2.Length


    End Sub
End Class
