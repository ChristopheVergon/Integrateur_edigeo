Imports Npgsql
Imports Wkb.Serialization
Public Class BaseCadastre

    Inherits BasePostGis

    Private mFlog As System.IO.StreamWriter
    Public Property Flog() As System.IO.StreamWriter
        Get
            Return mFlog
        End Get
        Set(ByVal value As System.IO.StreamWriter)
            mFlog = value
        End Set
    End Property

    Public Sub New(ByVal databasename As String, ByVal cnn As NpgsqlConnectionStringBuilder, ByVal NomSchema As String, Optional ByVal ConDirect As Boolean = False)
        MyBase.New(databasename, cnn, NomSchema, ConDirect)


        If mConnFailed Then
            Exit Sub
        End If

        If mConnDirect Then
            Exit Sub
        End If

        cnngen = mPostGisCnn

        If mBaseExiste Then

        Else
            InitTable()
        End If

    End Sub
    Private Sub InitTable()
        CreateTableParcelle()
        CreateTableSubSection()
        CreateTableSection()
        CreateTableCommune()
        CreateTableBatiment()
        CreateTableLieuDit()
        CreateTableLabel()
        CreateTableTronFluv()
        CreateTableZoneCommuni()
        CreateTableTronRoute()
        CreateTableTopoLine()
        CreateTableTPoint()
        CreateTableVoiep()
        CreateTableTsurf()
    End Sub
    Private Sub CreateTableParcelle()
        m_cmd = New NpgsqlCommand
        m_cmd.Connection = mPostGisCnn

        m_cmd.CommandText = "CREATE TABLE " & SchemaName & ".parcelle (idparcelle serial, ptrsubsection integer, numero varchar(255)," _
