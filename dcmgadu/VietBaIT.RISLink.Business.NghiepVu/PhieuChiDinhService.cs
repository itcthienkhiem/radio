using System;
using System.Collections;
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
   
   public class PhieuChiDinhService
    {
       private NLog.Logger log;
       public PhieuChiDinhService()
       {
           log = LogManager.GetCurrentClassLogger();

       }
       /// <summary>
       /// hàm thực hiện việc thêm mới thôn tin của phiéu điều trị
       /// </summary>
       /// <param name="objPhieuCdinh"></param>
       /// <param name="arrPhieuChiTiet"></param>
       /// <param name="IDPhieu"></param>
       /// <returns></returns>
       public ActionResult InsertPhieuChiDinh(RisPhieuCdinh objPhieuCdinh, RisPhieuCdinhCtiet[] arrPhieuChiTiet, ref int IDPhieu,ref string SoPhieu)
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
                       log.Info("Lay thong tin cua so phieu lon nhat co trong db voi id_phieu=" + objPhieuCdinh.IdPhieu);
                       SqlQuery sqlQuery = new Select(SubSonic.Aggregate.Max(RisPhieuCdinh.Columns.IdPhieu)).From(RisPhieuCdinh.Schema)
                           .Where(RisPhieuCdinh.Columns.IdBnhan).IsEqualTo(objPhieuCdinh.IdBnhan)
                           .And(RisPhieuCdinh.Columns.Pid).IsEqualTo(objPhieuCdinh.Pid);
                       int MaxPhieu = Utility.Int32Dbnull(sqlQuery.ExecuteScalar(),-1);
                       log.Info("Bat dau kiem tra so phieu xem co dung cua benh nhan do khong");
                       if (MaxPhieu != objPhieuCdinh.IdPhieu)
                       {
                           VietBaIT.CommonLibrary.ErrorLog.InsertInfolog("Mã phiếu khác nhau, thì thực hiện gán cái vừa lấy được cho cái cũ",
                                                             Assembly.GetExecutingAssembly().GetName().Name,
                                                             Assembly.GetExecutingAssembly().FullName);
                          
                           objPhieuCdinh.IdPhieu = MaxPhieu;
                           log.Info("Thuc hien viec kiem tra xem so phieu da ton tai chua,neu co rui thi lay id =" + objPhieuCdinh.IdPhieu);
                       }
                       sqlQuery = new Select().From(RisPhieuCdinh.Schema)
                           .Where(RisPhieuCdinh.Columns.MaPhieu).IsEqualTo(objPhieuCdinh.MaPhieu)
                           .And(RisPhieuCdinh.Columns.IdPhieu).IsNotEqualTo(objPhieuCdinh.IdPhieu);
                       VietBaIT.CommonLibrary.ErrorLog.InsertInfolog("Kiểm tra xem có trùng mã phiếu không, nếu trùng thì thực hiện cập nhập lại số phiếu khác",
                                                            Assembly.GetExecutingAssembly().GetName().Name,
                                                            Assembly.GetExecutingAssembly().FullName);
                       log.Info("Kiem tra xem so phieu da ton tai chua");
                       if (sqlQuery.GetRecordCount() > 0)
                       {
                        
                           objPhieuCdinh.MaPhieu = BusinessHelper.LayMaPhieu();
                           new Update(RisPhieuCdinh.Schema)
                               .Set(RisPhieuCdinh.Columns.MaPhieu).EqualTo(objPhieuCdinh.MaPhieu)
                               .Where(RisPhieuCdinh.Columns.IdPhieu).IsEqualTo(objPhieuCdinh.IdPhieu).Execute();
                           log.Info("Neu ton tai rui thi cap lai thong tin cua so phieu, voi so phieu moi la=" + objPhieuCdinh.MaPhieu);

                       }
                       log.Info("Bat dau insert thon tin chi tiet");
                       foreach (RisPhieuCdinhCtiet objPhieuCdinhCtiet in arrPhieuChiTiet)
                       {
                           log.Info("Insert thong tin voi IDPhieu=" + objPhieuCdinh.IdPhieu+" va iddich vu="+objPhieuCdinhCtiet.IdDvu);
                           objPhieuCdinhCtiet.IdPhieu = objPhieuCdinh.IdPhieu;
                           objPhieuCdinhCtiet.IsNew = true;
                           objPhieuCdinhCtiet.Save();
                       }
                   }
                   log.Info("Hoan thanh viec them moi so phieu");
                   scope.Complete();
                   IDPhieu = objPhieuCdinh.IdPhieu;
                   SoPhieu = objPhieuCdinh.MaPhieu;
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
       /// hàm thực hiện việc cập nhập thogn tin của chỉ định
       /// </summary>
       /// <param name="objPhieuCdinh"></param>
       /// <param name="arrPhieuChiTiet"></param>
       /// <returns></returns>
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
                           .Set(RisPhieuCdinh.Columns.BsCdinh).EqualTo(objPhieuCdinh.BsCdinh)
                           .Where(RisPhieuCdinh.Columns.IdPhieu).IsEqualTo(objPhieuCdinh.IdPhieu).Execute();
                       new Delete().From(RisPhieuCdinhCtiet.Schema)
                           .Where(RisPhieuCdinhCtiet.Columns.IdPhieu).IsEqualTo(objPhieuCdinh.IdPhieu)
                           .AndExpression(RisPhieuCdinhCtiet.Columns.TrangThai).IsEqualTo(0).Or(RisPhieuCdinhCtiet.Columns.TrangThai)
                           .IsNull().CloseExpression()
                           .Execute();
                       foreach (RisPhieuCdinhCtiet objPhieuCdinhCtiet in arrPhieuChiTiet)
                       {

                           if (Utility.Int32Dbnull(objPhieuCdinhCtiet.TrangThai,0) > 0) continue;
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


       /// <summary>
       /// hàm thực hiện việc xác nhận thông tin chỉ định
       /// </summary>
       /// <param name="arrPhieuChiTiet"></param>
       /// <returns></returns>
       public ActionResult XacNhanPhieuChiDinh(RisPhieuCdinhCtiet arrPhieuChiTiet)
       {
           Query _Query = RisPhieuCdinh.CreateQuery();
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                       new Update(RisPhieuCdinhCtiet.Schema)
                           .Set(RisPhieuCdinhCtiet.Columns.TrangThai).EqualTo(1)
                           .Set(RisPhieuCdinhCtiet.Columns.KyThuat).EqualTo(arrPhieuChiTiet.KyThuat)
                           .Set(RisPhieuCdinhCtiet.Columns.MoTa).EqualTo(arrPhieuChiTiet.MoTa)
                           .Set(RisPhieuCdinhCtiet.Columns.KetLuan).EqualTo(arrPhieuChiTiet.KetLuan)
                           .Set(RisPhieuCdinhCtiet.Columns.DeNghi).EqualTo(arrPhieuChiTiet.DeNghi)
                           .Set(RisPhieuCdinhCtiet.Columns.NguoiThien).EqualTo(globalVariables.UserName)
                           .Set(RisPhieuCdinhCtiet.Columns.NgayThien).EqualTo(BusinessHelper.GetSysDateTime())
                           .Set(RisPhieuCdinhCtiet.Columns.IdKhoaThien).EqualTo(globalVariables.gv_DepartmentID)
                           .Where(RisPhieuCdinhCtiet.Columns.IdPhieuCtiet).IsEqualTo(arrPhieuChiTiet.IdPhieuCtiet).
                           Execute();
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
       /// <summary>
       /// hàm thực hiện việc kết thúc phiếu chỉ định
       /// </summary>
       /// <param name="arrPhieuChiTiet"></param>
       /// <returns></returns>
       public ActionResult KetThucPhieuChiDinh(RisPhieuCdinhCtiet arrPhieuChiTiet)
       {
           Query _Query = RisPhieuCdinh.CreateQuery();
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                       new Update(RisPhieuCdinhCtiet.Schema)
                           .Set(RisPhieuCdinhCtiet.Columns.TrangThai).EqualTo(2)
                           .Set(RisPhieuCdinhCtiet.Columns.KyThuat).EqualTo(arrPhieuChiTiet.KyThuat)
                           .Set(RisPhieuCdinhCtiet.Columns.MoTa).EqualTo(arrPhieuChiTiet.MoTa)
                           .Set(RisPhieuCdinhCtiet.Columns.KetLuan).EqualTo(arrPhieuChiTiet.KetLuan)
                           .Set(RisPhieuCdinhCtiet.Columns.DeNghi).EqualTo(arrPhieuChiTiet.DeNghi)
                           .Set(RisPhieuCdinhCtiet.Columns.NguoiKthuc).EqualTo(globalVariables.UserName)
                           .Set(RisPhieuCdinhCtiet.Columns.NgayKthuc).EqualTo(BusinessHelper.GetSysDateTime())
                           .Where(RisPhieuCdinhCtiet.Columns.IdPhieuCtiet).IsEqualTo(arrPhieuChiTiet.IdPhieuCtiet).
                           Execute();
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

       /// <summary>
       /// hàm thực hiện việc hủy thông tin của phiếu chỉ định
       /// </summary>
       /// <param name="IdPhieuChiTiet"></param>
       /// <returns></returns>
       public ActionResult HuyPhieuChiDinh(int  IdPhieuChiTiet)
       {
           Query _Query = RisPhieuCdinh.CreateQuery();
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                       new Update(RisPhieuCdinhCtiet.Schema)
                           .Set(RisPhieuCdinhCtiet.Columns.TrangThai).EqualTo(1)
                           .Set(RisPhieuCdinhCtiet.Columns.NgayKthuc).EqualTo(null)
                           .Set(RisPhieuCdinhCtiet.Columns.NguoiKthuc).EqualTo(null)

                           .Where(RisPhieuCdinhCtiet.Columns.IdPhieuCtiet).IsEqualTo(IdPhieuChiTiet).
                           Execute();
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

       public ActionResult DaInPhieu(int IdPhieuChiTiet)
       {
           Query _Query = RisPhieuCdinh.CreateQuery();
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                       new Update(RisPhieuCdinhCtiet.Schema)
                           .Set(RisPhieuCdinhCtiet.Columns.DaIn).EqualTo(1)

                           .Where(RisPhieuCdinhCtiet.Columns.IdPhieuCtiet).IsEqualTo(IdPhieuChiTiet).
                           Execute();
                   }
                   scope.Complete();
                   //IDPhieu = objPhieuCdinh.IdPhieu;
                   return ActionResult.Success;
               }
           }
           catch (Exception exception)
           {
               log.Error("Loi trong qua trinh cập nhập thông tin in phiếu {0}", exception.ToString());
               VietBaIT.CommonLibrary.ErrorLog.InsertErrorlog(exception.ToString(),
                                                              Assembly.GetExecutingAssembly().GetName().Name,
                                                              Assembly.GetExecutingAssembly().FullName);
               return ActionResult.Error;
           }
       }
       /// <summary>
       /// hàm thực hiện xóa thông tin của phiếu
       /// </summary>
       /// <returns></returns>
       public ActionResult XoaChiTietphieu(RisPhieuCdinhCtiet []arrPhieuChiTiet)
       {
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                       
                       foreach (RisPhieuCdinhCtiet objDetail in arrPhieuChiTiet)
                       {
                           RisPhieuCdinhCtiet.Delete(objDetail.IdPhieuCtiet);
                           SqlQuery sqlQuery = new Select().From(RisPhieuCdinhCtiet.Schema)
                               .Where(RisPhieuCdinhCtiet.Columns.IdPhieu).IsEqualTo(objDetail.IdPhieu);
                           if(sqlQuery.GetRecordCount()<=0)
                           {
                               RisPhieuCdinh.Delete(objDetail.IdPhieu);
                           }
                       }
                   }
                   scope.Complete();
                   return ActionResult.Success;
               }
           }
           catch (Exception exception)
           {
               log.Error("Loi trong qua trinh xóa chi tiết phiếu {0}", exception.ToString());
               VietBaIT.CommonLibrary.ErrorLog.InsertErrorlog(exception.ToString(),
                                                              Assembly.GetExecutingAssembly().GetName().Name,
                                                              Assembly.GetExecutingAssembly().FullName);
               return ActionResult.Error;
           }
       }


       /// <summary>
       /// hàm thực hiện xóa thông tin của phiếu
       /// </summary>
       /// <returns></returns>
       public ActionResult XoaChiTietphieu(RisPhieuCdinhCtiet objPhieuCdinhCtiet)
       {
           try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {

                       RisPhieuCdinhCtiet.Delete(objPhieuCdinhCtiet.IdPhieuCtiet);

                           SqlQuery sqlQuery = new Select().From(RisPhieuCdinhCtiet.Schema)
                               .Where(RisPhieuCdinhCtiet.Columns.IdPhieu).IsEqualTo(objPhieuCdinhCtiet.IdPhieu);
                           if (sqlQuery.GetRecordCount() <= 0)
                           {
                               RisPhieuCdinh.Delete(objPhieuCdinhCtiet.IdPhieu);
                           }
                   }
                   scope.Complete();
                   return ActionResult.Success;
               }
           }
           catch (Exception exception)
           {
               log.Error("Loi trong qua trinh xóa chi tiết phiếu {0}", exception.ToString());
               VietBaIT.CommonLibrary.ErrorLog.InsertErrorlog(exception.ToString(),
                                                              Assembly.GetExecutingAssembly().GetName().Name,
                                                              Assembly.GetExecutingAssembly().FullName);
               return ActionResult.Error;
           }
       }
       private string ArrayToString(ArrayList arr)
       {
           return Utility.ConvertArrayListToString(arr);


       }
       /// <summary>
       /// hàm thực hiện việc xử lý thông tin của xử lý thông tin của phần chỉ định ngang
       /// </summary>
       /// <param name="m_dtPhieuChiDinh">Phiếu chỉ định</param>
       /// <param name="dtTemps">Bảng tạm để thực hiện việc xử lý thông tin </param>
       public void ProcessChiDinhNgang(ref  DataTable m_dtPhieuChiDinh, DataTable dtTemps)
       {
           Utility.AddColumToDataTable(ref m_dtPhieuChiDinh, "TEN_DVU", typeof(string));
           foreach (DataRow drv in m_dtPhieuChiDinh.Rows)
           {
               DataRow[] arrDr =
                   dtTemps.Select(string.Format("{0}={1}", RisPhieuCdinh.Columns.IdPhieu, Utility.Int32Dbnull(drv[RisPhieuCdinh.Columns.IdPhieu])));
               if (arrDr.GetLength(0) > 0)
               {

                   ArrayList arrayList = new ArrayList();

                   foreach (DataRow dr in arrDr)
                   {
                       arrayList.Add(dr["TEN_DVU"]);
                   }
                   string strDichVu = ArrayToString(arrayList);
                   if (!String.IsNullOrEmpty(strDichVu))
                   {
                       drv["TEN_DVU"] = strDichVu;
                   }
               }
           }
           m_dtPhieuChiDinh.AcceptChanges();
       }
       /// <summary>
       /// hàm thực hiện việc chuyển phòng ban
       /// </summary>
       /// <param name="objPhieuCdinh"></param>
       /// <returns></returns>
       public ActionResult ChuyenPhongThucHien(RisPhieuCdinh objPhieuCdinh)
       {
            try
           {
               using (var scope = new TransactionScope())
               {
                   using (var sh = new SharedDbConnectionScope())
                   {
                       log.Info("Bạn chuyen thong tin dieu huong phong voi ID=" + objPhieuCdinh.IdKhoaThien);
                       log.Info("Bạn chuyen thong tin dieu huong ma thiet bi voi ID=" + objPhieuCdinh.MaTbi);
                       SqlQuery sqlQuery;
                       if(globalVariables.gv_ExistsThietBi)
                       {
                           new Update(RisPhieuCdinh.Schema)
                          .Set(RisPhieuCdinh.Columns.IdKhoaThien).EqualTo(objPhieuCdinh.IdKhoaThien)
                          .Set(RisPhieuCdinh.Columns.MaTbi).EqualTo(objPhieuCdinh.MaTbi)
                          .Where(RisPhieuCdinh.Columns.IdPhieu).IsEqualTo(objPhieuCdinh.IdPhieu).Execute();
                       }
                       else
                       {
                           new Update(RisPhieuCdinh.Schema)
                         .Set(RisPhieuCdinh.Columns.IdKhoaThien).EqualTo(objPhieuCdinh.IdKhoaThien)
                        // .Set(RisPhieuCdinh.Columns.MaTbi).EqualTo(objPhieuCdinh.MaTbi)
                         .Where(RisPhieuCdinh.Columns.IdPhieu).IsEqualTo(objPhieuCdinh.IdPhieu).Execute();
                       }
                     
                       
                   }
                   scope.Complete();
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
