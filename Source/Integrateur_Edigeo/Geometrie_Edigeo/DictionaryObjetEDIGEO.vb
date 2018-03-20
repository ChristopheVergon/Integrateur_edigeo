Imports System.Collections.Generic
Public Class DictionaryObjetEDIGEO

    Private mDic As Dictionary(Of String, ObjetEDIGEO)


    Private mItem As ObjetEDIGEO
    Default Public Property Item(ByVal i As Integer) As ObjetEDIGEO
        Get
            Return mDic.ElementAt(i).Value
        End Get
        Set(ByVal value As ObjetEDIGEO)
            mItem = value
        End Set
    End Property


    Public Function Item1(ByVal Nomlot As String, ByVal K As String) As ObjetEDIGEO
        K = Nomlot & K
        Return mDic.Item(K)
    End Function

    Public Function count() As Integer
        Return mDic.Count
    End Function

    Public Sub New()
        mDic = New Dictionary(Of String, ObjetEDIGEO)
    End Sub

    Public Sub add(ByVal NomLot As String, ByVal OE As ObjetEDIGEO)

        mDic.Add(NomLot & OE.ID_Objet, OE)
    End Sub

    Public Sub clear()
        mDic.Clear()
    End Sub

    Public Function Remove(ByVal Nomlot As String, ByVal K As String) As Boolean
        K = Nomlot & K
        Return mDic.Remove(K)
    End Function

    Public Function TryGetValue(ByVal Nomlot As String, ByVal K As String, ByRef value As ObjetEDIGEO) As Boolean
        K = Nomlot & K
        Return mDic.TryGetValue(K, value)
    End Function


    Public Function ConstruitCouche(ByVal NomCouche As String) As LayerEDIGEO

        Dim DicCouche As New DictionaryObjetEDIGEO

        For Each OB In mDic.Values
            If OB.NameSCD = NomCouche Then
                DicCouche.add(OB.NomLot, OB)
            End If
        Next

        Dim l As New LayerEDIGEO(NomCouche)

        l.DictionaryObj = DicCouche

        Return l

    End Function
End Class