& " contenance integer, dateacte varchar(8), primitive varchar(4),arpente boolean, nfp boolean, anomalie integer,ptrparcasspdl integer," _
& " pdltype varchar(3), numvoie varchar(4), voiemajic varchar(5), rivoli varchar(5),inseemere varchar(3)," _
& "prefsecmere varchar(3), sectionmere varchar(2), numplanmere character varying(4), typefiliation varchar(1),millesime varchar(4),active boolean);"
        m_cmd.ExecuteNonQuery()
        m_cmd.CommandText = "SELECT AddGeometryColumn('" & SchemaName & "','parcelle','the_geom'," & SRID & ",'POLYGON',2);"
        m_cmd.ExecuteNonQuery()
        

        m_cmd.CommandText = "CREATE TABLE " & SchemaName & ".textparcelle (idtextparcelle serial,ptrparcelle integer, numero varchar(255));"
        m_cmd.ExecuteNonQuery()
        m_cmd.CommandText = "SELECT AddGeometryColumn ('" & SchemaName & "', 'textparcelle', 'the_geom'," & SRID & ",'POINT',2);"
        m_cmd.ExecuteNonQuery()

        m_cmd.CommandText = "CREATE TABLE " & SchemaName & ".numvoie (idnumvoie serial,ptrparcelle integer, numero varchar(255));"
        m_cmd.ExecuteNonQuery()
        m_cmd.CommandText = "SELECT AddGeometryColumn ('" & SchemaName & "', 'numvoie', 'the_geom'," & SRID & ",'POINT',2);"
        m_cmd.ExecuteNonQuery()


        m_cmd.CommandText = "CREATE TABLE " & SchemaName & ".SYMBLIM_PARCELLE (idsymblim_parcelle serial,sym_id integer,ori_id numeric(9,6), ptrparcelle int)"
        m_cmd.ExecuteNonQuery()
        m_cmd.CommandText = "SELECT AddGeometryColumn ('" & SchemaName & "','symblim_parcelle', 'the_geom'," & SRID & ",'POINT',2);"
        m_cmd.ExecuteNonQuery()

        Dim autre = New String() {"BORNE_PARCELLE"}
        For Each s In autre
            m_cmd.CommandText = "CREATE TABLE " & SchemaName & "." & s & "(id" & s & " serial," & "ptrparcelle int)"
            m_cmd.ExecuteNonQuery()
            m_cmd.CommandText = "SELECT AddGeometryColumn ('" & SchemaName & "','" & s.ToLower() & "', 'the_geom'," & SRID & ",'POINT',2);"
            m_cmd.ExecuteNonQuery()
        Next

        m_cmd.Dispose()
    End Sub
  
    Private Sub CreateTableBatiment()
        
        m_ds = New System.Data.DataSet


        m_cmd = New NpgsqlCommand
        m_cmd.Connection = mPostGisCnn
        m_cmd.CommandText = "CREATE TABLE " & SchemaName & ".batiment (idbatiment serial, nom varchar (255), dur boolean, millesime varchar (4),active boolean);"
        m_cmd.ExecuteNonQuery()
        m_cmd.CommandText = "SELECT AddGeometryColumn('" & SchemaName & "','batiment','the_geom'," & SRID & ",'POLYGON',2);"
        m_cmd.ExecuteNonQuery()

    End Sub

   


   
    

    Private Sub CreateTableSubSection()

        m_cmd.CommandText = "CREATE TABLE " & SchemaName & ".subsection (idsubsection serial, nom varchar (10),ptrsection integer);"
        m_cmd.ExecuteNonQuery()
        m_cmd.CommandText = "SELECT AddGeometryColumn('" & SchemaName & "','subsection','the_geom'," & SRID & ",'POLYGON',2);"
        m_cmd.ExecuteNonQuery()

    End Sub

    Private Sub CreateTableSection()
        m_cmd.CommandText = "CREATE TABLE " & SchemaName & ".section (idsection serial, nom varchar (10),ptrcommune integer,fusion varchar(3));"
        m_cmd.ExecuteNonQuery()

        m_cmd.CommandText = "SELECT AddGeometryColumn('" & SchemaName & "','section','the_geom'," & SRID & ",'POLYGON',2);"
        m_cmd.ExecuteNonQuery()

        m_cmd.CommandText = "SELECT AddGeometryColumn('" & SchemaName & "','section','the_point'," & SRID & ",'POINT',2);"
        m_cmd.ExecuteNonQuery()
    End Sub
    Public Function SectionExiste(ByVal insee As String, ByVal nomsec As String, fus As String) As Integer

        m_cmd.CommandText = "SELECT idsection FROM " & SchemaName & ".commune INNER JOIN " & SchemaName & ".section ON idcommune=ptrcommune WHERE insee='" & insee & "' AND section.nom='" & nomsec & "' AND fusion='" & fus & "';"

        m_ds = New System.Data.DataSet
        m_da.SelectCommand = m_cmd
        m_da.Fill(m_ds)

        If m_ds.Tables(0).Rows.Count = 0 Then
            Return -1
        Else
            Return m_ds.Tables(0).Rows(0).Item(0)
        End If
    End Function
    Private Sub CreateTableCommune()
        m_cmd.CommandText = "CREATE TABLE " & SchemaName & ".commune (idcommune serial, nom varchar (255),insee varchar(3));"
        m_cmd.ExecuteNonQuery()
        m_cmd.CommandText = "SELECT AddGeometryColumn('" & SchemaName & "','commune','the_geom'," & SRID & ",'POLYGON',2);"
        m_cmd.ExecuteNonQuery()
        m_cmd.CommandText = "SELECT AddGeometryColumn('" & SchemaName & "','commune','the_point'," & SRID & ",'POINT',2);"
        m_cmd.ExecuteNonQuery()
    End Sub

    Public Sub CreateTableLieuDit()

        m_cmd.CommandText = "CREATE TABLE " & SchemaName & ".lieudit (idlieudit serial);"
        m_cmd.ExecuteNonQuery()
        m_cmd.CommandText = "SELECT AddGeometryColumn('" & SchemaName & "','lieudit','the_geom'," & SRID & ",'POLYGON',2);"
        m_cmd.ExecuteNonQuery()
    End Sub
    Public Sub CreateTableLabel()
        m_cmd.CommandText = "CREATE TABLE " & SchemaName & ".label (idlabel serial, valeur varchar (255),ptrobj integer,reftable smallint,ordre smallint,police varchar(255),hauteur real,angle real);"
        m_cmd.ExecuteNonQuery()
        m_cmd.CommandText = "SELECT AddGeometryColumn('" & SchemaName & "','label','the_geom'," & SRID & ",'POINT',2);"
        m_cmd.ExecuteNonQuery()
    End Sub

    Public Sub CreateTableVoiep()
        m_cmd.CommandText = "CREATE TABLE " & SchemaName & ".voiep (idvoiep serial, valeur varchar (255),police varchar(255),hauteur real,angle real);"
        m_cmd.ExecuteNonQuery()
        m_cmd.CommandText = "SELECT AddGeometryColumn('" & SchemaName & "','voiep','the_geom'," & SRID & ",'POINT',2);"
        m_cmd.ExecuteNonQuery()
    End Sub

    Public Sub CreateTableTronFluv()
        m_cmd.CommandText = "CREATE TABLE " & SchemaName & ".tronfluv (idtronfluv serial,ptrcommune integer);"
        m_cmd.ExecuteNonQuery()
        m_cmd.CommandText = "SELECT AddGeometryColumn('" & SchemaName & "','tronfluv','the_geom'," & SRID & ",'POLYGON',2);"
        m_cmd.ExecuteNonQuery()
    End Sub

    Public Sub CreateTableZoneCommuni()
        m_cmd.CommandText = "CREATE TABLE " & SchemaName & ".zonecommuni (idzonecommuni serial,ptrcommune integer);"
        m_cmd.ExecuteNonQuery()
       
        m_cmd.CommandText = "SELECT AddGeometryColumn('" & SchemaName & "','zonecommuni','the_geom'," & SRID & ",'LINESTRING',2);"
        m_cmd.ExecuteNonQuery()
    End Sub

    Public Sub CreateTableTronRoute()
        m_cmd.CommandText = "CREATE TABLE " & SchemaName & ".tronroute (idtronroute serial,ptrcommune integer);"
        m_cmd.ExecuteNonQuery()
      
        m_cmd.CommandText = "SELECT AddGeometryColumn('" & SchemaName & "','tronroute','the_geom'," & SRID & ",'LINESTRING',2);"
        m_cmd.ExecuteNonQuery()
    End Sub

    Public Sub CreateTableTopoLine()
        m_cmd.CommandText = "CREATE TABLE " & SchemaName & ".topoline (idtopoline serial, nom varchar (255),ptrcommune integer,symbol integer);"
        m_cmd.ExecuteNonQuery()
        m_cmd.CommandText = "SELECT AddGeometryColumn('" & SchemaName & "','topoline','the_geom'," & SRID & ",'LINESTRING',2);"
        m_cmd.ExecuteNonQuery()
    End Sub
    Public Sub CreateTableTPoint()
        m_cmd.CommandText = "CREATE TABLE " & SchemaName & ".tpoint (idtpoint serial, ptrcommune integer,valeur varchar(255),symbol integer,ori real);"
        m_cmd.ExecuteNonQuery()
        m_cmd.CommandText = "SELECT AddGeometryColumn('" & SchemaName & "','tpoint','the_geom'," & SRID & ",'POINT',2);"
        m_cmd.ExecuteNonQuery()
    End Sub
    Public Sub CreateTableTsurf()
        m_cmd.CommandText = "CREATE TABLE " & SchemaName & ".tsurf (idtsurf serial, valeur varchar(255), symbol integer,ptrcommune integer)"
        m_cmd.ExecuteNonQuery()
        m_cmd.CommandText = "SELECT AddGeometryColumn('" & SchemaName & "','tsurf','the_geom'," & SRID & ",'POLYGON',2);"
        m_cmd.ExecuteNonQuery()
    End Sub

    Public Sub CreateTableTempEdigeo(ByVal nomlot As String)
        nomlot = nomlot.ToLower
        m_cmd = New NpgsqlCommand
        m_cmd.Connection = mPostGisCnn
        m_cmd.CommandText = "DROP TABLE IF EXISTS " & nomlot & ";"
        m_cmd.ExecuteNonQuery()

        m_cmd.CommandText = "CREATE TEMP TABLE " & nomlot & " (idobj serial, ptrobj integer, refedigeo varchar(255));"


        Dim c As Integer = m_cmd.ExecuteNonQuery()
        m_cmd.Dispose()
    End Sub

    Public Sub PopulateBatiment(ByVal la As DictionaryObjetEDIGEO, ByVal nomlot As String)
        m_cmd = New NpgsqlCommand
        m_cmd.Connection = mPostGisCnn

        Dim sr As New Wkb.Serialization.WkbSerializer

        Dim wkb As New NpgsqlParameter
        wkb.ParameterName = "wkb"
        wkb.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea

        Dim dur As New NpgsqlParameter
        dur.ParameterName = "dur"
        dur.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Boolean

        Dim nom As New NpgsqlParameter
        nom.ParameterName = "nom"
        nom.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text

        Dim an As New NpgsqlParameter
        an.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text
        an.ParameterName = "an"
        an.Value = millesime

        m_cmd.Parameters.Clear()
        m_cmd.Parameters.Add(wkb)
        m_cmd.Parameters.Add(dur)
        m_cmd.Parameters.Add(nom)
        m_cmd.Parameters.Add(an)

        For I = 0 To la.count - 1

            Dim SURF As New ObjetEDIGEO_SURF
            SURF = CType(la(I), ObjetEDIGEO_SURF)

            Dim PO As New POLYGON
            PO = SURF.Polygone
            wkb.Value = sr.serialize(PO)

            Dim d As Integer = SURF.IndexOfAttribut("DUR_id")
            Dim n As Integer = SURF.IndexOfAttribut("TEX_id")



            If d = -1 Then
                dur.Value = False
            Else
                If SURF.ValeurAtt(d) = 1 Then
                    dur.Value = True
                Else
                    dur.Value = False
                End If
            End If

            If n = -1 Then
                nom.Value = ""
            Else
                nom.Value = SURF.ValeurAtt(n)
            End If

            m_cmd.CommandText = "INSERT INTO " & SchemaName & ".batiment (the_geom,dur,nom,millesime,active) VALUES (ST_GeomFromWKB(:wkb," & SRID & "),:dur,:nom,:an,true) RETURNING idbatiment;"
            m_cmd.ExecuteNonQuery()

           
        Next

        m_cmd.Dispose()
    End Sub
    Public Sub PopulateTopoLine(ByVal la As DictionaryObjetEDIGEO, ByVal nomlot As String)
        m_cmd = New NpgsqlCommand
        m_cmd.Connection = mPostGisCnn

        Dim sr As New Wkb.Serialization.WkbSerializer

        Dim wkb As New NpgsqlParameter
        wkb.ParameterName = "wkb"
        wkb.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea

        Dim symb As New NpgsqlParameter
        symb.ParameterName = "symb"
        symb.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer

        Dim nom As New NpgsqlParameter
        nom.ParameterName = "nom"
        nom.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text

        m_cmd.Parameters.Add(wkb)
        m_cmd.Parameters.Add(symb)
        m_cmd.Parameters.Add(nom)

        For I = 0 To la.count - 1

            Dim SURF As New ObjetEDIGEO_LIN
            SURF = CType(la(I), ObjetEDIGEO_LIN)


            wkb.Value = sr.serialize(SURF.Geom)

            Dim d As Integer = SURF.IndexOfAttribut("SYM_id")
            Dim n As Integer = SURF.IndexOfAttribut("TEX_id")

            If d = -1 Then
                symb.Value = -1
            Else
                symb.Value = SURF.ValeurAtt(d)
            End If

            If n = -1 Then
                nom.Value = ""
            Else
                nom.Value = SURF.ValeurAtt(n)
            End If

            m_cmd.CommandText = "INSERT INTO " & SchemaName & ".topoline (the_geom,ptrcommune,nom,symbol) VALUES (ST_GeomFromWKB(:wkb," & SRID & ")," & Idcommune & ",:nom,:symb) RETURNING idtopoline;"
            'm_ds = New System.Data.DataSet
            'm_da.SelectCommand = m_cmd
            'm_da.Fill(m_ds)
            Dim idtopoline As Integer = m_cmd.ExecuteScalar 'm_ds.Tables(0).Rows(0).Item(0)

            m_cmd.CommandText = "INSERT INTO " & nomlot & " (ptrobj,refedigeo) VALUES (" & idtopoline & ",'" & SURF.NomLot & SURF.ID_Objet & "');"
            m_cmd.ExecuteNonQuery()

        Next

        m_cmd.Dispose()
    End Sub
    Public Sub PopulateVoiep(ByVal la As DictionaryObjetEDIGEO, ByVal nomlot As String)
        m_cmd = New NpgsqlCommand
        m_cmd.Connection = mPostGisCnn

        Dim sr As New Wkb.Serialization.WkbSerializer

        Dim wkb As New NpgsqlParameter
        wkb.ParameterName = "wkb"
        wkb.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea


        Dim nom As New NpgsqlParameter
        nom.ParameterName = "nom"
        nom.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text


        Dim police As New NpgsqlParameter("police", NpgsqlTypes.NpgsqlDbType.Text)
        Dim hauteur As New NpgsqlParameter("hauteur", NpgsqlTypes.NpgsqlDbType.Real)
        Dim angle As New NpgsqlParameter("angle", NpgsqlTypes.NpgsqlDbType.Real)


        m_cmd.Parameters.Add(wkb)
        m_cmd.Parameters.Add(nom)
        m_cmd.Parameters.Add(police)
        m_cmd.Parameters.Add(hauteur)
        m_cmd.Parameters.Add(angle)

        For I = 0 To la.count - 1

            Dim SURF As New ObjetEDIGEO_PCT
            SURF = CType(la(I), ObjetEDIGEO_PCT)

            Dim PO As New NOEUD
            PO = SURF.Geom
            wkb.Value = sr.serialize(PO)

            Dim n As Integer = SURF.IndexOfAttribut("TEX_id")

            If n = -1 Then
                nom.Value = ""
            Else
                nom.Value = SURF.ValeurAtt(n)
            End If

            Dim P As New ObjetEDIGEO_PCT

            If SURF.EstAssocieA.Count = 0 Then

            Else
                For m = 0 To SURF.EstAssocieA(0).ListeObj.Count - 1
                    If SURF.EstAssocieA(0).ListeObj(m).Nature = NatureObjetSCD.PCT Then
                        P = SURF.EstAssocieA(0).ListeObj(m)


                        Dim di3 As Double = P.ValeurAtt(P.IndexOfAttribut("ID_S_ATT_DI3"))
                        Dim di4 As Double = P.ValeurAtt(P.IndexOfAttribut("ID_S_ATT_DI4"))
                        Dim f As String = P.ValeurAtt(P.IndexOfAttribut("ID_S_ATT_FON"))
                        Dim h As Double = P.ValeurAtt(P.IndexOfAttribut("ID_S_ATT_HEI"))
                        Dim a As Double = Math.Atan2(di4, di3) * (180 / Math.PI)

                        police.Value = f
                        hauteur.Value = h
                        angle.Value = a
                        '*******************************************************************************

                    End If
                Next
            End If

            m_cmd.CommandText = "INSERT INTO " & SchemaName & ".voiep (the_geom,valeur,police,hauteur,angle) VALUES (ST_GeomFromWKB(:wkb," & SRID & "),:nom,:police," & hauteur.Value & "," & angle.Value & ") RETURNING idvoiep;"
            'm_ds = New System.Data.DataSet
            'm_da.SelectCommand = m_cmd
            'm_da.Fill(m_ds)
            Dim idbat As Integer

            Try
                idbat = m_cmd.ExecuteScalar
                m_cmd.CommandText = "INSERT INTO " & nomlot & " (ptrobj,refedigeo) VALUES (" & idbat & ",'" & SURF.NomLot & SURF.ID_Objet & "');"
                m_cmd.ExecuteNonQuery()
            Catch ex As Exception

            End Try
            'm_ds.Tables(0).Rows(0).Item(0)

            

        Next

        m_cmd.Dispose()
    End Sub

    
    Public Sub PopulateTpoint(ByVal la As DictionaryObjetEDIGEO, ByVal nomlot As String)
        m_cmd = New NpgsqlCommand
        m_cmd.Connection = mPostGisCnn

        Dim sr As New Wkb.Serialization.WkbSerializer

        Dim wkb As New NpgsqlParameter
        wkb.ParameterName = "wkb"
        wkb.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea

        Dim symb As New NpgsqlParameter
        symb.ParameterName = "symb"
        symb.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer

        Dim nom As New NpgsqlParameter
        nom.ParameterName = "nom"
        nom.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text

        Dim ori As New NpgsqlParameter
        ori.ParameterName = "ori"
        ori.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Real

        m_cmd.Parameters.Clear()
        m_cmd.Parameters.Add(wkb)
        m_cmd.Parameters.Add(symb)
        m_cmd.Parameters.Add(nom)
        m_cmd.Parameters.Add(ori)

        For I = 0 To la.count - 1


            Dim SURF As New ObjetEDIGEO_PCT
            SURF = CType(la(I), ObjetEDIGEO_PCT)

            wkb.Value = sr.serialize(SURF.Geom)

            Dim d As Integer = SURF.IndexOfAttribut("SYM_id")
            Dim n As Integer = SURF.IndexOfAttribut("TEX_id")
            Dim o As Integer = SURF.IndexOfAttribut("ORI_id")

            If d = -1 Then
                symb.Value = -1
            Else
                symb.Value = SURF.ValeurAtt(d)
            End If

            If n = -1 Then
                nom.Value = ""
            Else
                nom.Value = SURF.ValeurAtt(n)
            End If

            If o = -1 Then
            Else
                ori.Value = Val(SURF.ValeurAtt(o))
            End If



            'm_cmd.CommandText = "INSERT INTO " & SchemaName & ".tpoint (the_geom,ptrcommune,ori,symbol,valeur) VALUES (ST_GeomFromWKB(:wkb," & SRID & ")," & Idcommune & "," & ori.Value & ",:symb,:nom) RETURNING idtpoint;"

            m_cmd.CommandText = "INSERT INTO " & SchemaName & ".tpoint (the_geom,ptrcommune,ori,symbol,valeur) VALUES (ST_GeomFromWKB(:wkb," & SRID & ")," & Idcommune & "," & ori.Value & ",:symb,:nom) RETURNING idtpoint;"
            'm_ds = New System.Data.DataSet
            'm_da.SelectCommand = m_cmd
            'm_da.Fill(m_ds)


            Dim idtopoline As Integer = m_cmd.ExecuteScalar 'm_ds.Tables(0).Rows(0).Item(0)



            m_cmd.CommandText = "INSERT INTO " & nomlot & " (ptrobj,refedigeo) VALUES (" & idtopoline & ",'" & SURF.NomLot & SURF.ID_Objet & "');"
            m_cmd.ExecuteNonQuery()

        Next

        m_cmd.Dispose()
    End Sub
    Private Idcommune As Integer
    Public Function CommuneExiste(ByVal insee As String) As Integer
        m_cmd.Connection = mPostGisCnn
        m_cmd.CommandText = "SELECT idcommune FROM " & SchemaName & ".commune WHERE insee='" & insee & "';"

        m_ds = New System.Data.DataSet
        m_da.SelectCommand = m_cmd
        m_da.Fill(m_ds)

        If m_ds.Tables(0).Rows.Count = 0 Then
            Return -1
        Else
            Return m_ds.Tables(0).Rows(0).Item(0)
        End If

    End Function
    Public Sub PopulateCommune(ByVal la As DictionaryObjetEDIGEO, ByVal nomlot As String)

        m_cmd = New NpgsqlCommand
        m_cmd.Connection = mPostGisCnn

        Dim sr As New Wkb.Serialization.WkbSerializer

        Dim wkb As New NpgsqlParameter
        wkb.ParameterName = "wkb"
        wkb.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea

        Dim wkbp As New NpgsqlParameter
        wkbp.ParameterName = "wkbp"
        wkbp.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea


        m_cmd.Parameters.Add(wkb)
        m_cmd.Parameters.Add(wkbp)

        Dim SURF As New ObjetEDIGEO_SURF
        SURF = CType(la(0), ObjetEDIGEO_SURF)

        Dim nom As Integer = SURF.IndexOfAttribut("TEX2_id")
        Dim insee As Integer = SURF.IndexOfAttribut("IDU_id")

        Idcommune = CommuneExiste(SURF.ValeurAtt(insee))

        If Idcommune = -1 Then
            Dim PO As New POLYGON
            PO = SURF.Polygone
            wkb.Value = sr.serialize(PO)

            '***********************************************************************************************************************************
            m_cmd.CommandText = "SELECT st_asbinary(st_pointonsurface(st_geomfromwkb(:wkb," & SRID & ")));"
            m_ds = New System.Data.DataSet
            m_da.SelectCommand = m_cmd
            m_da.Fill(m_ds)

            wkbp.Value = m_ds.Tables(0).Rows(0).Item(0)

            '***********************************************************************************************************************************
            m_cmd.CommandText = "INSERT INTO " & SchemaName & ".commune (nom,insee,the_geom,the_point) VALUES ('" & SURF.ValeurAtt(nom).Replace("'", "_") & "','" & SURF.ValeurAtt(insee) & "',st_geomfromwkb(:wkb," & SRID & "),st_geomfromwkb(:wkbp," & SRID & ")) RETURNING idcommune;"
                m_ds = New System.Data.DataSet
                m_da.SelectCommand = m_cmd
                m_da.Fill(m_ds)
                Idcommune = m_ds.Tables(0).Rows(0).Item(0)

            m_cmd.CommandText = "INSERT INTO " & nomlot & " (ptrobj,refedigeo) VALUES (" & Idcommune & ",'" & SURF.NomLot & SURF.ID_Objet & "');"
            m_cmd.ExecuteNonQuery()

        End If


        For j = 0 To SURF.EstAssocieA.Count - 1

            If SURF.EstAssocieA(j).RefSCD._ID = "SECTION_COMMUNE" Then

                m_cmd.CommandText = "SELECT ptrobj FROM " & nomlot & " WHERE refedigeo='" & SURF.EstAssocieA(j).ListeObj(0).NomLot & SURF.EstAssocieA(j).ListeObj(0).ID_Objet & "';"

                Dim idsection As Integer = m_cmd.ExecuteScalar 'm_ds.Tables(0).Rows(0).Item(0)

                m_cmd.CommandText = "UPDATE " & SchemaName & ".section SET ptrcommune=" & Idcommune & " WHERE idsection=" & idsection & ";"
                m_cmd.ExecuteNonQuery()


            End If
        Next



        m_cmd.Dispose()

    End Sub

    Public Sub PopulateSection(ByVal la As DictionaryObjetEDIGEO, ByVal nomlot As String)

        m_cmd = New NpgsqlCommand
        m_cmd.Connection = mPostGisCnn

        Dim sr As New Wkb.Serialization.WkbSerializer

        Dim wkb As New NpgsqlParameter
        wkb.ParameterName = "wkb"
        wkb.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea

        Dim wkbp As New NpgsqlParameter
        wkbp.ParameterName = "wkbp"
        wkbp.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea

        Dim fus As New NpgsqlParameter
        fus.ParameterName = "fus"
        fus.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text

        m_cmd.Parameters.Add(wkb)
        m_cmd.Parameters.Add(wkbp)
        m_cmd.Parameters.Add(fus)

        For I = 0 To la.count - 1


            Dim SURF As New ObjetEDIGEO_SURF
            SURF = CType(la(I), ObjetEDIGEO_SURF)

            Dim IDU As String = SURF.ValeurAtt(SURF.IndexOfAttribut("IDU_id"))
            Dim nomsec1 = IDU.Substring(IDU.Length - 2)
            fus.Value = IDU.Substring(3, 3)
            Dim insee As String = IDU.Substring(0, 3)

            Dim idsec As Integer = -1

            idsec = SectionExiste(insee, nomsec1, fus.Value)


            If idsec = -1 Then
                Dim PO As POLYGON = SURF.Polygone
                wkb.Value = sr.serialize(PO)

                'Dim idsection As Integer = -1
                '***********************************************************************************************************************************

                m_cmd.CommandText = "SELECT st_isvalid(st_geomfromwkb(:wkb," & SRID & "));"

                If cmd.ExecuteScalar = True Then
                    m_cmd.CommandText = "SELECT st_asbinary(st_pointonsurface(st_geomfromwkb(:wkb," & SRID & ")));"
                    m_ds = New System.Data.DataSet
                    m_da.SelectCommand = m_cmd
                    m_da.Fill(m_ds)
                    wkbp.Value = m_ds.Tables(0).Rows(0).Item(0)



                    m_cmd.CommandText = "INSERT INTO " & SchemaName & ".section (nom,the_geom,the_point,fusion) VALUES ('" & nomsec1 & "',st_geomfromwkb(:wkb," & SRID & "),st_geomfromwkb(:wkbp," & SRID & "),:fus) RETURNING idsection;"
                    m_ds = New System.Data.DataSet
                    m_da.SelectCommand = m_cmd
                    m_da.Fill(m_ds)
                    idsec = m_ds.Tables(0).Rows(0).Item(0)

                    m_cmd.CommandText = "INSERT INTO " & nomlot & " (ptrobj,refedigeo) VALUES (" & idsec & ",'" & SURF.NomLot & SURF.ID_Objet & "');"
                    m_cmd.ExecuteNonQuery()


                    m_cmd.CommandText = "INSERT INTO " & SchemaName & ".section (nom) VALUES ('" & nomsec1 & "') RETURNING idsection;"
                    m_ds = New System.Data.DataSet
                    m_da.SelectCommand = m_cmd
                    m_da.Fill(m_ds)
                    idsec = m_ds.Tables(0).Rows(0).Item(0)

                    '***********************************************************************************************************************************
                Else
                    m_cmd.CommandText = "INSERT INTO " & SchemaName & ".section (nom) VALUES ('" & nomsec1 & "') RETURNING idsection;"
                    m_ds = New System.Data.DataSet
                    m_da.SelectCommand = m_cmd
                    m_da.Fill(m_ds)
                    idsec = m_ds.Tables(0).Rows(0).Item(0)

                End If
            End If

            For j = 0 To SURF.EstAssocieA.Count - 1

                If SURF.EstAssocieA(j).RefSCD._ID = "SUBDSECT_SECTION" Then

                    m_cmd.CommandText = "SELECT ptrobj FROM " & nomlot & " WHERE refedigeo='" & SURF.EstAssocieA(j).ListeObj(0).NomLot & SURF.EstAssocieA(j).ListeObj(0).ID_Objet & "';"
                    'm_ds = New System.Data.DataSet
                    'm_da.SelectCommand = m_cmd
                    'm_da.Fill(m_ds)

                    Dim idsubsection As Integer = m_cmd.ExecuteScalar 'm_ds.Tables(0).Rows(0).Item(0)


                    m_cmd.CommandText = "UPDATE " & SchemaName & ".subsection SET ptrsection=" & idsec & " WHERE idsubsection=" & idsubsection & ";"
                    m_cmd.ExecuteNonQuery()


                End If
            Next
        Next
        m_cmd.Dispose()
    End Sub

    Public Sub PopulateSubSection(ByVal la As DictionaryObjetEDIGEO, ByVal nomlot As String)

        For I = 0 To la.count - 1

            m_cmd = New NpgsqlCommand
            m_cmd.Connection = mPostGisCnn

            Dim SURF As New ObjetEDIGEO_SURF
            SURF = CType(la(I), ObjetEDIGEO_SURF)
            Dim idu As Integer = SURF.IndexOfAttribut("IDU_id")
            m_cmd.CommandText = "INSERT INTO " & SchemaName & ".subsection (nom) VALUES ('" & SURF.ValeurAtt(idu) & "') RETURNING idsubsection;"
          
            Dim ptrsubsec As Integer = m_cmd.ExecuteScalar 'm_ds.Tables(0).Rows(0).Item(0)

            m_cmd.CommandText = "INSERT INTO " & nomlot & " (ptrobj,refedigeo) VALUES (" & ptrsubsec & ",'" & SURF.NomLot & SURF.ID_Objet & "');"
            m_cmd.ExecuteNonQuery()

            Dim tabobj As String = ""

            For j = 0 To SURF.EstAssocieA.Count - 1

                If SURF.EstAssocieA(j).RefSCD._ID = "PARCELLE_SUBDSECT" Then

                    tabobj = tabobj & ",'" & SURF.EstAssocieA(j).ListeObj(0).NomLot & SURF.EstAssocieA(j).ListeObj(0).ID_Objet & "'"

                    'm_cmd.CommandText = "SELECT ptrobj FROM " & nomlot & " WHERE refedigeo='" & SURF.EstAssocieA(j).ListeObj(0).NomLot & SURF.EstAssocieA(j).ListeObj(0).ID_Objet & "';"

                    'Dim idparc As Integer = m_cmd.ExecuteScalar

                    'm_cmd.CommandText = "UPDATE " & SchemaName & ".parcelle SET ptrsubsection=" & ptrsubsec & " WHERE idparcelle=" & idparc & ";"
                    'm_cmd.ExecuteNonQuery()

                End If

            Next

            m_cmd.CommandText = "WITH maliste(refedigeo) as (VALUES(" & tabobj.TrimStart(",") & ")), p2 as (SELECT ptrobj FROM " & nomlot & " JOIN maliste USING (refedigeo))" _
                               & "UPDATE " & SchemaName & ".parcelle SET ptrsubsection=" & ptrsubsec & " WHERE idparcelle IN (SELECT ptrobj FROM p2);"
            m_cmd.ExecuteNonQuery()
        Next

    End Sub

    Public Sub PopulateParcelle(ByVal la As DictionaryObjetEDIGEO, ByVal nomlot As String)

        Dim sr As New WkbSerializer
        Dim param As New Npgsql.NpgsqlParameter

        m_cmd = New NpgsqlCommand
        m_cmd.Connection = mPostGisCnn

        param.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea
        param.ParameterName = ":wkb"

        Dim paramname As New Npgsql.NpgsqlParameter
        paramname.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text
        paramname.ParameterName = ":name"

        Dim paramannee As New Npgsql.NpgsqlParameter
        paramannee.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text
        paramannee.ParameterName = ":an"
        paramannee.Value = millesime

        m_cmd.Parameters.Add(param)
        m_cmd.Parameters.Add(paramname)
        m_cmd.Parameters.Add(paramannee)

        mFlog.WriteLine("Intégration parcelles")

        For I = 0 To la.count - 1
            Dim SURF As New ObjetEDIGEO_SURF
            SURF = CType(la(I), ObjetEDIGEO_SURF)

            Dim num As Integer = SURF.IndexOfAttribut("TEX_id")


            Dim PO As New POLYGON
            PO = SURF.Polygone
            '*****************************************************


            Dim idparc As Integer = 0

            m_da = New Npgsql.NpgsqlDataAdapter
            m_ds = New System.Data.DataSet




            '*************************************************
            param.Value = sr.serialize(PO)





            If num = -1 Then
                paramname.Value = ""
            Else
                paramname.Value = Format(Val(SURF.ValeurAtt(num)), "0000")
            End If

            m_cmd.CommandText = "INSERT INTO " & SchemaName & ".parcelle (numero,millesime,active,the_geom) VALUES (:name,:an,true,st_geomfromwkb(:wkb," & SRID & ")) RETURNING idparcelle;"

            idparc = m_cmd.ExecuteScalar

            m_cmd.CommandText = "INSERT INTO " & nomlot & " (ptrobj,refedigeo) VALUES (" & idparc & ",'" & SURF.NomLot & SURF.ID_Objet & "');"
            m_cmd.ExecuteNonQuery()



            Dim P As New ObjetEDIGEO_PCT

            If idparc <> 0 Then

                For k = 0 To SURF.EstAssocieA.Count - 1

                    For m = 0 To SURF.EstAssocieA(k).ListeObj.Count - 1
                        If SURF.EstAssocieA(k).ListeObj(m).Nature = NatureObjetSCD.PCT Then
                            P = SURF.EstAssocieA(k).ListeObj(m)
                            'm_da = New Npgsql.NpgsqlDataAdapter
                            'm_ds = New System.Data.DataSet
                            param.Value = sr.serialize(P.Geom)
                            '*******************************************************************************
                            Select Case SURF.EstAssocieA(k).RefSCD._ID
                                Case "NUMVOIE_PARCELLE"
                                    m_cmd.CommandText = "INSERT INTO " & SchemaName & ".numvoie (ptrparcelle,numero,the_geom) VALUES (" & idparc & ",'" & SURF.EstAssocieA(k).ListeObj(m).ValeurAtt(0).ToString & "', st_geomfromwkb(:wkb," & SRID & "));"
                                Case "SYMBLIM_PARCELLE"
                                    Dim ida = SURF.EstAssocieA(k).ListeObj(m).IndexOfAttribut("SYM_id")
                                    Dim idb = SURF.EstAssocieA(k).ListeObj(m).IndexOfAttribut("ORI_id")
                                    m_cmd.CommandText = "INSERT INTO " & SchemaName & "." & SURF.EstAssocieA(k).RefSCD._ID & "(ptrparcelle,sym_id,ori_id,the_geom) " _
                                        & "VALUES(" & idparc & "," & SURF.EstAssocieA(k).ListeObj(m).ValeurAtt(ida) & "," & SURF.EstAssocieA(k).ListeObj(m).ValeurAtt(idb) & ",st_geomfromwkb(:wkb," & SRID & "))"
                                Case "BORNE_PARCELLE"
                                    m_cmd.CommandText = "INSERT INTO " & SchemaName & "." & SURF.EstAssocieA(k).RefSCD._ID & "(ptrparcelle,the_geom) VALUES(" & idparc & ",st_geomfromwkb(:wkb," & SRID & "))"
                                Case Else
                                    m_cmd.CommandText = "INSERT INTO " & SchemaName & ".textparcelle (ptrparcelle,numero,the_geom) VALUES (" & idparc & ",'" & SURF.ValeurAtt(num) & "', st_geomfromwkb(:wkb," & SRID & "));"
                            End Select

                            m_cmd.ExecuteNonQuery()
                            'm_ds = New System.Data.DataSet
                            'm_da.SelectCommand = m_cmd
                            'm_da.Fill(m_ds)
                        End If


                    Next

                Next

            End If



            m_da.Dispose()
            m_ds.Dispose()

        Next


        m_cmd.Dispose()
    End Sub

    Public Sub PopulateTronFluv(ByVal la As DictionaryObjetEDIGEO, ByVal nomlot As String)
        m_cmd.Dispose()
        m_cmd = New NpgsqlCommand

        m_cmd.Connection = mPostGisCnn

        Dim sr As New Wkb.Serialization.WkbSerializer

        Dim wkb As New NpgsqlParameter
        wkb.ParameterName = "wkb"
        wkb.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea

        Dim nom As New NpgsqlParameter
        nom.ParameterName = "nom"
        nom.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text

        Dim police As New NpgsqlParameter("police", NpgsqlTypes.NpgsqlDbType.Text)
        Dim hauteur As New NpgsqlParameter("hauteur", NpgsqlTypes.NpgsqlDbType.Real)
        Dim angle As New NpgsqlParameter("angle", NpgsqlTypes.NpgsqlDbType.Real)


        m_cmd.Parameters.Add(wkb)
        m_cmd.Parameters.Add(nom)
        m_cmd.Parameters.Add(police)
        m_cmd.Parameters.Add(hauteur)

        m_cmd.Parameters.Add(angle)

        For I = 0 To la.count - 1


            Dim SURF As New ObjetEDIGEO_SURF
            SURF = CType(la(I), ObjetEDIGEO_SURF)



            Dim PO As POLYGON = SURF.Polygone
            wkb.Value = sr.serialize(PO)

            Dim idtronfluv As Integer = -1
            '***********************************************************************************************************************************



            

            

            m_cmd.CommandText = "INSERT INTO " & SchemaName & ".tronfluv (ptrcommune,the_geom) VALUES (" & Idcommune & ",st_geomfromwkb(:wkb," & SRID & ")) RETURNING idtronfluv;"
                   
            idtronfluv = m_cmd.ExecuteScalar

            m_cmd.CommandText = "INSERT INTO " & nomlot & " (ptrobj,refedigeo) VALUES (" & idtronfluv & ",'" & SURF.NomLot & SURF.ID_Objet & "');"
            m_cmd.ExecuteNonQuery()
            


            Dim P As New ObjetEDIGEO_PCT

            For k = 0 To SURF.EstAssocieA.Count - 1

                For m = 0 To SURF.EstAssocieA(k).ListeObj.Count - 1
                    If SURF.EstAssocieA(k).ListeObj(m).Nature = NatureObjetSCD.PCT Then
                        P = SURF.EstAssocieA(k).ListeObj(m)
                        m_da = New Npgsql.NpgsqlDataAdapter
                        m_ds = New System.Data.DataSet
                        wkb.Value = sr.serialize(P.Geom)


                        Dim di3 As Double = Val(P.ValeurAtt(P.IndexOfAttribut("ID_S_ATT_DI3")))
                        Dim di4 As Double = Val(P.ValeurAtt(P.IndexOfAttribut("ID_S_ATT_DI4")))
                        Dim f As String = P.ValeurAtt(P.IndexOfAttribut("ID_S_ATT_FON"))
                        Dim h As Double = Val(P.ValeurAtt(P.IndexOfAttribut("ID_S_ATT_HEI")))
                        Dim a As Double = Math.Atan2(di4, di3) * (180 / Math.PI)

                        Dim ordre As Integer = SURF.OrdreTEX_id(k)


                        nom.Value = SURF.ValeurAtt(k)
                        police.Value = f
                        hauteur.Value = h
                        angle.Value = a
                        '*******************************************************************************




                        m_cmd.CommandText = "INSERT INTO " & SchemaName & ".label (valeur,ptrobj,reftable,ordre,police,hauteur,angle,the_geom) VALUES (:nom," & idtronfluv & ",1," & ordre & ",:police," & h & "," & a & ",st_geomfromwkb(:wkb," & SRID & "));"
                        m_cmd.ExecuteNonQuery()

                    End If
                Next

            Next





        Next
        m_cmd.Dispose()
    End Sub

    Public Sub PopulateZoneCommuni(ByVal la As DictionaryObjetEDIGEO, ByVal nomlot As String)
        m_cmd = New NpgsqlCommand
        m_cmd.Connection = mPostGisCnn

        Dim sr As New Wkb.Serialization.WkbSerializer

        Dim wkb As New NpgsqlParameter
        wkb.ParameterName = "wkb"
        wkb.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea

        Dim nom As New NpgsqlParameter
        nom.ParameterName = "nom"
        nom.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text

        Dim police As New NpgsqlParameter("police", NpgsqlTypes.NpgsqlDbType.Text)
        Dim hauteur As New NpgsqlParameter("hauteur", NpgsqlTypes.NpgsqlDbType.Real)
        Dim angle As New NpgsqlParameter("angle", NpgsqlTypes.NpgsqlDbType.Real)

        m_cmd.Parameters.Clear()
        m_cmd.Parameters.Add(wkb)
        m_cmd.Parameters.Add(nom)
        m_cmd.Parameters.Add(police)
        m_cmd.Parameters.Add(hauteur)
        m_cmd.Parameters.Add(angle)

        For I = 0 To la.count - 1


            Dim SURF As New ObjetEDIGEO_LIN
            SURF = CType(la(I), ObjetEDIGEO_LIN)
            wkb.Value = sr.serialize(SURF.Geom)

            Dim idzonecommuni As Integer = -1
            '***********************************************************************************************************************************


            m_cmd.CommandText = "INSERT INTO " & SchemaName & ".zonecommuni (ptrcommune,the_geom) VALUES (" & Idcommune & ",st_geomfromwkb(:wkb," & SRID & ")) RETURNING idzonecommuni;"
           
            idzonecommuni = m_cmd.ExecuteScalar

            m_cmd.CommandText = "INSERT INTO " & nomlot & " (ptrobj,refedigeo) VALUES (" & idzonecommuni & ",'" & SURF.NomLot & SURF.ID_Objet & "');"
            m_cmd.ExecuteNonQuery()


            Dim P As New ObjetEDIGEO_PCT

            For k = 0 To SURF.EstAssocieA.Count - 1

                For m = 0 To SURF.EstAssocieA(k).ListeObj.Count - 1
                    If SURF.EstAssocieA(k).ListeObj(m).Nature = NatureObjetSCD.PCT Then
                        P = SURF.EstAssocieA(k).ListeObj(m)
                        m_da = New Npgsql.NpgsqlDataAdapter
                        m_ds = New System.Data.DataSet
                        wkb.Value = sr.serialize(P.Geom)

                        Dim di3 As Double = Val(P.ValeurAtt(P.IndexOfAttribut("ID_S_ATT_DI3")))
                        Dim di4 As Double = Val(P.ValeurAtt(P.IndexOfAttribut("ID_S_ATT_DI4")))
                        Dim f As String = P.ValeurAtt(P.IndexOfAttribut("ID_S_ATT_FON"))
                        Dim h As Double = Val(P.ValeurAtt(P.IndexOfAttribut("ID_S_ATT_HEI")))
                        Dim a As Double = Math.Atan2(di4, di3) * (180 / Math.PI)

                        nom.Value = SURF.ValeurAtt(k)
                        police.Value = f
                        hauteur.Value = h
                        angle.Value = a

                        Dim ordre As Integer = SURF.OrdreTEX_id(k)

                        '*******************************************************************************
                        'm_cmd.CommandText = "INSERT INTO " & SchemaName & ".label (valeur,ptrobj,reftable,ordre,police,hauteur,angle,the_geom) VALUES (:nom," & idzonecommuni & ",3," & ordre & ",:police,:hauteur,:angle,st_geomfromwkb(:wkb," & SRID & "));"

                        m_cmd.CommandText = "INSERT INTO " & SchemaName & ".label (valeur,ptrobj,reftable,ordre,police,hauteur,angle,the_geom) VALUES (:nom," & idzonecommuni & ",3," & ordre & ",:police," & hauteur.Value & "," & angle.Value & ",st_geomfromwkb(:wkb," & SRID & "));"
                        m_cmd.ExecuteNonQuery()


                    End If
                Next

            Next

        Next
        m_cmd.Dispose()
    End Sub
    Public Sub PopulateLieuDit(ByVal la As DictionaryObjetEDIGEO, ByVal nomlot As String)
        m_cmd = New NpgsqlCommand
        m_cmd.Connection = mPostGisCnn

        Dim sr As New Wkb.Serialization.WkbSerializer

        Dim wkb As New NpgsqlParameter
        wkb.ParameterName = "wkb"
        wkb.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea
        Dim nom As New NpgsqlParameter
        nom.ParameterName = "nom"
        nom.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text

        Dim police As New NpgsqlParameter("police", NpgsqlTypes.NpgsqlDbType.Text)
        Dim hauteur As New NpgsqlParameter("hauteur", NpgsqlTypes.NpgsqlDbType.Real)
        Dim angle As New NpgsqlParameter("angle", NpgsqlTypes.NpgsqlDbType.Real)

        m_cmd.Parameters.Add(wkb)
        m_cmd.Parameters.Add(nom)
        m_cmd.Parameters.Add(police)
        m_cmd.Parameters.Add(hauteur)
        m_cmd.Parameters.Add(angle)



        For I = 0 To la.count - 1

            Dim SURF As New ObjetEDIGEO_SURF
            SURF = CType(la(I), ObjetEDIGEO_SURF)

            Dim PO As New POLYGON
            PO = SURF.Polygone
            wkb.Value = sr.serialize(PO)


            m_cmd.CommandText = "INSERT INTO " & SchemaName & ".lieudit (the_geom) VALUES (ST_GeomFromWKB(:wkb," & SRID & ")) RETURNING idlieudit;"
            'm_ds = New System.Data.DataSet
            'm_da.SelectCommand = m_cmd
            'm_da.Fill(m_ds)
            Dim idlieudit As Integer = m_cmd.ExecuteScalar 'm_ds.Tables(0).Rows(0).Item(0)

            m_cmd.CommandText = "INSERT INTO " & nomlot & " (ptrobj,refedigeo) VALUES (" & idlieudit & ",'" & SURF.NomLot & SURF.ID_Objet & "');"
            m_cmd.ExecuteNonQuery()

            Dim P As New ObjetEDIGEO_PCT

            For k = 0 To SURF.EstAssocieA.Count - 1

                For m = 0 To SURF.EstAssocieA(k).ListeObj.Count - 1
                    If SURF.EstAssocieA(k).ListeObj(m).Nature = NatureObjetSCD.PCT Then
                        P = SURF.EstAssocieA(k).ListeObj(m)
                        m_da = New Npgsql.NpgsqlDataAdapter
                        m_ds = New System.Data.DataSet
                        wkb.Value = sr.serialize(P.Geom)

                        Dim di3 As Double = P.ValeurAtt(P.IndexOfAttribut("ID_S_ATT_DI3"))
                        Dim di4 As Double = P.ValeurAtt(P.IndexOfAttribut("ID_S_ATT_DI4"))
                        Dim f As String = P.ValeurAtt(P.IndexOfAttribut("ID_S_ATT_FON"))
                        Dim h As Double = P.ValeurAtt(P.IndexOfAttribut("ID_S_ATT_HEI"))
                        Dim a As Double = Math.Atan2(di4, di3) * (180 / Math.PI)

                        nom.Value = SURF.ValeurAtt(k)
                        police.Value = f
                        hauteur.Value = h
                        angle.Value = a

                        nom.Value = SURF.ValeurAtt(k)
                        '*******************************************************************************
                        'm_cmd.CommandText = "INSERT INTO " & SchemaName & ".label (valeur,ptrobj,reftable,ordre,police,hauteur,angle,the_geom) VALUES (:nom," & idlieudit & ",4," & k & ",:police,:hauteur,:angle,st_geomfromwkb(:wkb," & SRID & "));"
                        'm_cmd.ExecuteNonQuery()

                        m_cmd.CommandText = "INSERT INTO " & SchemaName & ".label (valeur,ptrobj,reftable,ordre,police,hauteur,angle,the_geom) VALUES (:nom," & idlieudit & ",4," & k & ",:police, " & hauteur.Value & "," & angle.Value & ",st_geomfromwkb(:wkb," & SRID & "));"
                        m_cmd.ExecuteNonQuery()

                    End If
                Next
            Next
        Next
        m_cmd.Dispose()
    End Sub

    Public Sub PopulateTsurf(ByVal la As DictionaryObjetEDIGEO, ByVal nomlot As String)

        m_cmd = New NpgsqlCommand
        m_cmd.Connection = mPostGisCnn

        Dim sr As New Wkb.Serialization.WkbSerializer

        Dim wkb As New NpgsqlParameter
        wkb.ParameterName = "wkb"
        wkb.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea

        Dim symb As New NpgsqlParameter
        symb.ParameterName = "symb"
        symb.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer

        Dim nom As New NpgsqlParameter
        nom.ParameterName = "nom"
        nom.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text
        'm_cmd.Parameters.Clear()
        m_cmd.Parameters.Add(wkb)
        m_cmd.Parameters.Add(symb)
        m_cmd.Parameters.Add(nom)

        For I = 0 To la.count - 1

            'Dim SURF As New ObjetEDIGEO_SURF
            'SURF = CType(la(I), ObjetEDIGEO_SURF)

            Dim SURF As New ObjetEDIGEO_SURF
            SURF = CType(la(I), ObjetEDIGEO_SURF)

            wkb.Value = sr.serialize(SURF.Polygone)

            Dim d As Integer = SURF.IndexOfAttribut("SYM_id")
            Dim n As Integer = SURF.IndexOfAttribut("TEX_id")

            If d = -1 Then
                symb.Value = -1
            Else
                symb.Value = SURF.ValeurAtt(d)
            End If

            If n = -1 Then
                nom.Value = ""
            Else
                nom.Value = SURF.ValeurAtt(n)
            End If

            m_cmd.CommandText = "INSERT INTO " & SchemaName & ".tsurf (the_geom,ptrcommune,valeur,symbol) VALUES (ST_GeomFromWKB(:wkb," & SRID & ")," & Idcommune & ",:nom,:symb) RETURNING idtsurf;"
            m_ds = New System.Data.DataSet
            m_da.SelectCommand = m_cmd
            m_da.Fill(m_ds)
            Dim idtopoline As Integer = m_ds.Tables(0).Rows(0).Item(0)

            m_cmd.CommandText = "INSERT INTO " & nomlot & " (ptrobj,refedigeo) VALUES (" & idtopoline & ",'" & SURF.NomLot & SURF.ID_Objet & "');"
            m_cmd.ExecuteNonQuery()

        Next

        m_cmd.Dispose()

    End Sub
    Public Sub DeleteTableTempEdigeo(ByVal nomlot As String)
        nomlot = nomlot.ToLower
        m_cmd = New NpgsqlCommand
        m_cmd.Connection = mPostGisCnn
        m_cmd.CommandText = "DROP TABLE IF EXISTS " & nomlot & ";"
        m_cmd.ExecuteNonQuery()
        m_cmd.Dispose()
    End Sub
End Class
