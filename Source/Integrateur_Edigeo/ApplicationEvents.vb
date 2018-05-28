Imports System.Threading
Imports System.Globalization

Namespace My
    ' Les événements suivants sont disponibles pour MyApplication :
    ' 
    ' Startup : déclenché au démarrage de l'application avant la création du formulaire de démarrage.
    ' Shutdown : déclengSystem.Globalization.CultureInfo.DefaultThreadCurrentCulture = cché après la fermeture de tous les formulaires de l'application. Cet événement n'est pas déclenché si l'application se termine de façon anormale.
    ' UnhandledException : déclenché si l'application rencontre une exception non gérée.
    ' StartupNextInstance : déclenché lors du lancement d'une application à instance unique et si cette application est déjà active. 
    ' NetworkAvailabilityChanged : déclenché lorsque la connexion réseau est connectée ou déconnectée.
    Partial Friend Class MyApplication
        Private Sub Application_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            Dim c As CultureInfo = Application.Culture.Clone()
            c.NumberFormat.NumberDecimalSeparator = "."
            Thread.CurrentThread.CurrentCulture = c
            Thread.CurrentThread.CurrentUICulture = c
            CultureInfo.DefaultThreadCurrentCulture = c
            CultureInfo.DefaultThreadCurrentUICulture = c
        End Sub
    End Class


End Namespace

