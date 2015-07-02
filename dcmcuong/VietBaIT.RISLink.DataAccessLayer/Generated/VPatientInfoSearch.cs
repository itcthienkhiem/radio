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
namespace VietBaIT.RISLink.DataAccessLayer{
    /// <summary>
    /// Strongly-typed collection for the VPatientInfoSearch class.
    /// </summary>
    [Serializable]
    public partial class VPatientInfoSearchCollection : ReadOnlyList<VPatientInfoSearch, VPatientInfoSearchCollection>
    {        
        public VPatientInfoSearchCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the VPatientInfoSearch view.
    /// </summary>
    [Serializable]
    public partial class VPatientInfoSearch : ReadOnlyRecord<VPatientInfoSearch>, IReadOnlyRecord
    {
    
	    #region Default Settings
	    protected static void SetSQLProps() 
	    {
		    GetTableSchema();
	    }
	    #endregion
        #region Schema Accessor
	    public static TableSchema.Table Schema
        {
            get
            {
                if (BaseSchema == null)
                {
                    SetSQLProps();
                }
                return BaseSchema;
            }
        }
    	
        private static void GetTableSchema() 
        {
            if(!IsSchemaInitialized)
            {
                //Schema declaration
                TableSchema.Table schema = new TableSchema.Table("VPatientInfoSearch", TableType.View, DataService.GetInstance("ORM"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarPatientId = new TableSchema.TableColumn(schema);
                colvarPatientId.ColumnName = "Patient_ID";
                colvarPatientId.DataType = DbType.Int32;
                colvarPatientId.MaxLength = 0;
                colvarPatientId.AutoIncrement = false;
                colvarPatientId.IsNullable = false;
                colvarPatientId.IsPrimaryKey = false;
                colvarPatientId.IsForeignKey = false;
                colvarPatientId.IsReadOnly = false;
                
                schema.Columns.Add(colvarPatientId);
                
                TableSchema.TableColumn colvarPatientCode = new TableSchema.TableColumn(schema);
                colvarPatientCode.ColumnName = "Patient_Code";
                colvarPatientCode.DataType = DbType.AnsiString;
                colvarPatientCode.MaxLength = 20;
                colvarPatientCode.AutoIncrement = false;
                colvarPatientCode.IsNullable = false;
                colvarPatientCode.IsPrimaryKey = false;
                colvarPatientCode.IsForeignKey = false;
                colvarPatientCode.IsReadOnly = false;
                
                schema.Columns.Add(colvarPatientCode);
                
                TableSchema.TableColumn colvarInputDate = new TableSchema.TableColumn(schema);
                colvarInputDate.ColumnName = "Input_Date";
                colvarInputDate.DataType = DbType.DateTime;
                colvarInputDate.MaxLength = 0;
                colvarInputDate.AutoIncrement = false;
                colvarInputDate.IsNullable = false;
                colvarInputDate.IsPrimaryKey = false;
                colvarInputDate.IsForeignKey = false;
                colvarInputDate.IsReadOnly = false;
                
                schema.Columns.Add(colvarInputDate);
                
                TableSchema.TableColumn colvarHosStatus = new TableSchema.TableColumn(schema);
                colvarHosStatus.ColumnName = "Hos_status";
                colvarHosStatus.DataType = DbType.Byte;
                colvarHosStatus.MaxLength = 0;
                colvarHosStatus.AutoIncrement = false;
                colvarHosStatus.IsNullable = false;
                colvarHosStatus.IsPrimaryKey = false;
                colvarHosStatus.IsForeignKey = false;
                colvarHosStatus.IsReadOnly = false;
                
                schema.Columns.Add(colvarHosStatus);
                
                TableSchema.TableColumn colvarPatientName = new TableSchema.TableColumn(schema);
                colvarPatientName.ColumnName = "Patient_Name";
                colvarPatientName.DataType = DbType.String;
                colvarPatientName.MaxLength = 100;
                colvarPatientName.AutoIncrement = false;
                colvarPatientName.IsNullable = true;
                colvarPatientName.IsPrimaryKey = false;
                colvarPatientName.IsForeignKey = false;
                colvarPatientName.IsReadOnly = false;
                
                schema.Columns.Add(colvarPatientName);
                
                TableSchema.TableColumn colvarPatientSex = new TableSchema.TableColumn(schema);
                colvarPatientSex.ColumnName = "Patient_Sex";
                colvarPatientSex.DataType = DbType.Boolean;
                colvarPatientSex.MaxLength = 0;
                colvarPatientSex.AutoIncrement = false;
                colvarPatientSex.IsNullable = false;
                colvarPatientSex.IsPrimaryKey = false;
                colvarPatientSex.IsForeignKey = false;
                colvarPatientSex.IsReadOnly = false;
                
                schema.Columns.Add(colvarPatientSex);
                
                TableSchema.TableColumn colvarPatientYOB = new TableSchema.TableColumn(schema);
                colvarPatientYOB.ColumnName = "PatientYOB";
                colvarPatientYOB.DataType = DbType.Int32;
                colvarPatientYOB.MaxLength = 0;
                colvarPatientYOB.AutoIncrement = false;
                colvarPatientYOB.IsNullable = true;
                colvarPatientYOB.IsPrimaryKey = false;
                colvarPatientYOB.IsForeignKey = false;
                colvarPatientYOB.IsReadOnly = false;
                
                schema.Columns.Add(colvarPatientYOB);
                
                TableSchema.TableColumn colvarPatientDOB = new TableSchema.TableColumn(schema);
                colvarPatientDOB.ColumnName = "PatientDOB";
                colvarPatientDOB.DataType = DbType.DateTime;
                colvarPatientDOB.MaxLength = 0;
                colvarPatientDOB.AutoIncrement = false;
                colvarPatientDOB.IsNullable = true;
                colvarPatientDOB.IsPrimaryKey = false;
                colvarPatientDOB.IsForeignKey = false;
                colvarPatientDOB.IsReadOnly = false;
                
                schema.Columns.Add(colvarPatientDOB);
                
                TableSchema.TableColumn colvarPatientAddr = new TableSchema.TableColumn(schema);
                colvarPatientAddr.ColumnName = "Patient_Addr";
                colvarPatientAddr.DataType = DbType.String;
                colvarPatientAddr.MaxLength = 200;
                colvarPatientAddr.AutoIncrement = false;
                colvarPatientAddr.IsNullable = true;
                colvarPatientAddr.IsPrimaryKey = false;
                colvarPatientAddr.IsForeignKey = false;
                colvarPatientAddr.IsReadOnly = false;
                
                schema.Columns.Add(colvarPatientAddr);
                
                TableSchema.TableColumn colvarPatientJob = new TableSchema.TableColumn(schema);
                colvarPatientJob.ColumnName = "Patient_Job";
                colvarPatientJob.DataType = DbType.String;
                colvarPatientJob.MaxLength = 100;
                colvarPatientJob.AutoIncrement = false;
                colvarPatientJob.IsNullable = true;
                colvarPatientJob.IsPrimaryKey = false;
                colvarPatientJob.IsForeignKey = false;
                colvarPatientJob.IsReadOnly = false;
                
                schema.Columns.Add(colvarPatientJob);
                
                TableSchema.TableColumn colvarPaymentStatus = new TableSchema.TableColumn(schema);
                colvarPaymentStatus.ColumnName = "Payment_Status";
                colvarPaymentStatus.DataType = DbType.Boolean;
                colvarPaymentStatus.MaxLength = 0;
                colvarPaymentStatus.AutoIncrement = false;
                colvarPaymentStatus.IsNullable = true;
                colvarPaymentStatus.IsPrimaryKey = false;
                colvarPaymentStatus.IsForeignKey = false;
                colvarPaymentStatus.IsReadOnly = false;
                
                schema.Columns.Add(colvarPaymentStatus);
                
                TableSchema.TableColumn colvarObjectTypeId = new TableSchema.TableColumn(schema);
                colvarObjectTypeId.ColumnName = "ObjectType_ID";
                colvarObjectTypeId.DataType = DbType.Int16;
                colvarObjectTypeId.MaxLength = 0;
                colvarObjectTypeId.AutoIncrement = false;
                colvarObjectTypeId.IsNullable = false;
                colvarObjectTypeId.IsPrimaryKey = false;
                colvarObjectTypeId.IsForeignKey = false;
                colvarObjectTypeId.IsReadOnly = false;
                
                schema.Columns.Add(colvarObjectTypeId);
                
                TableSchema.TableColumn colvarObjectTypeName = new TableSchema.TableColumn(schema);
                colvarObjectTypeName.ColumnName = "ObjectTypeName";
                colvarObjectTypeName.DataType = DbType.String;
                colvarObjectTypeName.MaxLength = 100;
                colvarObjectTypeName.AutoIncrement = false;
                colvarObjectTypeName.IsNullable = false;
                colvarObjectTypeName.IsPrimaryKey = false;
                colvarObjectTypeName.IsForeignKey = false;
                colvarObjectTypeName.IsReadOnly = false;
                
                schema.Columns.Add(colvarObjectTypeName);
                
                TableSchema.TableColumn colvarInsurranceNum = new TableSchema.TableColumn(schema);
                colvarInsurranceNum.ColumnName = "Insurrance_Num";
                colvarInsurranceNum.DataType = DbType.AnsiString;
                colvarInsurranceNum.MaxLength = 100;
                colvarInsurranceNum.AutoIncrement = false;
                colvarInsurranceNum.IsNullable = true;
                colvarInsurranceNum.IsPrimaryKey = false;
                colvarInsurranceNum.IsForeignKey = false;
                colvarInsurranceNum.IsReadOnly = false;
                
                schema.Columns.Add(colvarInsurranceNum);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("VPatientInfoSearch",schema);
            }
        }
        #endregion
        
        #region Query Accessor
	    public static Query CreateQuery()
	    {
		    return new Query(Schema);
	    }
	    #endregion
	    
	    #region .ctors
	    public VPatientInfoSearch()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public VPatientInfoSearch(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public VPatientInfoSearch(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public VPatientInfoSearch(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("PatientId")]
        [Bindable(true)]
        public int PatientId 
	    {
		    get
		    {
			    return GetColumnValue<int>("Patient_ID");
		    }
            set 
		    {
			    SetColumnValue("Patient_ID", value);
            }
        }
	      
        [XmlAttribute("PatientCode")]
        [Bindable(true)]
        public string PatientCode 
	    {
		    get
		    {
			    return GetColumnValue<string>("Patient_Code");
		    }
            set 
		    {
			    SetColumnValue("Patient_Code", value);
            }
        }
	      
        [XmlAttribute("InputDate")]
        [Bindable(true)]
        public DateTime InputDate 
	    {
		    get
		    {
			    return GetColumnValue<DateTime>("Input_Date");
		    }
            set 
		    {
			    SetColumnValue("Input_Date", value);
            }
        }
	      
        [XmlAttribute("HosStatus")]
        [Bindable(true)]
        public byte HosStatus 
	    {
		    get
		    {
			    return GetColumnValue<byte>("Hos_status");
		    }
            set 
		    {
			    SetColumnValue("Hos_status", value);
            }
        }
	      
        [XmlAttribute("PatientName")]
        [Bindable(true)]
        public string PatientName 
	    {
		    get
		    {
			    return GetColumnValue<string>("Patient_Name");
		    }
            set 
		    {
			    SetColumnValue("Patient_Name", value);
            }
        }
	      
        [XmlAttribute("PatientSex")]
        [Bindable(true)]
        public bool PatientSex 
	    {
		    get
		    {
			    return GetColumnValue<bool>("Patient_Sex");
		    }
            set 
		    {
			    SetColumnValue("Patient_Sex", value);
            }
        }
	      
        [XmlAttribute("PatientYOB")]
        [Bindable(true)]
        public int? PatientYOB 
	    {
		    get
		    {
			    return GetColumnValue<int?>("PatientYOB");
		    }
            set 
		    {
			    SetColumnValue("PatientYOB", value);
            }
        }
	      
        [XmlAttribute("PatientDOB")]
        [Bindable(true)]
        public DateTime? PatientDOB 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("PatientDOB");
		    }
            set 
		    {
			    SetColumnValue("PatientDOB", value);
            }
        }
	      
        [XmlAttribute("PatientAddr")]
        [Bindable(true)]
        public string PatientAddr 
	    {
		    get
		    {
			    return GetColumnValue<string>("Patient_Addr");
		    }
            set 
		    {
			    SetColumnValue("Patient_Addr", value);
            }
        }
	      
        [XmlAttribute("PatientJob")]
        [Bindable(true)]
        public string PatientJob 
	    {
		    get
		    {
			    return GetColumnValue<string>("Patient_Job");
		    }
            set 
		    {
			    SetColumnValue("Patient_Job", value);
            }
        }
	      
        [XmlAttribute("PaymentStatus")]
        [Bindable(true)]
        public bool? PaymentStatus 
	    {
		    get
		    {
			    return GetColumnValue<bool?>("Payment_Status");
		    }
            set 
		    {
			    SetColumnValue("Payment_Status", value);
            }
        }
	      
        [XmlAttribute("ObjectTypeId")]
        [Bindable(true)]
        public short ObjectTypeId 
	    {
		    get
		    {
			    return GetColumnValue<short>("ObjectType_ID");
		    }
            set 
		    {
			    SetColumnValue("ObjectType_ID", value);
            }
        }
	      
        [XmlAttribute("ObjectTypeName")]
        [Bindable(true)]
        public string ObjectTypeName 
	    {
		    get
		    {
			    return GetColumnValue<string>("ObjectTypeName");
		    }
            set 
		    {
			    SetColumnValue("ObjectTypeName", value);
            }
        }
	      
        [XmlAttribute("InsurranceNum")]
        [Bindable(true)]
        public string InsurranceNum 
	    {
		    get
		    {
			    return GetColumnValue<string>("Insurrance_Num");
		    }
            set 
		    {
			    SetColumnValue("Insurrance_Num", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string PatientId = @"Patient_ID";
            
            public static string PatientCode = @"Patient_Code";
            
            public static string InputDate = @"Input_Date";
            
            public static string HosStatus = @"Hos_status";
            
            public static string PatientName = @"Patient_Name";
            
            public static string PatientSex = @"Patient_Sex";
            
            public static string PatientYOB = @"PatientYOB";
            
            public static string PatientDOB = @"PatientDOB";
            
            public static string PatientAddr = @"Patient_Addr";
            
            public static string PatientJob = @"Patient_Job";
            
            public static string PaymentStatus = @"Payment_Status";
            
            public static string ObjectTypeId = @"ObjectType_ID";
            
            public static string ObjectTypeName = @"ObjectTypeName";
            
            public static string InsurranceNum = @"Insurrance_Num";
            
	    }
	    #endregion
	    
	    
	    #region IAbstractRecord Members
        public new CT GetColumnValue<CT>(string columnName) {
            return base.GetColumnValue<CT>(columnName);
        }
        public object GetColumnValue(string columnName) {
            return base.GetColumnValue<object>(columnName);
        }
        #endregion
	    
    }
}
