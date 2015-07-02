using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using SubSonic;
using System.Xml;
using VietBaIT.CommonLibrary;
using System.Data;

namespace VietBaIT.RISLink.Business.NghiepVu.ConnectionSQL
{
  public class ConnectionSQL
  {
     public ConnectionSQL()
     {
         DataSet dsConfigXML = new DataSet();

         string sPathXML = "Config.XML";
         if (System.IO.File.Exists(sPathXML))
         {
             dsConfigXML.ReadXml(sPathXML);
             if (dsConfigXML.Tables[0].Rows.Count > 0)
             {
                 globalVariables.ServerName = Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["SERVERADDRESS"], ".");
                 globalVariables.sUName = Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["USERNAME"], "sa");
                 globalVariables.sPwd = Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["PASSWORD"], "sa");
                 globalVariables.sDbName = Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["DATABASE_ID"], "RISLINK_DB");
             }
             //else
             //{
             //    Utility.ShowMsg("Không tìm thấy File Config.XML , Bạn xem lại", "Thông báo", MessageBoxIcon.Error);

             //}
         }
        
     }
      public  string KhoiTaoKetNoi()
      {
          string strSQL = "";
          try
          {
              strSQL =
             string.Format(
                 "workstation id={0};packet size=4096;data source={0};persist security info=False;initial catalog={1};uid={2};pwd={3}",
                 globalVariables.ServerName, globalVariables.sDbName, globalVariables.sUName, globalVariables.sPwd);
              globalVariables.SqlConnectionString = strSQL;
              return strSQL;
          }
          catch (Exception ex)
          {
              
             
          }
          return strSQL;


      }

   
     
     
    }
}
