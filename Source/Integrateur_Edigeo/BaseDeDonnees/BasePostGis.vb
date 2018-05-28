Imports Npgsql
Imports Wkb.Serialization
Public Class BasePostGis

    Protected mNomBase As String
    Public Property NomBase() As String
        Get
            Return mNomBase
        End Get
        Set(ByVal value As String)
            mNomBase = value
        End Set
    End Property

    Protected mConnectionStringB As NpgsqlConnectionStringBuilder
    Public ReadOnly Property ConnectionStringB() As NpgsqlConnectionStringBuilder
        Get
            Return mConnectionStringB
        End Get

    End Property


    Protected mPostGisCnn As NpgsqlConnection
    Public ReadOnly Property PostGisCnn() As NpgsqlConnection
        Get
            Return mPostGisCnn
        End Get

    End Property
    Protected m_cmd As NpgsqlCommand
    Public ReadOnly Property cmd() As NpgsqlCommand
        Get
            Return m_cmd
        End Get

    End Property


    Protected m_da As Npgsql.NpgsqlDataAdapter
    Public ReadOnly Property da() As NpgsqlDataAdapter
        Get
            Return m_da
        End Get

    End Property

    Protected m_ds As System.Data.DataSet

    Public Property ds() As System.Data.DataSet
        Get
            Return m_ds
        End Get
        Set(ByVal value As System.Data.DataSet)
            m_ds = value
        End Set
    End Property


    Protected mBaseExiste As Boolean
    Protected mConnDirect As Boolean = False
    Protected mConnFailed As Boolean = False
    Public ReadOnly Property COnnFailed() As Boolean
        Get
            Return mConnFailed
        End Get
    End Property
    Public Sub New(ByVal databasename As String, ByVal cnn As NpgsqlConnectionStringBuilder, ByVal NomSchema As String, Optional ByVal ConDirect As Boolean = False)
        mConnDirect = ConDirect

        mNomBase = databasename
        mConnectionStringB = cnn

        If mConnDirect Then

            ConnectionStringB.Database = mNomBase
            mPostGisCnn = New Npgsql.NpgsqlConnection(ConnectionStringB.ConnectionString)

            Try
                mPostGisCnn.Open()
            Catch ex As Exception
                MsgBox("Connection à la base " & mNomBase & " impossible", MsgBoxStyle.Critical)
                mConnFailed = True
            End Try
            cnngen = mPostGisCnn
            Exit Sub

        End If

        mConnectionStringB.Database = "postgres"
        mPostGisCnn = New NpgsqlConnection
        mPostGisCnn.ConnectionString = mConnectionStringB.ConnectionString

        Try
            mPostGisCnn.Open()
        Catch ex As Exception
            MsgBox("Connection impossible")
            mConnFailed = True
            Exit Sub
        End Try

        If Not Database_Exist() Then
            CreateEmptyDataBase(NomSchema)
            mBaseExiste = False
        Else
            mBaseExiste = True
        End If


        mPostGisCnn.Close()
        ConnectionStringB.Database = mNomBase
        mPostGisCnn = New Npgsql.NpgsqlConnection(ConnectionStringB.ConnectionString)

        Try
            mPostGisCnn.Open()
        Catch ex As Exception
            MsgBox("Connection à la base " & mNomBase & " impossible", MsgBoxStyle.Critical)
            mConnFailed = True
        End Try

    End Sub

    Protected Sub CreateEmptyDataBase(ByVal NomSchema As String)
        'to do

        m_cmd = New NpgsqlCommand
        m_cmd.Connection = mPostGisCnn
        'm_cmd.CommandText = "CREATE DATABASE " & databasename & " TEMPLATE=template_postgis_20;"
        m_cmd.CommandText = "CREATE DATABASE " & databasename & ";"
        m_cmd.ExecuteNonQuery()

        '****************test****************
        mPostGisCnn.Close()
        ConnectionStringB.Database = "" & databasename & ""
        mPostGisCnn = New Npgsql.NpgsqlConnection(ConnectionStringB.ConnectionString)
        mPostGisCnn.Open()
        m_cmd.Connection = mPostGisCnn

        'm_cmd.CommandText = "CREATE EXTENTION postgis;"
        'm_cmd.ExecuteNonQuery()

        m_cmd.CommandText = "CREATE EXTENSION postgis;"
        m_cmd.ExecuteNonQuery()
        m_cmd.CommandText = "CREATE EXTENSION postgis_topology;"
        m_cmd.ExecuteNonQuery()
        '***********************************


        m_cmd.CommandText = "ALTER DATABASE " & databasename & " SET search_path=""$user"", public, topology;"
        m_cmd.ExecuteNonQuery()

        If NomSchema <> "public" Then
            m_cmd.CommandText = "CREATE SCHEMA " & NomSchema & ";"
            m_cmd.ExecuteNonQuery()
            'm_cmd.CommandText = "CREATE SCHEMA napoleonien;"
            'm_cmd.ExecuteNonQuery()
        End If

        mPostGisCnn.Close()
        m_cmd.Dispose()
    End Sub


    Protected Function Database_Exist() As Boolean

        m_cmd = New Npgsql.NpgsqlCommand
        m_cmd.CommandText = "SELECT COUNT(*) FROM pg_catalog.pg_database WHERE datname = :database_name"
        m_cmd.Connection = mPostGisCnn
        m_cmd.Parameters.Add(New Npgsql.NpgsqlParameter(":database_name", mNomBase))
        Return m_cmd.ExecuteScalar > 0
        m_cmd.Dispose()

    End Function

    Protected Function schema_exists(schemaname As String) As Boolean
        Dim ds As New DataSet

        m_cmd = New NpgsqlCommand
        m_cmd.Connection = mPostGisCnn
        m_cmd.CommandText = "SELECT schema_name FROM information_schema.schemata WHERE schema_name='" & schemaname & "';"
        m_da = New NpgsqlDataAdapter
        m_da.SelectCommand = m_cmd
        m_da.Fill(ds)

        m_da.Dispose()
        m_cmd.Dispose()

        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub Create_schema(schemaname As String)

        If schemaname <> "public" Then
            m_cmd = New NpgsqlCommand
            m_cmd.Connection = mPostGisCnn
            m_cmd.CommandText = "CREATE SCHEMA '" & schemaname & "';"
            m_cmd.ExecuteNonQuery()
        End If

    End Sub
    Protected Function table_exists(nomschema As String, nomtable As String) As Boolean
        Dim ds As New DataSet

        m_cmd = New NpgsqlCommand
        m_cmd.Connection = mPostGisCnn
        m_cmd.CommandText = "select table_name from information_schema.tables WHERE table_schema=:p AND table_name=:p1;"

        Dim p As New NpgsqlParameter("p", nomschema)
        m_cmd.Parameters.Add(p)

        Dim p1 As New NpgsqlParameter("p1", nomtable)
        m_cmd.Parameters.Add(p1)

        m_da = New NpgsqlDataAdapter
        m_da.SelectCommand = m_cmd


        m_da.Fill(ds)

        m_da.Dispose()
        m_cmd.Dispose()

        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function Intersection(s1 As ObjetEDIGEO_SURF, s2 As ObjetEDIGEO_SURF) As Integer
        Dim sr As New WkbSerializer


        Dim cmd1 As New NpgsqlCommand
        cmd1.Connection = mPostGisCnn

        Dim wkb1 As New Npgsql.NpgsqlParameter
        wkb1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea
        wkb1.ParameterName = ":wkb1"

        Dim wkb2 As New Npgsql.NpgsqlParameter
        wkb2.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea
        wkb2.ParameterName = ":wkb2"

        cmd1.Parameters.Add(wkb1)
        cmd1.Parameters.Add(wkb2)

        Dim P1 As New POLYGON
        P1 = s1.Polygone

        Dim P2 As New POLYGON
        P2 = s2.Polygone

        wkb1.Value = sr.serialize(P1)
        wkb2.Value = sr.serialize(P2)

        cmd1.CommandText = "SELECT st_intersects(st_geomfromwkb(:wkb1," & SRID & "),st_geomfromwkb(:wkb2," & SRID & "));"

        If cmd1.ExecuteScalar Then
            Return 1
        Else
            Return 0
        End If
        cmd1.Dispose()
    End Function
End Class
