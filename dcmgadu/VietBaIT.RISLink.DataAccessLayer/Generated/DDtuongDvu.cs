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
	/// Strongly-typed collection for the DDtuongDvu class.
	/// </summary>
    [Serializable]
	public partial class DDtuongDvuCollection : ActiveList<DDtuongDvu, DDtuongDvuCollection>
	{	   
		public DDtuongDvuCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DDtuongDvuCollection</returns>
		public DDtuongDvuCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DDtuongDvu o = this[i];
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
	/// This is an ActiveRecord class which wraps the D_DTUONG_DVU table.
	/// </summary>
	[Serializable]
	public partial class DDtuongDvu : ActiveRecord<DDtuongDvu>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DDtuongDvu()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DDtuongDvu(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DDtuongDvu(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DDtuongDvu(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("D_DTUONG_DVU", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarMaDtuong = new TableSchema.TableColumn(schema);
				colvarMaDtuong.ColumnName = "MA_DTUONG";
				colvarMaDtuong.DataType = DbType.AnsiString;
				colvarMaDtuong.MaxLength = 50;
				colvarMaDtuong.AutoIncrement = false;
				colvarMaDtuong.IsNullable = false;
				colvarMaDtuong.IsPrimaryKey = true;
				colvarMaDtuong.IsForeignKey = false;
				colvarMaDtuong.IsReadOnly = false;
				colvarMaDtuong.DefaultSetting = @"";
				colvarMaDtuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaDtuong);
				
				TableSchema.TableColumn colvarIdDvu = new TableSchema.TableColumn(schema);
				colvarIdDvu.ColumnName = "ID_DVU";
				colvarIdDvu.DataType = DbType.Int32;
				colvarIdDvu.MaxLength = 0;
				colvarIdDvu.AutoIncrement = false;
				colvarIdDvu.IsNullable = false;
				colvarIdDvu.IsPrimaryKey = true;
				colvarIdDvu.IsForeignKey = false;
				colvarIdDvu.IsReadOnly = false;
				colvarIdDvu.DefaultSetting = @"";
				colvarIdDvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdDvu);
				
				TableSchema.TableColumn colvarDonGia = new TableSchema.TableColumn(schema);
				colvarDonGia.ColumnName = "DON_GIA";
				colvarDonGia.DataType = DbType.Decimal;
				colvarDonGia.MaxLength = 0;
				colvarDonGia.AutoIncrement = false;
				colvarDonGia.IsNullable = true;
				colvarDonGia.IsPrimaryKey = false;
				colvarDonGia.IsForeignKey = false;
				colvarDonGia.IsReadOnly = false;
				colvarDonGia.DefaultSetting = @"";
				colvarDonGia.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDonGia);
				
				TableSchema.TableColumn colvarIdLoaiDvu = new TableSchema.TableColumn(schema);
				colvarIdLoaiDvu.ColumnName = "ID_LOAI_DVU";
				colvarIdLoaiDvu.DataType = DbType.Int32;
				colvarIdLoaiDvu.MaxLength = 0;
				colvarIdLoaiDvu.AutoIncrement = false;
				colvarIdLoaiDvu.IsNullable = true;
				colvarIdLoaiDvu.IsPrimaryKey = false;
				colvarIdLoaiDvu.IsForeignKey = false;
				colvarIdLoaiDvu.IsReadOnly = false;
				colvarIdLoaiDvu.DefaultSetting = @"";
				colvarIdLoaiDvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdLoaiDvu);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("D_DTUONG_DVU",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("MaDtuong")]
		[Bindable(true)]
		public string MaDtuong 
		{
			get { return GetColumnValue<string>(Columns.MaDtuong); }
			set { SetColumnValue(Columns.MaDtuong, value); }
		}
		  
		[XmlAttribute("IdDvu")]
		[Bindable(true)]
		public int IdDvu 
		{
			get { return GetColumnValue<int>(Columns.IdDvu); }
			set { SetColumnValue(Columns.IdDvu, value); }
		}
		  
		[XmlAttribute("DonGia")]
		[Bindable(true)]
		public decimal? DonGia 
		{
			get { return GetColumnValue<decimal?>(Columns.DonGia); }
			set { SetColumnValue(Columns.DonGia, value); }
		}
		  
		[XmlAttribute("IdLoaiDvu")]
		[Bindable(true)]
		public int? IdLoaiDvu 
		{
			get { return GetColumnValue<int?>(Columns.IdLoaiDvu); }
			set { SetColumnValue(Columns.IdLoaiDvu, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varMaDtuong,int varIdDvu,decimal? varDonGia,int? varIdLoaiDvu)
		{
			DDtuongDvu item = new DDtuongDvu();
			
			item.MaDtuong = varMaDtuong;
			
			item.IdDvu = varIdDvu;
			
			item.DonGia = varDonGia;
			
			item.IdLoaiDvu = varIdLoaiDvu;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(string varMaDtuong,int varIdDvu,decimal? varDonGia,int? varIdLoaiDvu)
		{
			DDtuongDvu item = new DDtuongDvu();
			
				item.MaDtuong = varMaDtuong;
			
				item.IdDvu = varIdDvu;
			
				item.DonGia = varDonGia;
			
				item.IdLoaiDvu = varIdLoaiDvu;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn MaDtuongColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdDvuColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn DonGiaColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn IdLoaiDvuColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string MaDtuong = @"MA_DTUONG";
			 public static string IdDvu = @"ID_DVU";
			 public static string DonGia = @"DON_GIA";
			 public static string IdLoaiDvu = @"ID_LOAI_DVU";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
