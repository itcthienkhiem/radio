﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;


using System.Collections;
using System.Drawing.Printing;
using System.IO;
using System.ServiceProcess;
using System.Data.SqlClient;
using System.Linq;
using System.Collections;
using System.Net;
//using ClearCanvas.Dicom.Samples;
using VB6 = Microsoft.VisualBasic;

using System.Reflection;


using VietBaIT.CommonLibrary;
using VBString = Microsoft.VisualBasic.Strings;

////using CrystalDecisions.CrystalReports.Engine;
using System.Drawing.Drawing2D;
using VietBaIT.DROC.Objects.ObjectController;
using VietBaIT.DROC.Objects;
using DROCLibs.Entities;
using VietBaIT.DROC.Objects.ObjectInfor;
using System.Runtime.InteropServices;

using VietBaIT.Entities;
using VietBaIT.Controls;
using System.IO.Ports;
using GemBox.Spreadsheet;
using System.Data.OleDb;


using Microsoft.Win32;
using TE_IMAGE_INFO = FPDType.ToshibaFPD.tagImgInfo;
using TE_EVENT = System.Int32;
using TE_RESULT = System.Int32;
using TE_ERR_INFO = FPDType.ToshibaFPD.tagErrInfo;
using TE_CALPROGRESS_INFO = FPDType.ToshibaFPD.tagCalProgressInfo;
using TE_FPIHWSTATUS_INFO = FPDType.ToshibaFPD.tagFPIHWStatusInfo;
using TE_FPD_INFO = FPDType.ToshibaFPD.tagFPDInfo;
using WORD=System.UInt16;
using DWORD=System.UInt32;
using HRAW=System.UIntPtr;
using System.Globalization;


using OleDbDAL;

using System.Management;

using ClearCanvas.Common;
using ClearCanvas.Dicom.Codec;
using ClearCanvas.Dicom.Network.Scu;
using ClearCanvas.Dicom.Utilities.Xml;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using System.Xml.Linq;
using Vietbait.MediaBurner;

using DROCLibs.Reports;
using DROCKey;
//using DROCLibs.Reports;
//using System.Diagnostics;
namespace VietBaIT.DROC
{
    public partial class DROC_Ribbon : Form
    {
        public AppType.AppEnum.AppMode _AppMode = AppType.AppEnum.AppMode.License;

        #region Vị trí Annotation
        string _L = "L";
        string _R = "R";
        string _U = "U";
        string _B = "B";
        string _O = "O";
        #endregion
        
        #region Hardware Params
        public decimal MaxKvp = 150m;
        public decimal MinKvp = 15m;
        public decimal StepKvp = 0.5m;
        public int MaxmA = 1000;
        public int MinmA = 0;
        public int StepmA = 1;
        public int MaxmAs = 5000;
        public int MinmAs = 1;
        public int StepmAs = 1;
        #endregion

        string AutoWLPath = Application.StartupPath + @"\AutoWL.dat";
        List<string> lstPresiquitesFiles;
        //Biến xác định Login vào hệ thống sau khi LogOut chứ ko phải Login lúc kích hoạt
        public bool LoginAfterLogOut = false;
       

        AppType.AppEnum.DoubleMode _DoubleMode = AppType.AppEnum.DoubleMode.WorkList;
        AppType.AppEnum.DoubleMode _LastDoubleMode = AppType.AppEnum.DoubleMode.WorkList;
        AppType.AppEnum.TabMode _currTab = AppType.AppEnum.TabMode.WorkList;
        AppType.AppEnum.FPDMode _FPDMode = AppType.AppEnum.FPDMode.SingleMode;

        AppType.AppEnum.ViewState _ViewState = AppType.AppEnum.ViewState.Ready;
        public string RAWFileNameWillbeCreated = "IMG";
        public bool isAcq = false;//Đang ở Tab ACQ
        ScheduledControl m_objCurrScheduledControl = new ScheduledControl("", -1, -1, "", "", "", "", "", "", "", "", "", "", "", "", 0M, 0, 0, 0, 0, 0, 0);
        public bool m_blnCallFromLogin = false;
        public bool mv_bLoginSuccess = false;
        private string UID = "VBIT";
       

        private string PWD = "";
       
        private Hashtable arrRadCode = new Hashtable();
        #region "WL declaration"
        DataTable m_dtWLDataSource = new DataTable();
        DataTable m_dtWLDataSource_Suspending = new DataTable();
        DataTable m_dtStudyListDataSource = new DataTable();
        DataTable m_dtAcquisitionDataSource = new DataTable();
        DataTable m_dtWLRegSource = new DataTable();
        DataTable m_dtWLRegDetailSource = new DataTable();
     
        DataRow currentStudyRow;
        ContextMenuStrip ctx = new ContextMenuStrip();
        ToolStripMenuItem mnu0 = new ToolStripMenuItem("Tự thêm mới dịch vụ đang chọn");
        ToolStripMenuItem mnu1 = new ToolStripMenuItem("Chọn dịch vụ cần chụp khác");
        ToolStripMenuItem mnu2 = new ToolStripMenuItem("Xóa bớt dịch vụ");
        ToolStripSeparator mnu3 = new ToolStripSeparator();
        ToolStripMenuItem mnuselectImgPath = new ToolStripMenuItem("Chọn lại ảnh cho dịch vụ này");
        ToolStripMenuItem mnuChangeViewPos = new ToolStripMenuItem("Cập nhật lại vị trí chuẩn cho ảnh này");
        ToolStripSeparator mnu01 = new ToolStripSeparator();
        ToolStripMenuItem mnu4 = new ToolStripMenuItem("Properties");
        ToolStripMenuItem mnu5 = new ToolStripMenuItem("Xử lý lại từ ảnh gốc");
        bool AcquisitionFromWL = true;
        string Sex = "M";
        bool FirstExposure = true;

        public int GammaValue = 16;
        DateTime RegDate = DateTime.Now;
        DateTime BirthDate = DateTime.Now;
        string FileName = "VBIT_ABCD.raw";
        string CurrFileName = "VBIT_ABCD.raw";
        bool OK = true;
        DataTable DetailDataSource = new DataTable();
        delegate void ScanImg();
        Int16 DirectionCapture = 0;
        #endregion
        #region "DicomViewer Decleration"
        bool _AutoEnhance = true;
      
        int enumScalMode = 0;
        public bool blnWLCircular = true;
        Rectangle _currBound = new Rectangle(0, 0, 1135, 635);
        string LazerPrinterName = "";
        string CurrCellFileName = "";
        string HospitalName = "BENH VIEN GIAO THONG VAN TAI TW";
        string DepartmentName = "KHOA CHAN DOAN HINH ANH";
        static string fileName_FilmPrintConfig_XML = Application.StartupPath + @"\ServerConfig.XML";

        static string fileName_ParamConfig = Application.StartupPath + @"\ParameterConfig.XML";
        public int IMGW = 2560;
        public int IMGH = 2048;

        public int IMGW1 = 2560;
        public int IMGH1 = 2048;

        public int IMGW2 = 2560;
        public int IMGH2 = 2048;
        public int PORT_NUM = 104;
        public int PORT_NUM2 = 104;
        // string IPPrefix = "";
        public int _defaultIMGW = 2560;
        public int _defaultIMGH = 2048;

        public int _defaultIMGW2 = 2560;
        public int _defaultIMGH2 = 2048;
        public int LOW2 = -1;
        public int HIGH2 = -1;
        public int WL_LOW2 = -1;
        public int WL_HIGH2 = -1;
        public int LOW = -1;
        public int HIGH = -1;
        public int WL_LOW = -1;
        public int WL_HIGH = -1;
        public string Range;
      
        public string Range2;
       
        // delegate void LoadRawImg(string fileName, ref RasterImage img);
        Thread TFPD500_PCI = null;
        Thread TFPD550_PCI = null;
        Thread WLThread;
        Thread tSaveLastTime;
        Thread tfpd;
        public int TEMPLOW;
        public int TEMPHIGH;
        public string TEMPRange;
       
        ArrayList arrFile = new ArrayList();
      
        private bool IsClicking = true;
        private bool SendtoViewer = false;
        private int ImgSequence = 0;
        private bool Hasresult = false;
        private int X1, Y1, X2, Y2;
       
        // private int X1_Acq, Y1_Acq, X2_Acq, Y2_Acq;
        public Hashtable DicomDS = new Hashtable();
        public bool Running = false;
        public bool FitImg = true;
        //Biến xác định xem ứng dụng được gọi trực tiếp từ Menu hay từ một Form khác?
        public bool bCallFromMenu = true;
        //Đường dẫn các file ảnh khi được gọi từ form khác. Cách nhau bởi dấu ;
        public string FilePath = "";
        int Angle = 0;
        //Số cột và số hàng của MedicalViewer
        private int Cols, Rows;
        //Vị trí của cell người dùng đang thao tác
        public int _medicalViewerCellIndex = -1;

        //Biến xác định xem có phải đang ở chế độ PrintPreview ảnh hay không
        private bool IsPreview = true;
        //Số ảnh được nạp
        private int _images;

        //RasterCodecs _codecs;
        //public VietBaIT.DcmProcessing.UndoRedo UR = new VietBaIT.DcmProcessing.UndoRedo();

        //Dataset chứa dữ liệu của ảnh đang chọn. Dataset này được dùng để ghi vào file ảnh mới khi chọn Save as...
       
        Rectangle ImgRec;

        private bool _IsFilmPrinting = false;

       
        private int BPP = 8;
        public string C150181 = "";
        public string DiagnosticResult = "";
        public int DiagnosticStatus = 1;
        private Size pnlSize = new Size(0, 0);
        private FullScreenMode.FullScreen _FullScreen;
        private bool IsFullScreenMode = true;
        int PrintPreview = 0;
      
        public int m_intCurrDevice1 = -1;

        public int m_intCurrDevice2 = -1;

        public string modName = "";
        public string modCode = "";
        public string modTypeCode = "";
        public string modTypeName = "";

        public string modName2 = "";
        public string modCode2 = "";
        public string modTypeCode2 = "";
        public string modTypeName2 = "";

        public int intAlphaSensitively = 1;
        public int intWLSensitively = 1100;
        public int intOffsetSensitive = 100;
        public int intScaleSensitive = 100;
        public int intMagnifyZoom = 350;
        public int intMagnifyW = 200;
        public int intMagnifyH = 200;
        public int intMagnifyBorder = 200;
        public int intMagnifyCrossHair = 1;
        public bool blnMagnifyEllipse = false;



        private int intMagnifyRatio = 200;
        private bool blnAlwaysFitImg = true;
        bool blnhasJustCreated = false;
     
        private bool TempCrop = false;
        bool isLoadding = false;

        private double m_dblZoomOutRatio = 1.2;
        private double m_dblZoomInRatio = 0.8;
        PrintDocument _printDocument;
        DataTable dtPatient = new DataTable();
        public delegate void OnNavigation(bool IsForward);
        public event OnNavigation OnNav;
        public bool AutoPageSettings = true;
        int _GrdAcqIdx = -1;
        int m_intCurrentDetail_ID = -1;
        public bool blnAutoDetect = true;
        public bool AutoContrastAndBrightness = false;
        public int ContrastValue = 0;
        public int BrightnessValue = 0;
        public int OverlayTextSize = 16;
        #endregion
       
        #region IEParams
        bool ApplyWL = false;
        bool ADJUST_WOB = false;
        public int APPLY_WL = -1;
        public int ADJUST_WIDTH = 5000;
        public int ADJUST_CENTER = 5000;
        int WL_STT = 1;

        public int AUTO_MIN_MAX_BIT = 0;
        public int APPLY_INVERT_FIRST = 0;
        public int HEC_AFTER = 0;
        public int HEC_STT = 0;
        public int APPLY_HEC = 0;

        public int _ID = 0;
        public bool WOB = false;
        public int START_WIDTH = 5000;
        public int END_WIDTH = 5000;
        public int START_CENTER = 5000;
        public int END_CENTER = 5000;
        public int INVERT_STT = 0;
        public int MED_STT = 0;
        public int INVERT_AFTER = 1;
        public int APPLY_INVERT = 1;
        public bool APPLY_MED = false;
        public int GValue = 150;
        public int MEDValue = 2;
        public int CValue = 100;
        public int BValue = 100;
        public int MSEValue = 1600;
        bool ApplyMB = false;
        bool ApplyAAlias = false;
        public bool ApplyG = false;
        public bool ApplyMSE = false;
        public bool ApplyC = false;
        public bool ApplyB = false;
        bool ValidatedAfterRunningCommand = false;
        public bool MSEAC = true;//Apply coieficient
        public bool MSEAL = true;//Apply Lattitude
        public int EEC = 1700;
        public int EEL = 3;
        public int ELC = 140;
        public int ELL = 5;
        public int MSE_CommandType = 0;
        public int G_STT = 1;
        public int MSE_STT = 2;
        public int B_STT = 3;

        public Int32 C_STT = 4;

        public int _Threshold;
        public int _AADimension;

        public int Common_Threshold = 5;
        public int Common_AADimension = 5;
        public int Common_Filter = 1;
        public int _Filter;
        public int _AA_STT;

        public int _Angle;
        public int _MBDimension;
        public int _MBSTT;
        #endregion
        MessageProcDelegate _CallBackFunc;
        MessageProcDelegate _CallBackFuncFPDEU;
        ToshibaMessageProcDelegate _ToshibaCallBackFunc;
        public Dictionary<string, bool> _AutoEnhanceList = new Dictionary<string, bool>();
        public bool _Enhanced = false;
        string BODYSIZE_CODE = "M";
        string BODYSIZE_NAME = "Medium";
        bool IsUsingDicomConverter = true;
        action _CurrAct = action.FirstOrFinished;
        //SerialPort declaration
        SerialPort _COM;
        #region Dicomconverter
        DataTable m_dtDicomconverterInfo = new DataTable();
        const string colBitsStored = "BitsStored";
        const string colHightBit = "HightBit";
        const string colBitsAllocated = "BitsAllocated";
        const string colPid = "PID";
        const string colMonoChrome = "MonoChrome";
        const string colKVP = "KVP";
        const string colMAS = "MAS";
        const string colPatientName = "Patient_Name";
        const string colPatientSex = "Patient_Sex";
        const string colPatientAge = "Patient_Age";
        const string colPatientBirthdate = "Patient_Birthdate";
        const string colRegDate = "Reg_Date";
        const string colRegNum = "Reg_Num";
        const string colImgWidth = "IMGWidth";
        const string colImgHeight = "IMGHeigh";
        const string colModalityCode = "Modality_Code";
        const string colAtonomyCode = "Atonomy_Code";
        const string colProjectionCode = "Projection_Code";
        const string colHostpitalName = "Hostpital_Name";
        const string colDepartmentName = "Department_Name";

        const string _colStudyInstanceUID = "StudyInstanceUID";
        const string colSeriesInstanceUID = "SeriesInstanceUID";
        const string colSOPInstanceUID = "SOPInstanceUID";
        const string colAcqDate = "AcqDate";
        const string colAppName = "AppName";
        #endregion
        int FPDPanel1Idx = 0;
        int FPDPanel2Idx = 1;
        int FPDPanelSelectErrValue = -1;
        string m_strCurrACode = "";
        string m_strCurrPCode = "";
        int FPDSeq = 1;
        #region LogFile
        string strRealizeLogFile = Application.StartupPath + @"\DROCLogs\RealizeLog_" + Utility.GetYYYYMMDD(DateTime.Now) + ".log";
        #endregion

      

        /// <summary>
        /// Hàm khởi tạo Form
        /// </summary>
        public DROC_Ribbon()
        {
            try
            {
                InitializeComponent();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": InitializeComponent  Completely");
                //Khởi tạo các sự kiện của một số UCs
                InitEvents();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": Init Events Completely");
                cmdReady.Enabled = GetSelectedScheduled() != null;// _AppMode == AppMode.License ? false : true;
                cmdReady.Text = MultiLanguage.GetText(globalVariables.DisplayLanguage, "Sẵn sàng", "Ready");// _AppMode == AppMode.License ? cmdReady.Text : "Thực hiện chụp";
                CreateDicomConverterInfo();
                InitUI();
                _CallBackFunc = new MessageProcDelegate(MessageProc);
                _CallBackFuncFPDEU = new MessageProcDelegate(MessageProc_FPDEU);
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": MessageProc  Completely");
                _ToshibaCallBackFunc = new ToshibaMessageProcDelegate(ToshibaMessageProc);
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": ToshibaMessageProc  Completely");
                LoadUILan();
                SetLang();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": SetLang  Completely");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                AppLogger.LogAction.LogActions("START DROC at: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                
            }
        }
        void Try2CreateLogFolder()
        {
            try
            {
                if (!Directory.Exists(Application.StartupPath + @"\DROCLogs")) Directory.CreateDirectory(Application.StartupPath + @"\DROCLogs");
            }
            catch
            {
            }
        }





        
        void Try2EndInvoke()
        {
            try
            {
                EndInvoke(_IAr);
            }
            catch
            {
            }
        }

        #region "Licsence"
        private IAsyncResult _IAr;

        private string mv_RegKey = "";



      

        private delegate void StartUpCallBack();
        #endregion

        void CreateDicomConverterInfo()
        {

            m_dtDicomconverterInfo.Columns.AddRange(new DataColumn[]{new DataColumn(colPid,typeof(string)),new DataColumn(colHightBit,typeof(string)),new DataColumn(colBitsAllocated,typeof(string)),new DataColumn(colMonoChrome,typeof(string)),new DataColumn(colDepartmentName,typeof(string)),
            new DataColumn(colPatientName,typeof(string)),new DataColumn(colPatientSex,typeof(string)),new DataColumn(colPatientAge,typeof(string)),
            new DataColumn(colPatientBirthdate,typeof(string)),new DataColumn(colRegDate,typeof(string)),new DataColumn(colRegNum,typeof(string)),
            new DataColumn(colImgWidth,typeof(string)),new DataColumn(colImgHeight,typeof(string)),new DataColumn(colModalityCode,typeof(string)),
            new DataColumn(_colStudyInstanceUID,typeof(string)),new DataColumn(colSeriesInstanceUID,typeof(string)),new DataColumn(colSOPInstanceUID,typeof(string)),new DataColumn(colAcqDate,typeof(string)),new DataColumn(colAppName,typeof(string)),new DataColumn(colAtonomyCode,typeof(string)),new DataColumn(colKVP,typeof(string)),new DataColumn(colMAS,typeof(string)),new DataColumn(colProjectionCode,typeof(string)),new DataColumn(colBitsStored,typeof(string)),new DataColumn(colHostpitalName,typeof(string))});
        }
        void SetLang()
        {
            try
            {
                if (!globalVariables.gv_ConnectSuccess)
                {
                    clsUser.KhoiTaoKetNoi();
                }
                if (System.IO.File.Exists(Application.StartupPath + @"\MultiLanguageCSharp.Dll") || System.IO.File.Exists(Application.StartupPath + @"\MultiLanguageCSharp.Dll"))
                {
                    cmdSetLang.Visible = true;
                    CSHARP.MultiLanguage.SetLanguage(globalVariables.DisplayLanguage, this, this.GetType().Assembly.ManifestModule.Name.Split('.')[0], globalVariables.OleDbConnection);
                }
                else
                    cmdSetLang.Visible = false;

                if (globalVariables.DisplayLanguage == "VN")
                {
                    toolTip1.SetToolTip(cmdSetLang, "English UI");
                    cmdSetLang.Text = "EN";
                }
                else
                {
                    toolTip1.SetToolTip(cmdSetLang, "VietNamese UI");
                    cmdSetLang.Text = "VN";
                }
            }
            catch
            {
            }
        }
     
        #region FPD550
        int FPD500_PCIIsWorking;
        bool WLIsWorking = true;

        bool WLhasjustApplied = false;
        bool FPD560IsWorking = true;
        private const string STR_DRTech_F550_LAN_REFdll = @"DRTech_F500_PCI_REF.dll";

        #region API Declaration

        //Functions for System
        [DllImport("DRTech_F500_PCI_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int Sys_Allocation_F500_PCI();

        [DllImport("DRTech_F500_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "Sys_Free")]
        public static extern int Sys_FreeF500_PCI();

        [DllImport("DRTech_F500_PCI_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int CheckSwitch();

        [DllImport("DRTech_F500_PCI_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetTemperature();

        [DllImport("DRTech_F500_PCI_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetSwitchMsg(int i);

        [DllImport("DRTech_F500_PCI_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int CheckInterface();

        [DllImport("DRTech_F500_PCI_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetMsgLength();

        [DllImport("DRTech_F500_PCI_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int RunforDetector(int cmd);



        [DllImport("DRTech_F500_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "LoadGainMap")]
        public static extern int LoadGainMap500_PCI([MarshalAs(UnmanagedType.LPStr)] string pPathMap, int Index);

        [DllImport("DRTech_F500_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "LoadPixelMap")]
        public static extern int LoadPixelMap500_PCI([MarshalAs(UnmanagedType.LPStr)] string pPathMap, int Index);

        //Functions for Calibration
        [DllImport("DRTech_F500_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "PanelSelect")]
        public static extern int PanelSelect500_PCI(int Index);

        //Functions for Calibration
        [DllImport("DRTech_F500_PCI_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe ushort* GetImagePtr();

        [DllImport("DRTech_F500_PCI_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool Init_Comm();

        [DllImport("DRTech_F500_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "Map_Calibration")]
        public static extern int Map_Calibration500_PCI(ref UInt16 pInputS, int P_ImageNum, int T_ImageNum, int Index);

        //Functions for Calibration
        [DllImport("DRTech_F500_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "CalibrationMode")]
        public static extern int CalibrationMode500_PCI(int Index);

        [DllImport("DRTech_F500_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SaveGainMap")]
        public static extern int SaveGainMap500_PCI([MarshalAs(UnmanagedType.LPStr)] string pPathMap);

        [DllImport("DRTech_F500_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SavePixelMap")]
        public static extern int SavePixelMap500_PCI([MarshalAs(UnmanagedType.LPStr)] string pPathMap);



        #endregion


        #region attributies



        #endregion


        bool v_blnHasConvertRawin2DicomFile = false;
        #region Init FPD
        void InitFPD500_PCI()
        {
            try
            {
                lstPresiquitesFiles = new List<string>();
                lstPresiquitesFiles.AddRange(new string[] { "Test.MAP", "Test.GMP" });
                if (!EnoughFiles(lstPresiquitesFiles)) return;
                Sys_Allocation_F500_PCI();
                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Sys_Allocation success");
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": Sys_Allocation success");
                LoadPixelMap500_PCI("Test.MAP", 0);
                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "LoadPixelMap success");
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": LoadPixelMap success");
                LoadGainMap500_PCI("Test.GMP", 0);
                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "LoadGainMap success");
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": LoadGainMap success");
                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Init FPD550 completely");

                TFPD500_PCI = new Thread(new ThreadStart(FPD500_PCICallbackFunction));
               
                TFPD500_PCI.Start();
            }
            catch (Exception ex)
            {
                AppLogger.LogAction.LogActions("==>InitFPD500_PCI().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex.ToString());
                Utility.ShowMsg(ex.Message);
            }
        }
        private void FPD500_PCICallbackFunction()
        {
            try
            {
                int fpdFlag = -100;
                int fpdStatus = -100;
                int test = 0;

                int ErrCount = 0;
                Init_Comm();

                FPD500_PCIIsWorking = 1;

                while (FPD500_PCIIsWorking != 0)
                {
                    //Thread.Sleep(1);
                    while (FPD500_PCIIsWorking != 0)
                    {
                        try
                        {
                            CheckSwitch();
                        }
                        catch (Exception ex00)
                        {
                            AppLogger.LogAction.LogActions("==>FPD500CallbackFuntion.CheckSwitch().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex00.ToString());

                        }
                        // AppLogger.LogAction.ShowEventStatus(lblFPDStatus,"CheckSwitch...");
                        //AppLogger.LogAction.AddLog2List(lstFPD560,DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": CheckSwitch...");
                        for (int i = 0; i < GetMsgLength(); i++)
                        {
                            try
                            {
                                fpdFlag = GetSwitchMsg(i);
                            }
                            catch (Exception ex01)
                            {
                                AppLogger.LogAction.LogActions("==>FPD500CallbackFuntion.GetSwitchMsg().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex01.ToString());

                            }
                            //  AppLogger.LogAction.ShowEventStatus(lblFPDStatus,"GetSwitchMsg("+i.ToString()+")...");
                            // AppLogger.LogAction.AddLog2List(lstFPD560,DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": GetSwitchMsg(" + i.ToString() + ")...");
                            try
                            {
                                fpdStatus = RunforDetector(fpdFlag);
                            }
                            catch (Exception ex02)
                            {
                                ErrCount += 1;
                                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Bạn hãy thử chụp lại lần " + ErrCount.ToString());
                                AppLogger.LogAction.LogActions("==>FPD500CallbackFuntion.RunforDetector().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex02.ToString());
                                if (ErrCount >= 3)
                                    new frm_LargeMsgBoxOK("THÔNG BÁO", "SAU 3 LẦN CHỤP MÀ PHẦN MỀM CHƯA BẮT ĐƯỢC ẢNH THÌ BẠN NÊN KHỞI ĐỘNG LẠI PHẦN MỀM ĐỂ CHỤP LẠI.PHÍA VIETBAIT SẼ CỐ GẮNG XỬ LÝ SỰ CỐ NÀY", "TÔI ĐỒNG Ý", "").ShowDialog();
                            }
                            // AppLogger.LogAction.ShowEventStatus(lblFPDStatus,"RunforDetector(" + fpdFlag.ToString() + ")...");
                            // AppLogger.LogAction.AddLog2List(lstFPD560,DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": RunforDetector(" + fpdFlag.ToString() + ")...");
                            if (fpdFlag == 1 && FPD500_PCIIsWorking == 1) // if hand-switch ready is detected
                            {
                                PlayBeepReady();
                                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "FPD500 Ready");
                                AppLogger.LogAction.LogActions("==>FPD500 ready: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": FPD500 Ready");
                            }
                            else if (fpdFlag == 2 && FPD500_PCIIsWorking == 1) //if hand-switch shot is detected
                            {
                                if (fpdStatus != 0)
                                {

                                    PlayBeepShot();
                                    ScheduledControl _selected = GetSelectedScheduled();
                                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Hand-switch shot");
                                    AppLogger.LogAction.LogActions("==>Hand-switch shot: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": Hand-switch shot");
                                    unsafe
                                    {
                                        try
                                        {
                                            v_blnHasConvertRawin2DicomFile = false;
                                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đang nhận dữ liệu...", ": Getting RAW Data"));
                                            AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang nhận dữ liệu...", ": Getting RAW Data"));
                                            bool _isSelected = true;
                                            string RAWFilePath = "";
                                            string _SubDirLv1 = SubDirLv1();
                                            string _SubDirLv2_Patient = SubDirLv2_Patient();
                                            //Tạo các thư mục theo cấp ngày\Bệnh nhân(Mã bệnh nhân _ Tên Bệnh nhân _ Tuổi)
                                            if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1)) Directory.CreateDirectory(txtImgDir.Text + @"\" + _SubDirLv1);
                                            if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient)) Directory.CreateDirectory(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient);
                                            //Kiểm tra nếu chưa chọn thủ tục nào thì cần lưu ảnh thành tên file theo định dạng
                                            //YYYY_MM_DD_HH_mm_ss
                                            if (_selected == null || RAWFileNameWillbeCreated == "NONE_SELECTED")
                                            {
                                                _isSelected = false;
                                                RAWFileNameWillbeCreated = "NONE_SELECTED_" + Utility.GetYYYYMMDDHHMMSS(DateTime.Now);
                                                if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED")) Directory.CreateDirectory(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED");
                                                RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED\" + RAWFileNameWillbeCreated + ".RAW";
                                            }
                                            else
                                            {
                                                //Kiểm tra xem Schedule đang chọn có ảnh chưa. Nếu đã có ảnh thì thực hiện theo tùy chọn phần cấu hình hệ thống
                                                if (_selected.Status > 0)
                                                {
                                                    AppLogger.LogAction.AddLog2List(lstFPD560, "BEGIN: Trạng thái dịch vụ đang chọn đã có ảnh");
                                                    
                                                    
                                                        RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\" + RAWFileNameWillbeCreated + ".RAW";//RAWFileNameWillbeCreated=REGNUM+_Detail_ID+_ACode+_Pcode tại phần click của Scheduled
                                                }
                                                else
                                                    RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\" + RAWFileNameWillbeCreated + ".RAW";//RAWFileNameWillbeCreated=REGNUM+_Detail_ID+_ACode+_Pcode tại phần click của Scheduled
                                            }
                                            var bytes = new byte[2];
                                            try
                                            {
                                                ushort* image = GetImagePtr();
                                                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": ushort* image = GetImagePtr() OK; ", ": ushort* image = GetImagePtr(); OK"));
                                                bytes = new byte[IMAGE_HEIGHT * IMAGE_WIDTH * sizeof(ushort)];
                                                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "IMAGE_HEIGHT = " + IMAGE_HEIGHT.ToString() + ", IMAGE_WIDTH=" + IMAGE_WIDTH.ToString());
                                                Marshal.Copy((IntPtr)image, bytes, 0, IMAGE_HEIGHT * IMAGE_WIDTH * sizeof(ushort));
                                                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "Marshal.Copy((IntPtr)image, bytes, 0, IMAGE_HEIGHT * IMAGE_WIDTH * sizeof(ushort)) OK ");
                                                if (IsCanFreeColTaskMem())
                                                {
                                                    try
                                                    {
                                                        Marshal.FreeCoTaskMem((IntPtr)image);
                                                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": FreeCoTaskMem");
                                                    }
                                                    catch (Exception exmemo)
                                                    {
                                                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": FreeCoTaskMem Exception" + exmemo.Message);
                                                    }
                                                }
                                            }
                                            catch (Exception ex550)
                                            {
                                                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ex550.Message);
                                            }
                                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": GetImagePtr() OK ", ": GetImagePtr() OK"));
                                            string _RAWFilePath1 = txtImgDir.Text + @"\" + RAWFileNameWillbeCreated + ".RAW";
                                            AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "RAW file...");
                                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": Xác định xong tên file RAW");
                                            //Luôn lưu file raw trước
                                            try
                                            {
                                                pnlScheduled.Enabled = false;
                                                try2RenameExistedFile(RAWFilePath);
                                                File.WriteAllBytes(RAWFilePath, bytes);
                                                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Lưu xong ảnh thô...", ": SAVE RAW IMAGE SUCCESSFUL"));
                                                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Lưu xong ảnh thô...", ": SAVE RAW IMAGE SUCCESSFUL"));
                                            }
                                            catch (Exception ex0)
                                            {
                                                AppLogger.LogAction.LogActions("==>Save Raw File().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex0.ToString());
                                            }
                                            if (IsUsingDicomConverter)//Tự động convert thành file Dicom
                                            {
                                                DataRow dr = MakeDcmConverterInfor(_selected);
                                                if (dr != null)
                                                {
                                                    if (DicomConverter.DicomConverter.Convert2Dicom(bytes, RAWFilePath, dr,true, AutoWLPath))
                                                    {

                                                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đã chuyển thành file Dicom...", ": Convert Dicom completely"));
                                                        AutoUpdateResultAfterCapturingPictureFromModality(_selected);
                                                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đã chuyển thành file Dicom...", ": Convert Dicom completely"));
                                                        v_blnHasConvertRawin2DicomFile = true;
                                                        lblAutoDcmConverter.ImageIndex = 0;
                                                        IsUsingDicomConverter = true;
                                                    }
                                                    else
                                                    {

                                                        File.WriteAllBytes(RAWFilePath, bytes);
                                                        lblAutoDcmConverter.ImageIndex = 0;
                                                        IsUsingDicomConverter = true;
                                                    }
                                                }
                                                else
                                                {
                                                    lblAutoDcmConverter.ImageIndex = 0;
                                                    IsUsingDicomConverter = true;
                                                    File.WriteAllBytes(RAWFilePath, bytes);
                                                }
                                            }
                                            else
                                            {
                                                lblAutoDcmConverter.ImageIndex = 0;
                                                IsUsingDicomConverter = true;
                                                File.WriteAllBytes(RAWFilePath, bytes);
                                            }
                                            Thread.Sleep(100);
                                            mdlStatic.isDisplayImg = false;
                                            isLoadding = false;
                                        }
                                        catch (Exception ex)
                                        {
                                            AppLogger.LogAction.LogActions("==>Get Image From FPD().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex.ToString());
                                        }
                                        finally
                                        {
                                            #region "Reset Allengers GCOM"
                                            blnCheckSignalAfterEXPCommand = false;
                                            blnCheckSignalAfterSetKvpMas = false;
                                            CountOfResendingEXPCmd = 0;
                                            CountOfResendingDataFrame = 0;
                                            isSendbyCmd = true;
                                            isSendbyCmdExp = true;

                                            preventsenddata = false;
                                            preventExp = true;
                                            _SetEnableButton(true);
                                            _SetTextBtn("Ready");
                                            cmdReady.Tag = "R";
                                            State = 0;
                                            #endregion
                                            cmdReady.Enabled = m_objCurrScheduledControl != null && m_objCurrScheduledControl.Status == 0;
                                            pnlScheduled.Enabled = cmdReady.Enabled;
                                            if (!IsUsingDicomConverter)
                                            {
                                                string RAWFilePath1 = txtImgDir.Text + @"\" + RAWFileNameWillbeCreated + ".RAW";

                                                // Lấy tên file vừa tạo trong thư mục DownFolder
                                                string IMGfileHasJustCreated = Directory.GetFiles(txtImgDir.Text)[0];

                                                // Đổi tên thành file .raw
                                                File.Move(IMGfileHasJustCreated, RAWFilePath1);
                                            }
                                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đang hiển thị ảnh...", ": Displaying Img"));
                                            AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang hiển thị ảnh...", ": Displaying Img"));
                                            _IAr = BeginInvoke(new DisplayImg(ViewImg));
                                            fileCreated = true;

                                        }
                                    }
                                }
                                else
                                {
                                    PlayBeepAbort();
                                }
                            }
                            else if (fpdFlag == 3)
                            {
                                fpdFlag = 3;
                            }
                            else fpdFlag = 0;
                            fpdFlag = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogAction.LogActions("==>FPD500 Callback Funtion().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex.ToString());
                Utility.ShowMsg(ex.Message, "Lỗi");
            }
        }
        bool IsCanFreeColTaskMem()
        {
            try
            {
                if (File.Exists(Application.StartupPath + @"\FreeMemory.memo"))
                {
                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ":Can FreeCoTaskMem");
                    return true;
                }
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ":Can not FreeCoTaskMem");
                return false;
            }
            catch (Exception ex)
            {
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ":Exception before FreeCoTaskMem:" + ex.Message);
                return false;
            }
        }
        unsafe void Try2FreeCoTaskMem(ushort* image)
        {
            try
            {
            }
            catch
            {
            }
        }
        #endregion


        #endregion


        #region FPD550_PCI
        int FPD550_PCIIsWorking;

        private const string STR_DRTech_F550_PCI_LAN_REFdll = @"DRTech_F550_PCI_REF.dll";

        #region API Declaration

        //Functions for System
        [DllImport("DRTech_F550_PCI_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int Sys_Allocation_F550();

        [DllImport("DRTech_F550_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "Sys_Free")]
        public static extern int Sys_FreeF550_PCI();

        [DllImport("DRTech_F550_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "CheckSwitch")]
        public static extern int CheckSwitch_550_PCI();

        [DllImport("DRTech_F550_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetTemperature")]
        public static extern int GetTemperature_550_PCI();

        [DllImport("DRTech_F550_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetSwitchMsg")]
        public static extern int GetSwitchMsg_550_PCI(int i);

        [DllImport("DRTech_F550_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "CheckInterface")]
        public static extern int CheckInterface_550_PCI();

        [DllImport("DRTech_F550_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetMsgLength")]
        public static extern int GetMsgLength_550_PCI();

        [DllImport("DRTech_F550_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "RunforDetector")]
        public static extern int RunforDetector_550_PCI(int cmd);



        [DllImport("DRTech_F550_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "LoadGainMap")]
        public static extern int LoadGainMap_550_PCI([MarshalAs(UnmanagedType.LPStr)] string pPathMap, int Index);

        [DllImport("DRTech_F550_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "LoadPixelMap")]
        public static extern int LoadPixelMap_550_PCI([MarshalAs(UnmanagedType.LPStr)] string pPathMap, int Index);

        //Functions for Calibration
        [DllImport("DRTech_F550_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "PanelSelect")]
        public static extern int PanelSelect_550_PCI(int Index);

        //Functions for Calibration
        [DllImport("DRTech_F550_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetImagePtr")]
        public static extern unsafe ushort* GetImagePtr_550_PCI();

        [DllImport("DRTech_F550_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "Init_Comm")]
        public static extern bool Init_Comm_550_PCI();

        [DllImport("DRTech_F550_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "Map_Calibration")]
        public static extern int Map_Calibration550_PCI(ref UInt16 pInputS, int P_ImageNum, int T_ImageNum, int Index);

        //Functions for Calibration
        [DllImport("DRTech_F550_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "CalibrationMode")]
        public static extern int CalibrationMode550_PCI(int Index);

        [DllImport("DRTech_F550_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SaveGainMap")]
        public static extern int SaveGainMap550_PCI([MarshalAs(UnmanagedType.LPStr)] string pPathMap);

        [DllImport("DRTech_F550_PCI_REF.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SavePixelMap")]
        public static extern int SavePixelMap550_PCI([MarshalAs(UnmanagedType.LPStr)] string pPathMap);



        #endregion


        #region attributies



        #endregion



        #region FPD550_PCI
        void InitFPD550_PCI()
        {
            try
            {
              
                Sys_Allocation_F550();
                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Sys_Allocation success");
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": Sys_Allocation success");
                LoadPixelMap_550_PCI("Test.MAP", 0);
                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "LoadPixelMap success");
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": LoadPixelMap success");
                LoadGainMap_550_PCI("Test.GMP", 0);
                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "LoadGainMap success");
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": LoadGainMap success");
                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Init FPD550 completely");

                TFPD550_PCI = new Thread(new ThreadStart(FPD550_PCICallbackFunction));
              
                TFPD550_PCI.Start();
            }
            catch (Exception ex)
            {
                AppLogger.LogAction.LogActions("==>InitFPD500_PCI().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex.ToString());
                Utility.ShowMsg(ex.Message);
            }
        }
        bool EnoughFiles(List<string> lstPresiquitesFiles)
        {
            try
            {
                if (lstPresiquitesFiles == null || lstPresiquitesFiles.Count <= 0) return true;
                foreach (string file in lstPresiquitesFiles)
                    if (!File.Exists(file)) return false;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not enough presiquites files\n" + ex.ToString());
                return true;
            }
        }
        private void FPD550_PCICallbackFunction()
        {
            try
            {
                int fpdFlag = -100;
                int fpdStatus = -100;
                int test = 0;

                int ErrCount = 0;
                Init_Comm();

                FPD550_PCIIsWorking = 1;

                while (FPD550_PCIIsWorking != 0)
                {
                    //Thread.Sleep(1);
                    while (FPD550_PCIIsWorking != 0)
                    {
                        try
                        {
                            CheckSwitch_550_PCI();
                        }
                        catch (Exception ex00)
                        {
                            AppLogger.LogAction.LogActions("==>FPD550CallbackFuntion.CheckSwitch().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex00.ToString());

                        }
                        // AppLogger.LogAction.ShowEventStatus(lblFPDStatus,"CheckSwitch...");
                        //AppLogger.LogAction.AddLog2List(lstFPD560,DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": CheckSwitch...");
                        for (int i = 0; i < GetMsgLength_550_PCI(); i++)
                        {
                            try
                            {
                                fpdFlag = GetSwitchMsg_550_PCI(i);
                            }
                            catch (Exception ex01)
                            {
                                AppLogger.LogAction.LogActions("==>FPD550CallbackFuntion.GetSwitchMsg().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex01.ToString());

                            }
                            //  AppLogger.LogAction.ShowEventStatus(lblFPDStatus,"GetSwitchMsg("+i.ToString()+")...");
                            // AppLogger.LogAction.AddLog2List(lstFPD560,DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": GetSwitchMsg(" + i.ToString() + ")...");
                            try
                            {
                                fpdStatus = RunforDetector_550_PCI(fpdFlag);
                            }
                            catch (Exception ex02)
                            {
                                ErrCount += 1;
                                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Bạn hãy thử chụp lại lần " + ErrCount.ToString());
                                AppLogger.LogAction.LogActions("==>FPD550CallbackFuntion.RunforDetector().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex02.ToString());
                                if (ErrCount >= 3)
                                    new frm_LargeMsgBoxOK("THÔNG BÁO", "SAU 3 LẦN CHỤP MÀ PHẦN MỀM CHƯA BẮT ĐƯỢC ẢNH THÌ BẠN NÊN KHỞI ĐỘNG LẠI PHẦN MỀM ĐỂ CHỤP LẠI.PHÍA VIETBAIT SẼ CỐ GẮNG XỬ LÝ SỰ CỐ NÀY", "TÔI ĐỒNG Ý", "").ShowDialog();
                            }
                            // AppLogger.LogAction.ShowEventStatus(lblFPDStatus,"RunforDetector(" + fpdFlag.ToString() + ")...");
                            // AppLogger.LogAction.AddLog2List(lstFPD560,DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": RunforDetector(" + fpdFlag.ToString() + ")...");
                            if (fpdFlag == 1 && FPD550_PCIIsWorking == 1) // if hand-switch ready is detected
                            {
                                PlayBeepReady();
                                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "FPD550 Ready");
                                AppLogger.LogAction.LogActions("==>FPD550 ready: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": FPD550 Ready");
                            }
                            else if (fpdFlag == 2 && FPD550_PCIIsWorking == 1) //if hand-switch shot is detected
                            {
                                if (fpdStatus != 0)
                                {

                                    PlayBeepShot();
                                    ScheduledControl _selected = GetSelectedScheduled();
                                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Hand-switch shot");
                                    AppLogger.LogAction.LogActions("==>Hand-switch shot: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": Hand-switch shot");
                                    unsafe
                                    {
                                        try
                                        {
                                            v_blnHasConvertRawin2DicomFile = false;
                                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đang nhận dữ liệu...", ": Getting RAW Data"));
                                            AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang nhận dữ liệu...", ": Getting RAW Data"));
                                            bool _isSelected = true;
                                            string RAWFilePath = "";
                                            string _SubDirLv1 = SubDirLv1();
                                            string _SubDirLv2_Patient = SubDirLv2_Patient();
                                            //Tạo các thư mục theo cấp ngày\Bệnh nhân(Mã bệnh nhân _ Tên Bệnh nhân _ Tuổi)
                                            if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1)) Directory.CreateDirectory(txtImgDir.Text + @"\" + _SubDirLv1);
                                            if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient)) Directory.CreateDirectory(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient);
                                            //Kiểm tra nếu chưa chọn thủ tục nào thì cần lưu ảnh thành tên file theo định dạng
                                            //YYYY_MM_DD_HH_mm_ss
                                            if (_selected == null || RAWFileNameWillbeCreated == "NONE_SELECTED")
                                            {
                                                _isSelected = false;
                                                RAWFileNameWillbeCreated = "NONE_SELECTED_" + Utility.GetYYYYMMDDHHMMSS(DateTime.Now);
                                                if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED")) Directory.CreateDirectory(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED");
                                                RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED\" + RAWFileNameWillbeCreated + ".RAW";
                                            }
                                            else
                                            {
                                                //Kiểm tra xem Schedule đang chọn có ảnh chưa. Nếu đã có ảnh thì thực hiện theo tùy chọn phần cấu hình hệ thống
                                                if (_selected.Status > 0)
                                                {
                                                    AppLogger.LogAction.AddLog2List(lstFPD560, "BEGIN: Trạng thái dịch vụ đang chọn đã có ảnh");
                                                   
                                                        RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\" + RAWFileNameWillbeCreated + ".RAW";//RAWFileNameWillbeCreated=REGNUM+_Detail_ID+_ACode+_Pcode tại phần click của Scheduled
                                                }
                                                else
                                                    RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\" + RAWFileNameWillbeCreated + ".RAW";//RAWFileNameWillbeCreated=REGNUM+_Detail_ID+_ACode+_Pcode tại phần click của Scheduled
                                            }
                                            var bytes = new byte[2];
                                            try
                                            {
                                                ushort* image = GetImagePtr_550_PCI();
                                                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": ushort* image = GetImagePtr() OK; ", ": ushort* image = GetImagePtr(); OK"));
                                                bytes = new byte[IMAGE_HEIGHT * IMAGE_WIDTH * sizeof(ushort)];
                                                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "IMAGE_HEIGHT = " + IMAGE_HEIGHT.ToString() + ", IMAGE_WIDTH=" + IMAGE_WIDTH.ToString());
                                                Marshal.Copy((IntPtr)image, bytes, 0, IMAGE_HEIGHT * IMAGE_WIDTH * sizeof(ushort));
                                                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "Marshal.Copy((IntPtr)image, bytes, 0, IMAGE_HEIGHT * IMAGE_WIDTH * sizeof(ushort)) OK ");
                                                if (IsCanFreeColTaskMem())
                                                {
                                                    try
                                                    {
                                                        Marshal.FreeCoTaskMem((IntPtr)image);
                                                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": FreeCoTaskMem");
                                                    }
                                                    catch (Exception exmemo)
                                                    {
                                                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": FreeCoTaskMem Exception" + exmemo.Message);
                                                    }
                                                }
                                            }
                                            catch (Exception ex550)
                                            {
                                                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ex550.Message);
                                            }
                                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": GetImagePtr() OK ", ": GetImagePtr() OK"));
                                            string _RAWFilePath1 = txtImgDir.Text + @"\" + RAWFileNameWillbeCreated + ".RAW";
                                            AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "RAW file...");
                                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": Xác định xong tên file RAW");
                                            //Luôn lưu file raw trước
                                            try
                                            {
                                                pnlScheduled.Enabled = false;
                                                try2RenameExistedFile(RAWFilePath);
                                                File.WriteAllBytes(RAWFilePath, bytes);
                                                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Lưu xong ảnh thô...", ": SAVE RAW IMAGE SUCCESSFUL"));
                                                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Lưu xong ảnh thô...", ": SAVE RAW IMAGE SUCCESSFUL"));
                                            }
                                            catch (Exception ex0)
                                            {
                                                AppLogger.LogAction.LogActions("==>Save Raw File().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex0.ToString());
                                            }
                                            if (IsUsingDicomConverter)//Tự động convert thành file Dicom
                                            {
                                                DataRow dr = MakeDcmConverterInfor(_selected);
                                                if (dr != null)
                                                {
                                                    if (DicomConverter.DicomConverter.Convert2Dicom(bytes, RAWFilePath, dr, true, AutoWLPath))
                                                    {

                                                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đã chuyển thành file Dicom...", ": Convert Dicom completely"));
                                                        AutoUpdateResultAfterCapturingPictureFromModality(_selected);
                                                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đã chuyển thành file Dicom...", ": Convert Dicom completely"));
                                                        v_blnHasConvertRawin2DicomFile = true;
                                                        lblAutoDcmConverter.ImageIndex = 0;
                                                        IsUsingDicomConverter = true;
                                                    }
                                                    else
                                                    {

                                                        File.WriteAllBytes(RAWFilePath, bytes);
                                                        lblAutoDcmConverter.ImageIndex = 0;
                                                        IsUsingDicomConverter = true;
                                                    }
                                                }
                                                else
                                                {
                                                    lblAutoDcmConverter.ImageIndex = 0;
                                                    IsUsingDicomConverter = true;
                                                    File.WriteAllBytes(RAWFilePath, bytes);
                                                }
                                            }
                                            else
                                            {
                                                lblAutoDcmConverter.ImageIndex = 0;
                                                IsUsingDicomConverter = true;
                                                File.WriteAllBytes(RAWFilePath, bytes);
                                            }
                                            Thread.Sleep(100);
                                            mdlStatic.isDisplayImg = false;
                                            isLoadding = false;
                                        }
                                        catch (Exception ex)
                                        {
                                            AppLogger.LogAction.LogActions("==>Get Image From FPD().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex.ToString());
                                        }
                                        finally
                                        {
                                            #region "Reset Allengers GCOM"
                                            blnCheckSignalAfterEXPCommand = false;
                                            blnCheckSignalAfterSetKvpMas = false;
                                            CountOfResendingEXPCmd = 0;
                                            CountOfResendingDataFrame = 0;
                                            isSendbyCmd = true;
                                            isSendbyCmdExp = true;

                                            preventsenddata = false;
                                            preventExp = true;
                                            _SetEnableButton(true);
                                            _SetTextBtn("Ready");
                                            cmdReady.Tag = "R";
                                            State = 0;
                                            #endregion
                                            cmdReady.Enabled = m_objCurrScheduledControl != null && m_objCurrScheduledControl.Status == 0;
                                            pnlScheduled.Enabled = cmdReady.Enabled;
                                            if (!IsUsingDicomConverter)
                                            {
                                                string RAWFilePath1 = txtImgDir.Text + @"\" + RAWFileNameWillbeCreated + ".RAW";

                                                // Lấy tên file vừa tạo trong thư mục DownFolder
                                                string IMGfileHasJustCreated = Directory.GetFiles(txtImgDir.Text)[0];

                                                // Đổi tên thành file .raw
                                                File.Move(IMGfileHasJustCreated, RAWFilePath1);
                                            }
                                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đang hiển thị ảnh...", ": Displaying Img"));
                                            AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang hiển thị ảnh...", ": Displaying Img"));
                                            _IAr = BeginInvoke(new DisplayImg(ViewImg));
                                            fileCreated = true;

                                        }
                                    }
                                }
                                else
                                {
                                    PlayBeepAbort();
                                }
                            }
                            else if (fpdFlag == 3)
                            {
                                fpdFlag = 3;
                            }
                            else fpdFlag = 0;
                            fpdFlag = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogAction.LogActions("==>FPD550 Callback Funtion().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex.ToString());
                Utility.ShowMsg(ex.Message, "Lỗi");
            }
        }

        #endregion


        #endregion


        #region FPDEthernet-F500,560,750,760

      
        #region API Declaration
        [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int ChangeWindowOpenTime(int Cmd);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ConnectDevice")]
         public static extern int ConnectDevice_FPDEU();
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int ConnectNetwork([MarshalAs(UnmanagedType.LPStr)] string pSSID);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int CreateGainMap(ref UInt16 pInputS, int PImageNum, int TImageNum, int Index);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "EnableDebugMessage")]
         public static extern void EnableDebugMessage_FPDEU(bool Flag);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int EnableDetector(int Cmd);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int FinalizeDetector();
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         //public static extern HBITMAP __stdcall GetBitmapHandle();
         public static extern string GetCurrentSSID();
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern string GetDeviceSSID();
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern unsafe ushort* GetImageBuffer();
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetIPPrefix")]
         public static extern string GetIPPrefix_FPDEU();
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetMinMax")]
         public static extern int GetMinMax_FPDEU(ref UInt16 pImgBuf, ref int pMin, ref int pMax);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int GetModelType(int PanelIndex);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int GetPanelIndex();
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetSerialMessage")]
         public static extern int GetSerialMessage_FPDEU();
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int GetSSIDCount();
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int GetSSIDList(ref char ppSSIDs);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern bool GetTemperature(ref int pTemperature);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int InitializeDetector(int Model);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "LoadFilter")]
         public static extern int LoadFilter_FPDEU([MarshalAs(UnmanagedType.LPStr)] string pPathMap);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int LoadGainMap([MarshalAs(UnmanagedType.LPStr)] string pPathMap);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int LoadPanelMap([MarshalAs(UnmanagedType.LPStr)] string pPathMap);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int LoadPixelMap([MarshalAs(UnmanagedType.LPStr)] string pPathMap);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "OpenConnection")]
         public static extern bool OpenConnection_FPDEU();
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SaveGainMap")]
         public static extern int SaveGainMap_FPDEU([MarshalAs(UnmanagedType.LPStr)] string pPathMap);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SavePixelMap")]
         public static extern int SavePixelMap_FPDEU([MarshalAs(UnmanagedType.LPStr)] string pPathMap);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int SelectPanel(int Index);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern void SetCallback(MulticastDelegate pCallbackFunc, IntPtr pCustomData);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int SetCalibrationMode(int Index);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int SetDeviceSSID([MarshalAs(UnmanagedType.LPStr)] string pSSID);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetIPPrefix")]
         public static extern int SetIPPrefix_FPDEU([MarshalAs(UnmanagedType.LPStr)] string pIPPrefix);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "StartReceiver")]
         public static extern bool StartReceiver_FPDEU(int Port);
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "UseWiredNetwork")]
         public static extern int UseWiredNetwork_FPDEU();
         [DllImport("DRTECH_LAN.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int UseWirelessNetwork();

      


        #endregion

        #region attributies

     public const int	IDC_CONNECTION_SUCCEEDED_FPDEU =   0x1001;
     public const int IDC_CONNECTION_FAILED_FPDEU = 0x1002;
     public const int IDC_IP_CHANGED_FPDEU = 0x1003;
     public const int IDC_DEVICE_READY_FPDEU = 0x1004;
     public const int IDC_FILE_RECEIVED_FPDEU = 0x1005;
     public const int IDC_DEBUG_MESSAGE_FPDEU = 0x1006;
     public const int IDC_SERIAL_MESSAGE_FPDEU = 0x1007;
     public const int IDC_XCF_RECEIVED_FPDEU = 0x1008;
     public const int IDC_IMGFILE_CREATED_FPDEU = 0x1009;
     public const int IDC_CONNECTED_FPDEU = 0x1010;
     public const int IDC_DISCONNECTED_FPDEU = 0x1011;
     public const int IDC_DETECTOR_INITIALIZED_FPDEU = 0x1012;
     public const int XRAY_RECEIVER_PORT_1_FPDEU = 8002;
     public const int XRAY_RECEIVER_PORT_2_FPDEU = 8003;
     public const string XRAY_IP_PREFIX_FPDEU = "192.168.250.";
     public const string XRAY_WIRED_IP_FPDEU = "192.168.0.10";
     public const string XRAY_WIRED_MASK_FPDEU = "255.255.255.0";
     public const int NET_TYPE_WIRED_FPDEU = 0;
     public const int NET_TYPE_WIRELESS_FPDEU = 1;
     public const int PANEL1_FPDEU = 0;
     public const int PANEL2_FPDEU = 1;
     public const int DXF500LAN_FPDEU = 0;
     public const int DXF560LAN_FPDEU = 1;
     public const int DXF750ELAN_FPDEU = 2;
     public const int DXF760LAN_FPDEU = 3;
     public int ModelType = 0;
     public  string m_AppPath = Application.StartupPath;
     bool Wired = false;

        #endregion

        #region Delegate Declaration
      
        #endregion

        #region Delegate Callback Processing

     public void MessageProc_FPDEU(int MsgType, string pMsg, ref IntPtr test)
     {
         try
         {
             string _action = "";
             int Ret = 0;
             switch (MsgType)
             {
                 case IDC_CONNECTED_FPDEU:
                     AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ":Thiết lập kết nối với FPD...", ": FPD established"));
                     AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Thiết lập kết nối với FPD...", ": FPD established"));
                     _action = Wired ? "UseWiredNetwork()" : "UseWirelessNetwork()";
                     AppLogger.LogAction.AddLog2List(lstFPD560, _action);
                     Ret = Wired ? UseWiredNetwork_FPDEU() : UseWirelessNetwork();
                     break;
                 case IDC_DISCONNECTED_FPDEU:
                       AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ":Mất kết nối với FPD...", ": Connection lost"));
                       AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, ":Mất kết nối với FPD...", ": Connection lost"));
                     
                     break;
                 case IDC_CONNECTION_SUCCEEDED_FPDEU:
                     AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ":Đã kết nối với FPD...", ": WLAN Connection succeeded"));
                     AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đã kết nối với FPD...", ": WLAN Connection succeeded"));
                     break;
                 case IDC_IP_CHANGED_FPDEU:
                     ConnectDevice_FPDEU();
                     break;
                 case IDC_DEVICE_READY_FPDEU:
                     blnFPDReady = true;
                     AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Tấm FPD sẵn sàng chụp...", ": FPD Ready"));
                     AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Tấm FPD sẵn sàng chụp...", "FPD Ready"));
                     break;
                 case IDC_XCF_RECEIVED_FPDEU:
                     v_blnHasConvertRawin2DicomFile = false;
                     AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, "Xác định tên file RAW...", "Detecting RAW file name..."));
                     AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Xác định tên file RAW...", "Getting RAW Data..."));
                     ScheduledControl _selected = GetSelectedScheduled();
                     bool _isSelected = true;
                     string RAWFilePath = "";
                     string _SubDirLv1 = SubDirLv1();
                     string _SubDirLv2_Patient = SubDirLv2_Patient();
                     if (_SubDirLv1.Trim() == "" || _SubDirLv2_Patient.Trim() == "") break;
                     //Tạo các thư mục theo cấp ngày\Bệnh nhân(Mã bệnh nhân _ Tên Bệnh nhân _ Tuổi)
                     if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1)) Directory.CreateDirectory(txtImgDir.Text + @"\" + SubDirLv1());
                     if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient)) Directory.CreateDirectory(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient);
                     //Kiểm tra nếu chưa chọn thủ tục nào thì cần lưu ảnh thành tên file theo định dạng
                     //YYYY_MM_DD_HH_mm_ss
                     if (_selected == null || RAWFileNameWillbeCreated == "NONE_SELECTED")
                     {
                         _isSelected = false;
                         RAWFileNameWillbeCreated = "NONE_SELECTED_" + Utility.GetYYYYMMDDHHMMSS(DateTime.Now);
                         if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED")) Directory.CreateDirectory(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED");
                         RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED\" + RAWFileNameWillbeCreated + ".RAW";
                     }
                     else
                     {
                         //Kiểm tra xem Schedule đang chọn có ảnh chưa. Nếu đã có ảnh thì thực hiện theo tùy chọn phần cấu hình hệ thống
                         if (_selected.Status > 0)
                         {
                             AppLogger.LogAction.AddLog2List(lstFPD560, "BEGIN: Trạng thái dịch vụ đang chọn đã có ảnh");
                             
                                 RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\" + RAWFileNameWillbeCreated + ".RAW";//RAWFileNameWillbeCreated=REGNUM+_Detail_ID+_ACode+_Pcode tại phần click của Scheduled
                         }
                         else
                             RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\" + RAWFileNameWillbeCreated + ".RAW";//RAWFileNameWillbeCreated=REGNUM+_Detail_ID+_ACode+_Pcode tại phần click của Scheduled
                     }
                     AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang lấy dữ liệu thô...", "Getting RAW Data..."));
                     AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang lấy dữ liệu thô...", "Getting RAW Data..."));
                     if (m_bSaveImgFile)
                     {
                        //No code 
                     }
                     else
                     {
                         unsafe
                         {
                             try
                             {
                                 #region Autodetect size based on FPD Type
                                 switch (GetModelType(GetPanelIndex()))
                                 {
                                     case 0: IMAGE_WIDTH = 2560; IMAGE_HEIGHT = 3072; break;
                                     case 1: IMAGE_WIDTH = 2560; IMAGE_HEIGHT = 3072; break;
                                     case 2: IMAGE_WIDTH = 2560; IMAGE_HEIGHT = 2560; break;
                                     case 3: IMAGE_WIDTH = 2560; IMAGE_HEIGHT = 2560; break;
                                     default:
                                         break;
                                 }
                                 #endregion

                                 ushort* image = GetImageBuffer();

                                 var bytes = new byte[IMAGE_HEIGHT * IMAGE_WIDTH * sizeof(ushort)];
                                 Marshal.Copy((IntPtr)image, bytes, 0, IMAGE_HEIGHT * IMAGE_WIDTH * sizeof(ushort));
                                 if (IsCanFreeColTaskMem())
                                 {
                                     Marshal.FreeCoTaskMem((IntPtr)image);
                                     AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": FreeCoTaskMem");
                                 }
                                 string _RAWFilePath1 = txtImgDir.Text + @"\" + RAWFileNameWillbeCreated + ".RAW";
                                 //Luôn lưu file raw trước
                                 try
                                 {
                                     pnlScheduled.Enabled = false;
                                     try2RenameExistedFile(RAWFilePath);
                                     File.WriteAllBytes(RAWFilePath, bytes);
                                     AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Lưu xong ảnh thô...", ": SAVE RAW IMAGE SUCCESSFUL"));
                                     AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Lưu xong ảnh thô...", ": SAVE RAW IMAGE SUCCESSFUL"));
                                 }
                                 catch
                                 {
                                 }
                                 if (IsUsingDicomConverter)//Tự động convert thành file Dicom
                                 {
                                     DataRow dr = MakeDcmConverterInfor(_selected);
                                     if (dr != null)
                                     {
                                         if (DicomConverter.DicomConverter.Convert2Dicom(bytes, RAWFilePath, dr, true, AutoWLPath))
                                         {

                                             AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đã chuyển thành file Dicom...", ": Convert Dicom completely"));
                                             AutoUpdateResultAfterCapturingPictureFromModality(_selected);
                                             AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đã chuyển thành file Dicom...", ": Convert Dicom completely"));
                                             v_blnHasConvertRawin2DicomFile = true;
                                             lblAutoDcmConverter.ImageIndex = 0;
                                             IsUsingDicomConverter = true;
                                         }
                                         else
                                         {

                                             File.WriteAllBytes(RAWFilePath, bytes);
                                             lblAutoDcmConverter.ImageIndex = 0;
                                             IsUsingDicomConverter = true;
                                         }
                                     }
                                     else
                                     {
                                         lblAutoDcmConverter.ImageIndex = 0;
                                         IsUsingDicomConverter = true;
                                         File.WriteAllBytes(RAWFilePath, bytes);
                                     }
                                 }
                                 else
                                 {
                                     lblAutoDcmConverter.ImageIndex = 0;
                                     IsUsingDicomConverter = true;
                                     File.WriteAllBytes(RAWFilePath, bytes);
                                 }
                                 Thread.Sleep(100);
                                 mdlStatic.isDisplayImg = false;
                                 isLoadding = false;

                             }
                             catch
                             {
                             }
                             finally
                             {
                                 #region "Reset Allengers GCOM"
                                 blnCheckSignalAfterEXPCommand = false;
                                 blnCheckSignalAfterSetKvpMas = false;
                                 CountOfResendingEXPCmd = 0;
                                 CountOfResendingDataFrame = 0;
                                 isSendbyCmd = true;
                                 isSendbyCmdExp = true;

                                 preventsenddata = false;
                                 preventExp = true;
                                 _SetEnableButton(true);
                                 _SetTextBtn("Ready");
                                 cmdReady.Tag = "R";
                                 State = 0;
                                 #endregion

                                 #region reset AMECCOM
                                 ResetValue();
                                 _EnableControl(cmdReady, true, "Ready");
                                 isInProgress = false;
                                 #endregion

                                 cmdReady.Enabled = m_objCurrScheduledControl != null && m_objCurrScheduledControl.Status == 0;
                                 pnlScheduled.Enabled = cmdReady.Enabled;
                                 if (!IsUsingDicomConverter)
                                 {
                                     string RAWFilePath1 = txtImgDir.Text + @"\" + RAWFileNameWillbeCreated + ".RAW";

                                     // Lấy tên file vừa tạo trong thư mục DownFolder
                                     string IMGfileHasJustCreated = Directory.GetFiles(txtImgDir.Text)[0];

                                     // Đổi tên thành file .raw
                                     File.Move(IMGfileHasJustCreated, RAWFilePath1);
                                 }
                                 AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đang hiển thị ảnh...", ": Displaying Img"));
                                 AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang hiển thị ảnh...", ": Displaying Img"));
                                 _IAr = BeginInvoke(new DisplayImg(ViewImg));
                                 fileCreated = true;

                             }
                         }
                     }
                     break;
                 case IDC_IMGFILE_CREATED_FPDEU:
                     AppLogger.LogAction.ShowEventStatus(lblFPDStatus, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đã tạo file img...", ": Image File Created"));
                     break;
                 case IDC_DEBUG_MESSAGE_FPDEU:
                     break;
                 case IDC_SERIAL_MESSAGE_FPDEU:
                    
                     #region comments
                     //0
                     //UnKnown
                     //1
                     //Ready/PREP
                     //2
                     //Exposure
                     //3
                     //Ready/Prep Off
                     //6
                     //Window open time is default
                     //7
                     //Window open time is 1.0sec
                     //8
                     //Window open time is 1.5sec
                     //9
                     //Window open time is 2.0sec
                     #endregion

                     int i = GetSerialMessage_FPDEU();
                     switch (i)
                     {
                         case 0:

                             break;
                         case 1:
                             AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Ready/PREP");
                             break;
                         case 2:
                             AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Exposure...");
                             break;
                         case 3:
                             AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Ready/Prep Off");
                             break;
                     }
                    


                     break;
                 case IDC_DETECTOR_INITIALIZED_FPDEU:
                     AppLogger.LogAction.ShowEventStatus(lblFPDStatus,  MultiLanguage.GetText(globalVariables.DisplayLanguage,"FPD đã khởi tạo xong...","Detector Initialized"));
                     EnableDetector(1);
                     break;
                 default:
                     break;
             }
         }
         catch (Exception ex)
         {
             Utility.ShowMsg("Lỗi trong hàm CallBackFunc:\n" + ex.ToString(), "Lỗi");
         }
     }
        #endregion

        #region Init FPDEU
     bool InitPanel_EU(int PanelIndex, int ModelIndex, int ReceiverPort)
     {
         try
         {
             if (ModelIndex <= -1) return false;
             //lstPresiquitesFiles = new List<string>();
             //lstPresiquitesFiles.AddRange(new string[] { pathMap_A, pathGmp_A, "FLAATZ-560.FIL" });
             //if (_FPDMode == AppType.AppEnum.FPDMode.DualMode) lstPresiquitesFiles.AddRange(new string[] { pathMap_B, pathGmp_B });
             //if (!EnoughFiles(lstPresiquitesFiles)) return false;
             bool Ret = false;
             int OldPanelIndex = GetPanelIndex();

             if (SelectPanel(PanelIndex) == 1)
             {
                 if (InitializeDetector(ModelIndex) != 0)
                 {
                     string MapFileName = "";
                     string GmpFileName = "";
                     string PmpFileName = "";
                     if (GetMapFileNames(ModelIndex, ref MapFileName, ref GmpFileName, ref PmpFileName))
                     {
                         if ((LoadPixelMap(MapFileName) == 1) &&
                             (LoadGainMap(GmpFileName) == 1))
                         {
                             bool UseFilter = false;
                             string FilterPath = "";
                             // If it fail to load the panel map it is not affect to run the program.
                             LoadPanelMap(PmpFileName);
                             switch (ModelIndex)
                             {
                                 case DXF500LAN_FPDEU:
                                     FilterPath = m_AppPath + "FLAATZ-500.FIL";
                                     UseFilter = true;
                                     break;

                                 case DXF560LAN_FPDEU:
                                     FilterPath = m_AppPath + "FLAATZ-560.FIL";
                                     UseFilter = true;
                                     break;

                                 case DXF750ELAN_FPDEU:
                                     FilterPath = m_AppPath + "FLAATZ-750E.FIL";
                                     UseFilter = true;
                                     break;

                                 case DXF760LAN_FPDEU:
                                     FilterPath = m_AppPath + "FLAATZ-760.FIL";
                                     UseFilter = true;
                                     break;
                             }

                             if (UseFilter)
                                 if (LoadFilter_FPDEU(FilterPath) != 1)
                                 {
                                     AppLogger.LogAction.AddLog2List(lstFPD560, "LoadFilter Failed");
                                     AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "LoadFilter Failed");
                                 }

                             SetCallback(_CallBackFuncFPDEU, IntPtr.Zero);
                             AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": RegisterCallbackFunc success");
                             AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "RegisterCallbackFunc success");
                             EnableDebugMessage_FPDEU(true);
                             if (OpenConnection_FPDEU())
                             {
                                 AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": Open Connection success");
                                 AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Open Connection success");
                             }
                             else
                             {
                                 AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": Open Connection failed");
                                 AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Open Connection failed");
                             }
                             SetIPPrefix_FPDEU(IPPrefix);
                             StartReceiver_FPDEU(ReceiverPort);
                             //will call UseWiredNetwork()
                             OnselectPanel(PANEL1_FPDEU, true);
                             Ret = true;
                         }
                     }
                 }
             }
             SelectPanel(OldPanelIndex);

             return Ret;
         }
         catch (Exception ex)
         {
             new frm_LargeMsgBoxOK(MultiLanguage.GetText(globalVariables.DisplayLanguage, "CẢNH BÁO", "WARNING"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Lỗi khi Init tấm: " + ex.Message, "Init Panel Exception: " + ex.Message), MultiLanguage.GetText(globalVariables.DisplayLanguage, "ĐÃ HIỂU", "I SEE"), "").ShowDialog();
             AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ":" + ex.Message);
             AppLogger.LogAction.ShowEventStatus(lblFPDStatus, ex.Message);
             return false;
         }
     }
     void OnselectPanel(int NewPanelIdx, bool Wired)
     {


         if (Connect(NewPanelIdx, Wired))
         {
             AppLogger.LogAction.AddLog2List(lstFPD560, "Connect Panel " + NewPanelIdx + " succeeded...");
             AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Connect Panel " + NewPanelIdx + " succeeded ");
             return;
         }
         else
         {
             AppLogger.LogAction.AddLog2List(lstFPD560, "Connect Panel " + NewPanelIdx + " failed...");
             AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Connect Panel " + NewPanelIdx + " failed...");
         }

         ComparePanelSelected(NewPanelIdx);
     }
        /// <summary>
        /// Check lại Panel sau khi kiểm tra bằng chính API
        /// </summary>
     void ComparePanelSelected(int panelIdx)
     {
         int currentPanelIdx=GetPanelIndex();
         if (currentPanelIdx != panelIdx)//Chứng tỏ chưa Select được Panel
         {
             AppLogger.LogAction.AddLog2List(lstFPD560, "Connect Panel " + panelIdx + " failed...");
             AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Connect Panel " + panelIdx + " failed...");
         }
     }
       bool Connect(int PanelIndex, bool Wired)
       {
           if ((PanelIndex == PANEL1_FPDEU) || (PanelIndex == PANEL2_FPDEU))
           {
               int Ret = SelectPanel(PanelIndex);
               AppLogger.LogAction.AddLog2List(lstFPD560, "SelectPanel(" + PanelIndex .ToString()+ ")=" + Ret.ToString());
               if (Ret == 1)
               {
                    Ret = Wired ? UseWiredNetwork_FPDEU() : UseWirelessNetwork();
                   AppLogger.LogAction.AddLog2List(lstFPD560, "UseWiredNetwork="+Ret.ToString());
                   return Ret == 1;
               }
           }
           return false;
       }
       bool GetMapFileNames(int ModelIndex, ref string MapFileName, ref string GmpFileName, ref string PmpFileName)
       {
           string s;
           switch (ModelIndex)
           {
               case 0: s = @"\FLAATZ-500"; break;
               case 1: s = @"\FLAATZ-560"; break;
               case 2: s = @"\FLAATZ-750E"; break;
               case 3: s = @"\FLAATZ-760"; break;
               default:
                   return false;
           }
           MapFileName = m_AppPath + s + ".MAP";
           GmpFileName = m_AppPath + s + ".GMP";
           PmpFileName = m_AppPath + s + ".PMP";

           return true;
       }
    
          

           

        #endregion


        #endregion

        #region FPD560
        private static bool fileCreated = false;
        private const string STR_DRTech_F560_LAN_REF = @"DRTech_F560_LAN_REF.dll";
        #region API Declaration
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct MEMORYSTATUSEX
        {

            /// DWORD->unsigned int  
            public uint dwLength;

            /// DWORD->unsigned int  
            public uint dwMemoryLoad;

            /// DWORDLONG->ULONGLONG->unsigned __int64  
            public ulong ullTotalPhys;

            /// DWORDLONG->ULONGLONG->unsigned __int64  
            public ulong ullAvailPhys;

            /// DWORDLONG->ULONGLONG->unsigned __int64  
            public ulong ullTotalPageFile;

            /// DWORDLONG->ULONGLONG->unsigned __int64  
            public ulong ullAvailPageFile;

            /// DWORDLONG->ULONGLONG->unsigned __int64  
            public ulong ullTotalVirtual;

            /// DWORDLONG->ULONGLONG->unsigned __int64  
            public ulong ullAvailVirtual;

            /// DWORDLONG->ULONGLONG->unsigned __int64  
            public ulong ullAvailExtendedVirtual;
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);
        //Functions for System
        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool Sys_Allocation_F560();

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int Sys_Free();

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenConnection();

        //Functions for Calibration
        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int CalibrationMode(int Index);

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int LoadGainMap([MarshalAs(UnmanagedType.LPStr)] string pPathMap, int Index);

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int LoadPixelMap([MarshalAs(UnmanagedType.LPStr)] string pPathMap, int Index);

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int LoadFilter([MarshalAs(UnmanagedType.LPStr)] string pPathMap);

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int Map_Calibration(ref UInt16 pInputS, int P_ImageNum, int T_ImageNum, int Index);

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int PanelSelect(int Index);

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SaveGainMap([MarshalAs(UnmanagedType.LPStr)] string pPathMap);

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SavePixelMap([MarshalAs(UnmanagedType.LPStr)] string pPathMap);

        //Functions for Acquisition
        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int ConnectDevice();

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int Detector_Enable(int cmd);

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern string GetDownFolder();

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern string GetIPPrefix();

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void GetMinMax(ref UInt16 pImgBuf, ref int Min, ref int Max);

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetSerialMessage();

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void EnableDebugMessage(bool bFlag);

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe ushort* MakeImgBuf();

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void MakeImgFile();

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        //public static extern void RegisterCallbackFunc(MessageProcDelegate pCallbackFunc, IntPtr CustomData);
        public static extern void RegisterCallbackFunc(MulticastDelegate messageProc, IntPtr CustomData);

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDownFolder(string pFolder);

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SetIPPrefix(string pIPPrefix);

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartReceiver(int Port);

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int UseWiredNetwork();

        [DllImport("DRTech_F560_LAN_REF.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int WindowOpenTime_Change(int cmd);

        #endregion

        #region attributies

        public static string pathMap_A = "PANEL_A\\FLAATZ-560.MAP";
        public static string pathGmp_A = "PANEL_A\\FLAATZ-560.GMP";

        public static string pathMap_B = "PANEL_B\\FLAATZ-560.MAP";
        public static string pathGmp_B = "PANEL_B\\FLAATZ-560.GMP";


        public static bool DualMode = false;
        public static string IPPrefix = "192.168.250.";
        public static string IPPrefix2 = "192.168.250.";
        public static int XRAY_RECEIVER_PORT = 8002;
        public static int XRAY_RECEIVER_PORT2 = 8002;
        public static int IMAGE_WIDTH2 = 2560;
        public static int IMAGE_HEIGHT2 = 3072;
        public static bool m_bSaveImgFile = false;

        public static int IMAGE_WIDTH = 2560;
        public static int IMAGE_HEIGHT = 3072;
        public static string DownFolder = @"C:\Capture";

        public const int IDC_IP_CHANGED = 0x1003;
        public const int IDC_DEVICE_READY = 0x1004;
        public const int IDC_DEBUG_MESSAGE = 0x1006;
        public const int IDC_SERIAL_MESSAGE = 0x1007;
        public const int IDC_XCF_RECEIVED = 0x1008;
        public const int IDC_IMGFILE_CREATED = 0x1009;
        bool blnFPDReady = false;

        #endregion

        #region Delegate Declaration
        public delegate void MessageProcDelegate(int MsgType, string pMsg, ref IntPtr test);
        #endregion

        #region Delegate Callback Processing
        public void MessageProc(int MsgType, string pMsg, ref IntPtr test)
        {
            try
            {
                //AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MsgType.ToString());
                //AppLogger.LogAction.AddLog2List(lstFPD560, MsgType.ToString());
                //AppLogger.LogAction.AddLog2List(lstFPD560,"Chuỗi nguồn:" + pMsg);
                switch (MsgType)
                {
                    case IDC_IP_CHANGED:
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ":Đã kết nối với FPD...", ": FPD ConnectDevice"));
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đã kết nối với FPD...", "FPD ConnectDevice"));
                        AppLogger.LogAction.AddLog2List(lstFPD560, "ConnectDevice()");
                        ConnectDevice();
                        break;
                    case IDC_DEVICE_READY:
                        blnFPDReady = true;
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Tấm FPD sẵn sàng chụp...", ": FPD Ready"));
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Tấm FPD sẵn sàng chụp...", "FPD Ready"));
                        break;
                    case IDC_XCF_RECEIVED:
                        v_blnHasConvertRawin2DicomFile = false;
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, "Xác định tên file RAW...", "Detecting RAW file name..."));
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Xác định tên file RAW...", "Getting RAW Data..."));
                        ScheduledControl _selected = GetSelectedScheduled();
                        bool _isSelected = true;
                        string RAWFilePath = "";
                        string _SubDirLv1 = SubDirLv1();
                        string _SubDirLv2_Patient = SubDirLv2_Patient();
                        if (_SubDirLv1.Trim() == "" || _SubDirLv2_Patient.Trim() == "") break;
                        //Tạo các thư mục theo cấp ngày\Bệnh nhân(Mã bệnh nhân _ Tên Bệnh nhân _ Tuổi)
                        if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1)) Directory.CreateDirectory(txtImgDir.Text + @"\" + SubDirLv1());
                        if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient)) Directory.CreateDirectory(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient);
                        //Kiểm tra nếu chưa chọn thủ tục nào thì cần lưu ảnh thành tên file theo định dạng
                        //YYYY_MM_DD_HH_mm_ss
                        if (_selected == null || RAWFileNameWillbeCreated == "NONE_SELECTED")
                        {
                            _isSelected = false;
                            RAWFileNameWillbeCreated = "NONE_SELECTED_" + Utility.GetYYYYMMDDHHMMSS(DateTime.Now);
                            if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED")) Directory.CreateDirectory(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED");
                            RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED\" + RAWFileNameWillbeCreated + ".RAW";
                        }
                        else
                        {
                            //Kiểm tra xem Schedule đang chọn có ảnh chưa. Nếu đã có ảnh thì thực hiện theo tùy chọn phần cấu hình hệ thống
                            if (_selected.Status > 0)
                            {
                               
                                    RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\" + RAWFileNameWillbeCreated + ".RAW";//RAWFileNameWillbeCreated=REGNUM+_Detail_ID+_ACode+_Pcode tại phần click của Scheduled
                            }
                            else
                                RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\" + RAWFileNameWillbeCreated + ".RAW";//RAWFileNameWillbeCreated=REGNUM+_Detail_ID+_ACode+_Pcode tại phần click của Scheduled
                        }
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang lấy dữ liệu thô...", "Getting RAW Data..."));
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang lấy dữ liệu thô...", "Getting RAW Data..."));
                        if (m_bSaveImgFile)
                        {
                            MakeImgFile();
                        }
                        else
                        {
                            unsafe
                            {
                                try
                                {
                                    ushort* image = MakeImgBuf();


                                   
                                    var bytes = new byte[IMAGE_HEIGHT * IMAGE_WIDTH * sizeof(ushort)];
                                    Marshal.Copy((IntPtr)image, bytes, 0, IMAGE_HEIGHT * IMAGE_WIDTH * sizeof(ushort));
                                    #region new CIP
                                    //ushort* image2 = null;
                                    //ushort* image3 = null;
                                    //ushort* image4 = null;

                                    //var bytes2 = new byte[IMAGE_HEIGHT * IMAGE_WIDTH * sizeof(ushort)];
                                    //var bytes3 = new byte[IMAGE_HEIGHT * IMAGE_WIDTH * sizeof(ushort)];
                                    //Step1(image, image2, (WORD)IMAGE_WIDTH, (WORD)IMAGE_HEIGHT, (WORD)0, (WORD)65535);
                                    //Step2(image2, image3, (WORD)IMAGE_WIDTH, (WORD)IMAGE_HEIGHT, 48767, 33535, 65535, 1);
                                    //Step3(image3, image4, (WORD)IMAGE_WIDTH, (WORD)IMAGE_HEIGHT, 0, 65535, 32, 32, 65536, 2.5f);
                                    #endregion
                                    
                                    if (IsCanFreeColTaskMem())
                                    {
                                        Marshal.FreeCoTaskMem((IntPtr)image);
                                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": FreeCoTaskMem");
                                    }
                                    string _RAWFilePath1 = txtImgDir.Text + @"\" + RAWFileNameWillbeCreated + ".RAW";
                                    //Luôn lưu file raw trước
                                    try
                                    {
                                        pnlScheduled.Enabled = false;
                                        try2RenameExistedFile(RAWFilePath);
                                        File.WriteAllBytes(RAWFilePath, bytes);
                                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Lưu xong ảnh thô...", ": SAVE RAW IMAGE SUCCESSFUL"));
                                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Lưu xong ảnh thô...", ": SAVE RAW IMAGE SUCCESSFUL"));
                                    }
                                    catch(Exception ex)
                                    {
                                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "2212:" + ex.Message);
                                    }
                                    if (IsUsingDicomConverter)//Tự động convert thành file Dicom
                                    {
                                        DataRow dr = MakeDcmConverterInfor(_selected);
                                        if (dr != null)
                                        {
                                            if (_isUseNewAPI)
                                            {
                                                FromRAW2Dicom(RAWFilePath);
                                                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đã chuyển thành file Dicom...", ": Convert Dicom completely"));
                                                AutoUpdateResultAfterCapturingPictureFromModality(_selected);
                                                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đã chuyển thành file Dicom...", ": Convert Dicom completely"));
                                                v_blnHasConvertRawin2DicomFile = true;
                                                lblAutoDcmConverter.ImageIndex = 0;
                                                IsUsingDicomConverter = true;
                                            }
                                            else
                                            {
                                                if (DicomConverter.DicomConverter.Convert2Dicom(bytes, RAWFilePath, dr, true, AutoWLPath))
                                                {

                                                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đã chuyển thành file Dicom...", ": Convert Dicom completely"));
                                                    AutoUpdateResultAfterCapturingPictureFromModality(_selected);
                                                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đã chuyển thành file Dicom...", ": Convert Dicom completely"));
                                                    v_blnHasConvertRawin2DicomFile = true;
                                                    lblAutoDcmConverter.ImageIndex = 0;
                                                    IsUsingDicomConverter = true;
                                                }
                                                else
                                                {

                                                    File.WriteAllBytes(RAWFilePath, bytes);
                                                    lblAutoDcmConverter.ImageIndex = 0;
                                                    IsUsingDicomConverter = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            lblAutoDcmConverter.ImageIndex = 0;
                                            IsUsingDicomConverter = true;
                                            File.WriteAllBytes(RAWFilePath, bytes);
                                        }
                                    }
                                    else
                                    {
                                        lblAutoDcmConverter.ImageIndex = 0;
                                        IsUsingDicomConverter = true;
                                        File.WriteAllBytes(RAWFilePath, bytes);
                                    }
                                    Thread.Sleep(100);
                                    mdlStatic.isDisplayImg = false;
                                    isLoadding = false;

                                }
                                catch (Exception ex)
                                {
                                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "2255:" + ex.Message);
                                }
                                finally
                                {
                                    #region "Reset Allengers GCOM"
                                    blnCheckSignalAfterEXPCommand = false;
                                    blnCheckSignalAfterSetKvpMas = false;
                                    CountOfResendingEXPCmd = 0;
                                    CountOfResendingDataFrame = 0;
                                    isSendbyCmd = true;
                                    isSendbyCmdExp = true;

                                    preventsenddata = false;
                                    preventExp = true;
                                    _SetEnableButton(true);
                                    _SetTextBtn("Ready");
                                    cmdReady.Tag = "R";
                                    State = 0;
                                    #endregion
                                    cmdReady.Enabled = m_objCurrScheduledControl != null && m_objCurrScheduledControl.Status == 0;
                                    pnlScheduled.Enabled = cmdReady.Enabled;
                                    if (!IsUsingDicomConverter)
                                    {
                                        string RAWFilePath1 = txtImgDir.Text + @"\" + RAWFileNameWillbeCreated + ".RAW";

                                        // Lấy tên file vừa tạo trong thư mục DownFolder
                                        string IMGfileHasJustCreated = Directory.GetFiles(txtImgDir.Text)[0];

                                        // Đổi tên thành file .raw
                                        File.Move(IMGfileHasJustCreated, RAWFilePath1);
                                    }
                                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đang hiển thị ảnh...", ": Displaying Img"));
                                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang hiển thị ảnh...", ": Displaying Img"));
                                    _IAr = BeginInvoke(new DisplayImg(ViewImg));
                                    fileCreated = true;

                                }
                            }
                        }
                        break;
                    case IDC_IMGFILE_CREATED:
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đã tạo file img...", ": Image File Created"));
                        break;
                    case IDC_DEBUG_MESSAGE:
                        break;
                    case IDC_SERIAL_MESSAGE:
                        #region comments
                        //0
                        //UnKnown
                        //1
                        //Ready/PREP
                        //2
                        //Exposure
                        //3
                        //Ready/Prep Off
                        //6
                        //Window open time is default
                        //7
                        //Window open time is 1.0sec
                        //8
                        //Window open time is 1.5sec
                        //9
                        //Window open time is 2.0sec
                        #endregion

                        int i = GetSerialMessage();
                        switch (i)
                        {
                            case 0:

                                break;
                            case 1:
                                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Ready/PREP");
                                break;
                            case 2:
                                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Exposure...");
                                break;
                            case 3:
                                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Ready/Prep Off");
                                break;
                        }
                        //if (i == 2)
                        //{
                        //    AppLogger.LogAction.AddLog2List(lstFPD560,DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage,": Đang bắn tia...",": Exposure..."));
                        //}
                        //if (i == 3)
                        //{
                        //    AppLogger.LogAction.AddLog2List(lstFPD560,DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage,": Tấm sẵn sàng chụp...",": FPD Ready"));
                        //    blnFPDReady = true;

                        //    //cmdReady.Enabled = _AppMode == AppMode.Demo ? true : blnFPDReady && GetSelectedScheduled() != null;
                        //    //pnlScheduled.Enabled = cmdReady.Enabled;
                        //    //cmdReadySignal.Image =DROCLibs.Properties.Resources.NotReady;
                        //    //cmdReady.BackColor = Color.CornflowerBlue;
                        //    //cmdReady.Text = "SẴN SÀNG CHỤP kVp=" + kVp.ToString() + " mA=" + mA.ToString() + " mAs=" + mAs.ToString();
                        //    //cmdReady.Tag = "R";
                        //}

                        //AppLogger.LogAction.AddLog2List(lstFPD560,"i:" + i.ToString());


                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
              
                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "2363:" + ex.Message);
                Utility.ShowMsg("Lỗi trong hàm CallBackFunc:\n" + ex.ToString(), "Lỗi");
            }
        }
      
        #endregion
      
        #region Init FPD
        void InitFPD560()
        {
            try
            {
                RegisterCallbackFunc(_CallBackFunc, IntPtr.Zero);
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": RegisterCallbackFunc success");
                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "RegisterCallbackFunc success");
                if (Sys_Allocation_F560())
                {
                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Sys_Allocation success");
                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": Sys_Allocation success");
                    if (((_FPDMode == AppType.AppEnum.FPDMode.SingleMode && LoadPixelMap(pathMap_A, 0) == 1 && LoadGainMap(pathGmp_A, 0) == 1)
                        || (_FPDMode == AppType.AppEnum.FPDMode.DualMode && LoadPixelMap(pathMap_A, 0) == 1 && LoadGainMap(pathGmp_A, 0) == 1 && LoadPixelMap(pathMap_B, 0) == 1 && LoadGainMap(pathGmp_B, 0) == 1)) &&
                         LoadFilter("FLAATZ-560.FIL") == 1)
                    {
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": LoadPixelMap success");
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "LoadPixelMap success");
                        bool blnSuccess = OpenConnection();
                        if (!blnSuccess)
                        {
                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": OpenConnection failed");
                            int icount = 0;
                            while (icount < 10 && !blnSuccess)
                            {
                                icount++;
                                try
                                {
                                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Thử khởi động lại lần: " + icount.ToString(), "try to restart: " + icount.ToString()));
                                    //KillProcess("DROC.exe");
                                    Sys_Free();
                                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": Sys_Free success");
                                }
                                catch
                                {
                                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Lần: " + icount.ToString() + " thất bại...", "Restart: " + icount.ToString() + " failed..."));
                                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": Sys_Free failed: " + icount.ToString());
                                    if (icount < 10) Thread.Sleep(5000);
                                }
                                finally
                                {
                                    blnSuccess = OpenConnection();
                                    if (!blnSuccess)
                                    {
                                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": OpenConnection " + icount.ToString() + "failed");
                                    }
                                }
                            }
                        }
                        if (!blnSuccess)
                        {
                            new frm_LargeMsgBoxOK(MultiLanguage.GetText(globalVariables.DisplayLanguage, "Cảnh báo...", "Warning..."), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Bộ nhớ đang có chút trục trặc. Bạn hãy tạm dừng chương trình và khởi động lại sau 1 phút. Nếu sau 3 lần khởi động vẫn còn tình trạng trên thì xin liên hệ với VietBaIT để được hỗ trợ kịp thời: Đào Văn Cường 09 15 15 01 48. Xin cám ơn!", "Faltal Error occurred. Pls shutdown application and restart in one minute. if the problem still occurs after 3 times of restart, then contact to VietBaIT to get supports: Dao Van Cuong 09 15 15 01 48"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đã hiểu", "I see"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Không hiểu", "Sorry")).ShowDialog();
                            //Application.Exit();
                        }
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": Open Connection success");
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Open Connection success");
                        SetIPPrefix(IPPrefix);
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": SetIPPrefix success");
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "SetIPPrefix success");
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": try to SetDownFolder " + txtImgDir.Text);
                        SetDownFolder(txtImgDir.Text);
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": SetDownFolder success");
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "SetDownFolder success");
                        StartReceiver(XRAY_RECEIVER_PORT);
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": StartReceiver success");
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "StartReceiver success");

                        int ret = UseWiredNetwork();
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": Init FPD560 completely");
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Init FPD560 completely");
                    }
                }

            }
            catch (Exception ex)
            {
                new frm_LargeMsgBoxOK(MultiLanguage.GetText(globalVariables.DisplayLanguage, "CẢNH BÁO", "WARNING"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Hệ thống chưa kết nối được với tấm cảm biến FPD. Bạn cần thoát khỏi ứng dụng và chờ đợi chút sau đó khởi động lại", "Not connected to FPD. Pls exit Application and wait for a few seconds, then restart Application"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "ĐÃ HIỂU", "I SEE"), "").ShowDialog();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ":" + ex.Message);
                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, ex.Message);

            }
        }
       
      
      
        #endregion


        #endregion

        #region Toshiba_new
        #region private fields
        // Lower API interface class handler
        private CInterfaceLower cIfLower;
        // Log class handler
        private CLog cLog;
        // Calibration callback function
        public delegate void CALIB_CALLBACK_t(Int32 eventID, IntPtr eventData, IntPtr userData, EVENTDATA ev);
        private CALIB_CALLBACK_t calilbCallBack = null;
        // Output to Log text box delegate for other thread
        private CLog.OutputLogHandler cLog_OutputLog_Delegate;
        // User callback function delegate for other thread
        private delegate void EVENT_CALLBACK_t(Int32 eventID, IntPtr eventData, IntPtr userData, EVENTDATA ev, FPD_ALARM fa, Byte[] imagePtr);
        private EVENT_CALLBACK_t CallbackFromFPD_Delegate;

        // Mutex for Marshal.Copy()
        private Mutex MarshalCopyMutex = new Mutex();

        // Mutex for this.txt_CallbackEvent
        private Mutex txt_CallbackMutex = new Mutex();

        // Exposure request switch
        private UInt32 ExposureRequestSwitch;
        public const UInt32 HARD_EXPOSURE = 0;
        public const UInt32 SOFT_EXPOSURE = 1;

        // Exposure mode
        private UInt32 ExposureMode;
        public const UInt32 SINGLE_EXPOSURE = 0;
        public const UInt32 DOUBLE_EXPOSURE = 1;
        #endregion

        #region public fields
        // Types of callback event (eventID)
        public enum TETD_EVENT_CALLBACK_ENUM
        {
            TETD_EVENT_EXP_END = 1,                 // Exposure end
            TETD_EVENT_EXP_IMG_THUMB,               // Thumbnail image collected
            TETD_EVENT_EXP_IMG_FULL,                // Full image collected
            TETD_EVENT_EXP_COMPLETE,                // Image process completed
            TETD_EVENT_OFFSET_COMPLETE,             // Offset LUT generated
            TETD_EVENT_OFFSET_PROGRESS,             // Offset LUT generating
            TETD_EVENT_FLATNESS_CHECK,              // Flatness check result
            TETD_EVENT_FPD_ALARM,                   // FPD system alarm
            TETD_EVENT_FPD_REBOOT_ERROR,            // Reset received
            TETD_EVENT_EXP_REQ_EXTERNAL_NOTIFTY,    // External trigger received
            TETD_EVENT_CMD_TIMEOUT,                 // Command timeout
            TETD_EVENT_CONNECTION_ERROR,            // Connection acknowledge timeout
            TETD_EVENT_PRE_STBY_TIMEOUT             // Pre standby timeout
        }

        #region Others
        //***************************************************************************************************
        /*!
         * @brief		User callback function
         *
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[in]   Int32  eventID      Event ID.
         * @param[in]   IntPtr eventData    Event data.
         * @param[in]   IntPtr userData     User data.
         * 
         * @return		none
         */
        //***************************************************************************************************
        // UINT32 myEventData[3];
        [StructLayout(LayoutKind.Sequential)]
        public struct EVENTDATA
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public Int32[] myEventData;
        }

        // FPD_ALARM & FPD_REBOOT_ERROR
        [StructLayout(LayoutKind.Sequential)]
        public struct FPD_ALARM
        {
            public Int32 ev;
            public CInterfaceLower.FPD_CONDITION fc;
        }

        // Parameter from C++ thread to C# thread
        private struct ThreadParam
        {
            public Int32 eventID;
            public IntPtr eventData;
            public IntPtr userData;
        }
        #endregion

        Dictionary<int, string> dicFPDNo = new Dictionary<int, string>();
        string CurrentToshibaFPDNo = "";

        void InitFDX3543RP()
        {
            try
            {
                // Prepare delegate for other thread
                this.cLog_OutputLog_Delegate = new CLog.OutputLogHandler(cLog_OutputLog_Exec);
                this.CallbackFromFPD_Delegate = new EVENT_CALLBACK_t(CallbackFromFPD_Exec);

                // Create log class
                this.cLog = new CLog();
                // Attach output to Log text box handler
                this.cLog.OutputLog = new CLog.OutputLogHandler(this.cLog_OutputLog);

                // Display PC information
               // DisplayPCInfo();

                // Create lower API interface class
                IntPtr ptr = Marshal.AllocHGlobal(1024);    // 1024 byte for debug
                this.cIfLower = new CInterfaceLower(this.cLog, ptr);
                this.cIfLower.ActiveDetector = 0;

                // Setup ActiveDetector combo box
                for (int i = 0; i < this.cIfLower.ConnectFPDs; i++)
                {
                    dicFPDNo.Add(i, "FPD" + i.ToString());
                }
                if (0 < cIfLower.ConnectFPDs)
                {
                    CurrentToshibaFPDNo = dicFPDNo[0];
                }

                // Register user callback function (for debug)
                cIfLower.TetdFpdRegisterCallback(new CInterfaceLower.TETD_CALLBACK_t(this.CallbackFromFPD));

                // Show DLL version information
                UInt32 version;
                if (CInterfaceLower.E_ERR_TETD_CONTROLLER_ENUM.TETD_OK == this.cIfLower.TetdDllVer(out version))
                {
                    this.cLog.WriteLine("DLL version " + (version / 100).ToString("D1") + "." + (version % 100).ToString("D2"));
                }

                // Initialize FPD State as "Not Init"
                Connect_ToshibaNew();
                //this.rbt_FPDStateNotInit.Checked = true;
                //this.rbt_FPDStateStandby.Checked = false;
                //this.rbt_FPDStateWork.Checked = false;
                //this.rbt_FPDStateCalibration.Checked = false;

                // Disable Exposure button
                //this.btn_Exposure.Enabled = false;

                // Disable Calibration button
                //this.btn_Calibration.Enabled = false;

                // Disable To Double button
                //this.btn_ToDouble.Enabled = false;
            }
            catch (Exception ex)
            {
                AppLogger.LogAction.LogActions("==>InitFDX3543RP().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex.ToString());
                Utility.ShowMsg(ex.Message);
            }
        }
        private void DisplayPCInfo()
        {
            Microsoft.VisualBasic.Devices.ComputerInfo info = new Microsoft.VisualBasic.Devices.ComputerInfo();

            // Get OS version
            this.cLog.WriteLine("OS : " + info.OSFullName.ToString() + " " + System.Environment.OSVersion.ServicePack);

            // Get CPU information
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0",
                RegistryKeyPermissionCheck.ReadSubTree);    // This registry entry contains entry for processor information
            if (null != processor_name)
            {
                if (null != processor_name.GetValue("ProcessorNameString"))
                {
                    this.cLog.WriteLine("CPU : " + (String)processor_name.GetValue("ProcessorNameString") + " " +
                        (info.TotalPhysicalMemory / 1024.0 / 1024.0 / 1024.0).ToString("F2") + " GB");
                }
            }

            // Get disk size
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            String strSize = disk["Size"].ToString();
            String strFree = disk["FreeSpace"].ToString();
            double Size = double.Parse(strSize) / 1024.0 / 1024.0 / 1024.0;
            double Free = double.Parse(strFree) / 1024.0 / 1024.0 / 1024.0;
            this.cLog.WriteLine("HDD : " + (Size - Free).ToString("F1") + "GB / " + Size.ToString("F1") + "GB (Use/All)");

            // Get host name
            String hostname = Dns.GetHostName();
            // Get I/P address from host name
            IPAddress[] adrList = Dns.GetHostAddresses(hostname);
            foreach (IPAddress address in adrList)
            {
                // To display only the IP address of the V4
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    this.cLog.WriteLine("Network : " + address.ToString() + " (Active)");
                }
            }
        }
        private void cLog_OutputLog(String message)
        {
            // Delegate for other thread
            this.Invoke(this.cLog_OutputLog_Delegate, message);
        }
        private void Connect_ToshibaNew()
        {
            if (0 != this.cIfLower.ConnectFPDs)
            {
                // Register user callback function
                this.cIfLower.TetdFpdRegisterCallback(new CInterfaceLower.TETD_CALLBACK_t(this.CallbackFromFPD));

                // Call API wrapper
                if (CInterfaceLower.E_ERR_TETD_CONTROLLER_ENUM.TETD_OK == this.cIfLower.TetdFpdOpen((UInt32)this.cIfLower.ActiveDetector,
                    out this.ExposureRequestSwitch))
                {
                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Kết nối tấm Toshiba thành công");
                    // Check ExposureRequestSwitch
                    if (HARD_EXPOSURE == this.ExposureRequestSwitch)
                    {
                        //this.rbt_HardExposure.Checked = true;
                    }
                    else
                    {
                        //this.rbt_HardExposure.Checked = false;
                    }

                    // Change FPD State as "Standby"
                    EnterWorkMode();
                    //this.rbt_FPDStateNotInit.Checked = false;
                    //this.rbt_FPDStateStandby.Checked = true;
                    //this.rbt_FPDStateWork.Checked = false;
                    //this.rbt_FPDStateCalibration.Checked = false;

                    // Disable ActiveDetector combo box
                    // this.cmb_ActiveDetector.Enabled = false;

                    // Disable Exposure button
                    //this.btn_Exposure.Enabled = false;

                    // Disable Calibration button
                    //this.btn_Calibration.Enabled = false;

                    // Disable To Double button
                    //this.btn_ToDouble.Enabled = false;

                    // Get Correction button clicked
                    //this.btn_GetCorrection.PerformClick();

                    // Get Condition button clicked
                    //this.btn_GetCondition.PerformClick();
                }
                else
                {
                    // Error process (none)
                }
            }
        }
        private void EnterWorkMode()
        {
            this.ExposureMode = SINGLE_EXPOSURE;

            // Set Exposure mode
            //          this.cIfLower.TetdFpdExposureMode(this.ExposureMode);

            // Call API wrapper
            if (CInterfaceLower.E_ERR_TETD_CONTROLLER_ENUM.TETD_OK == this.cIfLower.TetdFpdWorkState(this.ExposureMode))
            {
                // Change FPD State as "Work"
                //this.rbt_FPDStateNotInit.Checked = false;
                //this.rbt_FPDStateStandby.Checked = false;
                //this.rbt_FPDStateWork.Checked = true;
                //this.rbt_FPDStateCalibration.Checked = false;
                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Sẵn sàng chụp");
                if (SOFT_EXPOSURE == this.ExposureRequestSwitch)
                {
                    // Enable Exposure button
                    //this.btn_Exposure.Enabled = true;
                }
                else
                {
                    // External trigger enabled
                    this.cLog.WriteLine("Ready to Exposure");
                }

                // Disable Calibration button
                //this.btn_Calibration.Enabled = false;

                // Enable To Double button
                //this.btn_ToDouble.Enabled = true;
            }
            else
            {
                // Error process (none)
            }
        }
        #region CallBackFunc
        // User callback function (C++ thread)
        private void CallbackFromFPD(Int32 eventID, IntPtr eventData, IntPtr userData)
        {
            try
            {
                // Parameter from C++ thread to C# thread
                ThreadParam Param = new ThreadParam();

                Param.eventID = eventID;
                Param.eventData = eventData;
                Param.userData = userData;

                // Start C# thread
                Thread CSthread = new Thread(new ParameterizedThreadStart(CallbackThread));
                CSthread.Start(Param);

                // Wait for Marshal.Copy()
                CSthread.Join();
            }
            catch
            {
            }
            // return from User callback function
        }

        // User callback function (C# thread)
        private void CallbackThread(object tParam)
        {
            try
            {
                // Get Mutex lock for Marshal.Copy()
                this.MarshalCopyMutex.WaitOne();

                // Parameter for delegate
                ThreadParam Param = new ThreadParam();

                Param.eventID = ((ThreadParam)tParam).eventID;
                Param.eventData = ((ThreadParam)tParam).eventData;
                Param.userData = ((ThreadParam)tParam).userData;

                EVENTDATA ev = new EVENTDATA();
                FPD_ALARM fa = new FPD_ALARM();
                Byte[] imagePtr = null;

                // Thumbnail image collected
                // Full image collected
                if ((TETD_EVENT_CALLBACK_ENUM.TETD_EVENT_EXP_IMG_THUMB == (TETD_EVENT_CALLBACK_ENUM)Param.eventID) ||
                    (TETD_EVENT_CALLBACK_ENUM.TETD_EVENT_EXP_IMG_FULL == (TETD_EVENT_CALLBACK_ENUM)Param.eventID))
                {
                    ev = (EVENTDATA)Marshal.PtrToStructure(Param.eventData, typeof(EVENTDATA));
                    imagePtr = new Byte[ev.myEventData[2]];

                    Marshal.Copy((IntPtr)ev.myEventData[1], imagePtr, 0, imagePtr.Length);
                }
                // Normal event
                if ((TETD_EVENT_CALLBACK_ENUM.TETD_EVENT_FPD_ALARM != (TETD_EVENT_CALLBACK_ENUM)Param.eventID) &&
                    (TETD_EVENT_CALLBACK_ENUM.TETD_EVENT_FPD_REBOOT_ERROR != (TETD_EVENT_CALLBACK_ENUM)Param.eventID))
                {
                    ev = (EVENTDATA)Marshal.PtrToStructure(Param.eventData, typeof(EVENTDATA));
                }
                // Alarm from FPD
                else
                {
                    fa = (FPD_ALARM)Marshal.PtrToStructure(Param.eventData, typeof(FPD_ALARM));
                }

                // Delegate for other thread
                this.BeginInvoke(this.CallbackFromFPD_Delegate, Param.eventID, Param.eventData, Param.userData, ev, fa, imagePtr);

                // Release Mutex lock for Marshal.Copy()
                this.MarshalCopyMutex.ReleaseMutex();
            }
            catch
            {
            }
        }
        void SaveImgFromFDX4343R_New(Byte[] imagePtr)
        {
            try
            {
                v_blnHasConvertRawin2DicomFile = false;
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đang nhận dữ liệu...", ": Getting RAW Data"));
                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang nhận dữ liệu...", ": Getting RAW Data"));
                ScheduledControl _selected = GetSelectedScheduled();
                bool _isSelected = true;
                string RAWFilePath = "";
                string _SubDirLv1 = SubDirLv1();
                string _SubDirLv2_Patient = SubDirLv2_Patient();
                if (_SubDirLv1.Trim() == "" || _SubDirLv2_Patient.Trim() == "") return;
                //Tạo các thư mục theo cấp ngày\Bệnh nhân(Mã bệnh nhân _ Tên Bệnh nhân _ Tuổi)
                if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1)) Directory.CreateDirectory(txtImgDir.Text + @"\" + _SubDirLv1);
                if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient)) Directory.CreateDirectory(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient);
                //Kiểm tra nếu chưa chọn thủ tục nào thì cần lưu ảnh thành tên file theo định dạng
                //YYYY_MM_DD_HH_mm_ss
                if (_selected == null || RAWFileNameWillbeCreated == "NONE_SELECTED")
                {
                    _isSelected = false;
                    RAWFileNameWillbeCreated = "NONE_SELECTED_" + Utility.GetYYYYMMDDHHMMSS(DateTime.Now);
                    if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED")) Directory.CreateDirectory(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED");
                    RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED\" + RAWFileNameWillbeCreated + ".RAW";
                }
                else
                {
                    //Kiểm tra xem Schedule đang chọn có ảnh chưa. Nếu đã có ảnh thì thực hiện theo tùy chọn phần cấu hình hệ thống
                    if (_selected.Status > 0)
                    {
                        AppLogger.LogAction.AddLog2List(lstFPD560, "BEGIN: Trạng thái dịch vụ đang chọn đã có ảnh");
                       
                            RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\" + RAWFileNameWillbeCreated + ".RAW";//RAWFileNameWillbeCreated=REGNUM+_Detail_ID+_ACode+_Pcode tại phần click của Scheduled
                    }
                    else
                        RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\" + RAWFileNameWillbeCreated + ".RAW";//RAWFileNameWillbeCreated=REGNUM+_Detail_ID+_ACode+_Pcode tại phần click của Scheduled
                }
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": FULLIMG READY EVENT");
                try
                {
                    unsafe
                    {
                        try
                        {


                            //if (IMAGE_HEIGHT != m_pSImage_Info.nHeight) IMAGE_HEIGHT = m_pSImage_Info.nHeight;
                            //if (IMAGE_WIDTH != m_pSImage_Info.nWidth) IMAGE_WIDTH = m_pSImage_Info.nWidth;
                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": W=" + IMAGE_WIDTH.ToString() + " H=" + IMAGE_HEIGHT.ToString());

                            //Luôn lưu file raw trước
                            try
                            {
                                pnlScheduled.Enabled = false;
                                try2RenameExistedFile(RAWFilePath);
                                File.WriteAllBytes(RAWFilePath, imagePtr);
                                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Lưu xong ảnh thô...", ": SAVE RAW IMAGE SUCCESSFUL"));
                                AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Lưu xong ảnh thô...", ": SAVE RAW IMAGE SUCCESSFUL"));
                            }
                            catch
                            {
                            }
                            if (IsUsingDicomConverter)//Tự động convert thành file Dicom
                            {
                                DataRow dr = MakeDcmConverterInfor(_selected);
                                if (dr != null)
                                {
                                    if (DicomConverter.DicomConverter.Convert2Dicom(imagePtr, RAWFilePath, dr, true, AutoWLPath))
                                    {

                                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đã chuyển thành file Dicom...", ": Convert Dicom completely"));
                                        AutoUpdateResultAfterCapturingPictureFromModality(_selected);
                                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đã chuyển thành file Dicom...", ": Convert Dicom completely"));
                                        v_blnHasConvertRawin2DicomFile = true;
                                        lblAutoDcmConverter.ImageIndex = 0;
                                        IsUsingDicomConverter = true;
                                    }
                                    else
                                    {

                                        File.WriteAllBytes(RAWFilePath, imagePtr);
                                        lblAutoDcmConverter.ImageIndex = 0;
                                        IsUsingDicomConverter = true;
                                    }
                                }
                                else
                                {
                                    lblAutoDcmConverter.ImageIndex = 0;
                                    IsUsingDicomConverter = true;
                                    File.WriteAllBytes(RAWFilePath, imagePtr);
                                }
                            }
                            else
                            {
                                lblAutoDcmConverter.ImageIndex = 0;
                                IsUsingDicomConverter = true;
                                File.WriteAllBytes(RAWFilePath, imagePtr);
                            }
                            Thread.Sleep(100);
                            mdlStatic.isDisplayImg = false;
                            isLoadding = false;
                        }
                        catch
                        {
                        }
                        finally
                        {
                            #region "Reset Allengers GCOM"
                            blnCheckSignalAfterEXPCommand = false;
                            blnCheckSignalAfterSetKvpMas = false;
                            CountOfResendingEXPCmd = 0;
                            CountOfResendingDataFrame = 0;
                            isSendbyCmd = true;
                            isSendbyCmdExp = true;

                            preventsenddata = false;
                            preventExp = true;
                            _SetEnableButton(true);
                            _SetTextBtn("Ready");
                            cmdReady.Tag = "R";
                            State = 0;
                            #endregion
                            cmdReady.Enabled = m_objCurrScheduledControl != null && m_objCurrScheduledControl.Status == 0;
                            pnlScheduled.Enabled = cmdReady.Enabled;
                            if (!IsUsingDicomConverter)
                            {
                                string RAWFilePath1 = txtImgDir.Text + @"\" + RAWFileNameWillbeCreated + ".RAW";

                                // Lấy tên file vừa tạo trong thư mục DownFolder
                                string IMGfileHasJustCreated = Directory.GetFiles(txtImgDir.Text)[0];

                                // Đổi tên thành file .raw
                                File.Move(IMGfileHasJustCreated, RAWFilePath1);
                            }
                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đang hiển thị ảnh...", ": Displaying Img"));
                            AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang hiển thị ảnh...", ": Displaying Img"));
                            _IAr = BeginInvoke(new DisplayImg(ViewImg));
                            fileCreated = true;
                        }
                    }
                }
                catch
                {
                }
            }
            catch
            {
            }
        }
        private void CallbackFromFPD_Exec(Int32 eventID, IntPtr eventData, IntPtr userData, EVENTDATA ev, FPD_ALARM fa, Byte[] imagePtr)
        {
            try
            {
                // Thumbnail image collected
                // Full image collected
                if ((TETD_EVENT_CALLBACK_ENUM.TETD_EVENT_EXP_IMG_THUMB == (TETD_EVENT_CALLBACK_ENUM)eventID) ||
                    (TETD_EVENT_CALLBACK_ENUM.TETD_EVENT_EXP_IMG_FULL == (TETD_EVENT_CALLBACK_ENUM)eventID))
                {
                    SaveImgFromFDX4343R_New(imagePtr);

                }
                // Image process completed
                else if (TETD_EVENT_CALLBACK_ENUM.TETD_EVENT_EXP_COMPLETE == (TETD_EVENT_CALLBACK_ENUM)eventID)
                {
                    this.ExposureMode = SINGLE_EXPOSURE;
                    //this.btn_ToDouble.Text = "To Double";

                    // Set Exposure mode
                    //              if (CInterfaceLower.E_ERR_TETD_CONTROLLER_ENUM.TETD_OK == this.cIfLower.TetdFpdExposureMode(this.ExposureMode))
                    //              {
                    if (SOFT_EXPOSURE == this.ExposureRequestSwitch)
                    {
                        // Enable Exposure button
                        //this.btn_Exposure.Enabled = true;
                    }
                    else
                    {
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Sẵn sàng chụp");
                        // External trigger enabled
                        this.cLog.WriteLine("Ready to Exposure");
                    }
                    //              }
                }
                // External trigger received
                else if (TETD_EVENT_CALLBACK_ENUM.TETD_EVENT_EXP_REQ_EXTERNAL_NOTIFTY == (TETD_EVENT_CALLBACK_ENUM)eventID)
                {
                    // If calibration callback function is registered
                    if (null != this.calilbCallBack)
                    {
                        // Call calibration callback function
                        this.calilbCallBack(eventID, eventData, userData, ev);
                    }
                    else
                    {
                        // Exposure button clicked
                        if (HARD_EXPOSURE == this.ExposureRequestSwitch)
                        {
                            Exposure();
                        }
                    }
                }
                // Normal event
                if ((TETD_EVENT_CALLBACK_ENUM.TETD_EVENT_FPD_ALARM != (TETD_EVENT_CALLBACK_ENUM)eventID) &&
                    (TETD_EVENT_CALLBACK_ENUM.TETD_EVENT_FPD_REBOOT_ERROR != (TETD_EVENT_CALLBACK_ENUM)eventID))
                {
                    // Update Callback Event text box
                    // Get Mutex lock for this.txt_CallbackEvent
                    this.txt_CallbackMutex.WaitOne();

                    // Begin with current time
                    if ((TETD_EVENT_CALLBACK_ENUM.TETD_EVENT_OFFSET_PROGRESS == (TETD_EVENT_CALLBACK_ENUM)eventID) ||
                        (TETD_EVENT_CALLBACK_ENUM.TETD_EVENT_OFFSET_COMPLETE == (TETD_EVENT_CALLBACK_ENUM)eventID))
                    {
                         }
                    else
                    {
                        }

                   

                    // Release Mutex lock for this.txt_CallbackEvent
                    this.txt_CallbackMutex.ReleaseMutex();

                    // If calibration callback function is registered
                    if ((null != this.calilbCallBack) &&
                       ((TETD_EVENT_CALLBACK_ENUM.TETD_EVENT_OFFSET_PROGRESS == (TETD_EVENT_CALLBACK_ENUM)eventID) ||
                        (TETD_EVENT_CALLBACK_ENUM.TETD_EVENT_OFFSET_COMPLETE == (TETD_EVENT_CALLBACK_ENUM)eventID) ||
                        (TETD_EVENT_CALLBACK_ENUM.TETD_EVENT_FLATNESS_CHECK == (TETD_EVENT_CALLBACK_ENUM)eventID)))
                    {
                        // Call calibration callback function
                        this.calilbCallBack(eventID, eventData, userData, ev);
                    }
                }
                // Alarm from FPD
                else
                {
                 
                    // Begin with current time
                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("HH:mm:ss.fff") + " " + ((TETD_EVENT_CALLBACK_ENUM)eventID).ToString() +
                         " " + ((CInterfaceLower.E_ERR_TETD_CONTROLLER_ENUM)fa.ev).ToString());

                  

                  
                    // Release Mutex lock for this.txt_CallbackEvent
                    this.txt_CallbackMutex.ReleaseMutex();
                    // Convert FPD status information to CCondition class
                    CCondition cCondition = new CCondition(fa.fc);

                    // Update Status
                   AppLogger.LogAction.ShowEventStatus(lblFPDStatus, cCondition.fpdStatus.ToString());
                  

                }
            }
            catch (Exception ex)
            {
            }
        }
        private void Exposure()
        {
            // Call API wrapper
            if (CInterfaceLower.E_ERR_TETD_CONTROLLER_ENUM.TETD_OK == this.cIfLower.TetdFpdExposure(this.ExposureMode))
            {
                // Disable Exposure button
               // this.btn_Exposure.Enabled = false;
            }
            else
            {
                // Error process (none)
            }
        }
        #endregion
        private void cLog_OutputLog_Exec(String message)
        {
            // Update Log
            AppLogger.LogAction.AddLog2List(lstFPD560, message);

        }
        #endregion
        #endregion

        #region Toshiba FDX4343R



        #region Toshiba API Declaration
        private const string STR_FPDOprdll = @"FPDOpr.dll";
        [DllImport(STR_FPDOprdll)]
        unsafe public static extern int TE_InitDetector(string workingDirectory);
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_ResetDetector();
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_SwitchDetector(int nFPDIndex);
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_EnableExposure();
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_DisableExposure();
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_EnterWorkMode();
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_ExitWorkMode();
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_EnterCalibrationMode();
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_ExitCalibrationMode();
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_ExitDetector();
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_GetFPDInfo(TE_FPD_INFO FPDInfo);
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_GetStatus(int nFPDstatus, int nFPDtemperature, int nFPDvoltage);
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_GetCalibrationStep(int nTargetDV, int ntotalTimesforCurrentDV, int ncurrentTimeforCurrentDV, int ntotalTargetDV);
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_ConfirmCalibrationStep();
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_CompleteCalibration();
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_AbortCalibration();
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_RegisterCallback(MulticastDelegate callbackFuntion, IntPtr pCustomData);
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_StartAcqImgSequence();
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_SetCorrectionType(int nCorrectionType);
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_SetImageOutputType(int nOutputType);
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_GetCorrectionType(int nCorrectionType);
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_GetImageOutputType(int nOutputType);
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_GetActiveDetectorID(int nFPDIndex);
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_GenerateShipedDefectMap();
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_StartDarkImageAcquisition();
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_SetExposureMode(int nExposureMode);
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_GetExposureMode(int nExposureMode);
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_GetSystemState(int nFPDState);
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_GetSystemVersionNo(string sysVersionNo, int nLen);
        [DllImport(STR_FPDOprdll, CallingConvention = CallingConvention.StdCall)]
        unsafe public static extern int TE_GetFPIVersionNo(string fpiVersionNo, int nLen);
        #endregion

        #region Init Toshiba
        unsafe bool InitFDX4343R()
        {
            try
            {
                unsafe
                {
                    TE_RESULT ErrorStatus;

                    //register call back function
                    ErrorStatus = TE_RegisterCallback(_ToshibaCallBackFunc, IntPtr.Zero);
                    if (!TestError(ErrorStatus, 0))
                    {
                        return false;
                    }
                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, "Khởi động tấm Toshiba...", "Start Init Detecting..."));
                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Khởi động tấm Toshiba...", "Start Init Detecting..."));
                    //Khởi động FPD với cấu hình lấy trong thư mục Config
                    string strRunPath = Application.StartupPath + @"\tetdconfig";
                    ErrorStatus = TE_InitDetector(strRunPath);
                    if (!TestError(ErrorStatus, 1))
                    {
                        return false;
                    }
                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, "Khởi động tấm Toshiba thành công...", "Init FDX4343R Completely"));
                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Khởi động tấm Toshiba thành công...", "Init FDX4343R Completely"));
                }

                return true;

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return false;
            }
        }
        #endregion

        #region Toshiba Common functions
        #region Toshiba Common functions
        bool TestError(TE_RESULT error_status, int _Type)
        {
            switch (error_status)
            {
                case FPDType.ToshibaFPD.TE_OK:
                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, (_Type == 0 ? ": RegisterCallBack sucessfully" : ": InitDetector sucessfully"));
                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + (_Type == 0 ? ": RegisterCallBack sucessfully" : ": InitDetector sucessfully"));

                    return true;
                case FPDType.ToshibaFPD.TE_COMMUERR:
                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Communication Error");
                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + (_Type == 0 ? ": Communication error" : ": Communication error"));

                    break;
                case FPDType.ToshibaFPD.TE_PERFORMERR:
                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Perform error");
                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + (_Type == 0 ? ": Perform error" : ": Perform error"));

                    break;
                case FPDType.ToshibaFPD.TE_MAPERR:
                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Calibration data error");
                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + (_Type == 0 ? ": Calibration data Error" : ": Calibration data Error"));

                    break;
                case FPDType.ToshibaFPD.TE_STATUSERR:
                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + (_Type == 0 ? ": This operation is inhibited in current status" : ": This operation is inhibited in current status"));

                    break;
                case FPDType.ToshibaFPD.TE_PARAMERR:
                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + (_Type == 0 ? ": Parameters are not correct" : ": Parameters are not correct"));

                    break;
                case FPDType.ToshibaFPD.TE_NOTINIT:
                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "The system is not initialized");
                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + (_Type == 0 ? ": The system is not initialized" : ": The system is not initialized"));

                    break;
                case FPDType.ToshibaFPD.TE_NOTSUPPORT:
                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + (_Type == 0 ? ": The function is not supported" : ": The function is not supported"));

                    break;
                default:
                    break;
            }
            return false;
        }
        #endregion
        #endregion

        #region Toshiba Delegate+ Event Processing
        unsafe public delegate int ToshibaMessageProcDelegate(TE_EVENT eventid, IntPtr datastructure, ref IntPtr pCustomData);
        unsafe public int ToshibaMessageProc(TE_EVENT eventid, IntPtr datastructure, ref IntPtr pCustomData)
        {
            ProcessEvent(eventid, datastructure);
            return 0;
        }
        string m_strWarning = "";
        bool m_bWarnFlag = false;
        unsafe void ProcessEvent(TE_EVENT eventID, IntPtr eventData)
        {
            try
            {
                switch (eventID)
                {
                    case FPDType.ToshibaFPD.TE_FPDPrePareEvent:
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": FPD PREP EVENT-Standby Mode");
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Chờ chụp...", "STANDBY..."));
                        //-->Chuyển sang chế độ Enterworkmode 
                        unsafe
                        {
                            TE_EnterWorkMode();
                        }
                        break;
                    case FPDType.ToshibaFPD.TE_FPDReadyEvent:
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Sẵn sàng chụp...", "READY..."));
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": FPD READY EVENT");
                        blnFPDReady = true;
                        cmdReady.BackColor = Color.CornflowerBlue;
                        cmdReady.Text = "SẴN SÀNG CHỤP kVp=" + kVp.ToString() + " mA=" + mA.ToString() + " mAs=" + mAs.ToString();
                        cmdReady.Tag = "R";
                        break;
                    case FPDType.ToshibaFPD.TE_FPDAbortEvent:
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Vừa bỏ chụp...", "ABORT"));
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": FPD ABORT EVENT");
                        break;
                    case FPDType.ToshibaFPD.TE_FPDSleepEvent:
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang nghỉ...", "SLEEP"));
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": FPD SLEEP EVENT");
                        break;
                    case FPDType.ToshibaFPD.TE_FPDWakingEvent:
                        unsafe
                        {
                            AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "WAKING");
                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": SEQ FPDWAKING EVENT");
                        }
                        break;
                    case FPDType.ToshibaFPD.TE_SeqStartEvent:
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Bắt đầu...", "START"));
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": SEQ START EVENT");
                        break;
                    case FPDType.ToshibaFPD.TE_XWindowOnEvent:
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": XWINDOW ON EVENT");
                        break;
                    case FPDType.ToshibaFPD.TE_XWindowOffEvent:
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": XWINDOW OFF EVENT");
                        break;
                    case FPDType.ToshibaFPD.TE_ImgTransStartEvent:
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Bắt đầu truyền hình ảnh...", "IMAGE TRANSFER START..."));
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang truyền hình ảnh...", "IMAGE TRANSFER START..."));
                        break;
                    case FPDType.ToshibaFPD.TE_ImgTransEndEvent:
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang truyền hình ảnh...", "IMAGE TRANSFERING..."));
                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang truyền hình ảnh...", "IMAGE TRANSFERING..."));
                        m_strWarning = "";
                        m_bWarnFlag = false;

                        break;
                    case FPDType.ToshibaFPD.TE_ImgDataReadyEvent://When in exposure and calibration process, not for offset calibration.
                        {
                            v_blnHasConvertRawin2DicomFile = false;
                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đang nhận dữ liệu...", ": Getting RAW Data"));
                            AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang nhận dữ liệu...", ": Getting RAW Data"));
                            ScheduledControl _selected = GetSelectedScheduled();
                            bool _isSelected = true;
                            string RAWFilePath = "";
                            string _SubDirLv1 = SubDirLv1();
                            string _SubDirLv2_Patient = SubDirLv2_Patient();
                            if (_SubDirLv1.Trim() == "" || _SubDirLv2_Patient.Trim() == "") break;
                            //Tạo các thư mục theo cấp ngày\Bệnh nhân(Mã bệnh nhân _ Tên Bệnh nhân _ Tuổi)
                            if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1)) Directory.CreateDirectory(txtImgDir.Text + @"\" + _SubDirLv1);
                            if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient)) Directory.CreateDirectory(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient);
                            //Kiểm tra nếu chưa chọn thủ tục nào thì cần lưu ảnh thành tên file theo định dạng
                            //YYYY_MM_DD_HH_mm_ss
                            if (_selected == null || RAWFileNameWillbeCreated == "NONE_SELECTED")
                            {
                                _isSelected = false;
                                RAWFileNameWillbeCreated = "NONE_SELECTED_" + Utility.GetYYYYMMDDHHMMSS(DateTime.Now);
                                if (!Directory.Exists(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED")) Directory.CreateDirectory(txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED");
                                RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\NONE_SELECTED\" + RAWFileNameWillbeCreated + ".RAW";
                            }
                            else
                            {
                                //Kiểm tra xem Schedule đang chọn có ảnh chưa. Nếu đã có ảnh thì thực hiện theo tùy chọn phần cấu hình hệ thống
                                if (_selected.Status > 0)
                                {
                                   
                                        RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\" + RAWFileNameWillbeCreated + ".RAW";//RAWFileNameWillbeCreated=REGNUM+_Detail_ID+_ACode+_Pcode tại phần click của Scheduled
                                }
                                else
                                    RAWFilePath = txtImgDir.Text + @"\" + _SubDirLv1 + @"\" + _SubDirLv2_Patient + @"\" + RAWFileNameWillbeCreated + ".RAW";//RAWFileNameWillbeCreated=REGNUM+_Detail_ID+_ACode+_Pcode tại phần click của Scheduled
                            }
                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": FULLIMG READY EVENT");
                            try
                            {
                                unsafe
                                {
                                    try
                                    {

                                        //Lấy dữ liệu ảnh
                                        FPDType.ToshibaFPD.tagImgInfo m_pSImage_Info = (FPDType.ToshibaFPD.tagImgInfo)Marshal.PtrToStructure(eventData, typeof(FPDType.ToshibaFPD.tagImgInfo));
                                        IntPtr image = m_pSImage_Info.pwData;
                                        if (IMAGE_HEIGHT != m_pSImage_Info.nHeight) IMAGE_HEIGHT = m_pSImage_Info.nHeight;
                                        if (IMAGE_WIDTH != m_pSImage_Info.nWidth) IMAGE_WIDTH = m_pSImage_Info.nWidth;
                                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": W=" + IMAGE_WIDTH.ToString() + " H=" + IMAGE_HEIGHT.ToString());
                                        var bytes = new byte[IMAGE_HEIGHT * IMAGE_WIDTH * sizeof(ushort)];
                                        Marshal.Copy(image, bytes, 0, IMAGE_HEIGHT * IMAGE_WIDTH * sizeof(ushort));
                                        if (IsCanFreeColTaskMem())
                                        {
                                            Marshal.FreeCoTaskMem((IntPtr)image);
                                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": FreeCoTaskMem");
                                        }
                                        //Luôn lưu file raw trước
                                        try
                                        {
                                            pnlScheduled.Enabled = false;
                                            try2RenameExistedFile(RAWFilePath);
                                            File.WriteAllBytes(RAWFilePath, bytes);
                                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Lưu xong ảnh thô...", ": SAVE RAW IMAGE SUCCESSFUL"));
                                            AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Lưu xong ảnh thô...", ": SAVE RAW IMAGE SUCCESSFUL"));
                                        }
                                        catch
                                        {
                                        }
                                        if (IsUsingDicomConverter)//Tự động convert thành file Dicom
                                        {
                                            DataRow dr = MakeDcmConverterInfor(_selected);
                                            if (dr != null)
                                            {
                                                if (DicomConverter.DicomConverter.Convert2Dicom(bytes, RAWFilePath, dr, true, AutoWLPath))
                                                {

                                                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đã chuyển thành file Dicom...", ": Convert Dicom completely"));
                                                    AutoUpdateResultAfterCapturingPictureFromModality(_selected);
                                                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đã chuyển thành file Dicom...", ": Convert Dicom completely"));
                                                    v_blnHasConvertRawin2DicomFile = true;
                                                    lblAutoDcmConverter.ImageIndex = 0;
                                                    IsUsingDicomConverter = true;
                                                }
                                                else
                                                {

                                                    File.WriteAllBytes(RAWFilePath, bytes);
                                                    lblAutoDcmConverter.ImageIndex = 0;
                                                    IsUsingDicomConverter = true;
                                                }
                                            }
                                            else
                                            {
                                                lblAutoDcmConverter.ImageIndex = 0;
                                                IsUsingDicomConverter = true;
                                                File.WriteAllBytes(RAWFilePath, bytes);
                                            }
                                        }
                                        else
                                        {
                                            lblAutoDcmConverter.ImageIndex = 0;
                                            IsUsingDicomConverter = true;
                                            File.WriteAllBytes(RAWFilePath, bytes);
                                        }
                                        Thread.Sleep(100);
                                        mdlStatic.isDisplayImg = false;
                                        isLoadding = false;
                                    }
                                    catch
                                    {
                                    }
                                    finally
                                    {
                                        #region "Reset Allengers GCOM"
                                        blnCheckSignalAfterEXPCommand = false;
                                        blnCheckSignalAfterSetKvpMas = false;
                                        CountOfResendingEXPCmd = 0;
                                        CountOfResendingDataFrame = 0;
                                        isSendbyCmd = true;
                                        isSendbyCmdExp = true;

                                        preventsenddata = false;
                                        preventExp = true;
                                        _SetEnableButton(true);
                                        _SetTextBtn("Ready");
                                        cmdReady.Tag = "R";
                                        State = 0;
                                        #endregion
                                        cmdReady.Enabled = m_objCurrScheduledControl != null && m_objCurrScheduledControl.Status == 0;
                                        pnlScheduled.Enabled = cmdReady.Enabled;
                                        if (!IsUsingDicomConverter)
                                        {
                                            string RAWFilePath1 = txtImgDir.Text + @"\" + RAWFileNameWillbeCreated + ".RAW";

                                            // Lấy tên file vừa tạo trong thư mục DownFolder
                                            string IMGfileHasJustCreated = Directory.GetFiles(txtImgDir.Text)[0];

                                            // Đổi tên thành file .raw
                                            File.Move(IMGfileHasJustCreated, RAWFilePath1);
                                        }
                                        AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + MultiLanguage.GetText(globalVariables.DisplayLanguage, ": Đang hiển thị ảnh...", ": Displaying Img"));
                                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang hiển thị ảnh...", ": Displaying Img"));
                                        _IAr = BeginInvoke(new DisplayImg(ViewImg));
                                        fileCreated = true;
                                    }
                                }
                            }
                            catch
                            {
                            }
                            finally
                            {
                                //TE_ExitWorkMode();
                            }
                            break;
                        }
                    case FPDType.ToshibaFPD.TE_FPDErrorEvent:
                        {
                            TE_ERR_INFO pError_Info = (TE_ERR_INFO)Marshal.PtrToStructure(eventData, typeof(TE_ERR_INFO));
                            string strError = "";
                            switch (pError_Info.nCode)
                            {
                                case FPDType.ToshibaFPD.TE_FPDErrorNotConnected:
                                    strError = "Ethernet Not connected, Please Close TETDRadMgr!";
                                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Chưa kết nối với tấm...", "ETHERNET NOT CONNECTED"));
                                    break;
                                default:
                                    break;
                            }
                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": " + strError);
                            break;
                        }
                    case FPDType.ToshibaFPD.TE_FPDWarningEvent:
                        {
                            TE_ERR_INFO pError_Info = (TE_ERR_INFO)Marshal.PtrToStructure(eventData, typeof(TE_ERR_INFO));
                            m_bWarnFlag = true;
                            switch (pError_Info.nCode)
                            {
                                case FPDType.ToshibaFPD.TE_FPDWarningFlatImageCheckFailed:
                                    m_strWarning = "WARNING: The flat image is invalid";
                                    break;
                                case FPDType.ToshibaFPD.TE_FPDWarningDarkImageCheckFailed:
                                    m_strWarning = "WARNING: The dark image is invalid";
                                    break;
                                default:
                                    m_strWarning = "WARNING: Unknown Error";
                                    break;
                            }
                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": " + m_strWarning);
                            break;
                        }
                    case FPDType.ToshibaFPD.TE_FPDCalProgressEvent:
                        {
                            TE_CALPROGRESS_INFO pCalProgress_Info = (TE_CALPROGRESS_INFO)Marshal.PtrToStructure(eventData, typeof(TE_CALPROGRESS_INFO));
                            string strCalProgress = "";
                            AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đang được: " + ((decimal)pCalProgress_Info.fPercentage * 100).ToString(), "The calibration percentage is " + ((decimal)pCalProgress_Info.fPercentage * 100).ToString()));
                            strCalProgress = "The calibration percentage is " + ((decimal)pCalProgress_Info.fPercentage * 100).ToString();
                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": " + strCalProgress);
                            break;
                        }
                    case FPDType.ToshibaFPD.TE_ImgAcqTimeoutEvent:
                        {

                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": Image acquire failed, please acquiring image again");
                            break;
                        }
                    case FPDType.ToshibaFPD.TE_FPDHWStatusEvent:
                        {
                            AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": HardWare Status met some problem, please check the hardware");
                            break;
                        }
                    default:
                        break;

                }

            }
            catch
            {

            }
        }
        #endregion

        #endregion

        #region  Properties
        public int Images
        {
            get
            {
                return _images;
            }

            set
            {
                _images = value;
            }
        }
       
        public bool _AutoPageSettings
        {
            get { return AutoPageSettings; }
            set { AutoPageSettings = value; }
        }
        /// <summary>
        /// Tên máy in lazer
        /// </summary>
        public string _LazerPrinterName
        {
            get
            {
                return LazerPrinterName;

            }
            set
            {
                LazerPrinterName = value;
            }

        }
        public string _HosName
        {
            get
            {
                return HospitalName;

            }
            set
            {
                HospitalName = value;
            }
        }
        public string _DepartName
        {
            get
            {
                return DepartmentName;

            }
            set
            {
                DepartmentName = value;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public PrintDocument PrintDocument
        {
            get
            {
                return _printDocument;
            }
        }

       
        #endregion

        #region Init functions
        //Khởi tạo Controls
        void InitControls()
        {
            try
            {

                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": Start KernelExpired  Completely");

                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": End KernelExpired  Completely");
                //Load một số thông tin cấu hình
                LoadAutoContrastAndBrightnessConfig();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": LoadAutoContrastAndBrightnessConfig  Completely");
                LoadAutoDetectConfig();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": LoadAutoDetectConfig  Completely");
                LoadHospitalInfor();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": LoadHospitalInfor  Completely");
                //Tạo dữ liệu mẫu cho file Excel mẫu
                CreateTemplate();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": CreateTemplate  Completely");
              
              
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": InitDicomViewer  Completely");
                InitWLComunication();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": InitWLComunication  Completely");
                //Khởi tạo phần máy in lazer
               // InitLazerPrinter();

                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": InitLazerPrinter  Completely");

                InitInfo();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": InitInfo  Completely");
                SetActforButtons();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": SetActforButtons  Completely");
            }
            catch (Exception ex)
            {
                AppLogger.LogAction.LogActions("==>Init Controls().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex.ToString());
            }
            finally
            {
                cmdLogin.Enabled = cboDeviceLogin.Items.Count > 0;

            }
        }
        void InitUI()
        {
            try
            {
                lstFPD560.DoubleClick += new EventHandler(lstFPD560_DoubleClick);
               
                SetDocking();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": SetDocking  Completely");
                SetFPDMode();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": LoadDevice  Completely");
                _FullScreen = new FullScreenMode.FullScreen(this);
                //_FullScreen.ShowFullScreen();
                pnlDirectPrint.SendToBack();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": ShowFullScreen  Completely");
                pnlLogin.BringToFront();
                pnlMain.SendToBack();
            }
            catch
            {
            }
        }
        void lblFPDStatus_DoubleClick(object sender, EventArgs e)
        {
            lstFPD560_DoubleClick(lstFPD560, e);
        }
        void lstFPD560_DoubleClick(object sender, EventArgs e)
        {
            lstFPD560.Location = new Point(0, 0);
            if (lstFPD560.Size.Equals(new Size(4, 4)))
                lstFPD560.Size = new Size(pnlImgViewer.Width, pnlImgViewer.Height);
            else
                lstFPD560.Size = new Size(4, 4);
        }
       

        #endregion

        #region Commonfunction
     
        DataRow MakeDcmConverterInfor(ScheduledControl _selected)
        {

            try
            {
                DataRow dr = m_dtDicomconverterInfo.NewRow();
                dr[colPid] = _selected == null && txtID2.Text.Trim() == "" ? "PATIENT_ID" : txtID2.Text;
                dr[colPatientName] = _selected == null && txtName2.Text.Trim() == "" ? "PATIENT_NAME" : (Bodau(txtName2.Text).Replace(txtAge.Text.Trim(), "") + " " + txtAge.Text);
                dr[colPatientSex] = _selected == null ? "O" : Sex;
                dr[colPatientAge] = _selected == null && txtAge.Text.Trim() == "" ? "0" : txtAge.Text;
                dr[colPatientBirthdate] = _selected == null ? ToDicomString(DateTime.Now) : ToDicomString(BirthDate);
                dr[colRegDate] = _selected == null ? ToDicomString(DateTime.Now) : ToDicomString(RegDate);
                dr[colRegNum] = _selected == null && txtRegNumber2.Text.Trim() == "" ? "REG_NUMBER" : txtRegNumber2.Text;

                dr[colKVP] = lblKvpVal.Value.ToString();
                dr[colMAS] = lblmAsVal.Value.ToString();
               
                    dr[colImgHeight] = FPDSeq == 1 ? IMGH : IMGH2;
                    dr[colImgWidth] = FPDSeq == 1 ? IMGW : IMGW2;
               
                dr[colModalityCode] = modTypeCode;
                dr[colAtonomyCode] = _selected == null ? "A_CODE" : CurrSchedule.A_Code;
                dr[colProjectionCode] = _selected == null ? "P_CODE" : CurrSchedule.P_Code;

                dr[colHostpitalName] = Bodau(HospitalName);
                dr[colDepartmentName] = Bodau(DepartmentName);
                dr[colAcqDate] = Utility.GetYYYYMMDD(DateTime.Now);
                dr[colAppName] = "DROC";
                dr[_colStudyInstanceUID] = _selected == null ? ClearCanvas.Dicom.DicomUid.GenerateUid().UID.ToString() : _selected.StudyInstanceUID;
                dr[colSOPInstanceUID] = _selected == null ? ClearCanvas.Dicom.DicomUid.GenerateUid().UID.ToString() : _selected.SOPInstanceUID;
                dr[colSeriesInstanceUID] = _selected == null ? ClearCanvas.Dicom.DicomUid.GenerateUid().UID.ToString() : _selected.SeriesInstanceUID;
                string BA = "16";
                string BS = "16";
                string HB = "15";
                GetBpp(ref BA, ref BS, ref HB);
                dr[colBitsStored] = BS;
                dr[colHightBit] = HB;
                dr[colBitsAllocated] = BA;

                return dr;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi tạo thông tin BN để dùng cho hàm DicomConverter\n" + ex.Message);
                return null;
            }
        }
        DataRow MakeDcmConverterInforReprocess(ScheduledControl _selected)
        {

            try
            {
                DataRow dr = m_dtDicomconverterInfo.NewRow();
                dr[colPid] = _selected == null && txtID2.Text.Trim() == "" ? "PATIENT_ID" : txtID2.Text;
                dr[colPatientName] = _selected == null && txtName2.Text.Trim() == "" ? "PATIENT_NAME" : (Bodau(txtName2.Text).Replace(txtAge.Text.Trim(), "") + " " + txtAge.Text);
                dr[colPatientSex] = _selected == null ? "O" : Sex;
                dr[colPatientAge] = _selected == null && txtAge.Text.Trim() == "" ? "0" : txtAge.Text;
                dr[colPatientBirthdate] = _selected == null ? ToDicomString(DateTime.Now) : ToDicomString(BirthDate);
                dr[colRegDate] = _selected == null ? ToDicomString(DateTime.Now) : ToDicomString(RegDate);
                dr[colRegNum] = _selected == null && txtRegNumber2.Text.Trim() == "" ? "REG_NUMBER" : txtRegNumber2.Text;

                dr[colKVP] = _selected == null ? lblKvpVal.Value.ToString() : _selected.kVp.ToString();
                dr[colMAS] = _selected == null ? lblmAsVal.Value.ToString() : _selected.mAs.ToString();
                
                    dr[colImgHeight] = FPDSeq == 1 ? IMGH : IMGH2;
                    dr[colImgWidth] = FPDSeq == 1 ? IMGW : IMGW2;
               
                dr[colModalityCode] = modTypeCode;
                dr[colAtonomyCode] = _selected == null ? "A_CODE" : CurrSchedule.A_Code;
                dr[colProjectionCode] = _selected == null ? "P_CODE" : CurrSchedule.P_Code;
                dr[colHostpitalName] = Bodau(HospitalName);
                dr[colDepartmentName] = Bodau(DepartmentName);
                dr[colAcqDate] = Utility.GetYYYYMMDD(DateTime.Now);
                dr[colAppName] = "DROC";
                dr[_colStudyInstanceUID] = _selected == null ? ClearCanvas.Dicom.DicomUid.GenerateUid().UID.ToString() : _selected.StudyInstanceUID;
                dr[colSOPInstanceUID] = _selected == null ? ClearCanvas.Dicom.DicomUid.GenerateUid().UID.ToString() : _selected.SOPInstanceUID;
                dr[colSeriesInstanceUID] = _selected == null ? ClearCanvas.Dicom.DicomUid.GenerateUid().UID.ToString() : _selected.SeriesInstanceUID;
                string BA = "16";
                string BS = "16";
                string HB = "15";
                GetBpp(ref BA, ref BS, ref HB);
                dr[colBitsStored] = BS;
                dr[colHightBit] = HB;
                dr[colBitsAllocated] = BA;
                return dr;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi tạo thông tin BN để dùng cho hàm DicomConverter\n" + ex.Message);
                return null;
            }
        }
        void GetBpp(ref string BA, ref string BS, ref string HB)
        {
            try
            {
                string fileName = Application.StartupPath + @"\Bpp.info";
                if (File.Exists(fileName))
                {
                    using (StreamReader _reader = new StreamReader(fileName))
                    {
                        object obj = _reader.ReadLine();
                        BA = getVal(obj, "16");
                        obj = _reader.ReadLine();
                        BS = getVal(obj, "16");
                        obj = _reader.ReadLine();
                        HB = getVal(obj, "15");
                    }
                }
            }
            catch
            {
            }
        }
        string getVal(object obj, string defaultVal)
        {
            try
            {
                if (obj == null) return defaultVal;
                string[] arrValues = obj.ToString().Trim().Split('-');
                if (arrValues.Length != 2) return defaultVal;
                return arrValues[1].Trim();
            }
            catch
            {
                return defaultVal;
            }
        }
        private static string ToDicomString(DateTime dtmValue)
        {
            return dtmValue.Year.ToString() + Microsoft.VisualBasic.Strings.Right("00" + dtmValue.Month.ToString(), 2) + Microsoft.VisualBasic.Strings.Right("00" + dtmValue.Day.ToString(), 2);
        }

      
        int RLUB_FontSize = 100;
        private string AnnConfigFilePath = Application.StartupPath + @"\AnnConfig.dat";
        private void ReadFontSize()
        {
            try
            {

                if (!File.Exists(AnnConfigFilePath))
                {
                    _R = "R";
                    _L = "L";
                    _U = "U";
                    _B = "B";
                    _O = "O";
                    RLUB_FontSize = 100;
                }
                else
                {
                    using (StreamReader _reader = new StreamReader(AnnConfigFilePath))
                    {
                        string obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            _L = obj.ToString();
                        }
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            _R = obj.ToString();
                        }
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            _U = obj.ToString();
                        }
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            _B = obj.ToString();
                        }
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            _O = obj.ToString();
                        }
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            RLUB_FontSize = Convert.ToInt32(obj);
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                
            }
        }

        

        FileStream GetFileStream(string filename)
        {
            FileStream fs;
            if (File.Exists(filename))
                fs = new FileStream(filename, FileMode.Open);
            else
                fs = File.Create(filename);

            return fs;
        }
       
        void modifyStudyButtons()
        {
            try
            {

                int Reg_Status = Convert.ToInt32(Utility.sDbnull(Utility.GetObjectValueFromGridColumn(grdStudyList, "colRegStatus1", grdStudyList.CurrentRow.Index)));
                cmdRepeatAcquisition.Enabled = grdStudyList.RowCount > 0;

                cmdSendtoServer.Enabled = grdStudyList.RowCount > 0 && Reg_Status == 2 ? true : false;

            }
            catch
            {
                cmdRepeatAcquisition.Enabled = false;
                cmdSendtoServer.Enabled = false;
            }
        }
        public void ModifyWorkListButtons()
        {
            try
            {
                cmdInsertWL.Enabled = tabControl2.SelectedIndex == 0;

                cmdEditWL.Enabled = tabControl2.SelectedIndex == 0 && grdWorkList.RowCount > 0;
                cmdDelWL.Enabled = tabControl2.SelectedIndex == 0 && grdWorkList.RowCount > 0;
                cmdBeginExamWL.Enabled = grdWorkList.RowCount > 0 || grdWorkListSuspending.RowCount > 0;
            }
            catch
            {
            }

        }
        private void cboDevice_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch
            {
            }
        }
       
        #endregion
        void AutoSetFPDModebyLoginInfo()
        {
            try
            {

                if (_FPDMode == AppType.AppEnum.FPDMode.SingleMode)
                {
                    chkChay1Tam_Click(chkChay1Tam, new EventArgs());
                    cboFPD1.SelectedIndex = Utility.GetSelectedIndex(cboFPD1, m_intCurrDevice1.ToString(), true);
                }
                else if (_FPDMode == AppType.AppEnum.FPDMode.DualMode)
                {
                    chk2tamgiongnhau_Click(chk2tamgiongnhau, new EventArgs());
                    cboFPD1.SelectedIndex = Utility.GetSelectedIndex(cboFPD1, m_intCurrDevice1.ToString(), true);
                }
                else
                {
                    chk2tamkhacnhau_Click(chk2tamkhacnhau, new EventArgs());
                    cboFPD1.SelectedIndex = Utility.GetSelectedIndex(cboFPD1, m_intCurrDevice1.ToString(), true);
                    cboFPD2.SelectedIndex = Utility.GetSelectedIndex(cboFPD2, m_intCurrDevice2.ToString(), true);
                }

            }
            catch
            {
            }
        }
        void SetFPDText(Button cmdPanel, string text)
        {
            try
            {
                cmdPanel.Text = text;
            }
            catch
            {
            }
        }
        void SetFPDMode()
        {
            try
            {
                lblFPD2.Visible = false;
                cboDeviceLogin2.Visible = false;
                cmdPanel1.Visible = false;
                cmdPanel2.Visible = false;

                if (!File.Exists(strFPDModeConfig))
                {
                    LoadDevice();

                }
                else
                {
                    using (StreamReader _Reader = new StreamReader(strFPDModeConfig))
                    {
                        string[] arrContent = null;
                        object objLine = _Reader.ReadLine();
                        if (objLine == null) return;
                        arrContent = objLine.ToString().Split(':');
                        if (arrContent.Length < 2) return;
                        if (arrContent[1].Trim() == "0")//SingleMode
                        {
                            _FPDMode = AppType.AppEnum.FPDMode.SingleMode;
                            objLine = _Reader.ReadLine();
                            if (objLine == null) return;
                            arrContent = objLine.ToString().Split(':');
                            if (arrContent.Length < 2) return;
                            m_intCurrDevice1 = Convert.ToInt32(arrContent[1]);
                            LoadDevice(m_intCurrDevice1);
                            objLine = _Reader.ReadLine();//Bỏ qua ID tấm số 2 để đọc phần tên tấm
                            objLine = _Reader.ReadLine();
                            if (objLine != null)
                            {
                                arrContent = objLine.ToString().Split(':');
                                if (arrContent.Length >= 2)
                                {
                                    cmdPanel1.Text = arrContent[1];
                                }
                            }
                            objLine = _Reader.ReadLine();
                            if (objLine != null)
                            {
                                arrContent = objLine.ToString().Split(':');
                                if (arrContent.Length >= 2)
                                {
                                    cmdPanel2.Text = arrContent[1];
                                }
                            }
                        }
                        else if (arrContent[1].Trim() == "1")//DualMode
                        {
                            cmdPanel1.Visible = true;
                            cmdPanel2.Visible = true;
                            _FPDMode = AppType.AppEnum.FPDMode.DualMode;
                            objLine = _Reader.ReadLine();
                            if (objLine == null) return;
                            arrContent = objLine.ToString().Split(':');
                            if (arrContent.Length < 2) return;
                            m_intCurrDevice1 = Convert.ToInt32(arrContent[1]);
                            LoadDevice(m_intCurrDevice1);
                            objLine = _Reader.ReadLine();//Bỏ qua ID tấm số 2 để đọc phần tên tấm
                            objLine = _Reader.ReadLine();
                            if (objLine != null)
                            {
                                arrContent = objLine.ToString().Split(':');
                                if (arrContent.Length >= 2)
                                {
                                    cmdPanel1.Text = arrContent[1];
                                }
                            }
                            objLine = _Reader.ReadLine();
                            if (objLine != null)
                            {
                                arrContent = objLine.ToString().Split(':');
                                if (arrContent.Length >= 2)
                                {
                                    cmdPanel2.Text = arrContent[1];
                                }
                            }
                        }
                        else//Others
                        {
                            _FPDMode = AppType.AppEnum.FPDMode.Other;
                            lblFPD2.Visible = true;
                            cboDeviceLogin2.Visible = true;
                            // cmdPanel1.Visible = true;
                            // cmdPanel2.Visible = true;

                            objLine = _Reader.ReadLine();
                            if (objLine == null) return;
                            arrContent = objLine.ToString().Split(':');
                            if (arrContent.Length < 2) return;
                            m_intCurrDevice1 = Convert.ToInt32(arrContent[1]);
                            objLine = _Reader.ReadLine();
                            if (objLine == null) return;
                            arrContent = objLine.ToString().Split(':');
                            if (arrContent.Length < 2) return;
                            m_intCurrDevice2 = Convert.ToInt32(arrContent[1]);

                            LoadDualDevice(m_intCurrDevice1, m_intCurrDevice2);
                            objLine = _Reader.ReadLine();
                            if (objLine != null)
                            {
                                arrContent = objLine.ToString().Split(':');
                                if (arrContent.Length >= 2)
                                {
                                    cmdPanel1.Text = arrContent[1];
                                }
                            }
                            objLine = _Reader.ReadLine();
                            if (objLine != null)
                            {
                                arrContent = objLine.ToString().Split(':');
                                if (arrContent.Length >= 2)
                                {
                                    cmdPanel2.Text = arrContent[1];
                                }
                            }
                        }

                    }

                }
            }
            catch
            {
            }
            finally
            {

            }
        }
        void LoadDualDevice(int deviceid1, int deviceid2)
        {
            try
            {
                lblFPD1.Text = "Tấm cảm biến 1";
                lblFPD2.Text = "Tấm cảm biến 2";
                clsUser objtemp = new clsUser();

                DataTable dtDevice = new ModalityController().GetAllData(false).Tables[0];
                if (dtDevice != null)
                {

                    cboFPD1.DataSource = dtDevice.Select("1=1").CopyToDataTable().DefaultView;
                    cboFPD1.DisplayMember = "MODALITY_NAME";
                    cboFPD1.ValueMember = "MODALITY_ID";


                    cboFPD2.DataSource = dtDevice.Select("1=1").CopyToDataTable().DefaultView;
                    cboFPD2.DisplayMember = "MODALITY_NAME";
                    cboFPD2.ValueMember = "MODALITY_ID";


                    DataRow[] arrDR = dtDevice.Select("MODALITY_ID=" + deviceid2);
                    if (arrDR.Length <= 0) return;
                    cboDeviceLogin2.DataSource = arrDR.CopyToDataTable().DefaultView;
                    cboDeviceLogin2.DisplayMember = "MODALITY_NAME";
                    cboDeviceLogin2.ValueMember = "MODALITY_ID";

                    arrDR = dtDevice.Select("MODALITY_ID=" + deviceid1);
                    if (arrDR.Length <= 0) return;
                    cboDeviceLogin.DataSource = arrDR.CopyToDataTable().DefaultView;
                    cboDeviceLogin.DisplayMember = "MODALITY_NAME";
                    cboDeviceLogin.ValueMember = "MODALITY_ID";
                    if (cboDeviceLogin.Items.Count > 0)
                    {
                        cboDeviceLogin.SelectedIndex = 0;
                        modName = ((DataRowView)cboDeviceLogin.SelectedItem)["MODALITY_NAME"].ToString();
                        modTypeCode = ((DataRowView)cboDeviceLogin.SelectedItem)["MOD_TYPE_CODE"].ToString();
                        modTypeName = ((DataRowView)cboDeviceLogin.SelectedItem)["Mod_Type_Name"].ToString();
                        
                        modCode = ((DataRowView)cboDeviceLogin.SelectedItem)["MODALITY_CODE"].ToString();
                        m_intCurrDevice1 = Convert.ToInt32(((DataRowView)cboDeviceLogin.SelectedItem)["MODALITY_ID"].ToString());
                        LoadDeviceInfor();

                    }
                    if (cboDeviceLogin2.Items.Count > 0)
                    {
                        cboDeviceLogin2.SelectedIndex = 0;
                        modName2 = ((DataRowView)cboDeviceLogin2.SelectedItem)["MODALITY_NAME"].ToString();
                        modTypeCode2 = ((DataRowView)cboDeviceLogin2.SelectedItem)["MOD_TYPE_CODE"].ToString();
                        modTypeName2 = ((DataRowView)cboDeviceLogin2.SelectedItem)["Mod_Type_Name"].ToString();
                        modCode2 = ((DataRowView)cboDeviceLogin2.SelectedItem)["MODALITY_CODE"].ToString();
                        m_intCurrDevice2 = Convert.ToInt32(((DataRowView)cboDeviceLogin2.SelectedItem)["MODALITY_ID"].ToString());
                        LoadDeviceInfor2();

                    }
                }
            }
            catch
            {
            }
            finally
            {

            }
        }
        void LoadDevice(int ModID)
        {
            try
            {
                clsUser objtemp = new clsUser();

                DataTable dtDevice = new ModalityController().GetAllData(false).Tables[0];
                if (dtDevice != null)
                {
                    cboDevice.DataSource = dtDevice.Select("MODALITY_ID=" + ModID).CopyToDataTable().DefaultView;
                    cboDevice.DisplayMember = "MODALITY_NAME";
                    cboDevice.ValueMember = "MODALITY_ID";


                    cboDeviceLogin.DataSource = dtDevice.Select("MODALITY_ID=" + ModID).CopyToDataTable().DefaultView;
                    cboDeviceLogin.DisplayMember = "MODALITY_NAME";
                    cboDeviceLogin.ValueMember = "MODALITY_ID";
                    if (cboDeviceLogin.Items.Count > 0) cboDeviceLogin.SelectedIndex = 0;
                    cboDevice_SelectedValueChanged(cboDeviceLogin, new EventArgs());


                    cboFPD1.DataSource = dtDevice.Select("1=1").CopyToDataTable().DefaultView;
                    cboFPD1.DisplayMember = "MODALITY_NAME";
                    cboFPD1.ValueMember = "MODALITY_ID";


                    cboFPD2.DataSource = dtDevice.Select("1=1").CopyToDataTable().DefaultView;
                    cboFPD2.DisplayMember = "MODALITY_NAME";
                    cboFPD2.ValueMember = "MODALITY_ID";

                    if (cboDeviceLogin.Items.Count > 0)
                    {
                        cboDeviceLogin.SelectedIndex = 0;
                        modName = ((DataRowView)cboDeviceLogin.SelectedItem)["MODALITY_NAME"].ToString();
                        modTypeCode = ((DataRowView)cboDeviceLogin.SelectedItem)["MOD_TYPE_CODE"].ToString();
                        modTypeName = ((DataRowView)cboDeviceLogin.SelectedItem)["Mod_Type_Name"].ToString();
                        modCode = ((DataRowView)cboDeviceLogin.SelectedItem)["MODALITY_CODE"].ToString();
                        m_intCurrDevice1 = Convert.ToInt32(((DataRowView)cboDeviceLogin.SelectedItem)["MODALITY_ID"].ToString());
                        LoadDeviceInfor();

                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (globalVariables.OleDbConnection.State == ConnectionState.Closed)
                {
                    Application.Exit();

                }
                else if (cboDevice.Items.Count <= 0)
                {
                    Utility.ShowMsg("Bạn phải khởi tạo danh mục tấm chụp trước khi thực hiện các nghiệp vụ. Nhấn OK để bắt đầu.");
                    frm_Modalities newForm = new frm_Modalities();
                    newForm.m_blnCallFromMenu = false;
                    newForm.ShowDialog();
                    LoadDevice();

                }
                cmdTest.Enabled = cboDevice.Items.Count > 0;
            }
        }
        void ReLoadDevice4FPD()
        {
            try
            {
                clsUser objtemp = new clsUser();

                DataTable dtDevice = new ModalityController().GetAllData(false).Tables[0];
                if (dtDevice != null)
                {

                    cboFPD1.DataSource = dtDevice.Select("1=1").CopyToDataTable().DefaultView;
                    cboFPD1.DisplayMember = "MODALITY_NAME";
                    cboFPD1.ValueMember = "MODALITY_ID";


                    cboFPD2.DataSource = dtDevice.Select("1=1").CopyToDataTable().DefaultView;
                    cboFPD2.DisplayMember = "MODALITY_NAME";
                    cboFPD2.ValueMember = "MODALITY_ID";

                }
            }
            catch
            {
            }
            finally
            {
                AutoSetFPDModebyLoginInfo();
            }
        }
        void LoadDevice()
        {
            try
            {
                clsUser objtemp = new clsUser();

                DataTable dtDevice = new ModalityController().GetAllData(false).Tables[0];
                if (dtDevice != null)
                {
                    cboDevice.DataSource = dtDevice.DefaultView;
                    cboDevice.DisplayMember = "MODALITY_NAME";
                    cboDevice.ValueMember = "MODALITY_ID";


                    cboDeviceLogin.DataSource = dtDevice.DefaultView;
                    cboDeviceLogin.DisplayMember = "MODALITY_NAME";
                    cboDeviceLogin.ValueMember = "MODALITY_ID";
                    cboDevice_SelectedValueChanged(cboDeviceLogin, new EventArgs());


                    cboFPD1.DataSource = dtDevice.Select("1=1").CopyToDataTable().DefaultView;
                    cboFPD1.DisplayMember = "MODALITY_NAME";
                    cboFPD1.ValueMember = "MODALITY_ID";


                    cboFPD2.DataSource = dtDevice.Select("1=1").CopyToDataTable().DefaultView;
                    cboFPD2.DisplayMember = "MODALITY_NAME";
                    cboFPD2.ValueMember = "MODALITY_ID";

                    if (cboDeviceLogin.Items.Count > 0)
                    {
                        cboDeviceLogin.SelectedIndex = 0;
                        modName = ((DataRowView)cboDeviceLogin.SelectedItem)["MODALITY_NAME"].ToString();
                        modTypeCode = ((DataRowView)cboDeviceLogin.SelectedItem)["MOD_TYPE_CODE"].ToString();
                        modTypeName = ((DataRowView)cboDeviceLogin.SelectedItem)["Mod_Type_Name"].ToString();
                        modCode = ((DataRowView)cboDeviceLogin.SelectedItem)["MODALITY_CODE"].ToString();
                        m_intCurrDevice1 = Convert.ToInt32(((DataRowView)cboDeviceLogin.SelectedItem)["MODALITY_ID"].ToString());
                        LoadDeviceInfor();

                    }

                }
            }
            catch
            {
            }
            finally
            {
                if (globalVariables.OleDbConnection.State == ConnectionState.Closed)
                {
                    Application.Exit();

                }
                else if (cboDevice.Items.Count <= 0)
                {
                    Utility.ShowMsg("Bạn phải khởi tạo danh mục tấm chụp trước khi thực hiện các nghiệp vụ. Nhấn OK để bắt đầu.");
                    frm_Modalities newForm = new frm_Modalities();
                    newForm.m_blnCallFromMenu = false;
                    newForm.ShowDialog();
                    LoadDevice();

                }
                cmdTest.Enabled = cboDevice.Items.Count > 0;
            }
        }
        void LoadDeviceInfor()
        {
            try
            {
                DataTable dtDevice = new ModalityController().GetData("MODALITY_ID=" + m_intCurrDevice1.ToString()).Tables[0];
                if (dtDevice != null)
                {
                    IMGH = Utility.Int32Dbnull(dtDevice.Rows[0]["IMGH"], 3072);
                    PORT_NUM = Utility.Int32Dbnull(dtDevice.Rows[0]["PORT_NUM"], 104);
                    XRAY_RECEIVER_PORT = PORT_NUM;
                    IPPrefix = Utility.sDbnull(dtDevice.Rows[0]["IP_ADDRESS"], "192.168.250.");
                    IMGW = Utility.Int32Dbnull(dtDevice.Rows[0]["IMGW"], 3072);

                    _defaultIMGH = Utility.Int32Dbnull(dtDevice.Rows[0]["IMGH"], 3072);
                    _defaultIMGW = Utility.Int32Dbnull(dtDevice.Rows[0]["IMGW"], 3072);

                    LOW = Utility.Int32Dbnull(dtDevice.Rows[0]["LOW"], 10158);
                    HIGH = Utility.Int32Dbnull(dtDevice.Rows[0]["HIGH"], 65535);
                    Range = Utility.sDbnull(dtDevice.Rows[0]["Range"]);
                   
                }
            }
            catch
            {
               
            }
            finally
            {
                IMAGE_HEIGHT = IMGH;
                IMAGE_WIDTH = IMGW;
            }
        }

        void LoadDeviceInfor2()
        {
            try
            {
                DataTable dtDevice = new ModalityController().GetData("MODALITY_ID=" + m_intCurrDevice2.ToString()).Tables[0];
                if (dtDevice != null)
                {
                    IMGH2 = Utility.Int32Dbnull(dtDevice.Rows[0]["IMGH"], 3072);
                    PORT_NUM2 = Utility.Int32Dbnull(dtDevice.Rows[0]["PORT_NUM"], 104);
                    XRAY_RECEIVER_PORT2 = PORT_NUM2;
                    IPPrefix2 = Utility.sDbnull(dtDevice.Rows[0]["IP_ADDRESS"], "192.168.250.");
                    IMGW2 = Utility.Int32Dbnull(dtDevice.Rows[0]["IMGW"], 3072);

                    _defaultIMGH2 = Utility.Int32Dbnull(dtDevice.Rows[0]["IMGH"], 3072);
                    _defaultIMGW2 = Utility.Int32Dbnull(dtDevice.Rows[0]["IMGW"], 3072);

                    LOW2 = Utility.Int32Dbnull(dtDevice.Rows[0]["LOW"], 10158);
                    HIGH2 = Utility.Int32Dbnull(dtDevice.Rows[0]["HIGH"], 65535);
                    Range2 = Utility.sDbnull(dtDevice.Rows[0]["Range"]);
                  
                }
            }
            catch
            {
               
            }
            finally
            {
                IMAGE_HEIGHT2 = IMGH2;
                IMAGE_WIDTH2 = IMGW2;
            }
        }
        public void AutoUpdateStatus()
        {
            try
            {
                if (_AutoEnhanceList.Count == 0) return;
                foreach (string key in _AutoEnhanceList.Keys)
                {
                    string[] arrValues = key.Split('_');

                    DataRow[] arrDr = m_dtAcquisitionDataSource.Select("Reg_number='" + arrValues[0] + "' AND detail_ID=" + arrValues[1]);
                    if (arrDr.Length > 0)
                    {
                        arrDr[0]["Status_Name"] = _AutoEnhanceList[key] ? "Đã xử lý xong" : "Đang xử lý...";
                        //Xóa các file đã xử lý xong
                        if (_AutoEnhanceList[key])
                            _AutoEnhanceList.Remove(key);
                        m_dtAcquisitionDataSource.AcceptChanges();
                    }
                }

            }
            catch
            {
            }
        }
        void DeleteFile(string FileName)
        {
            try
            {
                if (File.Exists(FileName)) File.Delete(FileName);
            }
            catch
            {
            }
        }

        private string GetFileName(string ImgPath, string DirPath, string FileName, ScheduledControl _reObj)
        {
            try
            {

                bool OnlyRaw = true;
                string RawFile = "";
                string DcmFile = "";
                string[] arrFile = Directory.GetFiles(DirPath);
                if (arrFile.Length <= 0)
                    arrFile = Directory.GetFiles(ImgPath);
                foreach (string file in arrFile)
                    if (!Path.GetExtension(file).ToUpper().Contains(".TXT") && !Path.GetExtension(file).ToUpper().Contains(".ANN") && !Path.GetExtension(file).ToUpper().Contains(".Rar") && (Path.GetFileNameWithoutExtension(file).ToUpper() == FileName.ToUpper() || Path.GetFileNameWithoutExtension(file).ToUpper() == FileName.Replace("_" + _reObj.A_Code.Trim() + "_" + _reObj.P_Code.Trim(), "").ToUpper()))
                    {

                        if (Path.GetExtension(file).ToUpper() != ".RAW")
                        {
                            DcmFile = file;
                            OnlyRaw = false;
                            break;
                        }
                        else
                        {
                            RawFile = file;
                        }
                    }
                if (DcmFile == "" && RawFile == "")
                {
                    arrFile = Directory.GetFiles(ImgPath);
                    foreach (string file in arrFile)
                        if (!Path.GetExtension(file).ToUpper().Contains(".TXT") && !Path.GetExtension(file).ToUpper().Contains(".ANN") && !Path.GetExtension(file).ToUpper().Contains(".Rar") && (Path.GetFileNameWithoutExtension(file).ToUpper() == FileName.ToUpper() || Path.GetFileNameWithoutExtension(file).ToUpper() == FileName.Replace("_" + _reObj.A_Code.Trim() + "_" + _reObj.P_Code.Trim(), "").ToUpper()))
                        {

                            if (Path.GetExtension(file).ToUpper() != ".RAW")
                            {
                                DcmFile = file;
                                OnlyRaw = false;
                                break;
                            }
                            else
                            {
                                RawFile = file;
                            }
                        }
                }
                if (!OnlyRaw) return DcmFile;
                else
                    return RawFile;
            }
            catch
            {
                return "";
            }
        }
       
        void UpdateDiagnostic()
        {
            try
            {
                //if (!_DicomMedicalViewer.IsValidCell()) return;
                //VietBaIT.DcmProcessing.EnterDiagnostic _Enter = new VietBaIT.DcmProcessing.EnterDiagnostic();
                //_Enter.DiagnosticResult = DiagnosticResult;
                //_Enter.status = DiagnosticStatus;
                //_Enter.ShowDialog();
                //DiagnosticResult = _Enter.DiagnosticResult;
            }
            catch
            {
            }
        }
        void UpdateControl()
        {
        }


        /// <summary>
        /// Dịch giới tính từ tiếng anh sang tiếng việt
        /// </summary>
        /// <param name="Sex">M(ale) hoặc F(emale)</param>
        /// <returns>Nam hoặc Nữ</returns>
        string TranslateSex(string Sex)
        {
            if (globalVariables.DisplayLanguage == "VN")
            {
                if (Sex.ToUpper() == "M")
                {
                    return "Nam";
                }
                if (Sex.ToUpper() == "F")
                {
                    return "Nữ";
                }
                else
                {
                    return "";
                }
            }
            return Sex;
        }
        #region Save Image to File

        void ConvertRaw2DicomFile(string filePath)
        {
            try
            {
                ScheduledControl _selected = GetSelectedScheduled();
                byte[] bytImg = Utility.bytGetImage(filePath);
                if (bytImg != null)
                {

                    DataRow dr = MakeDcmConverterInfor(_selected);
                    if (dr != null)
                        if (DicomConverter.DicomConverter.Convert2Dicom(bytImg, filePath, dr, true, AutoWLPath))
                        {
                            lblAutoDcmConverter.ImageIndex = 0;
                            IsUsingDicomConverter = true;
                        }

                }
            }
            catch
            {
            }
        }
        void ConvertRaw2DicomFileReprocess(string filePath)
        {
            try
            {
                ScheduledControl _selected = GetSelectedScheduled();
                byte[] bytImg = Utility.bytGetImage(filePath);
                if (bytImg != null)
                {

                    DataRow dr = MakeDcmConverterInforReprocess(_selected);
                    if (dr != null)
                        if (DicomConverter.DicomConverter.Convert2Dicom(bytImg, filePath, dr, true, AutoWLPath))
                        {
                            lblAutoDcmConverter.ImageIndex = 0;
                            IsUsingDicomConverter = true;
                        }

                }
            }
            catch
            {
            }
        }

       

        void try2MoveFileInDemoMode(string sourceFile, string destFile)
        {
            try
            {

                if (File.Exists(destFile)) File.Delete(destFile);
                File.Copy(sourceFile, destFile, true);
            }
            catch
            {
            }
        }
       
        string ID_Name_Age_Sex = "";
        decimal TotalSeconds = 0m;
        #region RAWAPI

        [DllImport("rawapi.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe int  VFlipRawImage(WORD* source, WORD* dest, WORD wWidth, WORD wHeight);
        [DllImport("rawapi.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe int HFlipRawImage(WORD* source, WORD* dest, WORD wWidth, WORD wHeight);
        [DllImport("rawapi.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe void   Invert(WORD*  src, WORD width, WORD height);
        [DllImport("rawapi.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe IntPtr WindowLeveling_v2(IntPtr hOriginal, WORD wImageWidth, WORD wImageHeight, WORD wWCenter, WORD wWWidth, WORD wMAX_BIN, byte ucLUTType);
        [DllImport("rawapi.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe void WindowLeveling(WORD* pSrc, WORD* pDst, WORD wImageWidth, WORD wImageHeight, WORD wWCenter, WORD wWWidth, WORD wMAX_BIN, byte ucLUTType);
        [DllImport("rawapi.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe void ContrastStretch(WORD* src, WORD* dest, WORD width, WORD height, WORD low, WORD high);
        [DllImport("rawapi.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "CLAHE")]
        public static extern unsafe int CLAHE1(WORD* pImageSrc, WORD* pImageDst, UInt32 uiXRes, UInt32 uiYRes, WORD Min, WORD Max, UInt32 uiNrX, UInt32 uiNrY, UInt32 uiNrBins, float fCliplimit);

        [DllImport("rawapi.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern  unsafe int ImageProcessing(WORD* src,  WORD* dest, WORD width, WORD height, DWORD LowLevel, DWORD HighLevel,uint uiNrX, uint uiNrY,bool RawInvert,byte ucLUTType,float fCliplimit);

        [DllImport("rawapi.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe int RawProcessing(WORD* pSrc, WORD* pDest, WORD width, WORD height, DWORD LowLevel, DWORD HighLevel, bool RawInvert,bool VFlip,bool HFlip, byte ucLUTType, float fCliplimit);

        [DllImport("rawapi.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe int CIP(WORD* src, WORD* dest, WORD width, WORD height, DWORD LowLevel, DWORD HighLevel, uint uiNrX, uint uiNrY, uint nBins, bool VFlip, bool HFLIP, bool RawInvert, byte ucLUTType, float fCliplimit);

        const int GHND = 0x0042;
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GlobalLock(IntPtr handle);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GlobalUnlock(IntPtr handle);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GlobalFree(IntPtr handle);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GlobalAlloc(int uFlags, int dwBytes);
        bool newCIP = false;
     

        unsafe void FromRAW2Dicom(string rawpath)
        {
            try
            {
                string temp_raw = rawpath.ToUpper().Replace(".RAW", "") + "_Temp.RAW";
                ScheduledControl _selected = GetSelectedScheduled();
                #region Call CIP
                System.Diagnostics.Process _Process = new System.Diagnostics.Process();
                _Process.StartInfo.FileName = Application.StartupPath + @"\imgprocessing.exe";
                _Process.StartInfo.Arguments = @"/i=" + rawpath + " /p=" + _selected.A_Code + "-" + _selected.P_Code + ".txt /o=" + temp_raw;
                _Process.Start();
                _Process.WaitForExit();

                byte[] bytes = Utility.bytGetImage(temp_raw);
                Try2DelFile(temp_raw);
                if (bytes != null)
                {
                    string errMsg = "";
                    DataRow dr = MakeDcmConverterInforReprocess(_selected);
                    if (dr != null)
                        if (DicomConverter.DicomConverter.Convert2Dicom_v1(bytes, rawpath, dr, AutoWLPath, true,ref errMsg))
                        {
                            lblAutoDcmConverter.ImageIndex = 0;
                            IsUsingDicomConverter = true;
                        }
                }
                IsUsingDicomConverter = true;
                #endregion
            }
            catch
            {
            }
            finally
            {
                newCIP = true;
            }
        }
       
        bool AutoDetectAPIParams = true;
        int iCIP = 0;
        bool canrun = true;
        private void cmdApplyAPI_Click(object sender, EventArgs e)
        {
            canrun = true;
            //for (int i = 0; i <= 200; i++)
            //{
               // if (!canrun) break;
               //  this.Text = i.ToString();
                Application.DoEvents();
                _isUseNewAPI = true;
                AutoDetectAPIParams = false;
                ReProcessingWithoutAsking();
            //}
            
        }
        #endregion
        // public static extern unsafe int Step3(WORD* pImageSrc, WORD* pImageDst, UInt32 uiXRes, UInt32 uiYRes, WORD Min, WORD Max, UInt32 uiNrX, UInt32 uiNrY, UInt32 uiNrBins, float fCliplimit);
        bool DemoWithRAWFile = true;
       
        string NewAPIList = Application.StartupPath + @"\APIList.txt";
        bool IsNewAPI(string A_CODE,string B_CODE)
        {
            try
            {
                string APIList = "";
                string _serviceName = A_CODE + "-" + B_CODE;
                if (!File.Exists(NewAPIList)) return false;
                using (StreamReader _Reader = new StreamReader(NewAPIList))
                {

                    APIList = _Reader.ReadToEnd();
                    _Reader.BaseStream.Flush();
                    _Reader.Close();
                }
                if (APIList.Contains(";" + _serviceName + ";")) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public void OpenDicom(ref bool IsCroping, ref int _Idx, string fileName, bool IsOnlyImg, bool CanCreatePicBox)
        {
            //Stopwatch _sw = new Stopwatch();
            try
            {
                AppLogger.LogAction.ShowEventStatus(lblTotalTime, "");
              
                pnlScheduled.Enabled = false;
                //_sw.Start();

                #region Chế độ Demo
                //if (_AppMode == AppType.AppEnum.AppMode.Demo)
                if (_AppMode == AppType.AppEnum.AppMode.Demo && !File.Exists(fileName))
                {
                    //Utility.ShowMsg("DEMO:"+fileName);
                    ScheduledControl _selected = GetSelectedScheduled();
                    bool _isSelected = true;
                    string RAWFilePath = "";
                    //Tạo các thư mục theo cấp ngày\Bệnh nhân(Mã bệnh nhân _ Tên Bệnh nhân _ Tuổi)
                    if (!Directory.Exists(txtImgDir.Text + @"\" + SubDirLv1())) Directory.CreateDirectory(txtImgDir.Text + @"\" + SubDirLv1());
                    if (!Directory.Exists(txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient())) Directory.CreateDirectory(txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient());
                    //Kiểm tra nếu chưa chọn thủ tục nào thì cần lưu ảnh thành tên file theo định dạng
                    //YYYY_MM_DD_HH_mm_ss
                    if (_selected == null || RAWFileNameWillbeCreated == "NONE_SELECTED")
                    {
                        _isSelected = false;
                        RAWFileNameWillbeCreated = "NONE_SELECTED_" + Utility.GetYYYYMMDDHHMMSS(DateTime.Now);
                        if (!Directory.Exists(txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient() + @"\NONE_SELECTED")) Directory.CreateDirectory(txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient() + @"\NONE_SELECTED");
                        RAWFilePath = txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient() + @"\NONE_SELECTED\" + RAWFileNameWillbeCreated + ".RAW";
                    }
                    else
                        RAWFilePath = txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient() + @"\" + RAWFileNameWillbeCreated + ".RAW";//RAWFileNameWillbeCreated=REGNUM+_Detail_ID+_ACode+_Pcode tại phần click của Scheduled
                    fileName = Application.StartupPath + @"\DemoImg\" + m_strCurrACode + "-" + m_strCurrPCode + ".RAW";
                    if (!File.Exists(fileName))
                    {

                        fileName = fileName.Replace(".RAW", ".DCM");
                        DemoWithRAWFile = false;
                    }
                    if (!DemoWithRAWFile)
                    {
                        string destFile = RAWFilePath.ToUpper().Replace(".RAW", ".DCM");
                        try2MoveFileInDemoMode(fileName, destFile);
                        fileName = destFile;
                        //Tự động cập nhật kết quả đã có hình ảnh
                        AutoUpdateResultAfterCapturingPictureFromModality();
                    }
                    else
                    {
                        string destFile = RAWFilePath;
                        try2MoveFileInDemoMode(fileName, destFile);
                        fileName = destFile;
                    }

                }
                #endregion

                isLoadding = true;
                //stopToolStripMenuItem1_Click(mnuStop, new EventArgs());
                TempCrop = IsCroping;
                int _ww = 0;
                int _wc = 0;
               // this.Text = MultiLanguage.GetText(globalVariables.DisplayLanguage, "VietBaIT JCS - DROC", "VietBaIT JSC-DROC");
                if (!IsCroping) FilePath = fileName;
                bool IsRawFile = false;
                _images = 1;

                try
                {
                    #region Xử lý lại ảnh dcm từ ảnh gốc(Raw file)
                    try
                    {
                        if (IsGenDcmFromRaw)
                        {
                            IsGenDcmFromRaw = false;
                            string tempf_raw = fileName.ToUpper().Replace(".DCM", ".RAW");

                            //Kiểm tra xem có ảnh Raw không
                            if (File.Exists(tempf_raw))
                            {
                                //xóa file Dcm đang có
                                string tempf_dcm = fileName.ToUpper().Replace(".RAW", ".DCM");
                                try
                                {
                                    File.Delete(tempf_dcm);
                                }
                                catch
                                {
                                }
                                //Gán lại giá trị cho fileName để bước kế tiếp load lại raw file
                                fileName = tempf_raw;
                             //   Utility.ShowMsg("Checking:" + fileName);
                            }

                        }
                    }
                    catch (Exception ex0)
                    {
                        AppLogger.LogAction.LogActions("==>OpenDicom.if (IsGenDcmFromRaw)().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex0.ToString());
                    }
                    try
                    {
                        if (Path.GetExtension(fileName).ToUpper().Contains("RAW"))
                        {
                            if (IsUsingDicomConverter || lblAutoDcmConverter.ImageIndex == 0)//Tự động convert thành file Dicom
                            {
                              //  Utility.ShowMsg("API:" + _isUseNewAPI.ToString());
                                //SetText(lblAPIReval, "API:" + _isUseNewAPI.ToString());
                                if (_isUseNewAPI) FromRAW2Dicom(fileName);
                                if (!_isUseNewAPI) ConvertRaw2DicomFileReprocess(fileName);
                                fileName = fileName.ToLower().Replace(".raw", ".dcm");
                                AutoUpdateResultAfterCapturingPictureFromModality();

                                IsUsingDicomConverter = true;
                                v_blnHasConvertRawin2DicomFile = true;// _isUseNewAPI ? false : true;
                                //Thực hiện thuật toán xử lý ảnh ở ngay sau bước load ảnh Dicom

                            }
                            else
                            {
                                new frm_LargeMsgBoxOK(MultiLanguage.GetText(globalVariables.DisplayLanguage, "Thông báo", "Warning"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Cần vào tab Cấu hình đánh dấu vào mục tự động Convert thành file Dicom", "go to Configuration Tab and check Auto Convert to Dicom File"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đã hiểu", "OK"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Không hiểu", "Cancel")).ShowDialog();
                            }
                        }
                    }
                    catch (Exception ex01)
                    {
                        AppLogger.LogAction.LogActions("==>OpenDicom.ReGenDcmFromRAWFile().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex01.ToString());
                    }
                    #endregion

                    if (File.Exists(fileName))
                    {
                       
                                    System.Diagnostics.Process _Process = new System.Diagnostics.Process();
                                    _Process.StartInfo.FileName = Application.StartupPath + @"\MiniDicomViewer\DROCDisplay.exe";
                                    _Process.StartInfo.Arguments = @"/i=" + fileName + " /o=" + fileName;
                                    _Process.Start();
                                    _Process.WaitForExit();
                                    
                               
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Mời bạn tiếp tục xử lý", "Image Result"));
                    }
                    else
                    {
                        mdlStatic.isDisplayImg = false;
                        if (_AppMode == AppType.AppEnum.AppMode.License || (_AppMode == AppType.AppEnum.AppMode.Demo && _ViewState == AppType.AppEnum.ViewState.Capture))
                            MessageBox.Show("Không tồn tại file ảnh sau:\n" + fileName);
                    }
                }
                catch (Exception ex)
                {
                    AppLogger.LogAction.LogActions("==>OpenDicom.SecondsException().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex.ToString());
                    Utility.ShowMsg("2225 " + ex.ToString() + "\n" + fileName);
                    GC.Collect();
                    isLoadding = false;
                    
                }
                finally
                {
                    isLoadding = false;

                }

                isLoadding = false;
            }
            catch (Exception ex1)
            {
                AppLogger.LogAction.LogActions("==>OpenDicom.FirstException().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex1.ToString());
                AppLogger.LogAction.AddLog2List(lstFPD560, "0002: " + ex1.Message);
            }
            finally
            {
                DemoWithRAWFile = true;
                newCIP = false;

                pnlScheduled.Enabled = true;
                //_sw.Stop();
                //AppLogger.LogAction.ShowEventStatus(lblFPDStatus,((decimal)_sw.ElapsedMilliseconds / 1000).ToString());
                IsUsingDicomConverter = false;
                //lblMemory.Text = getAvailableRAM();
                //Kiểm tra nếu đang chế độ Crop mà lại chọn mục xem lại ảnh gốc thì cần khôi phục lại chế độ đó
                
            }
        }
        int old_ww = 0;
        int old_wc = 0;
       
        
        #region IEConfig
       



        void RepeatAcq()
        {
            try
            {
                _LastDoubleMode = AppType.AppEnum.DoubleMode.StudyList;
                _DoubleMode = AppType.AppEnum.DoubleMode.StudyList;

                if (grdStudyList.CurrentRow == null)
                {
                    Utility.ShowMsg("Bạn cần chọn bệnh nhân để tiếp tục chụp", "Thông báo");
                    return;
                }
                _Click(cmdAcq, new EventArgs());
                cmdAcq.PerformClick();
                AcquisitionFromWL = false;

                DataRow dr = ((DataRowView)grdStudyList.CurrentRow.DataBoundItem).Row;
                currentStudyRow = dr;
                long Reg_ID = Convert.ToInt64(dr["Reg_ID"]);
                currREGID = Reg_ID;
                //CurrPatient_ID = Utility.Int32Dbnull(dr["Patient_ID"], -1);
                CurrStudyInstanceUID = Utility.sDbnull(dr["StudyInstanceUID"], ClearCanvas.Dicom.DicomUid.GenerateUid().UID);
                txtID2.Text = Utility.sDbnull(dr["Patient_Code"]);
                CurrPatient_ID = Utility.Int32Dbnull(dr["Patient_ID"], -1);
                txtName2.Text = Utility.sDbnull(dr["Patient_Name"]);
                txtRegNumber2.Text = Utility.sDbnull(dr["REG_Number"]);
                txtSex.Text = Utility.sDbnull(dr["Sex_Name"]);
                RegDate = Convert.ToDateTime(dr["CREATED_DATE"]);
                if (File.Exists(Application.StartupPath + @"\use.age"))
                    txtAge.Text = Utility.sDbnull(dr["Age"].ToString());
                else
                    txtAge.Text = (DateTime.Now.Year - Convert.ToDateTime(dr["BIRTH_DATE"]).Year).ToString();
                CurrStudyInstanceUID = Utility.sDbnull(dr["StudyInstanceUID"], ClearCanvas.Dicom.DicomUid.GenerateUid().UID);
                Sex = Sex2Sex(Utility.sDbnull(dr["Sex"]));
                lblPName.Text = txtName2.Text.Trim() + " - " + txtSex.Text.Trim() + " - " + txtAge.Text + " T";
                m_dtAcquisitionDataSource = new RegDetailController().GetAllData(Convert.ToInt64(dr["Reg_ID"])).Tables[0];
                ModifyAcqButton();
                _currTab = AppType.AppEnum.TabMode.Acq;
                mdlStatic.isDisplayImg = false;
                CreateScheduled();
                //ViewImg();

            }
            catch
            {
            }

        }
        delegate void DisplayImg();
        void Try2Delfile4Reprocess(string dcmfile)
        {
            try
            {
                if (dcmfile.ToUpper().Contains(".DCM") && IsUsingDicomConverter && IsGenDcmFromRaw)
                {
                    Try2DelFile(dcmfile);
                }
            }
            catch
            {
            }

        }
        void ViewImg()
        {
            try
            {
                Try2EndInvoke();
                
                if ((mdlStatic.isDisplayImg || isLoadding) && !IsLoadOriginalImage) return;
                ScheduledControl _reObj = GetSelectedScheduled();

                if (_AppMode == AppType.AppEnum.AppMode.Demo)
                {
                    //Try to dispose process DROCDisplay.exe
                    if (_reObj != null && m_strCurrACode + m_strCurrPCode != _reObj.A_Code + _reObj.P_Code && ExistsProcess("DROCDisplay.exe"))
                    {
                       KillProcess("DROCDisplay.exe");
                    }
                }
                string ImgDir = @"C:\";
                if (_reObj == null  && ExistsProcess("DROCDisplay.exe"))
                {
                  KillProcess("DROCDisplay.exe");
                    return;
                }
                else
                {
                    if (_reObj == null) return;
                    if (!Directory.Exists(txtImgDir.Text + @"\" + SubDirLv1())) Directory.CreateDirectory(txtImgDir.Text + @"\" + SubDirLv1());
                    if (!Directory.Exists(txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient())) Directory.CreateDirectory(txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient());
                    ImgDir = txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient();
                }
                if (_reObj == null) return;
                m_strCurrACode = _reObj.A_Code;
                m_strCurrPCode = _reObj.P_Code;
                string fileName = GetFileName(txtImgDir.Text, ImgDir, FileName, _reObj);

                #region Kiểm tra nếu fileName kiểu cũ thì tự động tạo thư mục đổi tên file...
                if (!fileName.Contains("_" + _reObj.A_Code.Trim() + "_" + _reObj.P_Code.Trim()))
                {
                    try
                    {
                        if (!Directory.Exists(txtImgDir.Text + @"\" + SubDirLv1())) Directory.CreateDirectory(txtImgDir.Text + @"\" + SubDirLv1());
                        if (!Directory.Exists(txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient())) Directory.CreateDirectory(txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient());
                        string newFilePath = txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient() + @"\" + FileName + Path.GetExtension(fileName);
                        //Chuyển file RAW
                        if (Path.GetExtension(fileName).ToLower() == ".dcm")
                        {
                            try
                            {
                                string SourceRAWFilePath = fileName.ToUpper().Replace(".DCM", ".RAW");
                                string newRAWFilePath = txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient() + @"\" + FileName + ".RAW";
                                File.Move(SourceRAWFilePath, newRAWFilePath);
                            }
                            catch
                            {
                            }
                        }
                        //Chuyển file Thumbnail

                        try
                        {
                            if (fileName.Trim() != "")
                            {
                                string SourceThumbnailFilePath = Path.GetDirectoryName(fileName) + @"\" + (Path.GetFileNameWithoutExtension(fileName) + "_THUMBNAIL").ToUpper() + ".PNG";
                                string newRAWFilePath = txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient() + @"\" + FileName + "_THUMBNAIL.PNG";
                                File.Move(SourceThumbnailFilePath, newRAWFilePath);
                            }
                        }
                        catch
                        {
                        }
                        if (fileName.Trim() != "")
                        {
                            //Chuyển file DCM
                            File.Move(fileName, newFilePath);
                            fileName = GetFileName(txtImgDir.Text, ImgDir, FileName, _reObj);
                        }
                    }
                    catch
                    {
                    }
                }
                #endregion

                CurrFileName = Path.GetFileNameWithoutExtension(fileName);

                if (IsLoadOriginalImage || (_AppMode == AppType.AppEnum.AppMode.Demo && _ViewState == AppType.AppEnum.ViewState.Capture) || (fileName != "" && !isLoadding && !mdlStatic.isDisplayImg))
                {
                    AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "Calling OpenDicom...");
                    mdlStatic.isDisplayImg = true;
                   // Try2Delfile4Reprocess(fileName);
                  //  Utility.ShowMsg(fileName);
                    int idx=0;
                    OpenDicom(ref _IsCropping, ref idx, fileName, false, false);
                    Angle = 0;
                    ModifyAcqButton();
                }
                else
                {
                    if (fileName == "")
                    {
                        mdlStatic.isDisplayImg = false;
                        KillProcess("DROCDisplay.exe");
                        ModifyAcqButton();
                    }
                }

            }
            catch (Exception ex)
            {
                AppLogger.LogAction.LogActions("==>ViewImage().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex.ToString());
                Utility.ShowMsg("3068 " + ex.Message);
            }
            finally
            {
                pnlScheduled.Enabled = true;
               
                IsLoadOriginalImage = false;

            }
        }

        #endregion

       
     
        public string Bodau(string s)
        {

            int i = 0;
            string CH = null;
            //###################################################
            if (!string.IsNullOrEmpty(s.Trim()))
            {
                for (i = 1; i <= s.Length; i++)
                {

                    CH = VB6.Strings.Mid(s, i, 1);
                    switch (CH)
                    {
                        case "â":
                        case "ă":
                        case "ấ":
                        case "ầ":
                        case "ậ":
                        case "ẫ":
                        case "ẩ":
                        case "ắ":
                        case "ằ":
                        case "ẵ":
                        case "ẳ":
                        case "ặ":
                        case "á":
                        case "à":
                        case "ả":
                        case "ã":
                        case "ạ":
                            s = s.Replace(CH, "a");
                            break;
                        case "Â":
                        case "Ă":
                        case "Ấ":
                        case "Ầ":
                        case "Ậ":
                        case "Ẫ":
                        case "Ẩ":
                        case "Ắ":
                        case "Ằ":
                        case "Ẵ":
                        case "Ẳ":
                        case "Ặ":
                        case "Á":
                        case "À":
                        case "Ả":
                        case "Ã":
                        case "Ạ":
                            s = s.Replace(CH, "A");
                            break;
                        case "ó":
                        case "ò":
                        case "ỏ":
                        case "õ":
                        case "ọ":
                        case "ô":
                        case "ố":
                        case "ồ":
                        case "ổ":
                        case "ỗ":
                        case "ộ":
                        case "ơ":
                        case "ớ":
                        case "ờ":
                        case "ợ":
                        case "ở":
                        case "ỡ":
                            s = s.Replace(CH, "o");
                            break;
                        case "Ó":
                        case "Ò":
                        case "Ỏ":
                        case "Õ":
                        case "Ọ":
                        case "Ô":
                        case "Ố":
                        case "Ồ":
                        case "Ổ":
                        case "Ỗ":
                        case "Ộ":
                        case "Ơ":
                        case "Ớ":
                        case "Ờ":
                        case "Ợ":
                        case "Ở":
                        case "Ỡ":
                            s = s.Replace(CH, "O");
                            break;
                        case "ư":
                        case "ứ":
                        case "ừ":
                        case "ự":
                        case "ử":
                        case "ữ":
                        case "ù":
                        case "ú":
                        case "ủ":
                        case "ũ":
                        case "ụ":
                            s = s.Replace(CH, "u");
                            break;
                        case "Ư":
                        case "Ứ":
                        case "Ừ":
                        case "Ự":
                        case "Ử":
                        case "Ữ":
                        case "Ù":
                        case "Ú":
                        case "Ủ":
                        case "Ũ":
                        case "Ụ":
                            s = s.Replace(CH, "U");
                            break;
                        case "ê":
                        case "ế":
                        case "ề":
                        case "ệ":
                        case "ể":
                        case "ễ":
                        case "è":
                        case "é":
                        case "ẻ":
                        case "ẽ":
                        case "ẹ":
                            s = s.Replace(CH, "e");
                            break;
                        case "Ê":
                        case "Ế":
                        case "Ề":
                        case "Ệ":
                        case "Ể":
                        case "Ễ":
                        case "È":
                        case "É":
                        case "Ẻ":
                        case "Ẽ":
                        case "Ẹ":
                            s = s.Replace(CH, "E");
                            break;
                        case "í":
                        case "ì":
                        case "ị":
                        case "ỉ":
                        case "ĩ":
                            s = s.Replace(CH, "i");
                            break;
                        case "Í":
                        case "Ì":
                        case "Ỉ":
                        case "Ĩ":
                        case "Ị":
                            s = s.Replace(CH, "I");
                            break;
                        case "ý":
                        case "ỳ":
                        case "ỵ":
                        case "ỷ":
                        case "ỹ":
                            s = s.Replace(CH, "y");
                            break;
                        case "Ý":
                        case "Ỳ":
                        case "Ỵ":
                        case "Ỷ":
                        case "Ỹ":
                            s = s.Replace(CH, "Y");
                            break;
                        case "đ":
                            s = s.Replace(CH, "d");
                            break;
                        case "Đ":
                            s = s.Replace(CH, "D");
                            break;
                    }
                }
            }
            return s;
        }
       
        private List<int> listOfFactors;
        int numberOfPixels = 0;
        public Size GetSizeofRAW(string _filename)
        {
            using (BinaryReader br = new BinaryReader(File.Open(_filename, FileMode.Open)))
            {
                long iTotalSize = br.BaseStream.Length;
                numberOfPixels = (int)(iTotalSize / 2);
            }

            int w = 0;
            int h = 0;
            int Factor = 0;

            if (SquareRoot(out Factor))
                w = h = Factor;
            else
            {
                listOfFactors = Factors(numberOfPixels);
                int noFactors = listOfFactors.Count;
                w = listOfFactors[noFactors - 2];
                h = listOfFactors[noFactors - 1];
            }

            return new Size(w, h);
        }

        public bool SquareRoot(out int Factor)
        {
            int temp = (int)Math.Floor(Math.Sqrt(numberOfPixels));
            Factor = 0;
            if (temp * temp == numberOfPixels)
            {
                Factor = temp;
                return true;
            }
            return false;
        }


        public List<int> Factors(int number)
        {
            List<int> factors = new List<int>();
            int max = (int)Math.Sqrt(number);  //round down 
            for (int factor = 1; factor <= max; ++factor)
            { //test from 1 to the square root, or the int below it, inclusive. 
                if (number % factor == 0)
                {
                    factors.Add(factor);
                    if (factor != max)
                    { // Don't add the square root twice!  Thanks Jon 
                        factors.Add(number / factor);
                    }
                }
            }
            return factors;
        }
        private void LoadImgAgain()
        {
            try
            {
                isLoadding = false;
                mdlStatic.isDisplayImg = false;
                _ScheduledControl__OnClick(GetSelectedScheduled());

            }
            catch
            {
            }
        }
      
        int m_intCurrentProcedure_ID = -1;

        public void SetMsg(Label lblMsg, string Message, bool isProcessing)
        {
            if (isProcessing)
            {
                lblMsg.Visible = true;
            }
            else
            {
                lblMsg.Visible = false;
            }
            lblMsg.Text = Message;
            lblMsg.Refresh();
            Thread.Sleep(10);
        }

        public static void SetTextWithErr(Label lblMsg, string Message, bool isErr)
        {
            if (lblMsg.InvokeRequired)
            {
                lblMsg.Invoke(new SetText4LableWithErr(SetTextWithErr), new object[] { lblMsg, Message, isErr });
            }
            else
            {
                if (isErr)
                {
                    lblMsg.ForeColor = Color.Red;
                }
                else
                {
                    lblMsg.ForeColor = Color.DarkBlue;
                }
                lblMsg.Text = Message;
                lblMsg.Refresh();
                Thread.Sleep(5);
            }
        }
        void ModifyAcqButton()
        {
            try
            {
                ScheduledControl _reObj = GetSelectedScheduled();


                cmdAddProc.Enabled = txtID2.Text.Trim() != "";
                cmdDelProc.Enabled = _reObj != null && _reObj.Status <= 0 && pnlScheduled.Controls.Count > 0;
                cmdReady.Enabled = _reObj != null && _reObj.Status <= 0;
                cmdSuspend.Enabled = pnlScheduled.Controls.Count > 0;
                cmdEndExam.Enabled = pnlScheduled.Controls.Count > 0;

                Int16 Status = GetCurrentScheduledStatus();
                if (!Directory.Exists(txtImgDir.Text + @"\" + SubDirLv1())) Directory.CreateDirectory(txtImgDir.Text + @"\" + SubDirLv1());
                if (!Directory.Exists(txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient())) Directory.CreateDirectory(txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient());
                string ImgDir = txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient();
               
                cmdReject.Enabled = _reObj != null && Status == 1 && pnlScheduled.Controls.Count > 0;
                cmdSaveImg.Enabled = _reObj != null && Status == 1 && pnlScheduled.Controls.Count > 0;
                if (Status == 0)
                {

                    //cmdDelProc.Enabled = true;
                    mnu2.Enabled = true;
                    tabCtrlAcq.SelectedTab = tabPageHardware;
                }
                else
                {
                   
                    cmdDelProc.Enabled = false;
                    mnu2.Enabled = false;
                }
            }
            catch
            {
            }
        }
        void ModifyAcqButton(ScheduledControl _reObj)
        {
            try
            {


                cmdAddProc.Enabled = txtID2.Text.Trim() != "";
                cmdDelProc.Enabled = _reObj != null && _reObj.Status <= 0 && pnlScheduled.Controls.Count > 0;
                cmdReady.Enabled = _reObj != null && _reObj.Status <= 0;
                cmdSuspend.Enabled = pnlScheduled.Controls.Count > 0;
                cmdEndExam.Enabled = pnlScheduled.Controls.Count > 0;

                Int16 Status = GetCurrentScheduledStatus();
                if (!Directory.Exists(txtImgDir.Text + @"\" + SubDirLv1())) Directory.CreateDirectory(txtImgDir.Text + @"\" + SubDirLv1());
                if (!Directory.Exists(txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient())) Directory.CreateDirectory(txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient());
                string ImgDir = txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient();
               
                cmdReject.Enabled = _reObj != null && Status == 1 && pnlScheduled.Controls.Count > 0;
                cmdSaveImg.Enabled = _reObj != null && Status == 1 && pnlScheduled.Controls.Count > 0;
                if (Status == 0)
                {

                    //cmdDelProc.Enabled = true;
                    mnu2.Enabled = true;
                    tabCtrlAcq.SelectedTab = tabPageHardware;
                }
                else
                {
                    //tabCtrlAcq.SelectedTab = tabPageImgTools;
                    cmdDelProc.Enabled = false;
                    mnu2.Enabled = false;
                }
            }
            catch
            {
            }
        }
        private Int16 GetCurrentScheduledStatus()
        {
            try
            {
                if (GetSelectedScheduled() == null) return 0;
                return (Int16)GetSelectedScheduled().Status;
            }
            catch
            {
                return 0;
            }
        }
        private string Sex2Sex(string Sex)
        {
            if (Sex == "0") return "M";
            else if (Sex == "1") return "F";
            else return "O";
        }
        string GetDirection(short DirectionCapture)
        {
            switch (DirectionCapture)
            {
                case 0:
                    return " - Hướng chụp: Chụp từ phía trước";
                case 1:
                    return " - Hướng chụp: Chụp từ phía sau";
                case 2:
                    return " - Hướng chụp: Chụp nghiêng phải";
                case 3:
                    return " - Hướng chụp: Chụp nghiêng trái";
                case 4:
                    return " - Hướng chụp: Chụp chếch";
                default:
                    return " - Hướng chụp: Không xác định";
            }


        }
        void SendData()
        {
            try
            {
                mdlStatic.SetMsg(lblAcqMsg, "", true);
                if (txtDir.Text.Trim() == "" || !Directory.Exists(txtDir.Text.Trim()))
                {
                    FolderBrowserDialog fld = new FolderBrowserDialog();
                    if (fld.ShowDialog() == DialogResult.OK)
                    {
                        txtDir.Text = fld.SelectedPath;
                        RegConfiguration.SaveSettings("hrk", "VBIT_DRTech_FOLDER", "FILE", txtDir.Text);

                    }
                    else
                    { return; }

                }
                string[] arrFile = Directory.GetFiles(txtDir.Text);
                if (arrFile.Length > 0)
                {
                    //Xóa tất cả các file mặc dù biết rằng phần này do anh Dũng phải xử lý
                    try
                    {
                        foreach (string s in arrFile)
                            System.IO.File.Delete(s);
                    }
                    catch
                    {
                    }
                    //mdlStatic.SetMsg(lblAcqMsg, "Đang tồn tại thông tin chụp chưa được xử lý. Đề nghị bạn xem lại!", true);
                    //return;
                }
                try
                {
                    //Write new file
                    ScheduledControl _reObj = GetSelectedScheduled();
                    string Detail_ID = _reObj.DETAIL_ID.ToString();
                    string FileName = txtRegNumber2.Text + "_" + Detail_ID + ".txt";
                    StreamWriter newWriter = new StreamWriter(txtDir.Text + @"\" + FileName);
                    newWriter.WriteLine("PID#" + txtID2.Text);
                    newWriter.WriteLine("PNAME#" + txtName2.Text);
                    newWriter.WriteLine("PAGE#" + txtAge.Text);
                    newWriter.WriteLine("PSEX#" + txtSex.Text);
                    newWriter.WriteLine("REGID#" + _reObj.REG_ID.ToString());
                    newWriter.WriteLine("REGDATE#" + RegDate);
                    newWriter.WriteLine("ANATOMY_CODE#" + Utility.sDbnull(_reObj.A_Code, ""));
                    newWriter.WriteLine("PROJECTION_CODE#" + Utility.sDbnull(_reObj.P_Code, ""));

                    newWriter.Flush();
                    newWriter.Close();
                    mdlStatic.SetMsg(lblAcqMsg, "Đã gửi dữ liệu chụp thành công.", false);

                }
                catch
                {
                }
            }
            catch
            {
            }
        }
        bool hasConfig = true;
        string IMGConfigName = "NONE";
        int IE_ID = -1;
        DataRow _currDRIEData = null;
        bool isLargeFocus = true;
        bool _isUseNewAPI = false;
         bool isUseNewAPI()
        {
            return File.Exists(Application.StartupPath+ @"\isnewapi.api");
        }
          Int32 XRES = 32;
          Int32 YRES = 32;
          uint NBINS = 500;
          Int32 LLV = 25000;
          Int32 HLV = 65535;


          decimal SLOPE = 2.5m;
          Int32 NEWAPI = 0;
          Int32 LUTTYPE = 0;
        void LoadIEConfig(int Device_ID, string ANATOMY_CODE, string PROJECTION_CODE)
        {
            try
            {
                DataSet ds = new ModalityController().GetIECONFIG(Device_ID, ANATOMY_CODE, PROJECTION_CODE, ref WW, ref WC);
                if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                {
                    WW = 0;
                    WC = 0;
                    //Thử load cấu hình đang được áp dụng cho một thiết bị bất kỳ trong hệ thống
                    ds = new ModalityController().GetIECONFIG(ANATOMY_CODE, PROJECTION_CODE);
                    if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        IE_ID = -1;
                        IMGConfigName = "NONE";
                        _currDRIEData = null;
                        hasConfig = false;
                        START_WIDTH = -1;
                        return;
                    }
                }
                _currDRIEData = ds.Tables[0].Rows[0];
                if (_currDRIEData != null)
                {
                    hasConfig = true;
                    if (isUseNewAPI())
                    {
                       

                      
                    }
                    APPLY_INVERT_FIRST = Utility.Int32Dbnull(_currDRIEData["APPLY_INVERT_FIRST"], 0);
                    AUTO_MIN_MAX_BIT = Utility.Int32Dbnull(_currDRIEData["AUTO_MIN_MAX_BIT"], 0);
                    APPLY_HEC = Utility.Int32Dbnull(_currDRIEData["APPLY_HEC"], 0);
                    HEC_STT = Utility.Int32Dbnull(_currDRIEData["HEC_STT"], 0);
                    MED_STT = Utility.Int32Dbnull(_currDRIEData["MED_STT"], 0);
                    IE_ID = Utility.Int32Dbnull(_currDRIEData["ID"], 0);
                    IMGConfigName = Utility.sDbnull(_currDRIEData["IE_NAME"], "NONE");
                    WL_STT = Utility.Int32Dbnull(_currDRIEData["WL_STT"], 0);
                    ADJUST_CENTER = Utility.Int32Dbnull(_currDRIEData["WL_CENTER"], 0);
                    ADJUST_WIDTH = Utility.Int32Dbnull(_currDRIEData["WL_WIDTH"], 0);
                    WW = ADJUST_WIDTH;
                    WC = ADJUST_CENTER;
                    ADJUST_WOB = Utility.sDbnull(_currDRIEData["WL_WOB"], 0) == "1" ? true : false;
                    APPLY_WL = Utility.Int32Dbnull(_currDRIEData["APPLY_WL"], 0);
                    LOW = Utility.Int32Dbnull(_currDRIEData["LOW"], 0);
                    HIGH = Utility.Int32Dbnull(_currDRIEData["HIGH"], 0);
                    WL_LOW = Utility.Int32Dbnull(_currDRIEData["WL_LOW"], 0);
                    WL_HIGH = Utility.Int32Dbnull(_currDRIEData["WL_HIGH"], 0);
                    ApplyWL = APPLY_WL == 1 ? true : false;
                    GValue = Utility.Int32Dbnull(_currDRIEData["GAMMA_VALUE"], 0);
                    MEDValue = Utility.Int32Dbnull(_currDRIEData["MED_VALUE"], 0);
                    CValue = Utility.Int32Dbnull(_currDRIEData["CONTRAST_VALUE"], 0);
                    BValue = Utility.Int32Dbnull(_currDRIEData["BRIGHTNESS_VALUE"], 0);
                    MSEValue = Utility.Int32Dbnull(_currDRIEData["MSE_VALUE"], 0);
                    LOW = Utility.Int32Dbnull(_currDRIEData["LOW"], 0);
                    HIGH = Utility.Int32Dbnull(_currDRIEData["HIGH"], 0);
                    WL_LOW = Utility.Int32Dbnull(_currDRIEData["WL_LOW"], 0);
                    WL_HIGH = Utility.Int32Dbnull(_currDRIEData["WL_HIGH"], 0);

                    EEC = Utility.Int32Dbnull(_currDRIEData["MSE_EC"], 0);
                    EEL = Utility.Int32Dbnull(_currDRIEData["MSE_EL"], 0);
                    ELC = Utility.Int32Dbnull(_currDRIEData["MSE_LC"], 0);
                    ELL = Utility.Int32Dbnull(_currDRIEData["MSE_LL"], 0);
                    MSE_CommandType = Utility.Int32Dbnull(_currDRIEData["MSE_TYPE"], 0);
                    MSE_STT = Utility.Int32Dbnull(_currDRIEData["MSE_ORDER"], 0);
                    C_STT = Utility.Int32Dbnull(_currDRIEData["CONTRAST_ORDER"], 0);
                    B_STT = Utility.Int32Dbnull(_currDRIEData["BRIGHTNESS_ORDER"], 0);
                    G_STT = Utility.Int32Dbnull(_currDRIEData["GAMMA_ORDER"], 0);
                    WOB = Utility.sDbnull(_currDRIEData["WOB"], 0) == "1" ? true : false;

                    ApplyMB = Utility.sDbnull(_currDRIEData["APPLY_MOTIONBLUR"], 0) == "1" ? true : false;
                    _MBDimension = Utility.Int32Dbnull(_currDRIEData["MB_DIMENSION"], 0);
                    _Angle = Utility.Int32Dbnull(_currDRIEData["MB_ANGLE"], 0);
                    _MBSTT = Utility.Int32Dbnull(_currDRIEData["MB_STT"], 0);


                    ApplyAAlias = Utility.sDbnull(_currDRIEData["APPLY_ANTIALIAS"], 0) == "1" ? true : false;
                    _Threshold = Utility.Int32Dbnull(_currDRIEData["ANTIALIAS_THRESHOLD"], 0);
                    _AADimension = Utility.Int32Dbnull(_currDRIEData["ANTIALIAS_DIMENSION"], 0);
                    _Filter = Utility.Int32Dbnull(_currDRIEData["ANTIALIAS_FILTER"], 0);
                    _AA_STT = Utility.Int32Dbnull(_currDRIEData["ANTIALIAS_STT"], 0);


                    MSEAC = Utility.sDbnull(_currDRIEData["MSE_APPLY_ELGE_EHANCEMENT"], 0) == "1" ? true : false;
                    MSEAL = Utility.sDbnull(_currDRIEData["MSE_APPLY_LATITUDE_REDUCTION"], 0) == "1" ? true : false;

                    START_WIDTH = Utility.Int32Dbnull(_currDRIEData["START_WIDTH"], 0);
                    START_CENTER = Utility.Int32Dbnull(_currDRIEData["START_CENTER"], 0);
                    END_WIDTH = Utility.Int32Dbnull(_currDRIEData["END_WIDTH"], 0);
                    END_CENTER = Utility.Int32Dbnull(_currDRIEData["END_CENTER"], 0);

                    INVERT_AFTER = Utility.Int32Dbnull(_currDRIEData["INVERT_AFTER"], 0);
                    INVERT_STT = Utility.Int32Dbnull(_currDRIEData["INVERT_STT"], 0);
                    APPLY_INVERT = Utility.Int32Dbnull(_currDRIEData["APPLY_INVERT"], 0);
                    ApplyG = Utility.sDbnull(_currDRIEData["APPLY_GAMMA"], 0) == "1" ? true : false;
                    ApplyC = Utility.sDbnull(_currDRIEData["APPLY_CONTRAST"], 0) == "1" ? true : false;
                    APPLY_MED = Utility.sDbnull(_currDRIEData["APPLY_MED"], 0) == "1" ? true : false;
                    ApplyB = Utility.sDbnull(_currDRIEData["APPLY_BRIGHTNESS"], 0) == "1" ? true : false;
                    ApplyMSE = Utility.sDbnull(_currDRIEData["APPLY_MSE"], 0) == "1" ? true : false;

                }
            }
            catch
            {
            }
        }
        void LoadIEConfigbyIEID(int IE_ID, string ANATOMY_CODE, string PROJECTION_CODE)
        {
            try
            {
                DataSet ds = new ModalityController().GetIECONFIG(IE_ID);
                if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                {
                    //Thử load cấu hình đang được áp dụng cho một thiết bị bất kỳ trong hệ thống
                    ds = new ModalityController().GetIECONFIG(ANATOMY_CODE, PROJECTION_CODE);
                    if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    {
                        IMGConfigName = "NONE";
                        hasConfig = false;
                        START_WIDTH = -1;
                        return;
                    }
                }
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr != null)
                {
                    hasConfig = true;
                    APPLY_INVERT_FIRST = Utility.Int32Dbnull(dr["APPLY_INVERT_FIRST"], 0);
                    AUTO_MIN_MAX_BIT = Utility.Int32Dbnull(dr["AUTO_MIN_MAX_BIT"], 0);
                    APPLY_HEC = Utility.Int32Dbnull(dr["APPLY_HEC"], 0);
                    HEC_STT = Utility.Int32Dbnull(dr["HEC_STT"], 0);
                    MED_STT = Utility.Int32Dbnull(dr["MED_STT"], 0);

                    IMGConfigName = Utility.sDbnull(dr["IE_NAME"], "NONE");
                    WL_STT = Utility.Int32Dbnull(dr["WL_STT"], 0);
                    ADJUST_CENTER = Utility.Int32Dbnull(dr["WL_CENTER"], 0);
                    ADJUST_WIDTH = Utility.Int32Dbnull(dr["WL_WIDTH"], 0);
                    WW = ADJUST_WIDTH;
                    WC = ADJUST_CENTER;
                    ADJUST_WOB = Utility.sDbnull(dr["WL_WOB"], 0) == "1" ? true : false;
                    APPLY_WL = Utility.Int32Dbnull(dr["APPLY_WL"], 0);
                    LOW = Utility.Int32Dbnull(dr["LOW"], 0);
                    HIGH = Utility.Int32Dbnull(dr["HIGH"], 0);
                    WL_LOW = Utility.Int32Dbnull(dr["WL_LOW"], 0);
                    WL_HIGH = Utility.Int32Dbnull(dr["WL_HIGH"], 0);
                    ApplyWL = APPLY_WL == 1 ? true : false;
                    GValue = Utility.Int32Dbnull(dr["GAMMA_VALUE"], 0);
                    MEDValue = Utility.Int32Dbnull(dr["MED_VALUE"], 0);
                    CValue = Utility.Int32Dbnull(dr["CONTRAST_VALUE"], 0);
                    BValue = Utility.Int32Dbnull(dr["BRIGHTNESS_VALUE"], 0);
                    MSEValue = Utility.Int32Dbnull(dr["MSE_VALUE"], 0);
                    LOW = Utility.Int32Dbnull(dr["LOW"], 0);
                    HIGH = Utility.Int32Dbnull(dr["HIGH"], 0);
                    WL_LOW = Utility.Int32Dbnull(dr["WL_LOW"], 0);
                    WL_HIGH = Utility.Int32Dbnull(dr["WL_HIGH"], 0);

                    EEC = Utility.Int32Dbnull(dr["MSE_EC"], 0);
                    EEL = Utility.Int32Dbnull(dr["MSE_EL"], 0);
                    ELC = Utility.Int32Dbnull(dr["MSE_LC"], 0);
                    ELL = Utility.Int32Dbnull(dr["MSE_LL"], 0);
                    MSE_CommandType = Utility.Int32Dbnull(dr["MSE_TYPE"], 0);
                    MSE_STT = Utility.Int32Dbnull(dr["MSE_ORDER"], 0);
                    C_STT = Utility.Int32Dbnull(dr["CONTRAST_ORDER"], 0);
                    B_STT = Utility.Int32Dbnull(dr["BRIGHTNESS_ORDER"], 0);
                    G_STT = Utility.Int32Dbnull(dr["GAMMA_ORDER"], 0);
                    WOB = Utility.sDbnull(dr["WOB"], 0) == "1" ? true : false;

                    ApplyMB = Utility.sDbnull(dr["APPLY_MOTIONBLUR"], 0) == "1" ? true : false;
                    _MBDimension = Utility.Int32Dbnull(dr["MB_DIMENSION"], 0);
                    _Angle = Utility.Int32Dbnull(dr["MB_ANGLE"], 0);
                    _MBSTT = Utility.Int32Dbnull(dr["MB_STT"], 0);


                    ApplyAAlias = Utility.sDbnull(dr["APPLY_ANTIALIAS"], 0) == "1" ? true : false;
                    _Threshold = Utility.Int32Dbnull(dr["ANTIALIAS_THRESHOLD"], 0);
                    _AADimension = Utility.Int32Dbnull(dr["ANTIALIAS_DIMENSION"], 0);
                    _Filter = Utility.Int32Dbnull(dr["ANTIALIAS_FILTER"], 0);
                    _AA_STT = Utility.Int32Dbnull(dr["ANTIALIAS_STT"], 0);


                    MSEAC = Utility.sDbnull(dr["MSE_APPLY_ELGE_EHANCEMENT"], 0) == "1" ? true : false;
                    MSEAL = Utility.sDbnull(dr["MSE_APPLY_LATITUDE_REDUCTION"], 0) == "1" ? true : false;

                    START_WIDTH = Utility.Int32Dbnull(dr["START_WIDTH"], 0);
                    START_CENTER = Utility.Int32Dbnull(dr["START_CENTER"], 0);
                    END_WIDTH = Utility.Int32Dbnull(dr["END_WIDTH"], 0);
                    END_CENTER = Utility.Int32Dbnull(dr["END_CENTER"], 0);

                    INVERT_AFTER = Utility.Int32Dbnull(dr["INVERT_AFTER"], 0);
                    INVERT_STT = Utility.Int32Dbnull(dr["INVERT_STT"], 0);
                    APPLY_INVERT = Utility.Int32Dbnull(dr["APPLY_INVERT"], 0);
                    ApplyG = Utility.sDbnull(dr["APPLY_GAMMA"], 0) == "1" ? true : false;
                    ApplyC = Utility.sDbnull(dr["APPLY_CONTRAST"], 0) == "1" ? true : false;
                    APPLY_MED = Utility.sDbnull(dr["APPLY_MED"], 0) == "1" ? true : false;
                    ApplyB = Utility.sDbnull(dr["APPLY_BRIGHTNESS"], 0) == "1" ? true : false;
                    ApplyMSE = Utility.sDbnull(dr["APPLY_MSE"], 0) == "1" ? true : false;

                }
            }
            catch
            {
            }
        }
        bool _IsCropping=false;
        void grdAcquisitionList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string ImgDir = @"C:\";
                if (txtImgDir.Text.Trim() == "" || !Directory.Exists(txtImgDir.Text.Trim()))
                {
                    KillProcess("DROCDisplay.exe");
                    return;
                }
                else
                {
                    ImgDir = txtImgDir.Text.Trim();
                }

                string fileName = GetFileName(txtImgDir.Text.Trim(), ImgDir, FileName, null);
                CurrFileName = Path.GetFileNameWithoutExtension(fileName);
                if (fileName != "" && !isLoadding)
                {
                    mdlStatic.isDisplayImg = true;
                    int idx=0;
                    OpenDicom(ref _IsCropping, ref idx, fileName, false, false);
                }
                else
                {
                    if (fileName == "")
                    {
                        mdlStatic.isDisplayImg = false;
                        KillProcess("DROCDisplay.exe");
                    }
                }
            }
            catch
            {
            }
        }

        void grdWorkList_DoubleClick(object sender, EventArgs e)
        {
            _LastDoubleMode = AppType.AppEnum.DoubleMode.WorkList;
            _DoubleMode = AppType.AppEnum.DoubleMode.WorkList;
            BeginExam();
        }

        void grdWorkList_CurrentCellChanged(object sender, EventArgs e)
        {
            RegDetailController _RegDetailController = new RegDetailController();
            try
            {
                cmdEditWL.Enabled = true;
                cmdDelWL.Enabled = true;
                if (grdWorkList.CurrentRow == null)
                {
                    cmdEditWL.Enabled = false;
                    cmdDelWL.Enabled = false;
                }
                Int64 RegID = Convert.ToInt64(Utility.sDbnull(Utility.GetObjectValueFromGridColumn(grdWorkList, "colReg_ID", grdWorkList.CurrentRow.Index)));
                Hasresult = _RegDetailController.HasResult(RegID);
                cmdDelWL.Enabled = !Hasresult;
            }
            catch
            {
            }
        }

       



        #endregion
       
      

        private void ChangeForeColorOfGrid()
        {
            try
            {
                for (int i = 0; i <= grdStudyList.RowCount - 1; i++)
                {
                    if (Utility.GetValueFromGridColumn(grdStudyList, "colRegStatus1", i) == "2")
                    {
                        grdStudyList.Rows[i].DefaultCellStyle.ForeColor = Color.DarkMagenta;
                    }
                }
                grdStudyList.Invalidate();
                grdStudyList.Refresh();
            }
            catch
            {
            }
        }
       






        #region Laser Printer Setting and Events
       
      
        string GetColValue(DataSet dsPageSetting, string ColumnName, string defaulVal)
        {
            try
            {
                if (dsPageSetting == null || dsPageSetting.Tables.Count <= 0 || dsPageSetting.Tables[0].Rows.Count <= 0 || dsPageSetting.Tables[0].TableName.ToUpper() != "PAGESETTING") return defaulVal;
                return dsPageSetting.Tables[0].Rows[0]["ColumnName"].ToString();
            }
            catch
            {
                return defaulVal;
            }
        }
     
        #endregion

        #region check Current RAM
        void try2RenameExistedFile(string newFile)
        {
            try
            {

                if (File.Exists(newFile))
                {
                   
                        int _max = 0;
                        string[] _files = Directory.GetFiles(Path.GetDirectoryName(newFile));
                        foreach (string _file in _files)
                        {
                            if (_file.ToUpper().Contains("_IDX"))
                            {
                                int _idx = Convert.ToInt32(Path.GetFileNameWithoutExtension(_file).ToUpper().Replace(Path.GetFileNameWithoutExtension(newFile).ToUpper(), "").Replace("_IDX", ""));
                                if (_idx > _max) _max = _idx;
                            }
                        }
                        _max += 1;
                        string Ext = Path.GetExtension(newFile).ToUpper();
                        string _newFileWithIdx = newFile.ToUpper().Replace(Ext, "") + "_IDX" + _max.ToString() + Ext;
                        //Đổi tên file đang có cộng thêm Idx
                        File.Move(newFile, _newFileWithIdx);
                   
                }
                else
                {
                    Try2DelFile(newFile);
                }
            }
            catch
            {
            }
        }
        
      
        #endregion

        /// <summary>
        /// Tạo dữ liệu mẫu cho file Excel
        /// </summary>
        void CreateTemplate()
        {
            try
            {
                DataTable dt = new DataTable("DS_BNHAN");
                dt.Columns.AddRange(new DataColumn[] { 
                new DataColumn("MA_BN",typeof(string)),
                new DataColumn("TEN_BN",typeof(string)),
                new DataColumn("NGAY_SINH",typeof(string)),
                new DataColumn("GIOI_TINH",typeof(string)),
                new DataColumn("DIA_CHI",typeof(string)),
                new DataColumn("DVI_KHAMBENH",typeof(string)),
                new DataColumn("NGAY_CHUP",typeof(string)),
                new DataColumn("TEN_FILE",typeof(string))
                });
                //Add sample roWs
                AddRow(dt, "001", "Nguyễn Văn Nam", "01/01/1981", "Nam", "Hà Nội", "Bệnh viện Bạch Mai", "11/11/2011", @"C:\XQUANG\001.Dcm");
                AddRow(dt, "002", "Nguyễn Thị Nữ", "01/01/1982", "Nữ", "Hồ Chí Minh", "Bệnh viện Bạch Mai", "11/11/2011", @"C:\XQUANG\002.Dcm");
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.WriteXml(@"C:\Bunny\ExcelTemplate.xml", XmlWriteMode.WriteSchema);
            }
            catch
            {
            }
        }
        void AddRow(DataTable dt, string MA_BN, string TEN_BN, string NGAY_SINH, string GIOI_TINH, string DIA_CHI, string DVI_KHAMBENH, string NGAY_CHUP, string TEN_FILE)
        {
            DataRow dr = dt.NewRow();
            dr["MA_BN"] = MA_BN;
            dr["TEN_BN"] = TEN_BN;
            dr["NGAY_SINH"] = NGAY_SINH;
            dr["GIOI_TINH"] = GIOI_TINH;
            dr["DIA_CHI"] = DIA_CHI;
            dr["DVI_KHAMBENH"] = DVI_KHAMBENH;
            dr["NGAY_CHUP"] = NGAY_CHUP;
            dr["TEN_FILE"] = TEN_FILE;
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }
        /// <summary>
        /// Tạo dữ liệu mẫu cho file Excel
        /// </summary>
        void CreateDROCTemplate(bool AutoSave)
        {
            try
            {
                DataTable dt = new DataTable("DS_BNHAN");
                dt.Columns.AddRange(new DataColumn[] { 
                new DataColumn("MA_BN",typeof(string)),
                new DataColumn("TEN_BN",typeof(string)),
                new DataColumn("NGAY_SINH",typeof(string)),
                new DataColumn("GIOI_TINH",typeof(string)),
                new DataColumn("DIA_CHI",typeof(string))
                });
                //Add sample roWs
                AddDROCRow(dt, "001", "Nguyễn Văn Nam", "01/01/1981", "Nam", "Vinasin", "ABDOMENT", "AP", "L");
                AddDROCRow(dt, "002", "Nguyễn Thị Nữ", "01/01/1982", "Nữ", "VietNam AirLine", "PEVIS", "KUB", "L");
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                string fileName = "";
                if (!AutoSave)
                {
                    SaveFileDialog savDlg = new SaveFileDialog();
                    savDlg.Title = "Chọn nơi lưu file Excel mẫu";
                    if (savDlg.ShowDialog() == DialogResult.OK)
                        fileName = savDlg.FileName;
                }
                else
                    fileName = Application.StartupPath + @"\DROCExcelTemplate";
                if (fileName.Trim() != "")
                    Export2Excel(dt, "DS_BENHNHAN", fileName, true);

                ds.WriteXml(fileName, XmlWriteMode.WriteSchema);
            }
            catch
            {
            }
        }
        bool Export2Excel(DataTable dt, string SheetName, string fileName, bool OpenAfterSaving)
        {
            try
            {
                ExcelFile excelFile = new ExcelFile();

                ExcelWorksheet ws = excelFile.Worksheets.Add(SheetName);
                Hashtable hst = new Hashtable();


                ws.DefaultColumnWidth = 10000;
                //write columns Name
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    ws.Cells[0, i].Value = dt.Columns[i].ColumnName.ToLower();
                    hst.Add(i, 0);
                }
                //write data into worksheet
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dt.Columns.Count - 1; j++)
                    {

                        ws.Cells[i + 1, j].Value = dt.Rows[i][j];
                        int Max = Convert.ToInt32(hst[j]);
                        if (Max < dt.Rows[i][j].ToString().Length)
                            Max = dt.Rows[i][j].ToString().Length;
                        if (Max < dt.Columns[j].ColumnName.Length)
                            Max = dt.Columns[j].ColumnName.Length;


                        hst[j] = Max;
                    }
                }
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                    ws.Columns[i].Width = Convert.ToInt32(hst[i]) * 280;
                if (!fileName.ToUpper().Contains(".XLS"))
                    fileName = fileName + ".Xls";
                excelFile.SaveXls(fileName);
                if (OpenAfterSaving) try2OpenExcelFile(fileName);
                return true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lưu file Excel mẫu:\n" + ex.Message, "Thông báo");
                return false;
            }
        }
        void try2OpenExcelFile(string fileName)
        {
            try
            {
                System.Diagnostics.Process.Start(fileName);
            }
            catch
            {
            }
        }
        void AddDROCRow(DataTable dt, string MA_BN, string TEN_BN, string NGAY_SINH, string GIOI_TINH, string DIA_CHI, string VI_TRI_CHUP, string HUONG_CHUP, string KICH_THUOC_CO_THE)
        {
            DataRow dr = dt.NewRow();
            dr["MA_BN"] = MA_BN;
            dr["TEN_BN"] = TEN_BN;
            dr["NGAY_SINH"] = NGAY_SINH;
            dr["GIOI_TINH"] = GIOI_TINH;
            dr["DIA_CHI"] = DIA_CHI;
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }
        /// <summary>
        /// Khởi tạo một số sự kiện
        /// </summary>
        void InitEvents()
        {
            try
            {
                this.FormClosing += new FormClosingEventHandler(DROC_RibbonFormClosing);
                lblFPDStatus.DoubleClick += new EventHandler(lblFPDStatus_DoubleClick);
                cmdExpand1.Click += new EventHandler(cmdExpand1_Click);
                cboDevice.SelectedValueChanged += new EventHandler(cboDevice_SelectedValueChanged);
                grdWorkList.CurrentCellChanged += new EventHandler(grdWorkList_CurrentCellChanged);
                grdWorkList.DoubleClick += new EventHandler(grdWorkList_DoubleClick);
                grdWorkListSuspending.DoubleClick += new EventHandler(grdWorkListSuspending_DoubleClick);
                cboDeviceLogin.SelectedValueChanged += new EventHandler(cboDevice_SelectedValueChanged);
                grdStudyList.CurrentCellChanged += new EventHandler(grdStudyList_CurrentCellChanged);
                grdStudyList.MouseDoubleClick += new MouseEventHandler(grdStudyList_MouseDoubleClick);
                this.KeyDown += new KeyEventHandler(DROC_RibbonKeyDown);

                txtImgDir.KeyDown += new KeyEventHandler(txtImgDir_KeyDown);
                chkAutoStart.CheckedChanged += new EventHandler(chkAutoStart_CheckedChanged);
                #region Action on Acquisition Viewer
               

                #endregion
                Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            }
            catch
            {
            }
        }

        void chkAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            //SaveInfor();
        }

       

     

        void grdWorkListSuspending_DoubleClick(object sender, EventArgs e)
        {
            BeginExam();
        }
        void txtImgDir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                RegConfiguration.SaveSettings("hrk", "VBIT_DRTech_FOLDER", "IMG", txtImgDir.Text);
        }
        void lblNone_SelectedImg_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                lstNONE_SELECTED.DataSource = null;
                lstNONE_SELECTED.DataSource = Directory.GetFiles(txtImgDir.Text + @"\NONE_SELECTED");
                lstNONE_SELECTED.Location = new Point(0, 0);
                if (lstNONE_SELECTED.Size.Equals(new Size(4, 4)) && lstNONE_SELECTED.Items.Count > 0)
                    lstNONE_SELECTED.Size = new Size(534, 490);
                else
                    lstNONE_SELECTED.Size = new Size(4, 4);


            }
            catch
            {
            }
        }





      
        void Application_ApplicationExit(object sender, EventArgs e)
        {
           
        }


        void grdStudyList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RepeatAcq();

        }


        string Last_Anatomy = "";
        string Last_Projection = "";
        bool notConnecttoFPD = false;
       
        void DROC_RibbonKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
               
                
                if (e.Control && e.Alt && e.Shift && e.KeyCode == Keys.N) notConnecttoFPD = true;
                if (isAcq)
                {
                    if (e.KeyCode == Keys.N || e.KeyCode == Keys.E)
                    {
                        try
                        {
                            frm_QuickRegistration newForm = new frm_QuickRegistration(m_intCurrDevice1);
                            newForm.ImgPath = txtImgDir.Text.Trim();
                            newForm.WLDataSource = m_dtWLDataSource;
                            newForm.grdList = grdWorkList;
                            newForm.Act = action.Insert;
                            newForm.ShowDialog();
                            SetSuspendingInfo();
                            if (newForm.IsBeginExam)
                            {
                                ModifyWorkListButtons();
                                BeginExam();
                                ShortCut2AddProc(Last_Anatomy, Last_Projection, true, ref _newDetailID);
                            }
                        }
                        catch
                        {
                        }
                        return;
                    }
                   
                    if (e.KeyCode == Keys.P)
                    {
                       
                        return;
                    }
                    if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                    {
                       
                        return;
                    }
                    if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                    {
                       
                        return;
                    }
                   
                    if (e.KeyCode == Keys.O)
                    {
                      
                        return;
                    }
                }
                if (e.KeyCode == Keys.Enter) ProcessTabKey(true);

               
                if (e.Modifiers == Keys.Control && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
                {
                    //a.PerformClick();
                }
                if (e.Modifiers == Keys.Alt && (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left))
                {
                    //av.PerformClick();
                }
                if (e.KeyCode == Keys.F12)
                {
                    lstFPD560_DoubleClick(lstFPD560, new EventArgs());
                    //_FullScreen.ShowFullScreen();
                    return;
                }
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.R)
                {
                    mdlStatic.isDisplayImg = false;
                    isLoadding = false;
                    ViewImg();
                    return;
                }
                if (e.KeyCode == Keys.F2 && !Running)
                {
                    cmdRepeatAcquisition.PerformClick();
                }
                if (e.KeyCode == Keys.N && e.Control && e.Alt)
                {
                    UpdateDiagnostic();
                }
                if (e.KeyCode == Keys.F6)
                {
                    //SetPermission();
                }
                if (e.KeyCode == Keys.Escape && Running)
                {
                    cmdSendtoServer.PerformClick();
                }
                if (e.Modifiers == Keys.Control && (e.KeyCode == Keys.V))
                {
                    //bool FileErr = false;
                    //string Err = "";
                    //System.Collections.Specialized.StringCollection _StrClt = Clipboard.GetFileDropList();
                    //foreach (string s in _StrClt)
                    //{
                    //    try
                    //    {
                    //        OpenDicom(_DicomMedicalViewer._medicalViewer, s);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Err += @"File:" + s + " không hợp lệ!\n";
                    //        FileErr = true;
                    //    }
                    //}
                    //if (FileErr) MessageBox.Show(Err);
                }
            }
            catch
            {
                return;
            }
        }
        DataRow drPatient = null;
        bool isLoadingStudy = false;
        long Curr_Study_PatientID = -1;
        void grdStudyList_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (isLoadingStudy ||  !canbeContinued) return;
                if (grdStudyList.RowCount <= 0 || grdStudyList.CurrentRow == null)
                {
                    cmdRepeatAcquisition.Enabled = false;
                    cmdSendtoServer.Enabled = false;
                    return;
                }
                long PatientID = -1;

                drPatient = ((DataRowView)grdStudyList.CurrentRow.DataBoundItem).Row;
                PatientID = Convert.ToInt32(drPatient["Patient_ID"]);
                cmdRepeatAcquisition.Enabled = true;
                int Reg_Status = Convert.ToInt32(Utility.sDbnull(Utility.GetObjectValueFromGridColumn(grdStudyList, "colRegStatus1", grdStudyList.CurrentRow.Index)));
                cmdSendtoServer.Enabled = Reg_Status >= 1 ? true : false;

                if (Curr_Study_PatientID == PatientID && pnlThumbnailResult.Controls.Count > 0) return;
                Curr_Study_PatientID = PatientID;
                ShowResultbyThumbnail();

            }
            catch
            {
            }
            finally
            {

            }
        }
        #region ShowResultbyThumbnail
        void ShowResultbyThumbnail()
        {
            try
            {
                DataRow dr = ((DataRowView)grdStudyList.CurrentRow.DataBoundItem).Row;
                ShowThumbnail(dr);
            }
            catch
            {
            }
        }
        void ShowThumbnail(DataRow dr)
        {
            try
            {
                if (dr == null)
                {
                    ClearAllSchedule();
                    return;
                }

                long Reg_ID = Convert.ToInt64(dr["Reg_ID"]);
                string RegNumber2 = dr["Reg_NUMBER"].ToString();
                LoadScheduled(Reg_ID, RegNumber2);
            }
            catch
            {
            }
        }
        void LoadScheduled(long Reg_ID, string RegNumber2)
        {
            try
            {
                DataTable _dtAcquisitionDataSource = new RegDetailController().GetAllData(Reg_ID).Tables[0];
                CreateScheduledThumbnail(_dtAcquisitionDataSource, RegNumber2);
            }
            catch
            {
            }
        }
       
        void ClearAllThumbnailSchedule()
        {
            try
            {
                foreach (Control ctrl in pnlThumbnailResult.Controls)
                {
                    OScheduledControl _ScheduledControl = ctrl as OScheduledControl;
                    _ScheduledControl.BackgroundImage = null;
                    _ScheduledControl._AnatomyObject.BackgroundImage = null;
                    _ScheduledControl._AnatomyObject.Image = null;
                    _ScheduledControl.Dispose();
                    GC.Collect();
                }

            }
            catch
            {
            }
            finally
            {
                pnlThumbnailResult.Controls.Clear();
            }
        }
        void CreateScheduledThumbnail(DataTable _dtAcquisitionDataSource, string RegNumber2)
        {
            ClearAllThumbnailSchedule();
            if (_dtAcquisitionDataSource == null) return;
            m_intCurrentDetail_ID = -1;
            try
            {
                // List<OScheduledControl> lstCtrl = new List<OScheduledControl>();
                foreach (DataRow dr in _dtAcquisitionDataSource.Rows)
                {
                    OScheduledControl _OScheduledControl = new OScheduledControl(txtImgDir.Text.Trim() + @"\" + SubDirLv1(RegNumber2) + @"\" + SubDirLv2_Patient(drPatient) + @"\" + RegNumber2 + "_" + Utility.Int32Dbnull(dr["DETAIL_ID"]).ToString() + "_" + Utility.sDbnull(dr["ANATOMY_CODE"], "") + "_" + Utility.sDbnull(dr["PROJECTION_CODE"], ""), Utility.Int32Dbnull(dr["REG_ID"], ""), Utility.Int32Dbnull(dr["DETAIL_ID"], ""), Utility.sDbnull(dr["ANATOMY_CODE"], ""), Utility.sDbnull(dr["PROJECTION_CODE"], ""), Utility.sDbnull(dr["BODYSIZE_CODE"], ""), Utility.sDbnull(dr["VN_ANATOMY_NAME"], ""), Utility.sDbnull(dr["EN_ANATOMY_NAME"], ""), Utility.sDbnull(dr["VN_PROJECTION_NAME"], ""), Utility.sDbnull(dr["EN_PROJECTION_NAME"], ""), Utility.sDbnull(dr["VN_BODYSIZE_NAME"], ""), Utility.sDbnull(dr["EN_BODYSIZE_NAME"], ""), Utility.DecimaltoDbnull(dr["kVp"], 0.0), Utility.Int32Dbnull(dr["mA"], 0), Utility.DecimaltoDbnull(dr["mAs"], 0), Utility.Int32Dbnull(dr["A_STT"], 0), Utility.Int32Dbnull(dr["P_STT"], 0), Utility.Int32Dbnull(dr["PRINTCOUNT"], 0), Utility.Int32Dbnull(dr["Status"], 0));
                    _OScheduledControl._OnClick += new OScheduledControl.OnClick(_OScheduledControl__OnClick);
                    _OScheduledControl._OnDoubleClick += new OScheduledControl.OnDoubleClick(_OScheduledControl__OnDoubleClick);

                    pnlThumbnailResult.Controls.Add(_OScheduledControl);
                    _OScheduledControl._AnatomyObject.Size = new Size(109, 108);
                    _OScheduledControl._Title.Size = new Size(154, 29);
                    _OScheduledControl._IconObject.Size = new Size(45, 108);
                    _OScheduledControl.Size = new Size(154, 137);// originalSize;  
                    // lstCtrl.Add(_OScheduledControl);
                }
                //pnlThumbnailResult.Controls.AddRange(lstCtrl.ToArray());
            }
            catch
            {
            }
        }
        bool AutoSelectSchedule = false;
        int _Detail_ID = -1;
        void _OScheduledControl__OnDoubleClick(OScheduledControl obj)
        {
            try
            {
                AutoSelectSchedule = true;
                _Detail_ID = obj.DETAIL_ID;
                RepeatAcq();
            }
            catch
            {
            }
        }
        void _OScheduledControl__OnClick(OScheduledControl obj)
        {
            try
            {



                if (pnlThumbnailResult.Controls.Count <= 0)
                {

                    return;
                }

            }
            catch
            {
            }
            finally
            {


            }
        }
        #endregion

        void try2SysFree()
        {
            try
            {
                if (modName.ToUpper().Contains("FPD560") || modName.ToUpper().Contains("FPD500ETHERNET") || modName2.ToUpper().Contains("FPD560") || modName2.ToUpper().Contains("FPD500ETHERNET"))
                {
                    Sys_Free();
                    _CallBackFunc = null;
                }
            }
            catch
            {
            }
        }
        void try2SysFreeF500()
        {
            try
            {
                if (modName.ToUpper().Contains("FPD500PCI") || modName.ToUpper().Contains("FPD500") || modName.ToUpper().Contains("FPD550") || modName2.ToUpper().Contains("FPD500PCI") || modName2.ToUpper().Contains("FPD500") || modName2.ToUpper().Contains("FPD550"))
                {
                    Sys_FreeF500_PCI();
                }
            }
            catch
            {
            }
        }
        void try2SysFreeF550_PCI()
        {
            try
            {
                if (modName.ToUpper().Contains("FPD550PCI") || modName2.ToUpper().Contains("FPD550PCI"))
                {
                    Sys_FreeF550_PCI();
                }
            }
            catch
            {
            }
        }
        void FreeWLThread()
        {
            try
            {
                WLIsWorking = false;
                Thread.Sleep(1);
                if (WLThread != null)
                {
                    //Giải phóng Thread xử lý chuyển file giữa các thư mục
                    if (WLThread.ThreadState == ThreadState.Background || WLThread.ThreadState == ThreadState.Running || WLThread.ThreadState == ThreadState.WaitSleepJoin)
                        WLThread.Abort();
                    Application.DoEvents();
                    WLThread = null;

                }
            }
            catch
            {
            }
        }
        void FreeFPD550Thread()
        {
            try
            {
                FPD500_PCIIsWorking = 0;
                Thread.Sleep(100);
                if (TFPD500_PCI != null)
                {
                    //Giải phóng Thread xử lý chuyển file giữa các thư mục
                    if (TFPD500_PCI.ThreadState == ThreadState.Background || TFPD500_PCI.ThreadState == ThreadState.Running || TFPD500_PCI.ThreadState == ThreadState.WaitSleepJoin)
                    {
                        TFPD500_PCI.Abort();
                    }
                    TFPD500_PCI = null;

                }
            }
            catch
            {
            }
        }
        void FreeFPD550_PCIThread()
        {
            try
            {
                FPD550_PCIIsWorking = 0;
                Thread.Sleep(100);
                if (TFPD550_PCI != null)
                {
                    //Giải phóng Thread xử lý chuyển file giữa các thư mục
                    if (TFPD550_PCI.ThreadState == ThreadState.Background || TFPD550_PCI.ThreadState == ThreadState.Running || TFPD550_PCI.ThreadState == ThreadState.WaitSleepJoin)
                    {
                        TFPD550_PCI.Abort();
                    }
                    TFPD550_PCI = null;

                }
            }
            catch
            {
            }
        }
        void FreeFPD560Thread()
        {
            try
            {
                if (tfpd != null)
                {
                    //Giải phóng Thread xử lý chuyển file giữa các thư mục
                    if (tfpd.ThreadState == ThreadState.Background || tfpd.ThreadState == ThreadState.Running || tfpd.ThreadState == ThreadState.WaitSleepJoin)
                        tfpd.Abort();
                    tfpd = null;

                }
            }
            catch
            {
            }
        }
        void try2ResetTaskbar()
        {
            try
            {
                _FullScreen.ShowFullScreen();
                _FullScreen.ResetTaskBar();
            }
            catch
            {
            }
        }
        void try2FreeToshiba()
        {
            try
            {
                if (_AppMode == AppType.AppEnum.AppMode.License)
                {
                    if (modName.ToUpper().Contains("FDX4343R") || modName2.ToUpper().Contains("FDX4343R"))
                    {
                        TE_ExitWorkMode();
                        TE_ExitDetector();
                    }
                }
            }
            catch
            {
                TE_ExitDetector();
            }
        }
        void try2FreeToshiba_New()
        {
            try
            {
                if (_AppMode == AppType.AppEnum.AppMode.License)
                {
                    if (modName.ToUpper().Contains("FDX3543RP") || modName2.ToUpper().Contains("FDX3543RP"))
                    {
                        Try2StandBy();
                        this.cIfLower.Dispose();
                        CallbackFromFPD_Delegate = null;
                        cIfLower = null;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                }
            }
            catch
            {
                
            }
        }
        void DROC_RibbonFormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
             
                try2ResetTaskbar();
                FreeWLThread();
                FreeFPD560Thread();
                try2SysFree();
                FreeFPD550Thread();
                FreeFPD550_PCIThread();
                try2SysFreeF500();
                try2SysFreeF550_PCI();
                try2FreeToshiba();
                try2FreeToshiba_New();
                //}

            }
            catch
            {
            }
        }
       
     


        #region Load các thông tin cấu hình: Đơn vị làm việc, độ sáng tối, OverlayTextSize,...
        /// <summary>
        /// Load một số thông tin đã được cấu hình và lưu lại vào biến dùng chung toàn hệ thống
        /// </summary>
        void LoadHospitalInfor()
        {
            #region Load đơn vị làm việc-Chủ yếu phục vụ để burn vào ảnh khi in ra máy in Film
            try
            {
                string _filePath = Application.StartupPath + @"\Company.dat";
                if (!File.Exists(_filePath))
                {
                }
                else
                {
                    using (StreamReader _reader = new StreamReader(Application.StartupPath + @"\Company.dat"))
                    {
                        string obj = _reader.ReadLine();
                        if (obj != null) HospitalName = obj.ToString();
                        obj = _reader.ReadLine();
                        if (obj != null) DepartmentName = obj.ToString();
                        _reader.BaseStream.Flush();
                        _reader.Close();
                    }
                }
            }
            catch
            {
            }
            #endregion
        }
        void LoadAutoDetectConfig()
        {
            #region Load cấu hình cell
            try
            {
                string _filePath = Application.StartupPath + @"\CellConfig.dat";
                if (!File.Exists(_filePath))
                {

                    blnAutoDetect = false;
                }
                else
                {
                    using (StreamReader _reader = new StreamReader(_filePath))
                    {
                        string obj = _reader.ReadLine();
                        if (obj != null) OverlayTextSize = Convert.ToInt32(obj);
                        obj = _reader.ReadLine();
                        if (obj != null) blnAutoDetect = obj.ToString() == "1" ? true : false;
                    }
                }
            }
            catch
            {
            }

            #endregion
        }

        void LoadAutoContrastAndBrightnessConfig()
        {
            #region Load cấu hình độ sáng-tương phản
            try
            {
                string _filePath = Application.StartupPath + @"\Brightness.dat";
                if (!File.Exists(_filePath))
                {
                    BrightnessValue = 0;
                    ContrastValue = 0;
                    AutoContrastAndBrightness = false;
                }
                else
                {
                    using (StreamReader _reader = new StreamReader(_filePath))
                    {
                        string obj = _reader.ReadLine();
                        if (obj != null) BrightnessValue = Convert.ToInt32(obj);
                        obj = _reader.ReadLine();
                        if (obj != null) ContrastValue = Convert.ToInt32(obj);
                        obj = _reader.ReadLine();
                        if (obj != null) AutoContrastAndBrightness = obj.ToString() == "1" ? true : false;


                    }
                }
            }
            catch
            {
            }
            finally
            {

            }
            #endregion
        }
        #endregion
        void SetActforButtons()
        {
            cmdWL.Click += new EventHandler(_Click);
            cmdSL.Click += new EventHandler(_Click);
            cmdAcq.Click += new EventHandler(_Click);
            cmdConfig.Click += new EventHandler(_Click);

        }
        void ResetStatus()
        {
            cmdWL.BackColor = Color.WhiteSmoke;
            cmdWL.ForeColor = Color.Black;
            cmdSL.BackColor = Color.WhiteSmoke;
            cmdSL.ForeColor = Color.Black;
            cmdAcq.BackColor = Color.WhiteSmoke;
            cmdAcq.ForeColor = Color.Black;
            cmdConfig.BackColor = Color.WhiteSmoke;
            cmdConfig.ForeColor = Color.Black;

            cmdWL.FlatStyle = FlatStyle.Standard;
            cmdSL.FlatStyle = FlatStyle.Standard;
            cmdAcq.FlatStyle = FlatStyle.Standard;
            cmdConfig.FlatStyle = FlatStyle.Standard;
        }
        void _Click(object sender, EventArgs e)
        {
            ResetStatus();
            Button cmd = sender as Button;
            cmd.FlatStyle = FlatStyle.Standard;
            cmd.BackColor = Color.SteelBlue;
        }

        void _LostFocus(object sender, EventArgs e)
        {
            Button cmd = sender as Button;
            cmd.BackColor = Color.WhiteSmoke;
            cmd.ForeColor = Color.Black;
        }

        void _GotFocus(object sender, EventArgs e)
        {
            Button cmd = sender as Button;
            cmd.BackColor = Color.SteelBlue;//DimGray;
            cmd.ForeColor = Color.White;
        }
        void SetDocking()
        {
            pnlLogin.Dock = DockStyle.Fill;
            pnlMain.Dock = DockStyle.Fill;
            pnlWL.Dock = DockStyle.Fill;

            pnlAcq.Dock = DockStyle.Fill;

            pnlStudyListRegion.Dock = DockStyle.Fill;

            pnlSystemConfig.Dock = DockStyle.Fill;
        }
        void InitInfo()
        {
            try
            {
                this.textBoxDescription.Text = "Văn phòng Hà Nội" + System.Environment.NewLine + "Tầng 10 nhà B7, Ký túc xác Thăng Long, Đường Cốm Vòng, Quận Cầu giấy, Hà Nội" + System.Environment.NewLine + "ĐT: 84 4 2696620 / 21 Fax: 84 4 2696623 " + System.Environment.NewLine + "E-mail: viet-ba@hn.vnn.vn " + System.Environment.NewLine + "Văn phòng TP. Hồ Chí Minh " + System.Environment.NewLine + "58/16 Đường Thành Thái, P.12, Q.10 " + System.Environment.NewLine + "ĐT/Fax: (848) 863 2915 " + System.Environment.NewLine + "E-mail: viet-ba@hcm.vnn.vn";
                this.textBoxDescription.Text += System.Environment.NewLine + "Author:" + System.Environment.NewLine + "   Đào Văn Cường(09 15 15 01 48).";
            }
            catch
            {
            }
        }
        private void lnkVKLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!ExistsVK())
                ShowVK();
            else
                KillVK();
        }
        void ShowVK()
        {
            try
            {
                System.Diagnostics.Process.Start("KEYBOARD.exe");
            }
            catch
            {
            }
        }
        void KillVK()
        {
            try
            {
                System.Diagnostics.Process[] arrProcess = System.Diagnostics.Process.GetProcessesByName("KEYBOARD");
                if (arrProcess.Length > 0) arrProcess[0].Kill();
            }
            catch
            {
            }
        }
        void KillProcess(string appName)
        {
            try
            {
                System.Diagnostics.Process[] arrProcess = System.Diagnostics.Process.GetProcessesByName(appName);
                if (arrProcess.Length > 0) arrProcess[0].Kill();
            }
            catch
            {
            }
        }
        bool ExistsVK()
        {
            try
            {
                System.Diagnostics.Process[] arrProcess = System.Diagnostics.Process.GetProcessesByName("KEYBOARD");
                return arrProcess.Length > 0;
            }
            catch
            {
                return false;
            }
        }
         bool ExistsProcess(string processName)
        {
            try
            {
                System.Diagnostics.Process[] arrProcess = System.Diagnostics.Process.GetProcessesByName(processName);
                return arrProcess.Length > 0;
            }
            catch
            {
                return false;
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch
            {
            }
        }

        private void cmdMainClose_Click(object sender, EventArgs e)
        {
            try
            {

                if (new frm_LargeMsgBox(MultiLanguage.GetText(globalVariables.DisplayLanguage, "XÁC NHẬN THOÁT DROC", "SHUTDOWN SYSTEM"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "BẠN CÓ CHẮC CHẮN MUỐN THOÁT KHỎI HỆ THỐNG?", "DO YOU WANT TO SHUTDOWN SYSTEM?"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "ĐỒNG Ý", "YES"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "KHÔNG ĐỒNG Ý", "NO")).ShowDialog() == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
            catch
            {
                this.Close();
            }
        }


        void StartDROC()
        {
            pnlLogin.SendToBack();
            pnlMain.BringToFront();
            Application.DoEvents();
            InitControls();
            if (!LoginAfterLogOut) //Nếu là Login lần đầu thì khởi động DROC
                InitDROC();
            else//Nếu là Login vào lại hệ thống thì cần cấu hình lại các phần liên quan đến GCOM
            {
                tmrExSignal.Enabled = lblGCOM.ImageIndex == 0;
                // if (!chkGCOM.Checked) cmdSaveResult.Location = new Point(221, 5);
                // else cmdSaveResult.Location = new Point(221, 88);
                //cmdSaveResult.Size = chkGCOM.Checked ? new Size(205, 80) : new Size(205, 164);

            }
        }
        void ChangeUI(int idx)
        {

            pnlAcqPatientInfor.Visible = false;
            isAcq = false;
            switch (idx)
            {
                case 0:
                    pnlWL.BringToFront();

                    break;
                case 1:
                    isAcq = true;
                    pnlAcqPatientInfor.Visible = true;
                    pnlAcq.BringToFront();
                    break;
                case 2:
                    pnlStudyListRegion.BringToFront();
                    break;
                case 3:
                    pnlSystemConfig.BringToFront();
                    break;
                case 4:
                    //pnlHelp.BringToFront();
                    break;
                default:
                    break;
            }
            lblhint.Visible = GetSelectedScheduled() != null;
        }
        private void cmdWL_Click_1(object sender, EventArgs e)
        {
            //_DoubleMode = DoubleMode.WorkList;
            _currTab = AppType.AppEnum.TabMode.WorkList;
            ChangeUI(0);
        }

        private void cmdAcq_Click(object sender, EventArgs e)
        {
            _currTab = AppType.AppEnum.TabMode.Acq;
            ChangeUI(1);
        }

        private void cmdSL_Click(object sender, EventArgs e)
        {
            //_DoubleMode = DoubleMode.StudyList;
            _currTab = AppType.AppEnum.TabMode.StudyList;
            ChangeUI(2);
        }


        #region LOGIN
        private bool Login_blnIsValidData()
        {
            try
            {
                if (txtUID.Text.Trim().Equals(string.Empty))
                {
                    new frm_LargeMsgBoxOK(MultiLanguage.GetText(globalVariables.DisplayLanguage, "Thông báo", "WARNING"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Bạn phải nhập tên đăng nhập!", "YOU HAVE ENTER USERNAME"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Tôi đã hiểu", "OK"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Không rõ", "UNKNOWN")).ShowDialog();
                    txtUID.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        private bool bLoginSuccess()
        {
            clsUser sv_oUser = new clsUser();
            VietBaIT.Encrypt sv_oEncrypt = new VietBaIT.Encrypt(picCom.Tag.ToString());

            if (!globalVariables.gv_ConnectSuccess)
            {
                return false;
            }
            if (!sv_oUser.bIsExisted(txtUID.Text.Trim()))
            {
                Utility.ShowMsg("Không tồn tại người dùng có tên đăng nhập là " + txtUID.Text.Trim() + ". Đề nghị nhập lại", "Thông báo");
                txtUID.Focus();
                return false;
            }
            if (!sv_oUser.bLoginSuccess(txtUID.Text.Trim(), sv_oEncrypt.Mahoa(txtPWD.Text.Trim())))
            {
                MessageBox.Show("Sai mật khẩu đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPWD.Focus();
                return false;
            }
            return true;
        }
        void InitLogin()
        {
            string sv_sUID = null;
            string sv_sPwd = null;
            try
            {

                sv_sUID = RegConfiguration.GetSettings("DVC_COMPANY", "VBIT_DRTech_DVC", "APP_UID");
                sv_sPwd = RegConfiguration.GetSettings("DVC_COMPANY", "VBIT_DRTech_DVC", "APP_PWD");
                if (sv_sUID.Trim() == "")
                {
                    sv_sUID = UID;
                    sv_sPwd = PWD;
                }
                else
                {
                    UID = sv_sUID;
                    PWD = sv_sPwd;
                }
                txtUID.Text = UID.Trim();
                txtUID.Text = GetFirstValueFromFile(Application.StartupPath + @"\Account.Info");
                txtPWD.Text = "";
                //sv_sPwd.Trim
                txtUID.Focus();
            }
            catch (Exception ex)
            {

            }
        }
        private void cmdLogin_Click(object sender, EventArgs e)
        {
            string sv_sUID = string.Empty;
            string sv_sPWD = string.Empty;
            VietBaIT.Encrypt sv_oEncrypt = new VietBaIT.Encrypt(picCom.Tag.ToString());

            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (Login_blnIsValidData())
                {
                    if (bLoginSuccess())
                    {
                        globalVariables.UserName = txtUID.Text.Trim();
                        //globalVariables.pư = sv_oEncrypt.Mahoa(txtPWD.Text.Trim);
                        RegConfiguration.SaveSettings("DVC_COMPANY", "VBIT_DRTech_DVC", "APP_UID", txtUID.Text.Trim());
                        RegConfiguration.SaveSettings("DVC_COMPANY", "VBIT_DRTech_DVC", "APP_PWD", txtPWD.Text.Trim());
                        globalVariables.gv_ConnectSuccess = true;
                        SaveValue2File(Application.StartupPath + @"\Account.Info", txtUID.Text.Trim());
                        this.Cursor = Cursors.Default;

                        mv_bLoginSuccess = true;
                        StartDROC();
                        return;
                    }

                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                AppLogger.LogAction.LogActions("==>Login().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex.ToString());
                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }

        }
        void SaveValue2File(string fileName, string value)
        {
            try
            {
                using (StreamWriter _Writer = new StreamWriter(fileName))
                {
                    _Writer.Write(value);
                    _Writer.Flush();
                    _Writer.Close();
                }
            }
            catch
            {
            }
        }
        string GetFirstValueFromFile(string fileName)
        {
            try
            {
                if (!File.Exists(fileName)) return "";
                using (StreamReader _Reader = new StreamReader(fileName))
                {
                    object obj = _Reader.ReadLine();
                    if (obj == null) return "";
                    return obj.ToString().Trim();
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
        #endregion
        bool IsNeverSaveLastTime = true;
        private void cmdInsertWL_Click(object sender, EventArgs e)
        {
            
            InsertWL();
        }
        void InsertWL()
        {
            try
            {
                frm_Registration newForm = new frm_Registration(m_intCurrDevice1);
                newForm.ImgPath = txtImgDir.Text.Trim();
                newForm.WLDataSource = m_dtWLDataSource;
                newForm.grdList = grdWorkList;
                newForm.Act = action.Insert;
                _CurrAct = action.Insert;
                newForm.AutoGenPID = chkAutoGenPID.ImageIndex == 0;
                newForm.ShowDialog();
                SetSuspendingInfo();
                if (newForm.IsBeginExam)
                {
                    ModifyWorkListButtons();
                    BeginExam();
                }
            }
            catch
            {
            }
            finally
            {
                _CurrAct = action.FirstOrFinished;
                ModifyWorkListButtons();
            }
        }
        private void SetSuspendingInfo()
        {
            try
            {

                tabPageSuspendingPatients.Text = "Danh sách các BN đang tạm dừng Dịch vụ " + (grdWorkListSuspending.RowCount > 0 ? "(" + grdWorkListSuspending.RowCount.ToString() + ")" : "");
                tabPageRegPatient.Text = "Danh sách các BN đã đăng ký " + (grdWorkList.RowCount > 0 ? "(" + grdWorkList.RowCount.ToString() + ")" : "");
            }
            catch
            {
            }

        }

        private void cmdEditWL_Click(object sender, EventArgs e)
        {
            _CurrAct = action.Update;
            EditWL();
        }
        void EditWL()
        {
            try
            {
                DataRow _dr = null;
                frm_Registration newForm = new frm_Registration(m_intCurrDevice1);
                if (_currTab == AppType.AppEnum.TabMode.Acq)
                {
                    if (_LastDoubleMode == AppType.AppEnum.DoubleMode.WorkList)
                    {
                        Utility.GotoNewRow(grdWorkList, "colPATIENT_CODE", txtID2.Text.Trim());
                        if (grdWorkList.RowCount <= 0 || grdWorkList.CurrentRow == null)
                        {
                            Utility.ShowMsg("Bạn phải chọn Bệnh nhân trên lưới để sửa", "Thông báo");
                            grdWorkList.Focus();
                            return;
                        }
                        _dr = ((DataRowView)grdWorkList.CurrentRow.DataBoundItem).Row;
                        newForm.grdList = grdWorkList;
                    }
                    if (_LastDoubleMode == AppType.AppEnum.DoubleMode.StudyList)
                    {
                        Utility.GotoNewRow(grdStudyList, "colPATIENT_CODE1", txtID2.Text.Trim());
                        if (grdStudyList.RowCount <= 0 || grdStudyList.CurrentRow == null)
                        {
                            Utility.ShowMsg("Bạn phải chọn Bệnh nhân  trên lưới để sửa", "Thông báo");
                            grdStudyList.Focus();
                            return;
                        }
                        _dr = ((DataRowView)grdStudyList.CurrentRow.DataBoundItem).Row;
                        newForm.grdList = grdStudyList;
                    }
                }

                if (_currTab == AppType.AppEnum.TabMode.WorkList)
                {
                    if (grdWorkList.RowCount <= 0 || grdWorkList.CurrentRow == null)
                    {
                        Utility.ShowMsg("Bạn phải chọn Bệnh nhân trên lưới để sửa", "Thông báo");
                        grdWorkList.Focus();
                        return;
                    }
                    _dr = ((DataRowView)grdWorkList.CurrentRow.DataBoundItem).Row;
                    newForm.grdList = grdWorkList;
                }
                if (_currTab == AppType.AppEnum.TabMode.StudyList)
                {
                    if (grdStudyList.RowCount <= 0 || grdStudyList.CurrentRow == null)
                    {
                        Utility.ShowMsg("Bạn phải chọn Bệnh nhân  trên lưới để sửa", "Thông báo");
                        grdStudyList.Focus();
                        return;
                    }
                    _dr = ((DataRowView)grdStudyList.CurrentRow.DataBoundItem).Row;
                    newForm.grdList = grdStudyList;
                }

                newForm.ImgPath = txtImgDir.Text.Trim();


                newForm.Act = action.Update;
                //newForm.lblmsg1 = lblMsg;

                newForm.dr = _dr;
                Hasresult = new RegDetailController().HasResult(Convert.ToInt64(_dr["REG_ID"]));
                newForm.Hasresult = Hasresult;
                newForm.ShowDialog();
                if (newForm.blnRegOK)
                {
                    string Folder2Changed = "";
                    //Tự động update sang phần ACQ
                    UpdatePinforinAcqTab(Utility.Int32Dbnull(newForm.dr["Patient_ID"], -1));
                    //Kiểm tra nếu tên tuổi hoặc giới tính khác thông tin cũ thì tạo luồng cập nhật dữ liệu trong các file ảnh dicom
                    if (newForm.oldSex.Trim().ToUpper() != newForm.NewSex.Trim().ToUpper() || newForm.oldName.Trim().Replace(newForm.oldAge.Trim().ToUpper(), "").Trim().ToUpper() != newForm.NewName.Trim().Replace(newForm.NewAge.Trim().ToUpper(), "").Trim().ToUpper() || newForm.oldAge.Trim().ToUpper() != newForm.NewAge.Trim().ToUpper())
                    {

                        //Duyệt tất cả các ảnh trong thư mục ảnh của BN ở thời điểm hiện tại
                        if (newForm.oldFolderName.Trim().ToUpper() == newForm.newFolderName.Trim().ToUpper())
                        {
                            Folder2Changed = newForm.oldFolderName;
                        }
                        else//Tất cả các ảnh đã được chuyển sang thư mục mới-->Cần thay đổi thông tin dicomdataset trong thư mục mới
                        {
                            Folder2Changed = newForm.newFolderName;
                        }
                        //Thực hiện duyệt các file Dicom ko có chữ IDX
                        string[] files = Directory.GetFiles(newForm.oldFolderName);
                        foreach (string _fileName in files)
                        {
                            if (Path.GetExtension(_fileName).Trim().Contains("DCM") && !Path.GetExtension(_fileName).Trim().Contains("_IDX"))
                            {
                                try
                                {
                                    //Update DicomDataset
                                    //using (DicomDataSet ds = new DicomDataSet())
                                    //{
                                    //    ds.Load(_fileName, DicomDataSetLoadFlags.LoadAndClose);
                                    //    if (newForm.oldSex.Trim().ToUpper() != newForm.NewSex.Trim().ToUpper())
                                    //    {
                                    //        Leadtools.DicomDemos.Utils.SetTag(ds, DicomTag.PatientSex, newForm.NewSex.Trim().ToUpper(), true);
                                    //    }
                                    //    if (newForm.oldPID.Trim().ToUpper() != newForm.NewPID.Trim().ToUpper())
                                    //    {
                                    //        Leadtools.DicomDemos.Utils.SetTag(ds, DicomTag.PatientID, newForm.NewPID.Trim().ToUpper(), true);
                                    //    }
                                    //    if (newForm.oldName.Trim().ToUpper() != newForm.NewName.Trim().ToUpper())
                                    //    {
                                    //        Leadtools.DicomDemos.Utils.SetTag(ds, DicomTag.PatientName, newForm.NewName.Trim().ToUpper(), true);
                                    //    }
                                    //    if (newForm.oldAge.Trim().ToUpper() != newForm.NewAge.Trim().ToUpper())
                                    //    {
                                    //        Leadtools.DicomDemos.Utils.SetTag(ds, DicomTag.PatientAge, newForm.NewAge.Trim().ToUpper(), true);
                                    //    }
                                    //    ds.Save(_fileName, DicomDataSetSaveFlags.None);
                                    //}
                                }
                                catch
                                {
                                }
                            }

                        }

                        //Update Thumbnail
                        UpdateThumbnailImgFolder(Folder2Changed, Convert.ToInt32(_dr["REG_ID"]));

                    }


                }
            }
            catch
            {
            }
            finally
            {
                _CurrAct = action.FirstOrFinished;
            }
        }
        void UpdateThumbnailImgFolder(string newFolder, int REG_ID)
        {
            try
            {
                foreach (Control ctr in pnlScheduled.Controls)
                {
                    ScheduledControl _reObj = ctr as ScheduledControl;
                    if (_reObj.REG_ID == REG_ID)
                        _reObj.UpdateFolder(newFolder);
                    //if (_reObj.isPressed)
                        //if (_DicomMedicalViewer.IsValidCell())
                        //    _CurrCell.Tag = _reObj.DcmfileName;
                }
            }
            catch
            {
            }
        }
        private void cmdDelWL_Click(object sender, EventArgs e)
        {

            DeleteWL();
        }
        void DeleteWL()
        {
            try
            {
                if (grdWorkList.CurrentRow == null)
                {
                    mdlStatic.SetMsg(lblMsg, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Bạn phải chọn dòng để xóa", "you have select at least one row to delete"), true);
                    return;
                }
                if (new frm_LargeMsgBox(MultiLanguage.GetText(globalVariables.DisplayLanguage, "XÁC NHẬN XÓA ĐĂNG KÝ CỦA BỆNH NHÂN", "delete confirm"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Bạn có muốn xóa Đăng ký đang chọn hay không?", "Do you want to delete this registration?"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "ĐỒNG Ý XÓA", "YES"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "KHÔNG XÓA", "NO")).ShowDialog() == DialogResult.OK)
                {
                    DataRow dr = ((DataRowView)grdWorkList.CurrentRow.DataBoundItem).Row;
                    Int64 Patient_ID = Convert.ToInt64(dr["Patient_ID"]);
                    Int64 Reg_ID = Convert.ToInt64(dr["Reg_ID"]);
                    //Gọi nghiệp vụ xóa dữ liệu
                    WLRules _BusRule = new WLRules();
                    ActionResult DeleteResult = _BusRule.Delete(Patient_ID, Reg_ID);
                    if (DeleteResult == ActionResult.Success)//Nếu xóa thành công trong CSDL
                    {

                        m_dtWLDataSource.Rows.Remove(dr);
                        m_dtWLDataSource.AcceptChanges();

                        //Return to the InitialStatus
                        //Xoa ben StudyList neu co
                        DataRow[] STdr = m_dtStudyListDataSource.Select("Reg_ID=" + Reg_ID);
                        if (STdr.Length > 0)
                        {
                            m_dtStudyListDataSource.Rows.Remove(STdr[0]);
                            m_dtStudyListDataSource.AcceptChanges();
                        }
                        DataRow[] Acdr = m_dtAcquisitionDataSource.Select("Reg_ID=" + Reg_ID);
                        if (Acdr.Length > 0)
                        {
                            m_dtAcquisitionDataSource.Clear();
                            pnlScheduled.Controls.Clear();
                            txtAge.Clear();
                            txtName2.Clear();
                            txtRegNumber2.Clear();
                            txtSex.Clear();
                            txtID2.Clear();
                        }
                        mdlStatic.SetMsg(lblMsg, "Đã xóa dữ liệu ra khỏi hệ thống. Mời bạn tiếp tục thao tác", false);
                    }
                    else//Có lỗi xảy ra
                    {
                        switch (DeleteResult)
                        {
                            case ActionResult.DataHasUsedinAnotherTable:
                                mdlStatic.SetMsg(lblMsg, "Dữ liệu đã được sử dụng trong bảng khác nên bạn không thể xóa!", true);
                                break;
                            default:
                                mdlStatic.SetMsg(lblMsg, "Lỗi khi xóa . Liên hệ với VBIT", true);
                                break;
                        }
                    }
                }
                SetSuspendingInfo();
            }
            catch
            {
            }
            finally
            {
                SetSuspendingInfo();
                SetText(lblCaption, "Tổng số: " + grdStudyList.RowCount.ToString() + " Bệnh nhân");
                ModifyWorkListButtons();
                ModifyAcqButton();
                modifyStudyButtons();
            }
        }

        private void cmdBeginExamWL_Click(object sender, EventArgs e)
        {

            BeginExam();
        }
        long currREGID;
        ScheduledControl CurrSchedule = null;
        public void BeginExam()
        {
            try
            {
                if (tabControl2.SelectedIndex == 0 && (grdWorkList.RowCount <= 0 || grdWorkList.CurrentRow == null))
                {
                    mdlStatic.SetMsg(lblMsg, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Bạn phải chọn Bệnh nhân để bắt đầu chụp", "you have to choose one patient to begin service"), true);
                    return;
                }
                if (_CurrAct == action.FirstOrFinished)
                {
                    if (tabControl2.SelectedIndex == 1 && (grdWorkListSuspending.RowCount <= 0 || grdWorkListSuspending.CurrentRow == null))
                    {
                        mdlStatic.SetMsg(lblMsg, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Bạn phải chọn Bệnh nhân để bắt đầu chụp", "You have to choose one patient to begin service"), true);
                        return;
                    }
                }
                if (grdWorkList.CurrentRow == null && grdWorkListSuspending.CurrentRow == null) return;
                IsClicking = false;
                _Click(cmdAcq, new EventArgs());
                cmdAcq.PerformClick();

                AcquisitionFromWL = true;
                //mdlStatic.SetMsg(lblAcqMsg, "", true);
                DataRow dr;
                if (tabControl2.SelectedIndex == 0 || _CurrAct == action.Insert)
                    dr = ((DataRowView)grdWorkList.CurrentRow.DataBoundItem).Row;
                else
                {
                    dr = ((DataRowView)grdWorkListSuspending.CurrentRow.DataBoundItem).Row;
                }
                CurrStudyInstanceUID = Utility.sDbnull(dr["StudyInstanceUID"], ClearCanvas.Dicom.DicomUid.GenerateUid().UID);
                if (dr["Patient_ID"].ToString() == "-1") ExecuteSavingAction(dr, null);
                CurrPatient_ID = Utility.Int32Dbnull(dr["Patient_ID"], -1);
                txtID2.Text = Utility.sDbnull(dr["Patient_Code"]);

                txtName2.Text = Utility.sDbnull(dr["Patient_Name"]);
                txtRegNumber2.Text = Utility.sDbnull(dr["REG_Number"]);
                Sex = Sex2Sex(Utility.sDbnull(dr["Sex"]));
                txtSex.Text = Utility.sDbnull(dr["Sex_Name"]);
                RegDate = Convert.ToDateTime(dr["CREATED_DATE"]);
                BirthDate = Convert.ToDateTime(dr["BIRTH_DATE"]);
                if (File.Exists(Application.StartupPath + @"\use.age"))
                    txtAge.Text = Utility.sDbnull(dr["Age"].ToString(), "0");
                else
                    txtAge.Text = (DateTime.Now.Year - Convert.ToDateTime(dr["BIRTH_DATE"]).Year).ToString();
                _DoubleMode = AppType.AppEnum.DoubleMode.WorkList;
                _LastDoubleMode = AppType.AppEnum.DoubleMode.WorkList;
                _currTab = AppType.AppEnum.TabMode.Acq;
                lblPName.Text = txtName2.Text.Trim() + " - " + txtSex.Text.Trim() + " - " + txtAge.Text + " T";
                //Chuyển về trạng thái not Suspending
                long Reg_ID = Convert.ToInt64(dr["Reg_ID"]);
                currREGID = Reg_ID;
                if (tabControl2.SelectedIndex == 1) MoveRow(dr, true);
                m_dtAcquisitionDataSource = new RegDetailController().GetAllData(Reg_ID).Tables[0];
                mdlStatic.isDisplayImg = false;
                CreateScheduled();
                ModifyAcqButton();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("6402 " + ex.Message);
            }
        }
        int CurrPatient_ID = -1;
        void UpdatePinforinAcqTab(int Patient_ID)
        {
            try
            {
                if (Patient_ID == CurrPatient_ID)
                {
                    DataRow dr;
                    if (_DoubleMode == AppType.AppEnum.DoubleMode.WorkList)
                    {
                        if (tabControl2.SelectedIndex == 0)
                            dr = ((DataRowView)grdWorkList.CurrentRow.DataBoundItem).Row;
                        else
                        {
                            dr = ((DataRowView)grdWorkListSuspending.CurrentRow.DataBoundItem).Row;
                        }
                    }
                    else
                    {
                        dr = ((DataRowView)grdStudyList.CurrentRow.DataBoundItem).Row;
                    }
                    txtID2.Text = Utility.sDbnull(dr["Patient_Code"]);

                    txtName2.Text = Utility.sDbnull(dr["Patient_Name"]);
                    txtRegNumber2.Text = Utility.sDbnull(dr["REG_Number"]);
                    Sex = Sex2Sex(Utility.sDbnull(dr["Sex"]));
                    txtSex.Text = Utility.sDbnull(dr["Sex_Name"]);
                    RegDate = Convert.ToDateTime(dr["CREATED_DATE"]);
                    BirthDate = Convert.ToDateTime(dr["BIRTH_DATE"]);
                    if (File.Exists(Application.StartupPath + @"\use.age"))
                        txtAge.Text = Utility.sDbnull(dr["Age"].ToString());
                    else
                        txtAge.Text = (DateTime.Now.Year - Convert.ToDateTime(dr["BIRTH_DATE"]).Year).ToString();

                    lblPName.Text = txtName2.Text.Trim() + " - " + txtSex.Text.Trim() + " - " + txtAge.Text + " T";
                }
            }
            catch
            {
            }
        }
        void ClearAllSchedule()
        {
            try
            {
                foreach (Control ctrl in pnlScheduled.Controls)
                {
                    ScheduledControl _ScheduledControl = ctrl as ScheduledControl;
                    _ScheduledControl.BackgroundImage = null;
                    _ScheduledControl._AnatomyObject.BackgroundImage = null;
                    _ScheduledControl._AnatomyObject.Image = null;
                    _ScheduledControl.Dispose();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

            }
            catch
            {
            }
            finally
            {
                pnlScheduled.Controls.Clear();
            }
        }
        void CreateScheduled()
        {
            ClearAllSchedule();
            MaxSeriesInstanceUID = -1;
            m_intCurrentDetail_ID = -1;
            try
            {
                int CurrIdx = 1;
                string DefaultSeriesInstanceUID = "";
                string DefaultSOPInstanceUID = "";
                foreach (DataRow dr in m_dtAcquisitionDataSource.Rows)
                {
                    DefaultSeriesInstanceUID = CurrStudyInstanceUID + "." + CurrIdx.ToString();
                    CurrIdx++;
                    DefaultSeriesInstanceUID = Utility.sDbnull(dr["SeriesInstanceUID"], DefaultSeriesInstanceUID);
                    DefaultSOPInstanceUID = DefaultSeriesInstanceUID + ".1";
                    DefaultSOPInstanceUID = Utility.sDbnull(dr["SOPInstanceUID"], DefaultSOPInstanceUID);
                    int _temp = GetLastIdxOfInstanceUID(DefaultSeriesInstanceUID);
                    if (MaxSeriesInstanceUID <= _temp) MaxSeriesInstanceUID = _temp;
                    ScheduledControl _ScheduledControl = new ScheduledControl(txtImgDir.Text.Trim() + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient() + @"\" + txtRegNumber2.Text.Trim() + "_" + Utility.Int32Dbnull(dr["DETAIL_ID"]).ToString() + "_" + Utility.sDbnull(dr["ANATOMY_CODE"], "") + "_" + Utility.sDbnull(dr["PROJECTION_CODE"], ""), Utility.Int32Dbnull(dr["REG_ID"], ""), Utility.Int32Dbnull(dr["DETAIL_ID"], ""), CurrStudyInstanceUID, DefaultSeriesInstanceUID, DefaultSOPInstanceUID, Utility.sDbnull(dr["ANATOMY_CODE"], ""), Utility.sDbnull(dr["PROJECTION_CODE"], ""), Utility.sDbnull(dr["BODYSIZE_CODE"], ""), Utility.sDbnull(dr["VN_ANATOMY_NAME"], ""), Utility.sDbnull(dr["EN_ANATOMY_NAME"], ""), Utility.sDbnull(dr["VN_PROJECTION_NAME"], ""), Utility.sDbnull(dr["EN_PROJECTION_NAME"], ""), Utility.sDbnull(dr["VN_BODYSIZE_NAME"], ""), Utility.sDbnull(dr["EN_BODYSIZE_NAME"], ""), Utility.DecimaltoDbnull(dr["kVp"], 0.0), Utility.Int32Dbnull(dr["mA"], 0), Utility.DecimaltoDbnull(dr["mAs"], 0), Utility.Int32Dbnull(dr["A_STT"], 0), Utility.Int32Dbnull(dr["P_STT"], 0), Utility.Int32Dbnull(dr["PRINTCOUNT"], 0), Utility.Int32Dbnull(dr["Status"], 0));
                    _ScheduledControl._OnClick += new ScheduledControl.OnClick(_ScheduledControl__OnClick);
                    _ScheduledControl.ContextMenuStrip = ctx;
                    _ScheduledControl._OnNewScheduleClick += new ScheduledControl.OnNewScheduleClick(_ScheduledControl__OnNewScheduleClick);
                    _ScheduledControl._OnRejectScheduleClick += new ScheduledControl.OnRejectScheduleClick(_ScheduledControl__OnRejectScheduleClick);
                    _ScheduledControl._OnDelScheduleClick += new ScheduledControl.OnDelScheduleClick(_ScheduledControl__OnDelScheduleClick);
                    _ScheduledControl._OnNewScheduleDoubleClick += new ScheduledControl.OnNewScheduleDoubleClick(_ScheduledControl__OnNewScheduleDoubleClick);
                    _ScheduledControl._OnRejectScheduleDoubleClick += new ScheduledControl.OnRejectScheduleDoubleClick(_ScheduledControl__OnRejectScheduleDoubleClick);
                    _ScheduledControl._OnDelScheduleDoubleClick += new ScheduledControl.OnDelScheduleDoubleClick(_ScheduledControl__OnDelScheduleDoubleClick);


                    _ScheduledControl._OnKeyDown += new ScheduledControl.OnKeyDown(_ScheduledControl__OnKeyDown);
                    Size originalSize = _ScheduledControl.Size;
                    pnlScheduled.Controls.Add(_ScheduledControl);
                    _ScheduledControl.Size = new Size(124, 71);// originalSize;                    
                }
                MaxSeriesInstanceUID++;
                if (!AutoSelectSchedule)
                {
                    ScheduledControl _reObj = GetSelectedScheduled_NOTDONE();
                    if (_reObj != null)
                    {
                        //Nếu chưa có kết quả thì tự động chọn và scroll tới
                        pnlScheduled.ScrollControlIntoView(_reObj);
                        _reObj._AnatomyObject.PerformClick();
                    }
                    else//Tự động chọn 1 dịch vụ đã có ảnh
                    {
                        _reObj = GetAnyScheduled();
                        if (_reObj != null)
                        {
                            //Nếu chưa có kết quả thì tự động chọn và scroll tới
                            pnlScheduled.ScrollControlIntoView(_reObj);
                            _reObj._AnatomyObject.PerformClick();
                        }
                    }
                }
                if (AutoSelectSchedule)
                {
                    ScheduledControl _ObjbyID = GetScheduledbyID(_Detail_ID);
                    if (_ObjbyID != null)
                    {
                        //Nếu chưa có kết quả thì tự động chọn và scroll tới
                        pnlScheduled.ScrollControlIntoView(_ObjbyID);
                        _ObjbyID._AnatomyObject.PerformClick();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                AutoSelectSchedule = false;
                _Detail_ID = -1;
            }
        }
        int GetLastIdxOfInstanceUID(string _value)
        {
            if (_value.Trim() == "" || _value.IndexOf(".") < 0)
                return 1;
            return Convert.ToInt32(_value.Substring(_value.LastIndexOf(".") + 1));
        }
        void _ScheduledControl__OnDelScheduleClick(ScheduledControl obj)
        {
          
            DelSelectedProc(obj);
        }

        void _ScheduledControl__OnRejectScheduleClick(ScheduledControl obj)
        {
           
            RejectImage(obj);
        }

        void _ScheduledControl__OnNewScheduleClick(ScheduledControl obj)
        {
           
            ShortCut2AddProc(obj.A_Code, obj.P_Code, true, ref _newDetailID);
        }

        void _ScheduledControl__OnDelScheduleDoubleClick(ScheduledControl obj)
        {
           
            DelSelectedProc(obj);
        }

        void _ScheduledControl__OnRejectScheduleDoubleClick(ScheduledControl obj)
        {
           
            RejectImage(obj);
        }

        void _ScheduledControl__OnNewScheduleDoubleClick(ScheduledControl obj)
        {
           
            ShortCut2AddProc(obj.A_Code, obj.P_Code, true, ref _newDetailID);
        }

        void _ScheduledControl__OnKeyDown(ScheduledControl obj, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.E || e.KeyCode == Keys.N)
                ShortCut2AddProc(obj.A_Code, obj.P_Code, true, ref _newDetailID);
        }
        ScheduledControl GetScheduledbyID(int detailID)
        {
            try
            {
                foreach (Control ctr in pnlScheduled.Controls)
                {
                    ScheduledControl _reObj = ctr as ScheduledControl;
                    if (_reObj.DETAIL_ID == detailID)
                    {
                        return _reObj;
                    }
                }

                return null;
            }
            catch
            {

                return null;
            }
        }
        ScheduledControl GetSelectedScheduled_NOTDONE()
        {
            try
            {
                int i = 0;
                ScheduledControl _reObj = null;
                foreach (Control ctr in pnlScheduled.Controls)
                {
                    _reObj = ctr as ScheduledControl;
                    if (_reObj.Status == 0)
                    {
                        return _reObj;
                    }
                }
                if (_reObj == null && pnlScheduled.Controls.Count > 0)
                    _reObj = (ScheduledControl)pnlScheduled.Controls[0];
                return null;
            }
            catch
            {
                return null;
            }
        }
        ScheduledControl GetSelectedScheduled()
        {
            try
            {
                foreach (Control ctr in pnlScheduled.Controls)
                {
                    ScheduledControl _reObj = ctr as ScheduledControl;
                    if (_reObj.isPressed)
                    {
                        m_objCurrScheduledControl = _reObj;
                        return _reObj;
                    }
                }
                //m_objCurrScheduledControl=null;
                return null;
            }
            catch
            {
                // m_objCurrScheduledControl=null;
                return null;
            }
        }
        ScheduledControl GetSelectedScheduled_CompareDetailID()
        {
            try
            {
                foreach (Control ctr in pnlScheduled.Controls)
                {
                    ScheduledControl _reObj = ctr as ScheduledControl;
                    if (_reObj.isPressed || _reObj.DETAIL_ID == m_intCurrentDetail_ID)
                    {
                        m_objCurrScheduledControl = _reObj;
                        return _reObj;
                    }
                }
                //m_objCurrScheduledControl=null;
                return null;
            }
            catch
            {
                // m_objCurrScheduledControl=null;
                return null;
            }
        }
        ScheduledControl GetAnyScheduled()
        {
            try
            {
                foreach (Control ctr in pnlScheduled.Controls)
                {
                    ScheduledControl _reObj = ctr as ScheduledControl;
                    return _reObj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public void ResetPreviousSelectedObject(int DETAIL_ID)
        {
            try
            {
                foreach (Control ctr in pnlScheduled.Controls)
                {
                    ScheduledControl _ScheduledControl = ctr as ScheduledControl;
                    //if (_ScheduledControl.isPressed && _ScheduledControl.DETAIL_ID != DETAIL_ID)
                    if (_ScheduledControl.DETAIL_ID != DETAIL_ID)
                        _ScheduledControl.Reset();
                }
            }
            catch
            {
            }
        }
        void ControlStatus(ScheduledControl obj)
        {
            try
            {
                if (!obj.isPressed)
                {
                    if (m_objCurrScheduledControl.DETAIL_ID != obj.DETAIL_ID)
                    {
                        m_objCurrScheduledControl.isPressed = !m_objCurrScheduledControl.isPressed;
                        m_objCurrScheduledControl.ResetStatus(m_objCurrScheduledControl.Status);
                        m_objCurrScheduledControl = obj.Copy();
                    }
                    ResetPreviousSelectedObject(obj.DETAIL_ID);
                    obj.isPressed = !obj.isPressed;
                    m_objCurrScheduledControl.isPressed = obj.isPressed;

                    if (!obj.isPressed)
                    {
                        obj._AnatomyObject.BackColor = Color.WhiteSmoke;
                        obj._AnatomyObject.ForeColor = Color.Black;
                    }
                    else
                    {
                        obj._AnatomyObject.BackColor = Color.Yellow;
                        obj._AnatomyObject.ForeColor = Color.DarkBlue;
                    }
                }
            }
            catch
            {
            }
            finally
            {

            }
        }
        decimal kVp = 15;
        int mA;
        int mAs = 1;
        int AUTO_FLIPV = 0;
        int AUTO_FLIPH = 0;
        int LARGE_FOCUS = 1;
        bool isGCOM_VN = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        void _ScheduledControl__OnClick(ScheduledControl obj)
        {
            AUTO_FLIPV = 0;
            AUTO_FLIPH = 0;
            LARGE_FOCUS = 1;
            ScheduledControl _reObj = null;
            try
            {
                _isUseNewAPI = false;
              

                if (_AppMode == AppType.AppEnum.AppMode.Demo) _ViewState = AppType.AppEnum.ViewState.Ready;
                pnlScheduled.Enabled = _AppMode == AppType.AppEnum.AppMode.License ? false : true;
                if (_IsCropping)
                {
                   
                }
                //Thiết lập về chế độ mặc định của cell
                _IsCropping = false;


                if (pnlScheduled.Controls.Count <= 0)
                {
                    mnu2.Enabled = false;
                    return;
                }
                ControlStatus(obj);
                //Nếu chưa có dòng nào thì ko làm gì cả
                _reObj = GetSelectedScheduled();
                CurrSchedule = _reObj;
                if (_reObj == null)
                {

                    chkDynamicGrid.ImageIndex = 1;
                    cmdCreateDcmfromRaw.Enabled = false;
                    RAWFileNameWillbeCreated = "NONE_SELECTED";
                    mdlStatic.isDisplayImg = false;
                    AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Mời chọn vị trí chụp...", "Pls select a View position..."));
                    isLoadding = false;
                    obj.ResetStatus(obj.Status);
                    cmdReady.Text = MultiLanguage.GetText(globalVariables.DisplayLanguage, "Sẵn sàng", "Ready");
                    cmdReady.Tag = "R";
                  
                    if (!isGCOM_VN)
                    {
                        kVp = 15M;
                        mA = 0;
                        mAs = 1;
                        SetValueNumeric(lblKvpVal, kVp);
                        SetValueNumeric(lblmAVal, mA);
                        SetValueNumeric(lblmAsVal, mAs);
                    }
                    else
                    {
                        kVp = 45M;
                        mA = 10;
                        mAs = 10;
                        SetValueNumeric(lblAMEC_KvpVal, kVp);
                        SetValueNumeric(lblAMEC_mAVal, mA);
                        SetValueNumeric(lblAMEC_timeVal, mAs);
                    }

                    FirstExposure = true;
                    cmdLargeFocus_Click(cmdLargeFocus, new EventArgs());
                    return;
                }
                
                chkDynamicGrid.ImageIndex = new RegDetailController().GetValueOfField(_reObj.DETAIL_ID, "UsingGrid").ToString() == "1" ? 0 : 1;
                FirstExposure = _reObj.Status <= 0;
                if (_reObj.Status <= 0) AppLogger.LogAction.ShowEventStatus(lblFPDStatus, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Chờ chụp...", "Waiting for Hand-switch"));
                PlayBeepReady();
                //Nếu chọn dòng khác dòng đang chọn thì gửi dữ liệu cần chụp ra thư mục Senddata
                if (m_intCurrentDetail_ID != _reObj.DETAIL_ID)
                {
                    m_intCurrentDetail_ID = _reObj.DETAIL_ID;
                    //SendData();
                }
                m_intCurrentProcedure_ID = -1;// Convert.ToInt32(Utility.sDbnull(Utility.GetObjectValueFromGridColumn(grdAcquisitionList, "colProcedure_ID", grdAcquisitionList.CurrentRow.Index), "-1"));
                //Load cấu hình xử lý ảnh áp dụng cho Vị trí và hướng chụp hiện tại

                if (_FPDMode != AppType.AppEnum.FPDMode.Other)
                    LoadIEConfig(m_intCurrDevice1, _reObj.A_Code, _reObj.P_Code);
                else
                {
                    if (FPDSeq == 1)
                        LoadIEConfig(m_intCurrDevice1, _reObj.A_Code, _reObj.P_Code);
                    else
                        LoadIEConfig(m_intCurrDevice2, _reObj.A_Code, _reObj.P_Code);
                }
                _isUseNewAPI = IsNewAPI(_reObj.A_Code, _reObj.P_Code);
               
                new DoctorController().GetAutoFlip(_reObj.A_Code, _reObj.P_Code, -1, ref AUTO_FLIPV, ref AUTO_FLIPH, ref LARGE_FOCUS);
                //Auto select body size
                cmdMedium_Click(cmdMedium, new EventArgs());


                cmdReady.Text = _AppMode == AppType.AppEnum.AppMode.Demo ? MultiLanguage.GetText(globalVariables.DisplayLanguage, "Thực hiện chụp", "Exposure") : MultiLanguage.GetText(globalVariables.DisplayLanguage, "Sẵn sàng", "Ready");
                cmdReady.Tag = "R";
                //Lấy về tên file ứng với dịch vụ đang chọn của KH
                string v_strTempFile = txtRegNumber2.Text.Trim() + "_" + _reObj.DETAIL_ID.ToString().Trim() + "_" + _reObj.A_Code.Trim() + "_" + _reObj.P_Code.Trim();
                //Tạo tên raw file để capture từ FPD
                RAWFileNameWillbeCreated = v_strTempFile;
                toolTip1.SetToolTip(obj._AnatomyObject, obj.A_Code + "/" + obj.P_Code + MultiLanguage.GetText(globalVariables.DisplayLanguage, " đang sử dụng cấu hình xử lý ảnh " + IMGConfigName, " is using image processing configuration: " + IMGConfigName) + " - RAW file: " + RAWFileNameWillbeCreated + "; kVp=" + kVp.ToString() + " mA=" + mA.ToString() + " ms=" + mAs.ToString());
                if (FileName.ToUpper() != v_strTempFile.ToUpper())
                {
                    FileName = v_strTempFile;
                    mdlStatic.isDisplayImg = false;
                }

                string ImgDir = @"C:\";
                if (txtImgDir.Text.Trim() == "" || !Directory.Exists(txtImgDir.Text.Trim()))
                {
                    KillProcess("");
                    //return;
                }
                else
                {
                    ImgDir = txtImgDir.Text.Trim();
                }


                Int16 Status = (Int16)_reObj.Status;
                _reObj.ResetStatus(_reObj.Status);
                if (Status == 0)
                {

                    //cmdDelProc.Enabled = true;
                    mnu2.Enabled = true;

                }
                else
                {

                    //cmdDelProc.Enabled = false;
                    mnu2.Enabled = false;
                }

            }
            catch
            {
            }
            finally
            {
                //cmdFlipH.BackColor = AUTO_FLIPH == 1 || chkAutoHFlip.ImageIndex == 0 ? Color.Orange : Color.WhiteSmoke;
              
                if (LARGE_FOCUS == 1) cmdLargeFocus_Click(cmdLargeFocus, new EventArgs());
                else cmdSmallFocus_Click(cmdSmallFocus, new EventArgs());
                lblhint.Visible = _reObj != null;
                ModifyAcqButton();
                //Gọi thủ tục hiển thị ảnh của dịch vụ đang chọn
                //if (isAcq)
                //{

                BeginInvoke(new DisplayImg(ViewImg));
                //}
            }
        }

        void LoadAPParam(string ACODE, string PCODE, string BCODE)
        {

        }

        #region Beep Sound

        /// <summary>
        /// Hàm phát ra tiếng BIP trên Console
        /// </summary>
        /// <param name="times"> Số lần BIP</param>
        private static void PlayBeep(int times)
        {
            for (int i = 1; i <= times; i++)
            {
                Console.Beep();
            }
        }

        private static void PlayBeepInitSuccess()
        {
            PlayBeep(3);
        }

        private static void BEEP_RDY()
        {
        }

        private static void PlayBeepReady()
        {
            PlayBeep(1);
        }

        private static void PlayBeepShot()
        {
            PlayBeep(2);
        }

        private static void PlayBeepAbort()
        {
            PlayBeep(3);
        }

        #endregion

        private void MoveRow(DataRow dr, bool IsFromSuspending)
        {
            try
            {
                Int16 RegStatus = 0;

                Int64 Reg_ID = Convert.ToInt64(dr["Reg_ID"]);
                if (IsFromSuspending)//Active lên-->Tức chọn từ Tab Suspending
                {
                    DataRow[] arrDr = m_dtStudyListDataSource.Select("REG_ID=" + Reg_ID);
                    if (arrDr.GetLength(0) > 0)
                    {
                        arrDr[0]["REGSTATUS"] = 1;
                    }
                    dr["RegStatus"] = 1;
                    dr["Suspending_Time"] = DBNull.Value;
                    m_dtWLDataSource.ImportRow(dr);
                    m_dtWLDataSource_Suspending.Rows.Remove(dr);
                    RegStatus = 1;
                }
                else//Bị Suspending
                {
                    dr["RegStatus"] = 0;
                    dr["Suspending_Time"] = DateTime.Now;
                    m_dtWLDataSource_Suspending.ImportRow(dr);
                    m_dtWLDataSource.Rows.Remove(dr);
                    RegStatus = 0;
                }
                new RegController().UpdateStatus(Reg_ID, RegStatus, DateTime.Now);
                m_dtWLDataSource_Suspending.AcceptChanges();
                m_dtWLDataSource.AcceptChanges();
                SetSuspendingInfo();
                //Clear Acquisition Datasource
                m_dtAcquisitionDataSource.Rows.Clear();
            }
            catch
            {
            }
        }



        private void cmdEmerency_Click(object sender, EventArgs e)
        {
            cmdInsertWL_Click(cmdInsertWL, e);
            //CreateEmerency();
        }
        public void CreateEmerency()
        {
            try
            {
                action Act = action.Insert;
                DataTable dtPhysician = new DoctorController().GetAllData().Tables[0];
                if (dtPhysician == null || dtPhysician.Rows.Count <= 0)
                {
                    new frm_LargeMsgBoxOK(MultiLanguage.GetText(globalVariables.DisplayLanguage, "Thông báo", "Warning"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đề nghị tạo danh mục bác sĩ trước khi thêm BN cấp cứu!", "Pls create Doctor list before add Emerency"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "ĐỒNG Ý", "I see"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "HỦY BỎ", "Cancel")).ShowDialog();
                    return;
                }
                DataSet dsEmerencyDataEntity = new DataSet();
                DataTable EmerencyPatientEntity = new PatientEntity.PatientEntityDataTable();
                DataTable EmerencyRegEntity = new RegEntity.RegEntityDataTable();
                DataTable EmerencyRegDetailEntity = new RegDetailEntity.RegDetailEntityDataTable();

                Utility.ResetEntity(ref dsEmerencyDataEntity);
                Utility.ResetEntity(ref EmerencyPatientEntity);
                Utility.ResetEntity(ref EmerencyRegDetailEntity);
                Utility.ResetEntity(ref EmerencyRegEntity);
                //Create new Row
                string PID = Utility.GetYYYYMMDDHHMMSS(DateTime.Now);
                DataRow dr0 = EmerencyPatientEntity.NewRow();
                dr0["Patient_ID"] = -1;
                dr0["Age"] = 20;

                dr0["PATIENT_CODE"] = Utility.GetValue(PID, false);
                dr0["Patient_Name"] = "Bệnh nhân cấp cứu vào lúc " + PID;
                dr0["Birth_Date"] = DateTime.Now.AddYears(-20);
                dr0["sBirth_Date"] = DateTime.Now.AddYears(-20).ToString("dd/MM/yyyy");
                //dr0["Doctor_Name"] = cboPhysician.Text;
                dr0["Sex"] = 0;//Mặc định là Male
                dr0["EMERGENCY"] = 1;//BN cấp cứu

                EmerencyPatientEntity.Rows.Add(dr0);
                EmerencyPatientEntity.AcceptChanges();
                //Tao thong tin dang ky chup
                DataRow dr1 = EmerencyRegEntity.NewRow();
                dr1["REG_ID"] = -1;
                //dr1["REGSTATUS"] = 1;
                dr1["REG_NUMBER"] = dr0["PATIENT_CODE"];
                dr1["PATIENT_ID"] = dr0["Patient_ID"];
                //dr1["DESC"] = Utility.GetValue("Bệnh nhân cấp cứu cần chụp ngay", false);
                DataTable dtAP = new ProcedureController().GetEmerencyData().Tables[0];
                DataSet dsEmerencyDoctor = new DoctorController().GetEmerencyData();
                ArrayList Emerency_arrProc = new ArrayList();
                string ProcList = "";

                int totalProc = 0;
                if (dtAP == null || dtAP.Rows.Count <= 0)
                {
                    frm_Choose_Anotomy_Projection _Choose_Anotomy_Projection = new frm_Choose_Anotomy_Projection(-1);
                    if (_Choose_Anotomy_Projection.ShowDialog() == DialogResult.OK)
                    {
                        dtAP = _Choose_Anotomy_Projection.AP_DataSource.Select("CHON=1").CopyToDataTable();
                        ProcList = GetProcedureEmerency(dtAP);
                        dr1["PROCEDURELIST"] = ProcList;
                    }
                }
                else
                {
                    //Update Procedure List to DB and Datasource
                    ProcList = GetProcedureEmerency(dtAP);
                    dr1["PROCEDURELIST"] = ProcList;
                }
                //Tự động gán bác sĩ cấp cứu
                if (dsEmerencyDoctor != null && dsEmerencyDoctor.Tables.Count > 0 && dsEmerencyDoctor.Tables[0].Rows.Count > 0)
                    dr1["PHYSICIAN"] = dsEmerencyDoctor.Tables[0].Rows[0]["DOCTOR_CODE"].ToString();
                else
                    dr1["PHYSICIAN"] = dtPhysician.Rows[0]["DOCTOR_CODE"].ToString();
                dr1["CREATED_DATE"] = DateTime.Now;

                EmerencyRegEntity.Rows.Add(dr1);
                EmerencyRegEntity.AcceptChanges();

                //Tao thong tin dang ky chup chi tiet
                if (dtAP != null)
                {
                    foreach (DataRow drAP in dtAP.Rows)
                    {
                        DataRow dr2 = EmerencyRegDetailEntity.NewRow();
                        dr2["DETAIL_ID"] = -1;
                        dr2["REG_ID"] = dr1["REG_ID"];
                        dr2["ANATOMY_CODE"] = drAP["ANATOMY_CODE"];
                        dr2["PROJECTION_CODE"] = drAP["PROJECTION_CODE"];
                        dr2["STATUS"] = 0;
                        totalProc += 1;
                        EmerencyRegDetailEntity.Rows.Add(dr2);
                        EmerencyRegDetailEntity.AcceptChanges();
                    }
                }
                dsEmerencyDataEntity.Tables.Add(EmerencyPatientEntity);
                dsEmerencyDataEntity.Tables.Add(EmerencyRegEntity);
                dsEmerencyDataEntity.Tables.Add(EmerencyRegDetailEntity);

                //Now AutoSave Emerency Patient
                WLRules _BusRule = new WLRules();
                //Gọi nghiệp vụ Insert dữ liệu
                PatientInfor _PatientInfor = new PatientInfor();
                RegInfor _RegInfor = new RegInfor();
                ActionResult InsertResult = _BusRule.Insert(dsEmerencyDataEntity, _PatientInfor, _RegInfor);
                if (InsertResult == ActionResult.Success)//Nếu thành công
                {
                    //Thêm mới một dòng vào Datasource để cập nhật lại dữ liệu trên DataGridView
                    //phải đảm bảo Datasource và RoomEntity có cấu trúc giống nhau mới dùng hàm này
                    DataRow newRow = m_dtWLDataSource.NewRow();
                    newRow["Patient_ID"] = _PatientInfor.Patient_ID;
                    newRow["PATIENT_CODE"] = Utility.GetValue(PID, false);
                    newRow["Patient_Name"] = "Bệnh nhân cấp cứu vào lúc " + PID;
                    newRow["Birth_Date"] = DateTime.Now.AddYears(-20);
                    newRow["sBirth_Date"] = DateTime.Now.AddYears(-20).ToShortDateString();
                    newRow["Sex"] = 0;
                    newRow["Age"] = 0;
                    newRow["EMERGENCY"] = 20;
                    newRow["REG_ID"] = _RegInfor.REG_ID;
                    newRow["REG_NUMBER"] = newRow["PATIENT_CODE"];
                    newRow["REGSTATUS"] = 1;
                    newRow["PROCEDURELIST"] = ProcList;
                    if (dsEmerencyDoctor != null && dsEmerencyDoctor.Tables.Count > 0 && dsEmerencyDoctor.Tables[0].Rows.Count > 0)
                    {
                        newRow["PHYSICIAN"] = dsEmerencyDoctor.Tables[0].Rows[0]["DOCTOR_CODE"].ToString();
                        newRow["Doctor_Name"] = dsEmerencyDoctor.Tables[0].Rows[0]["DOCTOR_Name"].ToString();
                    }
                    else
                    {
                        newRow["PHYSICIAN"] = dtPhysician.Rows[0]["DOCTOR_CODE"].ToString();
                        newRow["Doctor_Name"] = dtPhysician.Rows[0]["DOCTOR_Name"].ToString();
                    }

                    newRow["CREATED_DATE"] = DateTime.Now;
                    newRow["sCREATED_DATE"] = DateTime.Now.ToShortDateString();
                    newRow["SEX_NAME"] = "Male";
                    newRow["CanDel"] = 1;
                    newRow["HasProcessed"] = 0;
                    newRow["NoneProcessed"] = 1;
                    newRow["TotalProc"] = totalProc;
                    if (newRow != null)//99.99% là sẽ !=null
                    {
                        m_dtWLDataSource.Rows.Add(newRow);
                        m_dtWLDataSource.AcceptChanges();
                    }
                    //Return to the InitialStatus
                    Act = action.FirstOrFinished;


                    //Tự động Suspending Bệnh nhân đang được chọn ở mục Acquisition
                    SuspendCurrentPatient();

                    //Nhảy đến bản ghi vừa thêm mới trên lưới. Do txtID chưa bị reset nên dùng luôn
                    Utility.GotoNewRow(grdWorkList, "colPatient_ID", _PatientInfor.Patient_ID.ToString());
                    //Tự động chuyển BN cấp cứu vào phần Acquisition
                    BeginExam();
                }
                else//Có lỗi xảy ra
                {
                    switch (InsertResult)
                    {
                        case ActionResult.ExistedRecord:
                            mdlStatic.SetMsg(lblMsg, "Lỗi trong khi tạo Bệnh nhân cấp cứu!\nĐã tồn tại Bệnh nhân có mã: " + _PatientInfor.Patient_Code + ". Đề nghị bạn xem lại", true);
                            //Utility.FocusAndSelectAll(txtID);
                            break;
                        default:
                            mdlStatic.SetMsg(lblMsg, "Lỗi trong quá trình thêm mới Bệnh nhân cấp cứu. Liên hệ với VBIT", true);
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        string GetProcedureEmerency(DataTable dtAP)
        {
            try
            {
                string s = "";
                foreach (DataRow dr in dtAP.Rows)
                {
                    s += dr["ANATOMY_CODE"].ToString() + "/" + dr["PROJECTION_CODE"].ToString() + ";";
                }
                if (s.Length > 0) s = s.Substring(0, s.Length - 1);
                return s;
            }
            catch
            {
                return "";
            }
        }
        private void cmdSearch1_Click(object sender, EventArgs e)
        {

            SearchStudy(false);
        }
        private void SearchStudy(bool dummy)
        {
            try
            {
                isLoadingStudy = true;
                pnlThumbnailResult.Controls.Clear();
                m_dtStudyListDataSource.Rows.Clear();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter DA = new OleDbDataAdapter(cmd);
                string PCode = txtSearchID1.Text.Trim() == "" ? "NOTHING" : txtSearchID1.Text.Trim();
                string PName = txtSearchName1.Text.Trim() == "" ? "NOTHING" : txtSearchName1.Text.Trim();
                Int16 pSex = Convert.ToInt16(cboSearchSex1.SelectedIndex - 1);
                if (pSex <= 0) pSex = 100;
                if (dummy) PCode = "ABCDEFGHIJK";
                
                Int64 RegNumber = txtSearchRegNum1.Text.Trim() == "" ? -1 : Convert.ToInt64(txtSearchRegNum1.Text.Trim());
                string RegFrom = lblFromDateST.ImageIndex == 0 ? dtpFromDate1.Text : "1/1/1900";
                string RegTo = lblFromDateST.ImageIndex == 0 ? dtpToDate1.Text : "1/1/1900";

                {
                    cmd.Connection = globalVariables.OleDbConnection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getSL";
                    cmd.Parameters.AddWithValue("@Patient_Code", PCode).Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@Patient_Name", PName).Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@regFrom", RegFrom).Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@regTo", RegTo).Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@Sex", pSex).Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@REG_NUMBER", RegNumber).Direction = ParameterDirection.Input;

                }

                DA.Fill(m_dtStudyListDataSource);
                ProcessData();
                Utility.SetDataSourceForDataGridView(grdStudyList, m_dtStudyListDataSource, false, true, "REGSTATUS in (2,3)", "PATIENT_CODE DESC");
                ChangeForeColorOfGrid();
            }
            catch (Exception ex)
            {
                mdlStatic.SetMsg(lblMsg, ex.Message, true);
            }
            finally
            {

                isLoadingStudy = false;
                if (grdStudyList.RowCount <= 0) pnlThumbnailResult.Controls.Clear();
                SetSuspendingInfo();
                SetText(lblCaption, "Tổng số: " + grdStudyList.RowCount.ToString() + " Bệnh nhân");
                modifyStudyButtons();
            }
        }

        private void ProcessData()
        {
            if(!m_dtStudyListDataSource.Columns.Contains("CHON")) m_dtStudyListDataSource.Columns.Add(new DataColumn("CHON",typeof(int)));
            foreach (DataRow dr in m_dtStudyListDataSource.Rows)
            {
                dr["CHON"] = 0;
                string Img = Utility.sDbnull(dr["Img"], "0/0");
                if (Img.Split('/')[0].Trim() == Img.Split('/')[1].Trim())
                    dr["img1"] = "Send All";
                else
                    dr["img1"] = dr["Img"];
            }
            
        }
        private void cmdSearchWL_Click(object sender, EventArgs e)
        {

            SearchWL(false);
        }
        #region "worklist"
       
       


        void try2SplitPName_Age_Sex(string OriginalName, ref string pName, ref string Age, ref string Sex)
        {
            try
            {
                string _NUM = "0123456789";
                string[] _words = OriginalName.Split(' ');
                Age = "";
                Sex = "M";
                string Age2 = "";
                foreach (string s in _words)
                {
                    //Chỉ phân tích các từ có chữ số
                    if (s.IndexOfAny(_NUM.ToCharArray()) >= 0)
                    {
                        if (Utility.IsNumeric(s))
                        {
                            Age += s.Trim();
                        }
                        for (int i = 0; i <= s.Length - 1; i++)
                        {
                            if (Utility.IsNumeric(s.Substring(i, 1)))
                                Age2 += s.Substring(i, 1).Trim();
                        }
                    }
                }
                if (!Utility.IsNumeric(Age))
                {
                    if (!Utility.IsNumeric(Age2))
                        Age = "0";
                    else
                        Age = Age2;
                }
                pName = OriginalName.Trim();
                if (Age2.Trim() != "" && Age.Trim() != "")
                {
                    pName = OriginalName.Substring(0, OriginalName.IndexOf(Age)).Trim();
                    string ageSex = OriginalName.Replace(pName, "").Trim();
                    if (ageSex.Trim() != "")
                        Sex = ageSex.Replace(Age, "").Replace("T", "").Trim();
                }

            }
            catch
            {
            }
            finally
            {
            }
        }
       
        string WLLogFile = Application.StartupPath + @"\DROCLogs\WorkListLog\WLLog_" + Utility.GetYYMMDD(DateTime.Now) + ".txt";



      
        private DataTable GetAPfromMWL(DataTable dtAP, string Patient_Code)
        {
            DataTable _reval = dtAP.Clone();
            try
            {
                AppLogger.LogAction.LogActions(WLLogFile, "Bắt đầu ánh xạ RadCode");
                if (arrRadCode.Contains(Patient_Code))
                {
                    string[] arrCodeValues = arrRadCode[Patient_Code].ToString().Split(';');
                    foreach (string CodeValue in arrCodeValues)
                    {
                        if (CodeValue.Trim() != "")
                        {

                            DataRow[] arrDr = dtAP.Select("RAD_CODE='" + CodeValue + "'");
                            if (arrDr.Length > 0)//Chắc chắn chỉ có 1 dòng
                            {
                                AppLogger.LogAction.LogActions(WLLogFile, "RadCode=" + CodeValue + " đã được ánh xạ");
                                if (_reval.Select("RAD_CODE='" + CodeValue + "'").Length <= 0) _reval.ImportRow(arrDr[0]);
                            }
                            else
                            {
                                AppLogger.LogAction.LogActions(WLLogFile, "RadCode=" + CodeValue + " không tìm được ánh xạ");
                            }
                        }
                    }
                }
                AppLogger.LogAction.LogActions(WLLogFile, "Kết thúc ánh xạ RadCode");
                AppLogger.LogAction.LogActions(WLLogFile, "============================================================================");
                return _reval;
            }
            catch
            {
                return _reval;
            }
        }
        void AddProcSearchedFromMWL(string Patient_Code)
        {
            try
            {
                DataTable APParams_DataSource = new DoctorController().GetAnatomyProjectionParams().Tables[0];
                if (m_dtAcquisitionDataSource == null || m_dtAcquisitionDataSource.Columns.Count <= 0 || m_dtAcquisitionDataSource.Rows.Count <= 0)
                {
                    m_dtAcquisitionDataSource = new RegDetailController().GetAllData(-1).Tables[0];
                }
                DataRow dr;
                m_dtAcquisitionDataSource.Rows.Clear();
                if (m_dtAcquisitionDataSource.Rows.Count > 0)
                {
                    dr = m_dtAcquisitionDataSource.Rows[0];
                    if (dr["reg_id"].ToString() == "-1") dr["REG_ID"] = currREGID;
                }
                else
                {
                    dr = m_dtAcquisitionDataSource.NewRow();

                    dr["STATUS"] = 0;
                    dr["IMGNAME"] = "";
                    dr["PRINTCOUNT"] = 0;
                    dr["EXPOSURECOUNT"] = 0;
                    dr["HOST"] = "127.0.0.1";
                    dr["UsingGrid"] = 0;
                    dr["IMGDATA"] = DBNull.Value;
                    dr["REG_NUMBER"] = txtRegNumber2.Text.Trim();
                    dr["REG_ID"] = currREGID;


                }
                DataTable dtAP = new DoctorController().GetAnatomyProjection(-1).Tables[0];
                DataTable dtDefaultAP = new ProcedureController().GetEmerencyData().Tables[0];
                //Lọc chỉ các dịch vụ được gửi từ worklistServer
                dtAP = GetAPfromMWL(dtAP, Patient_Code);
                if (dtAP != null && dtDefaultAP != null && dtAP.Columns.Count > 0 && dtDefaultAP.Columns.Count > 0)
                {
                    try
                    {
                        foreach (DataRow drDefaultAP in dtDefaultAP.Rows)
                        {
                            if (dtAP.Select("ANATOMY_CODE='" + drDefaultAP["ANATOMY_CODE"].ToString() + "' AND PROJECTION_CODE='" + drDefaultAP["PROJECTION_CODE"].ToString() + "'").Length <= 0)
                                dtAP.ImportRow(drDefaultAP);
                        }
                    }
                    catch
                    {
                    }
                }
                DataRow newdr = m_dtAcquisitionDataSource.NewRow();
                Utility.CopyData(dr, ref newdr);
                if (dtAP != null)
                {
                    string SeriesInstanceUID = "";
                    foreach (DataRow drAP in dtAP.Rows)
                    {
                        RegDetailInfor infor = new RegDetailInfor();

                        int NextSeriesInstanceUID = MaxSeriesInstanceUID + 1;
                        SeriesInstanceUID = CurrStudyInstanceUID + "." + NextSeriesInstanceUID.ToString();
                        MaxSeriesInstanceUID = NextSeriesInstanceUID;
                        newdr["DETAIL_ID"] = -1;
                        //SeriesInstanceUID=StudyInstanceUID+Số thứ tự dịch vụ trong lần đăng ký đó
                        newdr["SeriesInstanceUID"] = SeriesInstanceUID;
                        //SOPInstanceUID=SeriesInstanceUID+Số lần chụp của dịch vụ đó. 
                        //SOPInstanceUID chỉ thay đổi khi dịch vụ này đang có ảnh và lại được chụp lại
                        newdr["SOPInstanceUID"] = SeriesInstanceUID + ".1";
                        newdr["REG_ID"] = dr["REG_ID"];
                        newdr["ANATOMY_CODE"] = drAP["ANATOMY_CODE"];
                        newdr["BODYSIZE_CODE"] = BODYSIZE_CODE;
                        newdr["UsingGrid"] = 0;
                        newdr["PROJECTION_CODE"] = drAP["PROJECTION_CODE"];
                        newdr["DISPLAY_NAME"] = drAP["ANATOMY_CODE"];
                        newdr["STANDARD_NAME"] = drAP["ANATOMY_CODE"];
                        newdr["DirectionCapture"] = 0;
                        newdr["STATUS"] = 0;
                        newdr["STATUS_NAME"] = "";
                        Utility.MapValueFromEntityIntoObjectInfor(infor, newdr);
                        if (new RegDetailController(infor).Insert() == ActionResult.Success)
                        {
                            newdr["DETAIL_ID"] = infor.DETAIL_ID;
                            decimal _kVp = 0M;
                            int _mA = 0;
                            int _mAs = 0;
                            GetAPParams(APParams_DataSource, infor.ANATOMY_CODE, infor.PROJECTION_CODE, infor.BODYSIZE_CODE, ref _kVp, ref _mA, ref _mAs);
                            //Add new Scheduled Control
                            ScheduledControl _Scheduled = new ScheduledControl(txtImgDir.Text.Trim() + @"\" + txtRegNumber2.Text.Trim() + "_" + infor.DETAIL_ID.ToString(), (int)infor.REG_ID, (int)infor.DETAIL_ID, CurrStudyInstanceUID, SeriesInstanceUID, newdr["SOPInstanceUID"].ToString(), infor.ANATOMY_CODE, infor.PROJECTION_CODE, infor.BODYSIZE_CODE, Utility.sDbnull(drAP["VN_ANATOMY_NAME"], ""), Utility.sDbnull(drAP["EN_ANATOMY_NAME"], ""), Utility.sDbnull(drAP["VN_PROJECTION_NAME"], ""), Utility.sDbnull(drAP["EN_PROJECTION_NAME"], ""), BODYSIZE_NAME, BODYSIZE_NAME, _kVp, _mA, _mAs, Utility.Int32Dbnull(drAP["A_STT"], 0), Utility.Int32Dbnull(drAP["P_STT"], 0), Utility.Int32Dbnull(dr["PRINTCOUNT"], 0), 0);
                            _Scheduled._OnClick += new ScheduledControl.OnClick(_ScheduledControl__OnClick);
                            _Scheduled.ContextMenuStrip = ctx;
                            _Scheduled._OnNewScheduleClick += new ScheduledControl.OnNewScheduleClick(_ScheduledControl__OnNewScheduleClick);
                            _Scheduled._OnRejectScheduleClick += new ScheduledControl.OnRejectScheduleClick(_ScheduledControl__OnRejectScheduleClick);
                            _Scheduled._OnDelScheduleClick += new ScheduledControl.OnDelScheduleClick(_ScheduledControl__OnDelScheduleClick);

                            _Scheduled._OnNewScheduleDoubleClick += new ScheduledControl.OnNewScheduleDoubleClick(_ScheduledControl__OnNewScheduleDoubleClick);
                            _Scheduled._OnRejectScheduleDoubleClick += new ScheduledControl.OnRejectScheduleDoubleClick(_ScheduledControl__OnRejectScheduleDoubleClick);
                            _Scheduled._OnDelScheduleDoubleClick += new ScheduledControl.OnDelScheduleDoubleClick(_ScheduledControl__OnDelScheduleDoubleClick);



                            _Scheduled._OnKeyDown += new ScheduledControl.OnKeyDown(_ScheduledControl__OnKeyDown);
                            pnlScheduled.Controls.Add(_Scheduled);
                        }

                        m_dtAcquisitionDataSource.Rows.Add(newdr);
                        m_dtAcquisitionDataSource.AcceptChanges();
                        newdr = m_dtAcquisitionDataSource.NewRow();
                    }
                }


                if (!AcquisitionFromWL)
                {
                    //string[] Img = Utility.sDbnull(currentStudyRow["Img"]).Split('/');
                    //currentStudyRow["Img"] = Img[0] + "/" + (Convert.ToInt64(Img[1]) + _newForm.arrProc.Count).ToString();
                    //currentStudyRow["Img1"] = currentStudyRow["Img"];
                }
                //Update Procedure List to DB and Datasource
                string ProcedureList = GetProcedureList();
                new RegDetailController().UpdateProcedureList(Convert.ToInt64(dr["REG_ID"]), ProcedureList);
                //Update Dataset
                DataRow[] drWL = m_dtWLDataSource.Select("Reg_ID=" + Convert.ToInt64(dr["REG_ID"]));
                DataRow[] drST = m_dtStudyListDataSource.Select("Reg_ID=" + Convert.ToInt64(dr["REG_ID"]));
                if (drWL.Length > 0)
                    drWL[0]["ProcedureList"] = ProcedureList;
                if (drST.Length > 0)
                    drST[0]["ProcedureList"] = ProcedureList;
                m_dtWLDataSource.AcceptChanges();
                m_dtStudyListDataSource.AcceptChanges();

            }
            catch
            {
            }

        }

        void autoSearch()
        {
            try
            {
                WLIsWorking = true;
                Thread.Sleep(1);
                while (nmrSecond.Enabled && WLIsWorking)
                {
                    if (!WLhasjustApplied) Thread.Sleep(Convert.ToInt32(nmrSecond.Value * 1000));
                    WLhasjustApplied = false;
                    //SearchWLfromWLServer();
                }
            }
            catch
            {
            }
        }
        private string strCommonAAConfig = Application.StartupPath + @"\DROCConfigs\CommonAA.txt";
        void LoadAASettings()
        {

            try
            {

                if (!File.Exists(strCommonAAConfig))
                {
                    Common_AADimension = 8;
                    Common_Threshold = 5;
                    Common_Filter = 1;

                    return;
                }
                else
                {
                    using (StreamReader _reader = new StreamReader(strCommonAAConfig))
                    {
                        string obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            Common_AADimension = Convert.ToInt32(obj);
                        }
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            Common_Threshold = Convert.ToInt32(obj);
                        }
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            Common_Filter = Convert.ToInt32(obj);
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {

            }
        }
        void LoadDeviceErrSettings()
        {

            try
            {

                if (!File.Exists(strSettingDeviceError))
                {
                    
                    lblDisplayOptions.ImageIndex = 1;
                    lblGridMode.ImageIndex = 1;
                   
                    chkSaveReject.ImageIndex = 0;
                   
                  
                    chkAutoSend.ImageIndex = 0;
                  
                    chkAutoGenPID.ImageIndex = 0;
                    lblGCOM_VN.ImageIndex = 1;
                 
                    isGCOM_VN = lblGCOM_VN.ImageIndex == 0;
                    return;
                }
                else
                {
                    using (StreamReader _reader = new StreamReader(strSettingDeviceError))
                    {
                        string obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            lblDisplayOptions.ImageIndex = obj.ToString() == "1" ? 0 : 1;
                        }
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            lblGridMode.ImageIndex = obj.ToString() == "1" ? 0 : 1;
                        }
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            chkSaveReject.ImageIndex = obj.ToString() == "1" ? 0 : 1;
                        }
                      
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            chkAutoSend.ImageIndex = obj.ToString() == "1" ? 0 : 1;
                        }
                        
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            chkAutoGenPID.ImageIndex = obj.ToString() == "1" ? 0 : 1;
                        }
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            lblGCOM_VN.ImageIndex = obj.ToString() == "1" ? 0 : 1;
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                isGCOM_VN = lblGCOM_VN.ImageIndex == 0;
                chkDynamicGrid.Visible = lblGridMode.ImageIndex == 1;
               
            }
        }
        void LoadParamSettings()
        {

            try
            {

                if (!File.Exists(strSettingParam))
                {

                    return;
                }
                else
                {
                    using (StreamReader _reader = new StreamReader(strSettingParam))
                    {
                        string obj = _reader.ReadLine();
                        if (obj != null && Utility.IsNumeric(obj))
                        {
                            MinKvp = Convert.ToDecimal(obj);
                        }
                        obj = _reader.ReadLine();
                        if (obj != null && Utility.IsNumeric(obj))
                        {
                            MaxKvp = Convert.ToDecimal(obj);
                        }
                        obj = _reader.ReadLine();
                        if (obj != null && Utility.IsNumeric(obj))
                        {
                            StepKvp = Convert.ToDecimal(obj);
                        }
                        //mA
                        obj = _reader.ReadLine();
                        if (obj != null && Utility.IsNumeric(obj))
                        {
                            MinmA = Convert.ToInt32(obj);
                        }
                        obj = _reader.ReadLine();
                        if (obj != null && Utility.IsNumeric(obj))
                        {
                            MaxmA = Convert.ToInt32(obj);
                        }
                        obj = _reader.ReadLine();
                        if (obj != null && Utility.IsNumeric(obj))
                        {
                            StepmA = Convert.ToInt32(obj);
                        }
                        //mAs

                        obj = _reader.ReadLine();
                        if (obj != null && Utility.IsNumeric(obj))
                        {
                            MinmAs = Convert.ToInt32(obj);
                        }
                        obj = _reader.ReadLine();
                        if (obj != null && Utility.IsNumeric(obj))
                        {
                            MaxmAs = Convert.ToInt32(obj);
                        }
                        obj = _reader.ReadLine();
                        if (obj != null && Utility.IsNumeric(obj))
                        {
                            StepmAs = Convert.ToInt32(obj);
                        }
                        nmrMinKvp.Value = MinKvp;
                        nmrMaxKvp.Value = MaxKvp;
                        nmrStepKvp.Value = StepKvp;

                        nmrMinmA.Value = MinmA;
                        nmrMaxmA.Value = MaxmA;
                        nmrStepmA.Value = StepmA;


                        nmrMinms.Value = MinmAs;
                        nmrMaxms.Value = MaxmAs;
                        nmrStepms.Value = StepmAs;


                        lblKvpVal.Minimum = MinKvp;
                        lblKvpVal.Maximum = MaxKvp;
                        lblKvpVal.Increment = StepKvp;

                        lblmAVal.Minimum = MinmA;
                        lblmAVal.Maximum = MaxmA;
                        lblmAVal.Increment = StepmA;

                        lblmAsVal.Minimum = MinmAs;
                        lblmAsVal.Maximum = MaxmAs;
                        lblmAsVal.Increment = StepmAs;

                    }
                }
            }
            catch
            {
            }
            finally
            {

            }


        }
        void LoadSettings()
        {

            //try
            //{

            //    if (!File.Exists(_WLconfigFilePath))
            //    {
            //        return;
            //    }
            //    else
            //    {
            //        using (StreamReader _reader = new StreamReader(_WLconfigFilePath))
            //        {
            //            //AETitle
            //            string obj = _reader.ReadLine();
            //            if (obj != null)
            //            {
            //                server.AETitle = Convert.ToString(obj);
            //            }
            //            //Port option
            //            obj = _reader.ReadLine();
            //            if (obj != null)
            //            {
            //                server.Port = Convert.ToInt32(obj);
            //            }
            //            //IPaddress option
            //            obj = _reader.ReadLine();
            //            if (obj != null)
            //            {
            //                server.Address = IPAddress.Parse(Convert.ToString(obj));
            //                server.ServerAddress = Convert.ToString(obj);
            //            }
            //            //Timeout option
            //            obj = _reader.ReadLine();
            //            if (obj != null)
            //            {
            //                server.Timeout = Convert.ToInt32(obj);
            //            }
            //            //Save option
            //            obj = _reader.ReadLine();
            //            if (obj != null)
            //            {
            //                lblSaveAfterSearching.ImageIndex = obj.ToString() == "1" ? 0 : 1;
            //            }
            //            //workList Option
            //            obj = _reader.ReadLine();
            //            if (obj != null)
            //            {
            //                lblSearchWorkList.ImageIndex = obj.ToString() == "1" ? 0 : 1;
            //                nmrSecond.Enabled = obj.ToString() == "1" ? true : false;
            //            }
            //            //Time to AutoSearch
            //            obj = _reader.ReadLine();
            //            if (obj != null)
            //            {
            //                nmrSecond.Value = Convert.ToDecimal(obj);
            //            }
            //            //AutoConvert Option
            //            obj = _reader.ReadLine();
            //            if (obj != null)
            //            {
            //                lblAutoDcmConverter.ImageIndex = obj.ToString() == "1" ? 0 : 1;
            //                IsUsingDicomConverter = lblAutoDcmConverter.ImageIndex == 0 ? true : false;
            //            }
            //            WLAETitle = server.AETitle;


            //            Port = server.Port;


            //            ServerIp.Text = server.Address.ToString();
            //            ServerAE.Text = server.AETitle;
            //            ServerPort.Text = server.Port.ToString();
            //            Timeout.Text = server.Timeout.ToString();
            //            ClientAE.Text = WLAETitle;
            //            ClientPort.Text = Port.ToString();

            //        }
            //    }
            //}
            //catch
            //{
            //}


        }

        /// <summary>
        /// Lưu thông tin đơn vị phục vụ in lên phim vào file Company.txt
        /// </summary>
        void SaveSettings()
        {
            //try
            //{
            //    using (StreamWriter writer = new StreamWriter(_WLconfigFilePath))
            //    {
            //        writer.WriteLine(server.AETitle);
            //        writer.WriteLine(server.Port);
            //        writer.WriteLine(server.Address);
            //        writer.WriteLine(server.Timeout);
            //        writer.WriteLine(lblSaveAfterSearching.ImageIndex == 1 ? 0 : 1);
            //        writer.WriteLine(lblSearchWorkList.ImageIndex == 0 ? 1 : 0);
            //        writer.WriteLine(nmrSecond.Value.ToString());
            //        writer.WriteLine(lblAutoDcmConverter.ImageIndex == 0 ? 1 : 0);
            //        writer.Flush();
            //        writer.Close();
            //    }
            //}
            //catch
            //{
            //}

        }
        void LoadAllConfigs()
        {
            LoadSettings();
            LoadAASettings();
            LoadDeviceErrSettings();
            LoadTimeConfig();
            LoadParamSettings();
          

        }
        void InitWLComunication()
        {
            try
            {
                //cfind.ImplementationClass = CONFIGURATION_IMPLEMENTATIONCLASS;
                //cfind.ProtocolVersion = CONFIGURATION_PROTOCOLVERSION;
                //cfind.ImplementationVersionName = CONFIGURATION_IMPLEMENTATIONVERSIONNAME;
                //LoadAllConfigs();
                ////Init Events
                //cfind.Status += new Leadtools.Commands.DicomDemos.StatusEventHandler(cfind_Status);
                //cfind.FindComplete += new FindCompleteEventHandler(cfind_FindComplete);
                //cfind.MoveComplete += new MoveCompleteEventHandler(cfind_MoveComplete);
                //ServerIp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ServerIp_KeyPress);
                //ServerPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Port_KeyPress);
            }
            catch
            {
            }
        }
        //private void cfind_Status(object sender, Leadtools.Commands.DicomDemos.StatusEventArgs e)
        //{

        //}
        //private void cfind_MoveComplete(object sender, MoveCompleteEventArgs e)
        //{


        //}
        //private void cfind_FindComplete(object sender, FindCompleteEventArgs e)
        //{
        //    switch (e.Type)
        //    {
        //        case FindType.Study:

        //            foreach (DicomDataSet ds in e.Datasets)
        //            {
        //                AddStudyItem(ds);
        //            }

        //            break;

        //        case FindType.StudySeries:
        //            //StartUpdate(listViewSeries);
        //            //foreach (DicomDataSet ds in e.Datasets)
        //            //{
        //            //    AddSeriesItem(ds);
        //            //}
        //            //EndUpdate(listViewSeries);
        //            break;
        //    }

        //}
        private string GetDateStringFromDicomTagValue(string value)
        {
            try
            {
                if (value.Trim().Length != 8)
                    return DateTime.Now.ToString("dd/MM/yyyy");
                return value.Substring(6, 2) + "/" + value.Substring(4, 2) + "/" + value.Substring(0, 4);

            }
            catch
            {
                return DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        private DateTime GetDateTimeFromDicomTagValue(string value)
        {
            try
            {
                if (value.Trim().Length != 8)
                    return DateTime.Now;
                return new DateTime(Convert.ToInt32(value.Substring(0, 4)), Convert.ToInt32(value.Substring(4, 2)), Convert.ToInt32(value.Substring(6, 2)));

            }
            catch
            {
                return DateTime.Now;
            }
        }
        private DateTime GetDateTimeFromDicomTagValue(int day, int month, int year)
        {
            try
            {

                return new DateTime(year, month, day);

            }
            catch
            {
                return DateTime.Now;
            }
        }
       
        #region Lưu dữ liệu tìm được từ workList
        private void ExecuteSavingAction(DataRow drPInfor, DataTable dtAP)
        {
            try
            {

                //Gán RoomEntity vào DataEntity
                SetValueforEntity(drPInfor, dtAP);
                WLRules _BusRule = new WLRules();

                //Gọi nghiệp vụ Insert dữ liệu
                PatientInfor _PatientInfor = new PatientInfor();
                RegInfor _RegInfor = new RegInfor();
                ActionResult InsertResult = _BusRule.Insert(DataEntity, _PatientInfor, _RegInfor);
                if (InsertResult == ActionResult.Success)//Nếu thành công
                {
                    currREGID = _RegInfor.REG_ID;
                    //Thêm dịch vụ đăng ký
                    AddProcSearchedFromMWL(_PatientInfor.Patient_Code);
                    //Thêm mới một dòng vào Datasource để cập nhật lại dữ liệu trên DataGridView
                    //phải đảm bảo Datasource và RoomEntity có cấu trúc giống nhau mới dùng hàm này
                    DataRow[] _arrDR = m_dtWLDataSource.Select("PATIENT_CODE='" + _PatientInfor.Patient_Code + "'");
                    if (_arrDR.Length > 0)
                    {
                        _arrDR[0]["Patient_ID"] = _PatientInfor.Patient_ID;
                        drPInfor["Patient_ID"] = _PatientInfor.Patient_ID;
                        _arrDR[0]["REG_ID"] = _RegInfor.REG_ID;




                        _arrDR[0]["HasProcessed"] = 0;
                        _arrDR[0]["NoneProcessed"] = 1;
                        _arrDR[0]["TotalProc"] = 1;


                        m_dtWLDataSource.AcceptChanges();


                    }
                }
                else//Có lỗi xảy ra
                {

                    switch (InsertResult)
                    {
                        case ActionResult.ExistedRecord:
                            mdlStatic.SetMsg(lblMsg, "Đã tồn tại Bệnh nhân có mã: " + _PatientInfor.Patient_Code + ". Đề nghị bạn xem lại", true);

                            break;
                        default:
                            mdlStatic.SetMsg(lblMsg, "Lỗi trong quá trình thêm mới Bệnh nhân. Liên hệ với VBIT", true);
                            break;
                    }
                }
            }
            catch
            {
            }
        }
        private void SetValueforEntity(DataRow drPInfor, DataTable dtAP)
        {
            try
            {
                Utility.ResetEntity(ref DataEntity);
                Utility.ResetEntity(ref PatientEntity);
                Utility.ResetEntity(ref _RegDetailEntity);
                Utility.ResetEntity(ref RegEntity);
                //Create new Row
                DataRow dr0 = PatientEntity.NewRow();
                dr0["Patient_ID"] = -1;
                dr0["PATIENT_CODE"] = drPInfor["PATIENT_CODE"];
                dr0["Age"] = drPInfor["Age"];
                dr0["Patient_Name"] = drPInfor["Patient_Name"];
                dr0["PATIENT_NAME_UNSIGNED"] = drPInfor["PATIENT_NAME_UNSIGNED"];
                dr0["PATIENT_NAME_NOSPACE"] = drPInfor["PATIENT_NAME_NOSPACE"];
                dr0["Birth_Date"] = drPInfor["Birth_Date"];
                dr0["sBirth_Date"] = drPInfor["sBirth_Date"];
                //dr0["Doctor_Name"] = cboPhysician.Text;
                dr0["Sex"] = drPInfor["Sex"];
                dr0["EMERGENCY"] = drPInfor["EMERGENCY"];
                dr0["CREATED_BY"] = drPInfor["CREATED_BY"];

                PatientEntity.Rows.Add(dr0);
                PatientEntity.AcceptChanges();
                //Tao thong tin dang ky chup
                string StudyInstanceUID = ClearCanvas.Dicom.DicomUid.GenerateUid().UID;
                DataRow dr1 = RegEntity.NewRow();
                dr1["REG_ID"] = -1;
                dr1["StudyInstanceUID"] = StudyInstanceUID;
                dr1["REG_NUMBER"] = drPInfor["PATIENT_CODE"];
                dr1["PATIENT_ID"] = dr0["Patient_ID"];
                dr1["DESC"] = "";

                dr1["PROCEDURELIST"] = GetProcedure(dtAP);
                dr1["PHYSICIAN"] = "";
                dr1["CREATED_DATE"] = drPInfor["CREATED_DATE"];
                dr1["Status"] = 0;
                RegEntity.Rows.Add(dr1);
                RegEntity.AcceptChanges();

                //Tao thong tin dang ky chup chi tiet
                if (dtAP != null)
                {
                    int CurrIdx = 1;
                    string SeriesInstanceUID = "";
                    foreach (DataRow drAP in dtAP.Rows)
                    {
                        DataRow dr2 = _RegDetailEntity.NewRow();
                        SeriesInstanceUID = StudyInstanceUID + "." + CurrIdx.ToString();
                        CurrIdx++;
                        dr2["DETAIL_ID"] = -1;
                        dr2["UsingGrid"] = 0;
                        dr2["StudyInstanceUID"] = StudyInstanceUID;
                        //SeriesInstanceUID=StudyInstanceUID+Số thứ tự dịch vụ trong lần đăng ký đó
                        dr2["SeriesInstanceUID"] = SeriesInstanceUID;
                        //SOPInstanceUID=SeriesInstanceUID+Số lần chụp của dịch vụ đó
                        dr2["SOPInstanceUID"] = SeriesInstanceUID + ".1";
                        dr2["REG_ID"] = dr1["REG_ID"];
                        dr2["ANATOMY_CODE"] = drAP["ANATOMY_CODE"];
                        dr2["PROJECTION_CODE"] = drAP["PROJECTION_CODE"];
                        //dr2["DISPLAY_NAME"] = arrProc[i].ToString().Split('#')[1];
                        //dr2["STANDARD_NAME"] = Convert.ToInt16(cboPhysician.SelectedValue);
                        dr2["STATUS"] = 0;
                        _RegDetailEntity.Rows.Add(dr2);
                        _RegDetailEntity.AcceptChanges();
                    }
                }
                DataEntity.Tables.Add(PatientEntity);
                DataEntity.Tables.Add(RegEntity);
                DataEntity.Tables.Add(_RegDetailEntity);
            }
            catch
            {
            }
        }

        string GetProcedure(DataTable dtAP)
        {
            try
            {
                string s = "";
                if (dtAP == null) return s;
                foreach (DataRow dr in dtAP.Rows)
                {
                    s += dr["ANATOMY_CODE"].ToString() + "/" + dr["PROJECTION_CODE"].ToString() + ";";
                }
                if (s.Length > 0) s = s.Substring(0, s.Length - 1);
                return s;
            }
            catch
            {
                return "";
            }
        }
        #endregion
        
        private void ServerIp_KeyPress
     (
        object sender,
        System.Windows.Forms.KeyPressEventArgs e
     )
        {
            if (!(Char.IsDigit(e.KeyChar) ||
                     Char.IsControl(e.KeyChar) ||
                     e.KeyChar == '.'))
            {
                e.Handled = true;
            }
        }


        private void Port_KeyPress
        (
           object sender,
           System.Windows.Forms.KeyPressEventArgs e
        )
        {
            if (!(Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }


        private void ParseError(TextBox tb, string fieldName, Exception ex)
        {
            string message = string.Format("Bạn phải nhập giá trị hợp lệ cho '{0}'{1}Error: {2}", fieldName, Environment.NewLine, ex.Message);
            MessageBox.Show(this, message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            tb.Focus();
            DialogResult = DialogResult.None;
        }

        private bool CheckFormat(TextBox tb, string fieldName, bool ipAddress)
        {
            try
            {
                if (ipAddress)
                    System.Net.IPAddress.Parse(tb.Text);
                else
                    Convert.ToInt32(tb.Text);

                return true;
            }
            catch (FormatException ex)
            {
                ParseError(tb, fieldName, ex);
                return false;
            }
            catch (OverflowException ex)
            {
                ParseError(tb, fieldName, ex);
                return false;
            }
        }
        private void cmdSaveWLSettings_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    WLhasjustApplied = true;
            //    server.AETitle = ServerAE.Text;
            //    server.Port = Convert.ToInt32(ServerPort.Text);
            //    server.ServerAddress = ServerIp.Text;
            //    server.Address = IPAddress.Parse(ServerIp.Text);
            //    server.Timeout = Convert.ToInt32(Timeout.Text);
            //    WLAETitle = ClientAE.Text;
            //    Port = Convert.ToInt32(ServerPort.Text);
            //    SaveSettings();
            //    //Kiểm tra xem có Thead nào chạy AutoSearch from WList hay không?
            //    if (lblSearchWorkList.ImageIndex == 1)
            //    {
            //        //StopWL
            //        if (WLIsWorking) FreeWLThread();
            //    }
            //    else//Tự động tìm kiếm WL
            //    {
            //        if (WLIsWorking) FreeWLThread();
            //        Application.DoEvents();
            //        if (!WLIsWorking)//Nếu chưa có thread chạy thì cần khởi động Thread mới tự động tìm kiếm
            //        {
            //            WLThread = new Thread(autoSearch);
            //            if (lblBackGroudThread.ImageIndex == 0) WLThread.IsBackground = true;
            //            WLThread.Start();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Utility.ShowMsg(ex.Message);
            //}
        }
        void SetValueCFind()
        {
            //try
            //{
            //    dcmQuery = new CFindQuery();
            //    string PCode = txtSearchPCode.Text.Trim() == "" ? "NOTHING" : txtSearchPCode.Text.Trim();
            //    string PName = txtSearchPName.Text.Trim() == "" ? "NOTHING" : txtSearchPName.Text.Trim();
            //    Int16 pSex = Convert.ToInt16(cboSearchSex.SelectedIndex);
            //    if (pSex <= 0) pSex = 100;

            //    Int64 RegNumber = txtRegID.Text.Trim() == "" ? -1 : Convert.ToInt64(txtRegID.Text.Trim());
            //    string RegFrom = lblFromDate.ImageIndex == 0 ? dtpFromDate.Text : "1/1/1900";
            //    string RegTo = lblFromDate.ImageIndex == 0 ? dtpToDate.Text : "1/1/1900";

            //    if (PName != "NOTHING") dcmQuery.PatientName = PName;
            //    if (PCode != "NOTHING") dcmQuery.PatientID = PCode;

            //    if (RegFrom != "1/1/1900")
            //        dcmQuery.StudyStartDate = dtpFromDate.Value;
            //    if (RegTo != "1/1/1900")
            //        dcmQuery.StudyEndDate = dtpToDate.Value;
            //    if (RegNumber != -1)
            //        dcmQuery.AccessionNo = txtRegID.Text.Trim();
            //}
            //catch
            //{
            //}


        }

        #endregion
       
        private void SearchWL(bool dummy)
        {
            try
            {
                m_dtWLDataSource.Rows.Clear();
                m_dtWLDataSource_Suspending.Rows.Clear();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter DA = new OleDbDataAdapter(cmd);
                string PCode = txtSearchPCode.Text.Trim() == "" ? "NOTHING" : txtSearchPCode.Text.Trim();
                string PName = txtSearchPName.Text.Trim() == "" ? "NOTHING" : txtSearchPName.Text.Trim();
                Int16 pSex = Convert.ToInt16(cboSearchSex.SelectedIndex);
                if (pSex <= 0) pSex = 100;
                if (dummy) PCode = "ABCDEFGH";
                Int64 RegNumber = txtRegID.Text.Trim() == "" ? -1 : Convert.ToInt64(txtRegID.Text.Trim());
                string RegFrom = lblFromDate.ImageIndex == 0 ? dtpFromDate.Text : "1/1/1900";
                string RegTo = lblFromDate.ImageIndex == 0 ? dtpToDate.Text : "1/1/1900";

                {
                    cmd.Connection = globalVariables.OleDbConnection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getWL";
                    cmd.Parameters.AddWithValue("@Patient_Code", PCode).Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@Patient_Name", PName).Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@regFrom", RegFrom).Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@regTo", RegTo).Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@Sex", pSex).Direction = ParameterDirection.Input;
                    //cmd.Parameters.AddWithValue("@REG_NUMBER", RegNumber).Direction = ParameterDirection.Input;

                }
                DataSet ds = new DataSet();
                DA.Fill(ds);
                if (ds == null || ds.Tables.Count <= 0) return;
                m_dtWLDataSource = ds.Tables[0].Clone();
                m_dtWLDataSource_Suspending = ds.Tables[0].Clone();
                //Tách bảng đăng ký và bảng suspend
                foreach (DataRow dr in ds.Tables[0].Rows)
                    if (dr["REGSTATUS"].ToString() == "0")//IsSuspending
                        m_dtWLDataSource_Suspending.ImportRow(dr);
                    else
                        m_dtWLDataSource.ImportRow(dr);
                Utility.SetDataSourceForDataGridView(grdWorkList, m_dtWLDataSource, false, true, "REGSTATUS in (0,1,3)", "Patient_Code DESC,CREATED_DATE DESC");
                Utility.SetDataSourceForDataGridView(grdWorkListSuspending, m_dtWLDataSource_Suspending, false, true, "REGSTATUS in (0,1,3)", "SUSPENDING_TIME DESC,Patient_Code DESC,CREATED_DATE DESC");
                SetSuspendingInfo();
                ModifyWorkListButtons();
            }
            catch (Exception ex)
            {
                mdlStatic.SetMsg(lblMsg, ex.Message, true);
            }
            finally
            {
                SetSuspendingInfo();
                SetText(lblCaption, "Tổng số: " + grdStudyList.RowCount.ToString() + " Bệnh nhân");
               
            }
        }

        private void cmdAddProc_Click(object sender, EventArgs e)
        {

            AddProc();
        }
        void GetAPParams(DataTable APParams_DataSource, string ACODE, string PCODE, string BCODE, ref decimal _kVp, ref int _mA, ref int _mAs)
        {
            try
            {
                DataRow[] arrDr = APParams_DataSource.Select("ANATOMY_CODE='" + ACODE + "' AND PROJECTION_CODE='" + PCODE + "' AND BODYSIZE_CODE='" + BCODE + "'");
                if (arrDr.Length > 0)
                {

                    _kVp = Convert.ToDecimal(arrDr[0]["KVP"]);
                    _mA = Convert.ToInt32(arrDr[0]["MA"]);
                    _mAs = Convert.ToInt32(arrDr[0]["MAS"]);
                }
                else
                {
                    _kVp = 15M;
                    _mA = 0;
                    _mAs = 1;
                }
            }
            catch
            {
                _kVp = 15M;
                _mA = 0;
                _mAs = 1;
            }
        }
        string CurrStudyInstanceUID = "";
        int MaxSeriesInstanceUID = -1;
        void AddProc()
        {
            try
            {
                frm_Choose_Anotomy_Projection _Choose_Anotomy_Projection = new frm_Choose_Anotomy_Projection(-1);
                if (_Choose_Anotomy_Projection.ShowDialog() == DialogResult.OK)
                {
                    if (m_dtAcquisitionDataSource == null || m_dtAcquisitionDataSource.Columns.Count <= 0 || m_dtAcquisitionDataSource.Rows.Count <= 0)
                    {
                        m_dtAcquisitionDataSource = new RegDetailController().GetAllData(-1).Tables[0];
                    }
                    DataRow dr;
                    if (m_dtAcquisitionDataSource.Rows.Count > 0)
                    {
                        dr = m_dtAcquisitionDataSource.Rows[0];
                        dr["STATUS"] = 0;
                        dr["IMGNAME"] = "";
                        dr["PRINTCOUNT"] = 0;
                        dr["EXPOSURECOUNT"] = 0;
                        dr["HOST"] = "127.0.0.1";
                        dr["UsingGrid"] = 0;
                        dr["IMGDATA"] = DBNull.Value;
                        dr["REG_NUMBER"] = txtRegNumber2.Text.Trim();
                        if (dr["reg_id"].ToString() == "-1") dr["REG_ID"] = currREGID;
                    }
                    else
                    {
                        dr = m_dtAcquisitionDataSource.NewRow();

                        dr["STATUS"] = 0;
                        dr["IMGNAME"] = "";
                        dr["PRINTCOUNT"] = 0;
                        dr["EXPOSURECOUNT"] = 0;
                        dr["HOST"] = "127.0.0.1";
                        dr["UsingGrid"] = 0;
                        dr["IMGDATA"] = DBNull.Value;
                        dr["REG_NUMBER"] = txtRegNumber2.Text.Trim();
                        dr["REG_ID"] = currREGID;


                    }
                    DataTable dtAP = _Choose_Anotomy_Projection.AP_DataSource.Select("CHON=1").CopyToDataTable();
                    DataRow newdr = m_dtAcquisitionDataSource.NewRow();
                    Utility.CopyData(dr, ref newdr);
                    if (dtAP != null)
                    {
                        string SeriesInstanceUID = "";
                        foreach (DataRow drAP in dtAP.Rows)
                        {
                            RegDetailInfor infor = new RegDetailInfor();
                            int NextSeriesInstanceUID = MaxSeriesInstanceUID + 1;
                            SeriesInstanceUID = CurrStudyInstanceUID + "." + NextSeriesInstanceUID.ToString();
                            MaxSeriesInstanceUID = NextSeriesInstanceUID;
                            newdr["DETAIL_ID"] = -1;
                            newdr["StudyInstanceUID"] = CurrStudyInstanceUID;
                            //SeriesInstanceUID=StudyInstanceUID+Số thứ tự dịch vụ trong lần đăng ký đó
                            newdr["SeriesInstanceUID"] = SeriesInstanceUID;
                            //SOPInstanceUID=SeriesInstanceUID+Số lần chụp của dịch vụ đó. 
                            //SOPInstanceUID chỉ thay đổi khi dịch vụ này đang có ảnh và lại được chụp lại
                            newdr["SOPInstanceUID"] = SeriesInstanceUID + ".1";
                            newdr["UsingGrid"] = 0;
                            newdr["REG_ID"] = dr["REG_ID"];
                            newdr["ANATOMY_CODE"] = drAP["ANATOMY_CODE"];
                            newdr["BODYSIZE_CODE"] = _Choose_Anotomy_Projection.BODYSIZE_CODE;

                            newdr["PROJECTION_CODE"] = drAP["PROJECTION_CODE"];
                            newdr["DISPLAY_NAME"] = drAP["ANATOMY_CODE"];
                            newdr["STANDARD_NAME"] = drAP["ANATOMY_CODE"];
                            newdr["DirectionCapture"] = 0;
                            newdr["STATUS"] = 0;
                            newdr["STATUS_NAME"] = "";
                            Utility.MapValueFromEntityIntoObjectInfor(infor, newdr);
                            if (new RegDetailController(infor).Insert() == ActionResult.Success)
                            {
                                newdr["DETAIL_ID"] = infor.DETAIL_ID;
                                decimal _kVp = 0M;
                                int _mA = 0;
                                int _mAs = 0;
                                GetAPParams(_Choose_Anotomy_Projection.APParams_DataSource, infor.ANATOMY_CODE, infor.PROJECTION_CODE, infor.BODYSIZE_CODE, ref _kVp, ref _mA, ref _mAs);
                                //Add new Scheduled Control
                                ScheduledControl _Scheduled = new ScheduledControl(txtImgDir.Text.Trim() + @"\" + txtRegNumber2.Text.Trim() + "_" + infor.DETAIL_ID.ToString(), (int)infor.REG_ID, (int)infor.DETAIL_ID, CurrStudyInstanceUID, SeriesInstanceUID, newdr["SOPInstanceUID"].ToString(), infor.ANATOMY_CODE, infor.PROJECTION_CODE, infor.BODYSIZE_CODE, Utility.sDbnull(drAP["VN_ANATOMY_NAME"], ""), Utility.sDbnull(drAP["EN_ANATOMY_NAME"], ""), Utility.sDbnull(drAP["VN_PROJECTION_NAME"], ""), Utility.sDbnull(drAP["EN_PROJECTION_NAME"], ""), _Choose_Anotomy_Projection.BODYSIZE_NAME, _Choose_Anotomy_Projection.BODYSIZE_NAME, _kVp, _mA, _mAs, Utility.Int32Dbnull(drAP["A_STT"], 0), Utility.Int32Dbnull(drAP["P_STT"], 0), Utility.Int32Dbnull(dr["PRINTCOUNT"], 0), 0);
                                Size originalSize = _Scheduled.Size;
                                _Scheduled._OnClick += new ScheduledControl.OnClick(_ScheduledControl__OnClick);
                                _Scheduled.ContextMenuStrip = ctx;
                                _Scheduled._OnNewScheduleClick += new ScheduledControl.OnNewScheduleClick(_ScheduledControl__OnNewScheduleClick);
                                _Scheduled._OnRejectScheduleClick += new ScheduledControl.OnRejectScheduleClick(_ScheduledControl__OnRejectScheduleClick);
                                _Scheduled._OnDelScheduleClick += new ScheduledControl.OnDelScheduleClick(_ScheduledControl__OnDelScheduleClick);

                                _Scheduled._OnNewScheduleDoubleClick += new ScheduledControl.OnNewScheduleDoubleClick(_ScheduledControl__OnNewScheduleDoubleClick);
                                _Scheduled._OnRejectScheduleDoubleClick += new ScheduledControl.OnRejectScheduleDoubleClick(_ScheduledControl__OnRejectScheduleDoubleClick);
                                _Scheduled._OnDelScheduleDoubleClick += new ScheduledControl.OnDelScheduleDoubleClick(_ScheduledControl__OnDelScheduleDoubleClick);



                                _Scheduled._OnKeyDown += new ScheduledControl.OnKeyDown(_ScheduledControl__OnKeyDown);
                                pnlScheduled.Controls.Add(_Scheduled);
                                _Scheduled.Size = new Size(124, 71);

                                if (_Scheduled != null)
                                {
                                    //Nếu chưa có kết quả thì tự động chọn và scroll tới
                                    pnlScheduled.ScrollControlIntoView(_Scheduled);
                                    _Scheduled._AnatomyObject.PerformClick();
                                }
                            }

                            m_dtAcquisitionDataSource.Rows.Add(newdr);
                            m_dtAcquisitionDataSource.AcceptChanges();
                            newdr = m_dtAcquisitionDataSource.NewRow();
                        }
                    }


                    if (!AcquisitionFromWL)
                    {
                        //string[] Img = Utility.sDbnull(currentStudyRow["Img"]).Split('/');
                        //currentStudyRow["Img"] = Img[0] + "/" + (Convert.ToInt64(Img[1]) + _newForm.arrProc.Count).ToString();
                        //currentStudyRow["Img1"] = currentStudyRow["Img"];
                    }
                    //Update Procedure List to DB and Datasource
                    string ProcedureList = GetProcedureList();
                    new RegDetailController().UpdateProcedureList(Convert.ToInt64(dr["REG_ID"]), ProcedureList);
                    //Update Dataset
                    DataRow[] drWL = m_dtWLDataSource.Select("Reg_ID=" + Convert.ToInt64(dr["REG_ID"]));
                    DataRow[] drST = m_dtStudyListDataSource.Select("Reg_ID=" + Convert.ToInt64(dr["REG_ID"]));
                    if (drWL.Length > 0)
                        drWL[0]["ProcedureList"] = ProcedureList;
                    if (drST.Length > 0)
                        drST[0]["ProcedureList"] = ProcedureList;
                    m_dtWLDataSource.AcceptChanges();
                    m_dtStudyListDataSource.AcceptChanges();
                }
            }
            catch
            {
            }
            finally
            {
                ScheduledControl _reObj = GetSelectedScheduled_NOTDONE();
                if (_reObj != null)
                {
                    //Nếu chưa có kết quả thì tự động
                    _reObj._AnatomyObject.PerformClick();
                }
            }

        }
        string GetNextSeriesInstanceUID(string StudyInstanceUID)
        {
            try
            {
                int lastIdx = Convert.ToInt32(StudyInstanceUID.Substring(StudyInstanceUID.LastIndexOf(".") + 1));
                lastIdx++;
                return StudyInstanceUID + "." + lastIdx.ToString();
            }
            catch
            {
                return StudyInstanceUID + ".1";
            }

        }
        int _newDetailID = -1;
        void ShortCut2AddProc(string ACODE, string PCODE, bool AutoSelectNotDone, ref int _detailId)
        {
            try
            {
                using (frm_Choose_Anotomy_Projection _Choose_Anotomy_Projection = new frm_Choose_Anotomy_Projection(-1))
                {
                    _Choose_Anotomy_Projection.InitComponents();
                    bool _Success = false;
                    _Choose_Anotomy_Projection.AutoSelectAnatomy(ACODE, PCODE, ref _Success);
                    if (_Success)
                    {
                        if (m_dtAcquisitionDataSource == null || m_dtAcquisitionDataSource.Columns.Count <= 0 || m_dtAcquisitionDataSource.Rows.Count <= 0)
                        {
                            m_dtAcquisitionDataSource = new RegDetailController().GetAllData(-1).Tables[0];
                        }
                        DataRow dr;
                        if (m_dtAcquisitionDataSource.Rows.Count > 0)
                        {
                            dr = m_dtAcquisitionDataSource.Rows[0];
                            if (dr["reg_id"].ToString() == "-1") dr["REG_ID"] = currREGID;
                        }
                        else
                        {
                            dr = m_dtAcquisitionDataSource.NewRow();
                            dr["UsingGrid"] = 0;
                            dr["STATUS"] = 0;
                            dr["IMGNAME"] = "";
                            dr["PRINTCOUNT"] = 0;
                            dr["EXPOSURECOUNT"] = 0;
                            dr["HOST"] = "127.0.0.1";

                            dr["IMGDATA"] = DBNull.Value;
                            dr["REG_NUMBER"] = txtRegNumber2.Text.Trim();
                            dr["REG_ID"] = currREGID;


                        }
                        DataTable dtAP = _Choose_Anotomy_Projection.AP_DataSource.Select("CHON=1").CopyToDataTable();
                        DataRow newdr = m_dtAcquisitionDataSource.NewRow();
                        Utility.CopyData(dr, ref newdr);
                        if (dtAP != null)
                        {
                            string SeriesInstanceUID = "";
                            foreach (DataRow drAP in dtAP.Rows)
                            {
                                RegDetailInfor infor = new RegDetailInfor();

                                int NextSeriesInstanceUID = MaxSeriesInstanceUID + 1;
                                SeriesInstanceUID = CurrStudyInstanceUID + "." + NextSeriesInstanceUID.ToString();
                                MaxSeriesInstanceUID = NextSeriesInstanceUID;
                                newdr["DETAIL_ID"] = -1;
                                newdr["StudyInstanceUID"] = CurrStudyInstanceUID;
                                //SeriesInstanceUID=StudyInstanceUID+Số thứ tự dịch vụ trong lần đăng ký đó
                                newdr["SeriesInstanceUID"] = SeriesInstanceUID;
                                //SOPInstanceUID=SeriesInstanceUID+Số lần chụp của dịch vụ đó. 
                                //SOPInstanceUID chỉ thay đổi khi dịch vụ này đang có ảnh và lại được chụp lại
                                newdr["SOPInstanceUID"] = SeriesInstanceUID + ".1";
                                newdr["REG_ID"] = dr["REG_ID"];
                                newdr["ANATOMY_CODE"] = drAP["ANATOMY_CODE"];
                                newdr["BODYSIZE_CODE"] = _Choose_Anotomy_Projection.BODYSIZE_CODE;
                                newdr["UsingGrid"] = 0;
                                newdr["PROJECTION_CODE"] = drAP["PROJECTION_CODE"];
                                newdr["DISPLAY_NAME"] = drAP["ANATOMY_CODE"];
                                newdr["STANDARD_NAME"] = drAP["ANATOMY_CODE"];
                                newdr["DirectionCapture"] = 0;
                                newdr["STATUS"] = 0;
                                newdr["STATUS_NAME"] = "";
                                Utility.MapValueFromEntityIntoObjectInfor(infor, newdr);
                                if (new RegDetailController(infor).Insert() == ActionResult.Success)
                                {
                                    newdr["DETAIL_ID"] = infor.DETAIL_ID;
                                    _detailId = Convert.ToInt32(infor.DETAIL_ID);
                                    decimal _kVp = 0M;
                                    int _mA = 0;
                                    int _mAs = 0;
                                    GetAPParams(_Choose_Anotomy_Projection.APParams_DataSource, infor.ANATOMY_CODE, infor.PROJECTION_CODE, infor.BODYSIZE_CODE, ref _kVp, ref _mA, ref _mAs);
                                    //Add new Scheduled Control
                                    ScheduledControl _Scheduled = new ScheduledControl(txtImgDir.Text.Trim() + @"\" + txtRegNumber2.Text.Trim() + "_" + infor.DETAIL_ID.ToString(), (int)infor.REG_ID, (int)infor.DETAIL_ID, CurrStudyInstanceUID, SeriesInstanceUID, newdr["SOPInstanceUID"].ToString(), infor.ANATOMY_CODE, infor.PROJECTION_CODE, infor.BODYSIZE_CODE, Utility.sDbnull(drAP["VN_ANATOMY_NAME"], ""), Utility.sDbnull(drAP["EN_ANATOMY_NAME"], ""), Utility.sDbnull(drAP["VN_PROJECTION_NAME"], ""), Utility.sDbnull(drAP["EN_PROJECTION_NAME"], ""), _Choose_Anotomy_Projection.BODYSIZE_NAME, _Choose_Anotomy_Projection.BODYSIZE_NAME, _kVp, _mA, _mAs, Utility.Int32Dbnull(drAP["A_STT"], 0), Utility.Int32Dbnull(drAP["P_STT"], 0), 0, 0);
                                    Size originalSize = _Scheduled.Size;
                                    _Scheduled._OnClick += new ScheduledControl.OnClick(_ScheduledControl__OnClick);
                                    _Scheduled.ContextMenuStrip = ctx;
                                    _Scheduled._OnNewScheduleClick += new ScheduledControl.OnNewScheduleClick(_ScheduledControl__OnNewScheduleClick);
                                    _Scheduled._OnRejectScheduleClick += new ScheduledControl.OnRejectScheduleClick(_ScheduledControl__OnRejectScheduleClick);
                                    _Scheduled._OnDelScheduleClick += new ScheduledControl.OnDelScheduleClick(_ScheduledControl__OnDelScheduleClick);

                                    _Scheduled._OnNewScheduleDoubleClick += new ScheduledControl.OnNewScheduleDoubleClick(_ScheduledControl__OnNewScheduleDoubleClick);
                                    _Scheduled._OnRejectScheduleDoubleClick += new ScheduledControl.OnRejectScheduleDoubleClick(_ScheduledControl__OnRejectScheduleDoubleClick);
                                    _Scheduled._OnDelScheduleDoubleClick += new ScheduledControl.OnDelScheduleDoubleClick(_ScheduledControl__OnDelScheduleDoubleClick);




                                    _Scheduled._OnKeyDown += new ScheduledControl.OnKeyDown(_ScheduledControl__OnKeyDown);
                                    AutoAddNewSchedule(_Scheduled);
                                    //pnlScheduled.Controls.Add(_Scheduled);
                                    _Scheduled.Size = new Size(124, 71);

                                    if (_Scheduled != null)
                                    {
                                        //Nếu chưa có kết quả thì tự động chọn và scroll tới
                                        AutoScroll2NewSchedule(_Scheduled);
                                        //pnlScheduled.ScrollControlIntoView(_Scheduled);
                                        _Scheduled._AnatomyObject.PerformClick();
                                    }
                                }

                                m_dtAcquisitionDataSource.Rows.Add(newdr);
                                m_dtAcquisitionDataSource.AcceptChanges();
                                newdr = m_dtAcquisitionDataSource.NewRow();
                            }
                        }


                        if (!AcquisitionFromWL)
                        {
                            //string[] Img = Utility.sDbnull(currentStudyRow["Img"]).Split('/');
                            //currentStudyRow["Img"] = Img[0] + "/" + (Convert.ToInt64(Img[1]) + _newForm.arrProc.Count).ToString();
                            //currentStudyRow["Img1"] = currentStudyRow["Img"];
                        }
                        //Update Procedure List to DB and Datasource
                        string ProcedureList = GetProcedureList();
                        new RegDetailController().UpdateProcedureList(Convert.ToInt64(dr["REG_ID"]), ProcedureList);
                        //Update Dataset
                        DataRow[] drWL = m_dtWLDataSource.Select("Reg_ID=" + Convert.ToInt64(dr["REG_ID"]));
                        DataRow[] drST = m_dtStudyListDataSource.Select("Reg_ID=" + Convert.ToInt64(dr["REG_ID"]));
                        if (drWL.Length > 0)
                            drWL[0]["ProcedureList"] = ProcedureList;
                        if (drST.Length > 0)
                            drST[0]["ProcedureList"] = ProcedureList;
                        m_dtWLDataSource.AcceptChanges();
                        m_dtStudyListDataSource.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogAction.AddLog2List(lstFPD560, "Action: Lỗi khi tạo DV mới-->" + ex.Message);
            }
            finally
            {
                if (AutoSelectNotDone)
                {
                    ScheduledControl _reObj = GetSelectedScheduled_NOTDONE();
                    if (_reObj != null)
                    {
                        //Nếu chưa có kết quả thì tự động
                        _reObj._AnatomyObject.PerformClick();
                    }
                }
            }

        }
        private string GetProcedureList()
        {
            string s = "";
            //ProcedureController _ProcedureController = new ProcedureController();
            foreach (DataRow dr in m_dtAcquisitionDataSource.Rows)
            {
                s += dr["ANATOMY_CODE"].ToString() + "/" + dr["PROJECTION_CODE"].ToString() + ";";
            }
            if (s.Length > 0) s = s.Substring(0, s.Length - 1);
            return s;
        }

        private void cmdReject_Click(object sender, EventArgs e)
        {
            RejectImage();
        }
        void DeletefileAndClearImg(ScheduledControl _reObj)
        {
            try
            {
                string ImgDir = @"C:\";
                if (txtImgDir.Text.Trim() == "" || !Directory.Exists(txtImgDir.Text.Trim()))
                {
                   
                    return;
                }
                else
                {
                    if (!Directory.Exists(txtImgDir.Text + @"\" + SubDirLv1())) Directory.CreateDirectory(txtImgDir.Text + @"\" + SubDirLv1());
                    if (!Directory.Exists(txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient())) Directory.CreateDirectory(txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient());
                    ImgDir = txtImgDir.Text + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient();
                }

                string fileName = _reObj.DcmfileName;// GetFileName(txtImgDir.Text.Trim(), ImgDir, FileName, _reObj);
                string rawfileName = fileName.ToUpper().Replace(".DCM", ".RAW");
                string PNGfileName = fileName.ToUpper().Replace(".DCM", "") + "_THUMBNAIL.PNG";
                string path = Path.GetDirectoryName(fileName);
                string rejectFolder = path + @"\REJECTs";
                string fileDestination = rejectFolder + @"\" + Path.GetFileName(fileName);
                string rawfileDestination = rejectFolder + @"\" + Path.GetFileName(rawfileName);
                string PNGfileDestination = rejectFolder + @"\" + Path.GetFileName(PNGfileName);
                if (chkSaveReject.ImageIndex == 1)//Save Reject Imgs
                    if (!Directory.Exists(rejectFolder))
                        Directory.CreateDirectory(rejectFolder);
                
                if (File.Exists(fileDestination))
                    File.Delete(fileDestination);
                if (chkSaveReject.ImageIndex == 0)
                    Try2DelFile(fileName);
                else
                    File.Move(fileName, fileDestination);

                if (File.Exists(rawfileDestination))
                    File.Delete(rawfileDestination);
                if (chkSaveReject.ImageIndex == 0)
                    Try2DelFile(rawfileName);
                else
                    File.Move(rawfileName, rawfileDestination);
                if (File.Exists(PNGfileDestination))
                    File.Delete(PNGfileDestination);
                if (chkSaveReject.ImageIndex == 0)
                    Try2DelFile(PNGfileName);
                else
                    File.Move(PNGfileName, PNGfileDestination);
                //UpdateThumbnailImgOnScheduled(PNGfileName, 0);
            }
            catch
            {
            }
        }
        void Try2DelFile(string file)
        {
            try
            {
                if (File.Exists(file)) File.Delete(file);
            }
            catch
            {
            }
        }
        void UpdateScheduledStatus(ScheduledControl _Obj, int Status)
        {
            try
            {
                foreach (Control ctr in pnlScheduled.Controls)
                {
                    ScheduledControl _reObj = ctr as ScheduledControl;
                    if (_reObj.DETAIL_ID == _Obj.DETAIL_ID)
                    {

                        _reObj.ResetStatus(Status);
                        m_objCurrScheduledControl = _reObj;
                        return;
                    }
                }

            }
            catch
            {

            }
        }
        void UpdateScheduledStatusAndHardwareParames(ScheduledControl _Obj, int Status)
        {
            try
            {
                foreach (Control ctr in pnlScheduled.Controls)
                {
                    ScheduledControl _reObj = ctr as ScheduledControl;
                    if (_reObj.DETAIL_ID == _Obj.DETAIL_ID)
                    {
                        _reObj.UpdateHardwareParams(lblKvpVal.Value, 0, lblmAsVal.Value);
                        _reObj.ResetStatus(Status);
                        m_objCurrScheduledControl = _reObj;
                        return;
                    }
                }

            }
            catch
            {

            }
        }
        void RejectImage()
        {
            try
            {
                if (new frm_LargeMsgBox(MultiLanguage.GetText(globalVariables.DisplayLanguage, "XÁC NHẬN CHỤP LẠI", "Reject confirm"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Bạn có chắc chắn muốn thực hiện chụp lại cho dịch vụ đang chọn hay không?", "Do you want to reject this image?"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "ĐỒNG Ý", "Yes"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "KHÔNG ĐỒNG Ý", "No")).ShowDialog() != DialogResult.OK) return;

                mdlStatic.isDisplayImg = true;
                ScheduledControl _reObj = GetSelectedScheduled();
                if (new RegDetailController().RejectImage(Convert.ToInt64(_reObj.DETAIL_ID)) == ActionResult.Success)
                {
                    UpdateScheduledStatus(_reObj, 0);
                    UpdateAcqStatus(_reObj.DETAIL_ID, 0);
                    DeletefileAndClearImg(_reObj);
                    _reObj.Invalidate();
                    cmdReady.Tag = "R";
                    cmdReady.Enabled = _reObj != null && _reObj.Status == 0;
                    mdlStatic.isDisplayImg = false;
                    mdlStatic.SetMsg(lblAcqMsg, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đã hủy kết quả chụp ảnh của dịch vụ đang chọn!", "Rejected"), false);
                }
            }
            catch
            {
            }
            finally
            {

                isLoadding = false;
                mdlStatic.isDisplayImg = false;
                ModifyAcqButton();
            }
        }
        void RejectImage(ScheduledControl _reObj)
        {
            try
            {
                if (new frm_LargeMsgBox(MultiLanguage.GetText(globalVariables.DisplayLanguage, "XÁC NHẬN CHỤP LẠI", "Reject confirm"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Bạn có chắc chắn muốn thực hiện chụp lại cho dịch vụ đang chọn hay không?", "Do you want to reject this image?"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "ĐỒNG Ý", "Yes"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "KHÔNG ĐỒNG Ý", "No")).ShowDialog() != DialogResult.OK) return;

                mdlStatic.isDisplayImg = true;
                if (new RegDetailController().RejectImage(Convert.ToInt64(_reObj.DETAIL_ID)) == ActionResult.Success)
                {
                    UpdateScheduledStatus(_reObj, 0);
                    UpdateAcqStatus(_reObj.DETAIL_ID, 0);
                    DeletefileAndClearImg(_reObj);
                    _reObj.Invalidate();
                    cmdReady.Tag = "R";
                    cmdReady.Enabled = _reObj != null && _reObj.Status == 0;
                    mdlStatic.isDisplayImg = false;
                    mdlStatic.SetMsg(lblAcqMsg, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đã hủy kết quả chụp ảnh của dịch vụ đang chọn!", "Rejected"), false);
                }
            }
            catch
            {
            }
            finally
            {

                isLoadding = false;
                mdlStatic.isDisplayImg = false;
                ModifyAcqButton();
            }
        }
        private void cmdSuspend_Click(object sender, EventArgs e)
        {

            SuspendCurrentPatient();
        }
        void SuspendCurrentPatient()
        {
            try
            {
                //Tự động Suspending Bệnh nhân đang được chọn ở mục Acquisition

                if (pnlScheduled.Controls.Count > 0)
                {
                    long RegID = Convert.ToInt64(GetAnyScheduled().REG_ID);
                    DataRow[] arrDr = m_dtWLDataSource.Select("REG_ID=" + RegID);
                    if (arrDr.GetLength(0) > 0)
                    {
                        MoveRow(arrDr[0], false);
                    }
                    arrDr = m_dtStudyListDataSource.Select("REG_ID=" + RegID);
                    if (arrDr.GetLength(0) > 0)
                    {
                        arrDr[0]["REGSTATUS"] = 0;
                        new RegController().UpdateStatus(RegID, 0, DateTime.Now);
                    }
                    m_dtStudyListDataSource.AcceptChanges();
                    FileName = "";
                    mdlStatic.isDisplayImg = false;
                    //DeleteCurrentImg(_DcmMedicalVwr, _DROC_Ribbon_RibbonCellIndex);
                    txtID2.Text = "";
                    txtName2.Text = "";
                    txtRegNumber2.Text = "";
                    txtSex.Text = "";
                    RegDate = DateTime.Now;
                    txtAge.Text = "";
                    pnlScheduled.Controls.Clear();
                    _Click(cmdWL, new EventArgs());
                    cmdWL.PerformClick();
                    ModifyAcqButton();
                }
            }

            catch
            {
            }
            finally
            {
                SetSuspendingInfo();
                SetText(lblCaption, "Tổng số: " + grdStudyList.RowCount.ToString() + " Bệnh nhân");
            }
        }
        private void AutoUpdateResultAfterCapturingPictureFromModality(ScheduledControl _selectedSchedule)
        {
            try
            {
                mdlStatic.isDisplayImg = true;
                if (_selectedSchedule != null)
                {
                    if (new RegDetailController().UpdateStatusAndImg(Convert.ToInt64(_selectedSchedule.DETAIL_ID), 1, FileName, chkDynamicGrid.ImageIndex == 0 ? 1 : 0, lblKvpVal.Value, lblmAsVal.Value) == ActionResult.Success)
                    {
                        UpdateScheduledStatusAndHardwareParames(_selectedSchedule, 1);
                        UpdateAcqStatus(_selectedSchedule.DETAIL_ID, 1);
                        ModifyAcqButton(_selectedSchedule);


                    }
                }
            }
            catch
            {
            }
            finally
            {
                SetSuspendingInfo();
                SetText(lblCaption, "Tổng số: " + grdStudyList.RowCount.ToString() + " Bệnh nhân");
            }
        }
        private void AutoUpdateResultAfterCapturingPictureFromModality()
        {
            try
            {
                ScheduledControl _selectedSchedule = GetSelectedScheduled();
                mdlStatic.isDisplayImg = true;
                if (_selectedSchedule != null)
                {
                    if (new RegDetailController().UpdateStatusAndImg(Convert.ToInt64(_selectedSchedule.DETAIL_ID), 1, FileName, chkDynamicGrid.ImageIndex == 0 ? 1 : 0) == ActionResult.Success)
                    {
                        UpdateScheduledStatus(_selectedSchedule, 1);
                        UpdateAcqStatus(_selectedSchedule.DETAIL_ID, 1);
                        ModifyAcqButton(_selectedSchedule);


                    }
                }
            }
            catch
            {
            }
            finally
            {
                SetSuspendingInfo();
                SetText(lblCaption, "Tổng số: " + grdStudyList.RowCount.ToString() + " Bệnh nhân");
            }
        }

        private void cmdSaveResult_Click(object sender, EventArgs e)
        {
            AutoUpdateResultAfterCapturingPictureFromModality();
        }
        void UpdateAcqStatus(int Detail_ID, int Status)
        {
            try
            {
                DataRow[] arrDr = m_dtAcquisitionDataSource.Select("Detail_ID=" + Detail_ID.ToString());
                if (arrDr.Length > 0) arrDr[0]["Status"] = Status;
                m_dtAcquisitionDataSource.AcceptChanges();
            }
            catch
            {
            }
        }
        
        int m_intThumnailIsSaving_Detail_ID = -1;
      
       
       

    


        
        Image GUIImg = null;
        delegate void StartSavingThumbnail();
       
       
        void UpdateThumbnailImgOnScheduled(string fileName, int ID, int newStatus)
        {
            try
            {
                foreach (Control ctr in pnlScheduled.Controls)
                {

                    ScheduledControl _reObj = ctr as ScheduledControl;
                    if (_reObj.DETAIL_ID == ID)
                    {
                        _reObj.fileName = fileName;
                        _reObj.Status = newStatus;
                        _reObj.ResetStatus(newStatus);
                        _reObj.Invalidate();
                        break;
                    }
                }
            }
            catch
            {
            }
        }
        void UpdateThumbnailImgOnScheduled(string fileName, int newStatus)
        {
            try
            {
                foreach (Control ctr in pnlScheduled.Controls)
                {

                    ScheduledControl _reObj = ctr as ScheduledControl;
                    if (_reObj.DETAIL_ID == m_intThumnailIsSaving_Detail_ID)
                    {
                        //_reObj.UpdateNewImage();
                        _reObj.fileName = fileName;
                        _reObj.Status = newStatus;
                        _reObj.ResetStatus(newStatus);
                        _reObj.Invalidate();
                        break;
                    }
                }
            }
            catch
            {
            }
        }
        void UpdateThumbnailImgOnScheduled(ScheduledControl _SelectedObj, string fileName, int newStatus)
        {
            try
            {
                foreach (Control ctr in pnlScheduled.Controls)
                {

                    ScheduledControl _reObj = ctr as ScheduledControl;
                    if (_reObj.DETAIL_ID == _SelectedObj.DETAIL_ID)
                    {
                        //_reObj.UpdateNewImage();
                        _reObj.fileName = fileName;
                        _reObj.Status = newStatus;
                        _reObj.ResetStatus(newStatus);
                        _reObj.Invalidate();
                        break;
                    }
                }
            }
            catch
            {
            }
        }


      
        int WW = 0;
        int WC = 0;

     

    

    
    
        private void cmdEndExam_Click(object sender, EventArgs e)
        {
            EndExam();
        }
        void EndExam()
        {
            try
            {
                if (pnlScheduled.Controls.Count <= 0) return;
                ScheduledControl _reObj = GetAnyScheduled();
                if (pnlScheduled.Controls.Count <= 0 && _reObj == null) return;
                long RegID = Convert.ToInt64(_reObj.REG_ID);
                if (new RegController().UpdateStatus(RegID, 2) == ActionResult.Success)
                {
                    DataRow[] arrDr = m_dtWLDataSource.Select("REG_ID=" + RegID + " AND REGSTATUS<=3");
                    if (arrDr.GetLength(0) > 0)
                    {
                        arrDr[0]["REGSTATUS"] = 2;
                        if (m_dtStudyListDataSource.Select("REG_ID=" + RegID + " AND REGSTATUS<=3").GetLength(0) <= 0)
                        {
                            m_dtStudyListDataSource.ImportRow(arrDr[0]);
                            m_dtStudyListDataSource.AcceptChanges();
                        }
                    }
                    m_dtWLDataSource.AcceptChanges();
                    ChangeForeColorOfGrid();
                    //Clear Acquisition Datasource
                    m_dtAcquisitionDataSource.Rows.Clear();

                    FileName = "";
                    mdlStatic.isDisplayImg = false;
                    KillProcess("DROCDisplay.exe");
                    ModifyAcqButton();
                    txtID2.Text = "";
                    txtName2.Text = "";
                    txtRegNumber2.Text = "";
                    txtSex.Text = "";
                    RegDate = DateTime.Now;
                    txtAge.Text = "";
                    _Click(cmdWL, new EventArgs());
                    cmdWL.PerformClick();
                }
            }
            catch
            {
            }
            finally
            {
                if (chkAutoSend.ImageIndex == 0)
                {
                    
                }
                SetSuspendingInfo();
                SetText(lblCaption, "Tổng số: " + grdStudyList.RowCount.ToString() + " Bệnh nhân");
            }
        }

        private void tbrDicomViewer1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        



        private void grdStudyList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
      
      
       
        delegate void EnableControl(Control ctrl, bool isEnabled, string text);
        void _EnableControl(Control ctrl, bool isEnabled, string text)
        {
            if (this.InvokeRequired)
            {
                Invoke(new EnableControl(_EnableControl), new object[] { ctrl, isEnabled, text });
            }
            else
            {
                ctrl.Enabled = isEnabled;
                if (text.Trim() != "") ctrl.Text = text;
                Application.DoEvents();
            }
        }

        bool canbeContinued = true;
     
      
        /// <summary>
        /// Hàm này dùng khi sử dụng ClearCanvas để lấy thông tin danh sách các đường dẫn file ảnh gửi tới PACS Server
        /// </summary>
        /// <param name="RegID"></param>
        /// <returns></returns>
        private ArrayList GetArrImg(long RegID)
        {
            //try
            //{
            //    ArrayList _arrImg = new ArrayList();
            //    DetailDataSource = new RegDetailController().GetAllData(RegID).Tables[0];
            //    if (DetailDataSource == null || DetailDataSource.Columns.Count <= 0 || DetailDataSource.Rows.Count <= 0) return null;
            //    foreach (DataRow dr in DetailDataSource.Rows)
            //        if (dr["STATUS"].ToString() == "1" && Utility.sDbnull(dr["IMGNAME"], "").Trim() != "")
            //        {
            //            ScheduledControl _temp = new ScheduledControl(txtImgDir.Text.Trim() + @"\" + SubDirLv1(Utility.sDbnull(dr["REG_NUMber"], "")) + @"\" + SubDirPatient + @"\" + dr["Reg_NUMBER"].ToString().ToString() + "_" + Utility.Int32Dbnull(dr["DETAIL_ID"]).ToString() + "_" + Utility.sDbnull(dr["ANATOMY_CODE"], "") + "_" + Utility.sDbnull(dr["PROJECTION_CODE"], ""), Utility.Int32Dbnull(dr["REG_ID"], ""), Utility.Int32Dbnull(dr["DETAIL_ID"], ""), Utility.sDbnull(dr["SeriesInstanceUID"], ""), Utility.sDbnull(dr["ANATOMY_CODE"], ""), Utility.sDbnull(dr["PROJECTION_CODE"], ""), Utility.sDbnull(dr["BODYSIZE_CODE"], ""), Utility.sDbnull(dr["VN_ANATOMY_NAME"], ""), Utility.sDbnull(dr["EN_ANATOMY_NAME"], ""), Utility.sDbnull(dr["VN_PROJECTION_NAME"], ""), Utility.sDbnull(dr["EN_PROJECTION_NAME"], ""), Utility.sDbnull(dr["VN_BODYSIZE_NAME"], ""), Utility.sDbnull(dr["EN_BODYSIZE_NAME"], ""), Utility.DecimaltoDbnull(dr["kVp"], 0.0), Utility.Int32Dbnull(dr["mA"], 0), Utility.DecimaltoDbnull(dr["mAs"], 0), Utility.Int32Dbnull(dr["A_STT"], 0), Utility.Int32Dbnull(dr["P_STT"], 0), Utility.Int32Dbnull(dr["PRINTCOUNT"], 0), Utility.Int32Dbnull(dr["Status"], 0));
            //            string _ImgDir = txtImgDir.Text + @"\" + SubDirLv1(Utility.sDbnull(dr["REG_NUMber"], "")) + @"\" + SubDirPatient;
            //         _arrImg.Add(GetFileName(txtImgDir.Text.Trim(),_ImgDir, dr["IMGNAME"].ToString(), _temp));
            //        }
            //    return _arrImg;

            //}
            //catch
            //{
            return null;
            //}
        }

      

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frm_ProcedureList newForm = new frm_ProcedureList();
            newForm.Show();
        }

        private void lnkDicomPrintSetting_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowFilmPrintConfig();

        }
        void ShowFilmPrintConfig()
        {

        }

        private void cmdDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fld = new FolderBrowserDialog();
            if (fld.ShowDialog() == DialogResult.OK)
            {
                txtDir.Text = fld.SelectedPath;
                RegConfiguration.SaveSettings("hrk", "VBIT_DRTech_FOLDER", "FILE", txtDir.Text);
            }
        }

        private void cmdDir2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fld = new FolderBrowserDialog();
            if (fld.ShowDialog() == DialogResult.OK)
            {
                txtImgDir.Text = fld.SelectedPath;
                RegConfiguration.SaveSettings("hrk", "VBIT_DRTech_FOLDER", "IMG", txtImgDir.Text);
            }
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frm_ChangePwd ChangePWD = new frm_ChangePwd();

            ChangePWD.Show();
        }

        private void DROC_Ribbon_Load(object sender, EventArgs e)
        {
            try
            {
                //string pName ="DAO VAN CUONG 32T M";
                //string tempName = "";
                //string tempAge = "";
                //string tempSex = "";
                //try2SplitPName_Age_Sex(pName, ref tempName, ref tempAge, ref tempSex);

                _Click(cmdWL, e);
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ":  _Click(cmdWL, e)  Completely");
                cmdWL.PerformClick();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ":  cmdWL.PerformClick()  Completely");
                InitLogin();
            }
            catch (Exception ex)
            {
                AppLogger.LogAction.LogActions("==>DROC_Ribbon_Load().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex.ToString());
            }
            finally
            {

                this.textBoxDescription.Text = "Built at : " + getBuiltDate() + System.Environment.NewLine + "Văn phòng Hà Nội" + System.Environment.NewLine + "Tầng 10 nhà B7, Ký túc xác Thăng Long, Đường Cốm Vòng, Quận Cầu giấy, Hà Nội" + System.Environment.NewLine + "ĐT: 84 4 2696620 / 21 Fax: 84 4 2696623 " + System.Environment.NewLine + "E-mail: viet-ba@hn.vnn.vn " + System.Environment.NewLine + "Văn phòng TP. Hồ Chí Minh " + System.Environment.NewLine + "58/16 Đường Thành Thái, P.12, Q.10 " + System.Environment.NewLine + "ĐT/Fax: (848) 863 2915 " + System.Environment.NewLine + "E-mail: viet-ba@hcm.vnn.vn";
               // this.Text = "DROC-Digital Radiology Operation Console. Built at : " + getBuiltDate();
                this.textBoxDescription.Text += System.Environment.NewLine + "Author:" + System.Environment.NewLine + "   Đào Văn Cường(091 5150148).";

            }
        }
        string getBuiltDate()
        {
            try
            {
                return new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");
            }
            catch
            {
                return "";
            }
        }
       
      





        private void InitDROC()
        {
            try
            {

                AutoSetFPDModebyLoginInfo();
                txtDir.Text = Utility.sDbnull(RegConfiguration.GetSettings("hrk", "VBIT_DRTech_FOLDER", "FILE"));
                txtImgDir.Text = Utility.sDbnull(RegConfiguration.GetSettings("hrk", "VBIT_DRTech_FOLDER", "IMG"));
                Try2CreateLogFolder();
                //SetMsg(lblJob, "", false);
                lblFromDate.ImageIndex = 0;
                lblFromDateST.ImageIndex = 0;


                mnu0.Text = globalVariables.DisplayLanguage == "VN" ? "Tự thêm mới dịch vụ đang chọn" : "Add view";
                mnu1.Text = globalVariables.DisplayLanguage == "VN" ? "Chọn dịch vụ cần chụp khác" : "Select other views";
                mnu2.Text = globalVariables.DisplayLanguage == "VN" ? "Xóa bớt dịch vụ" : "Delete view";

                mnuselectImgPath.Text = globalVariables.DisplayLanguage == "VN" ? "Chọn lại ảnh cho dịch vụ này" : "Select dicom file for this view";
                mnuChangeViewPos.Text = globalVariables.DisplayLanguage == "VN" ? "Cập nhật lại vị trí chuẩn cho ảnh này" : "Update View-body part for this view";

                mnu4.Text = globalVariables.DisplayLanguage == "VN" ? "Thuộc tính" : "Properties";
                mnu5.Text = globalVariables.DisplayLanguage == "VN" ? "Xử lý lại ảnh gốc" : "Reprocess from raw image";
                ctx.Items.AddRange(new ToolStripItem[] { mnu0, mnu1, mnu2, mnu3, mnuselectImgPath, mnuChangeViewPos, mnu01, mnu5, mnu4 });
                //pnlScheduled.ContextMenuStrip = ctx;
                mnu0.Click += new EventHandler(mnu0_Click);
                mnu1.Click += new EventHandler(mnu1_Click);
                mnu2.Click += new EventHandler(mnu2_Click);
                mnuselectImgPath.Click += new EventHandler(mnuselectImgPath_Click);
                mnuChangeViewPos.Click += new EventHandler(mnuChangeViewPos_Click);
                //mnu4.Enabled = false;
                mnu4.Click += new EventHandler(mnu4_Click);
                mnu5.Click += new EventHandler(mnu5_Click);
                
                isGCOM_VN = lblGCOM_VN.ImageIndex == 0;
                tmrExSignal.Enabled = lblGCOM.ImageIndex == 0;
                //cmdSaveResult.Size = chkGCOM.Checked ? new Size(205, 80) : new Size(205, 164);
                DataSet dsXML = new DataSet();
                dsXML.ReadXml(Application.StartupPath + @"\Config.XML");
                string Host = dsXML.Tables[0].Rows[0]["SERVERADDRESS"].ToString();
                string DBName = dsXML.Tables[0].Rows[0]["DATABASE_ID"].ToString();
                string UID = dsXML.Tables[0].Rows[0]["USERNAME"].ToString();
                string Pwd = dsXML.Tables[0].Rows[0]["PASSWORD"].ToString();
                //ShowScreen();

                LoadCOMConfig();
                if (lblGCOM.ImageIndex == 0 && chkAutoStart.Checked) cmdOpenCom_Click(cmdOpenCom, new EventArgs());
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": LoadCOMConfig  Completely");
                AutoSelectGCOM();
                CreatekVpDic();
                CreateAMECkVpDic();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": CreatekVpDic  Completely");

                cboSearchSex.SelectedIndex = 0;
                cboSearchSex1.SelectedIndex = 0;
                if (_AppMode == AppType.AppEnum.AppMode.License && !notConnecttoFPD)
                {
                    if (_FPDMode != AppType.AppEnum.FPDMode.Other)
                    {
                        if (modName.ToUpper().Contains("FPD550PCI")) InitFPD550_PCI();
                        if (modName.ToUpper().Contains("FPD500PCI") || modName.ToUpper().Contains("FPD500")) InitFPD500_PCI();
                        if (modName.ToUpper().Contains("FPD560") || modName.ToUpper().Contains("FPD500ETHERNET")) InitFPD560();
                        if (modName.ToUpper().Contains("FDX4343R")) InitFDX4343R();
                        if (modName.ToUpper().Contains("FDX3543RP")) InitFDX3543RP();
                        if (modName.ToUpper().Contains("FPDEU")) InitPanel_EU(PANEL1_FPDEU, GetModeType(modTypeName), XRAY_RECEIVER_PORT);
                    }
                    else
                    {
                        if (modName.ToUpper().Contains("FPD550PCI")) InitFPD550_PCI();
                        if (modName.ToUpper().Contains("FPD500PCI") || modName.ToUpper().Contains("FPD500")) InitFPD500_PCI();
                        if (modName.ToUpper().Contains("FPD560") || modName.ToUpper().Contains("FPD500ETHERNET")) InitFPD560();
                        if (modName.ToUpper().Contains("FDX4343R")) InitFDX4343R();
                        if (modName.ToUpper().Contains("FDX3543RP")) InitFDX3543RP();
                        if (modName.ToUpper().Contains("FPDEU")) InitPanel_EU(PANEL1_FPDEU, GetModeType(modTypeName), XRAY_RECEIVER_PORT);

                        if (modName2.ToUpper().Contains("FPD550PCI")) InitFPD550_PCI();
                        if (modName2.ToUpper().Contains("FPD500PCI") || modName2.ToUpper().Contains("FPD500")) InitFPD500_PCI();
                        if (modName2.ToUpper().Contains("FPD560") || modName2.ToUpper().Contains("FPD500ETHERNET")) InitFPD560();
                        if (modName2.ToUpper().Contains("FDX4343R")) InitFDX4343R();
                        if (modName2.ToUpper().Contains("FDX3543RP")) InitFDX3543RP();
                        if (modName2.ToUpper().Contains("FPDEU")) InitPanel_EU(PANEL2_FPDEU, GetModeType(modTypeName2), XRAY_RECEIVER_PORT);

                    }
                }
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": Init FPD  Completely");
                SearchWL(false);
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": SearchWL  Completely");
                SearchStudy(false);
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": SearchStudy  Completely");
                ModifyAcqButton();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": ModifyAcqButton  Completely");
                ModifyWorkListButtons();
                AppLogger.LogAction.AddLog2List(lstFPD560, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ": ModifyWorkListButtons  Completely");

               
              //  CreateCStoreObject_New();
                //lblMemory.Text = GetTotalMemory().ToString();
                //Thread tInitCounter = new Thread(new ThreadStart(InitCounter));
                //if (lblBackGroudThread.ImageIndex == 0) tInitCounter.IsBackground = true;
                //tInitCounter.Start();
                //lblMemory.Text = getAvailableRAM();

            }
            catch (Exception ex)
            {
                AppLogger.LogAction.LogActions("==>Init DROC().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex.ToString());
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
               
                if (_AppMode == AppType.AppEnum.AppMode.Demo)
                {
                    lblAppMode_Click(lblAppMode, new EventArgs());
                    
                }
                ReadFontSize();
                OK = true;
                AppLogger.LogAction.LogActions("ok");
               
                cmdPanel1_Click(cmdPanel1, new EventArgs());
                //this.Invoke(new _MoveImgFromCaptureDir(autoSearch));
                WLThread = new Thread(autoSearch);
               
                WLThread.Start();


            }
        }
        int GetModeType(string ModalityType)
        {
            int LAN_TYPE = -1;
            switch (ModalityType)
            {
                case "DXF500LAN":
                    AppLogger.LogAction.AddLog2List(lstFPD560, "GetModeType()=DXF500LAN");
                    LAN_TYPE= DXF500LAN_FPDEU;
                    break;
                case "DXF560LAN":
                    AppLogger.LogAction.AddLog2List(lstFPD560, "GetModeType()=DXF560LAN");
                    LAN_TYPE= DXF560LAN_FPDEU;
                    break;
                case "DXF750ELAN":
                    AppLogger.LogAction.AddLog2List(lstFPD560, "GetModeType()=DXF750ELAN");
                    LAN_TYPE= DXF750ELAN_FPDEU;
                    break;
                case "DXF760LAN":
                    AppLogger.LogAction.AddLog2List(lstFPD560, "GetModeType()=DXF760LAN");
                    LAN_TYPE= DXF760LAN_FPDEU;
                    break;
                default:
                    AppLogger.LogAction.AddLog2List(lstFPD560, "GetModeType()=NONE-->Failed");
                    LAN_TYPE= - 1;
                    break;
            }
            return LAN_TYPE;
            
        }
        void Try2SetPanelSelectinDualMode()
        {
            try
            {
                if (_AppMode != AppType.AppEnum.AppMode.License)
                {
                    if (_FPDMode == AppType.AppEnum.FPDMode.DualMode)
                    {
                        if (modName.ToUpper().Contains("FPD550PCI"))
                        {
                            FPDPanel1Idx = 0;
                            FPDPanel2Idx = 1;
                            FPDPanelSelectErrValue = -1;
                        }
                        if (modName.ToUpper().Contains("FPD500PCI"))
                        {
                            FPDPanel1Idx = 0;
                            FPDPanel2Idx = 1;
                            FPDPanelSelectErrValue = -1;
                        }
                        if (modName.ToUpper().Contains("FPD560") || modName.ToUpper().Contains("FPD500ETHERNET"))
                        {
                            FPDPanel1Idx = 0;
                            FPDPanel2Idx = 1;
                            FPDPanelSelectErrValue = -1;
                        }
                        if (modName.ToUpper().Contains("FDX4343R"))
                        {
                            FPDPanel1Idx = 0;
                            FPDPanel2Idx = 1;
                            FPDPanelSelectErrValue = -1;
                        }
                        if (modName.ToUpper().Contains("FPDEU"))
                        {
                            FPDPanel1Idx = 0;
                            FPDPanel2Idx = 1;
                            FPDPanelSelectErrValue = -1;
                        }

                    }
                }
            }
            catch
            {
            }
        }
        void mnu5_Click(object sender, EventArgs e)
        {
            ReProcessing();
        }

        void mnuChangeViewPos_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch
            {
            }
        }

        void mnuselectImgPath_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch
            {
            }
        }
        /// <summary>
        /// Show properties of selected schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnu4_Click(object sender, EventArgs e)
        {
            try
            {
                ScheduledControl _ScheduledControl = ((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl as ScheduledControl;
                frm_ScheduleProperties _ScheduleProperties = new frm_ScheduleProperties(_ScheduledControl, m_intCurrDevice1);
                _ScheduleProperties.ShowDialog();
            }
            catch
            {
            }
        }

        void mnu0_Click(object sender, EventArgs e)
        {
            try
            {
                ScheduledControl _ScheduledControl = GetSelectedScheduled();
                if (_ScheduledControl != null)
                    ShortCut2AddProc(_ScheduledControl.A_Code, _ScheduledControl.P_Code, true, ref _newDetailID);
            }
            catch
            {
            }
        }

        void mnu2_Click(object sender, EventArgs e)
        {
            DelSelectedProc();
        }

        void mnu1_Click(object sender, EventArgs e)
        {
            AddProc();
        }
        private void Try2FreeThread()
        {
            try
            {
                if (tfpd != null && tfpd.ThreadState == ThreadState.Running)
                {
                    tfpd.Abort();
                    tfpd = null;
                }
            }
            catch
            {
            }
        }

      
        
        /// <summary>
        /// Khởi tạo kết nối với tấm đang chọn
        /// </summary>
        /// <param name="pv_objAss"></param>
        /// <param name="MethodName2Run"></param>
        public void ProcessInterfacesAndMethods(Assembly pv_objAss, string MethodName2Run, object[] args, bool ShowMsg)
        {

            object NewObject = null;
            try
            {
                foreach (Type t in pv_objAss.GetTypes())
                {
                    if (t.IsInterface)
                    {
                        Type t2 = default(Type);
                        foreach (Type t1 in pv_objAss.GetTypes())
                        {
                            object i = t1.GetInterface(t.Name);
                            if ((i != null))
                            {
                                NewObject = pv_objAss.CreateInstance(t1.FullName);
                                t2 = t1;
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                        MethodInfo[] mi = t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        //  methods
                        //MethodInfo m = default(MethodInfo);
                        foreach (MethodInfo m in mi)
                        {
                            if (m.Name.ToUpper() == MethodName2Run)
                                if (ShowMsg && m.ReturnParameter.ParameterType.Name.ToUpper() == "STRING") Utility.ShowMsg(t2.InvokeMember(m.Name, BindingFlags.Default | BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, NewObject, args).ToString());
                                else t2.InvokeMember(m.Name, BindingFlags.Default | BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, NewObject, args);

                        }
                    }
                }
                NewObject = null;
            }
            catch (Exception ex)
            {

            }
        }

        private void cmdRepeatAcquisition_Click(object sender, EventArgs e)
        {
            RepeatAcq();

        }

        private void cmdVK_Click(object sender, EventArgs e)
        {

        }




        private void button1_Click(object sender, EventArgs e)
        {

        }
        void DelSelectedProc()
        {
            try
            {
                if (pnlScheduled.Controls.Count <= 1)
                {
                    new frm_LargeMsgBoxOK(MultiLanguage.GetText(globalVariables.DisplayLanguage, "Thông báo", "Warning"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đây là dịch vụ cuối cùng của BN nên bạn không thể xóa. Bạn có thể thêm dịch vụ khác và sau đó xóa dịch vụ đang chọn", "This is the last service so you can not delete it. Pls add new other service before deleting"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đồng ý", "OK"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Không đồng ý", "Cancel")).ShowDialog();
                    return;
                }
                foreach (Control ctr in pnlScheduled.Controls)
                {
                    ScheduledControl _Scheduled = ctr as ScheduledControl;
                    if (_Scheduled.isPressed && _Scheduled.Status == 0)
                    {
                        if (new frm_LargeMsgBox(MultiLanguage.GetText(globalVariables.DisplayLanguage, "XÁC NHẬN XÓA", "Confirm Delete"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Bạn có muốn xóa Vị trí chụp " + _Scheduled.A_Vn_Name + "/" + _Scheduled.P_Vn_Name + " hay không?", "Do you want to delete service " + _Scheduled.A_Vn_Name + "/" + _Scheduled.P_Vn_Name + " ?"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "ĐỒNG Ý XÓA", "YES"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "KHÔNG XÓA", "NO")).ShowDialog() == DialogResult.OK)
                        {
                            if (new DoctorController().DeleteDetail(_Scheduled.DETAIL_ID) == ActionResult.Success)
                            {
                                DeleteFromDataTable(_Scheduled.DETAIL_ID);
                                pnlScheduled.Controls.Remove(ctr);
                                return;
                            }
                        }
                    }
                }

            }
            catch
            {

            }
            finally
            {
                cmdDelProc.Enabled = pnlScheduled.Controls.Count > 0 && GetSelectedScheduled() != null;
            }
        }
        void DelSelectedProc(ScheduledControl _Scheduled)
        {
            try
            {
                if (pnlScheduled.Controls.Count <= 1)
                {
                    new frm_LargeMsgBoxOK(MultiLanguage.GetText(globalVariables.DisplayLanguage, "Thông báo", "Warning"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đây là dịch vụ cuối cùng của BN nên bạn không thể xóa. Bạn có thể thêm dịch vụ khác và sau đó xóa dịch vụ đang chọn", "This is the last service so you can not delete it. Pls add new other service before deleting"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đồng ý", "OK"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Không đồng ý", "Cancel")).ShowDialog();
                    return;
                }
                if (_Scheduled.Status == 0)
                {
                    if (new frm_LargeMsgBox(MultiLanguage.GetText(globalVariables.DisplayLanguage, "XÁC NHẬN XÓA", "Confirm Delete"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Bạn có muốn xóa Vị trí chụp " + _Scheduled.A_Vn_Name + "/" + _Scheduled.P_Vn_Name + " hay không?", "Do you want to delete service " + _Scheduled.A_Vn_Name + "/" + _Scheduled.P_Vn_Name + " ?"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "ĐỒNG Ý XÓA", "YES"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "KHÔNG XÓA", "NO")).ShowDialog() == DialogResult.OK)
                    {
                        if (new DoctorController().DeleteDetail(_Scheduled.DETAIL_ID) == ActionResult.Success)
                        {
                            DeleteFromDataTable(_Scheduled.DETAIL_ID);
                            pnlScheduled.Controls.Remove(_Scheduled);
                            return;
                        }
                    }

                }
            }
            catch
            {

            }
            finally
            {
                cmdDelProc.Enabled = pnlScheduled.Controls.Count > 0 && GetSelectedScheduled() != null;
            }
        }
        private void cmdDelProc_Click(object sender, EventArgs e)
        {
            DelSelectedProc();
        }

        void DeleteFromDataTable(int Detail_ID)
        {
            try
            {

                DataRow[] arrDR = m_dtAcquisitionDataSource.Select("DETAIL_ID=" + Detail_ID.ToString());
                long RegID = Utility.Int64Dbnull(arrDR[0]["REG_ID"], -1);
                if (arrDR.Length > 0)
                {
                    m_dtAcquisitionDataSource.Rows.Remove(arrDR[0]);
                }

                m_dtAcquisitionDataSource.AcceptChanges();
                //Update Procedure List to DB and Datasource
                string ProcedureList = GetProcedureList();
                new RegDetailController().UpdateProcedureList(RegID, ProcedureList);
                //Update Dataset
                DataRow[] drWL = m_dtWLDataSource.Select("Reg_ID=" + RegID);
                DataRow[] drST = m_dtStudyListDataSource.Select("Reg_ID=" + RegID);
                if (drWL.Length > 0)
                    drWL[0]["ProcedureList"] = ProcedureList;
                if (drST.Length > 0)
                    drST[0]["ProcedureList"] = ProcedureList;
                m_dtWLDataSource.AcceptChanges();
                m_dtStudyListDataSource.AcceptChanges();
            }
            catch
            {
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void cmdAPConfig_Click(object sender, EventArgs e)
        {
            frm_Anatomy_Projection _ChooseProjection = new frm_Anatomy_Projection();
            _ChooseProjection.ShowDialog();
        }

        private void cmdIEConfig_Click(object sender, EventArgs e)
        {

            //frmIEConfig _IEC = new frmIEConfig();
            //_IEC.ShowDialog();
        }

        private void cmdBrightAndContrast_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdAList_Click(object sender, EventArgs e)
        {
            frm_AnotomyList _AnotomyList = new frm_AnotomyList();
            _AnotomyList.ShowDialog();
        }

        private void cmdPList_Click(object sender, EventArgs e)
        {
            frm_ProjectionList _ProjectionList = new frm_ProjectionList();
            _ProjectionList.ShowDialog();
        }

        private void cmdDepart_Click(object sender, EventArgs e)
        {
            frmDepartmentList DepartList = new frmDepartmentList();
            DepartList.Show();
        }

        private void cmdDeviceList_Click(object sender, EventArgs e)
        {
            frm_Mod_TypeList newForm = new frm_Mod_TypeList();
            newForm.Show();
        }

        private void cmdCountryList_Click(object sender, EventArgs e)
        {
            frm_CountryList CountryList = new frm_CountryList();

            CountryList.Show();
        }

        private void cmdDoctorList_Click(object sender, EventArgs e)
        {
            frm_DoctorList newForm = new frm_DoctorList();
            newForm.Show();
        }

        private void cmdPACSServer_Click(object sender, EventArgs e)
        {
            frm_ServerList newForm = new frm_ServerList();
            newForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm_ManufactureList newForm = new frm_ManufactureList();
            newForm.Show();
        }

        private void cmdDeviceList_Click_1(object sender, EventArgs e)
        {
            //using (frm_adSec _adSec = new frm_adSec())
            //{
            //    if (_adSec.ShowDialog() != DialogResult.OK)
            //        return;
            //}
            frm_Modalities newForm = new frm_Modalities();
            newForm.ShowDialog();
            LoadDevice();
            cboDevice.SelectedIndex = Utility.GetSelectedIndex(cboDevice, m_intCurrDevice1.ToString());
        }

        private void cmdZoom_Click(object sender, EventArgs e)
        {
            frmRoomList newForm = new frmRoomList();
            newForm.Show();
        }

       

        private void txtDirCapture_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDir2_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdLoadInterface_Click(object sender, EventArgs e)
        {
            LoadDevice();
        }

        private void cmdSDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fld = new FolderBrowserDialog();
            if (fld.ShowDialog() == DialogResult.OK)
            {
                txtSDir.Text = fld.SelectedPath;

            }
        }

        private void cmdDesDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fld = new FolderBrowserDialog();
            if (fld.ShowDialog() == DialogResult.OK)
            {
                txtDesDir.Text = fld.SelectedPath;

            }
        }

        private void cmdDel_Click(object sender, EventArgs e)
        {
            try
            {
                string smsg = "";
                string[] files = Directory.GetFiles(txtSDir.Text.Trim());
                foreach (string _file in files)
                {
                    if (File.Exists(txtDesDir.Text.Trim() + @"\" + Path.GetFileName(_file)))
                    {
                        smsg += _file + " ; ";
                        File.Delete(txtDesDir.Text.Trim() + @"\" + Path.GetFileName(_file));
                    }
                }
                Utility.ShowMsg(smsg);
            }
            catch
            {
            }

        }
        bool IsValidData()
        {
            try
            {
                mdlStatic.SetMsg(lblErrMsg, "", false);
                if (txtCOMName.Text.Trim() == "")
                {
                    mdlStatic.SetMsg(lblErrMsg, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Bạn phải nhập tên cổng COM", "You have to enter COM name"), true);
                    txtCOMName.Focus();
                    return false;
                }
                if (txtBaudrate.Text.Trim() == "")
                {
                    mdlStatic.SetMsg(lblErrMsg, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Bạn phải nhập giá trị BaudRate(Thường là 9600)", "You have to enter Baudrate value(recommended 9600)"), true);
                    txtBaudrate.Focus();
                    return false;
                }
                if (txtDataBits.Text.Trim() == "")
                {
                    mdlStatic.SetMsg(lblErrMsg, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Bạn phải nhập giá trị DataBits(Thường là 8)", "You have to enter Databits(recommended 8)"), true);
                    txtDataBits.Focus();
                    return false;
                }


                return true;
            }
            catch
            {
                return false;
            }
        }
        bool Try2OpenCOM()
        {
            try
            {
                if (_COM != null && _COM.IsOpen)
                {
                    _COM.Close();
                    _COM = null;
                    _COM = new SerialPort(txtCOMName.Text.Trim(), Convert.ToInt32(txtBaudrate.Text.Trim()), (Parity)cboParity.SelectedIndex, Convert.ToInt32(txtDataBits.Text.Trim()), (StopBits)cboStopbits.SelectedIndex);

                    _COM.Open();

                    AppLogger.LogAction.AddLog2List(lstLog, "OPEN " + _COM.PortName + " Success...");

                    if (isGCOM_VN)
                    {
                        ResetValue();
                        _EnableControl(cmdReady, true, "Ready");
                        isInProgress = false;
                    }
                    _COM.DataReceived += new SerialDataReceivedEventHandler(_COM_DataReceived);
                    return true;
                }
                else
                {
                    _COM = new SerialPort(txtCOMName.Text.Trim(), Convert.ToInt32(txtBaudrate.Text.Trim()), (Parity)cboParity.SelectedIndex, Convert.ToInt32(txtDataBits.Text.Trim()), (StopBits)cboStopbits.SelectedIndex);
                    _COM.Open();
                    // SaveInfor();
                    AppLogger.LogAction.AddLog2List(lstLog, "OPEN" + _COM.PortName + " Success...");
                    if (isGCOM_VN)
                    {
                        ResetValue();
                        _EnableControl(cmdReady, true, "Ready");

                        isInProgress = false;
                    }
                    _COM.DataReceived += new SerialDataReceivedEventHandler(_COM_DataReceived);
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
       

        delegate void SetText4Lable(Label lbl, string content);
        delegate void SetText4LableWithErr(Label lbl, string content, bool isErr);
        void SetText(Label lbl, string content)
        {
            try
            {
                if (lbl.InvokeRequired)
                {
                    lbl.Invoke(new SetText4Lable(SetText), new object[] { lbl, content });
                }
                else
                    lbl.Text = content;
            }
            catch
            {
            }
        }
        delegate void SetValue4Numeric(NumericUpDown nmr, decimal content);
        void SetValueNumeric(NumericUpDown nmr, decimal content)
        {
            try
            {
                if (nmr.InvokeRequired)
                {
                    nmr.Invoke(new SetValue4Numeric(SetValueNumeric), new object[] { nmr, content });
                }
                else
                    nmr.Value = content;
            }
            catch
            {
            }
        }
        delegate void _AutoAddnewSchedule(ScheduledControl newSchedule);
        void AutoAddNewSchedule(ScheduledControl newSchedule)
        {
            try
            {
                if (pnlScheduled.InvokeRequired)
                    pnlScheduled.Invoke(new _AutoAddnewSchedule(AutoAddNewSchedule), new object[] { newSchedule });
                else
                    pnlScheduled.Controls.Add(newSchedule);
            }
            catch
            {
            }
        }
        delegate void _AutoScroll2NewSchedule(ScheduledControl newSchedule);
        void AutoScroll2NewSchedule(ScheduledControl newSchedule)
        {
            try
            {
                if (pnlScheduled.InvokeRequired)
                    pnlScheduled.Invoke(new _AutoScroll2NewSchedule(AutoScroll2NewSchedule), new object[] { newSchedule });
                else
                    pnlScheduled.ScrollControlIntoView(newSchedule);
            }
            catch
            {
            }
        }

        string SubDirLv1()
        {
            if (txtRegNumber2.Text.Trim() == "" || txtRegNumber2.Text.Trim().Length <= 7) return Utility.GetYYYYMMDD(DateTime.Now);
            return txtRegNumber2.Text.Trim().Substring(0, 8);
        }
        string SubDirLv1(string reg_num)
        {
            try
            {
                if (reg_num == "" || reg_num.Length <= 7) return Utility.GetYYYYMMDD(DateTime.Now);
                return reg_num.Substring(0, 8);
            }
            catch
            {
                return "";
            }
        }
        string SubDirLv2_Patient()
        {
            try
            {
                return txtID2.Text.Trim() + "_" + Bodau(txtName2.Text.Trim()).Replace(txtAge.Text.Trim(), "").Trim().Replace(" ", "_").Trim() + "_" + txtAge.Text;
            }
            catch
            {
                return "";
            }
        }
        string SubDirLv2_Patient(DataRow dr)
        {
            return Utility.sDbnull(dr["PATIENT_CODE"]) + "_" + Bodau(Utility.sDbnull(dr["PATIENT_NAME"]).Trim()).Replace(" ", "_").Replace(Utility.sDbnull(dr["Age"]).Trim(), "") + "_" + Utility.sDbnull(dr["Age"]);
        }
        string SubDirLv2_Patient(string PCode, string PName, string pAge)
        {
            return Utility.sDbnull(PCode) + "_" + Bodau(Utility.sDbnull(PName).Trim()).Replace(" ", "_").Replace(Utility.sDbnull(pAge).Trim(), "") + "_" + Utility.sDbnull(pAge);
        }

        string SubDirLv2_Patient_0Age(DataRow dr)
        {
            return Utility.sDbnull(dr["PATIENT_CODE"]) + "_" + Bodau(Utility.sDbnull(dr["PATIENT_NAME"]).Trim()).Replace(" ", "_").Replace(Utility.sDbnull(dr["Age"]).Trim(), "") + "_0";

        }

        #region COM Comunication
        int TimeOfResendingDataFrame = 3;
        int Time2ResendingDataFrame = 5000;
        int CountOfResendingDataFrame = 0;
        int TimeOfResendingEXPCmd = 2;
        int Time2ResendingEXPCmd = 2000;
        int CountOfResendingEXPCmd = 0;
        int State = 0;
        //delegate void AddLog(string content);
        // void AddLog2List(string content)
        //{
        //    try
        //    {
        //        if (lstLog.InvokeRequired)
        //        {
        //            lstLog.Invoke(new AddLog(AddLog2List), new object[] { content });
        //        }
        //        else
        //        {
        //            lstLog.Items.Add(content);
        //            lstLog.Refresh();
        //        }
        //    }
        //    catch
        //    {
        //    }

        //}
        #region Com Decleration
        const int StartCmd = 0x81;
        const int StartData = 0x7E;
        const int EndCmd = 0x82;
        const int EndData = 0xE7;

        const int ReadyCmd = 0x88;
        const int ReadyData = 0x00;
        const int ExposureCmd = 0x89;
        const int ExposureData = 0x00;

        const int AMECExposureCmd = 0x8F;





        #endregion
        #region Com Decleration


        const int SET = 0x02;
        const int LARGEFOCUS = 0xF0;
        const int SMALLFOCUS = 0xF1;
        const int END = 0x03;
        const int ACK = 0x06;
        const int NAK = 0x15;
        const int EXP = 0x88;


        #endregion
        delegate void SetEnableButton(bool enabled);
        void _SetEnableButton(bool enabled)
        {
            try
            {
                if (cmdReady.InvokeRequired)
                {
                    cmdReady.Invoke(new SetEnableButton(_SetEnableButton), new object[] { enabled });
                }
                else
                {
                    cmdReady.Enabled = enabled;
                }
            }
            catch
            {
            }
        }
        delegate void SetTextButton(string text);
        void _SetTextBtn(string text)
        {
            try
            {
                if (cmdReady.InvokeRequired)
                {
                    cmdReady.Invoke(new SetTextButton(_SetTextBtn), new object[] { text });
                }
                else
                {
                    cmdReady.Text = text;
                }
            }
            catch
            {
            }
        }
        bool preventsenddata = false;
        bool preventExp = false;
        void _COM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort _SP = null;
            List<int> lstReceivedData = null;
            string sTime = "";
            if (isGCOM_VN)
            {
                try
                {
                    if (isInProgress)//Chỉ đón dữ liệu khi đã nhấn nút Ready để bắt đầu quá trình truyền dữ liệu
                    {
                        _SP = sender as SerialPort;
                        lstReceivedData = new List<int>();
                        sTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        while (_SP.BytesToRead > 0)
                        {
                            int tempString = _SP.ReadByte();
                            lstReceivedData.Add(tempString);
                            System.Threading.Thread.Sleep(10);
                            //Nếu chưa nhận được GENCMD sau khi gửi EndData thì chỉ nhận từng Byte để kiểm tra?
                            if (!hasJustGENCMDAfterSendEndData)
                            {
                                if (lstReceivedData.Count >= 1) break;
                            }
                            else//Đã gửi EndCmd và chờ GEN cấu hình thành công
                            {
                                if (lstReceivedData.Count >= 2) break;
                            }
                        }
                        _SP.DiscardInBuffer();
                        //Kiểm tra tín hiệu gửi đến là gì
                        if (!hasJustGENCMDAfterSendEndData)
                        {
                            if (lstReceivedData[0] == GENCmd)
                            {
                                if (!hasJustSendEndData)
                                {
                                    //Gửi byte tiếp theo
                                    _intSendOrder++;
                                    if (_intSendOrder <= lstByte.Count - 1)
                                        SendData2COM_AMEC();
                                }
                                else//Vừa gửi byte EndData và nhận được GENCmd
                                {
                                    hasJustGENCMDAfterSendEndData = true;
                                    AppLogger.LogAction.AddLog2List(lstLog, sTime + " -> Đang chờ GEN cấu hình");
                                }
                            }
                            else//Nhận dữ liệu không phải từ GEN
                            {
                                AppLogger.LogAction.AddLog2List(lstLog, sTime + " -> Invalid GENData:" + lstReceivedData[0].ToString("X2"));
                            }
                        }
                        else//Đã nhận được GENCmd sau khi gửi EndData(kết thúc gửi xong 3 thông số)
                        {
                            //Khi GEN cấu hình các tham số tạo cao áp thành công sẽ gửi về cho DROC 2 bytes liên tiếp 
                            //có giá trị như sau: [0x88] [0x00] để DROC chuyển sang trạng thái chuẩn bị đọc ảnh.
                            if (lstReceivedData[0] == ReadyCmd && lstReceivedData[1] == ReadyData)
                            {
                                AppLogger.LogAction.AddLog2List(lstLog, sTime + " -> Sẵn sàng đọc ảnh...");
                            }
                            //Khi người sử dụng ấn nút chụp, GEN sẽ gửi tới DROC 2 byte liên tục: [0x8F] [0x00]
                            else if (lstReceivedData[0] == AMECExposureCmd && lstReceivedData[1] == ExposureData)
                            {
                                AppLogger.LogAction.AddLog2List(lstLog, sTime + " -> Nhận được tín hiệu vừa chụp...");
                            }
                            //Khi GEN phát cao áp xong sẽ gửi tới DROC 2 byte liên tục: [0x89] [0x00] và DROC có thể đọc ảnh từ tấm FPD.
                            else if (lstReceivedData[0] == GENHFCmd && lstReceivedData[1] == GENHFData)
                            {
                                AppLogger.LogAction.AddLog2List(lstLog, sTime + " -> Nhận được tín hiệu phát cao áp...");
                                AppLogger.LogAction.AddLog2List(lstLog, sTime + " -> DROC chờ tín hiệu ảnh từ FPD...");
                                //Reset các giá trị về trạng thái ban đầu
                                ResetValue();
                                _EnableControl(cmdReady, true, "Ready");//Enable nút gửi dữ liệu

                                isInProgress = false;//Phải gửi lại từ đầu do cổng COM bị lỗi

                            }
                        }
                    }

                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    _SP = sender as SerialPort;
                    lstReceivedData = new List<int>();
                    sTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    while (_SP.BytesToRead > 0)
                    {
                        int tempString = _SP.ReadByte();
                        if (File.Exists(Application.StartupPath + @"\COMReceived.log")) AppLogger.LogAction.AddLog2List(lstLog, "Received: " + tempString);

                        lstReceivedData.Add(tempString);
                        System.Threading.Thread.Sleep(10);
                        //Tín hiệu nhận về mỗi lần chứa tối đa 2 phần tử là CMD và DATA
                        //if (lstReceivedData.Count >= 2) break;
                    }
                    _SP.DiscardInBuffer();
                    //Kiểm tra tín hiệu gửi đến là gì
                    switch (State)
                    {
                        case 1://DROC đã gửi Data frame-->Cần kiểm tra dữ liệu gửi về
                            if (lstReceivedData.Count == 1 && lstReceivedData[0] == ACK)
                            {
                                AppLogger.LogAction.AddLog2List(lstLog, "Received: 0x" + Convert.ToByte(ACK).ToString("X2") + "(ACK)");
                                blnCheckSignalAfterSetKvpMas = false;
                                _SetEnableButton(true);
                                CountOfResendingDataFrame = 0;
                                _SetTextBtn("Exposure");
                                cmdReady.Tag = "EX";
                                preventsenddata = true;
                                //trước khi tự động gửi lại thì coi như gửi bằng nhấn nút Exposure-->cần khởi tạo Thread để làm việc
                                isSendbyCmdExp = true;
                                CountOfResendingEXPCmd = 0;
                                //SendExposureCMD();
                            }
                            //Nếu trả về NAK-->Cần gửi lại khung dữ liệu ngay lập tức
                            if (lstReceivedData.Count == 1 && lstReceivedData[0] == NAK)
                            {
                                AppLogger.LogAction.AddLog2List(lstLog, "Received: 0x" + Convert.ToByte(NAK).ToString("X2") + "(NAK)");
                                blnCheckSignalAfterSetKvpMas = false;
                                CountOfResendingDataFrame = 0;
                                preventsenddata = true;
                                isSendbyCmd = true;
                                SendData2COM();
                            }
                            //Nếu không phải ACK và NAK -->Cần gửi lại khung dữ liệu ngay lập tức
                            if (lstReceivedData.Count == 1 && lstReceivedData[0] != NAK && lstReceivedData[0] != ACK)
                            {
                                AppLogger.LogAction.AddLog2List(lstLog, "Received: 0x" + Convert.ToString((byte)lstReceivedData[0], 16) + " (neither ACK nor NAK)");
                                blnCheckSignalAfterSetKvpMas = false;
                                CountOfResendingDataFrame = 0;
                                preventsenddata = true;
                                isSendbyCmd = true;
                                SendData2COM();
                            }
                            break;
                        case 2://Nếu đã gửi EXP mà lại nhận phản hồi từ HFG là ACK-->kết thúc trao đổi dữ liệu
                            if (lstReceivedData.Count == 1 && lstReceivedData[0] == ACK)
                            {
                                AppLogger.LogAction.AddLog2List(lstLog, "Received: 0x" + Convert.ToByte(ACK).ToString("X2") + "(ACK)");
                                blnCheckSignalAfterEXPCommand = false;
                                CountOfResendingEXPCmd = 0;
                                isSendbyCmd = true;
                                isSendbyCmdExp = true;
                                CountOfResendingDataFrame = 0;
                                //tData.Stop();
                                //tExp.Stop();
                                preventsenddata = false;
                                preventExp = true;
                                _SetTextBtn("Ready");
                                cmdReady.Tag = "R";
                                State = 0;
                            }
                            //Nếu trả về NAK-->Cần gửi lại khung dữ liệu ngay lập tức
                            if (lstReceivedData.Count == 1 && lstReceivedData[0] == NAK)
                            {
                                AppLogger.LogAction.AddLog2List(lstLog, "Received: 0x" + Convert.ToByte(NAK).ToString("X2") + "(NAK)");
                                blnCheckSignalAfterEXPCommand = false;
                                CountOfResendingEXPCmd = 0;
                                isSendbyCmdExp = true;
                                SendExposureCMD();
                            }
                            //Nếu không phải ACK và NAK -->Cần gửi lại khung dữ liệu ngay lập tức
                            if (lstReceivedData.Count == 1 && lstReceivedData[0] != NAK && lstReceivedData[0] != ACK)
                            {
                                AppLogger.LogAction.AddLog2List(lstLog, "Received: 0x" + Convert.ToString((byte)lstReceivedData[0], 16) + " (neither ACK nor NAK)");
                                blnCheckSignalAfterEXPCommand = false;
                                CountOfResendingEXPCmd = 0;
                                isSendbyCmdExp = true;
                                SendExposureCMD();
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                }
            }

        }
        #region AMEC GCOM
        const int StartKVP = 0x83;
        const int StartmAH = 0x84;
        const int StartmAL = 0x85;
        const int StartmSH = 0x86;
        const int StartmSL = 0x87;

        const int GENCmd = 0x8E;

        const int GENHFCmd = 0x89;
        const int GENHFData = 0x00;

        bool hasJustSendEndData = false;
        bool hasJustGENCMDAfterSendEndData = false;




        int _intSendOrder = 0;
        int AMEC_kVp = 0;
        string MA_bin = "";
        string MAS_bin = "";
        int mAH = 0;
        int mAL = 0;
        int mSH = 0;
        int mSL = 0;
        List<byte> lstByte = new List<byte>();
        List<string> lstCommand = new List<string>();
        bool isInProgress = false;
        void ResetValue()
        {
            try
            {
                lstByte.Clear();
                lstCommand.Clear();
                _intSendOrder = 0;
                hasJustGENCMDAfterSendEndData = false;
                hasJustSendEndData = false;
                AMEC_kVp = (int)lblAMEC_KvpVal.Value;
                MA_bin = VBString.Right("00000000000000" + DecimalToBase(Convert.ToInt32(lblAMEC_mAVal.Value), 2), 14);
                MAS_bin = VBString.Right("00000000000000" + DecimalToBase(Convert.ToInt32(lblAMEC_timeVal.Value), 2), 14);
                mAH = BaseToDecimal(MA_bin.Substring(0, 7), 2);
                mAL = BaseToDecimal(MA_bin.Substring(7, 7), 2);
                mSH = BaseToDecimal(MAS_bin.Substring(0, 7), 2);
                mSL = BaseToDecimal(MAS_bin.Substring(7, 7), 2);

                lstByte.AddRange(new byte[] { 0x81, 0x7E, 0x83, Convert.ToByte(_AMECdickVp[AMEC_kVp]), 0x84, Convert.ToByte(mAH), 0x85,
             Convert.ToByte(mAL),0x86, Convert.ToByte(mSH), 0x87,Convert.ToByte(mSL),0x82,0xE7});
                lstCommand.AddRange(new string[] { "Start Command", "Start Data", "KVp Command", "Kvp Data", "mAH Command", "mAH Data", "mAL Command",
             "mAL Data","mSH Command","mSH Data", "mSL Command","mSL Data","End Command","End Data"});
            }
            catch
            {
            }
        }
        void SendData2COM_AMEC()
        {
            try
            {
                if (_COM == null || !_COM.IsOpen)
                {
                    AppLogger.LogAction.AddLog2List(lstLog, "Cổng COM đang bị đóng. Bạn cần truyền lại dữ liệu từ đầu bằng cách nhấn nút Ready");
                    cmdReady.Enabled = true;//Enable nút gửi dữ liệu
                    cmdReady.Text = "Ready";//Quay về trạng thái ban đầu khi chưa gửi
                    isInProgress = false;//Phải gửi lại từ đầu do cổng COM bị lỗi
                    hasJustSendEndData = false;//Chưa gửi byte EndData
                    return;
                }
                PlayBeep(1);

                //Gửi byte thứ i tới GEN
                byte _byte2Send = lstByte[_intSendOrder];
                string _Command = lstCommand[_intSendOrder];
                if (_byte2Send == EndData) hasJustSendEndData = true;
                string sTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                _COM.Write(new byte[] { _byte2Send }, 0, 1);

                AppLogger.LogAction.AddLog2List(lstLog, sTime + " -> Gửi dữ liệu: " + _Command + "=" + VBString.Right("0x" + _byte2Send.ToString("X2"), 4));
            }
            catch
            {
            }
        }


        #endregion
        private void cmdReady_Click(object sender, EventArgs e)
        {
            try
            {
                if (_AppMode == AppType.AppEnum.AppMode.Demo)
                {
                    FirstExposure = true;
                    _ViewState = AppType.AppEnum.ViewState.Capture;
                    ScheduledControl _reObj = GetSelectedScheduled();
                    if (_reObj == null)
                    {
                        new frm_LargeMsgBoxOK(MultiLanguage.GetText(globalVariables.DisplayLanguage, "Thông báo", "Warning"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Bạn cần chọn dịch vụ cần chụp trước khi nhấn nút Thực hiện chụp", "You have to select at least one Service before start exposure"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đồng ý", "OK"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Hủy bỏ", "Cancel")).ShowDialog();
                        return;

                    }
                    ViewImg();
                }
                else
                {
                    if (isGCOM_VN)
                    {
                        try
                        {
                            isInProgress = true;
                            //Bắt đầu reset value tạo các giá trị kvp,ma,mas để gửi vào GEN
                            ResetValue();
                            cmdReady.Enabled = false;
                            //cmdCancel.Enabled = true;
                            SendData2COM_AMEC();
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        try
                        {

                            CountOfResendingEXPCmd = 0;
                            isSendbyCmd = true;
                            isSendbyCmdExp = true;
                            CountOfResendingDataFrame = 0;
                            if (cmdReady.Tag.ToString() == "R")
                            {
                                preventsenddata = false;
                                isSendbyCmd = true;
                                SendData2COM();
                            }
                            else
                            {
                                preventExp = false;
                                isSendbyCmdExp = true;
                                SendExposureCMD();
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
            }
        }
        delegate void AutoResendDataFrame();
        bool isSendbyCmd = true;
        void _AutoResendDataFrame()
        {
            try
            {
                while (State == 1 && blnCheckSignalAfterSetKvpMas && CountOfResendingDataFrame < TimeOfResendingDataFrame)
                {

                    if (State == 1 && blnCheckSignalAfterSetKvpMas)
                    {
                        if (isSendbyCmd) Thread.Sleep(Time2ResendingDataFrame);
                        isSendbyCmd = false;
                        CountOfResendingDataFrame++;
                        if (!preventsenddata) SendData2COM();
                        if (CountOfResendingDataFrame >= TimeOfResendingDataFrame)
                        {
                            isSendbyCmd = true;
                            blnCheckSignalAfterSetKvpMas = false;
                            CountOfResendingDataFrame = 0;
                            _SetEnableButton(true);
                            _SetTextBtn("Ready");
                            cmdReady.Tag = "R";

                            State = 0;

                        }
                    }
                    Thread.Sleep(Time2ResendingDataFrame);

                }
            }
            catch
            {
            }
        }
        bool isSendbyCmdExp = true;
        void _AutoResendExpCmd()
        {
            try
            {
                while (State == 2 && blnCheckSignalAfterEXPCommand && CountOfResendingEXPCmd < TimeOfResendingEXPCmd)
                {

                    if (State == 2 && blnCheckSignalAfterEXPCommand)
                    {
                        if (isSendbyCmdExp) Thread.Sleep(Time2ResendingEXPCmd);
                        isSendbyCmdExp = false;
                        CountOfResendingEXPCmd++;
                        if (!preventExp) SendExposureCMD();
                        if (CountOfResendingEXPCmd >= TimeOfResendingEXPCmd)
                        {
                            isSendbyCmdExp = true;
                            blnCheckSignalAfterEXPCommand = false;
                            CountOfResendingEXPCmd = 0;
                            _SetEnableButton(true);
                            _SetTextBtn("Ready");
                            cmdReady.Tag = "R";

                            State = 0;
                        }
                    }
                    Thread.Sleep(Time2ResendingEXPCmd);
                }
            }
            catch
            {
            }
        }
        void SendExposureCMD()
        {
            try
            {
                if (_COM == null || !_COM.IsOpen)
                {
                    AppLogger.LogAction.AddLog2List(lstLog, "COM is closed...");
                    return;
                }
                string sTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                if (CountOfResendingEXPCmd <= 0 && !isSendbyCmdExp && !blnCheckSignalAfterEXPCommand)
                    return;
                _COM.Write(new byte[] { EXP }, 0, 1);
                if (isSendbyCmdExp) AppLogger.LogAction.AddLog2List(lstLog, "Send: EXP=0x" + EXP.ToString("X2"));
                else AppLogger.LogAction.AddLog2List(lstLog, "resend " + CountOfResendingEXPCmd.ToString() + ": EXP=0x" + EXP.ToString("X2"));
                State = 2;
                if (isSendbyCmdExp)
                {

                    blnCheckSignalAfterEXPCommand = true;
                    //tExp.Enabled = true;
                    //tExp.Start();
                    Thread t = new Thread(new ThreadStart(_AutoResendExpCmd));
                    t.Start();
                    //t.Join();

                }
                //_SetTextBtn("Ready");
                //AddLog2List("Send Exposure: " + ExposureCmd.ToString("X") + " "+ ExposureData.ToString("X") + " [" + sTime + "]");
            }
            catch
            {
            }
        }
        #region Checksum
        public const char NULL = (char)0;
        public const char STX = (char)2;
        public const char ETX = (char)3;
        public const char EOT = (char)4;
        public const char ENQ = (char)5;

        public const char CR = (char)13;
        public const char LF = (char)10;
        public const char VT = (char)11;

        public const char ETB = (char)23;
        public const char FS = (char)28;
        public const char GS = (char)29;
        public const char SOH = (char)1;
        public static int REPORTTYPE;
        public static readonly string CRLF = String.Format("{0}{1}", CR, LF);
        private string _errorString = "-vietbait-hientd-error";
        /// <summary>
        /// Hàm trả về chuỗi Checksum của một Frame.
        /// Check khi có một trong hai giá trị ETX, ETB
        /// </summary>
        /// <param name="frame">Chuỗi cần kiểm tra</param>
        /// <returns>Chuỗi trả về là 2 ký tự dùng để checksum</returns>


        #endregion
        private byte GetCheckSumValue(byte[] data)
        {
            byte checksum = 0;

            int sumOfChars = 0;
            bool complete = false;

            //take each byte in the string and add the values
            foreach (byte _byte in data)
            {
                int byteVal = Convert.ToInt32(_byte);

                switch (byteVal)
                {
                    //case STX:
                    //    // sumOfChars = 0;
                    //    break;
                    //case ETX:
                    //case ETB:
                    //    sumOfChars += byteVal;
                    //    complete = true;
                    //    break;
                    default:
                        sumOfChars += byteVal;
                        break;
                }

                if (complete)
                    break;
            }

            if (sumOfChars > 0)
            {
                //hex value mod 256 is checksum, return as hex value in upper case
                checksum = Convert.ToByte(sumOfChars % 256);
            }

            //if checksum is only 1 char then prepend a 0
            return checksum;
        }
        bool blnCheckSignalAfterSetKvpMas = false;
        bool blnCheckSignalAfterEXPCommand = false;
        void SendData2COM_LargefocusAdded()
        {
            try
            {
                if (_COM == null || !_COM.IsOpen)
                {
                    AppLogger.LogAction.AddLog2List(lstLog, "COM is closed...");
                    return;
                }

                PlayBeep(1);
                string sTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                int iCount = 0;
                Byte LSFValue = LARGE_FOCUS == 1 ? Convert.ToByte(LARGEFOCUS) : Convert.ToByte(SMALLFOCUS);
                byte[] _byte2sendHF = new byte[] { Convert.ToByte(lblKvpVal.Value), Convert.ToByte(lblmAsVal.Value), LSFValue };
                byte CS = GetCheckSumValue(_byte2sendHF);
                if (CountOfResendingDataFrame <= 0 && !isSendbyCmd && !blnCheckSignalAfterSetKvpMas)
                    return;
                _COM.Write(new byte[] { Convert.ToByte(SET), Convert.ToByte(lblKvpVal.Value), Convert.ToByte(lblmAsVal.Value), LSFValue, CS, Convert.ToByte(END) }, 0, 5);
                State = 1;
                if (isSendbyCmd)
                {
                    preventsenddata = false;
                    _SetEnableButton(false);
                    blnCheckSignalAfterSetKvpMas = true;
                    //tData.Enabled = true;
                    //tData.Start();
                    Thread t = new Thread(new ThreadStart(_AutoResendDataFrame));
                    t.Start();
                    //t.Join();

                }
                if (isSendbyCmd) AppLogger.LogAction.AddLog2List(lstLog, "Send: Data frame=0x" + Convert.ToByte(SET).ToString("X2") + " 0x" + Convert.ToByte(lblKvpVal.Value).ToString("X2") + " 0x" + Convert.ToByte(lblmAsVal.Value).ToString("X2") + " 0x" + LSFValue.ToString("X2") + " 0x" + CS.ToString("X2") + " 0x" + Convert.ToByte(END).ToString("X2"));
                else AppLogger.LogAction.AddLog2List(lstLog, "resend " + CountOfResendingDataFrame.ToString() + ": Data frame=0x" + Convert.ToByte(SET).ToString("X2") + " 0x" + Convert.ToByte(lblKvpVal.Value).ToString("X2") + " 0x" + Convert.ToByte(lblmAsVal.Value).ToString("X2") + " 0x" + LSFValue.ToString("X2") + " 0x" + CS.ToString("X2") + " 0x" + Convert.ToByte(END).ToString("X2"));
                //string sSendData = "0x81 0x7E 0x83 " + Convert.ToByte(_dickVp[nmrKVP.Value]).ToString("X2") + " 0x84 " + Convert.ToByte(_3MBS_MA).ToString("X2") + " 0x85 " + Convert.ToByte(_7MBS_MA).ToString("X2") + " 0x86 " + Convert.ToByte(_6MBS_MAS).ToString("X2") + " 0x87 " + Convert.ToByte(_7MBS_MAS).ToString("X2") + " 0x82 0xE7";
                //AddLog2List("Send Data: " + sSendData);//+ " [" + sTime + "]");
            }
            catch
            {
            }
        }
        void SendData2COM()
        {
            try
            {
                if (_COM == null || !_COM.IsOpen)
                {
                    AppLogger.LogAction.AddLog2List(lstLog, "COM is closed...");
                    return;
                }

                PlayBeep(1);
                string sTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                int iCount = 0;
                Byte LSFValue = LARGE_FOCUS == 1 ? Convert.ToByte(LARGEFOCUS) : Convert.ToByte(SMALLFOCUS);
                byte[] _byte2sendHF = new byte[] { Convert.ToByte(lblKvpVal.Value), Convert.ToByte(lblmAsVal.Value) };
                byte CS = GetCheckSumValue(_byte2sendHF);
                if (CountOfResendingDataFrame <= 0 && !isSendbyCmd && !blnCheckSignalAfterSetKvpMas)
                    return;
                _COM.Write(new byte[] { Convert.ToByte(SET), Convert.ToByte(lblKvpVal.Value), Convert.ToByte(lblmAsVal.Value), CS, Convert.ToByte(END) }, 0, 5);
                State = 1;
                if (isSendbyCmd)
                {
                    preventsenddata = false;
                    _SetEnableButton(false);
                    blnCheckSignalAfterSetKvpMas = true;
                    //tData.Enabled = true;
                    //tData.Start();
                    Thread t = new Thread(new ThreadStart(_AutoResendDataFrame));
                    t.Start();
                    //t.Join();

                }
                if (isSendbyCmd) AppLogger.LogAction.AddLog2List(lstLog, "Send: Data frame=0x" + Convert.ToByte(SET).ToString("X2") + " 0x" + Convert.ToByte(lblKvpVal.Value).ToString("X2") + " 0x" + Convert.ToByte(lblmAsVal.Value).ToString("X2") + " 0x" + LSFValue.ToString("X2") + " 0x" + CS.ToString("X2") + " 0x" + Convert.ToByte(END).ToString("X2"));
                else AppLogger.LogAction.AddLog2List(lstLog, "resend " + CountOfResendingDataFrame.ToString() + ": Data frame=0x" + Convert.ToByte(SET).ToString("X2") + " 0x" + Convert.ToByte(lblKvpVal.Value).ToString("X2") + " 0x" + Convert.ToByte(lblmAsVal.Value).ToString("X2") + " 0x" + LSFValue.ToString("X2") + " 0x" + CS.ToString("X2") + " 0x" + Convert.ToByte(END).ToString("X2"));
                //string sSendData = "0x81 0x7E 0x83 " + Convert.ToByte(_dickVp[nmrKVP.Value]).ToString("X2") + " 0x84 " + Convert.ToByte(_3MBS_MA).ToString("X2") + " 0x85 " + Convert.ToByte(_7MBS_MA).ToString("X2") + " 0x86 " + Convert.ToByte(_6MBS_MAS).ToString("X2") + " 0x87 " + Convert.ToByte(_7MBS_MAS).ToString("X2") + " 0x82 0xE7";
                //AddLog2List("Send Data: " + sSendData);//+ " [" + sTime + "]");
            }
            catch
            {
            }
        }
        void SendExposureCMDold()
        {
            try
            {
                if (_COM == null || !_COM.IsOpen)
                {
                    AppLogger.LogAction.AddLog2List(lstLog, "COM is closed...");
                    return;
                }
                string sTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                _COM.Write(new byte[] { ExposureCmd, ExposureData }, 0, 2);
                cmdReady.Tag = "WI";
                cmdReady.Enabled = false;
                iCount = 0;
                pnlScheduled.Enabled = false;
                AppLogger.LogAction.AddLog2List(lstLog, "Send Exposure: " + ExposureCmd.ToString("X") + ExposureData.ToString("X") + " [" + sTime + "]");
            }
            catch
            {
            }
        }
        void SendData2COMold()
        {
            try
            {
                if (_COM == null || !_COM.IsOpen)
                {
                    AppLogger.LogAction.AddLog2List(lstLog, "COM is closed...");
                    return;
                }
                PlayBeep(1);
                string sTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                cmdReady.Enabled = false;
                kVp = (int)lblKvpVal.Value;// VB6.Strings.Right("0000000000" + DecimalToBase(200, 2), 8);

                string MA_bin = VB6.Strings.Right("0000000000" + DecimalToBase(mA, 2), 10);
                string MAS_bin = VB6.Strings.Right("00000000000000" + DecimalToBase(mAs, 2), 13);
                //int _3MBS_MA = BaseToDecimal(MA_bin.Substring(0, 3), 2);
                //int _7MBS_MA = BaseToDecimal(MA_bin.Substring(3, 7), 2);
                int _6MBS_MAS = BaseToDecimal(MAS_bin.Substring(0, 6), 2);
                int _7MBS_MAS = BaseToDecimal(MAS_bin.Substring(6, 7), 2);
                mAs = (int)lblmAsVal.Value;
                //_COM.Write(new byte[] { StartCmd, StartData, 0x83, Convert.ToByte(_dickVp[kVp]), 0x84, Convert.ToByte(_3MBS_MA), 0x85, Convert.ToByte(_7MBS_MA), 0x86, Convert.ToByte(_6MBS_MAS), 0x87, Convert.ToByte(_7MBS_MAS), EndCmd, EndData }, 0, 14);
                //_COM.Write(new byte[] { StartCmd, StartData, 0x83, Convert.ToByte(_kVp), 0x86, Convert.ToByte(mAs), 0x87, Convert.ToByte(_7MBS_MAS), EndCmd, EndData }, 0, 10);
                _COM.Write(new byte[] { StartCmd, StartData, 0x83, Convert.ToByte(kVp), 0x86, Convert.ToByte(mAs), EndCmd, EndData }, 0, 8);
                //string sSendData = "0x81 0x7E 0x83 " + Convert.ToByte(_dickVp[kVp]).ToString("X2") + " 0x84 " + Convert.ToByte(_3MBS_MA).ToString("X2") + " 0x85 " + Convert.ToByte(_7MBS_MA).ToString("X2") + " 0x86 " + Convert.ToByte(_6MBS_MAS).ToString("X2") + " 0x87 " + Convert.ToByte(_7MBS_MAS).ToString("X2") + " 0x82 0xE7";
                //AddLog2List("Send Data: " + sSendData + " [" + sTime + "]");
            }
            catch
            {
            }
        }
        const int base10 = 10;
        char[] cHexa = new char[] { 'A', 'B', 'C', 'D', 'E', 'F' };
        int[] iHexaNumeric = new int[] { 10, 11, 12, 13, 14, 15 };
        int[] iHexaIndices = new int[] { 0, 1, 2, 3, 4, 5 };
        const int asciiDiff = 48;
        string DecimalToBase(int iDec, int numbase)
        {
            string strBin = "";
            int[] result = new int[32];
            int MaxBit = 32;
            for (; iDec > 0; iDec /= numbase)
            {
                int rem = iDec % numbase;
                result[--MaxBit] = rem;
            }
            for (int i = 0; i < result.Length; i++)
                if ((int)result.GetValue(i) >= base10)
                    strBin += cHexa[(int)result.GetValue(i) % base10];
                else
                    strBin += result.GetValue(i);
            strBin = strBin.TrimStart(new char[] { '0' });
            return strBin;
        }
        int BaseToDecimal(string sBase, int numbase)
        {
            int dec = 0;
            int b;
            int iProduct = 1;
            string sHexa = "";
            if (numbase > base10)
                for (int i = 0; i < cHexa.Length; i++)
                    sHexa += cHexa.GetValue(i).ToString();
            for (int i = sBase.Length - 1; i >= 0; i--, iProduct *= numbase)
            {
                string sValue = sBase[i].ToString();
                if (sValue.IndexOfAny(cHexa) >= 0)
                    b = iHexaNumeric[sHexa.IndexOf(sBase[i])];
                else
                    b = (int)sBase[i] - asciiDiff;
                dec += (b * iProduct);
            }
            return dec;
        }
        Dictionary<decimal, int> _AMECdickVp = new Dictionary<decimal, int>();
        void CreateAMECkVpDic()
        {
            decimal kvp = 40M;
            int i = 0;
            while (kvp <= 150M)
            {

                _AMECdickVp.Add(kvp, i);
                kvp += 1M;
                i++;
            }
        }

        Dictionary<decimal, int> _dickVp = new Dictionary<decimal, int>();
        void CreatekVpDic()
        {
            decimal kvp = 15M;
            int i = 1;
            while (kvp <= 75M)
            {

                if (!_dickVp.ContainsKey(kvp)) _dickVp.Add(kvp, i);
                kvp += 0.5M;
                i++;
            }
        }
        private void cmdOpenCom_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                //Thử Connect
                if (!Try2OpenCOM())
                {
                    mdlStatic.SetMsg(lblErrMsg, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Cổng Com bạn chọn không tồn tại hoặc đã được sử dụng bởi một tiến trình khác. Bạn hãy kiểm tra lại", "This Com port is not existed or being used by another process. Pls check it again!"), true);
                    //toolTip1.SetToolTip(cmdComSignal, "Cổng " + txtCOMName.Text.Trim() + " chưa sẵn sàng sử dụng");
                    txtCOMName.Focus();

                }
                else
                {
                    // cmdComSignal.Image =DROCLibs.Properties.Resources.IsOpeningCom;
                    cmdCloseCom.Enabled = true;
                    cmdOpenCom.Enabled = false;

                    //toolTip1.SetToolTip(cmdComSignal, "Đã khởi động cổng COM " + txtCOMName.Text.Trim() + " thành công. DROC đã sẵn sàng bắt tín hiệu...");
                    mdlStatic.SetMsg(lblErrMsg, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đã khởi động cổng COM thành công. DROC đã sẵn sàng bắt tín hiệu...", "Start " + txtCOMName.Text.Trim() + " successfully. DROC is ready to send and get signal from hardware..."), false);
                }
            }
        }
        string ComConfig_filePath = Application.StartupPath + @"\COMCONFIG.txt";
        void LoadCOMConfig()
        {
            #region Load thông tin cấu hình cổng COM
            try
            {

                if (!File.Exists(ComConfig_filePath))
                {
                    txtCOMName.Text = "COM1";
                    txtBaudrate.Text = "9600";
                    txtDataBits.Text = "8";
                    cboStopbits.SelectedIndex = 1;
                    cboParity.SelectedIndex = 0;
                    chkAutoStart.Checked = true;
                }
                else
                {
                    using (StreamReader _reader = new StreamReader(ComConfig_filePath))
                    {
                        string obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            txtCOMName.Text = obj.ToString();
                        }
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            txtBaudrate.Text = obj.ToString();
                        }
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            txtDataBits.Text = obj.ToString();
                        }
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            cboStopbits.SelectedIndex = Convert.ToInt32(obj);
                        }
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            cboParity.SelectedIndex = Convert.ToInt32(obj);
                        }
                        obj = _reader.ReadLine();
                        if (obj != null) chkAutoStart.Checked = obj.ToString() == "1" ? true : false;
                    }
                }
            }
            catch
            {
            }
            finally
            {

            }
            #endregion
        }
        void SaveInfor()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(ComConfig_filePath))
                {
                    writer.WriteLine(txtCOMName.Text);
                    writer.WriteLine(txtBaudrate.Text);
                    writer.WriteLine(txtDataBits.Text);
                    writer.WriteLine(cboStopbits.SelectedIndex.ToString());
                    writer.WriteLine(cboParity.SelectedIndex.ToString());
                    writer.WriteLine(chkAutoStart.Checked ? "1" : "0");

                    writer.Flush();
                    writer.Close();
                }
            }
            catch
            {
            }

        }
        bool Try2CloseCOM()
        {
            try
            {
                if (_COM != null && _COM.IsOpen)
                {
                    _COM.Close();
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void cmdCloseCom_Click(object sender, EventArgs e)
        {
            if (Try2CloseCOM())
            {
                AppLogger.LogAction.AddLog2List(lstLog, "Close " + _COM.PortName + " Success...");
                mdlStatic.SetMsg(lblErrMsg, MultiLanguage.GetText(globalVariables.DisplayLanguage, "Đã đóng cổng COM thành công. DROC sẽ không thực hiện giao tiếp GCOM...", "Closed " + txtCOMName.Text.Trim() + " successfully. DROC will not connect to GCOM..."), false);
                cmdCloseCom.Enabled = false;
                cmdOpenCom.Enabled = true;
                //cmdComSignal.Image =DROCLibs.Properties.Resources.isCloseCom;
            }
        }
        #endregion



        int iCount = 0;
        private void tmrExSignal_Tick(object sender, EventArgs e)
        {
            try
            {
                //if (cmdReady.Tag == null || cmdReady.Tag.ToString() == "R" || cmdReady.Tag.ToString() == "WI") return;
                //iCount++;
                //if (iCount <= 3) PlayBeep(1);
                //if (iCount % 2 == 0)
                //{
                //    //cmdReadySignal.Image =DROCLibs.Properties.Resources.BURN;
                //    cmdReady.ForeColor = Color.White;
                //    cmdReady.BackColor = Color.SteelBlue;
                //}
                //else
                //{
                //    //cmdReadySignal.Image =DROCLibs.Properties.Resources.Ready;
                //    cmdReady.ForeColor = Color.Black;
                //    cmdReady.BackColor = Color.AliceBlue;
                //}
            }
            catch
            {
            }
        }

        private void cmdSetLang_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmdSetLang.Text.Trim() == "VN")
                {
                    globalVariables.DisplayLanguage = "VN";
                    cmdSetLang.Text = "EN";
                    toolTip1.SetToolTip(cmdSetLang, "English UI");
                    toolTip1.SetToolTip(lblFPDStatus, "Double click here to show FPD events log");
                }
                else
                {
                    toolTip1.SetToolTip(lblFPDStatus, "Double click here to show FPD events log");
                    globalVariables.DisplayLanguage = "EN";
                    cmdSetLang.Text = "VN";
                    toolTip1.SetToolTip(cmdSetLang, "VietNamese UI");
                }

                if (System.IO.File.Exists(Application.StartupPath + @"\MultiLanguageCSharp.Dll") || System.IO.File.Exists(Application.StartupPath + @"\MultiLanguageCSharp.Dll"))
                {
                    CSHARP.MultiLanguage.SetLanguage(globalVariables.DisplayLanguage, this, this.GetType().Assembly.ManifestModule.Name.Split('.')[0], globalVariables.OleDbConnection);
                }

            }
            catch
            {
            }
            finally
            {
                mnu0.Text = globalVariables.DisplayLanguage == "VN" ? "Tự thêm mới dịch vụ đang chọn" : "Add view";
                mnu1.Text = globalVariables.DisplayLanguage == "VN" ? "Chọn dịch vụ cần chụp khác" : "Select other views";
                mnu2.Text = globalVariables.DisplayLanguage == "VN" ? "Xóa bớt dịch vụ" : "Delete view";

                mnuselectImgPath.Text = globalVariables.DisplayLanguage == "VN" ? "Chọn lại ảnh cho dịch vụ này" : "Select dicom file for this view";
                mnuChangeViewPos.Text = globalVariables.DisplayLanguage == "VN" ? "Cập nhật lại vị trí chuẩn cho ảnh này" : "Update View-body part for this view";

                mnu4.Text = globalVariables.DisplayLanguage == "VN" ? "Thuộc tính" : "Properties";
                mnu5.Text = globalVariables.DisplayLanguage == "VN" ? "Xử lý lại ảnh gốc" : "Reprocess from raw image";

                SaveUILan();
            }
        }
        string UILAN_PATH = Application.StartupPath + @"\DrocConfigs\UILanguage.dat";
        void SaveUILan()
        {
            try
            {
                string folder = Path.GetDirectoryName(UILAN_PATH);
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                using (StreamWriter writer = new StreamWriter(UILAN_PATH))
                {
                    writer.WriteLine(globalVariables.DisplayLanguage);
                    writer.Flush();
                    writer.Close();

                }
            }
            catch
            {
            }
        }
        void LoadUILan()
        {
            try
            {

                if (!File.Exists(UILAN_PATH))
                {
                    return;
                }
                else
                {
                    using (StreamReader _reader = new StreamReader(UILAN_PATH))
                    {
                        string obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            globalVariables.DisplayLanguage = obj.ToString().Trim();

                        }
                    }
                }
            }
            catch
            {
            }
        }
        private void cmdLogOut_Click(object sender, EventArgs e)
        {
           
           
        }
        #region ExcelImport
        DataSet dsExcelData = new DataSet();
        DataSet dsExcelMapping = new DataSet();
        private string sheetName = "";
        private void cmdBrowseExcelFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenExcel();
            }
            catch
            {
            }
        }
        /// <summary>
        /// Mở file Excel chứa danh sách các BN
        /// </summary>
        void OpenExcel()
        {
            try
            {
                OpenFileDialog openDlg = new OpenFileDialog();
                openDlg.Title = "Chọn file excel chứa DS Bệnh nhân";
                openDlg.FileName = ""; // Default file name
                //openDlg.InitialDirectory = "MyComputer";
                openDlg.DefaultExt = ".xls"; // Default file extension 
                openDlg.Filter = "Microsoft Excel (.xls) OpenOffice (.ods)|*.xls;*.ods;*.xlsx"; // Filter files by extension
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    txtExcelFilePath.Text = openDlg.FileName;
                    toolTip1.SetToolTip(txtExcelFilePath, openDlg.FileName);
                    Directory.SetCurrentDirectory(Path.GetDirectoryName(openDlg.FileName));
                    string fileName = openDlg.FileName;

                    if (!File.Exists(Application.StartupPath + @"\DROCExcelMapping.xml"))
                    {
                        using (DataSet _dstemp = CreateExcelMapping())
                        {
                            dsExcelMapping = _dstemp.Copy();
                            _dstemp.WriteXml(Application.StartupPath + @"\DROCExcelMapping.xml", XmlWriteMode.WriteSchema);
                        }
                    }
                    else
                        dsExcelMapping.ReadXml(Application.StartupPath + @"\DROCExcelMapping.xml");

                    LoadExcelData(fileName);
                }
            }
            catch
            {
            }
        }

        public void LoadExcelData(string fileName)
        {
            try
            {



                //Khởi tạo kết nối
                string TableName = dsExcelMapping.Tables["SheetName"].Rows[0]["SName"].ToString();

                sheetName = TableName.Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", "").Replace("$", "");
                String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";" + "Extended Properties=Excel 8.0;";
                OleDbConnection objConn = new OleDbConnection(sConnectionString);
                objConn.Open();
                //Đọc dữ liệu
                dsExcelData = new DataSet();
                OleDbCommand objCmd = new OleDbCommand("SELECT * FROM [" + TableName + "$]", objConn);
                OleDbDataAdapter objAdapter = new OleDbDataAdapter();
                objAdapter.SelectCommand = objCmd;
                objAdapter.Fill(dsExcelData, TableName);

                //Cấu hình Mapping
                if (isValidFile(dsExcelData, TableName))
                {
                    if (dsExcelData != null && dsExcelData.Tables.Count > 0 && !dsExcelData.Tables[TableName].Columns.Contains("CHON"))
                    {
                        dsExcelData.Tables[TableName].Columns.Add(new DataColumn("CHON", typeof(string)));
                    }
                    grdListExcelData.AutoGenerateColumns = false;
                    grdListExcelData.DataSource = dsExcelData.Tables[TableName].DefaultView;
                }
                else
                {
                    Utility.ShowMsg("File exel của bạn không đúng định dạng. Hãy nhấn OK để chương trình sinh file mẫu cho bạn");
                    CreateDROCTemplate(false);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("File exel của bạn không đúng định dạng. Hãy nhấn OK để chương trình sinh file mẫu cho bạn");
                CreateDROCTemplate(false);
                //Utils.ShowMsg("Lỗi khi đọc dữ liệu Bệnh nhân từ file Excel:\n" + ex.Message);
            }
            finally
            {
                chkAll.Enabled = dsExcelData != null && dsExcelData.Tables.Count > 0 && dsExcelData.Tables[0].Rows.Count > 0;
                chkReverse.Enabled = chkAll.Enabled;
                cmdImport.Enabled = chkAll.Enabled;
                lnkUpdateData.Enabled = chkAll.Enabled;
            }

        }
        public bool isValidFile(DataSet dsExcelData, string TableName)
        {
            try
            {
                if (dsExcelData != null && dsExcelData.Tables.Count > 0 &&
                       dsExcelData.Tables[TableName].Columns.Contains("MA_BN") && dsExcelData.Tables[TableName].Columns.Contains("TEN_BN")
                       && dsExcelData.Tables[TableName].Columns.Contains("NGAY_SINH") && dsExcelData.Tables[TableName].Columns.Contains("GIOI_TINH")
                       && dsExcelData.Tables[TableName].Columns.Contains("DIA_CHI"))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi kiểm tra sự hợp lệ của file Excel:\n" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Tạo
        /// </summary>
        /// <returns></returns>
        DataSet CreateExcelMapping()
        {
            DataSet ds = new DataSet();
            DataTable dtSheetName = new DataTable("SheetName");
            dtSheetName.Columns.Add(new DataColumn("SName", typeof(string)));
            DataRow dr = dtSheetName.NewRow();
            dr["SName"] = "DS_BENHNHAN";
            dtSheetName.Rows.Add(dr);
            DataTable dt = new DataTable("Mapping");
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("STT",typeof(Int32)),
            new DataColumn("Standard_ColName",typeof(string)),
            new DataColumn("Excel_ColName",typeof(string))});
            //Add defaultRows
            AddRow(dt, 1, "Mã Bệnh nhân", "MA_BN");
            AddRow(dt, 2, "Tên Bệnh nhân", "TEN_BN");
            AddRow(dt, 3, "Ngày sinh", "NGAY_SINH");
            AddRow(dt, 4, "Giới tính", "GIOI_TINH");
            AddRow(dt, 5, "Địa chỉ", "DIA_CHI");

            ds.Tables.Add(dtSheetName);
            ds.Tables.Add(dt);
            return ds;
        }
        void AddRow(DataTable dt, int STT, string Standard_ColName, string Excel_ColName)
        {
            DataRow dr = dt.NewRow();
            dr["STT"] = STT;
            dr["Standard_ColName"] = Standard_ColName;
            dr["Excel_ColName"] = Excel_ColName;
            dt.Rows.Add(dr);
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (dsExcelData == null || dsExcelData.Tables.Count <= 0) return;
                foreach (DataRow dr in dsExcelData.Tables[0].Rows)
                {
                    dr["CHON"] = chkAll.Checked ? "T" : "F";
                }
                dsExcelData.Tables[0].AcceptChanges();
            }
            catch
            {
            }
        }

        private void chkReverse_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (dsExcelData == null || dsExcelData.Tables.Count <= 0) return;
                foreach (DataRow dr in dsExcelData.Tables[0].Rows)
                {
                    dr["CHON"] = (dr["CHON"] == null || dr["CHON"] == DBNull.Value || dr["CHON"].ToString() == "F") ? "T" : "F";
                }
                dsExcelData.Tables[0].AcceptChanges();
            }
            catch
            {
            }
        }

        private void txtExcelFilePath_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cmdRefresh.Enabled = txtExcelFilePath.Text.Trim() != "" && File.Exists(txtExcelFilePath.Text.Trim());
            }
            catch
            {
            }
        }
        private DataSet DataEntity = new DataSet();
        private DataTable PatientEntity = new PatientEntity.PatientEntityDataTable();
        private DataTable RegEntity = new RegEntity.RegEntityDataTable();
        private DataTable _RegDetailEntity = new RegDetailEntity.RegDetailEntityDataTable();
        private void cmdImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dsExcelData == null || dsExcelData.Tables.Count <= 0 || dsExcelData.Tables[0].Select("CHON='T'").Length <= 0) return;
                using (frm_LargeMsgBox MsgBox = new frm_LargeMsgBox(globalVariables.DisplayLanguage == "VN" ? "Xác nhận nhập dữ liệu vào DROC từ file " : "Import Data from Excel file Confirm", globalVariables.DisplayLanguage == "VN" ? "Bạn có muốn nhập các Bệnh nhân đang chọn trên lưới vào hệ thống dữ liệu DROC hay không?" : "Do you want to import selected data on the Grid into DROC DataBase?", globalVariables.DisplayLanguage == "VN" ? "ĐỒNG Ý" : "ACCEPT", globalVariables.DisplayLanguage == "VN" ? "HỦY BỎ" : "CANCEL"))
                {
                    if (MsgBox.ShowDialog() == DialogResult.OK)
                    {
                        prgImportExcel.Visible = true;
                        prgImportExcel.Minimum = 0;
                        prgImportExcel.Maximum = dsExcelData.Tables[0].Rows.Count;
                        prgImportExcel.Value = 0;
                        foreach (DataRow drExcel in dsExcelData.Tables[0].Rows)
                        {
                            prgImportExcel.Value += 1;
                            if (drExcel["CHON"].ToString() == "T")
                            {
                                try
                                {

                                    //Gán RoomEntity vào DataEntity
                                    Utility.ResetEntity(ref DataEntity);
                                    Utility.ResetEntity(ref PatientEntity);

                                    Utility.ResetEntity(ref RegEntity);
                                    //Create new Row
                                    DataRow dr0 = PatientEntity.NewRow();
                                    dr0["Patient_ID"] = -1;
                                    dr0["PATIENT_CODE"] = Utility.sDbnull(drExcel["MA_BN"], "").Trim() == "" ? Utility.GetYYYYMMDDHHMMSS(DateTime.Now) : drExcel["MA_BN"].ToString();
                                    dr0["Patient_Name"] = Utility.GetValue(Utility.sDbnull(drExcel["TEN_BN"], "No Excel Name"), false);
                                    //Ngày tháng năm sinh
                                    if (Utility.sDbnull(drExcel["NGAY_SINH"], "").Trim().Length > 4 && Utility.sDbnull(drExcel["NGAY_SINH"], "").Contains("/"))
                                    {
                                        string[] birthdateOfPatient = Utility.sDbnull(drExcel["NGAY_SINH"], DateTime.Now.ToString("dd/MM/yyyy")).Split('/');
                                        dr0["Birth_Date"] = new DateTime(Convert.ToInt32(birthdateOfPatient[2]), Convert.ToInt32(birthdateOfPatient[1]), Convert.ToInt32(birthdateOfPatient[0]));
                                        dr0["sBirth_Date"] = Utility.sDbnull(drExcel["NGAY_SINH"], DateTime.Now.ToString("dd/MM/yyyy"));

                                        dr0["Age"] = DateTime.Now.AddYears(-1 * Convert.ToInt32(birthdateOfPatient[2])).Year;
                                    }
                                    //Năm sinh
                                    else if (Utility.sDbnull(drExcel["NGAY_SINH"], "").Trim().Length == 4 && !Utility.sDbnull(drExcel["NGAY_SINH"], "").Contains("/"))
                                    {
                                        dr0["Birth_Date"] = new DateTime(Convert.ToInt32(Utility.sDbnull(drExcel["NGAY_SINH"], "0")), 1, 1);
                                        dr0["sBirth_Date"] = new DateTime(Convert.ToInt32(Utility.sDbnull(drExcel["NGAY_SINH"], "0")), 1, 1).ToString("dd/MM/yyyy");

                                        dr0["Age"] = DateTime.Now.AddYears(-1 * Convert.ToInt32(Convert.ToInt32(Utility.sDbnull(drExcel["NGAY_SINH"], "0")))).Year;
                                    }
                                    else//Tuổi
                                    {
                                        dr0["Age"] = Convert.ToInt32(Convert.ToInt32(Utility.sDbnull(drExcel["NGAY_SINH"], "0")));
                                        dr0["Birth_Date"] = new DateTime(DateTime.Now.AddYears(-1 * Convert.ToInt32(dr0["Age"])).Year, 1, 1);
                                        dr0["sBirth_Date"] = new DateTime(DateTime.Now.AddYears(-1 * Convert.ToInt32(dr0["Age"])).Year, 1, 1).ToString("dd/MM/yyyy");

                                    }
                                    dr0["Sex"] = GioiTinh(drExcel["GIOI_TINH"].ToString().ToUpper());
                                    dr0["EMERGENCY"] = 0;

                                    PatientEntity.Rows.Add(dr0);
                                    PatientEntity.AcceptChanges();
                                    //Tao thong tin dang ky chup
                                    DataRow dr1 = RegEntity.NewRow();
                                    dr1["REG_ID"] = -1;

                                    dr1["REG_NUMBER"] = dr0["PATIENT_CODE"];
                                    dr1["PATIENT_ID"] = dr0["Patient_ID"];
                                    dr1["DESC"] = "";

                                    dr1["PROCEDURELIST"] = "";
                                    dr1["PHYSICIAN"] = "";
                                    dr1["CREATED_DATE"] = DateTime.Now;

                                    RegEntity.Rows.Add(dr1);
                                    RegEntity.AcceptChanges();


                                    DataEntity.Tables.Add(PatientEntity);
                                    DataEntity.Tables.Add(RegEntity);
                                    WLRules _BusRule = new WLRules();

                                    //Gọi nghiệp vụ Insert dữ liệu
                                    PatientInfor _PatientInfor = new PatientInfor();
                                    RegInfor _RegInfor = new RegInfor();
                                    ActionResult InsertResult = _BusRule.Insert(DataEntity, _PatientInfor, _RegInfor);
                                    if (InsertResult == ActionResult.Success)//Nếu thành công
                                    {

                                        //Thêm mới một dòng vào Datasource để cập nhật lại dữ liệu trên DataGridView
                                        //phải đảm bảo Datasource và RoomEntity có cấu trúc giống nhau mới dùng hàm này
                                        DataRow newRow = m_dtWLDataSource.NewRow();
                                        newRow["Patient_ID"] = _PatientInfor.Patient_ID;
                                        newRow["PATIENT_CODE"] = dr0["PATIENT_CODE"];
                                        newRow["Patient_Name"] = dr0["Patient_Name"];
                                        newRow["Birth_Date"] = dr0["Birth_Date"];
                                        newRow["sBirth_Date"] = dr0["sBirth_Date"];
                                        newRow["Sex"] = dr0["Sex"];
                                        newRow["Age"] = dr0["Age"];
                                        newRow["EMERGENCY"] = 0;
                                        newRow["REG_ID"] = _RegInfor.REG_ID;
                                        newRow["REG_NUMBER"] = dr1["REG_NUMBER"];
                                        //newRow["DESC"] = Utility.GetValue(txtDesc.Text, false);
                                        newRow["PROCEDURELIST"] = "";
                                        newRow["PHYSICIAN"] = "";
                                        newRow["Doctor_Name"] = "";
                                        newRow["CREATED_DATE"] = DateTime.Now;
                                        newRow["REGSTATUS"] = 0;
                                        newRow["sCREATED_DATE"] = DateTime.Now.ToString("dd/MM/yyyy");
                                        newRow["SEX_NAME"] = drExcel["GIOI_TINH"].ToString();
                                        newRow["CanDel"] = 1;
                                        newRow["HasProcessed"] = 0;
                                        newRow["NoneProcessed"] = 0;
                                        newRow["TotalProc"] = 0;

                                        if (newRow != null)//99.99% là sẽ !=null
                                        {
                                            m_dtWLDataSource.Rows.Add(newRow);
                                            m_dtWLDataSource.AcceptChanges();
                                        }





                                    }
                                    else//Có lỗi xảy ra
                                    {

                                        switch (InsertResult)
                                        {
                                            case ActionResult.ExistedRecord:
                                                Utility.ShowMsg("Đã tồn tại Bệnh nhân có mã: " + _PatientInfor.Patient_Code + ". Đề nghị bạn xem lại", "Thông báo");

                                                break;
                                            default:
                                                mdlStatic.SetMsg(lblMsg, "Lỗi trong quá trình thêm mới Bệnh nhân. Liên hệ với VBIT", true);
                                                break;
                                        }
                                    }
                                }
                                catch
                                {
                                    prgImportExcel.Visible = false;
                                }
                            }
                            prgImportExcel.Visible = false;
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                prgImportExcel.Visible = false;
            }
        }

        int GioiTinh(string sexName)
        {
            switch (sexName)
            {
                case "NAM":
                case "MALE":
                    return 0;
                case "NỮ":
                case "FEMALE":
                    return 1;


            }
            return 0;

        }
        #endregion

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            AboutBox1 frmAbout = new AboutBox1();
            frmAbout.ShowDialog();
        }



        private void lblSaveAfterSearching_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblSaveAfterSearching.ImageIndex == 1)
                    lblSaveAfterSearching.ImageIndex = 0;
                else
                    lblSaveAfterSearching.ImageIndex = 1;
            }
            catch
            {
            }
        }

        private void label45_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblSearchWorkList.ImageIndex == 1)
                {
                    nmrSecond.Enabled = true;
                    lblSearchWorkList.ImageIndex = 0;
                }
                else
                {
                    nmrSecond.Enabled = false;
                    lblSearchWorkList.ImageIndex = 1;
                }
            }
            catch
            {
            }
        }





        private void lblAutoDcmConverter_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (lblAutoDcmConverter.ImageIndex == 1)
                {
                    IsUsingDicomConverter = true;
                    lblAutoDcmConverter.ImageIndex = 0;
                }
                else
                {
                    IsUsingDicomConverter = false;
                    lblAutoDcmConverter.ImageIndex = 1;
                }
            }
            catch
            {
            }
        }

        private void lblAppMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblAppMode.ImageIndex == 1)
                {
                    cmdReady.Enabled = true;

                    _AppMode = AppType.AppEnum.AppMode.Demo;
                    lblAppMode.ImageIndex = 0;
                }
                else
                {
                    cmdReady.Enabled = false;
                    _AppMode = AppType.AppEnum.AppMode.License;
                    lblAppMode.ImageIndex = 1;
                }
            }
            catch
            {
            }
            finally
            {
                //_DicomMedicalViewer.UpdateMode(_AppMode);
            }
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            TE_EnterWorkMode();
        }





        private void cmdConfig_Click(object sender, EventArgs e)
        {
            _currTab = AppType.AppEnum.TabMode.System;
            ChangeUI(3);
        }
        private void cmdExpand1_Click(object sender, EventArgs e)
        {
            if (pnlAct.Size.Width == 0)
            {
                cmdExpand1.Text = "<<";
                pnlAct.Size = new Size(116, pnlAct.Size.Height);
            }
            else
            {
                cmdExpand1.Text = ">>";
                pnlAct.Size = new Size(0, pnlAct.Size.Height);
            }
        }

       

        int GetWidthOfCrop()
        {
            try
            {
                string fileName = Application.StartupPath + @"\CS.sze";
                if (File.Exists(fileName))
                {
                    using (StreamReader _reader = new StreamReader(fileName))
                    {
                        object obj = _reader.ReadLine();
                        if (obj == null || !Utility.IsNumeric(obj)) return 10;
                        return Utility.Int32Dbnull(obj, 10);
                    }
                }
                return 10;
            }
            catch
            {
                return 10;
            }
        }

        
        public void try2ResetScheduledStatus(Hashtable htbDetail)
        {
            try
            {
                foreach (ScheduledControl _ScheduledControl in pnlScheduled.Controls)
                {
                    if (htbDetail.Contains(_ScheduledControl.DETAIL_ID))
                    {
                        _ScheduledControl.PrintCount = Convert.ToInt32(htbDetail[_ScheduledControl.DETAIL_ID]);
                        _ScheduledControl.ResetPrintCount();
                    }
                }
            }
            catch
            {
            }
        }
        
        private void cmdOrgirinalImg_Acq_Click(object sender, EventArgs e)
        {
            IsLoadOriginalImage = true;
            _IAr = BeginInvoke(new DisplayImg(ViewImg));
        }

       
        private void cmdFullScreen_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmdFullScreen.Tag.ToString() == "F")
                {
                    cmdFullScreen.Tag = "N";
                    cmdFullScreen.Image = DROCLibs.Properties.Resources.window_minimize;
                    _FullScreen.ShowFullScreen();
                }
                else
                {
                    cmdFullScreen.Tag = "F";
                    cmdFullScreen.Image = DROCLibs.Properties.Resources.window_maximize;
                    _FullScreen.ShowFullScreen();
                    _FullScreen.ResetTaskBar();
                }
            }
            catch
            {
            }
        }
        bool IsLoadOriginalImage = false;
        bool IsGenDcmFromRaw = false;
       

        private void cmd_Click(object sender, EventArgs e)
        {
            try
            {
                frm_UpdateHosInfor _update = new frm_UpdateHosInfor(this);
                if (_update.ShowDialog() == DialogResult.OK)
                {
                    HospitalName = _update._HosName;
                    DepartmentName = _update._DepartName;
                }
            }
            catch
            {
            }
        }
        /// <summary>
       
        private void cmdBrightAndContrast_Click_2(object sender, EventArgs e)
        {

        }

        private void cmdUpKvp_Click(object sender, EventArgs e)
        {
            IncreseKvp();
        }

        private void cmdDownKvp_Click(object sender, EventArgs e)
        {
            DecreseKvp();
        }
        private void IncreseKvp()
        {
            try
            {
                decimal _CurrValue = Convert.ToDecimal(lblKvpVal.Text.Trim());
                if (_CurrValue + StepKvp <= MaxKvp)
                    _CurrValue = _CurrValue + StepKvp;
                else
                    _CurrValue = MaxKvp;
                cmdUpKvp.Enabled = _CurrValue < MaxKvp;
                cmdDownKvp.Enabled = _CurrValue > MinKvp;
                kVp = _CurrValue;
                SetValueNumeric(lblKvpVal, kVp);
            }
            catch
            {
            }
        }

        private void DecreseKvp()
        {
            try
            {
                decimal _CurrValue = Convert.ToDecimal(lblKvpVal.Text.Trim());
                if (_CurrValue - StepKvp >= MinKvp)
                    _CurrValue = _CurrValue - StepKvp;
                else
                    _CurrValue = MinKvp;
                cmdUpKvp.Enabled = _CurrValue < MaxKvp;
                cmdDownKvp.Enabled = _CurrValue > MinKvp;
                kVp = _CurrValue;
                SetValueNumeric(lblKvpVal, kVp);
            }
            catch
            {
            }
        }

        private void cmdUpmA_Click(object sender, EventArgs e)
        {
            IncresemA();
        }

        private void cmdDownmA_Click(object sender, EventArgs e)
        {
            DecresemA();
        }
        private void IncresemA()
        {
            try
            {
                int _CurrValue = Convert.ToInt32(lblmAVal.Text.Trim());
                if (_CurrValue + StepmA <= MaxmA)
                    _CurrValue = _CurrValue + StepmA;
                else
                    _CurrValue = MaxmA;
                cmdUpmA.Enabled = _CurrValue < MaxmA;
                cmdDownmA.Enabled = _CurrValue > MinmA;
                mA = _CurrValue;
                SetValueNumeric(lblmAVal, mA);
            }
            catch
            {
            }
        }

        private void DecresemA()
        {
            try
            {
                int _CurrValue = Convert.ToInt32(lblmAVal.Text.Trim());
                if (_CurrValue - StepmA >= MinmA)
                    _CurrValue = _CurrValue - StepmA;
                else
                    _CurrValue = MinmA;
                cmdUpmA.Enabled = _CurrValue < MaxmA;
                cmdDownmA.Enabled = _CurrValue > MinmA;
                mA = _CurrValue;
                SetValueNumeric(lblmAVal, mA);
            }
            catch
            {
            }
        }
        private void cmdUpmAs_Click(object sender, EventArgs e)
        {
            IncresemAs();
        }

        private void cmdDownmAs_Click(object sender, EventArgs e)
        {
            DecresemAs();
        }
        private void IncresemAs()
        {
            try
            {
                int _CurrValue = Convert.ToInt32(lblmAsVal.Text.Trim());
                if (_CurrValue + StepmAs <= MaxmAs)
                    _CurrValue = _CurrValue + StepmAs;
                else
                    _CurrValue = MaxmAs;
                cmdUpmAs.Enabled = _CurrValue < MaxmAs;
                cmdDownmAs.Enabled = _CurrValue > MinmAs;
                mAs = _CurrValue;
                SetValueNumeric(lblmAsVal, mAs);
            }
            catch
            {
            }
        }

        private void DecresemAs()
        {
            try
            {
                int _CurrValue = Convert.ToInt32(lblmAsVal.Text.Trim());
                if (_CurrValue - StepmAs >= MinmAs)
                    _CurrValue = _CurrValue - StepmAs;
                else
                    _CurrValue = MinmAs;
                cmdUpmAs.Enabled = _CurrValue < MaxmAs;
                cmdDownmAs.Enabled = _CurrValue > MinmAs;
                mAs = _CurrValue;
                SetValueNumeric(lblmAsVal, mAs);
            }
            catch
            {
            }
        }

       

        private void cmdCreateDcmfromRaw_Click(object sender, EventArgs e)
        {
            ReProcessing();
        }
        void ReProcessing()
        {
            try
            {
                if (GetSelectedScheduled().Status <= 0) return;
                if (new frm_LargeMsgBox("XÁC NHẬN XỬ LÝ LẠI ẢNH TỪ ẢNH GỐC", "Bạn có chắc chắn muốn tạo lại ảnh DICOM cho dịch vụ đang chọn của Bệnh nhân này từ ảnh gốc(ảnh thô) hay không?", "ĐỒNG Ý", "KHÔNG ĐỒNG Ý").ShowDialog() != DialogResult.OK) return;
                IsLoadOriginalImage = true;
                IsUsingDicomConverter = true;
                IsGenDcmFromRaw = true;
               
                _IAr = BeginInvoke(new DisplayImg(ViewImg));
            }
            catch
            {
            }
        }
        void ReProcessingWithoutAsking()
        {
            try
            {
                if (GetSelectedScheduled().Status <= 0) return;
                IsLoadOriginalImage = true;
                IsUsingDicomConverter = true;
                IsGenDcmFromRaw = true;
                _IAr = BeginInvoke(new DisplayImg(ViewImg));
            }
            catch
            {
            }
        }
       

       
        private string strSettingDeviceError = Application.StartupPath + @"\DROCConfigs\DeviceErrConfig.txt";
        private string strFPDModeConfig = Application.StartupPath + @"\DROCConfigs\FPDMode.mod";

        void SaveFPDModeSettings()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(strFPDModeConfig))
                {
                    writer.WriteLine("FPDMode:" + (chkChay1Tam.ImageIndex == 0 ? 0 : (chk2tamgiongnhau.ImageIndex == 0 ? 1 : 2)).ToString());
                    writer.WriteLine("PANEL1:" + cboFPD1.SelectedValue.ToString());
                    writer.WriteLine("PANEL2:" + cboFPD2.SelectedValue.ToString());
                    writer.WriteLine("PANEL1 NAME:" + cmdPanel1.Text.ToString());
                    writer.WriteLine("PANEL2 NAME:" + cmdPanel2.Text.ToString());
                    writer.Flush();
                    writer.Close();
                }
            }
            catch
            {
            }
            finally
            {
            }
            //try
            //{
            //    using (StreamWriter writer = new StreamWriter(strFPDModeConfig))
            //    {
            //        writer.WriteLine("FPDMode:"+cboFPDMode.SelectedIndex.ToString());
            //        writer.WriteLine("PANEL1:"+cboFPD1.SelectedValue.ToString());
            //        writer.WriteLine("PANEL2:" + cboFPD2.SelectedValue.ToString());
            //        writer.Flush();
            //        writer.Close();
            //    }
            //}
            //catch
            //{
            //}
            //finally
            //{
            //}

        }

        void SaveSettingsDeviceError()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(strSettingDeviceError))
                {
                   
                    writer.WriteLine(lblDisplayOptions.ImageIndex == 0 ? 1 : 0);
                   
                    writer.WriteLine(lblGridMode.ImageIndex == 0 ? 1 : 0);
                   
                    writer.WriteLine(chkSaveReject.ImageIndex == 0 ? 1 : 0);
                   
                  
                    writer.WriteLine(chkAutoSend.ImageIndex == 0 ? 1 : 0);
                  
                    writer.WriteLine(chkAutoGenPID.ImageIndex == 0 ? 1 : 0);
                    writer.WriteLine(lblGCOM_VN.ImageIndex == 0 ? 1 : 0);

                    writer.Flush();
                    writer.Close();
                }
            }
            catch
            {
            }
            finally
            {
                isGCOM_VN = lblGCOM_VN.ImageIndex == 0;
                chkDynamicGrid.Visible = lblGridMode.ImageIndex == 1;
            }

        }


        private void chkAutoVFlip_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoVFlip1.ImageIndex == 1)
                {

                    chkAutoVFlip1.ImageIndex = 0;
                }
                else
                {

                    chkAutoVFlip1.ImageIndex = 1;
                }
            }
            catch
            {
            }
            finally
            {
                //SaveSettingsDeviceError();
            }
        }

        private void chkAutoHFlip_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoHFlip1.ImageIndex == 1)
                {

                    chkAutoHFlip1.ImageIndex = 0;
                }
                else
                {

                    chkAutoHFlip1.ImageIndex = 1;
                }
            }
            catch
            {
            }
            finally
            {
                //SaveSettingsDeviceError();
            }
        }



        private void txtNumberOfRotate_TextChanged(object sender, EventArgs e)
        {

        }



        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void pnlImgViewer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblhint_Click(object sender, EventArgs e)
        {
            try
            {
                ScheduledControl _selectedItem = GetSelectedScheduled();
                if (_selectedItem != null)
                {
                    frm_ImgAlgorithsmList ImgConfigList = new frm_ImgAlgorithsmList();
                    if (ImgConfigList.ShowDialog() == DialogResult.OK)
                    {

                        IMGConfigName = ImgConfigList.ImgconfigName;
                        string _actResult = new ModalityController().Insert_IE_Device_Pos_Relation(m_intCurrDevice1, _selectedItem.A_Code, _selectedItem.P_Code, ImgConfigList.CurrentIE_ID).ToString();
                        new frm_LargeMsgBoxOK(MultiLanguage.GetText(globalVariables.DisplayLanguage, "THÔNG BÁO", "ACTION RESULT"), _actResult, "OK", "CANCEL").ShowDialog();
                        toolTip1.SetToolTip(_selectedItem._AnatomyObject, _selectedItem.A_Code + "/" + _selectedItem.P_Code + MultiLanguage.GetText(globalVariables.DisplayLanguage, " đang sử dụng cấu hình xử lý ảnh " + IMGConfigName, " is using image processing configuration: " + IMGConfigName) + " - RAW file: " + RAWFileNameWillbeCreated + "; kVp=" + kVp.ToString() + " mA=" + mA.ToString() + " ms=" + mAs.ToString());
                        LoadIEConfig(m_intCurrDevice1, _selectedItem.A_Code, _selectedItem.P_Code);
                    }
                }
            }
            catch
            {
            }
        }

        private void lblFromDate_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblFromDate.ImageIndex == 1)
                    lblFromDate.ImageIndex = 0;
                else
                    lblFromDate.ImageIndex = 1;
            }
            catch
            {
            }
        }

        private void lblGCOM_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblGCOM.ImageIndex == 1)
                    lblGCOM.ImageIndex = 0;
                else
                    lblGCOM.ImageIndex = 1;
            }
            catch
            {
            }
        }

        private void lblFromDateST_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblFromDateST.ImageIndex == 1)
                    lblFromDateST.ImageIndex = 0;
                else
                    lblFromDateST.ImageIndex = 1;
            }
            catch
            {
            }
        }

        private void tabPageHardware_Click(object sender, EventArgs e)
        {

        }

        void AutomaticDetect3Params()
        {
            try
            {
                ScheduledControl _selectedItem = GetSelectedScheduled();
                if (_selectedItem == null)
                {
                    return;
                }

                DataSet ds = new DoctorController().GetAPParams(_selectedItem.A_Code, _selectedItem.P_Code, BODYSIZE_CODE);
                if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                {
                    return;
                }
                DataRow dr = ds.Tables[0].Rows[0];
                kVp = Utility.DecimaltoDbnull(dr["kVp"], MinKvp);
                mA = Utility.Int32Dbnull(dr["mA"], MinmA);
                mAs = Utility.Int32Dbnull(dr["mAs"], MinmAs);
                if (kVp < MinKvp) kVp = MinKvp;
                if (kVp > MaxKvp) kVp = MaxKvp;
                if (mA < MinmA) mA = MinmA;
                if (mA > MaxmA) mA = MaxmA;
                if (mAs < MinmAs) mAs = MinmAs;
                if (mAs > MaxmAs) mAs = MaxmAs;
                if (!isGCOM_VN)
                {
                    SetValueNumeric(lblKvpVal, kVp);
                    SetValueNumeric(lblmAVal, mA);
                    SetValueNumeric(lblmAsVal, mAs);
                }
                else
                {
                    SetValueNumeric(lblAMEC_KvpVal, kVp);
                    SetValueNumeric(lblAMEC_mAVal, mA);
                    SetValueNumeric(lblAMEC_timeVal, mAs);
                }
            }
            catch
            {
            }
        }
       
        private void cmdSmall_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmdSmall.Tag.ToString() == "P")
                    return;
                else
                {
                    BODYSIZE_CODE = "S";
                    cmdSmall.Tag = "P";
                    cmdSmall.BackColor = Color.Orange;
                    cmdMedium.BackColor = Color.WhiteSmoke;
                    cmdLarge.BackColor = Color.WhiteSmoke;
                    cmdXLarge.BackColor = Color.WhiteSmoke;

                    cmdMedium.Tag = "N";
                    cmdLarge.Tag = "N";
                    cmdXLarge.Tag = "N";
                }
            }
            catch
            {
            }
            finally
            {
                AutomaticDetect3Params();
            }
        }

        private void cmdMedium_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmdMedium.Tag.ToString() == "P")
                    return;
                else
                {
                    BODYSIZE_CODE = "M";
                    cmdMedium.Tag = "P";
                    cmdMedium.BackColor = Color.Orange;
                    cmdSmall.BackColor = Color.WhiteSmoke;
                    cmdLarge.BackColor = Color.WhiteSmoke;
                    cmdXLarge.BackColor = Color.WhiteSmoke;

                    cmdSmall.Tag = "N";
                    cmdLarge.Tag = "N";
                    cmdXLarge.Tag = "N";
                }
            }
            catch
            {
            }
            finally
            {
                AutomaticDetect3Params();
            }
        }

        private void cmdLarge_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmdLarge.Tag.ToString() == "P")
                    return;
                else
                {
                    BODYSIZE_CODE = "L";
                    cmdLarge.Tag = "P";
                    cmdLarge.BackColor = Color.Orange;
                    cmdSmall.BackColor = Color.WhiteSmoke;
                    cmdMedium.BackColor = Color.WhiteSmoke;
                    cmdXLarge.BackColor = Color.WhiteSmoke;

                    cmdSmall.Tag = "N";
                    cmdMedium.Tag = "N";
                    cmdXLarge.Tag = "N";
                }
            }
            catch
            {
            }
            finally
            {
                AutomaticDetect3Params();
            }
        }

        private void cmdXLarge_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmdXLarge.Tag.ToString() == "P")
                    return;
                else
                {
                    BODYSIZE_CODE = "XL";
                    cmdXLarge.Tag = "P";
                    cmdXLarge.BackColor = Color.Orange;
                    cmdSmall.BackColor = Color.WhiteSmoke;
                    cmdMedium.BackColor = Color.WhiteSmoke;
                    cmdLarge.BackColor = Color.WhiteSmoke;

                    cmdSmall.Tag = "N";
                    cmdMedium.Tag = "N";
                    cmdLarge.Tag = "N";
                }
            }
            catch
            {
            }
            finally
            {
                AutomaticDetect3Params();
            }
        }
        private string strSettingParam = Application.StartupPath + @"\DROCConfigs\HardwareParamConfig.txt";
        void SaveSettingsParams()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(strSettingParam))
                {
                    writer.WriteLine(nmrMinKvp.Value.ToString());
                    writer.WriteLine(nmrMaxKvp.Value.ToString());
                    writer.WriteLine(nmrStepKvp.Value.ToString());

                    writer.WriteLine(nmrMinmA.Value.ToString());
                    writer.WriteLine(nmrMaxmA.Value.ToString());
                    writer.WriteLine(nmrStepmA.Value.ToString());

                    writer.WriteLine(nmrMinms.Value.ToString());
                    writer.WriteLine(nmrMaxms.Value.ToString());
                    writer.WriteLine(nmrStepms.Value.ToString());

                    writer.Flush();
                    writer.Close();
                }
            }
            catch
            {
            }

        }
        private void cmdApplyParam_Click(object sender, EventArgs e)
        {
            SaveSettingsParams();
            LoadParamSettings();
        }

        private void cmdExpand1_Click_1(object sender, EventArgs e)
        {

        }

        private void txtNumberOfRotate_90_TextChanged(object sender, EventArgs e)
        {

        }

     

        private void lblResetGCom_Click(object sender, EventArgs e)
        {
            try
            {
                cmdReady.Enabled = true;
                cmdReady.Tag = "R";
                cmd.Text = MultiLanguage.GetText(globalVariables.DisplayLanguage, "Sẵn sàng", "Ready");
                pnlScheduled.Enabled = cmdReady.Enabled;
            }
            catch
            {
            }
        }

      
       
      


        private void cmdCloseCellConfig_Click(object sender, EventArgs e)
        {
            // pnlCellConfig.Size = new Size(769, 0);
        }


        private void cmdApply_Click(object sender, EventArgs e)
        {

            try
            {
                TimeOfResendingDataFrame = (int)nmrTimeofDataresending.Value;
                Time2ResendingDataFrame = Convert.ToInt32(nmrTime2Dataresending.Value) * 1000;

                TimeOfResendingEXPCmd = (int)nmrTimeofEXP.Value;
                Time2ResendingEXPCmd = Convert.ToInt32(nmrTime2Exp.Value) * 1000;
                SaveConfig();
            }
            catch
            {
            }
            finally
            {

            }
        }
        private string strConfig = Application.StartupPath + @"\Config.txt";
        void SaveConfig()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(strConfig))
                {
                    writer.WriteLine(TimeOfResendingDataFrame.ToString());
                    writer.WriteLine(Convert.ToInt32(nmrTime2Dataresending.Value).ToString());
                    writer.WriteLine(TimeOfResendingEXPCmd.ToString());
                    writer.WriteLine(Convert.ToInt32(nmrTime2Exp.Value).ToString());
                    writer.Flush();
                    writer.Close();

                }
            }
            catch
            {
            }
            finally
            {
                // pnlCellConfig.Size = new Size(769, 0);
            }

        }

        void LoadTimeConfig()
        {

            try
            {

                if (!File.Exists(strConfig))
                {
                    TimeOfResendingDataFrame = 5;
                    Time2ResendingDataFrame = 5000;

                    TimeOfResendingEXPCmd = 5;
                    Time2ResendingEXPCmd = 5000;
                    return;
                }
                else
                {
                    using (StreamReader _reader = new StreamReader(strConfig))
                    {
                        string obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            nmrTimeofDataresending.Value = Convert.ToDecimal(obj);
                            TimeOfResendingDataFrame = (int)nmrTimeofDataresending.Value;

                        }
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            nmrTime2Dataresending.Value = Convert.ToDecimal(obj);

                            Time2ResendingDataFrame = Convert.ToInt32(nmrTime2Dataresending.Value) * 1000;

                        }
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            nmrTimeofEXP.Value = Convert.ToDecimal(obj);


                            TimeOfResendingEXPCmd = (int)nmrTimeofEXP.Value;

                        }
                        obj = _reader.ReadLine();
                        if (obj != null)
                        {
                            nmrTime2Exp.Value = Convert.ToDecimal(obj);
                            Time2ResendingEXPCmd = Convert.ToInt32(nmrTime2Exp.Value) * 1000;
                        }

                    }
                }
            }
            catch
            {
            }
            finally
            {

            }


        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblFPDStatus_Click(object sender, EventArgs e)
        {

        }

        private void txtSearchName1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpToDate1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmdDirectDicomPrint_Click(object sender, EventArgs e)
        {
           
        }

        private void lblMemory_Click(object sender, EventArgs e)
        {
           
        }

       

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

       

       
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                ScheduledControl _ScheduledControl = GetSelectedScheduled();
                if (_ScheduledControl != null)
                    ShortCut2AddProc(_ScheduledControl.A_Code, _ScheduledControl.P_Code, true, ref _newDetailID);
            }
            catch
            {
            }
        }

        private void ToolStripMenuItemReProcessing_Click(object sender, EventArgs e)
        {
            ReProcessing();
        }

        private void lblDisplayOptions_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblDisplayOptions.ImageIndex == 1)
                {

                    lblDisplayOptions.ImageIndex = 0;
                }
                else
                {

                    lblDisplayOptions.ImageIndex = 1;
                }
            }
            catch
            {
            }
            finally
            {
                SaveSettingsDeviceError();
            }
        }

        private void mnuCHESTPA_Click(object sender, EventArgs e)
        {
            try
            {
                frm_QuickRegistration newForm = new frm_QuickRegistration(m_intCurrDevice1);
                newForm.ImgPath = txtImgDir.Text.Trim();
                newForm.WLDataSource = m_dtWLDataSource;
                newForm.grdList = grdWorkList;
                newForm.Act = action.Insert;
                newForm.AutoGenPID = chkAutoGenPID.ImageIndex == 0;
                newForm.ShowDialog();
                SetSuspendingInfo();
                if (newForm.IsBeginExam)
                {
                    ModifyWorkListButtons();
                    BeginExam();
                    Last_Anatomy = "CHEST";
                    Last_Projection = "PA";
                    ShortCut2AddProc(Last_Anatomy, Last_Projection, true, ref _newDetailID);
                }
            }
            catch
            {
            }
        }

        private void mnuCHESTAP_Click(object sender, EventArgs e)
        {
            frm_QuickRegistration newForm = new frm_QuickRegistration(m_intCurrDevice1);
            newForm.WLDataSource = m_dtWLDataSource;
            newForm.grdList = grdWorkList;
            newForm.Act = action.Insert;
            newForm.ShowDialog();
            SetSuspendingInfo();
            if (newForm.IsBeginExam)
            {
                ModifyWorkListButtons();
                BeginExam();
                Last_Anatomy = "CHEST";
                Last_Projection = "AP";
                ShortCut2AddProc(Last_Anatomy, Last_Projection, true, ref _newDetailID);
            }
        }

        private void txtID2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void mnuAdomentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frm_QuickRegistration newForm = new frm_QuickRegistration(m_intCurrDevice1);
                newForm.WLDataSource = m_dtWLDataSource;
                newForm.grdList = grdWorkList;
                newForm.Act = action.Insert;
                newForm.ShowDialog();
                SetSuspendingInfo();
                if (newForm.IsBeginExam)
                {
                    ModifyWorkListButtons();
                    BeginExam();
                    Last_Anatomy = "ABDOMENT";
                    Last_Projection = "KUB";
                    ShortCut2AddProc(Last_Anatomy, Last_Projection, true, ref _newDetailID);
                }
            }
            catch
            {
            }
        }

        private void label13_Click_1(object sender, EventArgs e)
        {
            m_bSaveImgFile = !m_bSaveImgFile;
        }

        private void pnlAcqImgTools_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog _SaveFileDialog = new SaveFileDialog();
                if (_SaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dt = dailyReport();
                    if (dt != null && dt.Columns.Count > 0)
                        ExportExcel.exportToExcel(dt, _SaveFileDialog.FileName, "BACH_MAI");
                    else
                        Utility.ShowMsg("Không tìm thấy dữ liệu trong ngày");
                }
            }
            catch
            {
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private DataTable dailyReport()
        {
            DataTable dt = new DataTable();
            try
            {

                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter DA = new OleDbDataAdapter(cmd);
                string PCode = txtSearchPCode.Text.Trim() == "" ? "NOTHING" : txtSearchPCode.Text.Trim();
                string PName = txtSearchPName.Text.Trim() == "" ? "NOTHING" : txtSearchPName.Text.Trim();
                Int16 pSex = Convert.ToInt16(cboSearchSex.SelectedIndex);
                if (pSex <= 0) pSex = 100;

                Int64 RegNumber = txtRegID.Text.Trim() == "" ? -1 : Convert.ToInt64(txtRegID.Text.Trim());
                string RegFrom = lblFromDate.ImageIndex == 0 ? dtpFromDate.Text : "1/1/1900";
                string RegTo = lblFromDate.ImageIndex == 0 ? dtpToDate.Text : "1/1/1900";

                {
                    cmd.Connection = globalVariables.OleDbConnection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DailyReport";
                    cmd.Parameters.AddWithValue("@Patient_Code", PCode).Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@Patient_Name", PName).Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@regFrom", RegFrom).Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@regTo", RegTo).Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@Sex", pSex).Direction = ParameterDirection.Input;
                    //cmd.Parameters.AddWithValue("@REG_NUMBER", RegNumber).Direction = ParameterDirection.Input;

                }
                DataSet ds = new DataSet();
                DA.Fill(ds);
                if (ds == null || ds.Tables.Count <= 0) return dt;
                return ds.Tables[0];

            }
            catch (Exception ex)
            {
                mdlStatic.SetMsg(lblMsg, ex.Message, true);
                return dt;
            }

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void lstLog_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdReport_Click(object sender, EventArgs e)
        {
            ShowReport();
        }


        private DataSet GetData4Report()
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter DA = new OleDbDataAdapter(cmd);
                string FILMSIZE_ID = cboFSize.SelectedIndex <= 0 ? "NOTHING" : cboFSize.Text;

                string RegFrom = lblReportDate.ImageIndex == 0 ? dtpReportFrom.Text : "1/1/1900";
                string RegTo = lblReportDate.ImageIndex == 0 ? dtpReportTo.Text : "1/1/1900";
                string sql = "SELECT format(R.CREATED_DATE,'dd/MM/yyyy') AS Created_Date, R.PATIENT_ID, B.PRINT_ID, RD.DETAIL_ID, A.FILMSIZE_ID,(select top 1 FILM_HD from L_AP_Params where ANATOMY_CODE=RD.ANATOMY_CODE AND PROJECTION_CODE=RD.PROJECTION_CODE AND FILM_HD <> null) as FILM_HD";
                sql += " FROM L_REGS AS R, L_REGDETAILS AS RD, T_PRINT_LOG_DETAIL AS B, T_PRINT_LOG AS A ";
                sql += " WHERE R.REG_ID=RD.REG_ID And RD.detail_ID=B.Detail_ID And A.PRINT_ID=B.PRINT_ID And (" + RegFrom + "='1/1/1900' Or R.CREATED_DATE Between CDate('" + RegFrom + "') And CDate('" + RegTo + "')) And (LTRIM(RTRIM('" + FILMSIZE_ID + "'))='NOTHING' Or A.FILMSIZE_ID=LTRIM(RTRIM('" + FILMSIZE_ID + "')))";
                DataSet ds = new DataSet();
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, sql);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
                mdlStatic.SetMsg(lblMsg, ex.Message, true);
            }

        }
        DataTable ProcessData4Report(DataSet dsdata)
        {
            try
            {
                DataTable dtData = new DataTable(dsdata.Tables[0].TableName);
                dtData.Columns.AddRange(new DataColumn[] { new DataColumn("Created_Date", typeof(string)), new DataColumn("FilmSize_8x10", typeof(Int32)), new DataColumn("Patient_ID", typeof(Int32)),
                new DataColumn("TotalPictures", typeof(Int32)),new DataColumn("FilmSize_10x12", typeof(Int32)),new DataColumn("TotalFilm", typeof(Int32))
                ,new DataColumn("TotalNoMerge", typeof(Int32)),new DataColumn("FILM_HD", typeof(Int32))});
                Hashtable patient = new Hashtable();
                foreach (DataRow dr in dsdata.Tables[0].Rows)
                {
                    dr["FILM_HD"] = Utility.Int32Dbnull(dr["FILM_HD"], 0);
                }
                dsdata.AcceptChanges();
                foreach (DataRow dr in dsdata.Tables[0].Rows)
                {
                    string date = dr["Created_Date"].ToString();
                    string patient_id = dr["Patient_ID"].ToString();
                    if (dtData.Select("Created_Date='" + date + "'").Length <= 0)
                    {
                        var lstHdon = dsdata.Tables[0].AsEnumerable().Where(c => c.Field<string>("Created_Date") == date);
                        DataRow newDr = dtData.NewRow();
                        newDr["Created_Date"] = dr["Created_Date"].ToString();
                        newDr["FilmSize_8x10"] = lstHdon.Where(c => c.Field<string>("FILMSIZE_ID") == "8INX10IN").Select(c1 => c1.Field<int>("PRINT_ID")).Distinct().Count();
                        newDr["FilmSize_10x12"] = lstHdon.Where(c => c.Field<string>("FILMSIZE_ID") == "10INX12IN").Select(c1 => c1.Field<int>("PRINT_ID")).Distinct().Count();
                        newDr["Patient_ID"] = lstHdon.Select(c => c.Field<int>("Patient_ID")).Distinct().Count();
                        newDr["TotalFilm"] = lstHdon.Select(c => c.Field<int>("PRINT_ID")).Distinct().Count();
                        newDr["TotalPictures"] = lstHdon.Select(c => c.Field<int>("Detail_ID")).Distinct().Count();
                        int NoFILM_HD = lstHdon.Where(c => c.Field<int>("FILM_HD") != 1).Select(c => c.Field<int>("PRINT_ID")).Distinct().Count();
                        newDr["FILM_HD"] = NoFILM_HD + Utility.DecimaltoDbnull(lstHdon.Where(c => c.Field<int>("FILM_HD") == Convert.ToInt16(1)).Select(c1 => c1.Field<int>("FILM_HD")).Sum(), 0);
                        dtData.Rows.Add(newDr);
                    }
                }
                dtData.AcceptChanges();
                return dtData;
            }
            catch
            {
                return null;
            }
        }

        void ShowReport()
        {
            DataSet dsData = GetData4Report();
            if (dsData == null) return;
            DataTable v_dtData = ProcessData4Report(dsData);
            if (v_dtData == null) return;
            crpt_filmReport crpt = new crpt_filmReport();//=  GetReportDocument(cboReportType.SelectedIndex,cboObjectType.SelectedIndex);
            try
            {
                //crpt.Load(Server.MapPath("reports/crpt_filmReport.rpt"));
                crpt.SetDataSource(v_dtData);
                // crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "        NGƯỜI LẬP                                       THỦ TRƯỞNG ĐƠN VỊ           ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                crpt.SetParameterValue("ParentBranchName", HospitalName);
                crpt.SetParameterValue("BranchName", DepartmentName);
                crpt.SetParameterValue("rptBottomContent", "DROC-Hệ thống thu nhận và xử lý ảnh-In lúc:" + DateTime.Now.ToString("dd/MM/yyyy 24HH:mm:ss") + " bởi " + globalVariables.UserName.Trim());
                //crpt.SetParameterValue("ReportCondition", GetReportCondition());
                //crpt.SetParameterValue("NTN", "Hà Nội, Ngày " + DateTime.Now.Day.ToString() + " tháng " + DateTime.Now.Month.ToString() + " năm " + DateTime.Now.Year.ToString());
                crptViewer.ReportSource = crpt;



                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                Utility.DefaultNow(this);
            }

        }
        private void lblReportDate_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblReportDate.ImageIndex == 1)
                    lblReportDate.ImageIndex = 0;
                else
                    lblReportDate.ImageIndex = 1;
            }
            catch
            {
            }
        }

      
      

        private void cmdSaveParam_Click(object sender, EventArgs e)
        {
            try
            {
                ScheduledControl _ScheduledControl = GetSelectedScheduled();
                if (_ScheduledControl != null)
                {
                    string errMsg = "";
                    ActionResult actResult = new DoctorController().InsertAPParam_Acq(_ScheduledControl.A_Code, _ScheduledControl.P_Code, BODYSIZE_CODE, lblKvpVal.Value, 0, (int)lblmAsVal.Value, "", m_intCurrDevice1, 0, 0, ref errMsg);
                    if (actResult == ActionResult.Success)
                    {
                        SetText(lblResult, "Save OK");
                    }
                    else
                        SetText(lblResult, "Save failed");
                }
            }
            catch
            {
            }
        }

        private void txtName2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnlAcqPatientInfor_Paint(object sender, PaintEventArgs e)
        {

        }

       
      
        private void lblProcess_Click(object sender, EventArgs e)
        {
           
        }

        private void txtbitsStored_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblGridMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblGridMode.ImageIndex == 1)
                {

                    lblGridMode.ImageIndex = 0;
                }
                else
                {

                    lblGridMode.ImageIndex = 1;
                }
            }
            catch
            {
            }
            finally
            {
                SaveSettingsDeviceError();
            }
        }

       

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label76_Click(object sender, EventArgs e)
        {

        }

        private void cmdPrintDirect_Click(object sender, EventArgs e)
        {
           
        }

       

        private void cmdResetData_Click(object sender, EventArgs e)
        {
            try
            {

                if (new frm_LargeMsgBox("Cảnh báo", "Bạn có thực sự muốn khởi tạo lại dữ liệu cho đơn vị này?", "Tôi đồng ý", "Hủy bỏ").ShowDialog() == DialogResult.OK)
                {
                    ActionResult _ActionResult = resetData();
                    if (_ActionResult != ActionResult.Success)
                        new frm_LargeMsgBoxOK("Cảnh báo", "Khởi động lại dữ liệu không thành công: " + _ActionResult.ToString(), "Đã rõ", "Chưa hiểu").ShowDialog();
                }
            }
            catch
            {
            }
        }
        ActionResult resetData()
        {
            string SQLstring = "";
            try
            {
                SQLstring = "delete from L_Patients";
                DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring);
                SQLstring = "delete from L_Regs";
                DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring);
                SQLstring = "delete from L_Regdetails";
                DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring);
                return ActionResult.Success;

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return ActionResult.Exception;
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            cmdEditWL_Click(cmdEditWL, e);
        }

        

       

       

        private void chkSaveReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkSaveReject.ImageIndex == 1)
                {

                    chkSaveReject.ImageIndex = 0;
                }
                else
                {

                    chkSaveReject.ImageIndex = 1;
                }
            }
            catch
            {
            }
            finally
            {
                SaveSettingsDeviceError();
            }
        }

       

        private void cmdAdminSec_Click(object sender, EventArgs e)
        {
            using (frm_adSec _adSec = new frm_adSec())
            {
                if (_adSec.ShowDialog() != DialogResult.OK)
                    return;
            }

            pnlAdminSec.BringToFront();
        }

        private void cmdLogoutAdmin_Click(object sender, EventArgs e)
        {

            cmdAdminSec.BringToFront();
        }

       

        private void chkAutoSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoSend.ImageIndex == 1)
                {

                    chkAutoSend.ImageIndex = 0;
                }
                else
                {

                    chkAutoSend.ImageIndex = 1;
                }
            }
            catch
            {
            }
            finally
            {
                SaveSettingsDeviceError();
            }
        }

        private void chkDynamicGrid_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkDynamicGrid.ImageIndex == 1)
                {

                    chkDynamicGrid.ImageIndex = 0;
                }
                else
                {

                    chkDynamicGrid.ImageIndex = 1;
                }
            }
            catch
            {
            }
            finally
            {
            }
        }

       
        private void lblPCode_Click(object sender, EventArgs e)
        {

        }



        private void cmdCheckCode_Click(object sender, EventArgs e)
        {
            try
            {
                frm_CheckCode _CheckCode = new frm_CheckCode();
                _CheckCode.ShowDialog();
            }
            catch
            {
            }

        }





        

      

       


        private void lblPCode_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdTestAutoCrop_Click(object sender, EventArgs e)
        {

        }

       

       

        private void tabCtrlAcq_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {
                string folder2Search = "";

                foreach (DataRow dr in m_dtStudyListDataSource.Rows)
                {
                    string trueFolder = txtImgDir.Text + @"\" + dr["patient_code"].ToString().Substring(0, 8) + @"\" + SubDirLv2_Patient(dr);
                    string Folder_0age = txtImgDir.Text + @"\" + dr["patient_code"].ToString().Substring(0, 8) + @"\" + SubDirLv2_Patient_0Age(dr);
                    if (Directory.Exists(Folder_0age))
                    {
                        string[] files = Directory.GetFiles(Folder_0age);
                        if (!Directory.Exists(trueFolder)) Directory.CreateDirectory(trueFolder);
                        foreach (string file in files)
                        {
                            File.Move(file, trueFolder + @"\" + Path.GetFileName(file));
                        }
                        files = Directory.GetFiles(Folder_0age);
                        if (files.Length <= 0) Directory.Delete(Folder_0age);
                    }

                }
                Utility.ShowMsg("Đã cập nhật xong", "Thông báo");
            }
            catch
            {
            }
        }

        private void cmdPanel1_Click(object sender, EventArgs e)
        {
            try
            {
                if (_FPDMode == AppType.AppEnum.FPDMode.DualMode)
                {
                    if (cmdPanel1.Tag.ToString() == "P")
                        return;
                    else
                    {

                        cmdPanel1.Tag = "P";
                        cmdPanel1.BackColor = Color.Orange;
                        cmdPanel2.BackColor = Color.WhiteSmoke;

                        cmdPanel2.Tag = "N";

                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (_FPDMode == AppType.AppEnum.FPDMode.DualMode)
                {
                    if (Try2SelectPanel(FPDPanel1Idx))
                    {
                        FPDSeq = 1;
                       
                        ScheduledControl _reObj = GetSelectedScheduled();
                        if (_reObj != null)
                            LoadIEConfig(m_intCurrDevice1, _reObj.A_Code, _reObj.P_Code);
                    }
                    else
                    {
                        MessageBox.Show("Select panel failed");
                    }
                }
            }
        }
        bool Try2SelectPanel(int Idx)
        {
            try
            {
                TE_RESULT _result = 0;
                if (_FPDMode == AppType.AppEnum.FPDMode.DualMode)
                {
                    if (modName.ToUpper().Contains("FPD550PCI"))
                        _result = PanelSelect_550_PCI(Idx);
                    if (modName.ToUpper().Contains("FPD500PCI"))
                        _result = PanelSelect500_PCI(Idx);
                    if (modName.ToUpper().Contains("FPD560") || modName.ToUpper().Contains("FPD500ETHERNET"))
                        _result = PanelSelect(Idx);
                    if (modName.ToUpper().Contains("FDX4343R"))
                        _result = TE_SwitchDetector(Idx);
                    if (modName.ToUpper().Contains("FDX3543RP"))
                    {
                       //Change to Standby
                        Try2StandBy();
                        //CLose current Panel
                        CInterfaceLower.E_ERR_TETD_CONTROLLER_ENUM ret= this.cIfLower.TetdFpdClose();
                        if (ret == CInterfaceLower.E_ERR_TETD_CONTROLLER_ENUM.TETD_OK)
                        {
                            //Connect to new Panel
                            cIfLower.ActiveDetector = (DWORD)Idx;
                            SelectPanel_Toshiba();
                        }
                        else
                            return false;
                    }
                    AppLogger.LogAction.AddLog2List(lstFPD560, "Panel Select Result=" + _result.ToString());
                    return _result == Idx;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Select panel exception:\n" + ex.Message);
                return false;
            }
        }
        void SelectPanel_Toshiba()
        {
            try
            {
                // Register user callback function (for debug)
                cIfLower.TetdFpdRegisterCallback(new CInterfaceLower.TETD_CALLBACK_t(this.CallbackFromFPD));
                if (0 != this.cIfLower.ConnectFPDs)
                {
                    // Register user callback function
                    this.cIfLower.TetdFpdRegisterCallback(new CInterfaceLower.TETD_CALLBACK_t(this.CallbackFromFPD));
                    // Call API wrapper
                    if (CInterfaceLower.E_ERR_TETD_CONTROLLER_ENUM.TETD_OK == this.cIfLower.TetdFpdOpen((UInt32)this.cIfLower.ActiveDetector,
                        out this.ExposureRequestSwitch))
                    {
                        AppLogger.LogAction.ShowEventStatus(lblFPDStatus, "Select Panel success ");
                        // Check ExposureRequestSwitch
                        if (HARD_EXPOSURE == this.ExposureRequestSwitch)
                        {
                            //this.rbt_HardExposure.Checked = true;
                        }
                        else
                        {
                            //this.rbt_HardExposure.Checked = false;
                        }
                        EnterWorkMode();
                    }
                    else
                    {
                       
                    }
                }
               
            }
            catch (Exception ex)
            {
                AppLogger.LogAction.LogActions("==>InitFDX3543RP().Exception occurred at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "." + ex.ToString());
                Utility.ShowMsg(ex.Message);
            }
        }
        private void cmdPanel2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_FPDMode == AppType.AppEnum.FPDMode.DualMode)
                {
                    if (cmdPanel2.Tag.ToString() == "P")
                        return;
                    else
                    {

                        cmdPanel2.Tag = "P";
                        cmdPanel2.BackColor = Color.Orange;
                        cmdPanel1.BackColor = Color.WhiteSmoke;

                        cmdPanel1.Tag = "N";

                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (_FPDMode == AppType.AppEnum.FPDMode.DualMode)
                {
                    if (Try2SelectPanel(FPDPanel2Idx))
                    {
                        FPDSeq = 2;
                      
                        ScheduledControl _reObj = GetSelectedScheduled();
                        if (_reObj != null) LoadIEConfig(m_intCurrDevice2, _reObj.A_Code, _reObj.P_Code);
                    }
                    else
                    {
                        MessageBox.Show("Select panel failed");
                    }
                }
            }
        }

        private void cboFPD1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Int16 ModID = Convert.ToInt16(((DataRowView)cboFPD1.SelectedItem)["MODALITY_ID"].ToString());
                ModalityInfor _ModalityInfor = ModalityController.GetInfor(ModID);
                if (_ModalityInfor == null)
                {
                    chkAutoVFlip1.ImageIndex = 1;
                    chkAutoHFlip1.ImageIndex = 1;
                }
                else
                {
                    chkAutoVFlip1.ImageIndex = _ModalityInfor.AUTO_FLIPV == 1 ? 0 : 1;
                    chkAutoHFlip1.ImageIndex = _ModalityInfor.AUTO_FLIPH == 1 ? 0 : 1;
                }
            }
            catch
            {
            }
        }

        private void cboFPD2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Int16 ModID = Convert.ToInt16(((DataRowView)cboFPD2.SelectedItem)["MODALITY_ID"].ToString());
                ModalityInfor _ModalityInfor = ModalityController.GetInfor(ModID);
                if (_ModalityInfor == null)
                {
                    chkAutoVFlip2.ImageIndex = 1;
                    chkAutoHFlip2.ImageIndex = 1;
                }
                else
                {
                    chkAutoVFlip2.ImageIndex = _ModalityInfor.AUTO_FLIPV == 1 ? 0 : 1;
                    chkAutoHFlip2.ImageIndex = _ModalityInfor.AUTO_FLIPH == 1 ? 0 : 1;
                }
            }
            catch
            {
            }
        }

        private void chkAutoVFlip2_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoVFlip2.ImageIndex == 1)
                {

                    chkAutoVFlip2.ImageIndex = 0;
                }
                else
                {

                    chkAutoVFlip2.ImageIndex = 1;
                }
            }
            catch
            {
            }
            finally
            {
                //SaveSettingsDeviceError();
            }
        }

        private void chkAutoHFlip2_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoHFlip2.ImageIndex == 1)
                {

                    chkAutoHFlip2.ImageIndex = 0;
                }
                else
                {

                    chkAutoHFlip2.ImageIndex = 1;
                }
            }
            catch
            {
            }
        }

        private void cmdApplyFPDMode_Click(object sender, EventArgs e)
        {
            try
            {
                Int16 ModID1 = Convert.ToInt16(((DataRowView)cboFPD1.SelectedItem)["MODALITY_ID"].ToString());
                Int16 ModID2 = Convert.ToInt16(((DataRowView)cboFPD2.SelectedItem)["MODALITY_ID"].ToString());
                if (chk2tamkhacnhau.ImageIndex == 0 && ModID1 == ModID2)
                {
                    MessageBox.Show("Nếu muốn chạy 2 tấm giống nhau bạn cần chọn chế độ DualMode.Chế độ này chỉ cho phép bạn phải chọn 2 tấm khác nhau");
                    cboFPD2.Focus();
                    return;
                }
                if (chk2tamkhacnhau.ImageIndex == 1) new ModalityController().UpdateAutoFlip(ModID1, chkAutoVFlip1.ImageIndex == 0 ? 1 : 0, chkAutoHFlip1.ImageIndex == 0 ? 1 : 0);
                else
                {
                    new ModalityController().UpdateAutoFlip(ModID2, chkAutoVFlip2.ImageIndex == 0 ? 1 : 0, chkAutoHFlip2.ImageIndex == 0 ? 1 : 0);
                }
                SaveFPDModeSettings();
                MessageBox.Show("Bạn cần khởi động lại ứng dụng DROC để thiết lập có hiệu lực. Nhấn OK để thoát khỏi chương trình");
                Application.Exit();
            }
            catch
            {
            }

            //try
            //{
            //    Int16 ModID1 = Convert.ToInt16(((DataRowView)cboFPD1.SelectedItem)["MODALITY_ID"].ToString());
            //    Int16 ModID2 = Convert.ToInt16(((DataRowView)cboFPD2.SelectedItem)["MODALITY_ID"].ToString());
            //    if (cboFPDMode.SelectedIndex == 2 && ModID1==ModID2)
            //    {
            //        MessageBox.Show("Nếu muốn chạy 2 tấm giống nhau bạn cần chọn chế độ DualMode.Chế độ này chỉ cho phép bạn phải chọn 2 tấm khác nhau");
            //        cboFPD2.Focus();
            //        return;
            //    }
            //    if (cboFPDMode.SelectedIndex <= 1) new ModalityController().UpdateAutoFlip(ModID1, chkAutoVFlip1.ImageIndex == 0 ? 1 : 0, chkAutoHFlip1.ImageIndex == 0 ? 1 : 0);
            //    else
            //    {
            //        new ModalityController().UpdateAutoFlip(ModID1, chkAutoVFlip1.ImageIndex == 0 ? 1 : 0, chkAutoHFlip1.ImageIndex == 0 ? 1 : 0);
            //        new ModalityController().UpdateAutoFlip(ModID2, chkAutoVFlip2.ImageIndex == 0 ? 1 : 0, chkAutoHFlip2.ImageIndex == 0 ? 1 : 0);
            //    }
            //    SaveFPDModeSettings();
            //    MessageBox.Show("Bạn cần khởi động lại ứng dụng DROC để thiết lập có hiệu lực. Nhấn OK để thoát khỏi chương trình");
            //    Application.Exit();
            //}
            //catch
            //{
            //}
        }

        private void cboFPDMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (cboFPDMode.SelectedIndex >= 2)
            //    {
            //        lblPanel2.Enabled = true;
            //        cboFPD2.Enabled = true;
            //        grbPanel2.Enabled = true;
            //    }
            //    else
            //    {
            //        lblPanel2.Enabled = false;
            //        cboFPD2.Enabled = false;
            //        grbPanel2.Enabled = false;
            //    }

            //}
            //catch
            //{
            //}
        }

        private void cmdFPDadmin_Click(object sender, EventArgs e)
        {
            using (frm_adSec _adSec = new frm_adSec())
            {
                if (_adSec.ShowDialog() != DialogResult.OK)
                    return;
            }

            pnlFPDMode.BringToFront();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cmdFPDadmin.BringToFront();
        }

        private void chkChay1Tam_Click(object sender, EventArgs e)
        {
            chkChay1Tam.ImageIndex = 0;
            chk2tamgiongnhau.ImageIndex = 1;
            chk2tamkhacnhau.ImageIndex = 1;
            if (chk2tamkhacnhau.ImageIndex == 0)
            {
                lblPanel2.Enabled = true;
                cboFPD2.Enabled = true;
                grbPanel2.Enabled = true;
            }
            else
            {
                lblPanel2.Enabled = false;
                cboFPD2.Enabled = false;
                grbPanel2.Enabled = false;
            }
        }

        private void chk2tamgiongnhau_Click(object sender, EventArgs e)
        {
            chkChay1Tam.ImageIndex = 1;
            chk2tamgiongnhau.ImageIndex = 0;
            chk2tamkhacnhau.ImageIndex = 1;
            if (chk2tamkhacnhau.ImageIndex == 0)
            {
                lblPanel2.Enabled = true;
                cboFPD2.Enabled = true;
                grbPanel2.Enabled = true;
            }
            else
            {
                lblPanel2.Enabled = false;
                cboFPD2.Enabled = false;
                grbPanel2.Enabled = false;
            }
        }

        private void chk2tamkhacnhau_Click(object sender, EventArgs e)
        {
            chkChay1Tam.ImageIndex = 1;
            chk2tamgiongnhau.ImageIndex = 1;
            chk2tamkhacnhau.ImageIndex = 0;
            if (chk2tamkhacnhau.ImageIndex == 0)
            {
                lblPanel2.Enabled = true;
                cboFPD2.Enabled = true;
                grbPanel2.Enabled = true;
            }
            else
            {
                lblPanel2.Enabled = false;
                cboFPD2.Enabled = false;
                grbPanel2.Enabled = false;
            }
        }

        private void cmdCDWriter_Click(object sender, EventArgs e)
        {
            var tempFolder = @"C:\temp";
            try
            {
                DataRow[] selectecRows = m_dtStudyListDataSource.Select("Chon=1");
                if (selectecRows.Length == 0)
                {
                    Utility.ShowMsg("Bạn phải chọn bênh nhân trên lưới", "Thông báo", MessageBoxIcon.Information);
                    return;
                }
                if (!Utility.AcceptQuestion("Bạn có muốn lưu lại các bệnh nhân này ra đĩa CD/DVD không?", "Thông báo", true)) return;
                if (Directory.Exists(tempFolder)) Directory.Delete(tempFolder, true);
                if (BackupDataToFolder(tempFolder))
                {
                    var f = new Burner();
                    foreach (string folder in Directory.GetDirectories(tempFolder)) f.AddFolder(folder);
                    foreach (string file in Directory.GetFiles(tempFolder)) f.AddFile(file);
                    f.ShowDialog();

                }

            }
            finally
            {
                if (Directory.Exists(tempFolder)) Directory.Delete(tempFolder, true);
            }
        }
        private bool BackupDataToFolder(string folderName)
        {
            try
            {
                if (!Directory.Exists(folderName)) Directory.CreateDirectory(folderName);
                DataRow[] selectecRows = m_dtStudyListDataSource.Select("Chon=1");
                if (selectecRows.Length > 0)
                {
                    var xmlDoc = new XElement("PatientList", from c in selectecRows
                                                             select new XElement("Patient",
                                                                 new XElement("ID", Utility.sDbnull(c["Reg_ID"])),
                                                                 new XElement("PID", Utility.sDbnull(c["Patient_Code"])),
                                                                 new XElement("Age", Utility.sDbnull(c["Age"])),
                                                                 new XElement("PatientName", Utility.UnSignedCharacter(Utility.sDbnull(c["Patient_Name"])))));

                    foreach (var patient in xmlDoc.Elements())
                    {
                        Console.WriteLine(patient.ToString());
                        var idPhieu = Utility.Int32Dbnull(patient.Element("ID").Value, -1);
                        var pid = Utility.sDbnull(patient.Element("PID").Value, "");
                        if (idPhieu == -1) continue;
                        DataTable dt = new RegDetailController().GetAllData(idPhieu).Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            AddColumToDataTable(ref dt, "Dest_File", typeof(string));
                            foreach (DataRow dr in dt.Rows)
                            {
                                string RegNumber2 = dr["Reg_NUMBER"].ToString();
                                string sourceimgfile = txtImgDir.Text.Trim() + @"\" + SubDirLv1(RegNumber2) + @"\" + SubDirLv2_Patient(patient.Element("PID").Value, patient.Element("PatientName").Value, patient.Element("Age").Value) + @"\" + RegNumber2 + "_" + Utility.Int32Dbnull(dr["DETAIL_ID"]).ToString() + "_" + Utility.sDbnull(dr["ANATOMY_CODE"], "") + "_" + Utility.sDbnull(dr["PROJECTION_CODE"], "") + ".DCM";
                                if (File.Exists(sourceimgfile))
                                {
                                    string desimgfile = @"IMG\" + Path.GetFileName(sourceimgfile);
                                    // string desimgfile =  Path.GetFileName(sourceimgfile);

                                    patient.Add(new XElement("File", new XAttribute("FileName", desimgfile)));
                                    if (!Directory.Exists(Path.GetDirectoryName(desimgfile))) Directory.CreateDirectory(Path.GetDirectoryName(desimgfile));

                                    desimgfile = string.Format("{0}{1}{2}", folderName, Path.DirectorySeparatorChar, desimgfile);
                                    if (!Directory.Exists(Path.GetDirectoryName(desimgfile))) Directory.CreateDirectory(Path.GetDirectoryName(desimgfile));
                                    File.Copy(sourceimgfile, desimgfile, true);
                                }
                            }
                        }
                    }

                    xmlDoc.Save(string.Format("{0}{1}PatientList.xml", folderName, Path.DirectorySeparatorChar));
                    //Mã hóa file
                    var EncryptedString = Encrypt(xmlDoc.ToString(), Encoding.ASCII.GetBytes("V")[0]);
                    File.WriteAllBytes(string.Format("{0}{1}PatientList.dat", folderName, Path.DirectorySeparatorChar), EncryptedString);

                    // Copy Dicom Viewer
                    var miniDicomViewerFolder = "MiniDicomViewer";

                    foreach (string file in Directory.GetFiles(miniDicomViewerFolder))
                        File.Copy(file,
                                  string.Format("{0}{1}{2}", folderName, Path.DirectorySeparatorChar, Path.GetFileName(file)));
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Quá trình lưu trữ dữ liệu không thành công\n" + ex.Message, "Thông báo");
                return false;
            }
        }
        private byte[] Encrypt(string ipStrPlaintext, byte ipStrKey)
        {
            var vSecretBuffer = Encoding.ASCII.GetBytes(ipStrPlaintext);
            for (int i = 0; i < vSecretBuffer.Length; i++)
                vSecretBuffer[i] ^= ipStrKey;
            return vSecretBuffer;
        }
        public static void AddColumToDataTable(ref DataTable dataTable, string FieldName, Type type)
        {
            if (!dataTable.Columns.Contains(FieldName))
                dataTable.Columns.Add(FieldName, type);
            dataTable.AcceptChanges();
        }

        private void cmdDisk_Click(object sender, EventArgs e)
        {
            try
            {

                if (!Utility.AcceptQuestion("Bạn có muốn lưu trữ dữ liệu các BN đang chọn ra thư mục trên đĩa cứng hay không?", "Thông báo", true)) return;
                FolderBrowserDialog _FolderBrowserDialog = new FolderBrowserDialog();
                if (_FolderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (BackupDataToFolder(_FolderBrowserDialog.SelectedPath))
                        Utility.ShowMsg("Đã lưu trữ thành công");

                }
            }
            catch
            {
            }

        }

        private void cmdLargeFocus_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmdLargeFocus.Tag.ToString() == "P")
                    return;
                else
                {

                    cmdLargeFocus.Tag = "P";
                    cmdLargeFocus.BackColor = Color.Orange;
                    cmdSmallFocus.BackColor = Color.WhiteSmoke;

                    cmdSmallFocus.Tag = "N";

                }

            }
            catch
            {
            }
            finally
            {
                LARGE_FOCUS = 1;
            }
        }

        private void cmdSmallFocus_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmdSmallFocus.Tag.ToString() == "P")
                    return;
                else
                {

                    cmdSmallFocus.Tag = "P";
                    cmdSmallFocus.BackColor = Color.Orange;
                    cmdLargeFocus.BackColor = Color.WhiteSmoke;

                    cmdLargeFocus.Tag = "N";

                }

            }
            catch
            {
            }
            finally
            {
                LARGE_FOCUS = 0;
            }
        }

        private void mnuCheckAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow dr in m_dtStudyListDataSource.Rows)
                    dr["CHON"] = 1;
                m_dtStudyListDataSource.AcceptChanges();
            }
            catch
            {
            }
        }

        private void mnuReverse_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow dr in m_dtStudyListDataSource.Rows)
                    if (dr["CHON"].ToString() == "1")
                        dr["CHON"] = 0;
                    else
                        dr["CHON"] = 1;
                m_dtStudyListDataSource.AcceptChanges();
            }
            catch
            {
            }
        }

        private void cmdSendAll_Click(object sender, EventArgs e)
        {
           
        }
        private void cmdSaveComInfor_Click(object sender, EventArgs e)
        {
            SaveInfor();
        }
        private void chkAutoGenPID_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoGenPID.ImageIndex == 1)
                {

                    chkAutoGenPID.ImageIndex = 0;
                }
                else
                {

                    chkAutoGenPID.ImageIndex = 1;
                }
            }
            catch
            {
            }
            finally
            {
                SaveSettingsDeviceError();
            }
        }
        private void lblGCOMType_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblGCOM_VN.ImageIndex == 1)
                {

                    lblGCOM_VN.ImageIndex = 0;
                }
                else
                {

                    lblGCOM_VN.ImageIndex = 1;
                }
            }
            catch
            {
            }
            finally
            {
                AutoSelectGCOM();
                SaveSettingsDeviceError();
            }
        }
        void AutoSelectGCOM()
        {
            isGCOM_VN = lblGCOM_VN.ImageIndex == 0;
            if (isGCOM_VN) pnlAMECGCOM.BringToFront();
            else pnlAMECGCOM.SendToBack();
        }
        private void cmdSaveParam_VN_Click(object sender, EventArgs e)
        {
            try
            {
                ScheduledControl _ScheduledControl = GetSelectedScheduled();
                if (_ScheduledControl != null)
                {
                    string errMsg = "";
                    ActionResult actResult = new DoctorController().InsertAPParam_Acq(_ScheduledControl.A_Code, _ScheduledControl.P_Code, BODYSIZE_CODE, lblKvpVal.Value, (int)lblmAVal.Value, (int)lblmAsVal.Value, "", m_intCurrDevice1, 0, 0, ref errMsg);
                    if (actResult == ActionResult.Success)
                    {
                        SetText(lblResult, "Save OK");
                    }
                    else
                        SetText(lblResult, "Save failed");
                }
            }
            catch
            {
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            EnterWorkMode();
           
        }
        void Try2StandBy()
        {
            try
            {
                this.ExposureMode = SINGLE_EXPOSURE;
                //this.btn_ToDouble.Text = "To Double";

                // Set Exposure mode
                this.cIfLower.TetdFpdExposureMode(this.ExposureMode);

                // Call API wrapper
                if (CInterfaceLower.E_ERR_TETD_CONTROLLER_ENUM.TETD_OK == this.cIfLower.TetdFpdStandbyState())
                {
                  
                }
                else
                {
                    // Error process (none)
                }
            }
            catch
            {
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
           
        }
        private void cmdEndProcess_Click(object sender, EventArgs e)
        {
           
        }
        void try2createfolder(string spath)
        {
            if (!Directory.Exists(spath)) Directory.CreateDirectory(spath);
        }
        private void cmdCopy_Click(object sender, EventArgs e)
        {
            string folder=txtImgDir.Text.Trim()+@"\RAW";
            try2createfolder(folder);
            List<string> rawfile = Directory.GetFiles(txtImgDir.Text, "*.RAW",SearchOption.AllDirectories).ToList<string>();
            foreach (string file in rawfile)
            {
                try
                {
                    string destfileName=folder+@"\"+Path.GetFileName(file);
                    File.Copy(file, destfileName, true);
                }
                catch
                {
                }
            }
            Utility.ShowMsg("OK");
        }
        private void cmdRefresh_Click(object sender, EventArgs e)
        {

        }

        private void cmdReload_Click(object sender, EventArgs e)
        {
            LoadDeviceErrSettings();
        }
    }

    public class NumericTextBoxV2 : TextBox
    {
        bool allowSpace = false;

        // Restricts the entry of characters to digits (including hex), the negative sign,
        // the decimal point, and editing keystrokes (backspace).
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
             keyInput.Equals(negativeSign))
            {
                // Decimal separator is OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            //    else if ((ModifierKeys & (Keys.Control | Keys.Alt)) != 0)
            //    {
            //     // Let the edit control handle control and alt key combinations
            //    }
            else if (this.allowSpace && e.KeyChar == ' ')
            {

            }
            else
            {
                // Swallow this invalid key and beep
                e.Handled = true;
                //    MessageBeep();
            }
        }

        public int IntValue
        {
            get
            {
                return Int32.Parse(this.Text);
            }
        }

        public decimal DecimalValue
        {
            get
            {
                return Decimal.Parse(this.Text);
            }
        }

        public bool AllowSpace
        {
            set
            {
                this.allowSpace = value;
            }

            get
            {
                return this.allowSpace;
            }
        }
    }
    
    public partial class NumericTextBox : System.Windows.Forms.TextBox
    {
        private int _maximumAllowed;
        private int _minimumAllowed;
        private string _oldText;

        public NumericTextBox()
        {
            _maximumAllowed = 1000;
            _minimumAllowed = -1000;
            _oldText = "";
        }

        [Description("The minimum allowed value to be entered"),
        Category("Allowed Values")]
        public int MinimumAllowed
        {
            set
            {
                _minimumAllowed = value;
            }
            get
            {
                return _minimumAllowed;
            }
        }

        [Description("The maximum allowed value to be entered"),
        Category("Allowed Values")]
        public int MaximumAllowed
        {
            set
            {
                _maximumAllowed = value;
            }
            get
            {
                return _maximumAllowed;
            }
        }

        [Description("The maximum allowed value to be entered"),
        Category("Current Value")]
        public int Value
        {
            set
            {
                this.Text = value.ToString();
            }
            get
            {
                if (this.Text.Trim() == "")
                    return _minimumAllowed;
                else
                    return Convert.ToInt32(this.Text);
            }
        }

        // Is the entered number within the specified valid range
        private bool IsAllowed(string text)
        {
            bool isAllowed = true;

            try
            {
                int newNumber = Convert.ToInt32(text);
                if ((newNumber > _maximumAllowed) || (newNumber < _minimumAllowed))
                    isAllowed = false;
            }
            catch
            {
                // This happen if the entered value is not a numeric.
                isAllowed = false;
            }

            return isAllowed;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (!IsAllowed(this.Text))
            {
                if (_minimumAllowed <= 1)
                    this.Text = _oldText;
            }
            else
                _oldText = this.Text;

            base.OnTextChanged(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Increase or decrease the current value by 1 if the user presses the UP or DOWN
            // and test if the new value is allowed
            if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down))
            {
                int value = Convert.ToInt32(this.Text);

                value = (e.KeyCode == Keys.Up) ? value + 1 : value - 1;

                if (IsAllowed(value.ToString()))
                    this.Text = value.ToString();
            }

            base.OnKeyDown(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            int value = Convert.ToInt32(this.Text);
            if (value < _minimumAllowed)
                this.Text = _minimumAllowed.ToString();

            base.OnLostFocus(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();

            

            // if Enter, Escape, Ctrl or Alt key is pressed, then there is no need to check
            // since the user is not entering a new character...
            if (((Control.ModifierKeys & Keys.Control) == 0) &&
                ((Control.ModifierKeys & Keys.Alt) == 0) &&
                 (e.KeyChar != Convert.ToChar(Keys.Enter)) &&
                 (e.KeyChar != Convert.ToChar(Keys.Escape)))
            {

                #region Check if the entered character is valid for Numeric format
                if (Char.IsDigit(e.KeyChar))
                {
                    // Digits are OK
                }
                else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
             keyInput.Equals(negativeSign))
                {
                    // Decimal separator is OK
                }
                // Validate the entered character
                if (!Char.IsNumber(e.KeyChar))
                {

                    #region Check If the user has entered Minus character
                    // Here we check if the user wants to enter the "-" character.
                    if (!((this.Text.IndexOf('-') == -1) && // there is no Minus character
                          (this.SelectionStart == 0) && // the cursor at the begining
                          (_minimumAllowed < 0) && // the minimum allowed accept negative values
                          (e.KeyChar == '-')))  // the character entered was a Minus character
                        e.KeyChar = Char.MinValue;
                    #endregion
                }
                #endregion

                if (_minimumAllowed <= 1)
                    #region Checkinng if the value falles within the given range
                    if (e.KeyChar != Char.MinValue)
                    {
                        // Create the string that will be displayed, and check whether it's valid or not.

                        // Remove the selected character(s).
                        string newString = this.Text.Remove(this.SelectionStart, this.SelectionLength);
                        // Insert the new character.
                        newString = newString.Insert(this.SelectionStart, e.KeyChar.ToString());
                        if (!IsAllowed(newString))
                            // the new string is not valid, cancel the whole operation.
                            e.KeyChar = Char.MinValue;
                    }
                    #endregion
            }
            base.OnKeyPress(e);
        }
    }
}