Public Class DIC
    Inherits FichierEDIGEO

    Private mDictionaryObjet As System.Collections.Generic.Dictionary(Of String, DIC_Objet)
    Public Property DictionaryObjet() As System.Collections.Generic.Dictionary(Of String, DIC_Objet)
        Get
            Return mDictionaryObjet
        End Get
        Set(ByVal value As System.Collections.Generic.Dictionary(Of String, DIC_Objet))
            mDictionaryObjet = value
        End Set
    End Property


    Private mDictionaryAttribut As System.Collections.Generic.Dictionary(Of String, DIC_Attribut)
    Public Property DictionaryAttribut() As System.Collections.Generic.Dictionary(Of String, DIC_Attribut)
        Get
            Return mDictionaryAttribut
        End Get
        Set(ByVal value As System.Collections.Generic.Dictionary(Of String, DIC_Attribut))
            mDictionaryAttribut = value
        End Set
    End Property


    Private mDictionaryRelation As System.Collections.Generic.Dictionary(Of String, DIC_Relation)
    Public Property DictionaryRelation() As System.Collections.Generic.Dictionary(Of String, DIC_Relation)
        Get
            Return mDictionaryRelation
        End Get
        Set(ByVal value As System.Collections.Generic.Dictionary(Of String, DIC_Relation))
            mDictionaryRelation = value
        End Set
    End Property


    Public Sub New(ByVal chemin As String, ByVal nomlot As String, ByVal nom As ID_Nom, f As FormIntegration)
        If chemin.EndsWith("\") Then
        Else
            chemin = chemin & "\"
        End If
        MyBase.Chemin = chemin

        MyBase.Info = New ID_Nom
        MyBase.Info = nom

        MyBase.FileName = MyBase.Chemin & nomlot & nom.Nom & ".DIC"
       
        Fint = f

    End Sub

    Public Overrides Sub InitDictionary()
        InitDicObjet()

        InitDicAttribut()

        InitDicRelation()

    End Sub

    Private Sub InitDicObjet()

        Dim zl As New System.Collections.Generic.List(Of ZoneEdigeo)

        zl = MyBase.ZoneListe.ChercheToutZ6("DID")

        mDictionaryObjet = New System.Collections.Generic.Dictionary(Of String, DIC_Objet)

        zl.ForEach(AddressOf AjouteObj)

        If mDictionaryObjet.Where(Function(zone) zone.Value.Erreur <> ErreurStructure.NONE).Count > 0 Then
            fint.flog.writeline("Erreur structure objet dans dictionnaire")
        Else
            fint.flog.writeline("Structure objet OK")
        End If

    End Sub

    Private Sub AjouteObj(ByVal z As ZoneEdigeo)
        Dim cur As Integer = MyBase.ZoneListe.ListeZ.IndexOf(z)
        Dim dic_o As New DIC_Objet
        dic_o.Affecte(MyBase.ZoneListe, cur)
        mDictionaryObjet.Add(dic_o.Identificateur, dic_o)
    End Sub

    Private Sub InitDicAttribut()
        Dim zl As New System.Collections.Generic.List(Of ZoneEdigeo)
        zl = MyBase.ZoneListe.ChercheToutZ6("DIA")

        mDictionaryAttribut = New System.Collections.Generic.Dictionary(Of String, DIC_Attribut)
        zl.ForEach(AddressOf AjouteAttribut)

        If mDictionaryAttribut.Where(Function(zone) zone.Value.Erreur <> ErreurStructure.NONE).Count > 0 Then
            fint.flog.writeline("Erreur structure attribut dans dictionnaire")
        Else
            Fint.Flog.WriteLine("Structure attribut OK")
        End If
    End Sub

    Private Sub AjouteAttribut(ByVal z As ZoneEdigeo)
        Dim cur As Integer = MyBase.ZoneListe.ListeZ.IndexOf(z)
        Dim dic_a As New DIC_Attribut
        dic_a.Affecte(MyBase.ZoneListe, cur)
        mDictionaryAttribut.Add(dic_a.Identificateur, dic_a)
    End Sub

    Private Sub InitDicRelation()
        Dim zl As New System.Collections.Generic.List(Of ZoneEdigeo)
        zl = MyBase.ZoneListe.ChercheToutZ6("DIR")

        mDictionaryRelation = New System.Collections.Generic.Dictionary(Of String, DIC_Relation)
        zl.ForEach(AddressOf AjouteRelation)

        If mDictionaryRelation.Where(Function(zone) zone.Value.Erreur <> ErreurStructure.NONE).Count > 0 Then
            Fint.Flog.WriteLine("Erreur structure relation dans dictionnaire")
        Else
            fint.flog.writeline("Structure relation OK")
        End If
    End Sub

    Private Sub AjouteRelation(ByVal z As ZoneEdigeo)
        Dim cur As Integer = MyBase.ZoneListe.ListeZ.IndexOf(z)
        Dim dic_a As New DIC_Relation
        dic_a.Affecte(MyBase.ZoneListe, cur)
        mDictionaryRelation.Add(dic_a.Identificateur, dic_a)
    End Sub

    Protected Overrides Sub Finalize()

        MyBase.Finalize()
    End Sub
End Class
