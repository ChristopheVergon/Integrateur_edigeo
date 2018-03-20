Public Class SCD
    Inherits FichierEDIGEO



    Private mDictionaryObjet As System.Collections.Generic.Dictionary(Of String, SCD_Objet)
    Public Property DictionaryObjet() As System.Collections.Generic.Dictionary(Of String, SCD_Objet)
        Get
            Return mDictionaryObjet
        End Get
        Set(ByVal value As System.Collections.Generic.Dictionary(Of String, SCD_Objet))
            mDictionaryObjet = value
        End Set
    End Property

    Private mListeObjet As System.ComponentModel.BindingList(Of SCD_Objet)
    Public ReadOnly Property ListeObjet() As System.ComponentModel.BindingList(Of SCD_Objet)
        Get
            If mListeObjet Is Nothing Then
                InitBindingObj()
            End If
            Return mListeObjet
        End Get

    End Property
    Private Sub InitBindingObj()
        mListeObjet = New System.ComponentModel.BindingList(Of SCD_Objet)
        For Each obj As KeyValuePair(Of String, SCD_Objet) In mDictionaryObjet
            mListeObjet.Add(obj.Value)
        Next
    End Sub
    Private mDictionaryAttribut As System.Collections.Generic.Dictionary(Of String, SCD_Attribut)
    Public Property DictionaryAttribut() As System.Collections.Generic.Dictionary(Of String, SCD_Attribut)
        Get
            Return mDictionaryAttribut
        End Get
        Set(ByVal value As System.Collections.Generic.Dictionary(Of String, SCD_Attribut))
            mDictionaryAttribut = value
        End Set
    End Property


    Private mDictionaryPrimitive As System.Collections.Generic.Dictionary(Of String, SCD_Primitive)
    Public Property DictionaryPrimitive() As System.Collections.Generic.Dictionary(Of String, SCD_Primitive)
        Get
            Return mDictionaryPrimitive
        End Get
        Set(ByVal value As System.Collections.Generic.Dictionary(Of String, SCD_Primitive))
            mDictionaryPrimitive = value
        End Set
    End Property

    Private mListeRelationConstruction As System.Collections.Generic.List(Of SCD_RelConstruction)
    Public ReadOnly Property ListeRelationConstruction() As System.Collections.Generic.List(Of SCD_RelConstruction)
        Get
            If mListeRelationConstruction Is Nothing Then
                InitRelConstruction()
            End If
            Return mListeRelationConstruction
        End Get

    End Property

    Private Sub InitRelConstruction()
        mListeRelationConstruction = New System.Collections.Generic.List(Of SCD_RelConstruction)
        For Each obj As KeyValuePair(Of String, SCD_RelConstruction) In mDictionaryRelConcstruction
            mListeRelationConstruction.Add(obj.Value)
        Next
    End Sub

    Private mDictionaryRelConcstruction As System.Collections.Generic.Dictionary(Of String, SCD_RelConstruction)
    Public Property DictionaryRelConstruction() As System.Collections.Generic.Dictionary(Of String, SCD_RelConstruction)
        Get
            Return mDictionaryRelConcstruction
        End Get
        Set(ByVal value As System.Collections.Generic.Dictionary(Of String, SCD_RelConstruction))
            mDictionaryRelConcstruction = value
        End Set
    End Property


    Private mDictionaryRelSemantique As System.Collections.Generic.Dictionary(Of String, SCD_RelSemantique)
    Public Property DictionaryRelSemantique() As System.Collections.Generic.Dictionary(Of String, SCD_RelSemantique)
        Get
            Return mDictionaryRelSemantique
        End Get
        Set(ByVal value As System.Collections.Generic.Dictionary(Of String, SCD_RelSemantique))
            mDictionaryRelSemantique = value
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

        MyBase.FileName = MyBase.Chemin & nomlot & nom.Nom & ".SCD"
        Fint = f
    End Sub
    Public Overrides Sub InitDictionary()
        InitSCDObjet()

        InitSCDPrimitive()

        InitSCDAttribut()

        InitSCDPrimitive()

        InitSCDRelSemantique()

        InitSCDRelConstruction()

    End Sub
    Private Sub InitSCDObjet()

        Dim zl As New System.Collections.Generic.List(Of ZoneEdigeo)

        zl = MyBase.ZoneListe.ChercheToutZ6("OBJ")

        mDictionaryObjet = New System.Collections.Generic.Dictionary(Of String, SCD_Objet)

        zl.ForEach(AddressOf AjouteObj)

        If mDictionaryObjet.Where(Function(zone) zone.Value.Erreur <> ErreurStructure.NONE).Count > 0 Then
            ' MsgBox("Erreur structure objet dans SCD")
        End If

    End Sub

    Private Sub AjouteObj(ByVal z As ZoneEdigeo)
        Dim cur As Integer = MyBase.ZoneListe.ListeZ.IndexOf(z)
        Dim dic_o As New SCD_Objet
        dic_o.Affecte(MyBase.ZoneListe, cur)
        mDictionaryObjet.Add(dic_o.Identificateur, dic_o)
    End Sub

    Private Sub InitSCDPrimitive()

        Dim zl As New System.Collections.Generic.List(Of ZoneEdigeo)

        zl = MyBase.ZoneListe.ChercheToutZ6("PGE")

        mDictionaryPrimitive = New System.Collections.Generic.Dictionary(Of String, SCD_Primitive)

        zl.ForEach(AddressOf AjoutePrimitive)

        If mDictionaryPrimitive.Where(Function(zone) zone.Value.Erreur <> ErreurStructure.NONE).Count > 0 Then
            MsgBox("Erreur structure primitive dans SCD")
        End If

    End Sub

    Private Sub AjoutePrimitive(ByVal z As ZoneEdigeo)
        Dim cur As Integer = MyBase.ZoneListe.ListeZ.IndexOf(z)
        Dim dic_o As New SCD_Primitive
        dic_o.Affecte(MyBase.ZoneListe, cur)
        mDictionaryPrimitive.Add(dic_o.Identificateur, dic_o)
    End Sub

    Private mDico As DIC
    Public Property DICO() As DIC
        Get
            Return mDico
        End Get
        Set(ByVal value As DIC)
            mDico = value
        End Set
    End Property

    Private Sub InitSCDAttribut()

        Dim zl As New System.Collections.Generic.List(Of ZoneEdigeo)

        mDico = dico
        zl = MyBase.ZoneListe.ChercheToutZ6("ATT")

        mDictionaryAttribut = New System.Collections.Generic.Dictionary(Of String, SCD_Attribut)

        zl.ForEach(AddressOf AjouteAttribut)

        If mDictionaryPrimitive.Where(Function(zone) zone.Value.Erreur <> ErreurStructure.NONE).Count > 0 Then
            MsgBox("Erreur structure attribut dans SCD")
        End If

    End Sub

    Private Sub AjouteAttribut(ByVal z As ZoneEdigeo)
        Dim cur As Integer = MyBase.ZoneListe.ListeZ.IndexOf(z)
        Dim dic_o As New SCD_Attribut
        dic_o.Affecte(MyBase.ZoneListe, cur, mDico)
        mDictionaryAttribut.Add(dic_o.Identificateur, dic_o)
    End Sub

    Private Sub InitSCDRelSemantique()

        Dim zl As New System.Collections.Generic.List(Of ZoneEdigeo)

        zl = MyBase.ZoneListe.ChercheToutZ6("ASS")

        mDictionaryRelSemantique = New System.Collections.Generic.Dictionary(Of String, SCD_RelSemantique)

        zl.ForEach(AddressOf AjouteRelSemantique)

        If mDictionaryRelSemantique.Where(Function(zone) zone.Value.Erreur <> ErreurStructure.NONE).Count > 0 Then
            MsgBox("Erreur structure relation sémantique dans SCD")
        End If

    End Sub

    Private Sub AjouteRelSemantique(ByVal z As ZoneEdigeo)
        Dim cur As Integer = MyBase.ZoneListe.ListeZ.IndexOf(z)
        Dim dic_o As New SCD_RelSemantique
        dic_o.Affecte(MyBase.ZoneListe, cur)
        mDictionaryRelSemantique.Add(dic_o.Identificateur, dic_o)
    End Sub

    Private Sub InitSCDRelConstruction()

        Dim zl As New System.Collections.Generic.List(Of ZoneEdigeo)

        zl = MyBase.ZoneListe.ChercheToutZ6("REL")

        mDictionaryRelConcstruction = New System.Collections.Generic.Dictionary(Of String, SCD_RelConstruction)

        zl.ForEach(AddressOf AjouteRelConstruction)

        If mDictionaryRelConcstruction.Where(Function(zone) zone.Value.Erreur <> ErreurStructure.NONE).Count > 0 Then
            MsgBox("Erreur structure relation construction dans SCD")
        End If

    End Sub

    Private Sub AjouteRelConstruction(ByVal z As ZoneEdigeo)
        Dim cur As Integer = MyBase.ZoneListe.ListeZ.IndexOf(z)
        Dim dic_o As New SCD_RelConstruction
        dic_o.Affecte(MyBase.ZoneListe, cur)
        mDictionaryRelConcstruction.Add(dic_o.Identificateur, dic_o)
    End Sub

    Public Function ChercheRelationConstructionObj(ByVal ObjName As String) As System.Collections.Generic.List(Of SCD_RelConstruction)
        Dim l As New System.Collections.Generic.List(Of SCD_RelConstruction)
        For Each obj As KeyValuePair(Of String, SCD_RelConstruction) In mDictionaryRelConcstruction

            If obj.Value.RefSCDElement.FindAll(Function(idr) idr._ID = ObjName).Count > 0 Then
                l.Add(obj.Value)
            End If
        Next

        Return l
    End Function

    Protected Overrides Sub Finalize()

        MyBase.Finalize()
    End Sub
End Class
