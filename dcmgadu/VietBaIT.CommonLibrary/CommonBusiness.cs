using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using System.Data;
using VietBaIT.RISLink.DataAccessLayer;
using VietBaIT.CommonLibrary;
namespace VietBaIT.CommonLibrary
{
   public class CommonBusiness
    {
       /// <summary>
       /// hàm thực hiện lấy thông tin của thành phố
       /// </summary>
       /// <returns></returns>
       public static DataTable  LayThongTinThanhPho()
       {
           DataTable dataTable =

               new Select().From(DDviDchinh.Schema).Where(DDviDchinh.Columns.LoaiDviDchinh).IsEqualTo("TP").OrderAsc(
                   DDviDchinh.Columns.SttHthi).ExecuteDataSet().Tables[0];

           return dataTable;
       }
      
       public static DataTable LayThongTinDanhMucChung(string MaLoai)
       {
           DataTable dataTable = globalVariables.p_DataDanhMucDungChung.Copy();
           foreach (DataRow drv in dataTable.Rows)
           {
               if(!drv[DDmucChung.Columns.Loai].ToString().Equals(MaLoai))drv.Delete();
           }
           dataTable.AcceptChanges();
           return dataTable;
       }
       public static DataTable LayThongTinChuVu()
       {
           DataTable dataTable = globalVariables.P_DataChuVu.Copy();
      
           return dataTable;
       }
       public static DataTable LayThongTinQuocGia()
       {
           DataTable dataTable = globalVariables.p_DataQuocGia.Copy();
           return dataTable;
       }
       /// <summary>
       /// hàm thực hiện lấy thông tin của quận huyện
       /// </summary>
       /// <param name="MaTP"></param>
       /// <returns></returns>
       public static DataTable LayThongTinQuanHuyen(string MaTP)
       {
           SqlQuery sqlQuery = new Select().From(DDviDchinh.Schema)
               .Where(DDviDchinh.Columns.LoaiDviDchinh).IsEqualTo("QH").And(DDviDchinh.Columns.MaCha).IsEqualTo(MaTP)
               .OrderAsc(DDviDchinh.Columns.SttHthi);
           return sqlQuery.ExecuteDataSet().Tables[0];
       }
       /// <summary>
       /// hàm thực hiện việc lấy thôgn tin của phường xã
       /// </summary>
       /// <param name="MaQuanHuyen"></param>
       /// <returns></returns>
       public static DataTable LayThongTinPhuongXa(string MaQuanHuyen)
       {
           SqlQuery sqlQuery = new Select().From(DDviDchinh.Schema)
               .Where(DDviDchinh.Columns.LoaiDviDchinh).IsEqualTo("PX").And(DDviDchinh.Columns.MaCha).IsEqualTo(MaQuanHuyen)
               .OrderAsc(DDviDchinh.Columns.SttHthi);
           return sqlQuery.ExecuteDataSet().Tables[0];
       }
       /// <summary>
       /// hàm thực heiẹn việc lấy thông tin đối tượng
       /// </summary>
       /// <returns></returns>
       public static DataTable LayThongtinDoiTuong()
       {
           SqlQuery sqlQuery = new Select().From(DDoiTuong.Schema)
              .OrderAsc(DDoiTuong.Columns.SttHthi);
           return sqlQuery.ExecuteDataSet().Tables[0];
       }
       public static string TenLoaiDichVu(int IdLoaiDV)
       {
           string TenLoaiDV = "";
           DLoaiDvu objDLoaiDvu =
                   DLoaiDvu.FetchByID(IdLoaiDV);
           if (objDLoaiDvu != null)
           {
               TenLoaiDV = Utility.sDbnull(objDLoaiDvu.TenLoaiDvu, "");
           }
           return TenLoaiDV;
       }
       public static DataTable LayThongDichVu()
       {
           DataTable dataTable=new DataTable();
           DataTable DataLoaiDV=new DataTable();
          // DataLoaiDV = LayThongTinLoaiDichVu();
           SqlQuery sqlQuery = new Select(DDichVu.Columns.TenDvu,DDichVu.Columns.IdVungKs,DDichVu.Columns.TrangThai,DDichVu.Columns.SttHthi,DLoaiDvu.Columns.MaLoaiDvu,DDichVu.Columns.IdDvu, DDichVu.Columns.MaDvu,DDichVu.Columns.IdLoaiDvu,
                                          DLoaiDvu.Columns.TenLoaiDvu).From(DDichVu.Schema)
               .LeftOuterJoin(DLoaiDvu.IdLoaiDvuColumn, DDichVu.IdLoaiDvuColumn);
             
           dataTable = sqlQuery.ExecuteDataSet().Tables[0];
           return dataTable;
       }
       public static DataTable LayThongTinNgheNghiep(string MaLoai)
       {
           SqlQuery sqlQuery = new Select().From(DDmucChung.Schema)
               .Where(DDmucChung.Columns.Loai).IsEqualTo(MaLoai)
               .OrderAsc(DDmucChung.Columns.SttHthi);
           return sqlQuery.ExecuteDataSet().Tables[0];
       }
       /// <summary>
       /// lấy thông tin của loại dịch vụ
       /// </summary>
       /// <returns></returns>
       public static DataTable LayThongTinLoaiDichVu()
       {
           SqlQuery sqlQuery = new Select().From(DLoaiDvu.Schema)

              .OrderAsc(DLoaiDvu.Columns.SttHthi);
           return sqlQuery.ExecuteDataSet().Tables[0];
       }
       /// <summary>
       /// hàm thực hiện việc lấy thông tin của vùng khảo sát
       /// </summary>
       /// <returns></returns>
       public static DataTable LayThongTinVungKS()
       {
           SqlQuery sqlQuery = new Select().From(DVungKsat.Schema)

              .OrderAsc(DVungKsat.Columns.TenVungKs);
           return sqlQuery.ExecuteDataSet().Tables[0];
       }
       /// <summary>
       /// HÀM THỰC HIỆN VIỆC Llấy thông tin của khoa
       /// </summary>
       /// <returns></returns>
       public static  DataTable LayThongTinKhoa()
       {
           SqlQuery sqlQuery = new Select().From(DPhongBan.Schema)
               .Where(DPhongBan.Columns.KieuPban).IsEqualTo("KHOA")
               .OrderAsc(DPhongBan.Columns.SttHthi);
           return sqlQuery.ExecuteDataSet().Tables[0];
       }
       /// <summary>
       /// LẤY THÔNG TIN của khoa gửi thông tin 
       /// </summary>
       /// <returns></returns>
       public static DataTable LayThongTinKhoaGUI()
       {
           SqlQuery sqlQuery = new Select().From(DPhongBan.Schema)
               .Where(DPhongBan.Columns.KieuPban).IsEqualTo("KHOA")
               .And(DPhongBan.Columns.LoaiPban).IsEqualTo("GUI")
               .OrderAsc(DPhongBan.Columns.SttHthi);
           return sqlQuery.ExecuteDataSet().Tables[0];
       }
       /// <summary>
       /// HÀM THỰC HIỆN LẤY THÔNG TIN CỦA KHOA THỰC HIỆN
       /// </summary>
       /// <returns></returns>
       public static DataTable LayThongTinKhoaThucHien()
       {
           SqlQuery sqlQuery = new Select().From(DPhongBan.Schema)
               .Where(DPhongBan.Columns.KieuPban).IsEqualTo("KHOA")
               .And(DPhongBan.Columns.LoaiPban).IsEqualTo("THUCHIEN")
               .OrderAsc(DPhongBan.Columns.SttHthi);
           return sqlQuery.ExecuteDataSet().Tables[0];
       }
       /// <summary>
       /// HÀM THỰC HIỆN VIỆC LẤY THÔNG TIN CỦA PHÒNG THỰC HIỆN
       /// </summary>
       /// <returns></returns>
       public static DataTable LayThongTinPhongThucHien()
       {
           SqlQuery sqlQuery = new Select().From(DPhongBan.Schema)
               .Where(DPhongBan.Columns.KieuPban).IsEqualTo("PHONG")
               .And(DPhongBan.Columns.LoaiPban).IsEqualTo("THUCHIEN")
               .OrderAsc(DPhongBan.Columns.SttHthi);
           return sqlQuery.ExecuteDataSet().Tables[0];
       }
       public static DataTable LayThongTinUserNotExistUser()
       {
           SqlQuery sqlQueryNhanVien = new Select(DNhanVien.Columns.Uid).From(DNhanVien.Schema);
              
           SqlQuery sqlQuery = new Select().From(SysUser.Schema)
              .Where(SysUser.Columns.PkSuid).NotIn(sqlQueryNhanVien)
              .OrderAsc(SysUser.Columns.PkSuid);
           return sqlQuery.ExecuteDataSet().Tables[0];
       }
       /// <summary>
       /// hàm thực hiện lấy thông tin của User
       /// </summary>
       /// <returns></returns>
       public static DataTable LayThongTinUser()
       {
          // SqlQuery sqlQueryNhanVien = new Select(DNhanVien.Columns.Uid).From(DNhanVien.Schema);

           SqlQuery sqlQuery = new Select().From(SysUser.Schema)
              .OrderAsc(SysUser.Columns.PkSuid);
           return sqlQuery.ExecuteDataSet().Tables[0];
       }
       /// <summary>
       /// /hàm thực hiện lấy thông tin của thiết bị
       /// </summary>
       /// <returns></returns>
       public static DataTable LayThongTinThietBi()
       {
           // SqlQuery sqlQueryNhanVien = new Select(DNhanVien.Columns.Uid).From(DNhanVien.Schema);

           SqlQuery sqlQuery = new Select().From(DDmucChung.Schema)
               .Where(DDmucChung.Columns.TrangThai).IsEqualTo(1)
               .And(DDmucChung.Columns.Loai).IsEqualTo("THIET_BI")
              .OrderAsc(DDmucChung.Columns.SttHthi);
           return sqlQuery.ExecuteDataSet().Tables[0];
       }
       /// <summary>
       /// hàm thực hiện việc lấy thông tin thiết bị theo khoa phòng
       /// </summary>
       /// <param name="IDKhoaThucHien"></param>
       /// <returns></returns>
       public static DataTable LayThongTinThietBiTheoPhong(int IDKhoaThucHien)
       {
           // SqlQuery sqlQueryNhanVien = new Select(DNhanVien.Columns.Uid).From(DNhanVien.Schema);
           SqlQuery sqlQueryPBanTBi = new Select(DPbanThietbi.Columns.MaTbi)
               .From(DPbanThietbi.Schema);
           if(!globalVariables.IsAdmin)
           {
               sqlQueryPBanTBi.Where(DPbanThietbi.Columns.IdKhoa).IsEqualTo(IDKhoaThucHien);
           }
           SqlQuery sqlQuery = new Select().From(DDmucChung.Schema)
               .Where(DDmucChung.Columns.TrangThai).IsEqualTo(1)
               .And(DDmucChung.Columns.Loai).IsEqualTo("THIET_BI")
               .And(DDmucChung.Columns.Ma).In(sqlQueryPBanTBi)
               .OrderAsc(DDmucChung.Columns.SttHthi);
           return sqlQuery.ExecuteDataSet().Tables[0];
       }
       public static DLoaiDvuCollection LayThongTinKieuDichVu()
       {
         

           SqlQuery sqlQuery = new Select().From(DLoaiDvu.Schema)
               .OrderAsc(DLoaiDvu.Columns.SttHthi);

          
           return sqlQuery.ExecuteAsCollection<DLoaiDvuCollection>();
       }
       /// <summary>
       /// hàm thực hiện lấy thông tin PID
       /// </summary>
       /// <returns></returns>
       public static string SinhPID()
       {
          return Utility.GetFormatDateTime(BusinessHelper.GetSysDateTime(), "yyMMddhhssmm");
       }
       /// <summary>
       /// hàm thực hiện lấy tham số hệ thống
       /// </summary>
       /// <param name="sName"></param>
       /// <returns></returns>
       public static SysSystemParameter GetSysParamater(string sName)
       {
           SqlQuery sqlQuery = new Select().From(SysSystemParameter.Schema)
               .Where(SysSystemParameter.Columns.SName).IsEqualTo(sName);
           SysSystemParameter objParameter = sqlQuery.ExecuteSingle<SysSystemParameter>();
           return objParameter;
       }
      // public static DataTable 
    }
}
