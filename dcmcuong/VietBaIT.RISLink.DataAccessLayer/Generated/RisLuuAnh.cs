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
	/// Strongly-typed collection for the RisLuuAnh class.
	/// </summary>
    [Serializable]
	public partial class RisLuuAnhCollection : ActiveList<RisLuuAnh, RisLuuAnhCollection>
	{	   
		public RisLuuAnhCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>RisLuuAnhCollection</returns>
		public RisLuuAnhCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                RisLuuAnh o = this[i];
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
	/// This is an ActiveRecord class which wraps the RIS_LUU_ANH table.
	/// </summary>
	[Serializable]
	public partial class RisLuuAnh : ActiveRecord<RisLuuAnh>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public RisLuuAnh()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public RisLuuAnh(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public RisLuuAnh(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public RisLuuAnh(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("RIS_LUU_ANH", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "ID";
				colvarId.DataType = DbType.Int32;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = true;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = true;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				colvarId.DefaultSetting = @"";
				colvarId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarId);
				
				TableSchema.TableColumn colvarIdPhieuCtiet = new TableSchema.TableColumn(schema);
				colvarIdPhieuCtiet.ColumnName = "ID_PHIEU_CTIET";
				colvarIdPhieuCtiet.DataType = DbType.Int64;
				colvarIdPhieuCtiet.MaxLength = 0;
				colvarIdPhieuCtiet.AutoIncrement = false;
				colvarIdPhieuCtiet.IsNullable = false;
				colvarIdPhieuCtiet.IsPrimaryKey = false;
				colvarIdPhieuCtiet.IsForeignKey = false;
				colvarIdPhieuCtiet.IsReadOnly = false;
				colvarIdPhieuCtiet.DefaultSetting = @"";
				colvarIdPhieuCtiet.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdPhieuCtiet);
				
				TableSchema.TableColumn colvarSopInstanceUid = new TableSchema.TableColumn(schema);
				colvarSopInstanceUid.ColumnName = "SOP_INSTANCE_UID";
				colvarSopInstanceUid.DataType = DbType.String;
				colvarSopInstanceUid.MaxLength = 100;
				colvarSopInstanceUid.AutoIncrement = false;
				colvarSopInstanceUid.IsNullable = false;
				colvarSopInstanceUid.IsPrimaryKey = false;
				colvarSopInstanceUid.IsForeignKey = false;
				colvarSopInstanceUid.IsReadOnly = false;
				colvarSopInstanceUid.DefaultSetting = @"";
				colvarSopInstanceUid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSopInstanceUid);
				
				TableSchema.TableColumn colvarSeriesInstanceUid = new TableSchema.TableColumn(schema);
				colvarSeriesInstanceUid.ColumnName = "SERIES_INSTANCE_UID";
				colvarSeriesInstanceUid.DataType = DbType.String;
				colvarSeriesInstanceUid.MaxLength = 100;
				colvarSeriesInstanceUid.AutoIncrement = false;
				colvarSeriesInstanceUid.IsNullable = false;
				colvarSeriesInstanceUid.IsPrimaryKey = false;
				colvarSeriesInstanceUid.IsForeignKey = false;
				colvarSeriesInstanceUid.IsReadOnly = false;
				colvarSeriesInstanceUid.DefaultSetting = @"";
				colvarSeriesInstanceUid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeriesInstanceUid);
				
				TableSchema.TableColumn colvarDdanAnh = new TableSchema.TableColumn(schema);
				colvarDdanAnh.ColumnName = "DDAN_ANH";
				colvarDdanAnh.DataType = DbType.String;
				colvarDdanAnh.MaxLength = 500;
				colvarDdanAnh.AutoIncrement = false;
				colvarDdanAnh.IsNullable = true;
				colvarDdanAnh.IsPrimaryKey = false;
				colvarDdanAnh.IsForeignKey = false;
				colvarDdanAnh.IsReadOnly = false;
				colvarDdanAnh.DefaultSetting = @"";
				colvarDdanAnh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDdanAnh);
				
				TableSchema.TableColumn colvarDdanAnhThumb = new TableSchema.TableColumn(schema);
				colvarDdanAnhThumb.ColumnName = "DDAN_ANH_THUMB";
				colvarDdanAnhThumb.DataType = DbType.String;
				colvarDdanAnhThumb.MaxLength = 500;
				colvarDdanAnhThumb.AutoIncrement = false;
				colvarDdanAnhThumb.IsNullable = true;
				colvarDdanAnhThumb.IsPrimaryKey = false;
				colvarDdanAnhThumb.IsForeignKey = false;
				colvarDdanAnhThumb.IsReadOnly = false;
				colvarDdanAnhThumb.DefaultSetting = @"";
				colvarDdanAnhThumb.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDdanAnhThumb);
				
				TableSchema.TableColumn colvarTrangThai = new TableSchema.TableColumn(schema);
				colvarTrangThai.ColumnName = "TRANG_THAI";
				colvarTrangThai.DataType = DbType.Int32;
				colvarTrangThai.MaxLength = 0;
				colvarTrangThai.AutoIncrement = false;
				colvarTrangThai.IsNullable = true;
				colvarTrangThai.IsPrimaryKey = false;
				colvarTrangThai.IsForeignKey = false;
				colvarTrangThai.IsReadOnly = false;
				colvarTrangThai.DefaultSetting = @"";
				colvarTrangThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangThai);
				
				TableSchema.TableColumn colvarNgayTao = new TableSchema.TableColumn(schema);
				colvarNgayTao.ColumnName = "NGAY_TAO";
				colvarNgayTao.DataType = DbType.DateTime;
				colvarNgayTao.MaxLength = 0;
				colvarNgayTao.AutoIncrement = false;
				colvarNgayTao.IsNullable = true;
				colvarNgayTao.IsPrimaryKey = false;
				colvarNgayTao.IsForeignKey = false;
				colvarNgayTao.IsReadOnly = false;
				
						colvarNgayTao.DefaultSetting = @"(getdate())";
				colvarNgayTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayTao);
				
				TableSchema.TableColumn colvarStt = new TableSchema.TableColumn(schema);
				colvarStt.ColumnName = "STT";
				colvarStt.DataType = DbType.Int32;
				colvarStt.MaxLength = 0;
				colvarStt.AutoIncrement = false;
				colvarStt.IsNullable = true;
				colvarStt.IsPrimaryKey = false;
				colvarStt.IsForeignKey = false;
				colvarStt.IsReadOnly = false;
				colvarStt.DefaultSetting = @"";
				colvarStt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStt);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("RIS_LUU_ANH",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Id")]
		[Bindable(true)]
		public int Id 
		{
			get { return GetColumnValue<int>(Columns.Id); }
			set { SetColumnValue(Columns.Id, value); }
		}
		  
		[XmlAttribute("IdPhieuCtiet")]
		[Bindable(true)]
		public long IdPhieuCtiet 
		{
			get { return GetColumnValue<long>(Columns.IdPhieuCtiet); }
			set { SetColumnValue(Columns.IdPhieuCtiet, value); }
		}
		  
		[XmlAttribute("SopInstanceUid")]
		[Bindable(true)]
		public string SopInstanceUid 
		{
			get { return GetColumnValue<string>(Columns.SopInstanceUid); }
			set { SetColumnValue(Columns.SopInstanceUid, value); }
		}
		  
		[XmlAttribute("SeriesInstanceUid")]
		[Bindable(true)]
		public string SeriesInstanceUid 
		{
			get { return GetColumnValue<string>(Columns.SeriesInstanceUid); }
			set { SetColumnValue(Columns.SeriesInstanceUid, value); }
		}
		  
		[XmlAttribute("DdanAnh")]
		[Bindable(true)]
		public string DdanAnh 
		{
			get { return GetColumnValue<string>(Columns.DdanAnh); }
			set { SetColumnValue(Columns.DdanAnh, value); }
		}
		  
		[XmlAttribute("DdanAnhThumb")]
		[Bindable(true)]
		public string DdanAnhThumb 
		{
			get { return GetColumnValue<string>(Columns.DdanAnhThumb); }
			set { SetColumnValue(Columns.DdanAnhThumb, value); }
		}
		  
		[XmlAttribute("TrangThai")]
		[Bindable(true)]
		public int? TrangThai 
		{
			get { return GetColumnValue<int?>(Columns.TrangThai); }
			set { SetColumnValue(Columns.TrangThai, value); }
		}
		  
		[XmlAttribute("NgayTao")]
		[Bindable(true)]
		public DateTime? NgayTao 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayTao); }
			set { SetColumnValue(Columns.NgayTao, value); }
		}
		  
		[XmlAttribute("Stt")]
		[Bindable(true)]
		public int? Stt 
		{
			get { return GetColumnValue<int?>(Columns.Stt); }
			set { SetColumnValue(Columns.Stt, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(long varIdPhieuCtiet,string varSopInstanceUid,string varSeriesInstanceUid,string varDdanAnh,string varDdanAnhThumb,int? varTrangThai,DateTime? varNgayTao,int? varStt)
		{
			RisLuuAnh item = new RisLuuAnh();
			
			item.IdPhieuCtiet = varIdPhieuCtiet;
			
			item.SopInstanceUid = varSopInstanceUid;
			
			item.SeriesInstanceUid = varSeriesInstanceUid;
			
			item.DdanAnh = varDdanAnh;
			
			item.DdanAnhThumb = varDdanAnhThumb;
			
			item.TrangThai = varTrangThai;
			
			item.NgayTao = varNgayTao;
			
			item.Stt = varStt;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,long varIdPhieuCtiet,string varSopInstanceUid,string varSeriesInstanceUid,string varDdanAnh,string varDdanAnhThumb,int? varTrangThai,DateTime? varNgayTao,int? varStt)
		{
			RisLuuAnh item = new RisLuuAnh();
			
				item.Id = varId;
			
				item.IdPhieuCtiet = varIdPhieuCtiet;
			
				item.SopInstanceUid = varSopInstanceUid;
			
				item.SeriesInstanceUid = varSeriesInstanceUid;
			
				item.DdanAnh = varDdanAnh;
			
				item.DdanAnhThumb = varDdanAnhThumb;
			
				item.TrangThai = varTrangThai;
			
				item.NgayTao = varNgayTao;
			
				item.Stt = varStt;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn IdPhieuCtietColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn SopInstanceUidColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SeriesInstanceUidColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn DdanAnhColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn DdanAnhThumbColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangThaiColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn SttColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"ID";
			 public static string IdPhieuCtiet = @"ID_PHIEU_CTIET";
			 public static string SopInstanceUid = @"SOP_INSTANCE_UID";
			 public static string SeriesInstanceUid = @"SERIES_INSTANCE_UID";
			 public static string DdanAnh = @"DDAN_ANH";
			 public static string DdanAnhThumb = @"DDAN_ANH_THUMB";
			 public static string TrangThai = @"TRANG_THAI";
			 public static string NgayTao = @"NGAY_TAO";
			 public static string Stt = @"STT";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}