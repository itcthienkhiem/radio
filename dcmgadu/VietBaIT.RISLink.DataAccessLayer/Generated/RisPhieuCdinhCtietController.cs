using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
// <auto-generated />
namespace VietBaIT.RISLink.DataAccessLayer
{
    /// <summary>
    /// Controller class for RIS_PHIEU_CDINH_CTIET
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class RisPhieuCdinhCtietController
    {
        // Preload our schema..
        RisPhieuCdinhCtiet thisSchemaLoad = new RisPhieuCdinhCtiet();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
				if (userName.Length == 0) 
				{
    				if (System.Web.HttpContext.Current != null)
    				{
						userName=System.Web.HttpContext.Current.User.Identity.Name;
					}
					else
					{
						userName=System.Threading.Thread.CurrentPrincipal.Identity.Name;
					}
				}
				return userName;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public RisPhieuCdinhCtietCollection FetchAll()
        {
            RisPhieuCdinhCtietCollection coll = new RisPhieuCdinhCtietCollection();
            Query qry = new Query(RisPhieuCdinhCtiet.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public RisPhieuCdinhCtietCollection FetchByID(object IdPhieuCtiet)
        {
            RisPhieuCdinhCtietCollection coll = new RisPhieuCdinhCtietCollection().Where("ID_PHIEU_CTIET", IdPhieuCtiet).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public RisPhieuCdinhCtietCollection FetchByQuery(Query qry)
        {
            RisPhieuCdinhCtietCollection coll = new RisPhieuCdinhCtietCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdPhieuCtiet)
        {
            return (RisPhieuCdinhCtiet.Delete(IdPhieuCtiet) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdPhieuCtiet)
        {
            return (RisPhieuCdinhCtiet.Destroy(IdPhieuCtiet) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int IdPhieu,int IdLoaiDvu,int IdDvu,string MaLoaiDvu,string MaDvu,int? Sluong,decimal? DonGia,string DeNghi,string KetLuan,string MoTa,string KyThuat,string VungKs,int? DaIn,string GhiChu,int? IdKhoaThien,string MaTbi,string NguoiThien,DateTime? NgayThien,string NguoiKthuc,DateTime? NgayKthuc,int? TrangThai,int? TrangThaiImage,string StudyInstanceUid,string SeriesInstanceUid,string SopInstanceUid,int? HisIdPhieuCtiet,string MaKieuDvu)
	    {
		    RisPhieuCdinhCtiet item = new RisPhieuCdinhCtiet();
		    
            item.IdPhieu = IdPhieu;
            
            item.IdLoaiDvu = IdLoaiDvu;
            
            item.IdDvu = IdDvu;
            
            item.MaLoaiDvu = MaLoaiDvu;
            
            item.MaDvu = MaDvu;
            
            item.Sluong = Sluong;
            
            item.DonGia = DonGia;
            
            item.DeNghi = DeNghi;
            
            item.KetLuan = KetLuan;
            
            item.MoTa = MoTa;
            
            item.KyThuat = KyThuat;
            
            item.VungKs = VungKs;
            
            item.DaIn = DaIn;
            
            item.GhiChu = GhiChu;
            
            item.IdKhoaThien = IdKhoaThien;
            
            item.MaTbi = MaTbi;
            
            item.NguoiThien = NguoiThien;
            
            item.NgayThien = NgayThien;
            
            item.NguoiKthuc = NguoiKthuc;
            
            item.NgayKthuc = NgayKthuc;
            
            item.TrangThai = TrangThai;
            
            item.TrangThaiImage = TrangThaiImage;
            
            item.StudyInstanceUid = StudyInstanceUid;
            
            item.SeriesInstanceUid = SeriesInstanceUid;
            
            item.SopInstanceUid = SopInstanceUid;
            
            item.HisIdPhieuCtiet = HisIdPhieuCtiet;
            
            item.MaKieuDvu = MaKieuDvu;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(long IdPhieuCtiet,int IdPhieu,int IdLoaiDvu,int IdDvu,string MaLoaiDvu,string MaDvu,int? Sluong,decimal? DonGia,string DeNghi,string KetLuan,string MoTa,string KyThuat,string VungKs,int? DaIn,string GhiChu,int? IdKhoaThien,string MaTbi,string NguoiThien,DateTime? NgayThien,string NguoiKthuc,DateTime? NgayKthuc,int? TrangThai,int? TrangThaiImage,string StudyInstanceUid,string SeriesInstanceUid,string SopInstanceUid,int? HisIdPhieuCtiet,string MaKieuDvu)
	    {
		    RisPhieuCdinhCtiet item = new RisPhieuCdinhCtiet();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdPhieuCtiet = IdPhieuCtiet;
				
			item.IdPhieu = IdPhieu;
				
			item.IdLoaiDvu = IdLoaiDvu;
				
			item.IdDvu = IdDvu;
				
			item.MaLoaiDvu = MaLoaiDvu;
				
			item.MaDvu = MaDvu;
				
			item.Sluong = Sluong;
				
			item.DonGia = DonGia;
				
			item.DeNghi = DeNghi;
				
			item.KetLuan = KetLuan;
				
			item.MoTa = MoTa;
				
			item.KyThuat = KyThuat;
				
			item.VungKs = VungKs;
				
			item.DaIn = DaIn;
				
			item.GhiChu = GhiChu;
				
			item.IdKhoaThien = IdKhoaThien;
				
			item.MaTbi = MaTbi;
				
			item.NguoiThien = NguoiThien;
				
			item.NgayThien = NgayThien;
				
			item.NguoiKthuc = NguoiKthuc;
				
			item.NgayKthuc = NgayKthuc;
				
			item.TrangThai = TrangThai;
				
			item.TrangThaiImage = TrangThaiImage;
				
			item.StudyInstanceUid = StudyInstanceUid;
				
			item.SeriesInstanceUid = SeriesInstanceUid;
				
			item.SopInstanceUid = SopInstanceUid;
				
			item.HisIdPhieuCtiet = HisIdPhieuCtiet;
				
			item.MaKieuDvu = MaKieuDvu;
				
	        item.Save(UserName);
	    }
    }
}
