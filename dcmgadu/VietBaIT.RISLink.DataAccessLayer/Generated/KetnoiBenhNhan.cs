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
	/// Strongly-typed collection for the KetnoiBenhNhan class.
	/// </summary>
    [Serializable]
	public partial class KetnoiBenhNhanCollection : ActiveList<KetnoiBenhNhan, KetnoiBenhNhanCollection>
	{	   
		public KetnoiBenhNhanCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>KetnoiBenhNhanCollection</returns>
		public KetnoiBenhNhanCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                KetnoiBenhNhan o = this[i];
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
	/// This is an ActiveRecord class which wraps the KETNOI_BENH_NHAN table.
	/// </summary>
	[Serializable]
	public partial class KetnoiBenhNhan : ActiveRecord<KetnoiBenhNhan>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public KetnoiBenhNhan()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public KetnoiBenhNhan(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public KetnoiBenhNhan(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public KetnoiBenhNhan(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("KETNOI_BENH_NHAN", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarHisIdBnhan = new TableSchema.TableColumn(schema);
				colvarHisIdBnhan.ColumnName = "HIS_ID_BNHAN";
				colvarHisIdBnhan.DataType = DbType.Int32;
				colvarHisIdBnhan.MaxLength = 0;
				colvarHisIdBnhan.AutoIncrement = false;
				colvarHisIdBnhan.IsNullable = false;
				colvarHisIdBnhan.IsPrimaryKey = true;
				colvarHisIdBnhan.IsForeignKey = false;
				colvarHisIdBnhan.IsReadOnly = false;
				colvarHisIdBnhan.DefaultSetting = @"";
				colvarHisIdBnhan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisIdBnhan);
				
				TableSchema.TableColumn colvarHisMaLanKham = new TableSchema.TableColumn(schema);
				colvarHisMaLanKham.ColumnName = "HIS_MA_LAN_KHAM";
				colvarHisMaLanKham.DataType = DbType.String;
				colvarHisMaLanKham.MaxLength = 50;
				colvarHisMaLanKham.AutoIncrement = false;
				colvarHisMaLanKham.IsNullable = false;
				colvarHisMaLanKham.IsPrimaryKey = true;
				colvarHisMaLanKham.IsForeignKey = false;
				colvarHisMaLanKham.IsReadOnly = false;
				colvarHisMaLanKham.DefaultSetting = @"";
				colvarHisMaLanKham.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisMaLanKham);
				
				TableSchema.TableColumn colvarHisSoBa = new TableSchema.TableColumn(schema);
				colvarHisSoBa.ColumnName = "HIS_SO_BA";
				colvarHisSoBa.DataType = DbType.String;
				colvarHisSoBa.MaxLength = 50;
				colvarHisSoBa.AutoIncrement = false;
				colvarHisSoBa.IsNullable = false;
				colvarHisSoBa.IsPrimaryKey = false;
				colvarHisSoBa.IsForeignKey = false;
				colvarHisSoBa.IsReadOnly = false;
				colvarHisSoBa.DefaultSetting = @"";
				colvarHisSoBa.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisSoBa);
				
				TableSchema.TableColumn colvarHisMaPhieu = new TableSchema.TableColumn(schema);
				colvarHisMaPhieu.ColumnName = "HIS_MA_PHIEU";
				colvarHisMaPhieu.DataType = DbType.String;
				colvarHisMaPhieu.MaxLength = 50;
				colvarHisMaPhieu.AutoIncrement = false;
				colvarHisMaPhieu.IsNullable = false;
				colvarHisMaPhieu.IsPrimaryKey = true;
				colvarHisMaPhieu.IsForeignKey = false;
				colvarHisMaPhieu.IsReadOnly = false;
				colvarHisMaPhieu.DefaultSetting = @"";
				colvarHisMaPhieu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisMaPhieu);
				
				TableSchema.TableColumn colvarHisMaBenh = new TableSchema.TableColumn(schema);
				colvarHisMaBenh.ColumnName = "HIS_MA_BENH";
				colvarHisMaBenh.DataType = DbType.String;
				colvarHisMaBenh.MaxLength = 50;
				colvarHisMaBenh.AutoIncrement = false;
				colvarHisMaBenh.IsNullable = true;
				colvarHisMaBenh.IsPrimaryKey = false;
				colvarHisMaBenh.IsForeignKey = false;
				colvarHisMaBenh.IsReadOnly = false;
				colvarHisMaBenh.DefaultSetting = @"";
				colvarHisMaBenh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisMaBenh);
				
				TableSchema.TableColumn colvarHisChanDoan = new TableSchema.TableColumn(schema);
				colvarHisChanDoan.ColumnName = "HIS_CHAN_DOAN";
				colvarHisChanDoan.DataType = DbType.String;
				colvarHisChanDoan.MaxLength = 100;
				colvarHisChanDoan.AutoIncrement = false;
				colvarHisChanDoan.IsNullable = true;
				colvarHisChanDoan.IsPrimaryKey = false;
				colvarHisChanDoan.IsForeignKey = false;
				colvarHisChanDoan.IsReadOnly = false;
				colvarHisChanDoan.DefaultSetting = @"";
				colvarHisChanDoan.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisChanDoan);
				
				TableSchema.TableColumn colvarHisMaDtuong = new TableSchema.TableColumn(schema);
				colvarHisMaDtuong.ColumnName = "HIS_MA_DTUONG";
				colvarHisMaDtuong.DataType = DbType.String;
				colvarHisMaDtuong.MaxLength = 50;
				colvarHisMaDtuong.AutoIncrement = false;
				colvarHisMaDtuong.IsNullable = true;
				colvarHisMaDtuong.IsPrimaryKey = false;
				colvarHisMaDtuong.IsForeignKey = false;
				colvarHisMaDtuong.IsReadOnly = false;
				colvarHisMaDtuong.DefaultSetting = @"";
				colvarHisMaDtuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisMaDtuong);
				
				TableSchema.TableColumn colvarHisSoBhyt = new TableSchema.TableColumn(schema);
				colvarHisSoBhyt.ColumnName = "HIS_SO_BHYT";
				colvarHisSoBhyt.DataType = DbType.String;
				colvarHisSoBhyt.MaxLength = 50;
				colvarHisSoBhyt.AutoIncrement = false;
				colvarHisSoBhyt.IsNullable = true;
				colvarHisSoBhyt.IsPrimaryKey = false;
				colvarHisSoBhyt.IsForeignKey = false;
				colvarHisSoBhyt.IsReadOnly = false;
				colvarHisSoBhyt.DefaultSetting = @"";
				colvarHisSoBhyt.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisSoBhyt);
				
				TableSchema.TableColumn colvarHisDiaChi = new TableSchema.TableColumn(schema);
				colvarHisDiaChi.ColumnName = "HIS_DIA_CHI";
				colvarHisDiaChi.DataType = DbType.String;
				colvarHisDiaChi.MaxLength = 200;
				colvarHisDiaChi.AutoIncrement = false;
				colvarHisDiaChi.IsNullable = true;
				colvarHisDiaChi.IsPrimaryKey = false;
				colvarHisDiaChi.IsForeignKey = false;
				colvarHisDiaChi.IsReadOnly = false;
				colvarHisDiaChi.DefaultSetting = @"";
				colvarHisDiaChi.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisDiaChi);
				
				TableSchema.TableColumn colvarHisGtinh = new TableSchema.TableColumn(schema);
				colvarHisGtinh.ColumnName = "HIS_GTINH";
				colvarHisGtinh.DataType = DbType.AnsiString;
				colvarHisGtinh.MaxLength = 50;
				colvarHisGtinh.AutoIncrement = false;
				colvarHisGtinh.IsNullable = true;
				colvarHisGtinh.IsPrimaryKey = false;
				colvarHisGtinh.IsForeignKey = false;
				colvarHisGtinh.IsReadOnly = false;
				colvarHisGtinh.DefaultSetting = @"";
				colvarHisGtinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisGtinh);
				
				TableSchema.TableColumn colvarHisNamSinh = new TableSchema.TableColumn(schema);
				colvarHisNamSinh.ColumnName = "HIS_NAM_SINH";
				colvarHisNamSinh.DataType = DbType.Int32;
				colvarHisNamSinh.MaxLength = 0;
				colvarHisNamSinh.AutoIncrement = false;
				colvarHisNamSinh.IsNullable = true;
				colvarHisNamSinh.IsPrimaryKey = false;
				colvarHisNamSinh.IsForeignKey = false;
				colvarHisNamSinh.IsReadOnly = false;
				colvarHisNamSinh.DefaultSetting = @"";
				colvarHisNamSinh.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisNamSinh);
				
				TableSchema.TableColumn colvarHisIdKhoa = new TableSchema.TableColumn(schema);
				colvarHisIdKhoa.ColumnName = "HIS_ID_KHOA";
				colvarHisIdKhoa.DataType = DbType.Int32;
				colvarHisIdKhoa.MaxLength = 0;
				colvarHisIdKhoa.AutoIncrement = false;
				colvarHisIdKhoa.IsNullable = true;
				colvarHisIdKhoa.IsPrimaryKey = false;
				colvarHisIdKhoa.IsForeignKey = false;
				colvarHisIdKhoa.IsReadOnly = false;
				colvarHisIdKhoa.DefaultSetting = @"";
				colvarHisIdKhoa.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisIdKhoa);
				
				TableSchema.TableColumn colvarHisTenKhoa = new TableSchema.TableColumn(schema);
				colvarHisTenKhoa.ColumnName = "HIS_TEN_KHOA";
				colvarHisTenKhoa.DataType = DbType.String;
				colvarHisTenKhoa.MaxLength = 50;
				colvarHisTenKhoa.AutoIncrement = false;
				colvarHisTenKhoa.IsNullable = true;
				colvarHisTenKhoa.IsPrimaryKey = false;
				colvarHisTenKhoa.IsForeignKey = false;
				colvarHisTenKhoa.IsReadOnly = false;
				colvarHisTenKhoa.DefaultSetting = @"";
				colvarHisTenKhoa.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisTenKhoa);
				
				TableSchema.TableColumn colvarHisIdPhong = new TableSchema.TableColumn(schema);
				colvarHisIdPhong.ColumnName = "HIS_ID_PHONG";
				colvarHisIdPhong.DataType = DbType.Int32;
				colvarHisIdPhong.MaxLength = 0;
				colvarHisIdPhong.AutoIncrement = false;
				colvarHisIdPhong.IsNullable = true;
				colvarHisIdPhong.IsPrimaryKey = false;
				colvarHisIdPhong.IsForeignKey = false;
				colvarHisIdPhong.IsReadOnly = false;
				colvarHisIdPhong.DefaultSetting = @"";
				colvarHisIdPhong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisIdPhong);
				
				TableSchema.TableColumn colvarHisTenPhong = new TableSchema.TableColumn(schema);
				colvarHisTenPhong.ColumnName = "HIS_TEN_PHONG";
				colvarHisTenPhong.DataType = DbType.String;
				colvarHisTenPhong.MaxLength = 50;
				colvarHisTenPhong.AutoIncrement = false;
				colvarHisTenPhong.IsNullable = true;
				colvarHisTenPhong.IsPrimaryKey = false;
				colvarHisTenPhong.IsForeignKey = false;
				colvarHisTenPhong.IsReadOnly = false;
				colvarHisTenPhong.DefaultSetting = @"";
				colvarHisTenPhong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisTenPhong);
				
				TableSchema.TableColumn colvarHisIdGiuong = new TableSchema.TableColumn(schema);
				colvarHisIdGiuong.ColumnName = "HIS_ID_GIUONG";
				colvarHisIdGiuong.DataType = DbType.Int32;
				colvarHisIdGiuong.MaxLength = 0;
				colvarHisIdGiuong.AutoIncrement = false;
				colvarHisIdGiuong.IsNullable = true;
				colvarHisIdGiuong.IsPrimaryKey = false;
				colvarHisIdGiuong.IsForeignKey = false;
				colvarHisIdGiuong.IsReadOnly = false;
				colvarHisIdGiuong.DefaultSetting = @"";
				colvarHisIdGiuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisIdGiuong);
				
				TableSchema.TableColumn colvarHisTenGiuong = new TableSchema.TableColumn(schema);
				colvarHisTenGiuong.ColumnName = "HIS_TEN_GIUONG";
				colvarHisTenGiuong.DataType = DbType.String;
				colvarHisTenGiuong.MaxLength = 50;
				colvarHisTenGiuong.AutoIncrement = false;
				colvarHisTenGiuong.IsNullable = true;
				colvarHisTenGiuong.IsPrimaryKey = false;
				colvarHisTenGiuong.IsForeignKey = false;
				colvarHisTenGiuong.IsReadOnly = false;
				colvarHisTenGiuong.DefaultSetting = @"";
				colvarHisTenGiuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisTenGiuong);
				
				TableSchema.TableColumn colvarHisTrangThai = new TableSchema.TableColumn(schema);
				colvarHisTrangThai.ColumnName = "HIS_TRANG_THAI";
				colvarHisTrangThai.DataType = DbType.Int32;
				colvarHisTrangThai.MaxLength = 0;
				colvarHisTrangThai.AutoIncrement = false;
				colvarHisTrangThai.IsNullable = true;
				colvarHisTrangThai.IsPrimaryKey = false;
				colvarHisTrangThai.IsForeignKey = false;
				colvarHisTrangThai.IsReadOnly = false;
				colvarHisTrangThai.DefaultSetting = @"";
				colvarHisTrangThai.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisTrangThai);
				
				TableSchema.TableColumn colvarHisNgayYeucau = new TableSchema.TableColumn(schema);
				colvarHisNgayYeucau.ColumnName = "HIS_NGAY_YEUCAU";
				colvarHisNgayYeucau.DataType = DbType.DateTime;
				colvarHisNgayYeucau.MaxLength = 0;
				colvarHisNgayYeucau.AutoIncrement = false;
				colvarHisNgayYeucau.IsNullable = true;
				colvarHisNgayYeucau.IsPrimaryKey = false;
				colvarHisNgayYeucau.IsForeignKey = false;
				colvarHisNgayYeucau.IsReadOnly = false;
				colvarHisNgayYeucau.DefaultSetting = @"";
				colvarHisNgayYeucau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisNgayYeucau);
				
				TableSchema.TableColumn colvarHisNgayTao = new TableSchema.TableColumn(schema);
				colvarHisNgayTao.ColumnName = "HIS_NGAY_TAO";
				colvarHisNgayTao.DataType = DbType.DateTime;
				colvarHisNgayTao.MaxLength = 0;
				colvarHisNgayTao.AutoIncrement = false;
				colvarHisNgayTao.IsNullable = true;
				colvarHisNgayTao.IsPrimaryKey = false;
				colvarHisNgayTao.IsForeignKey = false;
				colvarHisNgayTao.IsReadOnly = false;
				colvarHisNgayTao.DefaultSetting = @"";
				colvarHisNgayTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisNgayTao);
				
				TableSchema.TableColumn colvarHisNguoiTao = new TableSchema.TableColumn(schema);
				colvarHisNguoiTao.ColumnName = "HIS_NGUOI_TAO";
				colvarHisNguoiTao.DataType = DbType.String;
				colvarHisNguoiTao.MaxLength = 50;
				colvarHisNguoiTao.AutoIncrement = false;
				colvarHisNguoiTao.IsNullable = true;
				colvarHisNguoiTao.IsPrimaryKey = false;
				colvarHisNguoiTao.IsForeignKey = false;
				colvarHisNguoiTao.IsReadOnly = false;
				colvarHisNguoiTao.DefaultSetting = @"";
				colvarHisNguoiTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisNguoiTao);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("KETNOI_BENH_NHAN",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("HisIdBnhan")]
		[Bindable(true)]
		public int HisIdBnhan 
		{
			get { return GetColumnValue<int>(Columns.HisIdBnhan); }
			set { SetColumnValue(Columns.HisIdBnhan, value); }
		}
		  
		[XmlAttribute("HisMaLanKham")]
		[Bindable(true)]
		public string HisMaLanKham 
		{
			get { return GetColumnValue<string>(Columns.HisMaLanKham); }
			set { SetColumnValue(Columns.HisMaLanKham, value); }
		}
		  
		[XmlAttribute("HisSoBa")]
		[Bindable(true)]
		public string HisSoBa 
		{
			get { return GetColumnValue<string>(Columns.HisSoBa); }
			set { SetColumnValue(Columns.HisSoBa, value); }
		}
		  
		[XmlAttribute("HisMaPhieu")]
		[Bindable(true)]
		public string HisMaPhieu 
		{
			get { return GetColumnValue<string>(Columns.HisMaPhieu); }
			set { SetColumnValue(Columns.HisMaPhieu, value); }
		}
		  
		[XmlAttribute("HisMaBenh")]
		[Bindable(true)]
		public string HisMaBenh 
		{
			get { return GetColumnValue<string>(Columns.HisMaBenh); }
			set { SetColumnValue(Columns.HisMaBenh, value); }
		}
		  
		[XmlAttribute("HisChanDoan")]
		[Bindable(true)]
		public string HisChanDoan 
		{
			get { return GetColumnValue<string>(Columns.HisChanDoan); }
			set { SetColumnValue(Columns.HisChanDoan, value); }
		}
		  
		[XmlAttribute("HisMaDtuong")]
		[Bindable(true)]
		public string HisMaDtuong 
		{
			get { return GetColumnValue<string>(Columns.HisMaDtuong); }
			set { SetColumnValue(Columns.HisMaDtuong, value); }
		}
		  
		[XmlAttribute("HisSoBhyt")]
		[Bindable(true)]
		public string HisSoBhyt 
		{
			get { return GetColumnValue<string>(Columns.HisSoBhyt); }
			set { SetColumnValue(Columns.HisSoBhyt, value); }
		}
		  
		[XmlAttribute("HisDiaChi")]
		[Bindable(true)]
		public string HisDiaChi 
		{
			get { return GetColumnValue<string>(Columns.HisDiaChi); }
			set { SetColumnValue(Columns.HisDiaChi, value); }
		}
		  
		[XmlAttribute("HisGtinh")]
		[Bindable(true)]
		public string HisGtinh 
		{
			get { return GetColumnValue<string>(Columns.HisGtinh); }
			set { SetColumnValue(Columns.HisGtinh, value); }
		}
		  
		[XmlAttribute("HisNamSinh")]
		[Bindable(true)]
		public int? HisNamSinh 
		{
			get { return GetColumnValue<int?>(Columns.HisNamSinh); }
			set { SetColumnValue(Columns.HisNamSinh, value); }
		}
		  
		[XmlAttribute("HisIdKhoa")]
		[Bindable(true)]
		public int? HisIdKhoa 
		{
			get { return GetColumnValue<int?>(Columns.HisIdKhoa); }
			set { SetColumnValue(Columns.HisIdKhoa, value); }
		}
		  
		[XmlAttribute("HisTenKhoa")]
		[Bindable(true)]
		public string HisTenKhoa 
		{
			get { return GetColumnValue<string>(Columns.HisTenKhoa); }
			set { SetColumnValue(Columns.HisTenKhoa, value); }
		}
		  
		[XmlAttribute("HisIdPhong")]
		[Bindable(true)]
		public int? HisIdPhong 
		{
			get { return GetColumnValue<int?>(Columns.HisIdPhong); }
			set { SetColumnValue(Columns.HisIdPhong, value); }
		}
		  
		[XmlAttribute("HisTenPhong")]
		[Bindable(true)]
		public string HisTenPhong 
		{
			get { return GetColumnValue<string>(Columns.HisTenPhong); }
			set { SetColumnValue(Columns.HisTenPhong, value); }
		}
		  
		[XmlAttribute("HisIdGiuong")]
		[Bindable(true)]
		public int? HisIdGiuong 
		{
			get { return GetColumnValue<int?>(Columns.HisIdGiuong); }
			set { SetColumnValue(Columns.HisIdGiuong, value); }
		}
		  
		[XmlAttribute("HisTenGiuong")]
		[Bindable(true)]
		public string HisTenGiuong 
		{
			get { return GetColumnValue<string>(Columns.HisTenGiuong); }
			set { SetColumnValue(Columns.HisTenGiuong, value); }
		}
		  
		[XmlAttribute("HisTrangThai")]
		[Bindable(true)]
		public int? HisTrangThai 
		{
			get { return GetColumnValue<int?>(Columns.HisTrangThai); }
			set { SetColumnValue(Columns.HisTrangThai, value); }
		}
		  
		[XmlAttribute("HisNgayYeucau")]
		[Bindable(true)]
		public DateTime? HisNgayYeucau 
		{
			get { return GetColumnValue<DateTime?>(Columns.HisNgayYeucau); }
			set { SetColumnValue(Columns.HisNgayYeucau, value); }
		}
		  
		[XmlAttribute("HisNgayTao")]
		[Bindable(true)]
		public DateTime? HisNgayTao 
		{
			get { return GetColumnValue<DateTime?>(Columns.HisNgayTao); }
			set { SetColumnValue(Columns.HisNgayTao, value); }
		}
		  
		[XmlAttribute("HisNguoiTao")]
		[Bindable(true)]
		public string HisNguoiTao 
		{
			get { return GetColumnValue<string>(Columns.HisNguoiTao); }
			set { SetColumnValue(Columns.HisNguoiTao, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varHisIdBnhan,string varHisMaLanKham,string varHisSoBa,string varHisMaPhieu,string varHisMaBenh,string varHisChanDoan,string varHisMaDtuong,string varHisSoBhyt,string varHisDiaChi,string varHisGtinh,int? varHisNamSinh,int? varHisIdKhoa,string varHisTenKhoa,int? varHisIdPhong,string varHisTenPhong,int? varHisIdGiuong,string varHisTenGiuong,int? varHisTrangThai,DateTime? varHisNgayYeucau,DateTime? varHisNgayTao,string varHisNguoiTao)
		{
			KetnoiBenhNhan item = new KetnoiBenhNhan();
			
			item.HisIdBnhan = varHisIdBnhan;
			
			item.HisMaLanKham = varHisMaLanKham;
			
			item.HisSoBa = varHisSoBa;
			
			item.HisMaPhieu = varHisMaPhieu;
			
			item.HisMaBenh = varHisMaBenh;
			
			item.HisChanDoan = varHisChanDoan;
			
			item.HisMaDtuong = varHisMaDtuong;
			
			item.HisSoBhyt = varHisSoBhyt;
			
			item.HisDiaChi = varHisDiaChi;
			
			item.HisGtinh = varHisGtinh;
			
			item.HisNamSinh = varHisNamSinh;
			
			item.HisIdKhoa = varHisIdKhoa;
			
			item.HisTenKhoa = varHisTenKhoa;
			
			item.HisIdPhong = varHisIdPhong;
			
			item.HisTenPhong = varHisTenPhong;
			
			item.HisIdGiuong = varHisIdGiuong;
			
			item.HisTenGiuong = varHisTenGiuong;
			
			item.HisTrangThai = varHisTrangThai;
			
			item.HisNgayYeucau = varHisNgayYeucau;
			
			item.HisNgayTao = varHisNgayTao;
			
			item.HisNguoiTao = varHisNguoiTao;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varHisIdBnhan,string varHisMaLanKham,string varHisSoBa,string varHisMaPhieu,string varHisMaBenh,string varHisChanDoan,string varHisMaDtuong,string varHisSoBhyt,string varHisDiaChi,string varHisGtinh,int? varHisNamSinh,int? varHisIdKhoa,string varHisTenKhoa,int? varHisIdPhong,string varHisTenPhong,int? varHisIdGiuong,string varHisTenGiuong,int? varHisTrangThai,DateTime? varHisNgayYeucau,DateTime? varHisNgayTao,string varHisNguoiTao)
		{
			KetnoiBenhNhan item = new KetnoiBenhNhan();
			
				item.HisIdBnhan = varHisIdBnhan;
			
				item.HisMaLanKham = varHisMaLanKham;
			
				item.HisSoBa = varHisSoBa;
			
				item.HisMaPhieu = varHisMaPhieu;
			
				item.HisMaBenh = varHisMaBenh;
			
				item.HisChanDoan = varHisChanDoan;
			
				item.HisMaDtuong = varHisMaDtuong;
			
				item.HisSoBhyt = varHisSoBhyt;
			
				item.HisDiaChi = varHisDiaChi;
			
				item.HisGtinh = varHisGtinh;
			
				item.HisNamSinh = varHisNamSinh;
			
				item.HisIdKhoa = varHisIdKhoa;
			
				item.HisTenKhoa = varHisTenKhoa;
			
				item.HisIdPhong = varHisIdPhong;
			
				item.HisTenPhong = varHisTenPhong;
			
				item.HisIdGiuong = varHisIdGiuong;
			
				item.HisTenGiuong = varHisTenGiuong;
			
				item.HisTrangThai = varHisTrangThai;
			
				item.HisNgayYeucau = varHisNgayYeucau;
			
				item.HisNgayTao = varHisNgayTao;
			
				item.HisNguoiTao = varHisNguoiTao;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn HisIdBnhanColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn HisMaLanKhamColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn HisSoBaColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn HisMaPhieuColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn HisMaBenhColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn HisChanDoanColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn HisMaDtuongColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn HisSoBhytColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn HisDiaChiColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn HisGtinhColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn HisNamSinhColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn HisIdKhoaColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn HisTenKhoaColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn HisIdPhongColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn HisTenPhongColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn HisIdGiuongColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn HisTenGiuongColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn HisTrangThaiColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn HisNgayYeucauColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn HisNgayTaoColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn HisNguoiTaoColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string HisIdBnhan = @"HIS_ID_BNHAN";
			 public static string HisMaLanKham = @"HIS_MA_LAN_KHAM";
			 public static string HisSoBa = @"HIS_SO_BA";
			 public static string HisMaPhieu = @"HIS_MA_PHIEU";
			 public static string HisMaBenh = @"HIS_MA_BENH";
			 public static string HisChanDoan = @"HIS_CHAN_DOAN";
			 public static string HisMaDtuong = @"HIS_MA_DTUONG";
			 public static string HisSoBhyt = @"HIS_SO_BHYT";
			 public static string HisDiaChi = @"HIS_DIA_CHI";
			 public static string HisGtinh = @"HIS_GTINH";
			 public static string HisNamSinh = @"HIS_NAM_SINH";
			 public static string HisIdKhoa = @"HIS_ID_KHOA";
			 public static string HisTenKhoa = @"HIS_TEN_KHOA";
			 public static string HisIdPhong = @"HIS_ID_PHONG";
			 public static string HisTenPhong = @"HIS_TEN_PHONG";
			 public static string HisIdGiuong = @"HIS_ID_GIUONG";
			 public static string HisTenGiuong = @"HIS_TEN_GIUONG";
			 public static string HisTrangThai = @"HIS_TRANG_THAI";
			 public static string HisNgayYeucau = @"HIS_NGAY_YEUCAU";
			 public static string HisNgayTao = @"HIS_NGAY_TAO";
			 public static string HisNguoiTao = @"HIS_NGUOI_TAO";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
