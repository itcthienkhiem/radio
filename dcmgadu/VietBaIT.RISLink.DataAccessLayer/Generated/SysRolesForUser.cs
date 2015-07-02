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
	/// Strongly-typed collection for the SysRolesForUser class.
	/// </summary>
    [Serializable]
	public partial class SysRolesForUserCollection : ActiveList<SysRolesForUser, SysRolesForUserCollection>
	{	   
		public SysRolesForUserCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysRolesForUserCollection</returns>
		public SysRolesForUserCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysRolesForUser o = this[i];
                foreach (SubSonic.Where w in this.wheres)
                {
                    bool remove = false;
                    System.Reflection.PropertyInfo pi = o.GetType().GetProperty(w.ColumnName);
                    if (pi.CanRead)
                    {
                        object val = pi.GetValue(o, null);
                        switch (w.Comparison)
                        {
                            case SubSonic.Comparison.Equals:
                                if (!val.Equals(w.ParameterValue))
                                {
                                    remove = true;
                                }
                                break;
                        }
                    }
                    if (remove)
                    {
                        this.Remove(o);
                        break;
                    }
                }
            }
            return this;
        }
		
		
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the Sys_RolesForUsers table.
	/// </summary>
	[Serializable]
	public partial class SysRolesForUser : ActiveRecord<SysRolesForUser>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public SysRolesForUser()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysRolesForUser(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysRolesForUser(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysRolesForUser(string columnName, object columnValue)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByParam(columnName,columnValue);
		}
		
		protected static void SetSQLProps() { GetTableSchema(); }
		
		#endregion
		
		#region Schema and Query Accessor	
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
					SetSQLProps();
				return BaseSchema;
			}
		}
		
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("Sys_RolesForUsers", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarSUID = new TableSchema.TableColumn(schema);
				colvarSUID.ColumnName = "sUID";
				colvarSUID.DataType = DbType.String;
				colvarSUID.MaxLength = 50;
				colvarSUID.AutoIncrement = false;
				colvarSUID.IsNullable = false;
				colvarSUID.IsPrimaryKey = true;
				colvarSUID.IsForeignKey = false;
				colvarSUID.IsReadOnly = false;
				colvarSUID.DefaultSetting = @"";
				colvarSUID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSUID);
				
				TableSchema.TableColumn colvarIRoleID = new TableSchema.TableColumn(schema);
				colvarIRoleID.ColumnName = "iRoleID";
				colvarIRoleID.DataType = DbType.Int64;
				colvarIRoleID.MaxLength = 0;
				colvarIRoleID.AutoIncrement = false;
				colvarIRoleID.IsNullable = false;
				colvarIRoleID.IsPrimaryKey = true;
				colvarIRoleID.IsForeignKey = false;
				colvarIRoleID.IsReadOnly = false;
				colvarIRoleID.DefaultSetting = @"";
				colvarIRoleID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIRoleID);
				
				TableSchema.TableColumn colvarIParentRoleID = new TableSchema.TableColumn(schema);
				colvarIParentRoleID.ColumnName = "iParentRoleID";
				colvarIParentRoleID.DataType = DbType.Int64;
				colvarIParentRoleID.MaxLength = 0;
				colvarIParentRoleID.AutoIncrement = false;
				colvarIParentRoleID.IsNullable = false;
				colvarIParentRoleID.IsPrimaryKey = true;
				colvarIParentRoleID.IsForeignKey = false;
				colvarIParentRoleID.IsReadOnly = false;
				colvarIParentRoleID.DefaultSetting = @"";
				colvarIParentRoleID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIParentRoleID);
				
				TableSchema.TableColumn colvarFpSBranchID = new TableSchema.TableColumn(schema);
				colvarFpSBranchID.ColumnName = "FP_sBranchID";
				colvarFpSBranchID.DataType = DbType.String;
				colvarFpSBranchID.MaxLength = 10;
				colvarFpSBranchID.AutoIncrement = false;
				colvarFpSBranchID.IsNullable = false;
				colvarFpSBranchID.IsPrimaryKey = true;
				colvarFpSBranchID.IsForeignKey = false;
				colvarFpSBranchID.IsReadOnly = false;
				colvarFpSBranchID.DefaultSetting = @"";
				colvarFpSBranchID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFpSBranchID);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("Sys_RolesForUsers",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("SUID")]
		[Bindable(true)]
		public string SUID 
		{
			get { return GetColumnValue<string>(Columns.SUID); }
			set { SetColumnValue(Columns.SUID, value); }
		}
		  
		[XmlAttribute("IRoleID")]
		[Bindable(true)]
		public long IRoleID 
		{
			get { return GetColumnValue<long>(Columns.IRoleID); }
			set { SetColumnValue(Columns.IRoleID, value); }
		}
		  
		[XmlAttribute("IParentRoleID")]
		[Bindable(true)]
		public long IParentRoleID 
		{
			get { return GetColumnValue<long>(Columns.IParentRoleID); }
			set { SetColumnValue(Columns.IParentRoleID, value); }
		}
		  
		[XmlAttribute("FpSBranchID")]
		[Bindable(true)]
		public string FpSBranchID 
		{
			get { return GetColumnValue<string>(Columns.FpSBranchID); }
			set { SetColumnValue(Columns.FpSBranchID, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varSUID,long varIRoleID,long varIParentRoleID,string varFpSBranchID)
		{
			SysRolesForUser item = new SysRolesForUser();
			
			item.SUID = varSUID;
			
			item.IRoleID = varIRoleID;
			
			item.IParentRoleID = varIParentRoleID;
			
			item.FpSBranchID = varFpSBranchID;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(string varSUID,long varIRoleID,long varIParentRoleID,string varFpSBranchID)
		{
			SysRolesForUser item = new SysRolesForUser();
			
				item.SUID = varSUID;
			
				item.IRoleID = varIRoleID;
			
				item.IParentRoleID = varIParentRoleID;
			
				item.FpSBranchID = varFpSBranchID;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn SUIDColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IRoleIDColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn IParentRoleIDColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn FpSBranchIDColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string SUID = @"sUID";
			 public static string IRoleID = @"iRoleID";
			 public static string IParentRoleID = @"iParentRoleID";
			 public static string FpSBranchID = @"FP_sBranchID";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
