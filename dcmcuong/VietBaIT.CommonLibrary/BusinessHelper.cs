using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using System.Data;
using VietBaIT.RISLink.DataAccessLayer;

namespace VietBaIT.CommonLibrary
{
   public class BusinessHelper
    {
       public static string LayMaLanKham()
       {
           string workingYear = BusinessHelper.GetSysDateTime().Year.ToString();
           string result = string.Format("{0}-{1}", workingYear, GenerateCode(PatientCodeIncrement() + 1));
           return result;
       }
       private static int PatientCodeIncrement()
       {

           return Convert.ToInt32(NumberPatientCode());
       }

       public static string NumberPatientCode()
       {
           string OutpatientcCode = "";
           string LastCode = "";

           // var Patient = SPs.GetPatientCode(patientcCode);
           //DateTime? outPutDT = null;
           string result = "";
           StoredProcedure sp = SPs.GetPatientCode(OutpatientcCode);
           sp.Execute();
           sp.OutputValues.ForEach(delegate(object objOutput)
           {
               result = (String)objOutput;
           });

           LastCode = result.Substring(result.IndexOf("-") + 1, result.Length - (result.IndexOf("-") + 1));
           //  VietBaIT.CommonLibrary.Utility.ShowMsg(LastCode);
           return LastCode;


       }
        /// <summary>
        /// hàm thực hiện việc lấy màu của hệ thống
        /// </summary>
        /// <returns></returns>
        public static string GetSysColor()
        {
            SqlQuery q = new Select().From(SysFormColor.Schema)
               .Where(SysFormColor.Columns.SystemColorId).IsEqualTo(1);
            string SysColor = "";
            SysFormColor objSystemParameter = q.ExecuteSingle<SysFormColor>();
            if (objSystemParameter != null)
            {
                SysColor = Utility.sDbnull(objSystemParameter.ColorValue);
            }
            return SysColor;
        }
       /// <summary>
       /// hàm thực hiện việc trả thông tin của Datetime
       /// </summary>
       /// <returns></returns>
       public static DateTime GetSysDateTime()
       {
           try
           {
               return new InlineQuery().ExecuteScalar<DateTime>("SELECT GETDATE()");
           }
           catch (Exception)
           {
               return DateTime.Now;
           }
       }
       private static string GenerateCode(int increment)
       {
           string result = string.Empty;
           string suffix;
           suffix = string.Empty;
           char[] arr = increment.ToString().ToCharArray();
           for (int i = arr.Length; i < 5; i++)
           {
               suffix += "0";
           }
           result = string.Format("{0}{1}", suffix, increment);
           return result;
       }
       /// <summary>
       /// hàm thực hiện việc kiểm tra xem thông tin của phần hình ảnh
       /// </summary>
       /// <returns></returns>
       public static bool IsHinhHanh()
       {
           SysConfigRadio objSysConfigRadio = SysConfigRadio.FetchByID(2);
           if (objSysConfigRadio != null)
           {
               if (objSysConfigRadio.PathUNC == "1") return true;
               else

                   return false;
           }
           return false;
       }
       public static string MaNhanVienCode()
       {

           string result = Utility.GetFormatDateTime(BusinessHelper.GetSysDateTime(), "yyMMddhhssmm");
           return result;
       }
       public static bool IsBaoHiem(string MaDoiTuong)
       {
           DDoiTuong objDoiTuong = DDoiTuong.FetchByID(MaDoiTuong);
           if(objDoiTuong!=null) return true;
           else
           {
               return false;
           }
       }

       public static string LayMaPhieu()
       {
           DataTable dataTable=new DataTable();
           string Barcode = "";
           dataTable = SPs.RisSinhmaPhieu(BusinessHelper.GetSysDateTime()).GetDataSet().Tables[0];
           if(dataTable.Rows.Count>0)
           {
               Barcode = Utility.sDbnull(dataTable.Rows[0]["BarCode"]);
           }
           return Barcode;
       }
      
    }
}
