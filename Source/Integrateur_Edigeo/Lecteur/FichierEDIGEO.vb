Public MustInherit Class FichierEDIGEO
   
    Private mFint As FormIntegration
    Public Property Fint() As FormIntegration
        Get
            Return mFint
        End Get
        Set(ByVal value As FormIntegration)
            mFint = value
        End Set
    End Property


    Private mFileName As String
    Public Property FileName() As String
        Get
            Return mFileName
        End Get
        Set(ByVal value As String)
            mFileName = value
        End Set
    End Property

    Private mChemin As String
    Public Property Chemin() As String
        Get
            Return mChemin
        End Get
        Set(ByVal value As String)
            mChemin = value
        End Set
    End Property


    Private mInfo As ID_Nom
    Public Property Info() As ID_Nom
        Get
            Return mInfo
        End Get
        Set(ByVal value As ID_Nom)
            mInfo = value
        End Set
    End Property


    Private mZoneListe As ListeZone
    Public Property ZoneListe() As ListeZone
        Get
            Return mZoneListe
        End Get
        Set(ByVal value As ListeZone)
            mZoneListe = value
        End Set
    End Property


    Public MustOverride Sub InitDictionary()


    Public Function VerifieBOMEOM(ByVal action As String, ByRef Y As Single) As Boolean
        Dim er As String = ""
        Dim i As Integer = 1
        Dim z As New ZoneEdigeo("")

        Dim arg1(1) As Object
        arg1(0) = action
        arg1(1) = Y
        'If Not mmFint.Handle <> 0 Then
        '    Debug.Print("")
        'End If
        mFint.Invoke(mFint.CarreRougeInstance, arg1)

        Y = Y + 15
        arg1(1) = Y

        If mZoneListe Is Nothing Then
            mZoneListe = New ListeZone
        End If
        Dim encode As System.Text.Encoding = System.Text.Encoding.GetEncoding(1252)
        Using sr As New System.IO.StreamReader(mFileName, encode)

            Do While Not sr.EndOfStream
                z = New ZoneEdigeo(sr.ReadLine)
                If z.Erreur = ErreurLectureEDIGEO.NON Then

                    mZoneListe.ListeZ.Add(z)
                Else
                    If z.Erreur <> ErreurLectureEDIGEO.VIDE Then
                        er = er & z.Erreur.ToString & " ligne : " & i
                        Fint.Flog.WriteLine("BOMEOM : " & er)
                    End If
                End If

                i = i + 1
            Loop
            sr.Close()
        End Using

        mFint.Invoke(mFint.CarreVertInstance, arg1)
        If er.Length > 0 Then
            Return False
        Else
            Return True
        End If
        
    End Function

    
End Class
