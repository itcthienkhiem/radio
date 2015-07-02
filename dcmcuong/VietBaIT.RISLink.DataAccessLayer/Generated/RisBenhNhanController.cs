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
    /// Controller class for RIS_BENH_NHAN
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class RisBenhNhanController
    {
        // Preload our schema..
        RisBenhNhan thisSchemaLoad = new RisBenhNhan();
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
        public RisBenhNhanCollection FetchAll()
        {
            RisBenhNhanCollection coll = new RisBenhNhanCollection();
            Query qry = new Query(RisBenhNhan.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public RisBenhNhanCollection FetchByID(object IdBnhan)
        {
            RisBenhNhanCollection coll = new RisBenhNhanCollection().Where("ID_BNHAN", IdBnhan).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public RisBenhNhanCollection FetchByQuery(Query qry)
        {
            RisBenhNhanCollection coll = new RisBenhNhanCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdBnhan)
        {
            return (RisBenhNhan.Delete(IdBnhan) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdBnhan)
        {
            return (RisBenhNhan.Destroy(IdBnhan) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Pid,string TenBnhan,string TenBnhanKdau,string Gtinh,DateTime? NgaySinh,int? NamSinh,string DiaChi,string MaTpho,string MaQhuyen,string MaNnghep,string SoDthoai,string Cmt,string MaDtuong,string SoBhyt,int? IdKhoa,string SoPhong,string SoGiuong,int? BvTrangthai,int? TrangThaiGui,string ChanDoan,DateTime NgayTao,string NguoiTao,DateTime? NgaySua,string NguoiSua,string MaBenh,string MaVphi,int AutoGen)
	    {
		    RisBenhNhan item = new RisBenhNhan();
		    
            item.Pid = Pid;
            
            item.TenBnhan = TenBnhan;
            
            item.TenBnhanKdau = TenBnhanKdau;
            
            item.Gtinh = Gtinh;
            
            item.NgaySinh = NgaySinh;
            
            item.NamSinh = NamSinh;
            
            item.DiaChi = DiaChi;
            
            item.MaTpho = MaTpho;
            
            item.MaQhuyen = MaQhuyen;
            
            item.MaNnghep = MaNnghep;
            
            item.SoDthoai = SoDthoai;
            
            item.Cmt = Cmt;
            
            item.MaDtuong = MaDtuong;
            
            item.SoBhyt = SoBhyt;
            
            item.IdKhoa = IdKhoa;
            
            item.SoPhong = SoPhong;
            
            item.SoGiuong = SoGiuong;
            
            item.BvTrangthai = BvTrangthai;
            
            item.TrangThaiGui = TrangThaiGui;
            
            item.ChanDoan = ChanDoan;
            
            item.NgayTao = NgayTao;
            
            item.NguoiTao = NguoiTao;
            
            item.NgaySua = NgaySua;
            
            item.NguoiSua = NguoiSua;
            
            item.MaBenh = MaBenh;
            
            item.MaVphi = MaVphi;
            
            item.AutoGen = AutoGen;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int IdBnhan,string Pid,string TenBnhan,string TenBnhanKdau,string Gtinh,DateTime? NgaySinh,int? NamSinh,string DiaChi,string MaTpho,string MaQhuyen,string MaNnghep,string SoDthoai,string Cmt,string MaDtuong,string SoBhyt,int? IdKhoa,string SoPhong,string SoGiuong,int? BvTrangthai,int? TrangThaiGui,string ChanDoan,DateTime NgayTao,string NguoiTao,DateTime? NgaySua,string NguoiSua,string MaBenh,string MaVphi,int AutoGen)
	    {
		    RisBenhNhan item = new RisBenhNhan();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdBnhan = IdBnhan;
				
			item.Pid = Pid;
				
			item.TenBnhan = TenBnhan;
				
			item.TenBnhanKdau = TenBnhanKdau;
				
			item.Gtinh = Gtinh;
				
			item.NgaySinh = NgaySinh;
				
			item.NamSinh = NamSinh;
				
			item.DiaChi = DiaChi;
				
			item.MaTpho = MaTpho;
				
			item.MaQhuyen = MaQhuyen;
				
			item.MaNnghep = MaNnghep;
				
			item.SoDthoai = SoDthoai;
				
			item.Cmt = Cmt;
				
			item.MaDtuong = MaDtuong;
				
			item.SoBhyt = SoBhyt;
				
			item.IdKhoa = IdKhoa;
				
			item.SoPhong = SoPhong;
				
			item.SoGiuong = SoGiuong;
				
			item.BvTrangthai = BvTrangthai;
				
			item.TrangThaiGui = TrangThaiGui;
				
			item.ChanDoan = ChanDoan;
				
			item.NgayTao = NgayTao;
				
			item.NguoiTao = NguoiTao;
				
			item.NgaySua = NgaySua;
				
			item.NguoiSua = NguoiSua;
				
			item.MaBenh = MaBenh;
				
			item.MaVphi = MaVphi;
				
			item.AutoGen = AutoGen;
				
	        item.Save(UserName);
	    }
    }
}
