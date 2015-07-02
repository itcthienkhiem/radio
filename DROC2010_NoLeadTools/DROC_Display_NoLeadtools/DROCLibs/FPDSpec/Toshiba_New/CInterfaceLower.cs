//***********************************************************************************************************
/*!
 * @file		CInterfaceLower.cs
 * @brief		Lower API interface class.
 * 
 * @author		TJ
 * @date		2012/03/30(Fri) 
 */
//***********************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace VietBaIT.DROC
{
    //*******************************************************************************************************
    /*!
     * @class		Lower API interface class.
     * @brief		Do marshalling if necessary.
     * 
     * @author		TJ
     * @date		2012/03/30(Fri) 
     */
    //*******************************************************************************************************

    public class CInterfaceLower : IDisposable
    {
        #region private fields
        // API DLL file name
        private const String DllName = "TETD_FPD_Controller.dll";
        // Log class handler
        private CLog cLog;
        // GC handle for callback function
        private GCHandle gch;
        #endregion

        #region public fields
        // Delegate for callback function
        public delegate void TETD_CALLBACK_t(Int32 eventID, IntPtr eventData, IntPtr userData);
        // System callback function executed by API
        public TETD_CALLBACK_t sysCallBack = null;
        // User callback function executed by system callback function
        public TETD_CALLBACK_t usrCallBack = null;
        // Number of FPDs connected
        public UInt32 ConnectFPDs { get; private set; }
        // Current FPD number
        public UInt32 ActiveDetector { get; set; }
        #endregion

        #region marshal
        // TETD API error codes
        [Flags]
        public enum E_ERR_TETD_CONTROLLER_ENUM
        {
            TETD_OK,                            // ( 0) Success
            TETD_ERR_STATE,                     // ( 1) State error
            TETD_ERR_PARAMETER,                 // ( 2) Parameter error
            TETD_ERR_CONTROLLER_ERROR,          // ( 3) FPD error
            TETD_ERR_DRIVER_ERROR,              // ( 4) Driver error
            TETD_ERR_COMMUNICATION_ERROR,       // ( 5) Communication error
            TETD_ERR_IMAGE_LIBRARY_ERROR,       // ( 6) Image library error
            TETD_ERR_COMMON_LIBRARY_ERROR,      // ( 7) Common library error
            TETD_ERR_BUSY,                      // ( 8) Busy
            TETD_ERR_DEFECT_LUT,                // ( 9) Defect Lut error
            TETD_ERR_OFFSETALUT_NOT_GANERATION, // (10) OFFSETA LUT not ganaration
            TETD_ERR_FPD_PROPERTY_ERROR,        // (11) FPD serial No. error
            TETD_ERR_LUT_HEADER_ERROR,          // (12) LUT header error
            TETD_ERR_SELF_DIAGNOSIS,            // (13) Self diagnosis error
            TETD_ERR_IMAGE_ID_ERROR,            // (14) Image ID mismatch	
            TETD_ERR_NOT_REGIST_CALLBACK,		// (15) Not Registed CallBack Function.
            TETD_ERR_ALRDY_REGIST_CALLBACK,		// (16) Already Registed Callback Function.
	        TETD_ERR_NOT_CALIBRATION,			// (16) Calibration Not Execute.
	        TETD_ERR_FPD_NOT_SUPPORTED			// (17) This Fpd is not supported.
        }

        // Controller state 
        [Flags]
        public enum TETD_STATE_ENUM
        {
            NotInit = 0,
            Init,
            Standby,
            Work,
            Calibration
        };

        // FPD Types
        [Flags]
        public enum FPDTYPE_ENUM
        {
            FPDTYPE_E9552,                  // E9552
            FPDTYPE_E9547,                  // E9547
            FPDTYPE_E9530                   // E9530
        }

        public const Int32 MAX_Connect = 9;
        public const Int32 SerialNoLen = 32;
        public const Int32 ModelLen    = 5;
        public const Int32 ReserveLen  = 9;
        public const Int32 PathLen = SerialNoLen + ModelLen + ReserveLen;
        
        // FPD information
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct TETD_FPD_INFO
        {
            public UInt32 FpdType;          // FPD Type
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = SerialNoLen + 1)]
            public String SerialNo;         // Serial number
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PathLen + 1)]
            public String Path;             // Path
        }

        // Driver information
        [StructLayout(LayoutKind.Sequential)]
        public struct TETD_DRIVER_INFO
        {
            public UInt32 FpdCount;         // number of FPDs connected
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_Connect)]
            public TETD_FPD_INFO [] xmlData;
        }

        // FPD status information
        [StructLayout(LayoutKind.Sequential)]
        public struct FPD_CONDITION
        {
            public UInt32 nSys;             // E9547_COMM_FPD_COND_SYS_XXX
            public UInt32 nTemp;            // E9547_COMM_FPD_COND_TEMP_XXX
            public UInt32 nVolt;            // E9547_COMM_FPD_COND_VOLT_XXX
            public UInt32 nRsrv1;
            public UInt32 nRsrv2;
            public UInt32 nRsrv3;
            public UInt32 nRsrv4;
        }

        // API calls
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_DLL_Ver(out UInt32 Ver_out);
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_DLL_Init(IntPtr ptr);
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_DLL_Exit();
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_FPD_Register_Callback(TETD_CALLBACK_t fnCallBack, IntPtr UserData_out);
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_FPD_Open(UInt32 FpdNumber_in, out UInt32 ExposureRequestSwitch);
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_FPD_Close();
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static TETD_STATE_ENUM TETD_DLL_Get_State();
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_FPD_Work_State(UInt32 ExposureMode_in);
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_FPD_Standby_State();
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_FPD_Calibration_State();
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_FPD_Exposure_Mode(UInt32 ExposureMode_in);
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_FPD_Exposure(UInt32 Exp_Num_in);
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_FPD_Calibration_Offset(UInt32 Flags_In);
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_FPD_Calibration_Exposure(UInt32 Exp_Num_in);
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_FPD_Calibration_Next(out UInt32 nNumAddImages_out);
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_FPD_Calibration_Abort();
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_FPD_Calibration_Complete();
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_FPD_Set_Correction(UInt32 Flags_in);
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_FPD_Get_Correction(out UInt32 Flags_out);
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_FPD_Get_Condition(out FPD_CONDITION FpdCondition_out);
        [DllImport(DllName, CallingConvention=CallingConvention.Cdecl)]
        private extern static E_ERR_TETD_CONTROLLER_ENUM TETD_FPD_Self_Diagnosis();
        #endregion

        #region constructor
        //***************************************************************************************************
        /*!
         * @brief		Constructor.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[in]   CLog cLog           Log class handler.
         * @param[in]   IntPtr UserData_out User defined data used by call back function.
         * 
         * @return		none
         */
        //***************************************************************************************************
        public CInterfaceLower(CLog cLog, IntPtr UserData_out)
        {
            TETD_DRIVER_INFO FpdData_out;

            // Log class handler
            this.cLog = cLog;

            // Initialize DLL
            this.TetdDllInit(out FpdData_out);
            this.cLog.WriteLine("Found FPDs = " + FpdData_out.FpdCount.ToString());
            this.ConnectFPDs = FpdData_out.FpdCount;

            // System callback function executed by API
            this.sysCallBack = new TETD_CALLBACK_t(this.EventCallback);
            // Callback function must NOT be GC collected
            gch = GCHandle.Alloc(this.sysCallBack, GCHandleType.Normal);
            // Register system callback function
            TETD_FPD_Register_Callback(this.sysCallBack, UserData_out);
        }
        #endregion

        #region destructor
        //**************************************************************************************************
        /*!
         * @brief		Destructor.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @return		none
         */
        //**************************************************************************************************
        ~CInterfaceLower()
        {
            this.TetdDllExit();
            this.cLog = null;

            // free GC handle
            gch.Free();
        }
        #endregion

        #region private methods
        //**************************************************************************************************
        /*!
         * @brief		TETD_DLL_Init() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[out]  TETD_DRIVER_INFO FpdData_out    Driver information
         * 
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //**************************************************************************************************
        private E_ERR_TETD_CONTROLLER_ENUM TetdDllInit(out TETD_DRIVER_INFO FpdData_out)
        {
            TETD_DRIVER_INFO info = new TETD_DRIVER_INFO();

            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(info));

            // Use Marshal.StructureToPtr
            Marshal.StructureToPtr(info, ptr, false);

            this.cLog.WriteLine("TETD_DLL_Init Start");
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_DLL_Init(ptr);
            this.cLog.WriteLine("TETD_DLL_Init End : ret = " + result.ToString());

            // Use Marshal.PtrToStructure
            FpdData_out = (TETD_DRIVER_INFO)Marshal.PtrToStructure(ptr, typeof(TETD_DRIVER_INFO));

            Marshal.FreeHGlobal(ptr);
            return result;
        }

        //**************************************************************************************************
        /*!
         * @brief		TETD_DLL_Exit() wrapper.
         *
         * @author		TJ
         * @date		2012/03/30(Fri) 
         * 
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //**************************************************************************************************
        private E_ERR_TETD_CONTROLLER_ENUM TetdDllExit()
        {
            this.cLog.WriteLine("TETD_DLL_Exit Start");
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_DLL_Exit();
            this.cLog.WriteLine("TETD_DLL_Exit End : ret = " + result.ToString());
            return result;
        }

        //**************************************************************************************************
        /*!
         * @brief		System callback function executed by API.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[in]   Int32 eventID       Event ID.
         * @param[in]   IntPtr eventData    Event data.
         * @param[in]   IntPtr userData     User data.
         * 
         * @return      none
         */
        //**************************************************************************************************
        private void EventCallback(Int32 eventID, IntPtr eventData, IntPtr userData)
        {
//          this.cLog.WriteLine("EventCallback Start " + eventID.ToString() + " " + eventData.ToString() + " " + userData.ToString());
            // If user callback function is registered
            if (null != this.usrCallBack)
            {
                // Call user callback function
                this.usrCallBack(eventID, eventData, userData);
            }
//          this.cLog.WriteLine("EventCallback End");
        }
        #endregion

        #region public methods
        //**************************************************************************************************
        /*!
         * @brief		TETD_DLL_Ver() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[out]  UInt32 Ver_out      Version information.
         * 
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //**************************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdDllVer(out UInt32 Ver_out)
        {
            this.cLog.WriteLine("TETD_DLL_Ver Start");
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_DLL_Ver(out Ver_out);
            this.cLog.WriteLine("TETD_DLL_Ver End : ret = " + result.ToString());
            return result;
        }

        //**************************************************************************************************
        /*!
         * @brief		Register user callback function
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[in]   TETD_CALLBACK_t CallbackFromFPD User callback function.
         * 
         * @return		TETD_OK                         Success
         */
        //**************************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdFpdRegisterCallback(TETD_CALLBACK_t CallbackFromFPD)
        {
            this.cLog.WriteLine("TETD_DLL_Register_Callback Start");
            this.usrCallBack = CallbackFromFPD;
            E_ERR_TETD_CONTROLLER_ENUM result = E_ERR_TETD_CONTROLLER_ENUM.TETD_OK;
            this.cLog.WriteLine("TETD_DLL_Register_Callback End : ret = " + result.ToString());
            return result;
        }

        //**************************************************************************************************
        /*!
         * @brief		TETD_FPD_Open() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[in]   UInt32 FpdNumber_in             FPD number to be connected.
         * @param[out   UInt32 ExpopsureRequestSwitch   0 : Hard exposure, 1 : Soft exposure.
         * 
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //**************************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdFpdOpen(UInt32 FpdNumber_in, out UInt32 ExpopsureRequestSwitch)
        {
            this.cLog.WriteLine("TETD_FPD_Open Start to FPD " + FpdNumber_in.ToString());
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_FPD_Open(FpdNumber_in, out ExpopsureRequestSwitch);
            this.cLog.WriteLine("TETD_FPD_Open End : ret = " + result.ToString());
            return result;
        }

        //**************************************************************************************************
        /*!
         * @brief		TETD_FPD_Close() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri)
         *
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //**************************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdFpdClose()
        {
            this.cLog.WriteLine("TETD_FPD_Close Start");
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_FPD_Close();
            this.cLog.WriteLine("TETD_FPD_Close End : ret = " + result.ToString());
            return result;
        }

        //**************************************************************************************************
        /*!
         * @brief		TETD_DLL_Get_State() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         * 
         * @return		UInt32      API state
         */
        //**************************************************************************************************
        public TETD_STATE_ENUM TetdDllGetState()
        {
            this.cLog.WriteLine("TETD_DLL_Get_State Start");
            TETD_STATE_ENUM result = TETD_DLL_Get_State();
            this.cLog.WriteLine("TETD_DLL_Get_State End : ret = " + result.ToString());
            return result;
        }

        //**************************************************************************************************
        /*!
         * @brief		TETD_FPD_Work_State() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[in]   UInt32 ExposureMode_in      0:Single exposure, 1:Double exposure.
         *
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //**************************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdFpdWorkState(UInt32 ExposureMode_in)
        {
            this.cLog.WriteLine("TETD_FPD_Work_State Start");
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_FPD_Work_State(ExposureMode_in);
            this.cLog.WriteLine("TETD_FPD_Work_State End : ret = " + result.ToString());
            return result;
        }

        //**************************************************************************************************
        /*!
         * @brief		TETD_FPD_Standby_State() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //**************************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdFpdStandbyState()
        {
            this.cLog.WriteLine("TETD_FPD_Standby_State Start");
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_FPD_Standby_State();
            this.cLog.WriteLine("TETD_FPD_Standby_State End : ret = " + result.ToString());
            return result;
        }

        //**************************************************************************************************
        /*!
         * @brief		TETD_FPD_Calibration_State() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //**************************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdFpdCalibrationState()
        {
            this.cLog.WriteLine("TETD_FPD_Calibration_State Start");
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_FPD_Calibration_State();
            this.cLog.WriteLine("TETD_FPD_Calibration_State End : ret = " + result.ToString());
            return result;
        }

        //**************************************************************************************************
        /*!
         * @brief		TETD_FPD_Exposure_Mode() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[in]   UInt32 ExposureMode_in      0:Single exposure, 1:Double exposure.
         * 
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //**************************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdFpdExposureMode(UInt32 ExposureMode_in)
        {
            this.cLog.WriteLine("TETD_FPD_Exposure_Mode Start");
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_FPD_Exposure_Mode(ExposureMode_in);
            this.cLog.WriteLine("TETD_FPD_Exposure_Mode End : ret = " + result.ToString());
            return result;
        }

        //**************************************************************************************************
        /*!
         * @brief		TETD_FPD_Exposure() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[in]   UInt32 Exp_Num_in       0:Single exposure, 1:Double exposure.
         *
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //**************************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdFpdExposure(UInt32 Exp_Num_in)
        {
            this.cLog.WriteLine("TETD_FPD_Exposure Start");
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_FPD_Exposure(Exp_Num_in);
            this.cLog.WriteLine("TETD_FPD_Exposure End : ret = " + result.ToString());
            return result;
        }

        //**************************************************************************************************
        /*!
         * @brief		TETD_FPD_Calibration_Offset() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[in]   UInt32 Flags_In     Number of pictures.
         * 
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //**************************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdFpdCalibrationOffset(UInt32 Flags_In)
        {
            this.cLog.WriteLine("TETD_FPD_Calibration_Offset Start numPicture = " + Flags_In.ToString());
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_FPD_Calibration_Offset(Flags_In);
            this.cLog.WriteLine("TETD_FPD_Calibration_Offset End : ret = " + result.ToString());
            return result;
        }

        //**************************************************************************************************
        /*!
         * @brief		TETD_FPD_Calibration_Exposure() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[in]   UInt32 Exp_Num_in       0:Single exposure, 1:Double exposure.
         *
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //**************************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdFpdCalibrationExposure(UInt32 Exp_Num_in)
        {
            this.cLog.WriteLine("TETD_FPD_Calibration_Exposure Start");
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_FPD_Calibration_Exposure(Exp_Num_in);
            this.cLog.WriteLine("TETD_FPD_Calibration_Exposure End : ret = " + result.ToString());
            return result;
        }

        //**************************************************************************************************
        /*!
         * @brief		TETD_FPD_Calibration_Next() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //********************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdFpdCalibrationNext(out UInt32 nNumAddImages_out)
        {
            this.cLog.WriteLine("TETD_FPD_Calibration_Next Start");
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_FPD_Calibration_Next(out nNumAddImages_out);
            this.cLog.WriteLine("TETD_FPD_Calibration_Next End : ret = " + result.ToString());
            return result;
        }

        //********************************************************************************************
        /*!
         * @brief		TETD_FPD_Calibration_Abort() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         * 
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //********************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdFpdCalibrationAbort()
        {
            this.cLog.WriteLine("TETD_FPD_Calibration_Abort Start");
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_FPD_Calibration_Abort();
            this.cLog.WriteLine("TETD_FPD_Calibration_Abort End : ret = " + result.ToString());
            return result;
        }

        //********************************************************************************************
        /*!
         * @brief		TETD_FPD_Calibration_Complete() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         * 
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //********************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdFpdCalibrationComplete()
        {
            this.cLog.WriteLine("TETD_FPD_Calibration_Complete Start");
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_FPD_Calibration_Complete();
            this.cLog.WriteLine("TETD_FPD_Calibration_Complete End : ret = " + result.ToString());
            return result;
        }

        //********************************************************************************************
        /*!
         * @brief		TETD_FPD_Set_Correction() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[in]   UInt32 Flags_in     Correction pattern.
         * 
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //********************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdFpdSetCorrection(UInt32 Flags_in)
        {
            this.cLog.WriteLine("TETD_FPD_Set_Correction Start");
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_FPD_Set_Correction(Flags_in);
            this.cLog.WriteLine("TETD_FPD_Set_Correction End : ret = " + result.ToString());
            return result;
        }

        //********************************************************************************************
        /*!
         * @brief		TETD_FPD_Get_Correction() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[out]  UInt32 Flags_out    Correction pattern.
         * 
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //********************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdFpdGetCorrection(out UInt32 Flags_out)
        {
            this.cLog.WriteLine("TETD_FPD_Get_Correction Start");
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_FPD_Get_Correction(out Flags_out);
            this.cLog.WriteLine("TETD_FPD_Get_Correction End : ret = " + result.ToString());
            return result;
        }

        //********************************************************************************************
        /*!
         * @brief		TETD_FPD_Get_Condition() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[out]  FPD_CONDITION FpdCondition_out  FPD Status Information.
         * 
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //********************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdFpdGetCondition(out FPD_CONDITION FpdCondition_out)
        {
            this.cLog.WriteLine("TETD_FPD_Get_Condition Start");
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_FPD_Get_Condition(out FpdCondition_out);
            this.cLog.WriteLine("TETD_FPD_Get_Condition End : ret = " + result.ToString());
            return result;
        }

        //********************************************************************************************
        /*!
         * @brief		TETD_FPD_Self_Diagnosis() wrapper.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         * 
         * @return		TETD_OK                         Success
         * @return		TETD_ERR_STATE                  Status error
         * @return		TETD_ERR_PARAMETER              Parameter error
         * @return		TETD_ERR_CONTROLLER_ERROR       FPD error
         * @return		TETD_ERR_DRIVER_ERROR           Driver error
         * @return		TETD_ERR_COMMUNICATION_ERROR    Communication error
         * @return		TETD_ERR_IMAGE_LIBRARY_ERROR    Image library error
         * @return		TETD_ERR_COMMON_LIBRARY_ERROR   Common library error
         */
        //********************************************************************************************
        public E_ERR_TETD_CONTROLLER_ENUM TetdFpdSelfDiagnosis()
        {
            this.cLog.WriteLine("TETD_FPD_Self_Diagnosis Start");
            E_ERR_TETD_CONTROLLER_ENUM result = TETD_FPD_Self_Diagnosis();
            this.cLog.WriteLine("TETD_FPD_Self_Diagnosis End : ret = " + result.ToString());
            return result;
        }

        //********************************************************************************************
        /*!
         * @brief		Dispose method.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @return		none
         */
        //********************************************************************************************
        public void Dispose()
        {
            this.TetdDllExit();
            this.cLog = null;

            // free GC handle
            gch.Free();

            // Suppress destructor
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
