Public Class Descripteur_Reference

    Private mGroupe As String
    Public Property Groupe() As String
        Get
            Return mGroupe
        End Get
        Set(ByVal value As String)
            mGroupe = value
        End Set
    End Property


    Private mModele As String
    Public Property Modele() As String
        Get
            Return mModele
        End Get
        Set(ByVal value As String)
            mModele = value
        End Set
    End Property


    Private mDescripteurType As String
    Public Property DescripteurType() As String
        Get
            Return mDescripteurType
        End Get
        Set(ByVal value As String)
            mDescripteurType = value
        End Set
    End Property


    Private m_ID As String
    Public Property _ID() As String
        Get
            Return m_ID
        End Get
        Set(ByVal value As String)
            m_ID = value
        End Set
    End Property


    Private mNbOccurence As Integer
    Public Property NbOccurence() As Integer
        Get
            Return mNbOccurence
        End Get
        Set(ByVal value As Integer)
            mNbOccurence = value
        End Set
    End Property

End Class
