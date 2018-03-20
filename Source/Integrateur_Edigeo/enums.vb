Public Enum NatureRel
    ''' <summary>
    ''' Est composé de
    ''' </summary>
    ''' <remarks></remarks>
    ICO
    ''' <summary>
    ''' est représenté par
    ''' </summary>
    ''' <remarks></remarks>
    IDB
    ''' <summary>
    ''' Est representé +/- par
    ''' </summary>
    ''' <remarks></remarks>
    IDR
    ''' <summary>
    ''' a pour noeud initial
    ''' </summary>
    ''' <remarks></remarks>
    IND
    ''' <summary>
    ''' a pour noeud final
    ''' </summary>
    ''' <remarks></remarks>
    FND
    ''' <summary>
    ''' a pour face gauche
    ''' </summary>
    ''' <remarks></remarks>
    LPO
    ''' <summary>
    ''' a pour face droite
    ''' </summary>
    ''' <remarks></remarks>
    RPO
    ''' <summary>
    ''' est inclus dans
    ''' </summary>
    ''' <remarks></remarks>
    ILI
    ''' <summary>
    ''' appartient à 
    ''' </summary>
    ''' <remarks></remarks>
    BET
End Enum
Public Enum TypeNoeud
    Initial = 1
    Final = 2
End Enum
Public Enum TypeArc
    Poly = 1
    Arc = 2
    Courbe = 3
End Enum
Public Enum NatureObjetSCD
    ''' <summary>
    ''' Objet complexe
    ''' </summary>
    ''' <remarks></remarks>
    CPX
    ''' <summary>
    ''' Objet simple ponctuel
    ''' </summary>
    ''' <remarks></remarks>
    PCT
    ''' <summary>
    ''' Objet simple linéaire
    ''' </summary>
    ''' <remarks></remarks>
    LIN
    ''' <summary>
    ''' Objet simple surfacique
    ''' </summary>
    ''' <remarks></remarks>
    ARE
End Enum
Public Enum NaturePrimitive
    NOD = 1
    ARC = 2
    FAC = 3
End Enum

Public Enum ErreurLectureEDIGEO
    ''' <summary>
    ''' pas d'erreur
    ''' </summary>
    ''' <remarks></remarks>
    NON = 1 'pas d'erreur
    ''' <summary>
    ''' pas de début
    ''' </summary>
    ''' <remarks></remarks>
    BOM = 2 'pas de début
    ''' <summary>
    ''' manque jeu de caractère
    ''' </summary>
    ''' <remarks></remarks>
    CSE = 3
    ''' <summary>
    ''' pas de fin
    ''' </summary>
    ''' <remarks></remarks>
    EOM = 4
    ''' <summary>
    ''' Longueur inf à 8
    ''' </summary>
    ''' <remarks></remarks>
    LEN8 = 5
    ''' <summary>
    ''' longueur data incohérente
    ''' </summary>
    ''' <remarks></remarks>
    LEND = 6
    ''' <summary>
    ''' pas de séparateur :
    ''' </summary>
    ''' <remarks></remarks>
    PT2 = 7
    ''' <summary>
    ''' ligne vide
    ''' </summary>
    ''' <remarks></remarks>
    VIDE = 8
    ''' <summary>
    ''' Pas de descripteur de lot
    ''' </summary>
    ''' <remarks></remarks>
    GTL = 9
    ''' <summary>
    ''' Pas de fichier dictionnaire
    ''' </summary>
    ''' <remarks></remarks>
    NODIC = 10
    ''' <summary>
    ''' pas de fichier SCD
    ''' </summary>
    ''' <remarks></remarks>
    NOSCD = 11
    ''' <summary>
    ''' Manque un fichier vecteur
    ''' </summary>
    ''' <remarks></remarks>
    NOVEC = 12
    ''' <summary>
    ''' Mauvaise structure Descripteur Référence
    ''' </summary>
    ''' <remarks></remarks>
    DESC4 = 13

End Enum
Public Enum ErreurStructure
    ''' <summary>
    ''' pas d'erreur
    ''' </summary>
    ''' <remarks></remarks>
    NONE = 0
    ''' <summary>
    ''' Erreur structure obj dans dictionnaire
    ''' </summary>
    ''' <remarks></remarks>
    DICOBJ = 1
    ''' <summary>
    ''' Erreur structure attribut dans dictionnaire
    ''' </summary>
    ''' <remarks></remarks>
    DICATT = 2
    ''' <summary>
    ''' Erreur structure relation dans dictionnaire
    ''' </summary>
    ''' <remarks></remarks>
    DICREL = 3
    ''' <summary>
    ''' Erreur structure objet dans SCD
    ''' </summary>
    ''' <remarks></remarks>
    SCDOBJ = 4
    ''' <summary>
    ''' Erreur structure primitive dans SCD
    ''' </summary>
    ''' <remarks></remarks>
    SCDPRIM = 5
    ''' <summary>
    ''' Erreur structure attribut dans SCD
    ''' </summary>
    ''' <remarks></remarks>
    SCDATT = 6
    ''' <summary>
    ''' Erreur structure relation sémantique dans SCD
    ''' </summary>
    ''' <remarks></remarks>
    SCDRELSEM = 7
    ''' <summary>
    ''' Erreur structure realtion de construction dans SCD
    ''' </summary>
    ''' <remarks></remarks>
    SCDRELCONSTR = 8
    ''' <summary>
    ''' Erreur structure ARC VEC
    ''' </summary>
    ''' <remarks></remarks>
    VECARC = 9
    ''' <summary>
    ''' Erreur structure noeud VEC
    ''' </summary>
    ''' <remarks></remarks>
    VECNOEUD = 10
    ''' <summary>
    ''' Erreur structure FACE VEC
    ''' </summary>
    ''' <remarks></remarks>
    VECFACE = 11
    ''' <summary>
    ''' Erreur structure Objet VEC
    ''' </summary>
    ''' <remarks></remarks>
    VECOBJ = 12
    ''' <summary>
    ''' Erreur structure relation construction VEC
    ''' </summary>
    ''' <remarks></remarks>

    VECRELATION = 13
