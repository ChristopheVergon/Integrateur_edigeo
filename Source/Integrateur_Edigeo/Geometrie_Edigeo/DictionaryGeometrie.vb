Imports System.Collections.Generic
Public Class DictionaryGeometrie
    Private mDic As Dictionary(Of String, Geometrie)


    Public Function Item(ByVal Nomlot As String, ByVal K As String) As Geometrie
        K = Nomlot & K
        Return mDic.Item(K)
    End Function

    Public Function count() As Integer
        Return mDic.Count
    End Function

    Public Sub New()
        mDic = New Dictionary(Of String, Geometrie)
    End Sub

    Public Sub add(ByVal NomLot As String, ByVal OE As Geometrie)
        mDic.Add(NomLot & OE.Identificateur, OE)
    End Sub

    Public Sub clear()
        mDic.Clear()
    End Sub

    Public Function Remove(ByVal Nomlot As String, ByVal K As String) As Boolean
        K = Nomlot & K
        Return mDic.Remove(K)
    End Function

    Public Function TryGetValue(ByVal Nomlot As String, ByVal K As String, ByRef value As Geometrie) As Boolean
        K = Nomlot & K
        Return mDic.TryGetValue(K, value)
    End Function

End Class
