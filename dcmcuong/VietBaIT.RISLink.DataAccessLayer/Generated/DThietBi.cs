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
	/// Strongly-typed collection for the DThietBi class.
	/// </summary>
    [Serializable]
	public partial class DThietBiCollection : ActiveList<DThietBi, DThietBiCollection>
	{	   
		public DThietBiCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DThietBiCollection</returns>
		public DThietBiCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DThietBi o = this[i];
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
	/// This is an ActiveRecord class which wraps the D_THIET_BI table.
	/// </summary>
	[Serializable]
	public partial class DThietBi : ActiveRecord<DThietBi>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DThietBi()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DThietBi(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DThietBi(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DThietBi(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("D_THIET_BI", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdThietBi = new TableSchema.TableColumn(schema);
				colvarIdThietBi.ColumnName = "IdThietBi";
				colvarIdThietBi.DataType = DbType.Int32;
				colvarIdThietBi.MaxLength = 0;
				colvarIdThietBi.AutoIncrement = true;
				colvarIdThietBi.IsNullable = false;
				colvarIdThietBi.IsPrimaryKey = true;
				colvarIdThietBi.IsForeignKey = false;
				colvarIdThietBi.IsReadOnly = false;
				colvarIdThietBi.DefaultSetting = @"";
				colvarIdThietBi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdThietBi);
				
				TableSchema.TableColumn colvarTenThietBi = new TableSchema.TableColumn(schema);
				colvarTenThietBi.ColumnName = "TenThietBi";
				colvarTenThietBi.DataType = DbType.String;
				colvarTenThietBi.MaxLength = 50;
				colvarTenThietBi.AutoIncrement = false;
				colvarTenThietBi.IsNullable = false;
				colvarTenThietBi.IsPrimaryKey = false;
				colvarTenThietBi.IsForeignKey = false;
				colvarTenThietBi.IsReadOnly = false;
				colvarTenThietBi.DefaultSetting = @"";
				colvarTenThietBi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenThietBi);
				
				TableSchema.TableColumn colvarAETitle = new TableSchema.TableColumn(schema);
				colvarAETitle.ColumnName = "AETitle";
				colvarAETitle.DataType = DbType.String;
				colvarAETitle.MaxLength = 50;
				colvarAETitle.AutoIncrement = false;
				colvarAETitle.IsNullable = false;
				colvarAETitle.IsPrimaryKey = false;
				colvarAETitle.IsForeignKey = false;
				colvarAETitle.IsReadOnly = false;
				colvarAETitle.DefaultSetting = @"";
				colvarAETitle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAETitle);
				
				TableSchema.TableColumn colvarTrangThai = new TableSchema.TableColumn(schema);
				colvarTrangThai.ColumnName = "TrangThai";
				colvarTrangThai.DataType = DbType.Int32;
				colvarTrangThai.MaxLength = 0;
				colvarTrangThai.AutoIncrement = false;
				colvarTrangThai.IsNullable = false;
				colvarTrangThai.IsPrimaryKey = false;
				colvarTrangThai.IsForeignKey = false;
				colvarTrangThai.IsReadOnly = false;
				
						colvarTrangThai.DefaultSetting = @"((1))";
				colvarTrangThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangThai);
				
				TableSchema.TableColumn colvarStt = new TableSchema.TableColumn(schema);
				colvarStt.ColumnName = "Stt";
				colvarStt.DataType = DbType.Int32;
				colvarStt.MaxLength = 0;
				colvarStt.AutoIncrement = false;
				colvarStt.IsNullable = false;
				colvarStt.IsPrimaryKey = false;
				colvarStt.IsForeignKey = false;
				colvarStt.IsReadOnly = false;
				
						colvarStt.DefaultSetting = @"((0))";
				colvarStt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStt);
				
				TableSchema.TableColumn colvarGhiChu = new TableSchema.TableColumn(schema);
				colvarGhiChu.ColumnName = "GhiChu";
				colvarGhiChu.DataType = DbType.String;
				colvarGhiChu.MaxLength = 200;
				colvarGhiChu.AutoIncrement = false;
				colvarGhiChu.IsNullable = true;
				colvarGhiChu.IsPrimaryKey = false;
				colvarGhiChu.IsForeignKey = false;
				colvarGhiChu.IsReadOnly = false;
				colvarGhiChu.DefaultSetting = @"";
				colvarGhiChu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGhiChu);
				
				TableSchema.TableColumn colvarCustomField1 = new TableSchema.TableColumn(schema);
				colvarCustomField1.ColumnName = "CustomField1";
				colvarCustomField1.DataType = DbType.String;
				colvarCustomField1.MaxLength = 200;
				colvarCustomField1.AutoIncrement = false;
				colvarCustomField1.IsNullable = true;
				colvarCustomField1.IsPrimaryKey = false;
				colvarCustomField1.IsForeignKey = false;
				colvarCustomField1.IsReadOnly = false;
				colvarCustomField1.DefaultSetting = @"";
				colvarCustomField1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomField1);
				
				TableSchema.TableColumn colvarCustomField2 = new TableSchema.TableColumn(schema);
				colvarCustomField2.ColumnName = "CustomField2";
				colvarCustomField2.DataType = DbType.String;
				colvarCustomField2.MaxLength = 200;
				colvarCustomField2.AutoIncrement = false;
				colvarCustomField2.IsNullable = true;
				colvarCustomField2.IsPrimaryKey = false;
				colvarCustomField2.IsForeignKey = false;
				colvarCustomField2.IsReadOnly = false;
				colvarCustomField2.DefaultSetting = @"";
				colvarCustomField2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomField2);
				
				TableSchema.TableColumn colvarCustomField3 = new TableSchema.TableColumn(schema);
				colvarCustomField3.ColumnName = "CustomField3";
				colvarCustomField3.DataType = DbType.String;
				colvarCustomField3.MaxLength = 200;
				colvarCustomField3.AutoIncrement = false;
				colvarCustomField3.IsNullable = true;
				colvarCustomField3.IsPrimaryKey = false;
				colvarCustomField3.IsForeignKey = false;
				colvarCustomField3.IsReadOnly = false;
				colvarCustomField3.DefaultSetting = @"";
				colvarCustomField3.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCustomField3);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("D_THIET_BI",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdThietBi")]
		[Bindable(true)]
		public int IdThietBi 
		{
			get { return GetColumnValue<int>(Columns.IdThietBi); }
			set { SetColumnValue(Columns.IdThietBi, value); }
		}
		  
		[XmlAttribute("TenThietBi")]
		[Bindable(true)]
		public string TenThietBi 
		{
			get { return GetColumnValue<string>(Columns.TenThietBi); }
			set { SetColumnValue(Columns.TenThietBi, value); }
		}
		  
		[XmlAttribute("AETitle")]
		[Bindable(true)]
		public string AETitle 
		{
			get { return GetColumnValue<string>(Columns.AETitle); }
			set { SetColumnValue(Columns.AETitle, value); }
		}
		  
		[XmlAttribute("TrangThai")]
		[Bindable(true)]
		public int TrangThai 
		{
			get { return GetColumnValue<int>(Columns.TrangThai); }
			set { SetColumnValue(Columns.TrangThai, value); }
		}
		  
		[XmlAttribute("Stt")]
		[Bindable(true)]
		public int Stt 
		{
			get { return GetColumnValue<int>(Columns.Stt); }
			set { SetColumnValue(Columns.Stt, value); }
		}
		  
		[XmlAttribute("GhiChu")]
		[Bindable(true)]
		public string GhiChu 
		{
			get { return GetColumnValue<string>(Columns.GhiChu); }
			set { SetColumnValue(Columns.GhiChu, value); }
		}
		  
		[XmlAttribute("CustomField1")]
		[Bindable(true)]
		public string CustomField1 
		{
			get { return GetColumnValue<string>(Columns.CustomField1); }
			set { SetColumnValue(Columns.CustomField1, value); }
		}
		  
		[XmlAttribute("CustomField2")]
		[Bindable(true)]
		public string CustomField2 
		{
			get { return GetColumnValue<string>(Columns.CustomField2); }
			set { SetColumnValue(Columns.CustomField2, value); }
		}
		  
		[XmlAttribute("CustomField3")]
		[Bindable(true)]
		public string CustomField3 
		{
			get { return GetColumnValue<string>(Columns.CustomField3); }
			set { SetColumnValue(Columns.CustomField3, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varTenThietBi,string varAETitle,int varTrangThai,int varStt,string varGhiChu,string varCustomField1,string varCustomField2,string varCustomField3)
		{
			DThietBi item = new DThietBi();
			
			item.TenThietBi = varTenThietBi;
			
			item.AETitle = varAETitle;
			
			item.TrangThai = varTrangThai;
			
			item.Stt = varStt;
			
			item.GhiChu = varGhiChu;
			
			item.CustomField1 = varCustomField1;
			
			item.CustomField2 = varCustomField2;
			
			item.CustomField3 = varCustomField3;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varIdThietBi,string varTenThietBi,string varAETitle,int varTrangThai,int varStt,string varGhiChu,string varCustomField1,string varCustomField2,string varCustomField3)
		{
			DThietBi item = new DThietBi();
			
				item.IdThietBi = varIdThietBi;
			
				item.TenThietBi = varTenThietBi;
			
				item.AETitle = varAETitle;
			
				item.TrangThai = varTrangThai;
			
				item.Stt = varStt;
			
				item.GhiChu = varGhiChu;
			
				item.CustomField1 = varCustomField1;
			
				item.CustomField2 = varCustomField2;
			
				item.CustomField3 = varCustomField3;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdThietBiColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn TenThietBiColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn AETitleColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangThaiColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SttColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn GhiChuColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn CustomField1Column
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn CustomField2Column
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn CustomField3Column
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdThietBi = @"IdThietBi";
			 public static string TenThietBi = @"TenThietBi";
			 public static string AETitle = @"AETitle";
			 public static string TrangThai = @"TrangThai";
			 public static string Stt = @"Stt";
			 public static string GhiChu = @"GhiChu";
			 public static string CustomField1 = @"CustomField1";
			 public static string CustomField2 = @"CustomField2";
			 public static string CustomField3 = @"CustomField3";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}