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
    /// Controller class for Sys_LockObject
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysLockObjectController
    {
        // Preload our schema..
        SysLockObject thisSchemaLoad = new SysLockObject();
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
        public SysLockObjectCollection FetchAll()
        {
            SysLockObjectCollection coll = new SysLockObjectCollection();
            Query qry = new Query(SysLockObject.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysLockObjectCollection FetchByID(object ObjectID)
        {
            SysLockObjectCollection coll = new SysLockObjectCollection().Where("ObjectID", ObjectID).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysLockObjectCollection FetchByQuery(Query qry)
        {
            SysLockObjectCollection coll = new SysLockObjectCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object ObjectID)
        {
            return (SysLockObject.Delete(ObjectID) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object ObjectID)
        {
            return (SysLockObject.Destroy(ObjectID) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string ObjectID,string UserName,DateTime LockTime)
	    {
		    SysLockObject item = new SysLockObject();
		    
            item.ObjectID = ObjectID;
            
            item.UserName = UserName;
            
            item.LockTime = LockTime;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string ObjectID,string UserName,DateTime LockTime)
	    {
		    SysLockObject item = new SysLockObject();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.ObjectID = ObjectID;
				
			item.UserName = UserName;
				
			item.LockTime = LockTime;
				
	        item.Save(UserName);
	    }
    }
}