using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace VIETBAIT.WORKLISTSCP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Thread.Sleep(5000);
            try
            {
                ServiceBase[] ServicesToRun;
                if (Debugger.IsAttached)
                {
                    var s = new WorklistScpService();
                }
                else
                {
                    ServicesToRun = new ServiceBase[] 
			{ 
				new WorklistScpService() 
			};
                    ServiceBase.Run(ServicesToRun);
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
            
        }
    }
}
