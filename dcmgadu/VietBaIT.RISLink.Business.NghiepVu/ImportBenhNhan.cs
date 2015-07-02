using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using VietBaIT.CommonLibrary;
using VietBaIT.RISLink.DataAccessLayer;
using System.Transactions;
namespace VietBaIT.RISLink.Business.NghiepVu
{
   public class ImportBenhNhan
    {
       public ImportBenhNhan()
       {
           //;
           Utility.InitSubSonic(new  ConnectionSQL.ConnectionSQL().KhoiTaoKetNoi(), "ORM");
           //globalVariables.UserName=""
       }
     /// <summary>
     /// Thực hiện thêm mới thông tin không nhập qua phần tiếp đón
     /// </summary>
     /// <param name="TenBenhNhan">Tên bệnh nhân</param>
       /// <param name="StudyInstanseUid">StudyInstanseUid</param>
       /// <param name="SERIES_INSTANCE_UID">SERIES_INSTANCE_UID</param>
       /// <param name="SOP_INSTANCE_UID">SOP_INSTANCE_UID</param>
     /// <param name="IdDichVu">ID dịch vụ </param>
     /// <param name="PathImage">Đường dẫn ảnh</param>
     /// <returns></returns>
       public ActionResult ThemBenhNhan(string TenBenhNhan,string GTinh, string StudyInstanseUid, string SERIES_INSTANCE_UID, string SOP_INSTANCE_UID, string BodyPart,string Position, string PathImage,string PathImage_thumb)
       {
           try
           {
               
               Query _QuerySoPhieu = RisPhieuCdinh.CreateQuery();
               Query _QueryChiTiet = RisPhieuCdinhCtiet.CreateQuery();
               using (var scope = new TransactionScope())
               {
                   using (var sp = new SharedDbConnectionScope())
                   {
                       DateTime sysDateTime = BusinessHelper.GetSysDateTime();
                       RisBenhNhan objBenhNhan = new RisBenhNhan();
                       SqlQuery sqlQuery = new Select().From(RisPhieuCdinh.Schema)
                           .Where(RisPhieuCdinh.Columns.StudyInstanseUid).IsEqualTo(StudyInstanseUid);
                       RisPhieuCdinh objPhieuCdinh = new RisPhieuCdinh();
                     
                       if(sqlQuery.GetRecordCount()<=0)
                       {
                               objBenhNhan = ThemMoiBenhNhan(TenBenhNhan, GTinh);
                               objPhieuCdinh.StudyInstanseUid = StudyInstanseUid;
                               objPhieuCdinh.IdBnhan = objBenhNhan.IdBnhan;
                               objPhieuCdinh.MaPhieu = BusinessHelper.LayMaPhieu();
                               objPhieuCdinh.Pid = objBenhNhan.Pid;
                               objPhieuCdinh.LoaiPhieu = 1;
                              // objPhieuCdinh.IdKhoaThien = -1;
                               //objPhieuCdinh.MaTbi = String.Empty;
                               objPhieuCdinh.NgayDky = sysDateTime;
                               objPhieuCdinh.NgayTao = sysDateTime;
                               objPhieuCdinh.NguoiTao = globalVariables.UserName;
                               objPhieuCdinh.IsNew = true;
                               objPhieuCdinh.Save();
                               objPhieuCdinh.IdPhieu = Utility.Int32Dbnull(_QuerySoPhieu.GetMax(RisPhieuCdinh.Columns.IdPhieu));
                          
                       }
                       else
                       {
                           objPhieuCdinh = sqlQuery.ExecuteSingle<RisPhieuCdinh>();
                           new Update(RisBenhNhan.Schema)
                               .Set(RisBenhNhan.Columns.TenBnhan).EqualTo(TenBenhNhan)
                               .Set(RisBenhNhan.Columns.TenBnhanKdau).EqualTo(Utility.UnSignedCharacter(TenBenhNhan))
                               .Set(RisBenhNhan.Columns.Gtinh).EqualTo(GTinh)
                               .Where(RisBenhNhan.Columns.IdBnhan).IsEqualTo(objPhieuCdinh.IdBnhan).Execute();
                           
                       }
                       sqlQuery = new Select().From(RisPhieuCdinhCtiet.Schema)
                           .Where(RisPhieuCdinhCtiet.Columns.IdPhieu).IsEqualTo(objPhieuCdinh.IdPhieu)
                           .And(RisPhieuCdinhCtiet.Columns.SeriesInstanceUid).IsEqualTo(SERIES_INSTANCE_UID)
                           .And(RisPhieuCdinhCtiet.Columns.StudyInstanceUid).IsEqualTo(StudyInstanseUid)
                           .And(RisPhieuCdinhCtiet.Columns.SopInstanceUid).IsEqualTo(SOP_INSTANCE_UID);
                       RisPhieuCdinhCtiet objRisPhieuCdinhCtiet = new RisPhieuCdinhCtiet();
                       if(sqlQuery.GetRecordCount()<=0)
                       {

                           SqlQuery sqlQueryDV = new Select().From(DDichVu.Schema)
                               .Where(DDichVu.Columns.MaDvu).IsEqualTo(Position)
                               .And(DDichVu.Columns.IdLoaiDvu).In(
                                   new Select(DLoaiDvu.Columns.IdLoaiDvu).From(DLoaiDvu.Schema)
                                   .Where(DLoaiDvu.Columns.MaLoaiDvu).
                                       IsEqualTo(BodyPart));
                           DDichVu objDichVu = sqlQueryDV.ExecuteSingle<DDichVu>();
                           if (objDichVu != null)
                           {
                               objRisPhieuCdinhCtiet.IdDvu = Utility.Int32Dbnull(objDichVu.IdDvu, -1);
                               if (objDichVu != null)
                               {
                                   objRisPhieuCdinhCtiet.IdLoaiDvu = objDichVu.IdLoaiDvu;
                                   objRisPhieuCdinhCtiet.GhiChu = Utility.sDbnull(objDichVu.MoTa, "");
                                   SqlQuery sqlQueryVungKS = new Select().From(DVungKsat.Schema)
                                       .Where(DVungKsat.Columns.IdVungKs).IsEqualTo(objDichVu.IdVungKs);
                                   DVungKsat objDVungKsat = sqlQueryVungKS.ExecuteSingle<DVungKsat>();
                                   if (objDVungKsat != null)
                                   {
                                       objRisPhieuCdinhCtiet.VungKs = Utility.sDbnull(objDVungKsat.TenVungKs, "");
                                       objRisPhieuCdinhCtiet.KetLuan = Utility.sDbnull(objDVungKsat.KetLuan, "");
                                       objRisPhieuCdinhCtiet.DeNghi = Utility.sDbnull(objDVungKsat.DeNghi, "");
                                       objRisPhieuCdinhCtiet.KyThuat = Utility.sDbnull(objDVungKsat.KyThuat, "");
                                       objRisPhieuCdinhCtiet.MoTa = Utility.sDbnull(objDVungKsat.MoTa, "");
                                   }
                               }
                           }
                           objRisPhieuCdinhCtiet.Sluong = 1;
                           objRisPhieuCdinhCtiet.DaIn = 0;
                           objRisPhieuCdinhCtiet.SeriesInstanceUid = SERIES_INSTANCE_UID;
                           objRisPhieuCdinhCtiet.SopInstanceUid = SOP_INSTANCE_UID;
                           objRisPhieuCdinhCtiet.StudyInstanceUid = objPhieuCdinh.StudyInstanseUid;
                           // objRisPhieuCdinhCtiet.DdanAnh = PathImage;
                           objRisPhieuCdinhCtiet.MaDvu = Position;
                           objRisPhieuCdinhCtiet.MaLoaiDvu = BodyPart;
                           objRisPhieuCdinhCtiet.MaTbi = string.Empty;
                           objRisPhieuCdinhCtiet.IdKhoaThien = -1;
                           objRisPhieuCdinhCtiet.TrangThaiImage = 1;
                           objRisPhieuCdinhCtiet.MaKieuDvu = "XQ";
                           objRisPhieuCdinhCtiet.TrangThai = 0;
                           objRisPhieuCdinhCtiet.IdPhieu = objPhieuCdinh.IdPhieu;
                           objRisPhieuCdinhCtiet.IsNew = true;
                           objRisPhieuCdinhCtiet.Save();
                           objRisPhieuCdinhCtiet.IdPhieuCtiet =
                               Utility.Int32Dbnull(_QueryChiTiet.GetMax(RisPhieuCdinhCtiet.Columns.IdPhieuCtiet), -1);
                          

                       }
                       else
                       {
                           objRisPhieuCdinhCtiet = sqlQuery.ExecuteSingle<RisPhieuCdinhCtiet>();
                       }
                       sqlQuery = new Select().From(RisLuuAnh.Schema)
                           .Where(RisLuuAnh.Columns.IdPhieuCtiet).IsEqualTo(objRisPhieuCdinhCtiet.IdPhieuCtiet)
                           .And(RisLuuAnh.Columns.SeriesInstanceUid).IsEqualTo(objRisPhieuCdinhCtiet.SeriesInstanceUid)
                           .And(RisLuuAnh.Columns.SopInstanceUid).IsEqualTo(SOP_INSTANCE_UID);
                       if(sqlQuery.GetRecordCount()<=0)
                       {
                           new Update(RisLuuAnh.Schema)
                               .Set(RisLuuAnh.Columns.TrangThai).EqualTo(0)
                               .Where(RisPhieuCdinhCtiet.Columns.IdPhieuCtiet).IsEqualTo(
                                   objRisPhieuCdinhCtiet.IdPhieuCtiet).Execute();
                           RisLuuAnh objLuuAnh = new RisLuuAnh();
                           objLuuAnh.DdanAnh = PathImage;
                           objLuuAnh.DdanAnhThumb = PathImage_thumb;
                           objLuuAnh.IdPhieuCtiet = Utility.Int64Dbnull(objRisPhieuCdinhCtiet.IdPhieuCtiet, -1);
                           objLuuAnh.SeriesInstanceUid = objRisPhieuCdinhCtiet.SeriesInstanceUid;
                           objLuuAnh.SopInstanceUid = SOP_INSTANCE_UID;
                           objLuuAnh.NgayTao = sysDateTime;
                           objLuuAnh.TrangThai = 1;
                           objLuuAnh.IsNew = true;
                           objLuuAnh.Save();

                       }
                       else
                       {
                           new Update(RisLuuAnh.Schema)
                               .Set(RisLuuAnh.Columns.DdanAnh).EqualTo(PathImage)
                               .Set(RisLuuAnh.Columns.NgayTao).EqualTo(sysDateTime)
                               .Where(RisLuuAnh.Columns.IdPhieuCtiet).IsEqualTo(objRisPhieuCdinhCtiet.IdPhieuCtiet)
                               .And(RisLuuAnh.Columns.SeriesInstanceUid).IsEqualTo(
                                   objRisPhieuCdinhCtiet.SeriesInstanceUid)
                               .And(RisLuuAnh.Columns.SopInstanceUid).IsEqualTo(SOP_INSTANCE_UID).Execute();
                       }
                   


                   }
                   scope.Complete();
                   return ActionResult.Success;
               }
           }
           catch (Exception ex)
           {

               return ActionResult.Error;
           }
          
           
       }

