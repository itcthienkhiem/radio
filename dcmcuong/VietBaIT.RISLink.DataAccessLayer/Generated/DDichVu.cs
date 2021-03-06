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
	/// Strongly-typed collection for the DDichVu class.
	/// </summary>
    [Serializable]
	public partial class DDichVuCollection : ActiveList<DDichVu, DDichVuCollection>
	{	   
		public DDichVuCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DDichVuCollection</returns>
		public DDichVuCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DDichVu o = this[i];
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
	/// This is an ActiveRecord class which wraps the D_DICH_VU table.
	/// </summary>
	[Serializable]
	public partial class DDichVu : ActiveRecord<DDichVu>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DDichVu()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DDichVu(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DDichVu(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DDichVu(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("D_DICH_VU", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdDvu = new TableSchema.TableColumn(schema);
				colvarIdDvu.ColumnName = "ID_DVU";
				colvarIdDvu.DataType = DbType.Int32;
				colvarIdDvu.MaxLength = 0;
				colvarIdDvu.AutoIncrement = true;
				colvarIdDvu.IsNullable = false;
				colvarIdDvu.IsPrimaryKey = true;
				colvarIdDvu.IsForeignKey = false;
				colvarIdDvu.IsReadOnly = false;
				colvarIdDvu.DefaultSetting = @"";
				colvarIdDvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdDvu);
				
				TableSchema.TableColumn colvarIdLoaiDvu = new TableSchema.TableColumn(schema);
				colvarIdLoaiDvu.ColumnName = "ID_LOAI_DVU";
				colvarIdLoaiDvu.DataType = DbType.Int32;
				colvarIdLoaiDvu.MaxLength = 0;
				colvarIdLoaiDvu.AutoIncrement = false;
				colvarIdLoaiDvu.IsNullable = false;
				colvarIdLoaiDvu.IsPrimaryKey = false;
				colvarIdLoaiDvu.IsForeignKey = false;
				colvarIdLoaiDvu.IsReadOnly = false;
				colvarIdLoaiDvu.DefaultSetting = @"";
				colvarIdLoaiDvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdLoaiDvu);
				
				TableSchema.TableColumn colvarMaDvu = new TableSchema.TableColumn(schema);
				colvarMaDvu.ColumnName = "MA_DVU";
				colvarMaDvu.DataType = DbType.String;
				colvarMaDvu.MaxLength = 50;
				colvarMaDvu.AutoIncrement = false;
				colvarMaDvu.IsNullable = false;
				colvarMaDvu.IsPrimaryKey = false;
				colvarMaDvu.IsForeignKey = false;
				colvarMaDvu.IsReadOnly = false;
				colvarMaDvu.DefaultSetting = @"";
				colvarMaDvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaDvu);
				
				TableSchema.TableColumn colvarTenDvu = new TableSchema.TableColumn(schema);
				colvarTenDvu.ColumnName = "TEN_DVU";
				colvarTenDvu.DataType = DbType.String;
				colvarTenDvu.MaxLength = 200;
				colvarTenDvu.AutoIncrement = false;
				colvarTenDvu.IsNullable = false;
				colvarTenDvu.IsPrimaryKey = false;
				colvarTenDvu.IsForeignKey = false;
				colvarTenDvu.IsReadOnly = false;
				colvarTenDvu.DefaultSetting = @"";
				colvarTenDvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenDvu);
				
				TableSchema.TableColumn colvarTenDvuEn = new TableSchema.TableColumn(schema);
				colvarTenDvuEn.ColumnName = "TEN_DVU_EN";
				colvarTenDvuEn.DataType = DbType.String;
				colvarTenDvuEn.MaxLength = 200;
				colvarTenDvuEn.AutoIncrement = false;
				colvarTenDvuEn.IsNullable = true;
				colvarTenDvuEn.IsPrimaryKey = false;
				colvarTenDvuEn.IsForeignKey = false;
				colvarTenDvuEn.IsReadOnly = false;
				colvarTenDvuEn.DefaultSetting = @"";
				colvarTenDvuEn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenDvuEn);
				
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
				
				TableSchema.TableColumn colvarTrangThai = new TableSchema.TableColumn(schema);
				colvarTrangThai.ColumnName = "TRANG_THAI";
				colvarTrangThai.DataType = DbType.Int32;
				colvarTrangThai.MaxLength = 0;
				colvarTrangThai.AutoIncrement = false;
				colvarTrangThai.IsNullable = false;
				colvarTrangThai.IsPrimaryKey = false;
				colvarTrangThai.IsForeignKey = false;
				colvarTrangThai.IsReadOnly = false;
				colvarTrangThai.DefaultSetting = @"";
				colvarTrangThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangThai);
				
				TableSchema.TableColumn colvarIdVungKs = new TableSchema.TableColumn(schema);
				colvarIdVungKs.ColumnName = "ID_VUNG_KS";
				colvarIdVungKs.DataType = DbType.Int32;
				colvarIdVungKs.MaxLength = 0;
				colvarIdVungKs.AutoIncrement = false;
				colvarIdVungKs.IsNullable = false;
				colvarIdVungKs.IsPrimaryKey = false;
				colvarIdVungKs.IsForeignKey = false;
				colvarIdVungKs.IsReadOnly = false;
				colvarIdVungKs.DefaultSetting = @"";
				colvarIdVungKs.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdVungKs);
				
				TableSchema.TableColumn colvarMoTa = new TableSchema.TableColumn(schema);
				colvarMoTa.ColumnName = "MO_TA";
				colvarMoTa.DataType = DbType.String;
				colvarMoTa.MaxLength = 255;
				colvarMoTa.AutoIncrement = false;
				colvarMoTa.IsNullable = true;
				colvarMoTa.IsPrimaryKey = false;
				colvarMoTa.IsForeignKey = false;
				colvarMoTa.IsReadOnly = false;
				colvarMoTa.DefaultSetting = @"";
				colvarMoTa.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMoTa);
				
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
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("D_DICH_VU",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdDvu")]
		[Bindable(true)]
		public int IdDvu 
		{
			get { return GetColumnValue<int>(Columns.IdDvu); }
			set { SetColumnValue(Columns.IdDvu, value); }
		}
		  
		[XmlAttribute("IdLoaiDvu")]
		[Bindable(true)]
		public int IdLoaiDvu 
		{
			get { return GetColumnValue<int>(Columns.IdLoaiDvu); }
			set { SetColumnValue(Columns.IdLoaiDvu, value); }
		}
		  
		[XmlAttribute("MaDvu")]
		[Bindable(true)]
		public string MaDvu 
		{
			get { return GetColumnValue<string>(Columns.MaDvu); }
			set { SetColumnValue(Columns.MaDvu, value); }
		}
		  
		[XmlAttribute("TenDvu")]
		[Bindable(true)]
		public string TenDvu 
		{
			get { return GetColumnValue<string>(Columns.TenDvu); }
			set { SetColumnValue(Columns.TenDvu, value); }
		}
		  
		[XmlAttribute("TenDvuEn")]
		[Bindable(true)]
		public string TenDvuEn 
		{
			get { return GetColumnValue<string>(Columns.TenDvuEn); }
			set { SetColumnValue(Columns.TenDvuEn, value); }
		}
		  
		[XmlAttribute("SttHthi")]
		[Bindable(true)]
		public int? SttHthi 
		{
			get { return GetColumnValue<int?>(Columns.SttHthi); }
			set { SetColumnValue(Columns.SttHthi, value); }
		}
		  
		[XmlAttribute("TrangThai")]
		[Bindable(true)]
		public int TrangThai 
		{
			get { return GetColumnValue<int>(Columns.TrangThai); }
			set { SetColumnValue(Columns.TrangThai, value); }
		}
		  
		[XmlAttribute("IdVungKs")]
		[Bindable(true)]
		public int IdVungKs 
		{
			get { return GetColumnValue<int>(Columns.IdVungKs); }
			set { SetColumnValue(Columns.IdVungKs, value); }
		}
		  
		[XmlAttribute("MoTa")]
		[Bindable(true)]
		public string MoTa 
		{
			get { return GetColumnValue<string>(Columns.MoTa); }
			set { SetColumnValue(Columns.MoTa, value); }
		}
		  
		[XmlAttribute("DonGia")]
		[Bindable(true)]
		public decimal? DonGia 
		{
			get { return GetColumnValue<decimal?>(Columns.DonGia); }
			set { SetColumnValue(Columns.DonGia, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varIdLoaiDvu,string varMaDvu,string varTenDvu,string varTenDvuEn,int? varSttHthi,int varTrangThai,int varIdVungKs,string varMoTa,decimal? varDonGia)
		{
			DDichVu item = new DDichVu();
			
			item.IdLoaiDvu = varIdLoaiDvu;
			
			item.MaDvu = varMaDvu;
			
			item.TenDvu = varTenDvu;
			
			item.TenDvuEn = varTenDvuEn;
			
			item.SttHthi = varSttHthi;
			
			item.TrangThai = varTrangThai;
			
			item.IdVungKs = varIdVungKs;
			
			item.MoTa = varMoTa;
			
			item.DonGia = varDonGia;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varIdDvu,int varIdLoaiDvu,string varMaDvu,string varTenDvu,string varTenDvuEn,int? varSttHthi,int varTrangThai,int varIdVungKs,string varMoTa,decimal? varDonGia)
		{
			DDichVu item = new DDichVu();
			
				item.IdDvu = varIdDvu;
			
				item.IdLoaiDvu = varIdLoaiDvu;
			
				item.MaDvu = varMaDvu;
			
				item.TenDvu = varTenDvu;
			
				item.TenDvuEn = varTenDvuEn;
			
				item.SttHthi = varSttHthi;
			
				item.TrangThai = varTrangThai;
			
				item.IdVungKs = varIdVungKs;
			
				item.MoTa = varMoTa;
			
				item.DonGia = varDonGia;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdDvuColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdLoaiDvuColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn MaDvuColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn TenDvuColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn TenDvuEnColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn SttHthiColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangThaiColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn IdVungKsColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn MoTaColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn DonGiaColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdDvu = @"ID_DVU";
			 public static string IdLoaiDvu = @"ID_LOAI_DVU";
			 public static string MaDvu = @"MA_DVU";
			 public static string TenDvu = @"TEN_DVU";
			 public static string TenDvuEn = @"TEN_DVU_EN";
			 public static string SttHthi = @"STT_HTHI";
			 public static string TrangThai = @"TRANG_THAI";
			 public static string IdVungKs = @"ID_VUNG_KS";
			 public static string MoTa = @"MO_TA";
			 public static string DonGia = @"DON_GIA";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
