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
	/// Strongly-typed collection for the SysManagementUnit class.
	/// </summary>
    [Serializable]
	public partial class SysManagementUnitCollection : ActiveList<SysManagementUnit, SysManagementUnitCollection>
	{	   
		public SysManagementUnitCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysManagementUnitCollection</returns>
		public SysManagementUnitCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysManagementUnit o = this[i];
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
	/// This is an ActiveRecord class which wraps the Sys_ManagementUnit table.
	/// </summary>
	[Serializable]
	public partial class SysManagementUnit : ActiveRecord<SysManagementUnit>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public SysManagementUnit()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysManagementUnit(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysManagementUnit(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysManagementUnit(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("Sys_ManagementUnit", TableType.Table, DataService.GetInstance("ORM"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarPkSBranchID = new TableSchema.TableColumn(schema);
				colvarPkSBranchID.ColumnName = "PK_sBranchID";
				colvarPkSBranchID.DataType = DbType.String;
				colvarPkSBranchID.MaxLength = 10;
				colvarPkSBranchID.AutoIncrement = false;
				colvarPkSBranchID.IsNullable = false;
				colvarPkSBranchID.IsPrimaryKey = true;
				colvarPkSBranchID.IsForeignKey = false;
				colvarPkSBranchID.IsReadOnly = false;
				colvarPkSBranchID.DefaultSetting = @"";
				colvarPkSBranchID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPkSBranchID);
				
				TableSchema.TableColumn colvarSName = new TableSchema.TableColumn(schema);
				colvarSName.ColumnName = "sName";
				colvarSName.DataType = DbType.String;
				colvarSName.MaxLength = 100;
				colvarSName.AutoIncrement = false;
				colvarSName.IsNullable = false;
				colvarSName.IsPrimaryKey = false;
				colvarSName.IsForeignKey = false;
				colvarSName.IsReadOnly = false;
				colvarSName.DefaultSetting = @"";
				colvarSName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSName);
				
				TableSchema.TableColumn colvarSParentBranchName = new TableSchema.TableColumn(schema);
				colvarSParentBranchName.ColumnName = "sParentBranchName";
				colvarSParentBranchName.DataType = DbType.String;
				colvarSParentBranchName.MaxLength = 100;
				colvarSParentBranchName.AutoIncrement = false;
				colvarSParentBranchName.IsNullable = true;
				colvarSParentBranchName.IsPrimaryKey = false;
				colvarSParentBranchName.IsForeignKey = false;
				colvarSParentBranchName.IsReadOnly = false;
				colvarSParentBranchName.DefaultSetting = @"";
				colvarSParentBranchName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSParentBranchName);
				
				TableSchema.TableColumn colvarFpSUpBranchID = new TableSchema.TableColumn(schema);
				colvarFpSUpBranchID.ColumnName = "FP_sUpBranchID";
				colvarFpSUpBranchID.DataType = DbType.String;
				colvarFpSUpBranchID.MaxLength = 10;
				colvarFpSUpBranchID.AutoIncrement = false;
				colvarFpSUpBranchID.IsNullable = true;
				colvarFpSUpBranchID.IsPrimaryKey = false;
				colvarFpSUpBranchID.IsForeignKey = false;
				colvarFpSUpBranchID.IsReadOnly = false;
				colvarFpSUpBranchID.DefaultSetting = @"";
				colvarFpSUpBranchID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFpSUpBranchID);
				
				TableSchema.TableColumn colvarIntLevel = new TableSchema.TableColumn(schema);
				colvarIntLevel.ColumnName = "intLevel";
				colvarIntLevel.DataType = DbType.Int32;
				colvarIntLevel.MaxLength = 0;
				colvarIntLevel.AutoIncrement = false;
				colvarIntLevel.IsNullable = true;
				colvarIntLevel.IsPrimaryKey = false;
				colvarIntLevel.IsForeignKey = false;
				colvarIntLevel.IsReadOnly = false;
				colvarIntLevel.DefaultSetting = @"";
				colvarIntLevel.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIntLevel);
				
				TableSchema.TableColumn colvarSAddress = new TableSchema.TableColumn(schema);
				colvarSAddress.ColumnName = "sAddress";
				colvarSAddress.DataType = DbType.String;
				colvarSAddress.MaxLength = 100;
				colvarSAddress.AutoIncrement = false;
				colvarSAddress.IsNullable = true;
				colvarSAddress.IsPrimaryKey = false;
				colvarSAddress.IsForeignKey = false;
				colvarSAddress.IsReadOnly = false;
				colvarSAddress.DefaultSetting = @"";
				colvarSAddress.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSAddress);
				
				TableSchema.TableColumn colvarSPhone = new TableSchema.TableColumn(schema);
				colvarSPhone.ColumnName = "sPhone";
				colvarSPhone.DataType = DbType.String;
				colvarSPhone.MaxLength = 20;
				colvarSPhone.AutoIncrement = false;
				colvarSPhone.IsNullable = true;
				colvarSPhone.IsPrimaryKey = false;
				colvarSPhone.IsForeignKey = false;
				colvarSPhone.IsReadOnly = false;
				colvarSPhone.DefaultSetting = @"";
				colvarSPhone.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSPhone);
				
				TableSchema.TableColumn colvarSBussinessPhone = new TableSchema.TableColumn(schema);
				colvarSBussinessPhone.ColumnName = "sBussinessPhone";
				colvarSBussinessPhone.DataType = DbType.String;
				colvarSBussinessPhone.MaxLength = 20;
				colvarSBussinessPhone.AutoIncrement = false;
				colvarSBussinessPhone.IsNullable = true;
				colvarSBussinessPhone.IsPrimaryKey = false;
				colvarSBussinessPhone.IsForeignKey = false;
				colvarSBussinessPhone.IsReadOnly = false;
				colvarSBussinessPhone.DefaultSetting = @"";
				colvarSBussinessPhone.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSBussinessPhone);
				
				TableSchema.TableColumn colvarSHotPhone = new TableSchema.TableColumn(schema);
				colvarSHotPhone.ColumnName = "sHotPhone";
				colvarSHotPhone.DataType = DbType.String;
				colvarSHotPhone.MaxLength = 20;
				colvarSHotPhone.AutoIncrement = false;
				colvarSHotPhone.IsNullable = true;
				colvarSHotPhone.IsPrimaryKey = false;
				colvarSHotPhone.IsForeignKey = false;
				colvarSHotPhone.IsReadOnly = false;
				colvarSHotPhone.DefaultSetting = @"";
				colvarSHotPhone.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSHotPhone);
				
				TableSchema.TableColumn colvarSDutyPhone = new TableSchema.TableColumn(schema);
				colvarSDutyPhone.ColumnName = "sDutyPhone";
				colvarSDutyPhone.DataType = DbType.String;
				colvarSDutyPhone.MaxLength = 20;
				colvarSDutyPhone.AutoIncrement = false;
				colvarSDutyPhone.IsNullable = true;
				colvarSDutyPhone.IsPrimaryKey = false;
				colvarSDutyPhone.IsForeignKey = false;
				colvarSDutyPhone.IsReadOnly = false;
				colvarSDutyPhone.DefaultSetting = @"";
				colvarSDutyPhone.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSDutyPhone);
				
				TableSchema.TableColumn colvarSFAX = new TableSchema.TableColumn(schema);
				colvarSFAX.ColumnName = "sFAX";
				colvarSFAX.DataType = DbType.String;
				colvarSFAX.MaxLength = 13;
				colvarSFAX.AutoIncrement = false;
				colvarSFAX.IsNullable = true;
				colvarSFAX.IsPrimaryKey = false;
				colvarSFAX.IsForeignKey = false;
				colvarSFAX.IsReadOnly = false;
				colvarSFAX.DefaultSetting = @"";
				colvarSFAX.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSFAX);
				
				TableSchema.TableColumn colvarSEMAIL = new TableSchema.TableColumn(schema);
				colvarSEMAIL.ColumnName = "sEMAIL";
				colvarSEMAIL.DataType = DbType.String;
				colvarSEMAIL.MaxLength = 30;
				colvarSEMAIL.AutoIncrement = false;
				colvarSEMAIL.IsNullable = true;
				colvarSEMAIL.IsPrimaryKey = false;
				colvarSEMAIL.IsForeignKey = false;
				colvarSEMAIL.IsReadOnly = false;
				colvarSEMAIL.DefaultSetting = @"";
				colvarSEMAIL.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSEMAIL);
				
				TableSchema.TableColumn colvarSTaxCode = new TableSchema.TableColumn(schema);
				colvarSTaxCode.ColumnName = "sTaxCode";
				colvarSTaxCode.DataType = DbType.String;
				colvarSTaxCode.MaxLength = 17;
				colvarSTaxCode.AutoIncrement = false;
				colvarSTaxCode.IsNullable = true;
				colvarSTaxCode.IsPrimaryKey = false;
				colvarSTaxCode.IsForeignKey = false;
				colvarSTaxCode.IsReadOnly = false;
				colvarSTaxCode.DefaultSetting = @"";
				colvarSTaxCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSTaxCode);
				
				TableSchema.TableColumn colvarSAccountID = new TableSchema.TableColumn(schema);
				colvarSAccountID.ColumnName = "sAccountID";
				colvarSAccountID.DataType = DbType.String;
				colvarSAccountID.MaxLength = 15;
				colvarSAccountID.AutoIncrement = false;
				colvarSAccountID.IsNullable = true;
				colvarSAccountID.IsPrimaryKey = false;
				colvarSAccountID.IsForeignKey = false;
				colvarSAccountID.IsReadOnly = false;
				colvarSAccountID.DefaultSetting = @"";
				colvarSAccountID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSAccountID);
				
				TableSchema.TableColumn colvarSRepresentative = new TableSchema.TableColumn(schema);
				colvarSRepresentative.ColumnName = "sRepresentative";
				colvarSRepresentative.DataType = DbType.String;
				colvarSRepresentative.MaxLength = 50;
				colvarSRepresentative.AutoIncrement = false;
				colvarSRepresentative.IsNullable = true;
				colvarSRepresentative.IsPrimaryKey = false;
				colvarSRepresentative.IsForeignKey = false;
				colvarSRepresentative.IsReadOnly = false;
				colvarSRepresentative.DefaultSetting = @"";
				colvarSRepresentative.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSRepresentative);
				
				TableSchema.TableColumn colvarSPosition = new TableSchema.TableColumn(schema);
				colvarSPosition.ColumnName = "sPosition";
				colvarSPosition.DataType = DbType.String;
				colvarSPosition.MaxLength = 50;
				colvarSPosition.AutoIncrement = false;
				colvarSPosition.IsNullable = true;
				colvarSPosition.IsPrimaryKey = false;
				colvarSPosition.IsForeignKey = false;
				colvarSPosition.IsReadOnly = false;
				colvarSPosition.DefaultSetting = @"";
				colvarSPosition.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSPosition);
				
				TableSchema.TableColumn colvarSProxyNumber = new TableSchema.TableColumn(schema);
				colvarSProxyNumber.ColumnName = "sProxyNumber";
				colvarSProxyNumber.DataType = DbType.String;
				colvarSProxyNumber.MaxLength = 20;
				colvarSProxyNumber.AutoIncrement = false;
				colvarSProxyNumber.IsNullable = true;
				colvarSProxyNumber.IsPrimaryKey = false;
				colvarSProxyNumber.IsForeignKey = false;
				colvarSProxyNumber.IsReadOnly = false;
				colvarSProxyNumber.DefaultSetting = @"";
				colvarSProxyNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSProxyNumber);
				
				TableSchema.TableColumn colvarTDateMandate = new TableSchema.TableColumn(schema);
				colvarTDateMandate.ColumnName = "tDateMandate";
				colvarTDateMandate.DataType = DbType.DateTime;
				colvarTDateMandate.MaxLength = 0;
				colvarTDateMandate.AutoIncrement = false;
				colvarTDateMandate.IsNullable = true;
				colvarTDateMandate.IsPrimaryKey = false;
				colvarTDateMandate.IsForeignKey = false;
				colvarTDateMandate.IsReadOnly = false;
				colvarTDateMandate.DefaultSetting = @"";
				colvarTDateMandate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTDateMandate);
				
				TableSchema.TableColumn colvarSMandateUnitName = new TableSchema.TableColumn(schema);
				colvarSMandateUnitName.ColumnName = "sMandateUnitName";
				colvarSMandateUnitName.DataType = DbType.String;
				colvarSMandateUnitName.MaxLength = 50;
				colvarSMandateUnitName.AutoIncrement = false;
				colvarSMandateUnitName.IsNullable = true;
				colvarSMandateUnitName.IsPrimaryKey = false;
				colvarSMandateUnitName.IsForeignKey = false;
				colvarSMandateUnitName.IsReadOnly = false;
				colvarSMandateUnitName.DefaultSetting = @"";
				colvarSMandateUnitName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSMandateUnitName);
				
				TableSchema.TableColumn colvarSMandateUnitAddress = new TableSchema.TableColumn(schema);
				colvarSMandateUnitAddress.ColumnName = "sMandateUnitAddress";
				colvarSMandateUnitAddress.DataType = DbType.String;
				colvarSMandateUnitAddress.MaxLength = 50;
				colvarSMandateUnitAddress.AutoIncrement = false;
				colvarSMandateUnitAddress.IsNullable = true;
				colvarSMandateUnitAddress.IsPrimaryKey = false;
				colvarSMandateUnitAddress.IsForeignKey = false;
				colvarSMandateUnitAddress.IsReadOnly = false;
				colvarSMandateUnitAddress.DefaultSetting = @"";
				colvarSMandateUnitAddress.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSMandateUnitAddress);
				
				TableSchema.TableColumn colvarTInvalidDate = new TableSchema.TableColumn(schema);
				colvarTInvalidDate.ColumnName = "tInvalidDate";
				colvarTInvalidDate.DataType = DbType.DateTime;
				colvarTInvalidDate.MaxLength = 0;
				colvarTInvalidDate.AutoIncrement = false;
				colvarTInvalidDate.IsNullable = true;
				colvarTInvalidDate.IsPrimaryKey = false;
				colvarTInvalidDate.IsForeignKey = false;
				colvarTInvalidDate.IsReadOnly = false;
				colvarTInvalidDate.DefaultSetting = @"";
				colvarTInvalidDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTInvalidDate);
				
				TableSchema.TableColumn colvarTInputDate = new TableSchema.TableColumn(schema);
				colvarTInputDate.ColumnName = "tInputDate";
				colvarTInputDate.DataType = DbType.DateTime;
				colvarTInputDate.MaxLength = 0;
				colvarTInputDate.AutoIncrement = false;
				colvarTInputDate.IsNullable = true;
				colvarTInputDate.IsPrimaryKey = false;
				colvarTInputDate.IsForeignKey = false;
				colvarTInputDate.IsReadOnly = false;
				colvarTInputDate.DefaultSetting = @"";
				colvarTInputDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTInputDate);
				
				TableSchema.TableColumn colvarSTyper = new TableSchema.TableColumn(schema);
				colvarSTyper.ColumnName = "sTyper";
				colvarSTyper.DataType = DbType.String;
				colvarSTyper.MaxLength = 20;
				colvarSTyper.AutoIncrement = false;
				colvarSTyper.IsNullable = true;
				colvarSTyper.IsPrimaryKey = false;
				colvarSTyper.IsForeignKey = false;
				colvarSTyper.IsReadOnly = false;
				colvarSTyper.DefaultSetting = @"";
				colvarSTyper.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSTyper);
				
				TableSchema.TableColumn colvarSBankCode = new TableSchema.TableColumn(schema);
				colvarSBankCode.ColumnName = "sBankCode";
				colvarSBankCode.DataType = DbType.String;
				colvarSBankCode.MaxLength = 7;
				colvarSBankCode.AutoIncrement = false;
				colvarSBankCode.IsNullable = true;
				colvarSBankCode.IsPrimaryKey = false;
				colvarSBankCode.IsForeignKey = false;
				colvarSBankCode.IsReadOnly = false;
				colvarSBankCode.DefaultSetting = @"";
				colvarSBankCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSBankCode);
				
				TableSchema.TableColumn colvarSLandSurveyUnitCode = new TableSchema.TableColumn(schema);
				colvarSLandSurveyUnitCode.ColumnName = "sLandSurveyUnitCode";
				colvarSLandSurveyUnitCode.DataType = DbType.String;
				colvarSLandSurveyUnitCode.MaxLength = 7;
				colvarSLandSurveyUnitCode.AutoIncrement = false;
				colvarSLandSurveyUnitCode.IsNullable = true;
				colvarSLandSurveyUnitCode.IsPrimaryKey = false;
				colvarSLandSurveyUnitCode.IsForeignKey = false;
				colvarSLandSurveyUnitCode.IsReadOnly = false;
				colvarSLandSurveyUnitCode.DefaultSetting = @"";
				colvarSLandSurveyUnitCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSLandSurveyUnitCode);
				
				TableSchema.TableColumn colvarWebsite = new TableSchema.TableColumn(schema);
				colvarWebsite.ColumnName = "website";
				colvarWebsite.DataType = DbType.String;
				colvarWebsite.MaxLength = 100;
				colvarWebsite.AutoIncrement = false;
				colvarWebsite.IsNullable = true;
				colvarWebsite.IsPrimaryKey = false;
				colvarWebsite.IsForeignKey = false;
				colvarWebsite.IsReadOnly = false;
				
						colvarWebsite.DefaultSetting = @"('')";
				colvarWebsite.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWebsite);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["ORM"].AddSchema("Sys_ManagementUnit",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("PkSBranchID")]
		[Bindable(true)]
		public string PkSBranchID 
		{
			get { return GetColumnValue<string>(Columns.PkSBranchID); }
			set { SetColumnValue(Columns.PkSBranchID, value); }
		}
		  
		[XmlAttribute("SName")]
		[Bindable(true)]
		public string SName 
		{
			get { return GetColumnValue<string>(Columns.SName); }
			set { SetColumnValue(Columns.SName, value); }
		}
		  
		[XmlAttribute("SParentBranchName")]
		[Bindable(true)]
		public string SParentBranchName 
		{
			get { return GetColumnValue<string>(Columns.SParentBranchName); }
			set { SetColumnValue(Columns.SParentBranchName, value); }
		}
		  
		[XmlAttribute("FpSUpBranchID")]
		[Bindable(true)]
		public string FpSUpBranchID 
		{
			get { return GetColumnValue<string>(Columns.FpSUpBranchID); }
			set { SetColumnValue(Columns.FpSUpBranchID, value); }
		}
		  
		[XmlAttribute("IntLevel")]
		[Bindable(true)]
		public int? IntLevel 
		{
			get { return GetColumnValue<int?>(Columns.IntLevel); }
			set { SetColumnValue(Columns.IntLevel, value); }
		}
		  
		[XmlAttribute("SAddress")]
		[Bindable(true)]
		public string SAddress 
		{
			get { return GetColumnValue<string>(Columns.SAddress); }
			set { SetColumnValue(Columns.SAddress, value); }
		}
		  
		[XmlAttribute("SPhone")]
		[Bindable(true)]
		public string SPhone 
		{
			get { return GetColumnValue<string>(Columns.SPhone); }
			set { SetColumnValue(Columns.SPhone, value); }
		}
		  
		[XmlAttribute("SBussinessPhone")]
		[Bindable(true)]
		public string SBussinessPhone 
		{
			get { return GetColumnValue<string>(Columns.SBussinessPhone); }
			set { SetColumnValue(Columns.SBussinessPhone, value); }
		}
		  
		[XmlAttribute("SHotPhone")]
		[Bindable(true)]
		public string SHotPhone 
		{
			get { return GetColumnValue<string>(Columns.SHotPhone); }
			set { SetColumnValue(Columns.SHotPhone, value); }
		}
		  
		[XmlAttribute("SDutyPhone")]
		[Bindable(true)]
		public string SDutyPhone 
		{
			get { return GetColumnValue<string>(Columns.SDutyPhone); }
			set { SetColumnValue(Columns.SDutyPhone, value); }
		}
		  
		[XmlAttribute("SFAX")]
		[Bindable(true)]
		public string SFAX 
		{
			get { return GetColumnValue<string>(Columns.SFAX); }
			set { SetColumnValue(Columns.SFAX, value); }
		}
		  
		[XmlAttribute("SEMAIL")]
		[Bindable(true)]
		public string SEMAIL 
		{
			get { return GetColumnValue<string>(Columns.SEMAIL); }
			set { SetColumnValue(Columns.SEMAIL, value); }
		}
		  
		[XmlAttribute("STaxCode")]
		[Bindable(true)]
		public string STaxCode 
		{
			get { return GetColumnValue<string>(Columns.STaxCode); }
			set { SetColumnValue(Columns.STaxCode, value); }
		}
		  
		[XmlAttribute("SAccountID")]
		[Bindable(true)]
		public string SAccountID 
		{
			get { return GetColumnValue<string>(Columns.SAccountID); }
			set { SetColumnValue(Columns.SAccountID, value); }
		}
		  
		[XmlAttribute("SRepresentative")]
		[Bindable(true)]
		public string SRepresentative 
		{
			get { return GetColumnValue<string>(Columns.SRepresentative); }
			set { SetColumnValue(Columns.SRepresentative, value); }
		}
		  
		[XmlAttribute("SPosition")]
		[Bindable(true)]
		public string SPosition 
		{
			get { return GetColumnValue<string>(Columns.SPosition); }
			set { SetColumnValue(Columns.SPosition, value); }
		}
		  
		[XmlAttribute("SProxyNumber")]
		[Bindable(true)]
		public string SProxyNumber 
		{
			get { return GetColumnValue<string>(Columns.SProxyNumber); }
			set { SetColumnValue(Columns.SProxyNumber, value); }
		}
		  
		[XmlAttribute("TDateMandate")]
		[Bindable(true)]
		public DateTime? TDateMandate 
		{
			get { return GetColumnValue<DateTime?>(Columns.TDateMandate); }
			set { SetColumnValue(Columns.TDateMandate, value); }
		}
		  
		[XmlAttribute("SMandateUnitName")]
		[Bindable(true)]
		public string SMandateUnitName 
		{
			get { return GetColumnValue<string>(Columns.SMandateUnitName); }
			set { SetColumnValue(Columns.SMandateUnitName, value); }
		}
		  
		[XmlAttribute("SMandateUnitAddress")]
		[Bindable(true)]
		public string SMandateUnitAddress 
		{
			get { return GetColumnValue<string>(Columns.SMandateUnitAddress); }
			set { SetColumnValue(Columns.SMandateUnitAddress, value); }
		}
		  
		[XmlAttribute("TInvalidDate")]
		[Bindable(true)]
		public DateTime? TInvalidDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.TInvalidDate); }
			set { SetColumnValue(Columns.TInvalidDate, value); }
		}
		  
		[XmlAttribute("TInputDate")]
		[Bindable(true)]
		public DateTime? TInputDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.TInputDate); }
			set { SetColumnValue(Columns.TInputDate, value); }
		}
		  
		[XmlAttribute("STyper")]
		[Bindable(true)]
		public string STyper 
		{
			get { return GetColumnValue<string>(Columns.STyper); }
			set { SetColumnValue(Columns.STyper, value); }
		}
		  
		[XmlAttribute("SBankCode")]
		[Bindable(true)]
		public string SBankCode 
		{
			get { return GetColumnValue<string>(Columns.SBankCode); }
			set { SetColumnValue(Columns.SBankCode, value); }
		}
		  
		[XmlAttribute("SLandSurveyUnitCode")]
		[Bindable(true)]
		public string SLandSurveyUnitCode 
		{
			get { return GetColumnValue<string>(Columns.SLandSurveyUnitCode); }
			set { SetColumnValue(Columns.SLandSurveyUnitCode, value); }
		}
		  
		[XmlAttribute("Website")]
		[Bindable(true)]
		public string Website 
		{
			get { return GetColumnValue<string>(Columns.Website); }
			set { SetColumnValue(Columns.Website, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varPkSBranchID,string varSName,string varSParentBranchName,string varFpSUpBranchID,int? varIntLevel,string varSAddress,string varSPhone,string varSBussinessPhone,string varSHotPhone,string varSDutyPhone,string varSFAX,string varSEMAIL,string varSTaxCode,string varSAccountID,string varSRepresentative,string varSPosition,string varSProxyNumber,DateTime? varTDateMandate,string varSMandateUnitName,string varSMandateUnitAddress,DateTime? varTInvalidDate,DateTime? varTInputDate,string varSTyper,string varSBankCode,string varSLandSurveyUnitCode,string varWebsite)
		{
			SysManagementUnit item = new SysManagementUnit();
			
			item.PkSBranchID = varPkSBranchID;
			
			item.SName = varSName;
			
			item.SParentBranchName = varSParentBranchName;
			
			item.FpSUpBranchID = varFpSUpBranchID;
			
			item.IntLevel = varIntLevel;
			
			item.SAddress = varSAddress;
			
			item.SPhone = varSPhone;
			
			item.SBussinessPhone = varSBussinessPhone;
			
			item.SHotPhone = varSHotPhone;
			
			item.SDutyPhone = varSDutyPhone;
			
			item.SFAX = varSFAX;
			
			item.SEMAIL = varSEMAIL;
			
			item.STaxCode = varSTaxCode;
			
			item.SAccountID = varSAccountID;
			
			item.SRepresentative = varSRepresentative;
			
			item.SPosition = varSPosition;
			
			item.SProxyNumber = varSProxyNumber;
			
			item.TDateMandate = varTDateMandate;
			
			item.SMandateUnitName = varSMandateUnitName;
			
			item.SMandateUnitAddress = varSMandateUnitAddress;
			
			item.TInvalidDate = varTInvalidDate;
			
			item.TInputDate = varTInputDate;
			
			item.STyper = varSTyper;
			
			item.SBankCode = varSBankCode;
			
			item.SLandSurveyUnitCode = varSLandSurveyUnitCode;
			
			item.Website = varWebsite;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(string varPkSBranchID,string varSName,string varSParentBranchName,string varFpSUpBranchID,int? varIntLevel,string varSAddress,string varSPhone,string varSBussinessPhone,string varSHotPhone,string varSDutyPhone,string varSFAX,string varSEMAIL,string varSTaxCode,string varSAccountID,string varSRepresentative,string varSPosition,string varSProxyNumber,DateTime? varTDateMandate,string varSMandateUnitName,string varSMandateUnitAddress,DateTime? varTInvalidDate,DateTime? varTInputDate,string varSTyper,string varSBankCode,string varSLandSurveyUnitCode,string varWebsite)
		{
			SysManagementUnit item = new SysManagementUnit();
			
				item.PkSBranchID = varPkSBranchID;
			
				item.SName = varSName;
			
				item.SParentBranchName = varSParentBranchName;
			
				item.FpSUpBranchID = varFpSUpBranchID;
			
				item.IntLevel = varIntLevel;
			
				item.SAddress = varSAddress;
			
				item.SPhone = varSPhone;
			
				item.SBussinessPhone = varSBussinessPhone;
			
				item.SHotPhone = varSHotPhone;
			
				item.SDutyPhone = varSDutyPhone;
			
				item.SFAX = varSFAX;
			
				item.SEMAIL = varSEMAIL;
			
				item.STaxCode = varSTaxCode;
			
				item.SAccountID = varSAccountID;
			
				item.SRepresentative = varSRepresentative;
			
				item.SPosition = varSPosition;
			
				item.SProxyNumber = varSProxyNumber;
			
				item.TDateMandate = varTDateMandate;
			
				item.SMandateUnitName = varSMandateUnitName;
			
				item.SMandateUnitAddress = varSMandateUnitAddress;
			
				item.TInvalidDate = varTInvalidDate;
			
				item.TInputDate = varTInputDate;
			
				item.STyper = varSTyper;
			
				item.SBankCode = varSBankCode;
			
				item.SLandSurveyUnitCode = varSLandSurveyUnitCode;
			
				item.Website = varWebsite;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn PkSBranchIDColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn SNameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn SParentBranchNameColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn FpSUpBranchIDColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn IntLevelColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn SAddressColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn SPhoneColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn SBussinessPhoneColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn SHotPhoneColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn SDutyPhoneColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn SFAXColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn SEMAILColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn STaxCodeColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn SAccountIDColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn SRepresentativeColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn SPositionColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn SProxyNumberColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn TDateMandateColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn SMandateUnitNameColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn SMandateUnitAddressColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn TInvalidDateColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn TInputDateColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        public static TableSchema.TableColumn STyperColumn
        {
            get { return Schema.Columns[22]; }
        }
        
        
        
        public static TableSchema.TableColumn SBankCodeColumn
        {
            get { return Schema.Columns[23]; }
        }
        
        
        
        public static TableSchema.TableColumn SLandSurveyUnitCodeColumn
        {
            get { return Schema.Columns[24]; }
        }
        
        
        
        public static TableSchema.TableColumn WebsiteColumn
        {
            get { return Schema.Columns[25]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string PkSBranchID = @"PK_sBranchID";
			 public static string SName = @"sName";
			 public static string SParentBranchName = @"sParentBranchName";
			 public static string FpSUpBranchID = @"FP_sUpBranchID";
			 public static string IntLevel = @"intLevel";
			 public static string SAddress = @"sAddress";
			 public static string SPhone = @"sPhone";
			 public static string SBussinessPhone = @"sBussinessPhone";
			 public static string SHotPhone = @"sHotPhone";
			 public static string SDutyPhone = @"sDutyPhone";
			 public static string SFAX = @"sFAX";
			 public static string SEMAIL = @"sEMAIL";
			 public static string STaxCode = @"sTaxCode";
			 public static string SAccountID = @"sAccountID";
			 public static string SRepresentative = @"sRepresentative";
			 public static string SPosition = @"sPosition";
			 public static string SProxyNumber = @"sProxyNumber";
			 public static string TDateMandate = @"tDateMandate";
			 public static string SMandateUnitName = @"sMandateUnitName";
			 public static string SMandateUnitAddress = @"sMandateUnitAddress";
			 public static string TInvalidDate = @"tInvalidDate";
			 public static string TInputDate = @"tInputDate";
			 public static string STyper = @"sTyper";
			 public static string SBankCode = @"sBankCode";
			 public static string SLandSurveyUnitCode = @"sLandSurveyUnitCode";
			 public static string Website = @"website";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
