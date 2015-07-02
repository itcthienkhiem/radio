using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SubSonic;
using System.Data;
using NLog;
using System.Transactions;
using VietBaIT.CommonLibrary;
using VietBaIT.RISLink.DataAccessLayer;
namespace VietBaIT.RISLink.Business.NghiepVu
{
   public class TIEPDON_BENHNHAN
   {
       private NLog.Logger log;

       public TIEPDON_BENHNHAN()
       {
           log = LogManager.GetCurrentClassLogger();

       }
       /// <summary>
       /// ham thực hiện việc thêm mới thông tni bệnh nhân
       /// </summary>
       /// <param name="objBenhNhan"></param>
       /// <param name="objKhamBnhan"></param>
       /// <returns></returns>
       public ActionResult ThemMoiBenhNhan(RisBenhNhan objBenhNhan,ref int IDBenhNhan,ref string PID)
       {
           Query _Query = RisBenhNhan.CreateQuery();
           Query _QueryPhieu = RisPhieuCdinh.CreateQuery();
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                       objBenhNhan.IsNew = true;
                       objBenhNhan.Save();
                       objBenhNhan.IdBnhan = Utility.Int32Dbnull(_Query.GetMax(RisBenhNhan.Columns.IdBnhan), -1);
                       log.Info("Them thong tin benh nhan cua ris voi ID_BenhNhan=" + objBenhNhan.IdBnhan);
                       SqlQuery sqlQuery = new Select().From(RisBenhNhan.Schema)
                           .Where(RisBenhNhan.Columns.IdBnhan).IsNotEqualTo(objBenhNhan.IdBnhan)
                           .And(RisBenhNhan.Columns.Pid).IsEqualTo(objBenhNhan.Pid);
                       log.Info(
                           "Kiem tra thong tin xem benh nhan voi PID da co chua, neu chua co thi commit, neu co rui thi thuc hien sinh lai");

                       if(sqlQuery.GetRecordCount()>0)
                       {
                           objBenhNhan.Pid = VietBaIT.CommonLibrary.CommonBusiness.SinhPID();
                           log.Info("Neu ton tai PID thi cap nhap lai PID voi ma moi nhat PID=" + objBenhNhan.Pid);
                        
                           new Update(RisBenhNhan.Schema)
                               .Set(RisBenhNhan.Columns.Pid).EqualTo(objBenhNhan.Pid)
                               .Where(RisBenhNhan.Columns.IdBnhan).IsEqualTo(objBenhNhan.IdBnhan).Execute();

                       }
                   }
                   log.Info("Hoan thanh viec them moi benh nhan");
                   scope.Complete();
                   IDBenhNhan = objBenhNhan.IdBnhan;
                   PID = objBenhNhan.Pid;
                   return ActionResult.Success;
               }
           }
           catch (Exception exception)
           {
               log.Error("Loi trong qua trinh them moi benh nhan {0}", exception.ToString());
               VietBaIT.CommonLibrary.ErrorLog.InsertErrorlog(exception.ToString(),
                                                             Assembly.GetExecutingAssembly().GetName().Name,
                                                             Assembly.GetExecutingAssembly().FullName);
               return ActionResult.Error;

           }
           
       }
       /// <summary>
       /// hàm thực hienj viecj sửa mơi bệnh nhân
       /// </summary>
       /// <param name="objBenhNhan"></param>
       /// <returns></returns>
       public ActionResult SuaMoiBenhNhan(RisBenhNhan objBenhNhan)
       {
           Query _Query = RisBenhNhan.CreateQuery();
           Query _QueryPhieu = RisPhieuCdinh.CreateQuery();
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                       new Update(RisBenhNhan.Schema)
                           .Set(RisBenhNhan.Columns.TenBnhanKdau).EqualTo(objBenhNhan.TenBnhanKdau)
                           .Set(RisBenhNhan.Columns.TenBnhan).EqualTo(objBenhNhan.TenBnhan)
                           .Set(RisBenhNhan.Columns.Gtinh).EqualTo(objBenhNhan.Gtinh)
                           .Set(RisBenhNhan.Columns.NgayTao).EqualTo(objBenhNhan.NgayTao)
                           .Set(RisBenhNhan.Columns.SoBhyt).EqualTo(objBenhNhan.SoBhyt)
                           .Set(RisBenhNhan.Columns.ChanDoan).EqualTo(objBenhNhan.ChanDoan)
                           .Set(RisBenhNhan.Columns.MaDtuong).EqualTo(objBenhNhan.MaDtuong)
                           .Set(RisBenhNhan.Columns.MaQhuyen).EqualTo(objBenhNhan.MaQhuyen)
                           .Set(RisBenhNhan.Columns.MaTpho).EqualTo(objBenhNhan.MaTpho)
                           .Set(RisBenhNhan.Columns.DiaChi).EqualTo(objBenhNhan.DiaChi)
                           .Set(RisBenhNhan.Columns.NamSinh).EqualTo(objBenhNhan.NamSinh)
                           .Set(RisBenhNhan.Columns.NgaySinh).EqualTo(objBenhNhan.NgaySinh)
                           .Set(RisBenhNhan.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                           .Set(RisBenhNhan.Columns.NgaySua).EqualTo(BusinessHelper.GetSysDateTime())
                           .Set(RisBenhNhan.Columns.TrangThaiGui).EqualTo(objBenhNhan.TrangThaiGui)
                           .Set(RisBenhNhan.Columns.BvTrangthai).EqualTo(objBenhNhan.BvTrangthai)
                           .Set(RisBenhNhan.Columns.IdKhoa).EqualTo(objBenhNhan.IdKhoa)
                           .Set(RisBenhNhan.Columns.SoPhong).EqualTo(objBenhNhan.SoPhong)
                           .Set(RisBenhNhan.Columns.SoGiuong).EqualTo(objBenhNhan.SoGiuong)
                           .Where(RisBenhNhan.Columns.IdBnhan).IsEqualTo(objBenhNhan.IdBnhan)
                           .Execute();
                       log.Info("Thuc hien them moi thong tin cua benh nhan voi ID_BNhan=" + objBenhNhan.IdBnhan);


                   }
                   scope.Complete();
                   //IDBenhNhan = objBenhNhan.IdBnhan;
                   return ActionResult.Success;
               }
           }
           catch (Exception exception)
           {
               log.Error("Loi trong qua trinh them moi benh nhan {0}", exception.ToString());
               VietBaIT.CommonLibrary.ErrorLog.InsertErrorlog(exception.ToString(),
                                                             Assembly.GetExecutingAssembly().GetName().Name,
                                                             Assembly.GetExecutingAssembly().FullName);
               return ActionResult.Error;

           }

       }
       /// <summary>
       /// hàm thực hiện việc lấy thông tin của bệnh nhân
       /// </summary>
       /// <param name="IDBenhNhan"></param>
       /// <param name="PID"></param>
       /// <param name="TenBenhNhan"></param>
       /// <param name="Gtinh"></param>
       /// <param name="Ma_DTuong"></param>
       /// <param name="fromDate"></param>
       /// <param name="toDate"></param>
       /// <returns></returns>
       public DataTable TimKiemThongTinBenhNhan(int IDBenhNhan,string PID,string TenBenhNhan,string Gtinh,string Ma_DTuong,DateTime fromDate,DateTime toDate)
       {
           DataTable dataTable=new DataTable();
           try
           {
               dataTable=
                   SPs.TimThongtinBenhnhan(IDBenhNhan, PID, TenBenhNhan, Gtinh, fromDate, toDate,Ma_DTuong).GetDataSet().Tables[0];
           }
           catch (Exception)
           {
               
              // throw;
           }
           return dataTable;
       }
      /// <summary>
      /// hàm thực hiện việc lấy thông tin của phếu chỉ định chi tiết
      /// </summary>
      /// <param name="PID"></param>
      /// <param name="IDBNhan"></param>
      /// <returns></returns>
       public DataTable LayThongtinPhieuChiDinh(string PID,int IDBNhan)
       {
           return SPs.RisLayPhieuChiTiet(IDBNhan, PID).GetDataSet().Tables[0];
       }

       public ActionResult XoaThongTinPhieu(RisPhieuCdinhCtiet[] arrPhieuChiTiet)
       {
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                       foreach (RisPhieuCdinhCtiet objPhieuCdinhCtiet in arrPhieuChiTiet)
                       {
                           RisPhieuCdinhCtiet.Delete(objPhieuCdinhCtiet.IdPhieuCtiet);
                           SqlQuery sqlQuery = new Select().From(RisPhieuCdinhCtiet.Schema)
                               .Where(RisPhieuCdinhCtiet.Columns.IdPhieu).IsEqualTo(objPhieuCdinhCtiet.IdPhieu);
                           log.Info("Xoa thong tin cua chi tiet ="+objPhieuCdinhCtiet.IdPhieuCtiet+" nguoi xoa la="+globalVariables.UserName);
                           if(sqlQuery.GetRecordCount()<=0)
                           {
                               log.Info("kiem tra phieu bi xoa het thi xoa luon phieu kia di="+objPhieuCdinhCtiet
                                   .IdPhieu);
                               RisPhieuCdinh.Delete(objPhieuCdinhCtiet.IdPhieu);
                           }
                       }
                   }
                   scope.Complete();
                   log.Info("Xoa thong tin thanh cong cua ban ghi="+arrPhieuChiTiet.Count());
                   return ActionResult.Success;
               }
           }
           catch (Exception exception)
           {
               log.Error("Loi trong qua trinh them moi benh nhan {0}", exception.ToString());
               VietBaIT.CommonLibrary.ErrorLog.InsertErrorlog(exception.ToString(),
                                                              Assembly.GetExecutingAssembly().GetName().Name,
                                                              Assembly.GetExecutingAssembly().FullName);

               return ActionResult.Error;

           }
       }

       public ActionResult InsertPhieuChiDinh(RisPhieuCdinh objPhieuCdinh, RisPhieuCdinhCtiet[] arrPhieuChiTiet,ref int IDPhieu)
       {
           Query _Query = RisPhieuCdinh.CreateQuery();
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                       objPhieuCdinh.MaPhieu = BusinessHelper.LayMaPhieu();
                       objPhieuCdinh.IsNew = true;
                       objPhieuCdinh.Save();
                       objPhieuCdinh.IdPhieu = Utility.Int32Dbnull(_Query.GetMax(RisPhieuCdinh.Columns.IdPhieu), -1);
                       SqlQuery sqlQuery = new Select().From(RisPhieuCdinh.Schema)
                           .Where(RisPhieuCdinh.Columns.MaPhieu).IsEqualTo(objPhieuCdinh.MaPhieu)
                           .And(RisPhieuCdinh.Columns.IdPhieu).IsNotEqualTo(objPhieuCdinh.IdPhieu);
                       if(sqlQuery.GetRecordCount()>0)
                       {
                           new Update(RisPhieuCdinh.Schema)
                               .Set(RisPhieuCdinh.Columns.MaPhieu).EqualTo(BusinessHelper.LayMaPhieu())
                               .Where(RisPhieuCdinh.Columns.IdPhieu).IsEqualTo(objPhieuCdinh.IdPhieu).Execute();

                       }
                       foreach (RisPhieuCdinhCtiet objPhieuCdinhCtiet in arrPhieuChiTiet)
                       {
                           objPhieuCdinhCtiet.IdPhieu = objPhieuCdinh.IdPhieu;
                           objPhieuCdinhCtiet.IsNew = true;
                           objPhieuCdinhCtiet.Save();
                       }
                   }
                   scope.Complete();
                   IDPhieu = objPhieuCdinh.IdPhieu;
                   return ActionResult.Success;
               }
           }
           catch (Exception exception)
           {
               log.Error("Loi trong qua trinh them moi benh nhan {0}", exception.ToString());
               VietBaIT.CommonLibrary.ErrorLog.InsertErrorlog(exception.ToString(),
                                                              Assembly.GetExecutingAssembly().GetName().Name,
                                                              Assembly.GetExecutingAssembly().FullName);
               return ActionResult.Error;
           }
       }
       public ActionResult UpdatePhieuChiDinh(RisPhieuCdinh objPhieuCdinh, RisPhieuCdinhCtiet[] arrPhieuChiTiet)
       {
           Query _Query = RisPhieuCdinh.CreateQuery();
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                       new Update(RisPhieuCdinh.Schema)
                           .Set(RisPhieuCdinh.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                           .Set(RisPhieuCdinh.Columns.NgaySua).EqualTo(BusinessHelper.GetSysDateTime())
                           .Set(RisPhieuCdinh.Columns.IdKhoaCd).EqualTo(objPhieuCdinh.IdKhoaCd)
                           .Where(RisPhieuCdinh.Columns.IdPhieu).IsEqualTo(objPhieuCdinh.IdPhieu).Execute();
                       new Delete().From(RisPhieuCdinhCtiet.Schema)
                           .Where(RisPhieuCdinhCtiet.Columns.IdPhieu).IsEqualTo(objPhieuCdinh.IdPhieu)
                           .And(RisPhieuCdinhCtiet.Columns.TrangThai).IsEqualTo(0)
                           .Execute();
                       foreach (RisPhieuCdinhCtiet objPhieuCdinhCtiet in arrPhieuChiTiet)
                       {
                          
                           if(objPhieuCdinhCtiet.TrangThai>0)continue;
                               objPhieuCdinhCtiet.IdPhieu = objPhieuCdinh.IdPhieu;
                               objPhieuCdinhCtiet.IsNew = true;
                               objPhieuCdinhCtiet.Save();
                         
                       }
                   }
                   scope.Complete();
                   //IDPhieu = objPhieuCdinh.IdPhieu;
                   return ActionResult.Success;
               }
           }
           catch (Exception exception)
           {
               log.Error("Loi trong qua trinh them moi benh nhan {0}", exception.ToString());
               VietBaIT.CommonLibrary.ErrorLog.InsertErrorlog(exception.ToString(),
                                                              Assembly.GetExecutingAssembly().GetName().Name,
                                                              Assembly.GetExecutingAssembly().FullName);
               return ActionResult.Error;
           }
       }
   }
}
