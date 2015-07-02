Imports System.Text
Imports VIETBAIT.DICOM.BASE
Imports VIETBAIT.DICOMHelper


Public Class Command
    Public Const LOW As UInt16 = 2
    Public Const MEDIUM As UInt16 = 0
    Public Const HIGH As UInt16 = 1


    Dim CommandGroupLengthVal As ULong
    Dim AffectedSOPClassUIDVal As String
    Dim RequestedSOPClassUIDVal As String
    Dim CommandFieldVal As UShort
    Dim MessageIDVal As UShort
    Dim MessageIDBeingRespondedToVal As UShort
    Dim MoveDestinationVal As String
    Dim PriorityVal As UShort
    Dim DataSetTypeVal As UShort
    Dim StatusVal As UShort
    Dim AffectedSOPInstanceUIDVal As String
    Dim RequestedSOPInstanceUIDVal As String

    Public Property CommandGroupLength() As ULong
        Get
            Return CommandGroupLengthVal
        End Get
        Set (ByVal value As ULong)
            CommandGroupLengthVal = value
        End Set
    End Property

    Public Property AffectedSOPClassUID() As String
        Get
            Return AffectedSOPClassUIDVal
        End Get
        Set (ByVal value As String)
            AffectedSOPClassUIDVal = value
        End Set
    End Property

    Public Property RequestedSOPClassUID() As String
        Get
            Return RequestedSOPClassUIDVal
        End Get
        Set (ByVal value As String)
            RequestedSOPClassUIDVal = value
        End Set
    End Property

    Public Property CommandField() As UShort
        Get
            Return CommandFieldVal
        End Get
        Set (ByVal value As UShort)
            CommandFieldVal = value
        End Set
    End Property

    Public Property MessageID() As UShort
        Get
            Return MessageIDVal
        End Get
        Set (ByVal value As UShort)
            MessageIDVal = value
        End Set
    End Property

    Public Property MessageIDBeingRespondedTo() As UShort
        Get
            Return MessageIDBeingRespondedToVal
        End Get
        Set (ByVal value As UShort)
            MessageIDBeingRespondedToVal = value
        End Set
    End Property

    Property MoveDestination() As String
        Get
            Return MoveDestinationVal
        End Get
        Set (ByVal value As String)
            MoveDestinationVal = value
        End Set
    End Property

    Property Priority() As UShort
        Get
            Return PriorityVal
        End Get
        Set (ByVal value As UShort)
            PriorityVal = value
        End Set
    End Property

    Property DataSetType() As UShort
        Get
            Return DataSetTypeVal
        End Get
        Set (ByVal value As UShort)
            DataSetTypeVal = value
        End Set
    End Property

    Public Property Status() As UShort
        Get
            Return StatusVal
        End Get
        Set (ByVal value As UShort)
            StatusVal = value
        End Set
    End Property

    Public Property AffectedSOPInstanceUID() As String
        Get
            Return AffectedSOPInstanceUIDVal
        End Get
        Set (ByVal value As String)
            AffectedSOPInstanceUIDVal = value
        End Set
    End Property

    Public Property RequestedSOPInstanceUID() As String
        Get
            Return RequestedSOPInstanceUIDVal
        End Get
        Set (ByVal value As String)
            RequestedSOPInstanceUIDVal = value
        End Set
    End Property

    Sub Parse (ByVal ParsedPDV As PDVItem)
        Dim de As DataElement
        For Each de In ParsedPDV.DataElements
            Select Case de.DataTag
                Case DICOMTag.CommandGroupLengthTag
                    MyClass.CommandGroupLength = BitConverter.ToUInt32 (de.DataValue, 0)
                Case DICOMTag.RequestedSOPClassUIDTag
                    MyClass.RequestedSOPClassUID = Encoding.ASCII.GetString(de.DataValue)
                Case DICOMTag.AffectedSOPClassUIDTag
                    MyClass.AffectedSOPClassUID = Encoding.ASCII.GetString(de.DataValue)
                Case DICOMTag.CommandFieldTag
                    MyClass.CommandField = BitConverter.ToUInt16(de.DataValue, 0)
                Case DICOMTag.MessageIDTag
                    MyClass.MessageID = BitConverter.ToUInt16(de.DataValue, 0)
                Case DICOMTag.MessageIDBeingRespondedToTag
                    MyClass.MessageIDBeingRespondedTo = BitConverter.ToUInt16(de.DataValue, 0)
                Case DICOMTag.PriorityTag
                    MyClass.Priority = BitConverter.ToUInt16(de.DataValue, 0)
                Case DICOMTag.DataSetTypeTag
                    MyClass.DataSetType = BitConverter.ToUInt16(de.DataValue, 0)
                Case DICOMTag.StatusTag
                    MyClass.Status = BitConverter.ToUInt16(de.DataValue, 0)
                Case DICOMTag.AffectedSOPInstanceUIDTag
                    MyClass.AffectedSOPInstanceUID = Encoding.ASCII.GetString(de.DataValue)
            End Select
        Next

    End Sub
End Class