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
	/// Strongly-typed collection for the DPhongBan class.
	/// </summary>
    [Serializable]
	public partial class DPhongBanCollection : ActiveList<DPhongBan, DPhongBanCollection>
	{	   
		public DPhongBanCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DPhongBanCollection</returns>
		public DPhongBanCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DPhongBan o = this[i];
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
	/// This is an ActiveRecord class which wraps the D_PHONG_BAN table.
	/// </summary>
	[Serializable]
	public partial class DPhongBan : ActiveRecord<DPhongBan>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DPhongBan()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DPhongBan(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DPhongBan(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DPhongBan(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("D_PHONG_BAN", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarIdKhoa = new TableSchema.TableColumn(schema);
				colvarIdKhoa.ColumnName = "ID_KHOA";
				colvarIdKhoa.DataType = DbType.Int32;
				colvarIdKhoa.MaxLength = 0;
				colvarIdKhoa.AutoIncrement = true;
				colvarIdKhoa.IsNullable = false;
				colvarIdKhoa.IsPrimaryKey = true;
				colvarIdKhoa.IsForeignKey = false;
				colvarIdKhoa.IsReadOnly = false;
				colvarIdKhoa.DefaultSetting = @"";
				colvarIdKhoa.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKhoa);
				
				TableSchema.TableColumn colvarMaPban = new TableSchema.TableColumn(schema);
				colvarMaPban.ColumnName = "MA_PBAN";
				colvarMaPban.DataType = DbType.AnsiString;
				colvarMaPban.MaxLength = 20;
				colvarMaPban.AutoIncrement = false;
				colvarMaPban.IsNullable = false;
				colvarMaPban.IsPrimaryKey = false;
				colvarMaPban.IsForeignKey = false;
				colvarMaPban.IsReadOnly = false;
				colvarMaPban.DefaultSetting = @"";
				colvarMaPban.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMaPban);
				
				TableSchema.TableColumn colvarTenPban = new TableSchema.TableColumn(schema);
				colvarTenPban.ColumnName = "TEN_PBAN";
				colvarTenPban.DataType = DbType.String;
				colvarTenPban.MaxLength = 200;
				colvarTenPban.AutoIncrement = false;
				colvarTenPban.IsNullable = true;
				colvarTenPban.IsPrimaryKey = false;
				colvarTenPban.IsForeignKey = false;
				colvarTenPban.IsReadOnly = false;
				colvarTenPban.DefaultSetting = @"";
				colvarTenPban.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTenPban);
				
				TableSchema.TableColumn colvarIdKhoaCha = new TableSchema.TableColumn(schema);
				colvarIdKhoaCha.ColumnName = "ID_KHOA_CHA";
				colvarIdKhoaCha.DataType = DbType.Int32;
				colvarIdKhoaCha.MaxLength = 0;
				colvarIdKhoaCha.AutoIncrement = false;
				colvarIdKhoaCha.IsNullable = true;
				colvarIdKhoaCha.IsPrimaryKey = false;
				colvarIdKhoaCha.IsForeignKey = false;
				colvarIdKhoaCha.IsReadOnly = false;
				colvarIdKhoaCha.DefaultSetting = @"";
				colvarIdKhoaCha.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIdKhoaCha);
				
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
				
				TableSchema.TableColumn colvarKieuPban = new TableSchema.TableColumn(schema);
				colvarKieuPban.ColumnName = "KIEU_PBAN";
				colvarKieuPban.DataType = DbType.AnsiString;
				colvarKieuPban.MaxLength = 50;
				colvarKieuPban.AutoIncrement = false;
				colvarKieuPban.IsNullable = true;
				colvarKieuPban.IsPrimaryKey = false;
				colvarKieuPban.IsForeignKey = false;
				colvarKieuPban.IsReadOnly = false;
				colvarKieuPban.DefaultSetting = @"";
				colvarKieuPban.ForeignKeyTableName = "";
				schema.Columns.Add(colvarKieuPban);
				
				TableSchema.TableColumn colvarLoaiPban = new TableSchema.TableColumn(schema);
				colvarLoaiPban.ColumnName = "LOAI_PBAN";
				colvarLoaiPban.DataType = DbType.AnsiString;
				colvarLoaiPban.MaxLength = 20;
				colvarLoaiPban.AutoIncrement = false;
				colvarLoaiPban.IsNullable = true;
				colvarLoaiPban.IsPrimaryKey = false;
				colvarLoaiPban.IsForeignKey = false;
				colvarLoaiPban.IsReadOnly = false;
				colvarLoaiPban.DefaultSetting = @"";
				colvarLoaiPban.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLoaiPban);
				
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
				
				TableSchema.TableColumn colvarNgaySua = new TableSchema.TableColumn(schema);
				colvarNgaySua.ColumnName = "NGAY_SUA";
				colvarNgaySua.DataType = DbType.DateTime;
				colvarNgaySua.MaxLength = 0;
				colvarNgaySua.AutoIncrement = false;
				colvarNgaySua.IsNullable = true;
				colvarNgaySua.IsPrimaryKey = false;
				colvarNgaySua.IsForeignKey = false;
				colvarNgaySua.IsReadOnly = false;
				colvarNgaySua.DefaultSetting = @"";
				colvarNgaySua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgaySua);
				
				TableSchema.TableColumn colvarNgayTao = new TableSchema.TableColumn(schema);
				colvarNgayTao.ColumnName = "NGAY_TAO";
				colvarNgayTao.DataType = DbType.DateTime;
				colvarNgayTao.MaxLength = 0;
				colvarNgayTao.AutoIncrement = false;
				colvarNgayTao.IsNullable = true;
				colvarNgayTao.IsPrimaryKey = false;
				colvarNgayTao.IsForeignKey = false;
				colvarNgayTao.IsReadOnly = false;
				colvarNgayTao.DefaultSetting = @"";
				colvarNgayTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNgayTao);
				
				TableSchema.TableColumn colvarNguoiSua = new TableSchema.TableColumn(schema);
				colvarNguoiSua.ColumnName = "NGUOI_SUA";
				colvarNguoiSua.DataType = DbType.String;
				colvarNguoiSua.MaxLength = 50;
				colvarNguoiSua.AutoIncrement = false;
				colvarNguoiSua.IsNullable = true;
				colvarNguoiSua.IsPrimaryKey = false;
				colvarNguoiSua.IsForeignKey = false;
				colvarNguoiSua.IsReadOnly = false;
				colvarNguoiSua.DefaultSetting = @"";
				colvarNguoiSua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiSua);
				
				TableSchema.TableColumn colvarNguoiTao = new TableSchema.TableColumn(schema);
				colvarNguoiTao.ColumnName = "NGUOI_TAO";
				colvarNguoiTao.DataType = DbType.String;
				colvarNguoiTao.MaxLength = 50;
				colvarNguoiTao.AutoIncrement = false;
				colvarNguoiTao.IsNullable = true;
				colvarNguoiTao.IsPrimaryKey = false;
				colvarNguoiTao.IsForeignKey = false;
				colvarNguoiTao.IsReadOnly = false;
				colvarNguoiTao.DefaultSetting = @"";
				colvarNguoiTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNguoiTao);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("D_PHONG_BAN",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("IdKhoa")]
		[Bindable(true)]
		public int IdKhoa 
		{
			get { return GetColumnValue<int>(Columns.IdKhoa); }
			set { SetColumnValue(Columns.IdKhoa, value); }
		}
		  
		[XmlAttribute("MaPban")]
		[Bindable(true)]
		public string MaPban 
		{
			get { return GetColumnValue<string>(Columns.MaPban); }
			set { SetColumnValue(Columns.MaPban, value); }
		}
		  
		[XmlAttribute("TenPban")]
		[Bindable(true)]
		public string TenPban 
		{
			get { return GetColumnValue<string>(Columns.TenPban); }
			set { SetColumnValue(Columns.TenPban, value); }
		}
		  
		[XmlAttribute("IdKhoaCha")]
		[Bindable(true)]
		public int? IdKhoaCha 
		{
			get { return GetColumnValue<int?>(Columns.IdKhoaCha); }
			set { SetColumnValue(Columns.IdKhoaCha, value); }
		}
		  
		[XmlAttribute("SttHthi")]
		[Bindable(true)]
		public int? SttHthi 
		{
			get { return GetColumnValue<int?>(Columns.SttHthi); }
			set { SetColumnValue(Columns.SttHthi, value); }
		}
		  
		[XmlAttribute("KieuPban")]
		[Bindable(true)]
		public string KieuPban 
		{
			get { return GetColumnValue<string>(Columns.KieuPban); }
			set { SetColumnValue(Columns.KieuPban, value); }
		}
		  
		[XmlAttribute("LoaiPban")]
		[Bindable(true)]
		public string LoaiPban 
		{
			get { return GetColumnValue<string>(Columns.LoaiPban); }
			set { SetColumnValue(Columns.LoaiPban, value); }
		}
		  
		[XmlAttribute("MoTa")]
		[Bindable(true)]
		public string MoTa 
		{
			get { return GetColumnValue<string>(Columns.MoTa); }
			set { SetColumnValue(Columns.MoTa, value); }
		}
		  
		[XmlAttribute("NgaySua")]
		[Bindable(true)]
		public DateTime? NgaySua 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgaySua); }
			set { SetColumnValue(Columns.NgaySua, value); }
		}
		  
		[XmlAttribute("NgayTao")]
		[Bindable(true)]
		public DateTime? NgayTao 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgayTao); }
			set { SetColumnValue(Columns.NgayTao, value); }
		}
		  
		[XmlAttribute("NguoiSua")]
		[Bindable(true)]
		public string NguoiSua 
		{
			get { return GetColumnValue<string>(Columns.NguoiSua); }
			set { SetColumnValue(Columns.NguoiSua, value); }
		}
		  
		[XmlAttribute("NguoiTao")]
		[Bindable(true)]
		public string NguoiTao 
		{
			get { return GetColumnValue<string>(Columns.NguoiTao); }
			set { SetColumnValue(Columns.NguoiTao, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varMaPban,string varTenPban,int? varIdKhoaCha,int? varSttHthi,string varKieuPban,string varLoaiPban,string varMoTa,DateTime? varNgaySua,DateTime? varNgayTao,string varNguoiSua,string varNguoiTao)
		{
			DPhongBan item = new DPhongBan();
			
			item.MaPban = varMaPban;
			
			item.TenPban = varTenPban;
			
			item.IdKhoaCha = varIdKhoaCha;
			
			item.SttHthi = varSttHthi;
			
			item.KieuPban = varKieuPban;
			
			item.LoaiPban = varLoaiPban;
			
			item.MoTa = varMoTa;
			
			item.NgaySua = varNgaySua;
			
			item.NgayTao = varNgayTao;
			
			item.NguoiSua = varNguoiSua;
			
			item.NguoiTao = varNguoiTao;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varIdKhoa,string varMaPban,string varTenPban,int? varIdKhoaCha,int? varSttHthi,string varKieuPban,string varLoaiPban,string varMoTa,DateTime? varNgaySua,DateTime? varNgayTao,string varNguoiSua,string varNguoiTao)
		{
			DPhongBan item = new DPhongBan();
			
				item.IdKhoa = varIdKhoa;
			
				item.MaPban = varMaPban;
			
				item.TenPban = varTenPban;
			
				item.IdKhoaCha = varIdKhoaCha;
			
				item.SttHthi = varSttHthi;
			
				item.KieuPban = varKieuPban;
			
				item.LoaiPban = varLoaiPban;
			
				item.MoTa = varMoTa;
			
				item.NgaySua = varNgaySua;
			
				item.NgayTao = varNgayTao;
			
				item.NguoiSua = varNguoiSua;
			
				item.NguoiTao = varNguoiTao;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdKhoaColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn MaPbanColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TenPbanColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn IdKhoaChaColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SttHthiColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn KieuPbanColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn LoaiPbanColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn MoTaColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string IdKhoa = @"ID_KHOA";
			 public static string MaPban = @"MA_PBAN";
			 public static string TenPban = @"TEN_PBAN";
			 public static string IdKhoaCha = @"ID_KHOA_CHA";
			 public static string SttHthi = @"STT_HTHI";
			 public static string KieuPban = @"KIEU_PBAN";
			 public static string LoaiPban = @"LOAI_PBAN";
			 public static string MoTa = @"MO_TA";
			 public static string NgaySua = @"NGAY_SUA";
			 public static string NgayTao = @"NGAY_TAO";
			 public static string NguoiSua = @"NGUOI_SUA";
			 public static string NguoiTao = @"NGUOI_TAO";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}