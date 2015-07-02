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
        public const UInt16 C_STORE_RQ = 0x1;
        public const UInt16 C_GET_RQ = 0x10;
        public const UInt16 C_FIND_RQ = 0x20;
        public const UInt16 C_MOVE_RQ = 0x21;
        public const UInt16 C_ECHO_RQ = 0x30;
        public const UInt16 N_EVENT_REPORT_RQ = 0x100;
        public const UInt16 N_EVENT_REPORT_RSP = 0x8100;
        public const UInt16 N_GET_RQ = 0x110;
        public const UInt16 N_GET_RSP = 0x8110;
        public const UInt16 N_SET_RQ = 0x120;
        public const UInt16 N_SET_RSP = 0x8120;
        public const UInt16 N_ACTION_RQ = 0x130;
        public const UInt16 N_ACTION_RSP = 0x8130;
        public const UInt16 N_CREATE_RQ = 0x140;
        public const UInt16 N_CREATE_RSP = 0x8140;
        public const UInt16 N_DELETE_RQ = 0x150;
        public const UInt16 N_DELETE_RSP = 0x8150;

        public const UInt16 NoDataSet = 0x0101;
    }
}