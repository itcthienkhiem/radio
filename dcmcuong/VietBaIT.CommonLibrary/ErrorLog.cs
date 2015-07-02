using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using VietBaIT.RISLink.DataAccessLayer;
using System.Configuration;
namespace VietBaIT.CommonLibrary
{
  public  class ErrorLog
    {
      public static void InsertErrorlog(string ContentError,string ClassName,string Application)
      {
          VietBaIT.RISLink.DataAccessLayer.ErrorLog.Insert(globalVariables.UserName, ClassName,
                                                           BusinessHelper.GetSysDateTime(), Application, ContentError,"ERROR");
      }
      public static void InsertInfolog(string ContentError, string ClassName, string Application)
      {
          VietBaIT.RISLink.DataAccessLayer.ErrorLog.Insert(globalVariables.UserName, ClassName,
                                                           BusinessHelper.GetSysDateTime(), Application, ContentError, "INFO");
      }
    }
}
