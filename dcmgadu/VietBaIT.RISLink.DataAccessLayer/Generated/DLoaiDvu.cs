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
	/// Strongly-typed collection for the DLoaiDvu class.
	/// </summary>
    [Serializable]
	public partial class DLoaiDvuCollection : ActiveList<DLoaiDvu, DLoaiDvuCollection>
	{	   
		public DLoaiDvuCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DLoaiDvuCollection</returns>
		public DLoaiDvuCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DLoaiDvu o = this[i];
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
	/// This is an ActiveRecord class which wraps the D_LOAI_DVU table.
	/// </summary>
	[Serializable]
	public partial class DLoaiDvu : ActiveRecord<DLoaiDvu>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DLoaiDvu()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DLoaiDvu(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DLoaiDvu(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DLoaiDvu(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("D_LOAI_DVU", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdLoaiDvu = new TableSchema.TableColumn(schema);
				colvarIdLoaiDvu.ColumnName = "ID_LOAI_DVU";
				colvarIdLoaiDvu.DataType = DbType.Int32;
				colvarIdLoaiDvu.MaxLength = 0;
				colvarIdLoaiDvu.AutoIncrement = true;
				colvarIdLoaiDvu.IsNullable = false;
				colvarIdLoaiDvu.IsPrimaryKey = true;
				colvarIdLoaiDvu.IsForeignKey = false;
				colvarIdLoaiDvu.IsReadOnly = false;
				colvarIdLoaiDvu.DefaultSetting = @"";
				colvarIdLoaiDvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdLoaiDvu);
				
				TableSchema.TableColumn colvarMaLoaiDvu = new TableSchema.TableColumn(schema);
				colvarMaLoaiDvu.ColumnName = "MA_LOAI_DVU";
				colvarMaLoaiDvu.DataType = DbType.String;
				colvarMaLoaiDvu.MaxLength = 50;
				colvarMaLoaiDvu.AutoIncrement = false;
				colvarMaLoaiDvu.IsNullable = false;
				colvarMaLoaiDvu.IsPrimaryKey = false;
				colvarMaLoaiDvu.IsForeignKey = false;
				colvarMaLoaiDvu.IsReadOnly = false;
				colvarMaLoaiDvu.DefaultSetting = @"";
				colvarMaLoaiDvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaLoaiDvu);
				
				TableSchema.TableColumn colvarTenLoaiDvu = new TableSchema.TableColumn(schema);
				colvarTenLoaiDvu.ColumnName = "TEN_LOAI_DVU";
				colvarTenLoaiDvu.DataType = DbType.String;
				colvarTenLoaiDvu.MaxLength = 200;
				colvarTenLoaiDvu.AutoIncrement = false;
				colvarTenLoaiDvu.IsNullable = false;
				colvarTenLoaiDvu.IsPrimaryKey = false;
				colvarTenLoaiDvu.IsForeignKey = false;
				colvarTenLoaiDvu.IsReadOnly = false;
				colvarTenLoaiDvu.DefaultSetting = @"";
				colvarTenLoaiDvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenLoaiDvu);
				
				TableSchema.TableColumn colvarTenLoaiDvuEn = new TableSchema.TableColumn(schema);
				colvarTenLoaiDvuEn.ColumnName = "TEN_LOAI_DVU_EN";
				colvarTenLoaiDvuEn.DataType = DbType.String;
				colvarTenLoaiDvuEn.MaxLength = 50;
				colvarTenLoaiDvuEn.AutoIncrement = false;
				colvarTenLoaiDvuEn.IsNullable = true;
				colvarTenLoaiDvuEn.IsPrimaryKey = false;
				colvarTenLoaiDvuEn.IsForeignKey = false;
				colvarTenLoaiDvuEn.IsReadOnly = false;
				colvarTenLoaiDvuEn.DefaultSetting = @"";
				colvarTenLoaiDvuEn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenLoaiDvuEn);
				
				TableSchema.TableColumn colvarSttHthi = new TableSchema.TableColumn(schema);
				colvarSttHthi.ColumnName = "STT_HTHI";
				colvarSttHthi.DataType = DbType.Int32;
				colvarSttHthi.MaxLength = 0;
				colvarSttHthi.AutoIncrement = false;
				colvarSttHthi.IsNullable = true;
				colvarSttHthi.IsPrimaryKey = false;
				colvarSttHthi.IsForeignKey = false;
				colvarSttHthi.IsReadOnly = false;
				colvarSttHthi.DefaultSetting = @"";
				colvarSttHthi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSttHthi);
				
				TableSchema.TableColumn colvarMaKieuDvu = new TableSchema.TableColumn(schema);
				colvarMaKieuDvu.ColumnName = "MA_KIEU_DVU";
				colvarMaKieuDvu.DataType = DbType.AnsiString;
				colvarMaKieuDvu.MaxLength = 50;
				colvarMaKieuDvu.AutoIncrement = false;
				colvarMaKieuDvu.IsNullable = true;
				colvarMaKieuDvu.IsPrimaryKey = false;
				colvarMaKieuDvu.IsForeignKey = false;
				colvarMaKieuDvu.IsReadOnly = false;
				colvarMaKieuDvu.DefaultSetting = @"";
				colvarMaKieuDvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaKieuDvu);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("D_LOAI_DVU",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdLoaiDvu")]
		[Bindable(true)]
		public int IdLoaiDvu 
		{
			get { return GetColumnValue<int>(Columns.IdLoaiDvu); }
			set { SetColumnValue(Columns.IdLoaiDvu, value); }
		}
		  
		[XmlAttribute("MaLoaiDvu")]
		[Bindable(true)]
		public string MaLoaiDvu 
		{
			get { return GetColumnValue<string>(Columns.MaLoaiDvu); }
			set { SetColumnValue(Columns.MaLoaiDvu, value); }
		}
		  
		[XmlAttribute("TenLoaiDvu")]
		[Bindable(true)]
		public string TenLoaiDvu 
		{
			get { return GetColumnValue<string>(Columns.TenLoaiDvu); }
			set { SetColumnValue(Columns.TenLoaiDvu, value); }
		}
		  
		[XmlAttribute("TenLoaiDvuEn")]
		[Bindable(true)]
		public string TenLoaiDvuEn 
		{
			get { return GetColumnValue<string>(Columns.TenLoaiDvuEn); }
			set { SetColumnValue(Columns.TenLoaiDvuEn, value); }
		}
		  
		[XmlAttribute("SttHthi")]
		[Bindable(true)]
		public int? SttHthi 
		{
			get { return GetColumnValue<int?>(Columns.SttHthi); }
			set { SetColumnValue(Columns.SttHthi, value); }
		}
		  
		[XmlAttribute("MaKieuDvu")]
		[Bindable(true)]
		public string MaKieuDvu 
		{
			get { return GetColumnValue<string>(Columns.MaKieuDvu); }
			set { SetColumnValue(Columns.MaKieuDvu, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varMaLoaiDvu,string varTenLoaiDvu,string varTenLoaiDvuEn,int? varSttHthi,string varMaKieuDvu)
		{
			DLoaiDvu item = new DLoaiDvu();
			
			item.MaLoaiDvu = varMaLoaiDvu;
			
			item.TenLoaiDvu = varTenLoaiDvu;
			
			item.TenLoaiDvuEn = varTenLoaiDvuEn;
			
			item.SttHthi = varSttHthi;
			
			item.MaKieuDvu = varMaKieuDvu;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varIdLoaiDvu,string varMaLoaiDvu,string varTenLoaiDvu,string varTenLoaiDvuEn,int? varSttHthi,string varMaKieuDvu)
		{
			DLoaiDvu item = new DLoaiDvu();
			
				item.IdLoaiDvu = varIdLoaiDvu;
			
				item.MaLoaiDvu = varMaLoaiDvu;
			
				item.TenLoaiDvu = varTenLoaiDvu;
			
				item.TenLoaiDvuEn = varTenLoaiDvuEn;
			
				item.SttHthi = varSttHthi;
			
				item.MaKieuDvu = varMaKieuDvu;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdLoaiDvuColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn MaLoaiDvuColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TenLoaiDvuColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn TenLoaiDvuEnColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SttHthiColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn MaKieuDvuColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdLoaiDvu = @"ID_LOAI_DVU";
			 public static string MaLoaiDvu = @"MA_LOAI_DVU";
			 public static string TenLoaiDvu = @"TEN_LOAI_DVU";
			 public static string TenLoaiDvuEn = @"TEN_LOAI_DVU_EN";
			 public static string SttHthi = @"STT_HTHI";
			 public static string MaKieuDvu = @"MA_KIEU_DVU";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
