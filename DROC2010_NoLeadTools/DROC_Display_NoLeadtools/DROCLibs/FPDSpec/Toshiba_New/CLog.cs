//***********************************************************************************************************
/*!
 * @file		CLog.cs
 * @brief		Log class.
 * 
 * @author		TJ
 * @date		2012/03/30(Fri) 
 */
//***********************************************************************************************************
using System;
using System.IO;
using System.Text;
using System.Reflection;

namespace VietBaIT.DROC
{
    //*******************************************************************************************************
    /*!
     * @class		Log class.
     * @brief		Output to Log text box and log file.
     * 
     * @author		TJ
     * @date		2012/03/30(Fri) 
     */
    //*******************************************************************************************************
    public class CLog : IDisposable
    {
        #region private fields
        // Log file retention period (days)
        private const Int32 retentionPeriod = 14;
        // Log file base name
        private const String logBaseName = "Sample.log";
        // Character encoding
        private Encoding logEncoding = Encoding.ASCII;
        // Log file name
        private String logFileName;
        #endregion

        #region public fields
        // Output to Log text box delegate
        public delegate void OutputLogHandler(String message);
        // Output to Log text box handler
        public OutputLogHandler OutputLog;
        // Output switch (true -> enabled, false -> disabled)
        public Boolean outputLogFlag = true;
        #endregion

        #region constructor
        //***************************************************************************************************
        /*!
         * @brief		Constructor.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @return		none
         */
        //***************************************************************************************************
        public CLog()
        {
            // Make log file name
            Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
            String logDir = Path.GetDirectoryName(assembly.Location);
            this.logFileName = Path.Combine(logDir, DateTime.Now.ToString("yyyyMMdd") + "_" + logBaseName);

            // Delete old log file
            String[] fileNames = Directory.GetFiles(logDir);
            DateTime limitDate = DateTime.Now.AddDays(0 - retentionPeriod);
            foreach (String checkFileName in fileNames)
            {
                if ((true == checkFileName.EndsWith(logBaseName)) &&
                    (limitDate.CompareTo(File.GetCreationTime(checkFileName)) > 0))
                {
                    File.Delete(checkFileName);
                }
            }

            // Output "---- LogBegin ----"
            this.WriteLine("---- LogBegin ----");
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
        ~CLog()
        {
        }
        #endregion

        #region public methods
        //***************************************************************************************************
        /*!
         * @brief		Output to Log text box and log file.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @param[in]   String message  Log output.
         * 
         * @return		none
         */
        //***************************************************************************************************
        public void WriteLine(String message)
        {
            if ((null != this.logFileName) && (0 != this.logFileName.Length) && (true == this.outputLogFlag))
            {
                StreamWriter streamWriter;
                // Begin with current time
                String logMessage = DateTime.Now.ToString("HH:mm:ss.fff") + " " + message;
                // If output to Log text box handler is attached
                if (null != this.OutputLog)
                {
                    // Output to Log text box
                    this.OutputLog(logMessage);
                }
                // Output to log file
                using (streamWriter = new StreamWriter(this.logFileName, true, logEncoding))
                {
                    streamWriter.WriteLine(logMessage);
                    streamWriter.Flush();
                }
            }
        }

        //***************************************************************************************************
        /*!
         * @brief		Dispose method.
         * 
         * @author		TJ
         * @date		2012/03/30(Fri) 
         *
         * @return		none
         */
        //***************************************************************************************************
        public void Dispose()
        {
            // Output "---- End ----"
            this.WriteLine("---- End ----");
        }
        #endregion
    }
}
