using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Transactions;
using SubSonic;
using VIETBAIT.CONFIG;
using VietBaIT.CommonLibrary;
using VietBaIT.RISLink.DataAccessLayer;

namespace VIETBAIT.DICOMSCP
{
    public static class DicomScpHelper
    {
        private static readonly Config Config = new Config();

        /// <summary>
        /// Trả về Thư mục lưu trữ ảnh 
        /// </summary>
        /// <returns></returns>
        public static String GetstoreLocation()
        {
            return Config.GetValueFromKey("imagepath");
        }

        /// <summary>
        /// Trả về AETitle của ứng dụng
        /// </summary>
        /// <returns></returns>
        public static String GetAeTitle()
        {
            return Config.GetValueFromKey("serveraetitle");
        }

        /// <summary>
        /// Trả về AETitle của ứng dụng
        /// </summary>
        /// <returns></returns>
        public static String GetMaKieuDv()
        {
            return Config.GetValueFromKey("makieudv");
        }

        /// <summary>
        /// Trả về port mà ứng dụng Listen
        /// </summary>
        /// <returns></returns>
        public static int GetPort()
        {
            return Convert.ToInt32(Config.GetValueFromKey("serverport"));
        }

        /// <summary>
        /// Trả về Rislink Connection
        /// </summary>
        public static void GetRisLinkConnection()
        {
            var rislinkPath = Config.GetValueFromKey("rislinkpath");
            string rislinkConfigFilename;
            if (!rislinkPath.EndsWith(".config"))
                rislinkConfigFilename = rislinkPath + Path.DirectorySeparatorChar + "App.config";
            else
                rislinkConfigFilename = rislinkPath;

            if (!File.Exists(rislinkConfigFilename)) return;
            var risConfig = new Config(rislinkConfigFilename);

            var serverName = risConfig.GetValueFromKey("risservername");
            var dataBaseName = risConfig.GetValueFromKey("risdatabase");
            var userName = risConfig.GetValueFromKey("risusername");
            var password = risConfig.GetValueFromKey("rispassword");

            var connectionstring = string.Empty;
            connectionstring = connectionstring + "Data Source=" + serverName + ";";
            connectionstring = connectionstring + "Initial Catalog=" + dataBaseName + ";";
            connectionstring = connectionstring + "User ID=" + userName + ";";
            connectionstring = connectionstring + "Password=" + password + ";";
            globalVariables.SqlConnectionString = connectionstring;
            Utility.InitSubSonic(globalVariables.SqlConnectionString, "ORM");
        }

        #region Import benh nhan
        /// <summary>
        /// Thực hiện thêm mới thông tin không nhập qua phần tiếp đón
        /// </summary>
        /// <param name="TenBenhNhan">Tên bệnh nhân</param>
        /// <param name="GTinh"> </param>
        /// <param name="StudyInstanseUid">StudyInstanseUid</param>
        /// <param name="SERIES_INSTANCE_UID">SERIES_INSTANCE_UID</param>
        /// <param name="SOP_INSTANCE_UID">SOP_INSTANCE_UID</param>
        /// <param name="BodyPart"> </param>
        /// <param name="Position"> </param>
        /// <param name="PathImage">Đường dẫn ảnh</param>
        /// <param name="PathImage_thumb"> </param>
        /// <param name="IdDichVu">ID dịch vụ </param>
        /// <returns></returns>
        public static void ThemBenhNhan(string TenBenhNhan, string GTinh, string StudyInstanseUid,
                                        string SERIES_INSTANCE_UID, string SOP_INSTANCE_UID, string BodyPart,
                                        string Position, string PathImage, string PathImage_thumb,
                                        string sourcePatientId)
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
                        var objBenhNhan = new RisBenhNhan();
                        SqlQuery sqlQuery = new Select().From(RisPhieuCdinh.Schema)
                            .Where(RisPhieuCdinh.Columns.StudyInstanseUid).IsEqualTo(StudyInstanseUid);
                        var objPhieuCdinh = new RisPhieuCdinh();

