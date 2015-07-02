//***********************************************************************************************************
/*!
 * @file		CCondition.cs
 * @brief		FPD status information class.
 * 
 * @author		TJ
 * @date		2012/03/30(Fri) 
 */
//***********************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VietBaIT.DROC
{
    //*******************************************************************************************************
    /*!
     * @class		FPD status information class.
     * @brief		keep FPD status information in enum.
     * 
     * @author		TJ
     * @date		2012/03/30(Fri) 
     */
    //*******************************************************************************************************
    public class CCondition
    {
        #region private fields

        // FPD Status bit (bit16-23)
        private const UInt32 FPDSTATUS_BIT = 0x00FF0000;
        // Shot Req bit (bit0)
        private const UInt32 SHOTREQ_BIT = 0x00000001;
        // MicroSD bit (bit2-3)
        private const UInt32 MICROSD_BIT = 0x0000000C;
        // Shot Mode bit (bit5-6)
        private const UInt32 SHOTMODE_BIT = 0x00000060;
        // EEPROM bit (bit7)
        private const UInt32 EEPROM_BIT = 0x00000080;
        // P-Save bit (bit9)
        private const UInt32 POWERSAVE_BIT = 0x00000200;
        // Port bit (bit30)
        private const UInt32 PORT_BIT = 0x40000000;
        // Pict-Buf No. bit (bit31)
        private const UInt32 PICTBUFNO_BIT = 0x80000000;

        // PWRBD TMP1 bit (bit0)
        private const UInt32 PWRBDTMP1_BIT = 0x00000001;
        // PWRBD TMP2 bit (bit1)
        private const UInt32 PWRBDTMP2_BIT = 0x00000002;
        // DTBD TMP1 bit (bit2)
        private const UInt32 DTBDTMP1_BIT = 0x00000004;
        // DTBD TMP2 bit (bit3)
        private const UInt32 DTBDTMP2_BIT = 0x00000008;

        // TFT ON bit (bit0)
        private const UInt32 TFTON_BIT = 0x00000001;
        // TFT OFF bit (bit1)
        private const UInt32 TFTOFF_BIT = 0x00000002;
        // PD bit (bit4)
        private const UInt32 PD_BIT = 0x00000010;
        #endregion

        #region public fields
        // FPD Status
        public enum FPDSTATUS
        {
            STATE_NULL = 0x00,
            STATE_FPD_INIT = 0x01,
            STATE_LAN_PHY_INIT = 0x04,
            STATE_SLEEP = 0x12,
            STATE_STBY_INIT = 0x20,
            STATE_STBY = 0x22,
            STATE_STBY_TIMEOUT = 0x26,
            STATE_STBY_END = 0x27,
            STATE_CMD_PROC_INIT = 0x28,
            STATE_CMD_PROC = 0x2A,
            STATE_CMD_PROC_END = 0x2F,
            STATE_READY_INIT = 0x30,
            STATE_READY = 0x32,
            STATE_READY_TIMEOUT = 0x36,
            STATE_READY_END = 0x37,
            STATE_MAINTE_INIT = 0x38,
            STATE_MAINTE = 0x3A,
            STATE_MAINTE_END = 0x3F,
            STATE_EXP_INIT = 0x40,
            STATE_EXP_ITG = 0x42,
            STATE_EXP_READ = 0x44,
            STATE_EXP_END = 0x47
        }
        // Shot Req
        public enum SHOTREQ
        {
            None = 0,
            Req = 1
        }
        // MicroSD
        public enum MICROSD
        {
            OK = 0,
            NotImplement = 1
        }
        // Shot Mode
        public enum SHOTMODE
        {
            Single = 0,
            Double = 1
        }
        // EEPROM
        public enum EEPROM
        {
            Normal = 0,
            Error = 1
        }
        // P-Save
        public enum POWERSAVE
        {
            OFF = 0,
            ON = 1
        }
        // Port
        public enum PORT
        {
            NOTBUSY = 0,
            BUSY = 1
        }
        // Temperature
        public enum TEMPERATURE
        {
            Normal = 0,
            Error = 1
        }
        // Voltage
        public enum VOLTAGE
        {
            Normal = 0,
            Error = 1
        }

        // FPD Status
        public FPDSTATUS fpdStatus { get; private set; }
        // Shot Req
        public SHOTREQ shotReq { get; private set; }
        // MicroSD
        public MICROSD microSD { get; private set; }
        // Shot Mode
        public SHOTMODE shotMode { get; private set; }
        // EEPROM
        public EEPROM eeprom { get; private set; }
        // P-Save
        public POWERSAVE powerSave { get; private set; }
        // Port
        public PORT port { get; private set; }
        // Pict-Buf No.
        public UInt32 pictBufNo { get; private set; }

        // PWRBD TMP1
        public TEMPERATURE pwrbdTmp1 { get; private set; }
        // PWRBD TMP2
        public TEMPERATURE pwrbdTmp2 { get; private set; }
        // DTBD TMP1
        public TEMPERATURE dtbdTmp1 { get; private set; }
        // DTBD TMP2
        public TEMPERATURE dtbdTmp2 { get; private set; }

        // TFT ON
        public VOLTAGE tftOn { get; private set; }
        // TFT OFF
        public VOLTAGE tftOff { get; private set; }
        // PD
        public VOLTAGE pd { get; private set; }
        #endregion

        #region constructor
        //***************************************************************************************************
        /*!
         * @brief		Constructor.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[in]   FPD_CONDITION fpdCondition      FPD status information.
         * 
         * @return		none
         */
        //***************************************************************************************************
        public CCondition(CInterfaceLower.FPD_CONDITION fpdCondition)
        {
            // Initialize
            this.fpdStatus = FPDSTATUS.STATE_NULL;
            this.shotReq = SHOTREQ.None;
            this.microSD = MICROSD.OK;
            this.shotMode = SHOTMODE.Single;
            this.eeprom = EEPROM.Normal;
            this.powerSave = POWERSAVE.OFF;
            this.port = PORT.NOTBUSY;
            this.pictBufNo = 0;

            this.pwrbdTmp1 = TEMPERATURE.Normal;
            this.pwrbdTmp2 = TEMPERATURE.Normal;
            this.dtbdTmp1 = TEMPERATURE.Normal;
            this.dtbdTmp2 = TEMPERATURE.Normal;

            this.tftOn = VOLTAGE.Normal;
            this.tftOff = VOLTAGE.Normal;
            this.pd = VOLTAGE.Normal;

            // Convert status information to enum
            try
            {
                // Status
                this.fpdStatus = (FPDSTATUS)Enum.Parse(typeof(FPDSTATUS),
                                    this.GetValidByte(fpdCondition.nSys, FPDSTATUS_BIT).ToString());
                this.shotReq = (SHOTREQ)Enum.Parse(typeof(SHOTREQ),
                                    this.GetValidByte(fpdCondition.nSys, SHOTREQ_BIT).ToString());
                this.microSD = (MICROSD)Enum.Parse(typeof(MICROSD), 
                                    this.GetValidByte(fpdCondition.nSys, MICROSD_BIT).ToString());
                this.shotMode = (SHOTMODE)Enum.Parse(typeof(SHOTMODE), 
                                    this.GetValidByte(fpdCondition.nSys, SHOTMODE_BIT).ToString());
                this.eeprom = (EEPROM)Enum.Parse(typeof(EEPROM),
                                    this.GetValidByte(fpdCondition.nSys, EEPROM_BIT).ToString());
                this.powerSave = (POWERSAVE)Enum.Parse(typeof(POWERSAVE),
                                    this.GetValidByte(fpdCondition.nSys, POWERSAVE_BIT).ToString());
                this.port = (PORT)Enum.Parse(typeof(PORT),
                                    this.GetValidByte(fpdCondition.nSys, PORT_BIT).ToString());
                this.pictBufNo = (UInt32) this.GetValidByte(fpdCondition.nSys, PICTBUFNO_BIT);

                // Temperature
                this.pwrbdTmp1 = (TEMPERATURE)Enum.Parse(typeof(TEMPERATURE),
                                    this.GetValidByte(fpdCondition.nTemp, PWRBDTMP1_BIT).ToString());
                this.pwrbdTmp2 = (TEMPERATURE)Enum.Parse(typeof(TEMPERATURE),
                                    this.GetValidByte(fpdCondition.nTemp, PWRBDTMP2_BIT).ToString());
                this.dtbdTmp1 = (TEMPERATURE)Enum.Parse(typeof(TEMPERATURE),
                                    this.GetValidByte(fpdCondition.nTemp, DTBDTMP1_BIT).ToString());
                this.dtbdTmp2 = (TEMPERATURE)Enum.Parse(typeof(TEMPERATURE),
                                    this.GetValidByte(fpdCondition.nTemp, DTBDTMP2_BIT).ToString());

                // Voltage
                this.tftOn = (VOLTAGE)Enum.Parse(typeof(VOLTAGE),
                                    this.GetValidByte(fpdCondition.nVolt, TFTON_BIT).ToString());
                this.tftOff = (VOLTAGE)Enum.Parse(typeof(VOLTAGE),
                                    this.GetValidByte(fpdCondition.nVolt, TFTOFF_BIT).ToString());
                this.pd = (VOLTAGE)Enum.Parse(typeof(VOLTAGE),
                                    this.GetValidByte(fpdCondition.nVolt, PD_BIT).ToString());
            }
            catch (Exception)
            {
                // Error process (none)
            }
        }
        #endregion

        #region destructor
        //***************************************************************************************************
        /*!
         * @brief		Destructor.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @return		none
         */
        //***************************************************************************************************
        ~CCondition()
        {
        }
        #endregion

        #region private methods
        //***************************************************************************************************
        /*!
         * @brief		Extract bit pattern and convert to enum.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[in]   UInt32 status       Original data.
         * @param[in]   UInt32 validBit     Bit pattern to be extracted.
         * 
         * @return		enum
         */
        //***************************************************************************************************
        private Byte GetValidByte(UInt32 status, UInt32 validBit)
        {
            // Extract bit pattern and convert to enum
            return (Byte)((status & validBit) >> this.SearchLeastSignificantOnBit(validBit));
        }

        //***************************************************************************************************
        /*!
         * @brief		Get least significant bit of on.
         *
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[in]   UInt32 validBit     Bit pattern.
         * 
         * @return		least significant bit of on
         */
        //***************************************************************************************************
        private Int32 SearchLeastSignificantOnBit(UInt32 validBit)
        {
            Int32 count;
            for (count = 0; count < (sizeof(Int32) * 8); count++)
            {
                if (1 == (validBit & 1))
                {
                    break;
                }
                validBit >>= 1;
            }
            return count;
        }
        #endregion
    }
}