End Enum

Public Enum PenStyle As Integer
    PS_SOLID = 0
    'The pen is solid.
    PS_DASH = 1
    'The pen is dashed.
    PS_DOT = 2
    'The pen is dotted.
    PS_DASHDOT = 3
    'The pen has alternating dashes and dots.
    PS_DASHDOTDOT = 4
    'The pen has alternating dashes and double dots.
    PS_NULL = 5
    'The pen is invisible.
    PS_INSIDEFRAME = 6
    ' Normally when the edge is drawn, it’s centred on the outer edge meaning that half the width of the pen is drawn
    ' outside the shape’s edge, half is inside the shape’s edge. When PS_INSIDEFRAME is specified the edge is drawn
    'completely inside the outer edge of the shape.
    PS_USERSTYLE = 7
    PS_ALTERNATE = 8
    PS_STYLE_MASK = &HF

    PS_ENDCAP_ROUND = &H0
    PS_ENDCAP_SQUARE = &H100
    PS_ENDCAP_FLAT = &H200
    PS_ENDCAP_MASK = &HF00

    PS_JOIN_ROUND = &H0
    PS_JOIN_BEVEL = &H1000
    PS_JOIN_MITER = &H2000
    PS_JOIN_MASK = &HF000

    PS_COSMETIC = &H0
    PS_GEOMETRIC = &H10000
    PS_TYPE_MASK = &HF0000
End Enum
Public Enum GDIAlignement
    left = 0
    right = 1
    center = 2
    centercenter = 3
End Enum
Public Enum BrushType
    solid32 = 0
    solidplus = 1
    texture = 2
End Enum
Public Enum PaperFormat
    A0 = 0
    A1 = 1
    A2 = 2
    A3 = 3
    A4 = 4
    A5 = 5
End Enum
Public Enum detailtopopoint
    Point_geodesique_borne = 1
    Point_geodesique_non_borne = 2
    Point_de_canevas_d_ensemble_borne = 3

    Point_de_canevas_d_ensemble_non_borne = 4

    Point_de_polygonation_borne = 5

    Point_de_polygonation_repere = 6

    Repere_NGF = 7

    Borne_du_NGF = 8

    Nivellement_MRL = 9

    Autre_repere_de_nivellement = 10

    Borne_limite_de_commune = 11

    Calvaire = 12

    Fleche_de_cours_d_eau = 30

    Mur_mitoyen = 39
    Mur_non_mitoyen = 40
    Fosse_mitoyen = 41
    Fosse_non_mitoyen = 42
    Cloture_mitoyenne = 43
    Cloture_non_mitoyenne = 44
    Haie_mitoyenne = 45
    Haie_non_mitoyenne = 46
    Halte = 47
    Arret = 48
    Station = 49
    Pylone = 50
    Puit = 63
End Enum
Public Enum detailtopo

    Symbole_d_eglise = 14

    Symbole_de_mosquee = 15

    Symbole_de_synagogue = 16

    Limite_d_Etat = 17

    Limite_de_Departement = 18



    Chemin = 21



    Trottoirs_sentier = 23

    Gazoduc_ou_oleoduc = 24

    Aqueduc = 25

    Telepherique = 26

    Ligne_de_transport_de_force = 27

    Rail_de_chemin_de_fer = 29


    Fleche_de_rattachement_d_un_numero_de_parcelle_ou_de_la_reference_d_un_batiment_sur_domaine_non_cadastre = 31


    Petits_ruisseaux_et_terrains_de_sport = 63

    Parking_terrasse_et_surplomb = 64

End Enum

Public Enum detailtoposurf
    Limites_ne_formant_pas_parcelle = 32

    Parapet_de_pont_ou_aqueduc = 33

    Etang_lac_piscine = 34

    Tunnel = 37
    piscine = 65
    Cimetiere = 51
    Cimetiere_israelite = 52
    Cimetiere_musulman = 53
End Enum

Public Enum CompositeCode
    Point_texte = 0
    PointBitmap_texte = 1
    PointPolygone_texte = 2
    PointEllipse_texte = 3
    PointPolyline_texte = 4
End Enum