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
    /// Controller class for KETNOI_PHIEU_CHIDINH
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class KetnoiPhieuChidinhController
    {
        // Preload our schema..
        KetnoiPhieuChidinh thisSchemaLoad = new KetnoiPhieuChidinh();
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
        public KetnoiPhieuChidinhCollection FetchAll()
        {
            KetnoiPhieuChidinhCollection coll = new KetnoiPhieuChidinhCollection();
            Query qry = new Query(KetnoiPhieuChidinh.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public KetnoiPhieuChidinhCollection FetchByID(object Id)
        {
            KetnoiPhieuChidinhCollection coll = new KetnoiPhieuChidinhCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public KetnoiPhieuChidinhCollection FetchByQuery(Query qry)
        {
            KetnoiPhieuChidinhCollection coll = new KetnoiPhieuChidinhCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (KetnoiPhieuChidinh.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (KetnoiPhieuChidinh.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int? HisIdBnhan,string HisMaLanKham,string HisMaPhieu,int? HisIdPhieu,int? HisIdChitietPhieu,int? HisBsyCd,string HisTenBsyCd,int? HisIdDvu,string HisTenDvu,int? HisIdLoaiDvu,string HisTenLoaiDvu,decimal? HisDonGia,int? HisSoLuong,DateTime? HisNgayYcau,string HisNguoiYcau,string HisMaKieuDvu,string HisTenKhoa,int? HisIdKhoa,int? RisIdChitietPhieu,string RisMaPhieu,int? RisIdPhieu,string RisNguoiTao,DateTime? RisNgayTao,int? RisDaTra,DateTime? RisNgaySua,string RisNguoiSua)
	    {
		    KetnoiPhieuChidinh item = new KetnoiPhieuChidinh();
		    
            item.HisIdBnhan = HisIdBnhan;
            
            item.HisMaLanKham = HisMaLanKham;
            
            item.HisMaPhieu = HisMaPhieu;
            
            item.HisIdPhieu = HisIdPhieu;
            
            item.HisIdChitietPhieu = HisIdChitietPhieu;
            
            item.HisBsyCd = HisBsyCd;
            
            item.HisTenBsyCd = HisTenBsyCd;
            
            item.HisIdDvu = HisIdDvu;
            
            item.HisTenDvu = HisTenDvu;
            
            item.HisIdLoaiDvu = HisIdLoaiDvu;
            
            item.HisTenLoaiDvu = HisTenLoaiDvu;
            
            item.HisDonGia = HisDonGia;
            
            item.HisSoLuong = HisSoLuong;
            
            item.HisNgayYcau = HisNgayYcau;
            
            item.HisNguoiYcau = HisNguoiYcau;
            
            item.HisMaKieuDvu = HisMaKieuDvu;
            
            item.HisTenKhoa = HisTenKhoa;
            
            item.HisIdKhoa = HisIdKhoa;
            
            item.RisIdChitietPhieu = RisIdChitietPhieu;
            
            item.RisMaPhieu = RisMaPhieu;
            
            item.RisIdPhieu = RisIdPhieu;
            
            item.RisNguoiTao = RisNguoiTao;
            
            item.RisNgayTao = RisNgayTao;
            
            item.RisDaTra = RisDaTra;
            
            item.RisNgaySua = RisNgaySua;
            
            item.RisNguoiSua = RisNguoiSua;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,int? HisIdBnhan,string HisMaLanKham,string HisMaPhieu,int? HisIdPhieu,int? HisIdChitietPhieu,int? HisBsyCd,string HisTenBsyCd,int? HisIdDvu,string HisTenDvu,int? HisIdLoaiDvu,string HisTenLoaiDvu,decimal? HisDonGia,int? HisSoLuong,DateTime? HisNgayYcau,string HisNguoiYcau,string HisMaKieuDvu,string HisTenKhoa,int? HisIdKhoa,int? RisIdChitietPhieu,string RisMaPhieu,int? RisIdPhieu,string RisNguoiTao,DateTime? RisNgayTao,int? RisDaTra,DateTime? RisNgaySua,string RisNguoiSua)
	    {
		    KetnoiPhieuChidinh item = new KetnoiPhieuChidinh();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.HisIdBnhan = HisIdBnhan;
				
			item.HisMaLanKham = HisMaLanKham;
				
			item.HisMaPhieu = HisMaPhieu;
				
			item.HisIdPhieu = HisIdPhieu;
				
			item.HisIdChitietPhieu = HisIdChitietPhieu;
				
			item.HisBsyCd = HisBsyCd;
				
			item.HisTenBsyCd = HisTenBsyCd;
				
			item.HisIdDvu = HisIdDvu;
				
			item.HisTenDvu = HisTenDvu;
				
			item.HisIdLoaiDvu = HisIdLoaiDvu;
				
			item.HisTenLoaiDvu = HisTenLoaiDvu;
				
			item.HisDonGia = HisDonGia;
				
			item.HisSoLuong = HisSoLuong;
				
			item.HisNgayYcau = HisNgayYcau;
				
			item.HisNguoiYcau = HisNguoiYcau;
				
			item.HisMaKieuDvu = HisMaKieuDvu;
				
			item.HisTenKhoa = HisTenKhoa;
				
			item.HisIdKhoa = HisIdKhoa;
				
			item.RisIdChitietPhieu = RisIdChitietPhieu;
				
			item.RisMaPhieu = RisMaPhieu;
				
			item.RisIdPhieu = RisIdPhieu;
				
			item.RisNguoiTao = RisNguoiTao;
				
			item.RisNgayTao = RisNgayTao;
				
			item.RisDaTra = RisDaTra;
				
			item.RisNgaySua = RisNgaySua;
				
			item.RisNguoiSua = RisNguoiSua;
				
	        item.Save(UserName);
	    }
    }
}
