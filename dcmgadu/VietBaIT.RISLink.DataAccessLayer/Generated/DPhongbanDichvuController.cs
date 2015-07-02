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
    /// Controller class for D_PHONGBAN_DICHVU
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class DPhongbanDichvuController
    {
        // Preload our schema..
        DPhongbanDichvu thisSchemaLoad = new DPhongbanDichvu();
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
        public DPhongbanDichvuCollection FetchAll()
        {
            DPhongbanDichvuCollection coll = new DPhongbanDichvuCollection();
            Query qry = new Query(DPhongbanDichvu.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DPhongbanDichvuCollection FetchByID(object IdLoaiDvu)
        {
            DPhongbanDichvuCollection coll = new DPhongbanDichvuCollection().Where("ID_LOAI_DVU", IdLoaiDvu).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public DPhongbanDichvuCollection FetchByQuery(Query qry)
        {
            DPhongbanDichvuCollection coll = new DPhongbanDichvuCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdLoaiDvu)
        {
            return (DPhongbanDichvu.Delete(IdLoaiDvu) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdLoaiDvu)
        {
            return (DPhongbanDichvu.Destroy(IdLoaiDvu) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(int IdLoaiDvu,int IdDvu,int IdKhoa)
        {
            Query qry = new Query(DPhongbanDichvu.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("IdLoaiDvu", IdLoaiDvu).AND("IdDvu", IdDvu).AND("IdKhoa", IdKhoa);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int IdLoaiDvu,int IdDvu,int IdKhoa,string MaLoaiDvu,string MaDvu,string TenLoaiDvu,string TenDvu,string MaKieuDvu)
	    {
		    DPhongbanDichvu item = new DPhongbanDichvu();
		    
            item.IdLoaiDvu = IdLoaiDvu;
            
            item.IdDvu = IdDvu;
            
            item.IdKhoa = IdKhoa;
            
            item.MaLoaiDvu = MaLoaiDvu;
            
            item.MaDvu = MaDvu;
            
            item.TenLoaiDvu = TenLoaiDvu;
            
            item.TenDvu = TenDvu;
            
            item.MaKieuDvu = MaKieuDvu;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int IdLoaiDvu,int IdDvu,int IdKhoa,string MaLoaiDvu,string MaDvu,string TenLoaiDvu,string TenDvu,string MaKieuDvu)
	    {
		    DPhongbanDichvu item = new DPhongbanDichvu();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdLoaiDvu = IdLoaiDvu;
				
			item.IdDvu = IdDvu;
				
			item.IdKhoa = IdKhoa;
				
			item.MaLoaiDvu = MaLoaiDvu;
				
			item.MaDvu = MaDvu;
				
			item.TenLoaiDvu = TenLoaiDvu;
				
			item.TenDvu = TenDvu;
				
			item.MaKieuDvu = MaKieuDvu;
				
	        item.Save(UserName);
	    }
    }
}
