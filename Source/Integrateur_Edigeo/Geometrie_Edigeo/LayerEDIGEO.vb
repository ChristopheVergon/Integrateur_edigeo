Public Class LayerEDIGEO

    Private mLayerName As String
    Public Property LayerName() As String
        Get
            Return mLayerName
        End Get
        Set(ByVal value As String)
            mLayerName = value
        End Set
    End Property


    Private mDictionaryObj As DictionaryObjetEDIGEO
    Public Property DictionaryObj() As DictionaryObjetEDIGEO
        Get
            Return mDictionaryObj
        End Get
        Set(ByVal value As DictionaryObjetEDIGEO)
            mDictionaryObj = value
        End Set
    End Property

    Public Sub New(ByVal NomDeLaCouche As String)
        mLayerName = NomDeLaCouche
    End Sub


    Private mTableName As String
    Public Property TableName() As String
        Get
            Return mTableName
        End Get
        Set(ByVal value As String)
            mTableName = value
        End Set
    End Property


    Private mLayerId As Integer
    Public Property LayerId() As Integer
        Get
            Return mLayerId
        End Get
        Set(ByVal value As Integer)
            mLayerId = value
        End Set
    End Property

End Class
