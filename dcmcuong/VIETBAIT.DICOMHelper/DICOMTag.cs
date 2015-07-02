using System;

namespace VIETBAIT.DICOMHelper
{
    public static class DICOMTag

    {
        public const UInt32 ItemIntroducerTag = 0xFFFEE000U;
        public const UInt32 ItemDelimiterTag = 0xFFFEE00DU;
        public const UInt32 SeqDelimiterTag = 0xFFFEE0DDU;
        public const UInt32 CommandGroupLengthTag = 0x00000000;
        public const UInt32 AffectedSOPClassUIDTag = 0x00000002;
        public const UInt32 RequestedSOPClassUIDTag = 0x00000003;
        public const UInt32 CommandFieldTag = 0x00000100;
        public const UInt32 MessageIDTag = 0x00000110;
        public const UInt32 MessageIDBeingRespondedToTag = 0x00000120;
        public const UInt32 MoveDestinationTag = 0x00000600;
        public const UInt32 PriorityTag = 0x00000700;
        public const UInt32 DataSetTypeTag = 0x00000800;
        public const UInt32 StatusTag = 0x00000900;
        public const UInt32 AffectedSOPInstanceUIDTag = 0x00001000;
        public const UInt32 RequestedSOPInstanceUIDTag = 0x00001001;
        public const UInt32 ActionTypeIDTag = 0x00001008;
        public const UInt32 MoveOriginatorAETTag = 0x00001030;
        public const UInt32 MoveOriginatorMessageIDTag = 0x00001031;

        public const UInt32 ReferencedSOPClassUIDTag = 0x00081150;
public const UInt32 ReferencedSOPInstanceUIDTag = 0x00081155;
        
        #region Printer Tag
        public const UInt32 PrintStatusTag = 0x21100010;
        public const UInt32 PrintStatusInforTag = 0x21100020;
        public const UInt32 FilmSessionGroupLengthTag = 0x20000000;
        public const UInt32 NumberOfCopiesTag = 0x20000010;
        public const UInt32 FilmBoxGroupLengthTag = 0x20100000;
        public const UInt32 ImageDisplayFormatTag = 0x20100010;
        public const UInt32 FilmOrientationTag = 0x20100040;
        public const UInt32 FilmSizeIDTag = 0x20100050;
        public const UInt32 BorderDensityTag = 0x20100100;
        public const UInt32 ConfigurationInforTag = 0x20100150;
        public const UInt32 ReferencedFilmSessionSequenceTag = 0x20100500;
        public const UInt32 ReferencedImageBoxSequenceTag = 0x20100510;
        public const UInt32 ImageBoxPositionTag = 0x20200010;
        public const UInt32 BasicColorImageSequenceTag = 0x20200111;
        public const UInt32 SamplesPerPixelTag = 0x00280002;
        public const UInt32 PhotometricInterpretationTag = 0x00280004;
        public const UInt32 PlanarConfigurationTag = 0x00280006;
        public const UInt32 RowsTag = 0x00280010;
        public const UInt32 ColumnsTag = 0x00280011;
        public const UInt32 PixelAspectRatioTag = 0x00280034;
        public const UInt32 BitsAllocatedTag = 0x00280100;
        public const UInt32 BitsStoredTag = 0x00280101;
        public const UInt32 HighBitTag = 0x00280102;
        public const UInt32 PixelRepresentationTag = 0x00280103;
        public const UInt32 PixelDataTag = 0x7FE00010;

        #endregion


    }
}