                        if (sqlQuery.GetRecordCount() <= 0)
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
                            objPhieuCdinh.SourcePatientid = sourcePatientId;
                            objPhieuCdinh.IsNew = true;
                            objPhieuCdinh.Save();
                            objPhieuCdinh.IdPhieu =
                                Utility.Int32Dbnull(_QuerySoPhieu.GetMax(RisPhieuCdinh.Columns.IdPhieu));
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
                            .And(RisPhieuCdinhCtiet.Columns.StudyInstanceUid).IsEqualTo(StudyInstanseUid);
                        //.And(RisPhieuCdinhCtiet.Columns.SopInstanceUid).IsEqualTo(SOP_INSTANCE_UID);
                        var objRisPhieuCdinhCtiet = new RisPhieuCdinhCtiet();
                        if (sqlQuery.GetRecordCount() <= 0)
                        {
                            SqlQuery sqlQueryDV = new Select().From(DDichVu.Schema)
                                .Where(DDichVu.Columns.MaDvu).IsEqualTo(Position)
                                .And(DDichVu.Columns.IdLoaiDvu).In(
                                    new Select(DLoaiDvu.Columns.IdLoaiDvu).From(DLoaiDvu.Schema)
                                        .Where(DLoaiDvu.Columns.MaLoaiDvu).
                                        IsEqualTo(BodyPart));
                            var objDichVu = sqlQueryDV.ExecuteSingle<DDichVu>();
                            if (objDichVu != null)
                            {
                                objRisPhieuCdinhCtiet.IdDvu = Utility.Int32Dbnull(objDichVu.IdDvu, -1);
                                if (objDichVu != null)
                                {
                                    objRisPhieuCdinhCtiet.IdLoaiDvu = objDichVu.IdLoaiDvu;
                                    objRisPhieuCdinhCtiet.GhiChu = Utility.sDbnull(objDichVu.MoTa, "");
                                    SqlQuery sqlQueryVungKS = new Select().From(DVungKsat.Schema)
                                        .Where(DVungKsat.Columns.IdVungKs).IsEqualTo(objDichVu.IdVungKs);
                                    var objDVungKsat = sqlQueryVungKS.ExecuteSingle<DVungKsat>();
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
                            objRisPhieuCdinhCtiet.MaKieuDvu = globalVariables.MaKieuDV;
                            objRisPhieuCdinhCtiet.MaDvu = Position;
                            objRisPhieuCdinhCtiet.MaLoaiDvu = BodyPart;
                            objRisPhieuCdinhCtiet.MaTbi = string.Empty;
                            objRisPhieuCdinhCtiet.IdKhoaThien = -1;
                            objRisPhieuCdinhCtiet.TrangThaiImage = 1;
                            // objRisPhieuCdinhCtiet.MaKieuDvu = globalVariables.;
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
                        if (sqlQuery.GetRecordCount() <= 0)
                        {
                            new Update(RisLuuAnh.Schema)
                                .Set(RisLuuAnh.Columns.TrangThai).EqualTo(0)
                                .Where(RisPhieuCdinhCtiet.Columns.IdPhieuCtiet).IsEqualTo(
                                    objRisPhieuCdinhCtiet.IdPhieuCtiet).Execute();
                            var objLuuAnh = new RisLuuAnh();
                            objLuuAnh.DdanAnh = PathImage;
                            objLuuAnh.DdanAnhThumb = PathImage_thumb;
                            objLuuAnh.IdPhieuCtiet = Utility.Int64Dbnull(objRisPhieuCdinhCtiet.IdPhieuCtiet, -1);
                            objLuuAnh.SeriesInstanceUid = objRisPhieuCdinhCtiet.SeriesInstanceUid;
                            objLuuAnh.SopInstanceUid = SOP_INSTANCE_UID;
                            objLuuAnh.NgayTao = sysDateTime;
                            objLuuAnh.TrangThai = 1;
                            string[] strings = SOP_INSTANCE_UID.Split('.');
                            int loz;
                            try
                            {
                                loz = Convert.ToInt32(strings[strings.Length - 1]);
                            }
                            catch (Exception)
                            {
                                loz = 0;
                            }
                            objLuuAnh.Stt = loz;
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
                    //  OrderImage(SERIES_INSTANCE_UID);
                    scope.Complete();
                    return;
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public static RisBenhNhan ThemMoiBenhNhan(string TenBenhNhan, string Gtinh)
        {
            DateTime sysDateTime = BusinessHelper.GetSysDateTime();
            Query query = RisBenhNhan.CreateQuery();
            var objBenhNhan = new RisBenhNhan();
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
            objBenhNhan.IdBnhan = Utility.Int32Dbnull(query.GetMax(RisBenhNhan.Columns.IdBnhan), -1);
            SqlQuery sqlQuery = new Select().From(RisBenhNhan.Schema)
                .Where(RisBenhNhan.Columns.IdBnhan).IsNotEqualTo(objBenhNhan.IdBnhan)
                .And(RisBenhNhan.Columns.Pid).IsEqualTo(objBenhNhan.Pid);

            if (sqlQuery.GetRecordCount() > 0)
            {
                objBenhNhan.Pid = CommonBusiness.SinhPID();
                new Update(RisBenhNhan.Schema)
                    .Set(RisBenhNhan.Columns.Pid).EqualTo(objBenhNhan.Pid)
                    .Where(RisBenhNhan.Columns.IdBnhan).IsEqualTo(objBenhNhan.IdBnhan).Execute();
            }
            return objBenhNhan;
        }

        /// <summary>
        /// Hàm lấy thông tin bệnh nhân. Trả về một đối tượng nếu tồn tại hoặc được tạo mới, trả về null nếu chưa tồn tại
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="tenBenhNhan"></param>
        /// <param name="gtinh"></param>
        /// <returns></returns>
        public static RisBenhNhan GetBenhNhan(string pid, string tenBenhNhan, string gtinh)
        {
            try
            {
                //B1: Kiểm tra xem bệnh nhân đã tồn tại chưa ? bằng khóa pid
                var tempBns =
                    new RisBenhNhanController().FetchByQuery(RisBenhNhan.CreateQuery().WHERE(RisBenhNhan.Columns.Pid,
                                                                                             Comparison.Equals, pid));
                //Nếu đã tồn tại bệnh nhân với pid truyền vào thì trả về đúng bệnh nhân đó.
                if ((tempBns != null) && (tempBns.Count > 0))
                {
                    var risBenhNhan = tempBns[0];
                    risBenhNhan.TrangThaiGui = 0;
                    risBenhNhan.Save();
                    return risBenhNhan;
                }

                //Nếu chưa tồn tại thêm bệnh nhân mới với pid và thông tin nhận được
                var sysDateTime = BusinessHelper.GetSysDateTime();
                var objBenhNhan = new RisBenhNhan();
                objBenhNhan.Pid = pid;
                objBenhNhan.TenBnhan = tenBenhNhan;
                objBenhNhan.TenBnhanKdau = Utility.UnSignedCharacter(tenBenhNhan);
                objBenhNhan.MaDtuong = "KHAC";
                objBenhNhan.BvTrangthai = 0;
                objBenhNhan.TrangThaiGui = 0;
                objBenhNhan.Gtinh = gtinh;
                objBenhNhan.TenBnhanKdau = tenBenhNhan;
                objBenhNhan.NamSinh = sysDateTime.Year;
                objBenhNhan.NgayTao = sysDateTime;
                objBenhNhan.AutoGen = 1;
                objBenhNhan.IsNew = true;
                objBenhNhan.Save();
                return objBenhNhan;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Trả về đối tượng Phiếu chỉ đinh trong trường hợp đã tồn tại hoặc tạo mới thành công. Null nếu ko tạo mới thành công
        /// </summary>
        /// <param name="oBenhNhan"></param>
        /// <param name="maPhieu"></param>
        /// <returns></returns>
        public static RisPhieuCdinh GetPhieuChiDinh(RisBenhNhan oBenhNhan, string maPhieu)
        {
            try
            {
                //B1: Kiểm tra xem đã tồn tại phiếu hay chưa ? bằng mã phiếu
                var tempPhieu =
                    new RisPhieuCdinhController().FetchByQuery(
                        RisPhieuCdinh.CreateQuery().WHERE(RisPhieuCdinh.Columns.MaPhieu, Comparison.Equals, maPhieu));
                if ((tempPhieu != null) && (tempPhieu.Count > 0))
                {
                    var risPhieuCdinh = tempPhieu[0];
                    risPhieuCdinh.SourcePatientid = oBenhNhan.Pid;
                    risPhieuCdinh.Save();
                    return risPhieuCdinh;
                }

                //Nếu chưa tồn tại phiếu thì thêm mới.
                var sysDateTime = BusinessHelper.GetSysDateTime();
                var objPhieuCdinh = new RisPhieuCdinh();
                objPhieuCdinh.IdBnhan = oBenhNhan.IdBnhan;
                //Nếu không có accession no thì sinh mã mới :D
                objPhieuCdinh.MaPhieu = string.IsNullOrEmpty(maPhieu.Trim())
                                            ? BusinessHelper.LayMaPhieu()
                                            : maPhieu.Trim();
                objPhieuCdinh.Pid = oBenhNhan.Pid;
                objPhieuCdinh.LoaiPhieu = 1;
                objPhieuCdinh.NgayDky = sysDateTime;
                objPhieuCdinh.NgayTao = sysDateTime;
                objPhieuCdinh.NguoiTao = "Service";
                objPhieuCdinh.SourcePatientid = oBenhNhan.Pid;
                objPhieuCdinh.AutoGen = 1;
                objPhieuCdinh.IsNew = true;
                objPhieuCdinh.Save();
                return objPhieuCdinh;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Trả về đối tượng chỉ định chi tiết nếu đã có chỉ định hoặc tạo mới thành công.Null nếu tạo mới không thành công
        /// </summary>
        /// <param name="oPhieuCdinh"></param>
        /// <param name="aeTitle"> </param>
        /// <param name="bodyPart"> </param>
        /// <param name="viewpos"> </param>
        /// <param name="studyInstanseUid"> </param>
        /// <param name="seriesInstanceUid"> </param>
        /// <param name="sopInstanceUid"> </param>
        /// <returns></returns>
        public static RisPhieuCdinhCtiet GetPhieuChiDinhChiTiet(RisPhieuCdinh oPhieuCdinh,
                                                                string aeTitle, string bodyPart, string viewpos,
                                                                string studyInstanseUid, string seriesInstanceUid,
                                                                string sopInstanceUid)
        {
            try
            {
                var o = new RisPhieuCdinhCtiet();
                //B1:Lấy về tất cả các chỉ định của phiếu chỉ định
                DataTable dt = SPs.SpGetTestListForInsert2(oPhieuCdinh.MaPhieu, aeTitle).GetDataSet().Tables[0];
                //Nếu có chỉ định thì tìm kiếm để lấy về số phiếu
                if (dt.Rows.Count > 0)
                {
                    //Lọc tìm phiếu chỉ định chi tiết
                    DataRow[] dataRows =
                        dt.Select(string.Format("BodyPart = '{0}' and ViewPos = '{1}'", bodyPart, viewpos));
                    //Nếu tìm thấy thì lấy luôn số phiếu đầu tiên
                    if (dataRows.Length > 0)
                    {
                        var phieuChiDinhChiTiet = new RisPhieuCdinhCtiet(dataRows[0]["ID_PHIEU_CTIET"]);
                        phieuChiDinhChiTiet.BodyPart = bodyPart;
                        phieuChiDinhChiTiet.ViewPos = viewpos;
                        phieuChiDinhChiTiet.Save();
                        return phieuChiDinhChiTiet;
                    }
                }
                //Bổ sung bước kiểm tra SOPID

                RisPhieuCdinhCtietCollection tempChitiets =
                    new RisPhieuCdinhCtietController().FetchByQuery(
                        RisPhieuCdinhCtiet.CreateQuery().WHERE(RisPhieuCdinhCtiet.Columns.StudyInstanceUid,
                                                               Comparison.Equals, studyInstanseUid).AND(
                                                                   RisPhieuCdinhCtiet.Columns.SeriesInstanceUid,
                                                                   Comparison.Equals, seriesInstanceUid).AND(
                                                                       RisPhieuCdinhCtiet.Columns.SopInstanceUid,
                                                                       Comparison.Equals, sopInstanceUid));
                if (tempChitiets.Count > 0)
                {
                    var phieuChiDinhChiTiet = tempChitiets[0];
                    phieuChiDinhChiTiet.BodyPart = bodyPart;
                    phieuChiDinhChiTiet.ViewPos = viewpos;
                    phieuChiDinhChiTiet.Save();
                    return phieuChiDinhChiTiet;
                }


                //Nếu không tìm thấy phiếu nào thì phải tạo phiếu chi tiết mới
                o.IdPhieu = oPhieuCdinh.IdPhieu;
                o.IdDvu = -1;
                int idThietBi =
                    Utility.Int32Dbnull(
                        new Select(DThietBi.IdThietBiColumn).From(DThietBi.Schema).Where(DThietBi.AETitleColumn).
                            IsEqualTo(aeTitle).ExecuteScalar(), -1);

                //Nếu id THiết bị <>-1
                //Lấy mã dịch vụ từ AETitle, bodypart và viewpos
                //Nếu chưa tồn tại bodypart và viewpos trong bảng mã điều khiển thì tự thêm mới.
                if (idThietBi != -1)
                {
                    // Nếu tồn tại thiết bị lấy về obj mã đk Thiết bị
                    DMaDieuKhienThietBiCollection objmdks = new DMaDieuKhienThietBiController().
                        FetchByQuery(DMaDieuKhienThietBi.CreateQuery().
                                         WHERE(DMaDieuKhienThietBi.Columns.IdThietBi, Comparison.Equals, idThietBi).
                                         AND(
                                             DMaDieuKhienThietBi.Columns.BodyPart, Comparison.Equals, bodyPart).AND(
                                                 DMaDieuKhienThietBi.Columns.ViewPos, Comparison.Equals, viewpos));
                    if (objmdks != null)
                    {
                        // Nếu tồn tại bodypart và viewpos thì lấy ra dịch vụ
                        if (objmdks.Count > 0)
                        {
                            o.IdDvu = Utility.Int32Dbnull(objmdks.First().IdDvu, -1);
                        }
                            //Nếu không tồn tại thì insert mã điều khiển mới
                        else
                        {
                            var objmdk = new DMaDieuKhienThietBi();
                            objmdk.IdDvu = -1;
                            objmdk.IdThietBi = idThietBi;
                            objmdk.BodyPart = bodyPart;
                            objmdk.ViewPos = viewpos;
                            objmdk.TrangThai = 1;
                            objmdk.Desc = string.Format("{0}-{1}", bodyPart, viewpos);
                            objmdk.Save();
                        }
                    }
                }
                //Xử lý trường hợp nếu idDvu =-1 gán về loại chưa xác định (loại có trạng thái =0)
                DDichVu objDichVu = null;
                if (o.IdDvu == -1)
                {
                    DDichVuCollection tempdvs =
                        new DDichVuController().FetchByQuery(DDichVu.CreateQuery().WHERE(DDichVu.Columns.TrangThai,
                                                                                         Comparison.Equals, 0));
                    if (tempdvs.Count > 0)
                    {
                        objDichVu = tempdvs[0];
                        o.IdDvu = objDichVu.IdDvu;
                    }
                    else
                    {
                        objDichVu = new DDichVu(o.IdDvu);
                    }
                }
                else
                {
                    objDichVu = new DDichVu(o.IdDvu);
                }

                o.MaDvu = Utility.sDbnull(objDichVu.MaDvu);
                o.Sluong = 1;
                o.DonGia = Utility.DecimaltoDbnull(objDichVu.DonGia, 0);
                o.DaIn = 0;
                o.AutoGen = 1;
                o.IdLoaiDvu = Utility.Int32Dbnull(objDichVu.IdLoaiDvu, -1);
                o.MaLoaiDvu =
                    Utility.sDbnull(
                        new Select(DLoaiDvu.MaLoaiDvuColumn).From(DLoaiDvu.Schema).Where(DLoaiDvu.IdLoaiDvuColumn).
                            IsEqualTo(o.IdLoaiDvu).ExecuteScalar());
                o.TrangThai = 0;
                o.IdKhoaThien = -1;
                o.MaKieuDvu = "";
                o.BodyPart = bodyPart;
                o.ViewPos = viewpos;
                o.Save();

                return o;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion
    }
}