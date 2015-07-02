using System;
using System.Collections.Generic;

using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing;
using VB6 = Microsoft.VisualBasic;
namespace VietBaIT.DROC
{
    public class ExportExcel
    {
        public static void exportToExcel(DataTable dtData, string fileName, string SheetName)
        {
            try
            {

                System.IO.StreamWriter excelDoc;

                excelDoc = new System.IO.StreamWriter(fileName);
                const string startExcelXML = "<xml version>\r\n<Workbook " +
                      "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n" +
                      " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n " +
                      "xmlns:x=\"urn:schemas-    microsoft-com:office:" +
                      "excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:" +
                      "office:spreadsheet\">\r\n <Styles>\r\n " +
                      "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
                      "<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>" +
                      "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
                      "\r\n <Protection/>\r\n </Style>\r\n " +
                      "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
                      "x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
                      "<Style     ss:ID=\"StringLiteral\">\r\n <NumberFormat" +
                      " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
                      "ss:ID=\"Decimal\">\r\n <NumberFormat " +
                      "ss:Format=\"0.0000\"/>\r\n </Style>\r\n " +
                      "<Style ss:ID=\"Integer\">\r\n <NumberFormat " +
                      "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
                      "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
                      "ss:Format=\"mm/dd/yyyy;@\"/>\r\n </Style>\r\n " +
                      "</Styles>\r\n ";
                const string endExcelXML = "</Workbook>";

                int rowCount = 0;
                int sheetCount = 1;
                excelDoc.Write(startExcelXML);
                excelDoc.Write("<Worksheet ss:Name=\"Sheet" + SheetName + "\">");
                excelDoc.Write("<Table>");
                excelDoc.Write("<Row>");
                for (int x = 0; x < dtData.Columns.Count; x++)
                {
                    excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                    excelDoc.Write(dtData.Columns[x].ColumnName);
                    excelDoc.Write("</Data></Cell>");
                }
                excelDoc.Write("</Row>");
                foreach (DataRow x in dtData.Rows)
                {
                    rowCount++;
                    //if the number of rows is > 64000 create a new page to continue output

                    if (rowCount == 64000)
                    {
                        rowCount = 0;
                        sheetCount++;
                        excelDoc.Write("</Table>");
                        excelDoc.Write(" </Worksheet>");
                        excelDoc.Write("<Worksheet ss:Name=\"Sheet" + sheetCount + "\">");
                        excelDoc.Write("<Table>");
                    }
                    excelDoc.Write("<Row>"); //ID=" + rowCount + "

                    for (int y = 0; y < dtData.Columns.Count; y++)
                    {
                        System.Type rowType;
                        rowType = x[y].GetType();
                        switch (rowType.ToString())
                        {
                            case "System.String":
                                string XMLstring = x[y].ToString();
                                XMLstring = XMLstring.Trim();
                                XMLstring = XMLstring.Replace("&", "&");
                                XMLstring = XMLstring.Replace(">", ">");
                                XMLstring = XMLstring.Replace("<", "<");
                                excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                               "<Data ss:Type=\"String\">");
                                excelDoc.Write(XMLstring);
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.DateTime":
                                //Excel has a specific Date Format of YYYY-MM-DD followed by 

                                //the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000

                                //The Following Code puts the date stored in XMLDate

                                //to the format above

                                DateTime XMLDate = (DateTime)x[y];
                                string XMLDatetoString = ""; //Excel Converted Date

                                XMLDatetoString = XMLDate.Year.ToString() +
                                     "-" +
                                     (XMLDate.Month < 10 ? "0" +
                                     XMLDate.Month.ToString() : XMLDate.Month.ToString()) +
                                     "-" +
                                     (XMLDate.Day < 10 ? "0" +
                                     XMLDate.Day.ToString() : XMLDate.Day.ToString()) +
                                     "T" +
                                     (XMLDate.Hour < 10 ? "0" +
                                     XMLDate.Hour.ToString() : XMLDate.Hour.ToString()) +
                                     ":" +
                                     (XMLDate.Minute < 10 ? "0" +
                                     XMLDate.Minute.ToString() : XMLDate.Minute.ToString()) +
                                     ":" +
                                     (XMLDate.Second < 10 ? "0" +
                                     XMLDate.Second.ToString() : XMLDate.Second.ToString()) +
                                     ".000";
                                excelDoc.Write("<Cell ss:StyleID=\"DateLiteral\">" +
                                             "<Data ss:Type=\"DateTime\">");
                                excelDoc.Write(XMLDatetoString);
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.Boolean":
                                excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                            "<Data ss:Type=\"String\">");
                                excelDoc.Write(x[y].ToString());
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                excelDoc.Write("<Cell ss:StyleID=\"Integer\">" +
                                        "<Data ss:Type=\"Number\">");
                                excelDoc.Write(x[y].ToString());
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.Decimal":
                            case "System.Double":
                                string _value = x[y].ToString();
                                if (_value.Contains(".") || _value.Contains(","))
                                {
                                }
                                else
                                {
                                }
                                excelDoc.Write("<Cell ss:StyleID=\"Decimal\">" +
                                      "<Data ss:Type=\"Number\">");
                                excelDoc.Write(_value);
                                excelDoc.Write("</Data></Cell>");
                                break;
                            case "System.DBNull":
                                excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\">" +
                                      "<Data ss:Type=\"String\">");
                                excelDoc.Write("");
                                excelDoc.Write("</Data></Cell>");
                                break;
                            default:
                                throw (new Exception(rowType.ToString() + " not handled."));
                        }
                    }
                    excelDoc.Write("</Row>");
                }
                excelDoc.Write("</Table>");
                excelDoc.Write(" </Worksheet>");
                excelDoc.Write(endExcelXML);
                excelDoc.Close();
            }
            catch
            {
            }
        }

    }
    
    public  class mdlStatic
    {
        public mdlStatic()
        {
            //
            // TODO: Add constructor logic here
            //
        }
      
        public static VietBaIT.DROC.DROC_Ribbon mainform;
       
        public static bool isDisplayImg = false;
        public static string sServer = "10.0.0.1";
        public static string sUser = "evnit\tuantt";
        public static string sPass = "chienbinh";
        public static string sPathNhan = "Traodoi";
        public static int iPort = 2122;
        public static System.Data.SqlClient.SqlConnection gConn;
        // Chứa tên hoặc ID của máy chủ CSDL
        public static string gv_sComName = System.Environment.MachineName;
        // Tên CSDL
        public static string gv_sDBName = "Assembly";
        // Chứa tên đăng nhập QTHT
        public static string gv_sUID = "";
        // Chứa tên đăng nhập QTHT
        public static string gv_sStaffName = "";
        // Chứa tên đăng nhập QTHT
        public static int gv_StaffID = -1;
        // Chứa mật khẩu công khai của QTHT
        public static string gv_sPWD = "";
        public static string gv_sConnString = "";
        public static string ProductKey = "";
        public static string sDBnull(object pv_obj)
        {
            if (( pv_obj == null)||Convert.IsDBNull(pv_obj))
            {
                return "";
            }
            else
            {
                return pv_obj.ToString().Trim();
            }
        }
       public static void AttatchVirtualKeyboard(Control Parentctrl)
        {
            try
            {
                foreach (Control _ctrl in Parentctrl.Controls)
                {
                    _ctrl.GotFocus += new EventHandler(_ctrl_GotFocus);
                    AttatchVirtualKeyboard(_ctrl);
                }
            }
            catch
            {
            }
        }

       static void _ctrl_KeyDown(object sender, KeyEventArgs e)
       {
           try
           {
               if (e.KeyCode == Keys.Enter) SendKeys.Send("{tab}");
           }
           catch
           {
           }
       }

       static void _ctrl_GotFocus(object sender, EventArgs e)
        {
          
        }
        public static void HandleGotFocusEventForTextBoxFromThis(System.Windows.Forms.Form frm)
        {
            foreach (Control ctr in frm.Controls)
            {
                if (ctr.GetType().Equals(typeof(TextBox)))
                {
                    TextBox objTxt = ctr as TextBox;
                    objTxt.GotFocus += new EventHandler(objTxt_GotFocus);
                }
                else
                    DelegateEventForTextBoxControls(ctr);
            }
        }

        private static void DelegateEventForTextBoxControls(System.Windows.Forms.Control _container)
        {
            foreach (Control ctr in _container.Controls)
            {
                if (ctr.GetType().Equals(typeof(TextBox)))
                {
                    TextBox objTxt = ctr as TextBox;
                    objTxt.GotFocus += new EventHandler(objTxt_GotFocus);
                }
                else
                    DelegateEventForTextBoxControls(ctr);
            }
        }

        static void  objTxt_GotFocus(object sender, EventArgs e)
        {
           
        }
        public static string sDBnull(object pv_obj,string reval)
        {
            if ((pv_obj == null)||Convert.IsDBNull(pv_obj))
            {
                return reval;
            }
            else
            {
                return pv_obj.ToString().Trim();
            }
        }
        public static int intDBnull(object pv_obj)
        {
            if ((pv_obj == null)||Convert.IsDBNull(pv_obj))
            {
                return -1;
            }
            else
            {
                return (int)pv_obj;
            }
        }
        public static string GetYYMMDD(System.DateTime tDate)
        {
            return VB6.Strings.Right(tDate.Year.ToString(), 2) + VB6.Strings.Right("0" + tDate.Month.ToString(), 2) + VB6.Strings.Right("0" + tDate.Day.ToString(), 2);
        }
        public static string GetYYMMDD(string tDate)
        {
            string[] arrdate = tDate.Split('/');
            return VB6.Strings.Right(arrdate[2], 2) + arrdate[1] + arrdate[0];//CheckNumeric.Numeric.Right(tDate.Year.ToString(), 2) + CheckNumeric.Numeric.Right("0" + tDate.Month.ToString(), 2) + CheckNumeric.Numeric.Right("0" + tDate.Day.ToString(), 2);
        }
        public static bool _IsNumeric(object obj)
        {
            return VB6.Information.IsNumeric(obj);
        }
        public static int intDBnull(object pv_obj,int reval)
        {
            if (pv_obj == null)
            {
                return reval;
            }
            else
            {
                return (int)pv_obj;
            }
        }
        /// <summary>
        /// HAM THỰC HIỆN HIÊN THỊ LABLEL
        /// </summary>
        /// <param name="lblMsg"></param>
        /// <param name="Message"></param>
        /// <param name="isErr"></param>
        public static void SetMsg(Label lblMsg, string Message, bool isErr)
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
        }

        /// <summary>
        /// hàm thực hiện việc hiển thị thông tin của labbel
        /// </summary>
        /// <param name="lblMsg"></param>
        /// <param name="Message"></param>
        /// <param name="isErr"></param>
        public static void SetMessage(Label lblMsg, string Message, bool isErr)
        {

            lblMsg.Text = Message;
            lblMsg.Visible = isErr;
        }
        public static void SetMsg(ToolStripStatusLabel lblMsg, string Message, bool isErr)
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
        }

        /// <summary>
        /// hàm thực hiện việc hiển thị thông tin của labbel
        /// </summary>
        /// <param name="lblMsg"></param>
        /// <param name="Message"></param>
        /// <param name="isErr"></param>
        public static void SetMessage(ToolStripStatusLabel lblMsg, string Message, bool isErr)
        {

            lblMsg.Text = Message;
            lblMsg.Visible = isErr;
        }
        //public static object GetFieldValue(string pv_sTableName, string pv_sFieldName, string pv_sCondition)
        //{
        //    string fv_sSql = "";
        //    fv_sSql = "SELECT " + pv_sFieldName + " FROM " + pv_sTableName + " WHERE " + pv_sCondition;
        //    OleDbDataAdapter DA = new OleDbDataAdapter(fv_sSql, mdlStatic.gConn);
        //    DataTable DT = new DataTable();
        //    try
        //    {
        //        DA.Fill(DT);
        //        if (DT.Rows.Count > 0)
        //        {
        //            return DT.Rows[0][0];
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //ShowErrMsg(ex.Message);
        //        return null;
        //    }

        //}
    }
   

}
