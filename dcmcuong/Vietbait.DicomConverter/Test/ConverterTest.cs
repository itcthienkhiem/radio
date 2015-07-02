using System.Data;
using System.Diagnostics;
using NUnit.Framework;

namespace Vietbait.DicomConverter.Test
{
    [TestFixture]
    internal class ConverterTest
    {
        [Test]
        public void Convert()
        {
            
            var sw = new Stopwatch() ;

            try
            {

                const string colPid = "PID";
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

                var dt = new DataTable();
                dt.Columns.Add(colPid);
                dt.Columns.Add(colPatientName);
                dt.Columns.Add(colPatientSex);
                dt.Columns.Add(colPatientAge);
                dt.Columns.Add(colPatientBirthdate);
                dt.Columns.Add(colRegDate);
                dt.Columns.Add(colRegNum);
                dt.Columns.Add(colImgWidth);
                dt.Columns.Add(colImgHeight);
                dt.Columns.Add(colModalityCode);
                dt.Columns.Add(colAtonomyCode);
                dt.Columns.Add(colProjectionCode);

                DataRow  dr = dt.NewRow();
                dr[colPid] = "120222-0001";
                dr[colPatientName] = "Nguyen Van C";
                dr[colPatientSex] = "M";
                dr[colPatientAge] = "27";
                dr[colPatientBirthdate] = "19890212";
                dr[colRegDate] = "20120221";
                dr[colRegNum] = "1202010078";
                dr[colImgWidth] = "";
                dr[colImgHeight] = "";
                dr[colModalityCode] = "VietBa-DR";
                dr[colAtonomyCode] = "Chest";
                dr[colProjectionCode] = "PA";

                //dt.Rows.Add(dr);

                sw.Start();
                DicomConverter.Convert2Dicom(@"C:\TETDRadMgr\demo\bin\Final_image__20120425123059.raw", dr);
            }
            catch (System.Exception exception )
            {

                throw;
            }
            sw.Stop();
            Debug.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}