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
    /// Controller class for RIS_PHIEU_CDINH
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class RisPhieuCdinhController
    {
        // Preload our schema..
        RisPhieuCdinh thisSchemaLoad = new RisPhieuCdinh();
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
        public RisPhieuCdinhCollection FetchAll()
        {
            RisPhieuCdinhCollection coll = new RisPhieuCdinhCollection();
            Query qry = new Query(RisPhieuCdinh.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public RisPhieuCdinhCollection FetchByID(object IdPhieu)
        {
            RisPhieuCdinhCollection coll = new RisPhieuCdinhCollection().Where("ID_PHIEU", IdPhieu).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public RisPhieuCdinhCollection FetchByQuery(Query qry)
        {
            RisPhieuCdinhCollection coll = new RisPhieuCdinhCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdPhieu)
        {
            return (RisPhieuCdinh.Delete(IdPhieu) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdPhieu)
        {
            return (RisPhieuCdinh.Destroy(IdPhieu) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaPhieu,int IdBnhan,string Pid,DateTime NgayDky,int? IdKhoaCd,string BsCdinh,int? LoaiPhieu,int? IdKhoaTao,string NguoiTao,DateTime NgayTao,string NguoiSua,DateTime? NgaySua,string MaPhieuHis,string MtaThem,string StudyInstanseUid,string MaPhong,string SourcePatientid,int AutoGen)
	    {
		    RisPhieuCdinh item = new RisPhieuCdinh();
		    
            item.MaPhieu = MaPhieu;
            
            item.IdBnhan = IdBnhan;
            
            item.Pid = Pid;
            
            item.NgayDky = NgayDky;
            
            item.IdKhoaCd = IdKhoaCd;
            
            item.BsCdinh = BsCdinh;
            
            item.LoaiPhieu = LoaiPhieu;
            
            item.IdKhoaTao = IdKhoaTao;
            
            item.NguoiTao = NguoiTao;
            
            item.NgayTao = NgayTao;
            
            item.NguoiSua = NguoiSua;
            
            item.NgaySua = NgaySua;
            
            item.MaPhieuHis = MaPhieuHis;
            
            item.MtaThem = MtaThem;
            
            item.StudyInstanseUid = StudyInstanseUid;
            
            item.MaPhong = MaPhong;
            
            item.SourcePatientid = SourcePatientid;
            
            item.AutoGen = AutoGen;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int IdPhieu,string MaPhieu,int IdBnhan,string Pid,DateTime NgayDky,int? IdKhoaCd,string BsCdinh,int? LoaiPhieu,int? IdKhoaTao,string NguoiTao,DateTime NgayTao,string NguoiSua,DateTime? NgaySua,string MaPhieuHis,string MtaThem,string StudyInstanseUid,string MaPhong,string SourcePatientid,int AutoGen)
	    {
		    RisPhieuCdinh item = new RisPhieuCdinh();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdPhieu = IdPhieu;
				
			item.MaPhieu = MaPhieu;
				
			item.IdBnhan = IdBnhan;
				
			item.Pid = Pid;
				
			item.NgayDky = NgayDky;
				
			item.IdKhoaCd = IdKhoaCd;
				
			item.BsCdinh = BsCdinh;
				
			item.LoaiPhieu = LoaiPhieu;
				
			item.IdKhoaTao = IdKhoaTao;
				
			item.NguoiTao = NguoiTao;
				
			item.NgayTao = NgayTao;
				
			item.NguoiSua = NguoiSua;
				
			item.NgaySua = NgaySua;
				
			item.MaPhieuHis = MaPhieuHis;
				
			item.MtaThem = MtaThem;
				
			item.StudyInstanseUid = StudyInstanseUid;
				
			item.MaPhong = MaPhong;
				
			item.SourcePatientid = SourcePatientid;
				
			item.AutoGen = AutoGen;
				
	        item.Save(UserName);
	    }
    }
}
