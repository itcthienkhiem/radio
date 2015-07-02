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
    /// Controller class for Sys_Version
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysVersionController
    {
        // Preload our schema..
        SysVersion thisSchemaLoad = new SysVersion();
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
        public SysVersionCollection FetchAll()
        {
            SysVersionCollection coll = new SysVersionCollection();
            Query qry = new Query(SysVersion.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysVersionCollection FetchByID(object PkIntID)
        {
            SysVersionCollection coll = new SysVersionCollection().Where("PK_intID", PkIntID).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysVersionCollection FetchByQuery(Query qry)
        {
            SysVersionCollection coll = new SysVersionCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object PkIntID)
        {
            return (SysVersion.Delete(PkIntID) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object PkIntID)
        {
            return (SysVersion.Destroy(PkIntID) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string SFileName,string SRarFileName,byte[] ObjData,string SVersion,short? IntRar,short? IntPatch,DateTime? TUpdatedDate,int? DblCapacity,string SDesc)
	    {
		    SysVersion item = new SysVersion();
		    
            item.SFileName = SFileName;
            
            item.SRarFileName = SRarFileName;
            
            item.ObjData = ObjData;
            
            item.SVersion = SVersion;
            
            item.IntRar = IntRar;
            
            item.IntPatch = IntPatch;
            
            item.TUpdatedDate = TUpdatedDate;
            
            item.DblCapacity = DblCapacity;
            
            item.SDesc = SDesc;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int PkIntID,string SFileName,string SRarFileName,byte[] ObjData,string SVersion,short? IntRar,short? IntPatch,DateTime? TUpdatedDate,int? DblCapacity,string SDesc)
	    {
		    SysVersion item = new SysVersion();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.PkIntID = PkIntID;
				
			item.SFileName = SFileName;
				
			item.SRarFileName = SRarFileName;
				
			item.ObjData = ObjData;
				
			item.SVersion = SVersion;
				
			item.IntRar = IntRar;
				
			item.IntPatch = IntPatch;
				
			item.TUpdatedDate = TUpdatedDate;
				
			item.DblCapacity = DblCapacity;
				
			item.SDesc = SDesc;
				
	        item.Save(UserName);
	    }
    }
}