       private RisBenhNhan ThemMoiBenhNhan(string TenBenhNhan,string Gtinh)
       {
           DateTime sysDateTime = BusinessHelper.GetSysDateTime();
           Query _Query = RisBenhNhan.CreateQuery();
           RisBenhNhan objBenhNhan=new RisBenhNhan();
           objBenhNhan.Pid = CommonBusiness.SinhPID();
           objBenhNhan.TenBnhan = TenBenhNhan;
           objBenhNhan.TenBnhanKdau = Utility.UnSignedCharacter(TenBenhNhan);
           objBenhNhan.MaDtuong = "KHAC";
           objBenhNhan.BvTrangthai = 0;
           objBenhNhan.TrangThaiGui = 0;
           objBenhNhan.Gtinh = Gtinh;
           objBenhNhan.TenBnhanKdau = TenBenhNhan;
           objBenhNhan.NamSinh = sysDateTime.Year;
           objBenhNhan.NgayTao = sysDateTime;
           objBenhNhan.IsNew = true;
           objBenhNhan.Save();
           objBenhNhan.IdBnhan = Utility.Int32Dbnull(_Query.GetMax(RisBenhNhan.Columns.IdBnhan), -1);
           SqlQuery sqlQuery = new Select().From(RisBenhNhan.Schema)
               .Where(RisBenhNhan.Columns.IdBnhan).IsNotEqualTo(objBenhNhan.IdBnhan)
               .And(RisBenhNhan.Columns.Pid).IsEqualTo(objBenhNhan.Pid);
                      
           if(sqlQuery.GetRecordCount()>0)
           {
               objBenhNhan.Pid = VietBaIT.CommonLibrary.CommonBusiness.SinhPID();
               new Update(RisBenhNhan.Schema)
                   .Set(RisBenhNhan.Columns.Pid).EqualTo(objBenhNhan.Pid)
                   .Where(RisBenhNhan.Columns.IdBnhan).IsEqualTo(objBenhNhan.IdBnhan).Execute();

           }
           return objBenhNhan;
       }

       //public ActionResult UpdateChiDinh()
    }
}
