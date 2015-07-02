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
	/// Strongly-typed collection for the KetnoiPhieuChidinh class.
	/// </summary>
    [Serializable]
	public partial class KetnoiPhieuChidinhCollection : ActiveList<KetnoiPhieuChidinh, KetnoiPhieuChidinhCollection>
	{	   
		public KetnoiPhieuChidinhCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>KetnoiPhieuChidinhCollection</returns>
		public KetnoiPhieuChidinhCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                KetnoiPhieuChidinh o = this[i];
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
	/// This is an ActiveRecord class which wraps the KETNOI_PHIEU_CHIDINH table.
	/// </summary>
	[Serializable]
	public partial class KetnoiPhieuChidinh : ActiveRecord<KetnoiPhieuChidinh>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public KetnoiPhieuChidinh()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public KetnoiPhieuChidinh(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public KetnoiPhieuChidinh(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public KetnoiPhieuChidinh(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("KETNOI_PHIEU_CHIDINH", TableType.Table, DataService.GetInstance("ORM"));
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
				
				TableSchema.TableColumn colvarHisIdBnhan = new TableSchema.TableColumn(schema);
				colvarHisIdBnhan.ColumnName = "HIS_ID_BNHAN";
				colvarHisIdBnhan.DataType = DbType.Int32;
				colvarHisIdBnhan.MaxLength = 0;
				colvarHisIdBnhan.AutoIncrement = false;
				colvarHisIdBnhan.IsNullable = true;
				colvarHisIdBnhan.IsPrimaryKey = false;
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
				colvarHisMaLanKham.IsNullable = true;
				colvarHisMaLanKham.IsPrimaryKey = false;
				colvarHisMaLanKham.IsForeignKey = false;
				colvarHisMaLanKham.IsReadOnly = false;
				colvarHisMaLanKham.DefaultSetting = @"";
				colvarHisMaLanKham.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisMaLanKham);
				
				TableSchema.TableColumn colvarHisMaPhieu = new TableSchema.TableColumn(schema);
				colvarHisMaPhieu.ColumnName = "HIS_MA_PHIEU";
				colvarHisMaPhieu.DataType = DbType.String;
				colvarHisMaPhieu.MaxLength = 50;
				colvarHisMaPhieu.AutoIncrement = false;
				colvarHisMaPhieu.IsNullable = true;
				colvarHisMaPhieu.IsPrimaryKey = false;
				colvarHisMaPhieu.IsForeignKey = false;
				colvarHisMaPhieu.IsReadOnly = false;
				colvarHisMaPhieu.DefaultSetting = @"";
				colvarHisMaPhieu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisMaPhieu);
				
				TableSchema.TableColumn colvarHisIdPhieu = new TableSchema.TableColumn(schema);
				colvarHisIdPhieu.ColumnName = "HIS_ID_PHIEU";
				colvarHisIdPhieu.DataType = DbType.Int32;
				colvarHisIdPhieu.MaxLength = 0;
				colvarHisIdPhieu.AutoIncrement = false;
				colvarHisIdPhieu.IsNullable = true;
				colvarHisIdPhieu.IsPrimaryKey = false;
				colvarHisIdPhieu.IsForeignKey = false;
				colvarHisIdPhieu.IsReadOnly = false;
				colvarHisIdPhieu.DefaultSetting = @"";
				colvarHisIdPhieu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisIdPhieu);
				
				TableSchema.TableColumn colvarHisIdChitietPhieu = new TableSchema.TableColumn(schema);
				colvarHisIdChitietPhieu.ColumnName = "HIS_ID_CHITIET_PHIEU";
				colvarHisIdChitietPhieu.DataType = DbType.Int32;
				colvarHisIdChitietPhieu.MaxLength = 0;
				colvarHisIdChitietPhieu.AutoIncrement = false;
				colvarHisIdChitietPhieu.IsNullable = true;
				colvarHisIdChitietPhieu.IsPrimaryKey = false;
				colvarHisIdChitietPhieu.IsForeignKey = false;
				colvarHisIdChitietPhieu.IsReadOnly = false;
				colvarHisIdChitietPhieu.DefaultSetting = @"";
				colvarHisIdChitietPhieu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisIdChitietPhieu);
				
				TableSchema.TableColumn colvarHisBsyCd = new TableSchema.TableColumn(schema);
				colvarHisBsyCd.ColumnName = "HIS_BSY_CD";
				colvarHisBsyCd.DataType = DbType.Int32;
				colvarHisBsyCd.MaxLength = 0;
				colvarHisBsyCd.AutoIncrement = false;
				colvarHisBsyCd.IsNullable = true;
				colvarHisBsyCd.IsPrimaryKey = false;
				colvarHisBsyCd.IsForeignKey = false;
				colvarHisBsyCd.IsReadOnly = false;
				colvarHisBsyCd.DefaultSetting = @"";
				colvarHisBsyCd.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisBsyCd);
				
				TableSchema.TableColumn colvarHisTenBsyCd = new TableSchema.TableColumn(schema);
				colvarHisTenBsyCd.ColumnName = "HIS_TEN_BSY_CD";
				colvarHisTenBsyCd.DataType = DbType.String;
				colvarHisTenBsyCd.MaxLength = 50;
				colvarHisTenBsyCd.AutoIncrement = false;
				colvarHisTenBsyCd.IsNullable = true;
				colvarHisTenBsyCd.IsPrimaryKey = false;
				colvarHisTenBsyCd.IsForeignKey = false;
				colvarHisTenBsyCd.IsReadOnly = false;
				colvarHisTenBsyCd.DefaultSetting = @"";
				colvarHisTenBsyCd.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisTenBsyCd);
				
				TableSchema.TableColumn colvarHisIdDvu = new TableSchema.TableColumn(schema);
				colvarHisIdDvu.ColumnName = "HIS_ID_DVU";
				colvarHisIdDvu.DataType = DbType.Int32;
				colvarHisIdDvu.MaxLength = 0;
				colvarHisIdDvu.AutoIncrement = false;
				colvarHisIdDvu.IsNullable = true;
				colvarHisIdDvu.IsPrimaryKey = false;
				colvarHisIdDvu.IsForeignKey = false;
				colvarHisIdDvu.IsReadOnly = false;
				colvarHisIdDvu.DefaultSetting = @"";
				colvarHisIdDvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisIdDvu);
				
				TableSchema.TableColumn colvarHisTenDvu = new TableSchema.TableColumn(schema);
				colvarHisTenDvu.ColumnName = "HIS_TEN_DVU";
				colvarHisTenDvu.DataType = DbType.String;
				colvarHisTenDvu.MaxLength = 50;
				colvarHisTenDvu.AutoIncrement = false;
				colvarHisTenDvu.IsNullable = true;
				colvarHisTenDvu.IsPrimaryKey = false;
				colvarHisTenDvu.IsForeignKey = false;
				colvarHisTenDvu.IsReadOnly = false;
				colvarHisTenDvu.DefaultSetting = @"";
				colvarHisTenDvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisTenDvu);
				
				TableSchema.TableColumn colvarHisIdLoaiDvu = new TableSchema.TableColumn(schema);
				colvarHisIdLoaiDvu.ColumnName = "HIS_ID_LOAI_DVU";
				colvarHisIdLoaiDvu.DataType = DbType.Int32;
				colvarHisIdLoaiDvu.MaxLength = 0;
				colvarHisIdLoaiDvu.AutoIncrement = false;
				colvarHisIdLoaiDvu.IsNullable = true;
				colvarHisIdLoaiDvu.IsPrimaryKey = false;
				colvarHisIdLoaiDvu.IsForeignKey = false;
				colvarHisIdLoaiDvu.IsReadOnly = false;
				colvarHisIdLoaiDvu.DefaultSetting = @"";
				colvarHisIdLoaiDvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisIdLoaiDvu);
				
				TableSchema.TableColumn colvarHisTenLoaiDvu = new TableSchema.TableColumn(schema);
				colvarHisTenLoaiDvu.ColumnName = "HIS_TEN_LOAI_DVU";
				colvarHisTenLoaiDvu.DataType = DbType.String;
				colvarHisTenLoaiDvu.MaxLength = 50;
				colvarHisTenLoaiDvu.AutoIncrement = false;
				colvarHisTenLoaiDvu.IsNullable = true;
				colvarHisTenLoaiDvu.IsPrimaryKey = false;
				colvarHisTenLoaiDvu.IsForeignKey = false;
				colvarHisTenLoaiDvu.IsReadOnly = false;
				colvarHisTenLoaiDvu.DefaultSetting = @"";
				colvarHisTenLoaiDvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisTenLoaiDvu);
				
				TableSchema.TableColumn colvarHisDonGia = new TableSchema.TableColumn(schema);
				colvarHisDonGia.ColumnName = "HIS_DON_GIA";
				colvarHisDonGia.DataType = DbType.Decimal;
				colvarHisDonGia.MaxLength = 0;
				colvarHisDonGia.AutoIncrement = false;
				colvarHisDonGia.IsNullable = true;
				colvarHisDonGia.IsPrimaryKey = false;
				colvarHisDonGia.IsForeignKey = false;
				colvarHisDonGia.IsReadOnly = false;
				colvarHisDonGia.DefaultSetting = @"";
				colvarHisDonGia.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisDonGia);
				
				TableSchema.TableColumn colvarHisSoLuong = new TableSchema.TableColumn(schema);
				colvarHisSoLuong.ColumnName = "HIS_SO_LUONG";
				colvarHisSoLuong.DataType = DbType.Int32;
				colvarHisSoLuong.MaxLength = 0;
				colvarHisSoLuong.AutoIncrement = false;
				colvarHisSoLuong.IsNullable = true;
				colvarHisSoLuong.IsPrimaryKey = false;
				colvarHisSoLuong.IsForeignKey = false;
				colvarHisSoLuong.IsReadOnly = false;
				colvarHisSoLuong.DefaultSetting = @"";
				colvarHisSoLuong.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisSoLuong);
				
				TableSchema.TableColumn colvarHisNgayYcau = new TableSchema.TableColumn(schema);
				colvarHisNgayYcau.ColumnName = "HIS_NGAY_YCAU";
				colvarHisNgayYcau.DataType = DbType.DateTime;
				colvarHisNgayYcau.MaxLength = 0;
				colvarHisNgayYcau.AutoIncrement = false;
				colvarHisNgayYcau.IsNullable = true;
				colvarHisNgayYcau.IsPrimaryKey = false;
				colvarHisNgayYcau.IsForeignKey = false;
				colvarHisNgayYcau.IsReadOnly = false;
				colvarHisNgayYcau.DefaultSetting = @"";
				colvarHisNgayYcau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisNgayYcau);
				
				TableSchema.TableColumn colvarHisNguoiYcau = new TableSchema.TableColumn(schema);
				colvarHisNguoiYcau.ColumnName = "HIS_NGUOI_YCAU";
				colvarHisNguoiYcau.DataType = DbType.String;
				colvarHisNguoiYcau.MaxLength = 50;
				colvarHisNguoiYcau.AutoIncrement = false;
				colvarHisNguoiYcau.IsNullable = true;
				colvarHisNguoiYcau.IsPrimaryKey = false;
				colvarHisNguoiYcau.IsForeignKey = false;
				colvarHisNguoiYcau.IsReadOnly = false;
				colvarHisNguoiYcau.DefaultSetting = @"";
				colvarHisNguoiYcau.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisNguoiYcau);
				
				TableSchema.TableColumn colvarHisMaKieuDvu = new TableSchema.TableColumn(schema);
				colvarHisMaKieuDvu.ColumnName = "HIS_MA_KIEU_DVU";
				colvarHisMaKieuDvu.DataType = DbType.String;
				colvarHisMaKieuDvu.MaxLength = 50;
				colvarHisMaKieuDvu.AutoIncrement = false;
				colvarHisMaKieuDvu.IsNullable = true;
				colvarHisMaKieuDvu.IsPrimaryKey = false;
				colvarHisMaKieuDvu.IsForeignKey = false;
				colvarHisMaKieuDvu.IsReadOnly = false;
				colvarHisMaKieuDvu.DefaultSetting = @"";
				colvarHisMaKieuDvu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHisMaKieuDvu);
				
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
				
				TableSchema.TableColumn colvarRisIdChitietPhieu = new TableSchema.TableColumn(schema);
				colvarRisIdChitietPhieu.ColumnName = "RIS_ID_CHITIET_PHIEU";
				colvarRisIdChitietPhieu.DataType = DbType.Int32;
				colvarRisIdChitietPhieu.MaxLength = 0;
				colvarRisIdChitietPhieu.AutoIncrement = false;
				colvarRisIdChitietPhieu.IsNullable = true;
				colvarRisIdChitietPhieu.IsPrimaryKey = false;
				colvarRisIdChitietPhieu.IsForeignKey = false;
				colvarRisIdChitietPhieu.IsReadOnly = false;
				colvarRisIdChitietPhieu.DefaultSetting = @"";
				colvarRisIdChitietPhieu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRisIdChitietPhieu);
				
				TableSchema.TableColumn colvarRisMaPhieu = new TableSchema.TableColumn(schema);
				colvarRisMaPhieu.ColumnName = "RIS_MA_PHIEU";
				colvarRisMaPhieu.DataType = DbType.String;
				colvarRisMaPhieu.MaxLength = 50;
				colvarRisMaPhieu.AutoIncrement = false;
				colvarRisMaPhieu.IsNullable = true;
				colvarRisMaPhieu.IsPrimaryKey = false;
				colvarRisMaPhieu.IsForeignKey = false;
				colvarRisMaPhieu.IsReadOnly = false;
				colvarRisMaPhieu.DefaultSetting = @"";
				colvarRisMaPhieu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRisMaPhieu);
				
				TableSchema.TableColumn colvarRisIdPhieu = new TableSchema.TableColumn(schema);
				colvarRisIdPhieu.ColumnName = "RIS_ID_PHIEU";
				colvarRisIdPhieu.DataType = DbType.Int32;
				colvarRisIdPhieu.MaxLength = 0;
				colvarRisIdPhieu.AutoIncrement = false;
				colvarRisIdPhieu.IsNullable = true;
				colvarRisIdPhieu.IsPrimaryKey = false;
				colvarRisIdPhieu.IsForeignKey = false;
				colvarRisIdPhieu.IsReadOnly = false;
				colvarRisIdPhieu.DefaultSetting = @"";
				colvarRisIdPhieu.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRisIdPhieu);
				
				TableSchema.TableColumn colvarRisNguoiTao = new TableSchema.TableColumn(schema);
				colvarRisNguoiTao.ColumnName = "RIS_NGUOI_TAO";
				colvarRisNguoiTao.DataType = DbType.String;
				colvarRisNguoiTao.MaxLength = 50;
				colvarRisNguoiTao.AutoIncrement = false;
				colvarRisNguoiTao.IsNullable = true;
				colvarRisNguoiTao.IsPrimaryKey = false;
				colvarRisNguoiTao.IsForeignKey = false;
				colvarRisNguoiTao.IsReadOnly = false;
				colvarRisNguoiTao.DefaultSetting = @"";
				colvarRisNguoiTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRisNguoiTao);
				
				TableSchema.TableColumn colvarRisNgayTao = new TableSchema.TableColumn(schema);
				colvarRisNgayTao.ColumnName = "RIS_NGAY_TAO";
				colvarRisNgayTao.DataType = DbType.DateTime;
				colvarRisNgayTao.MaxLength = 0;
				colvarRisNgayTao.AutoIncrement = false;
				colvarRisNgayTao.IsNullable = true;
				colvarRisNgayTao.IsPrimaryKey = false;
				colvarRisNgayTao.IsForeignKey = false;
				colvarRisNgayTao.IsReadOnly = false;
				colvarRisNgayTao.DefaultSetting = @"";
				colvarRisNgayTao.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRisNgayTao);
				
				TableSchema.TableColumn colvarRisDaTra = new TableSchema.TableColumn(schema);
				colvarRisDaTra.ColumnName = "RIS_DA_TRA";
				colvarRisDaTra.DataType = DbType.Int32;
				colvarRisDaTra.MaxLength = 0;
				colvarRisDaTra.AutoIncrement = false;
				colvarRisDaTra.IsNullable = true;
				colvarRisDaTra.IsPrimaryKey = false;
				colvarRisDaTra.IsForeignKey = false;
				colvarRisDaTra.IsReadOnly = false;
				colvarRisDaTra.DefaultSetting = @"";
				colvarRisDaTra.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRisDaTra);
				
				TableSchema.TableColumn colvarRisNgaySua = new TableSchema.TableColumn(schema);
				colvarRisNgaySua.ColumnName = "RIS_NGAY_SUA";
				colvarRisNgaySua.DataType = DbType.DateTime;
				colvarRisNgaySua.MaxLength = 0;
				colvarRisNgaySua.AutoIncrement = false;
				colvarRisNgaySua.IsNullable = true;
				colvarRisNgaySua.IsPrimaryKey = false;
				colvarRisNgaySua.IsForeignKey = false;
				colvarRisNgaySua.IsReadOnly = false;
				colvarRisNgaySua.DefaultSetting = @"";
				colvarRisNgaySua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRisNgaySua);
				
				TableSchema.TableColumn colvarRisNguoiSua = new TableSchema.TableColumn(schema);
				colvarRisNguoiSua.ColumnName = "RIS_NGUOI_SUA";
				colvarRisNguoiSua.DataType = DbType.String;
				colvarRisNguoiSua.MaxLength = 50;
				colvarRisNguoiSua.AutoIncrement = false;
				colvarRisNguoiSua.IsNullable = true;
				colvarRisNguoiSua.IsPrimaryKey = false;
				colvarRisNguoiSua.IsForeignKey = false;
				colvarRisNguoiSua.IsReadOnly = false;
				colvarRisNguoiSua.DefaultSetting = @"";
				colvarRisNguoiSua.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRisNguoiSua);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("KETNOI_PHIEU_CHIDINH",schema);
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
		  
		[XmlAttribute("HisIdBnhan")]
		[Bindable(true)]
		public int? HisIdBnhan 
		{
			get { return GetColumnValue<int?>(Columns.HisIdBnhan); }
			set { SetColumnValue(Columns.HisIdBnhan, value); }
		}
		  
		[XmlAttribute("HisMaLanKham")]
		[Bindable(true)]
		public string HisMaLanKham 
		{
			get { return GetColumnValue<string>(Columns.HisMaLanKham); }
			set { SetColumnValue(Columns.HisMaLanKham, value); }
		}
		  
		[XmlAttribute("HisMaPhieu")]
		[Bindable(true)]
		public string HisMaPhieu 
		{
			get { return GetColumnValue<string>(Columns.HisMaPhieu); }
			set { SetColumnValue(Columns.HisMaPhieu, value); }
		}
		  
		[XmlAttribute("HisIdPhieu")]
		[Bindable(true)]
		public int? HisIdPhieu 
		{
			get { return GetColumnValue<int?>(Columns.HisIdPhieu); }
			set { SetColumnValue(Columns.HisIdPhieu, value); }
		}
		  
		[XmlAttribute("HisIdChitietPhieu")]
		[Bindable(true)]
		public int? HisIdChitietPhieu 
		{
			get { return GetColumnValue<int?>(Columns.HisIdChitietPhieu); }
			set { SetColumnValue(Columns.HisIdChitietPhieu, value); }
		}
		  
		[XmlAttribute("HisBsyCd")]
		[Bindable(true)]
		public int? HisBsyCd 
		{
			get { return GetColumnValue<int?>(Columns.HisBsyCd); }
			set { SetColumnValue(Columns.HisBsyCd, value); }
		}
		  
		[XmlAttribute("HisTenBsyCd")]
		[Bindable(true)]
		public string HisTenBsyCd 
		{
			get { return GetColumnValue<string>(Columns.HisTenBsyCd); }
			set { SetColumnValue(Columns.HisTenBsyCd, value); }
		}
		  
		[XmlAttribute("HisIdDvu")]
		[Bindable(true)]
		public int? HisIdDvu 
		{
			get { return GetColumnValue<int?>(Columns.HisIdDvu); }
			set { SetColumnValue(Columns.HisIdDvu, value); }
		}
		  
		[XmlAttribute("HisTenDvu")]
		[Bindable(true)]
		public string HisTenDvu 
		{
			get { return GetColumnValue<string>(Columns.HisTenDvu); }
			set { SetColumnValue(Columns.HisTenDvu, value); }
		}
		  
		[XmlAttribute("HisIdLoaiDvu")]
		[Bindable(true)]
		public int? HisIdLoaiDvu 
		{
			get { return GetColumnValue<int?>(Columns.HisIdLoaiDvu); }
			set { SetColumnValue(Columns.HisIdLoaiDvu, value); }
		}
		  
		[XmlAttribute("HisTenLoaiDvu")]
		[Bindable(true)]
		public string HisTenLoaiDvu 
		{
			get { return GetColumnValue<string>(Columns.HisTenLoaiDvu); }
			set { SetColumnValue(Columns.HisTenLoaiDvu, value); }
		}
		  
		[XmlAttribute("HisDonGia")]
		[Bindable(true)]
		public decimal? HisDonGia 
		{
			get { return GetColumnValue<decimal?>(Columns.HisDonGia); }
			set { SetColumnValue(Columns.HisDonGia, value); }
		}
		  
		[XmlAttribute("HisSoLuong")]
		[Bindable(true)]
		public int? HisSoLuong 
		{
			get { return GetColumnValue<int?>(Columns.HisSoLuong); }
			set { SetColumnValue(Columns.HisSoLuong, value); }
		}
		  
		[XmlAttribute("HisNgayYcau")]
		[Bindable(true)]
		public DateTime? HisNgayYcau 
		{
			get { return GetColumnValue<DateTime?>(Columns.HisNgayYcau); }
			set { SetColumnValue(Columns.HisNgayYcau, value); }
		}
		  
		[XmlAttribute("HisNguoiYcau")]
		[Bindable(true)]
		public string HisNguoiYcau 
		{
			get { return GetColumnValue<string>(Columns.HisNguoiYcau); }
			set { SetColumnValue(Columns.HisNguoiYcau, value); }
		}
		  
		[XmlAttribute("HisMaKieuDvu")]
		[Bindable(true)]
		public string HisMaKieuDvu 
		{
			get { return GetColumnValue<string>(Columns.HisMaKieuDvu); }
			set { SetColumnValue(Columns.HisMaKieuDvu, value); }
		}
		  
		[XmlAttribute("HisTenKhoa")]
		[Bindable(true)]
		public string HisTenKhoa 
		{
			get { return GetColumnValue<string>(Columns.HisTenKhoa); }
			set { SetColumnValue(Columns.HisTenKhoa, value); }
		}
		  
		[XmlAttribute("HisIdKhoa")]
		[Bindable(true)]
		public int? HisIdKhoa 
		{
			get { return GetColumnValue<int?>(Columns.HisIdKhoa); }
			set { SetColumnValue(Columns.HisIdKhoa, value); }
		}
		  
		[XmlAttribute("RisIdChitietPhieu")]
		[Bindable(true)]
		public int? RisIdChitietPhieu 
		{
			get { return GetColumnValue<int?>(Columns.RisIdChitietPhieu); }
			set { SetColumnValue(Columns.RisIdChitietPhieu, value); }
		}
		  
		[XmlAttribute("RisMaPhieu")]
		[Bindable(true)]
		public string RisMaPhieu 
		{
			get { return GetColumnValue<string>(Columns.RisMaPhieu); }
			set { SetColumnValue(Columns.RisMaPhieu, value); }
		}
		  
		[XmlAttribute("RisIdPhieu")]
		[Bindable(true)]
		public int? RisIdPhieu 
		{
			get { return GetColumnValue<int?>(Columns.RisIdPhieu); }
			set { SetColumnValue(Columns.RisIdPhieu, value); }
		}
		  
		[XmlAttribute("RisNguoiTao")]
		[Bindable(true)]
		public string RisNguoiTao 
		{
			get { return GetColumnValue<string>(Columns.RisNguoiTao); }
			set { SetColumnValue(Columns.RisNguoiTao, value); }
		}
		  
		[XmlAttribute("RisNgayTao")]
		[Bindable(true)]
		public DateTime? RisNgayTao 
		{
			get { return GetColumnValue<DateTime?>(Columns.RisNgayTao); }
			set { SetColumnValue(Columns.RisNgayTao, value); }
		}
		  
		[XmlAttribute("RisDaTra")]
		[Bindable(true)]
		public int? RisDaTra 
		{
			get { return GetColumnValue<int?>(Columns.RisDaTra); }
			set { SetColumnValue(Columns.RisDaTra, value); }
		}
		  
		[XmlAttribute("RisNgaySua")]
		[Bindable(true)]
		public DateTime? RisNgaySua 
		{
			get { return GetColumnValue<DateTime?>(Columns.RisNgaySua); }
			set { SetColumnValue(Columns.RisNgaySua, value); }
		}
		  
		[XmlAttribute("RisNguoiSua")]
		[Bindable(true)]
		public string RisNguoiSua 
		{
			get { return GetColumnValue<string>(Columns.RisNguoiSua); }
			set { SetColumnValue(Columns.RisNguoiSua, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int? varHisIdBnhan,string varHisMaLanKham,string varHisMaPhieu,int? varHisIdPhieu,int? varHisIdChitietPhieu,int? varHisBsyCd,string varHisTenBsyCd,int? varHisIdDvu,string varHisTenDvu,int? varHisIdLoaiDvu,string varHisTenLoaiDvu,decimal? varHisDonGia,int? varHisSoLuong,DateTime? varHisNgayYcau,string varHisNguoiYcau,string varHisMaKieuDvu,string varHisTenKhoa,int? varHisIdKhoa,int? varRisIdChitietPhieu,string varRisMaPhieu,int? varRisIdPhieu,string varRisNguoiTao,DateTime? varRisNgayTao,int? varRisDaTra,DateTime? varRisNgaySua,string varRisNguoiSua)
		{
			KetnoiPhieuChidinh item = new KetnoiPhieuChidinh();
			
			item.HisIdBnhan = varHisIdBnhan;
			
			item.HisMaLanKham = varHisMaLanKham;
			
			item.HisMaPhieu = varHisMaPhieu;
			
			item.HisIdPhieu = varHisIdPhieu;
			
			item.HisIdChitietPhieu = varHisIdChitietPhieu;
			
			item.HisBsyCd = varHisBsyCd;
			
			item.HisTenBsyCd = varHisTenBsyCd;
			
			item.HisIdDvu = varHisIdDvu;
			
			item.HisTenDvu = varHisTenDvu;
			
			item.HisIdLoaiDvu = varHisIdLoaiDvu;
			
			item.HisTenLoaiDvu = varHisTenLoaiDvu;
			
			item.HisDonGia = varHisDonGia;
			
			item.HisSoLuong = varHisSoLuong;
			
			item.HisNgayYcau = varHisNgayYcau;
			
			item.HisNguoiYcau = varHisNguoiYcau;
			
			item.HisMaKieuDvu = varHisMaKieuDvu;
			
			item.HisTenKhoa = varHisTenKhoa;
			
			item.HisIdKhoa = varHisIdKhoa;
			
			item.RisIdChitietPhieu = varRisIdChitietPhieu;
			
			item.RisMaPhieu = varRisMaPhieu;
			
			item.RisIdPhieu = varRisIdPhieu;
			
			item.RisNguoiTao = varRisNguoiTao;
			
			item.RisNgayTao = varRisNgayTao;
			
			item.RisDaTra = varRisDaTra;
			
			item.RisNgaySua = varRisNgaySua;
			
			item.RisNguoiSua = varRisNguoiSua;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,int? varHisIdBnhan,string varHisMaLanKham,string varHisMaPhieu,int? varHisIdPhieu,int? varHisIdChitietPhieu,int? varHisBsyCd,string varHisTenBsyCd,int? varHisIdDvu,string varHisTenDvu,int? varHisIdLoaiDvu,string varHisTenLoaiDvu,decimal? varHisDonGia,int? varHisSoLuong,DateTime? varHisNgayYcau,string varHisNguoiYcau,string varHisMaKieuDvu,string varHisTenKhoa,int? varHisIdKhoa,int? varRisIdChitietPhieu,string varRisMaPhieu,int? varRisIdPhieu,string varRisNguoiTao,DateTime? varRisNgayTao,int? varRisDaTra,DateTime? varRisNgaySua,string varRisNguoiSua)
		{
			KetnoiPhieuChidinh item = new KetnoiPhieuChidinh();
			
				item.Id = varId;
			
				item.HisIdBnhan = varHisIdBnhan;
			
				item.HisMaLanKham = varHisMaLanKham;
			
				item.HisMaPhieu = varHisMaPhieu;
			
				item.HisIdPhieu = varHisIdPhieu;
			
				item.HisIdChitietPhieu = varHisIdChitietPhieu;
			
				item.HisBsyCd = varHisBsyCd;
			
				item.HisTenBsyCd = varHisTenBsyCd;
			
				item.HisIdDvu = varHisIdDvu;
			
				item.HisTenDvu = varHisTenDvu;
			
				item.HisIdLoaiDvu = varHisIdLoaiDvu;
			
				item.HisTenLoaiDvu = varHisTenLoaiDvu;
			
				item.HisDonGia = varHisDonGia;
			
				item.HisSoLuong = varHisSoLuong;
			
				item.HisNgayYcau = varHisNgayYcau;
			
				item.HisNguoiYcau = varHisNguoiYcau;
			
				item.HisMaKieuDvu = varHisMaKieuDvu;
			
				item.HisTenKhoa = varHisTenKhoa;
			
				item.HisIdKhoa = varHisIdKhoa;
			
				item.RisIdChitietPhieu = varRisIdChitietPhieu;
			
				item.RisMaPhieu = varRisMaPhieu;
			
				item.RisIdPhieu = varRisIdPhieu;
			
				item.RisNguoiTao = varRisNguoiTao;
			
				item.RisNgayTao = varRisNgayTao;
			
				item.RisDaTra = varRisDaTra;
			
				item.RisNgaySua = varRisNgaySua;
			
				item.RisNguoiSua = varRisNguoiSua;
			
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
        
        
        
        public static TableSchema.TableColumn HisIdBnhanColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn HisMaLanKhamColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn HisMaPhieuColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn HisIdPhieuColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn HisIdChitietPhieuColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn HisBsyCdColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn HisTenBsyCdColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn HisIdDvuColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn HisTenDvuColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn HisIdLoaiDvuColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn HisTenLoaiDvuColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn HisDonGiaColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn HisSoLuongColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn HisNgayYcauColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn HisNguoiYcauColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn HisMaKieuDvuColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn HisTenKhoaColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn HisIdKhoaColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn RisIdChitietPhieuColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn RisMaPhieuColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn RisIdPhieuColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        public static TableSchema.TableColumn RisNguoiTaoColumn
        {
            get { return Schema.Columns[22]; }
        }
        
        
        
        public static TableSchema.TableColumn RisNgayTaoColumn
        {
            get { return Schema.Columns[23]; }
        }
        
        
        
        public static TableSchema.TableColumn RisDaTraColumn
        {
            get { return Schema.Columns[24]; }
        }
        
        
        
        public static TableSchema.TableColumn RisNgaySuaColumn
        {
            get { return Schema.Columns[25]; }
        }
        
        
        
        public static TableSchema.TableColumn RisNguoiSuaColumn
        {
            get { return Schema.Columns[26]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"ID";
			 public static string HisIdBnhan = @"HIS_ID_BNHAN";
			 public static string HisMaLanKham = @"HIS_MA_LAN_KHAM";
			 public static string HisMaPhieu = @"HIS_MA_PHIEU";
			 public static string HisIdPhieu = @"HIS_ID_PHIEU";
			 public static string HisIdChitietPhieu = @"HIS_ID_CHITIET_PHIEU";
			 public static string HisBsyCd = @"HIS_BSY_CD";
			 public static string HisTenBsyCd = @"HIS_TEN_BSY_CD";
			 public static string HisIdDvu = @"HIS_ID_DVU";
			 public static string HisTenDvu = @"HIS_TEN_DVU";
			 public static string HisIdLoaiDvu = @"HIS_ID_LOAI_DVU";
			 public static string HisTenLoaiDvu = @"HIS_TEN_LOAI_DVU";
			 public static string HisDonGia = @"HIS_DON_GIA";
			 public static string HisSoLuong = @"HIS_SO_LUONG";
			 public static string HisNgayYcau = @"HIS_NGAY_YCAU";
			 public static string HisNguoiYcau = @"HIS_NGUOI_YCAU";
			 public static string HisMaKieuDvu = @"HIS_MA_KIEU_DVU";
			 public static string HisTenKhoa = @"HIS_TEN_KHOA";
			 public static string HisIdKhoa = @"HIS_ID_KHOA";
			 public static string RisIdChitietPhieu = @"RIS_ID_CHITIET_PHIEU";
			 public static string RisMaPhieu = @"RIS_MA_PHIEU";
			 public static string RisIdPhieu = @"RIS_ID_PHIEU";
			 public static string RisNguoiTao = @"RIS_NGUOI_TAO";
			 public static string RisNgayTao = @"RIS_NGAY_TAO";
			 public static string RisDaTra = @"RIS_DA_TRA";
			 public static string RisNgaySua = @"RIS_NGAY_SUA";
			 public static string RisNguoiSua = @"RIS_NGUOI_SUA";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}