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
    /// Controller class for D_DVI_DCHINH
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class DDviDchinhController
    {
        // Preload our schema..
        DDviDchinh thisSchemaLoad = new DDviDchinh();
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
        public DDviDchinhCollection FetchAll()
        {
            DDviDchinhCollection coll = new DDviDchinhCollection();
            Query qry = new Query(DDviDchinh.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DDviDchinhCollection FetchByID(object MaDviDchinh)
        {
            DDviDchinhCollection coll = new DDviDchinhCollection().Where("MA_DVI_DCHINH", MaDviDchinh).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public DDviDchinhCollection FetchByQuery(Query qry)
        {
            DDviDchinhCollection coll = new DDviDchinhCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object MaDviDchinh)
        {
            return (DDviDchinh.Delete(MaDviDchinh) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object MaDviDchinh)
        {
            return (DDviDchinh.Destroy(MaDviDchinh) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MaDviDchinh,string TenDviDchinh,string MaCha,int SttHthi,string LoaiDviDchinh,int? TrangThai)
	    {
		    DDviDchinh item = new DDviDchinh();
		    
            item.MaDviDchinh = MaDviDchinh;
            
            item.TenDviDchinh = TenDviDchinh;
            
            item.MaCha = MaCha;
            
            item.SttHthi = SttHthi;
            
            item.LoaiDviDchinh = LoaiDviDchinh;
            
            item.TrangThai = TrangThai;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string MaDviDchinh,string TenDviDchinh,string MaCha,int SttHthi,string LoaiDviDchinh,int? TrangThai)
	    {
		    DDviDchinh item = new DDviDchinh();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.MaDviDchinh = MaDviDchinh;
				
			item.TenDviDchinh = TenDviDchinh;
				
			item.MaCha = MaCha;
				
			item.SttHthi = SttHthi;
				
			item.LoaiDviDchinh = LoaiDviDchinh;
				
			item.TrangThai = TrangThai;
				
	        item.Save(UserName);
	    }
    }
}