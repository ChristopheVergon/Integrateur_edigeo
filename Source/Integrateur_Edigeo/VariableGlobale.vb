
Module VariableGlobale

    Public typeintegration As Integer = -1
    Public verrou_candidat As New Object
    Public verrou_tampon As New Object
    Public arret_recherche As Boolean = False
    Public finedigeo As Boolean = False

    Public arret_general As Boolean = False
    Public PrefixeLot As String = ""
    Public SRID As Integer = 3942    
    Public UserSchemaName As String    
    Public WithEvents DeviceDessin As Mdc
   
    Public millesime As String

    Public tailletampon As Integer = 40
    Public Tampon As New System.Collections.Generic.List(Of Echange_object)
    Public candidat As Echange_object = Nothing
    Public nbprocposgis As Integer = 10
    Public nbprocedigeo As Integer = 15
    Public nbprocedigeo_gros As Integer = 10
    Public OrderedTHF As THF_Liste
    Public nblottraites As Integer = 0
    Public nblot_a_traiter As Integer = 0
    Public maxpetit As Integer = 10
    Public starttime As Integer



    Public Property DatabaseName() As String
        Get
            Return My.Settings.Nombase
        End Get
        Set(ByVal value As String)
            My.Settings.Nombase = value
        End Set
    End Property

    Public Property SchemaName() As String
        Get
            Return My.Settings.NomSchema
        End Get
        Set(ByVal value As String)
            My.Settings.NomSchema = value
        End Set
    End Property

   
   

    Public Function GetConnectionStringFromSettings() As String
        Dim b As New Npgsql.NpgsqlConnectionStringBuilder
        b.Host = My.Settings.Host
        b.Port = My.Settings.PortTcp
        b.UserName = My.Settings.Utilisateur
        b.Database = My.Settings.Nombase        
        Return b.ConnectionString
    End Function

    Public Function ColorTranslation(ByVal coloint As Integer) As Color
        If Math.Sign(coloint) = -1 Then
            Return Color.FromArgb(coloint)
        Else
            Dim A, R, G, B As Integer
            Dim stcolor As String = System.Convert.ToString(coloint, 16)
            stcolor = stcolor.PadLeft(6, "0"c)
            A = 255
            B = System.Convert.ToInt32(stcolor.Substring(0, 2), 16)
            G = System.Convert.ToInt32(stcolor.Substring(2, 2), 16)
            R = System.Convert.ToInt32(stcolor.Substring(4, 2), 16)
            Return System.Drawing.Color.FromArgb(A, R, G, B)
        End If
    End Function

    Private mconnectionStringB As Npgsql.NpgsqlConnectionStringBuilder
    Public Property connectionstringB() As Npgsql.NpgsqlConnectionStringBuilder
        Get
            Return mconnectionStringB
        End Get
        Set(ByVal value As Npgsql.NpgsqlConnectionStringBuilder)
            mconnectionStringB = value
        End Set
    End Property



    Private mCnnGen As Npgsql.NpgsqlConnection
    Public Property CnnGen() As Npgsql.NpgsqlConnection
        Get
            Return mCnnGen
        End Get
        Set(ByVal value As Npgsql.NpgsqlConnection)
            mCnnGen = value
        End Set
    End Property


    Function GetUserName() As String
        If TypeOf My.User.CurrentPrincipal Is  _
        Security.Principal.WindowsPrincipal Then
            ' The application is using Windows authentication.
            ' The name format is DOMAIN\USERNAME.
            Dim parts() As String = Split(My.User.Name, "\")
            Dim username As String = parts(1).ToLower()
            Return username
        Else
            ' The application is using custom authentication.
            Return My.User.Name
        End If
    End Function


    Public Function GetFullQualifiedTableName(ByVal table As String)
        If table.Contains(".") Then
            If table.StartsWith(".") Then
                Throw New ArgumentException("table commence par un point.")
            End If
            Return table
        End If
        If String.IsNullOrEmpty(SchemaName) Then
            Return table
        End If
        Return SchemaName & "." & table
    End Function

   

   

   
End Module
