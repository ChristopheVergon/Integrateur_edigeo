Public Class GeoRegionArgs
    Inherits System.EventArgs

    Private mGeoRegion As GEO_Region
    Public ReadOnly Property GeoRegion() As GEO_Region
        Get
            Return mGeoRegion
        End Get
    End Property
    Public Sub New(ByVal geoRegion As GEO_Region)
        mGeoRegion = CType(geoRegion.Clone(), GEO_Region)
    End Sub

End Class
