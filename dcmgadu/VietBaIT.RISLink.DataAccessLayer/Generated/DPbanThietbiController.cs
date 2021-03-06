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
    /// Controller class for D_PBAN_THIETBI
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class DPbanThietbiController
    {
        // Preload our schema..
        DPbanThietbi thisSchemaLoad = new DPbanThietbi();
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
        public DPbanThietbiCollection FetchAll()
        {
            DPbanThietbiCollection coll = new DPbanThietbiCollection();
            Query qry = new Query(DPbanThietbi.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DPbanThietbiCollection FetchByID(object IdKhoa)
        {
            DPbanThietbiCollection coll = new DPbanThietbiCollection().Where("ID_KHOA", IdKhoa).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public DPbanThietbiCollection FetchByQuery(Query qry)
        {
            DPbanThietbiCollection coll = new DPbanThietbiCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object IdKhoa)
        {
            return (DPbanThietbi.Delete(IdKhoa) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object IdKhoa)
        {
            return (DPbanThietbi.Destroy(IdKhoa) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(int IdKhoa,string MaTbi)
        {
            Query qry = new Query(DPbanThietbi.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("IdKhoa", IdKhoa).AND("MaTbi", MaTbi);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int IdKhoa,string MaTbi)
	    {
		    DPbanThietbi item = new DPbanThietbi();
		    
            item.IdKhoa = IdKhoa;
            
            item.MaTbi = MaTbi;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int IdKhoa,string MaTbi)
	    {
		    DPbanThietbi item = new DPbanThietbi();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.IdKhoa = IdKhoa;
				
			item.MaTbi = MaTbi;
				
	        item.Save(UserName);
	    }
    }
}
