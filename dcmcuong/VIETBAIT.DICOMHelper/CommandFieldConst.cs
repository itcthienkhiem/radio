using System;

namespace VIETBAIT.DICOMHelper
{
    public static class CommandFieldConst
    {
        public const UInt16 C_STORE_RSP = 0x8001;
        public const UInt16 C_GET_RSP = 0x8010;
        public const UInt16 C_FIND_RSP = 0x8020;
        public const UInt16 C_MOVE_RSP = 0x8021;

        public const UInt16 C_ECHO_RSP = 0x8030;
        public const UInt16 C_STORE_RQ = 0x0001;
        public const UInt16 C_GET_RQ = 0x0010;
        public const UInt16 C_FIND_RQ = 0x0020;
        public const UInt16 C_MOVE_RQ = 0x0021;
        public const UInt16 C_ECHO_RQ = 0x0030;
        public const UInt16 N_EVENT_REPORT_RQ = 0x0100;
        public const UInt16 N_EVENT_REPORT_RSP = 0x8100;
        public const UInt16 N_GET_RQ = 0x0110;
        public const UInt16 N_GET_RSP = 0x8110;
        public const UInt16 N_SET_RQ = 0x0120;
        public const UInt16 N_SET_RSP = 0x8120;
        public const UInt16 N_ACTION_RQ = 0x0130;
        public const UInt16 N_ACTION_RSP = 0x8130;
        public const UInt16 N_CREATE_RQ = 0x0140;
        public const UInt16 N_CREATE_RSP = 0x8140;
        public const UInt16 N_DELETE_RQ = 0x0150;
        public const UInt16 N_DELETE_RSP = 0x8150;


        public const UInt16 NoDataSet = 0x0101;
        public const UInt16 SuccessStatus = 0x0000;
        public const UInt16 NCREATEWarningStatus1 = 0x0116;
        public const UInt16 NCREATEWarningStatus2 = 0xB600;
        public const UInt16 NCREATEWarningStatus3 = 0x0107;
        public const UInt16 NCREATEFailureStatus1 = 0x0106;
        public const UInt16 NCREATEFailureStatus2 = 0x0110;
        public const UInt16 NCREATEFailureStatus3 = 0x0117;
        public const UInt16 NCREATEFailureStatus4 = 0x0213;

        public const UInt16 NSETWarningStatus1 = 0xB604;
        public const UInt16 NSETWarningStatus2 = 0xB605;
        public const UInt16 NSETWarningStatus3 = 0xB609;
        public const UInt16 NSETWarningStatus4 = 0xB60A;
        public const UInt16 NSETFailureStatus1 = 0xC603;
        public const UInt16 NSETFailureStatus2 = 0xC605;
        public const UInt16 NSETFailureStatus3 = 0xC613;
    }
}