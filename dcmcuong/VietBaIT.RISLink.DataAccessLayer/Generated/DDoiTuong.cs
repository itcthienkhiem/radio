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
	/// Strongly-typed collection for the DDoiTuong class.
	/// </summary>
    [Serializable]
	public partial class DDoiTuongCollection : ActiveList<DDoiTuong, DDoiTuongCollection>
	{	   
		public DDoiTuongCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DDoiTuongCollection</returns>
		public DDoiTuongCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DDoiTuong o = this[i];
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
	/// This is an ActiveRecord class which wraps the D_DOI_TUONG table.
	/// </summary>
	[Serializable]
	public partial class DDoiTuong : ActiveRecord<DDoiTuong>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DDoiTuong()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DDoiTuong(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DDoiTuong(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DDoiTuong(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("D_DOI_TUONG", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarMaDtuong = new TableSchema.TableColumn(schema);
				colvarMaDtuong.ColumnName = "MA_DTUONG";
				colvarMaDtuong.DataType = DbType.AnsiString;
				colvarMaDtuong.MaxLength = 20;
				colvarMaDtuong.AutoIncrement = false;
				colvarMaDtuong.IsNullable = false;
				colvarMaDtuong.IsPrimaryKey = true;
				colvarMaDtuong.IsForeignKey = false;
				colvarMaDtuong.IsReadOnly = false;
				colvarMaDtuong.DefaultSetting = @"";
				colvarMaDtuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaDtuong);
				
				TableSchema.TableColumn colvarTenDtuong = new TableSchema.TableColumn(schema);
				colvarTenDtuong.ColumnName = "TEN_DTUONG";
				colvarTenDtuong.DataType = DbType.String;
				colvarTenDtuong.MaxLength = 100;
				colvarTenDtuong.AutoIncrement = false;
				colvarTenDtuong.IsNullable = true;
				colvarTenDtuong.IsPrimaryKey = false;
				colvarTenDtuong.IsForeignKey = false;
				colvarTenDtuong.IsReadOnly = false;
				colvarTenDtuong.DefaultSetting = @"";
				colvarTenDtuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenDtuong);
				
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
				
				TableSchema.TableColumn colvarMoTa = new TableSchema.TableColumn(schema);
				colvarMoTa.ColumnName = "MO_TA";
				colvarMoTa.DataType = DbType.String;
				colvarMoTa.MaxLength = 50;
				colvarMoTa.AutoIncrement = false;
				colvarMoTa.IsNullable = true;
				colvarMoTa.IsPrimaryKey = false;
				colvarMoTa.IsForeignKey = false;
				colvarMoTa.IsReadOnly = false;
				colvarMoTa.DefaultSetting = @"";
				colvarMoTa.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMoTa);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("D_DOI_TUONG",schema);
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
		  
		[XmlAttribute("TenDtuong")]
		[Bindable(true)]
		public string TenDtuong 
		{
			get { return GetColumnValue<string>(Columns.TenDtuong); }
			set { SetColumnValue(Columns.TenDtuong, value); }
		}
		  
		[XmlAttribute("SttHthi")]
		[Bindable(true)]
		public int? SttHthi 
		{
			get { return GetColumnValue<int?>(Columns.SttHthi); }
			set { SetColumnValue(Columns.SttHthi, value); }
		}
		  
		[XmlAttribute("MoTa")]
		[Bindable(true)]
		public string MoTa 
		{
			get { return GetColumnValue<string>(Columns.MoTa); }
			set { SetColumnValue(Columns.MoTa, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varMaDtuong,string varTenDtuong,int? varSttHthi,string varMoTa)
		{
			DDoiTuong item = new DDoiTuong();
			
			item.MaDtuong = varMaDtuong;
			
			item.TenDtuong = varTenDtuong;
			
			item.SttHthi = varSttHthi;
			
			item.MoTa = varMoTa;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(string varMaDtuong,string varTenDtuong,int? varSttHthi,string varMoTa)
		{
			DDoiTuong item = new DDoiTuong();
			
				item.MaDtuong = varMaDtuong;
			
				item.TenDtuong = varTenDtuong;
			
				item.SttHthi = varSttHthi;
			
				item.MoTa = varMoTa;
			
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
        
        
        
        public static TableSchema.TableColumn TenDtuongColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn SttHthiColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn MoTaColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string MaDtuong = @"MA_DTUONG";
			 public static string TenDtuong = @"TEN_DTUONG";
			 public static string SttHthi = @"STT_HTHI";
			 public static string MoTa = @"MO_TA";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
