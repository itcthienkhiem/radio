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
	/// Strongly-typed collection for the DDmucChung class.
	/// </summary>
    [Serializable]
	public partial class DDmucChungCollection : ActiveList<DDmucChung, DDmucChungCollection>
	{	   
		public DDmucChungCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DDmucChungCollection</returns>
		public DDmucChungCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DDmucChung o = this[i];
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
	/// This is an ActiveRecord class which wraps the D_DMUC_CHUNG table.
	/// </summary>
	[Serializable]
	public partial class DDmucChung : ActiveRecord<DDmucChung>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DDmucChung()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DDmucChung(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DDmucChung(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DDmucChung(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("D_DMUC_CHUNG", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarMa = new TableSchema.TableColumn(schema);
				colvarMa.ColumnName = "MA";
				colvarMa.DataType = DbType.AnsiString;
				colvarMa.MaxLength = 20;
				colvarMa.AutoIncrement = false;
				colvarMa.IsNullable = false;
				colvarMa.IsPrimaryKey = true;
				colvarMa.IsForeignKey = false;
				colvarMa.IsReadOnly = false;
				colvarMa.DefaultSetting = @"";
				colvarMa.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMa);
				
				TableSchema.TableColumn colvarLoai = new TableSchema.TableColumn(schema);
				colvarLoai.ColumnName = "LOAI";
				colvarLoai.DataType = DbType.AnsiString;
				colvarLoai.MaxLength = 20;
				colvarLoai.AutoIncrement = false;
				colvarLoai.IsNullable = false;
				colvarLoai.IsPrimaryKey = true;
				colvarLoai.IsForeignKey = false;
				colvarLoai.IsReadOnly = false;
				colvarLoai.DefaultSetting = @"";
				colvarLoai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLoai);
				
				TableSchema.TableColumn colvarTen = new TableSchema.TableColumn(schema);
				colvarTen.ColumnName = "TEN";
				colvarTen.DataType = DbType.String;
				colvarTen.MaxLength = 100;
				colvarTen.AutoIncrement = false;
				colvarTen.IsNullable = false;
				colvarTen.IsPrimaryKey = false;
				colvarTen.IsForeignKey = false;
				colvarTen.IsReadOnly = false;
				colvarTen.DefaultSetting = @"";
				colvarTen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTen);
				
				TableSchema.TableColumn colvarSttHthi = new TableSchema.TableColumn(schema);
				colvarSttHthi.ColumnName = "STT_HTHI";
				colvarSttHthi.DataType = DbType.Int32;
				colvarSttHthi.MaxLength = 0;
				colvarSttHthi.AutoIncrement = false;
				colvarSttHthi.IsNullable = false;
				colvarSttHthi.IsPrimaryKey = false;
				colvarSttHthi.IsForeignKey = false;
				colvarSttHthi.IsReadOnly = false;
				
						colvarSttHthi.DefaultSetting = @"((1))";
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
				
						colvarTrangThai.DefaultSetting = @"((1))";
				colvarTrangThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTrangThai);
				
				TableSchema.TableColumn colvarMotaThem = new TableSchema.TableColumn(schema);
				colvarMotaThem.ColumnName = "MOTA_THEM";
				colvarMotaThem.DataType = DbType.String;
				colvarMotaThem.MaxLength = 255;
				colvarMotaThem.AutoIncrement = false;
				colvarMotaThem.IsNullable = true;
				colvarMotaThem.IsPrimaryKey = false;
				colvarMotaThem.IsForeignKey = false;
				colvarMotaThem.IsReadOnly = false;
				colvarMotaThem.DefaultSetting = @"";
				colvarMotaThem.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMotaThem);
				
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
				
				TableSchema.TableColumn colvarUploadImage = new TableSchema.TableColumn(schema);
				colvarUploadImage.ColumnName = "UPLOAD_IMAGE";
				colvarUploadImage.DataType = DbType.Int32;
				colvarUploadImage.MaxLength = 0;
				colvarUploadImage.AutoIncrement = false;
				colvarUploadImage.IsNullable = true;
				colvarUploadImage.IsPrimaryKey = false;
				colvarUploadImage.IsForeignKey = false;
				colvarUploadImage.IsReadOnly = false;
				colvarUploadImage.DefaultSetting = @"";
				colvarUploadImage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUploadImage);
				
				TableSchema.TableColumn colvarViewDcm = new TableSchema.TableColumn(schema);
				colvarViewDcm.ColumnName = "VIEW_DCM";
				colvarViewDcm.DataType = DbType.Int32;
				colvarViewDcm.MaxLength = 0;
				colvarViewDcm.AutoIncrement = false;
				colvarViewDcm.IsNullable = true;
				colvarViewDcm.IsPrimaryKey = false;
				colvarViewDcm.IsForeignKey = false;
				colvarViewDcm.IsReadOnly = false;
				colvarViewDcm.DefaultSetting = @"";
				colvarViewDcm.ForeignKeyTableName = "";
				schema.Columns.Add(colvarViewDcm);
				
				TableSchema.TableColumn colvarViewVideo = new TableSchema.TableColumn(schema);
				colvarViewVideo.ColumnName = "VIEW_VIDEO";
				colvarViewVideo.DataType = DbType.Int32;
				colvarViewVideo.MaxLength = 0;
				colvarViewVideo.AutoIncrement = false;
				colvarViewVideo.IsNullable = true;
				colvarViewVideo.IsPrimaryKey = false;
				colvarViewVideo.IsForeignKey = false;
				colvarViewVideo.IsReadOnly = false;
				colvarViewVideo.DefaultSetting = @"";
				colvarViewVideo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarViewVideo);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("D_DMUC_CHUNG",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Ma")]
		[Bindable(true)]
		public string Ma 
		{
			get { return GetColumnValue<string>(Columns.Ma); }
			set { SetColumnValue(Columns.Ma, value); }
		}
		  
		[XmlAttribute("Loai")]
		[Bindable(true)]
		public string Loai 
		{
			get { return GetColumnValue<string>(Columns.Loai); }
			set { SetColumnValue(Columns.Loai, value); }
		}
		  
		[XmlAttribute("Ten")]
		[Bindable(true)]
		public string Ten 
		{
			get { return GetColumnValue<string>(Columns.Ten); }
			set { SetColumnValue(Columns.Ten, value); }
		}
		  
		[XmlAttribute("SttHthi")]
		[Bindable(true)]
		public int SttHthi 
		{
			get { return GetColumnValue<int>(Columns.SttHthi); }
			set { SetColumnValue(Columns.SttHthi, value); }
		}
		  
		[XmlAttribute("TrangThai")]
		[Bindable(true)]
		public int TrangThai 
		{
			get { return GetColumnValue<int>(Columns.TrangThai); }
			set { SetColumnValue(Columns.TrangThai, value); }
		}
		  
		[XmlAttribute("MotaThem")]
		[Bindable(true)]
		public string MotaThem 
		{
			get { return GetColumnValue<string>(Columns.MotaThem); }
			set { SetColumnValue(Columns.MotaThem, value); }
		}
		  
		[XmlAttribute("NguoiTao")]
		[Bindable(true)]
		public string NguoiTao 
		{
			get { return GetColumnValue<string>(Columns.NguoiTao); }
			set { SetColumnValue(Columns.NguoiTao, value); }
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
		  
		[XmlAttribute("NgaySua")]
		[Bindable(true)]
		public DateTime? NgaySua 
		{
			get { return GetColumnValue<DateTime?>(Columns.NgaySua); }
			set { SetColumnValue(Columns.NgaySua, value); }
		}
		  
		[XmlAttribute("UploadImage")]
		[Bindable(true)]
		public int? UploadImage 
		{
			get { return GetColumnValue<int?>(Columns.UploadImage); }
			set { SetColumnValue(Columns.UploadImage, value); }
		}
		  
		[XmlAttribute("ViewDcm")]
		[Bindable(true)]
		public int? ViewDcm 
		{
			get { return GetColumnValue<int?>(Columns.ViewDcm); }
			set { SetColumnValue(Columns.ViewDcm, value); }
		}
		  
		[XmlAttribute("ViewVideo")]
		[Bindable(true)]
		public int? ViewVideo 
		{
			get { return GetColumnValue<int?>(Columns.ViewVideo); }
			set { SetColumnValue(Columns.ViewVideo, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varMa,string varLoai,string varTen,int varSttHthi,int varTrangThai,string varMotaThem,string varNguoiTao,DateTime? varNgayTao,string varNguoiSua,DateTime? varNgaySua,int? varUploadImage,int? varViewDcm,int? varViewVideo)
		{
			DDmucChung item = new DDmucChung();
			
			item.Ma = varMa;
			
			item.Loai = varLoai;
			
			item.Ten = varTen;
			
			item.SttHthi = varSttHthi;
			
			item.TrangThai = varTrangThai;
			
			item.MotaThem = varMotaThem;
			
			item.NguoiTao = varNguoiTao;
			
			item.NgayTao = varNgayTao;
			
			item.NguoiSua = varNguoiSua;
			
			item.NgaySua = varNgaySua;
			
			item.UploadImage = varUploadImage;
			
			item.ViewDcm = varViewDcm;
			
			item.ViewVideo = varViewVideo;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(string varMa,string varLoai,string varTen,int varSttHthi,int varTrangThai,string varMotaThem,string varNguoiTao,DateTime? varNgayTao,string varNguoiSua,DateTime? varNgaySua,int? varUploadImage,int? varViewDcm,int? varViewVideo)
		{
			DDmucChung item = new DDmucChung();
			
				item.Ma = varMa;
			
				item.Loai = varLoai;
			
				item.Ten = varTen;
			
				item.SttHthi = varSttHthi;
			
				item.TrangThai = varTrangThai;
			
				item.MotaThem = varMotaThem;
			
				item.NguoiTao = varNguoiTao;
			
				item.NgayTao = varNgayTao;
			
				item.NguoiSua = varNguoiSua;
			
				item.NgaySua = varNgaySua;
			
				item.UploadImage = varUploadImage;
			
				item.ViewDcm = varViewDcm;
			
				item.ViewVideo = varViewVideo;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn MaColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn LoaiColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TenColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SttHthiColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn TrangThaiColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn MotaThemColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiTaoColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn NgayTaoColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn NguoiSuaColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn NgaySuaColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn UploadImageColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn ViewDcmColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn ViewVideoColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Ma = @"MA";
			 public static string Loai = @"LOAI";
			 public static string Ten = @"TEN";
			 public static string SttHthi = @"STT_HTHI";
			 public static string TrangThai = @"TRANG_THAI";
			 public static string MotaThem = @"MOTA_THEM";
			 public static string NguoiTao = @"NGUOI_TAO";
			 public static string NgayTao = @"NGAY_TAO";
			 public static string NguoiSua = @"NGUOI_SUA";
			 public static string NgaySua = @"NGAY_SUA";
			 public static string UploadImage = @"UPLOAD_IMAGE";
			 public static string ViewDcm = @"VIEW_DCM";
			 public static string ViewVideo = @"VIEW_VIDEO";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
