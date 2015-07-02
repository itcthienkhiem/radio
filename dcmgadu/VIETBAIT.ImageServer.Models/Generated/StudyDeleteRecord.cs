using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Xml.Serialization;
using SubSonic;

// <auto-generated />

namespace VIETBAIT.ImageServer.Models
{
    /// <summary>
    /// Strongly-typed collection for the StudyDeleteRecord class.
    /// </summary>
    [Serializable]
    public class StudyDeleteRecordCollection : ActiveList<StudyDeleteRecord, StudyDeleteRecordCollection>
    {
        /// <summary>
        /// Filters an existing collection based on the set criteria. This is an in-memory filter
        /// Thanks to developingchris for this!
        /// </summary>
        /// <returns>StudyDeleteRecordCollection</returns>
        public StudyDeleteRecordCollection Filter()
        {
            for (int i = Count - 1; i > -1; i--)
            {
                StudyDeleteRecord o = this[i];
                foreach (Where w in wheres)
                {
                    bool remove = false;
                    PropertyInfo pi = o.GetType().GetProperty(w.ColumnName);
                    if (pi.CanRead)
                    {
                        object val = pi.GetValue(o, null);
                        switch (w.Comparison)
                        {
                            case Comparison.Equals:
                                if (!val.Equals(w.ParameterValue))
                                {
                                    remove = true;
                                }
                                break;
                        }
                    }
                    if (remove)
                    {
                        Remove(o);
                        break;
                    }
                }
            }
            return this;
        }
    }

    /// <summary>
    /// This is an ActiveRecord class which wraps the StudyDeleteRecord table.
    /// </summary>
    [Serializable]
    public class StudyDeleteRecord : ActiveRecord<StudyDeleteRecord>, IActiveRecord
    {
        #region .ctors and Default Settings

        public StudyDeleteRecord()
        {
            SetSQLProps();
            InitSetDefaults();
            MarkNew();
        }

        public StudyDeleteRecord(bool useDatabaseDefaults)
        {
            SetSQLProps();
            if (useDatabaseDefaults)
                ForceDefaults();
            MarkNew();
        }

        public StudyDeleteRecord(object keyID)
        {
            SetSQLProps();
            InitSetDefaults();
            LoadByKey(keyID);
        }

        public StudyDeleteRecord(string columnName, object columnValue)
        {
            SetSQLProps();
            InitSetDefaults();
            LoadByParam(columnName, columnValue);
        }

        private void InitSetDefaults()
        {
            SetDefaults();
        }

        protected static void SetSQLProps()
        {
            GetTableSchema();
        }

        #endregion

        #region Schema and Query Accessor	

        public static TableSchema.Table Schema
        {
            get
            {
                if (BaseSchema == null)
                    SetSQLProps();
                return BaseSchema;
            }
        }

        public static Query CreateQuery()
        {
            return new Query(Schema);
        }

        private static void GetTableSchema()
        {
            if (!IsSchemaInitialized)
            {
                //Schema declaration
                var schema = new TableSchema.Table("StudyDeleteRecord", TableType.Table, DataService.GetInstance("ORM"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns

                var colvarGuid = new TableSchema.TableColumn(schema);
                colvarGuid.ColumnName = "GUID";
                colvarGuid.DataType = DbType.Guid;
                colvarGuid.MaxLength = 0;
                colvarGuid.AutoIncrement = false;
                colvarGuid.IsNullable = false;
                colvarGuid.IsPrimaryKey = true;
                colvarGuid.IsForeignKey = false;
                colvarGuid.IsReadOnly = false;

                colvarGuid.DefaultSetting = @"(newid())";
                colvarGuid.ForeignKeyTableName = "";
                schema.Columns.Add(colvarGuid);

                var colvarTimestamp = new TableSchema.TableColumn(schema);
                colvarTimestamp.ColumnName = "Timestamp";
                colvarTimestamp.DataType = DbType.DateTime;
                colvarTimestamp.MaxLength = 0;
                colvarTimestamp.AutoIncrement = false;
                colvarTimestamp.IsNullable = false;
                colvarTimestamp.IsPrimaryKey = false;
                colvarTimestamp.IsForeignKey = false;
                colvarTimestamp.IsReadOnly = false;
                colvarTimestamp.DefaultSetting = @"";
                colvarTimestamp.ForeignKeyTableName = "";
                schema.Columns.Add(colvarTimestamp);

                var colvarReason = new TableSchema.TableColumn(schema);
                colvarReason.ColumnName = "Reason";
                colvarReason.DataType = DbType.String;
                colvarReason.MaxLength = 1024;
                colvarReason.AutoIncrement = false;
                colvarReason.IsNullable = true;
                colvarReason.IsPrimaryKey = false;
                colvarReason.IsForeignKey = false;
                colvarReason.IsReadOnly = false;
                colvarReason.DefaultSetting = @"";
                colvarReason.ForeignKeyTableName = "";
                schema.Columns.Add(colvarReason);

                var colvarServerPartitionAE = new TableSchema.TableColumn(schema);
                colvarServerPartitionAE.ColumnName = "ServerPartitionAE";
                colvarServerPartitionAE.DataType = DbType.AnsiString;
                colvarServerPartitionAE.MaxLength = 64;
                colvarServerPartitionAE.AutoIncrement = false;
                colvarServerPartitionAE.IsNullable = false;
                colvarServerPartitionAE.IsPrimaryKey = false;
                colvarServerPartitionAE.IsForeignKey = false;
                colvarServerPartitionAE.IsReadOnly = false;
                colvarServerPartitionAE.DefaultSetting = @"";
                colvarServerPartitionAE.ForeignKeyTableName = "";
                schema.Columns.Add(colvarServerPartitionAE);

                var colvarFilesystemGUID = new TableSchema.TableColumn(schema);
                colvarFilesystemGUID.ColumnName = "FilesystemGUID";
                colvarFilesystemGUID.DataType = DbType.Guid;
                colvarFilesystemGUID.MaxLength = 0;
                colvarFilesystemGUID.AutoIncrement = false;
                colvarFilesystemGUID.IsNullable = false;
                colvarFilesystemGUID.IsPrimaryKey = false;
                colvarFilesystemGUID.IsForeignKey = true;
                colvarFilesystemGUID.IsReadOnly = false;
                colvarFilesystemGUID.DefaultSetting = @"";

                colvarFilesystemGUID.ForeignKeyTableName = "Filesystem";
                schema.Columns.Add(colvarFilesystemGUID);

                var colvarBackupPath = new TableSchema.TableColumn(schema);
                colvarBackupPath.ColumnName = "BackupPath";
                colvarBackupPath.DataType = DbType.String;
                colvarBackupPath.MaxLength = 256;
                colvarBackupPath.AutoIncrement = false;
                colvarBackupPath.IsNullable = true;
                colvarBackupPath.IsPrimaryKey = false;
                colvarBackupPath.IsForeignKey = false;
                colvarBackupPath.IsReadOnly = false;
                colvarBackupPath.DefaultSetting = @"";
                colvarBackupPath.ForeignKeyTableName = "";
                schema.Columns.Add(colvarBackupPath);

                var colvarStudyInstanceUid = new TableSchema.TableColumn(schema);
                colvarStudyInstanceUid.ColumnName = "StudyInstanceUid";
                colvarStudyInstanceUid.DataType = DbType.AnsiString;
                colvarStudyInstanceUid.MaxLength = 64;
                colvarStudyInstanceUid.AutoIncrement = false;
                colvarStudyInstanceUid.IsNullable = false;
                colvarStudyInstanceUid.IsPrimaryKey = false;
                colvarStudyInstanceUid.IsForeignKey = false;
                colvarStudyInstanceUid.IsReadOnly = false;
                colvarStudyInstanceUid.DefaultSetting = @"";
                colvarStudyInstanceUid.ForeignKeyTableName = "";
                schema.Columns.Add(colvarStudyInstanceUid);

                var colvarAccessionNumber = new TableSchema.TableColumn(schema);
                colvarAccessionNumber.ColumnName = "AccessionNumber";
                colvarAccessionNumber.DataType = DbType.AnsiString;
                colvarAccessionNumber.MaxLength = 64;
                colvarAccessionNumber.AutoIncrement = false;
                colvarAccessionNumber.IsNullable = true;
                colvarAccessionNumber.IsPrimaryKey = false;
                colvarAccessionNumber.IsForeignKey = false;
                colvarAccessionNumber.IsReadOnly = false;
                colvarAccessionNumber.DefaultSetting = @"";
                colvarAccessionNumber.ForeignKeyTableName = "";
                schema.Columns.Add(colvarAccessionNumber);

                var colvarPatientId = new TableSchema.TableColumn(schema);
                colvarPatientId.ColumnName = "PatientId";
                colvarPatientId.DataType = DbType.AnsiString;
                colvarPatientId.MaxLength = 64;
                colvarPatientId.AutoIncrement = false;
                colvarPatientId.IsNullable = true;
                colvarPatientId.IsPrimaryKey = false;
                colvarPatientId.IsForeignKey = false;
                colvarPatientId.IsReadOnly = false;
                colvarPatientId.DefaultSetting = @"";
                colvarPatientId.ForeignKeyTableName = "";
                schema.Columns.Add(colvarPatientId);

                var colvarPatientsName = new TableSchema.TableColumn(schema);
                colvarPatientsName.ColumnName = "PatientsName";
                colvarPatientsName.DataType = DbType.String;
                colvarPatientsName.MaxLength = 256;
                colvarPatientsName.AutoIncrement = false;
                colvarPatientsName.IsNullable = true;
                colvarPatientsName.IsPrimaryKey = false;
                colvarPatientsName.IsForeignKey = false;
                colvarPatientsName.IsReadOnly = false;
                colvarPatientsName.DefaultSetting = @"";
                colvarPatientsName.ForeignKeyTableName = "";
                schema.Columns.Add(colvarPatientsName);

                var colvarStudyId = new TableSchema.TableColumn(schema);
                colvarStudyId.ColumnName = "StudyId";
                colvarStudyId.DataType = DbType.String;
                colvarStudyId.MaxLength = 64;
                colvarStudyId.AutoIncrement = false;
                colvarStudyId.IsNullable = true;
                colvarStudyId.IsPrimaryKey = false;
                colvarStudyId.IsForeignKey = false;
                colvarStudyId.IsReadOnly = false;
                colvarStudyId.DefaultSetting = @"";
                colvarStudyId.ForeignKeyTableName = "";
                schema.Columns.Add(colvarStudyId);

                var colvarStudyDescription = new TableSchema.TableColumn(schema);
                colvarStudyDescription.ColumnName = "StudyDescription";
                colvarStudyDescription.DataType = DbType.String;
                colvarStudyDescription.MaxLength = 64;
                colvarStudyDescription.AutoIncrement = false;
                colvarStudyDescription.IsNullable = true;
                colvarStudyDescription.IsPrimaryKey = false;
                colvarStudyDescription.IsForeignKey = false;
                colvarStudyDescription.IsReadOnly = false;
                colvarStudyDescription.DefaultSetting = @"";
                colvarStudyDescription.ForeignKeyTableName = "";
                schema.Columns.Add(colvarStudyDescription);

                var colvarStudyDate = new TableSchema.TableColumn(schema);
                colvarStudyDate.ColumnName = "StudyDate";
                colvarStudyDate.DataType = DbType.AnsiString;
                colvarStudyDate.MaxLength = 16;
                colvarStudyDate.AutoIncrement = false;
                colvarStudyDate.IsNullable = true;
                colvarStudyDate.IsPrimaryKey = false;
                colvarStudyDate.IsForeignKey = false;
                colvarStudyDate.IsReadOnly = false;
                colvarStudyDate.DefaultSetting = @"";
                colvarStudyDate.ForeignKeyTableName = "";
                schema.Columns.Add(colvarStudyDate);

                var colvarStudyTime = new TableSchema.TableColumn(schema);
                colvarStudyTime.ColumnName = "StudyTime";
                colvarStudyTime.DataType = DbType.AnsiString;
                colvarStudyTime.MaxLength = 32;
                colvarStudyTime.AutoIncrement = false;
                colvarStudyTime.IsNullable = true;
                colvarStudyTime.IsPrimaryKey = false;
                colvarStudyTime.IsForeignKey = false;
                colvarStudyTime.IsReadOnly = false;
                colvarStudyTime.DefaultSetting = @"";
                colvarStudyTime.ForeignKeyTableName = "";
                schema.Columns.Add(colvarStudyTime);

                var colvarArchiveInfo = new TableSchema.TableColumn(schema);
                colvarArchiveInfo.ColumnName = "ArchiveInfo";
                colvarArchiveInfo.DataType = DbType.AnsiString;
                colvarArchiveInfo.MaxLength = -1;
                colvarArchiveInfo.AutoIncrement = false;
                colvarArchiveInfo.IsNullable = true;
                colvarArchiveInfo.IsPrimaryKey = false;
                colvarArchiveInfo.IsForeignKey = false;
                colvarArchiveInfo.IsReadOnly = false;
                colvarArchiveInfo.DefaultSetting = @"";
                colvarArchiveInfo.ForeignKeyTableName = "";
                schema.Columns.Add(colvarArchiveInfo);

                var colvarExtendedInfo = new TableSchema.TableColumn(schema);
                colvarExtendedInfo.ColumnName = "ExtendedInfo";
                colvarExtendedInfo.DataType = DbType.String;
                colvarExtendedInfo.MaxLength = -1;
                colvarExtendedInfo.AutoIncrement = false;
                colvarExtendedInfo.IsNullable = true;
                colvarExtendedInfo.IsPrimaryKey = false;
                colvarExtendedInfo.IsForeignKey = false;
                colvarExtendedInfo.IsReadOnly = false;
                colvarExtendedInfo.DefaultSetting = @"";
                colvarExtendedInfo.ForeignKeyTableName = "";
                schema.Columns.Add(colvarExtendedInfo);

                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("StudyDeleteRecord", schema);
            }
        }

        #endregion

        #region Props

        [XmlAttribute("Guid")]
        [Bindable(true)]
        public Guid Guid
        {
            get { return GetColumnValue<Guid>(Columns.Guid); }
            set { SetColumnValue(Columns.Guid, value); }
        }

        [XmlAttribute("Timestamp")]
        [Bindable(true)]
        public DateTime Timestamp
        {
            get { return GetColumnValue<DateTime>(Columns.Timestamp); }
            set { SetColumnValue(Columns.Timestamp, value); }
        }

        [XmlAttribute("Reason")]
        [Bindable(true)]
        public string Reason
        {
            get { return GetColumnValue<string>(Columns.Reason); }
            set { SetColumnValue(Columns.Reason, value); }
        }

        [XmlAttribute("ServerPartitionAE")]
        [Bindable(true)]
        public string ServerPartitionAE
        {
            get { return GetColumnValue<string>(Columns.ServerPartitionAE); }
            set { SetColumnValue(Columns.ServerPartitionAE, value); }
        }

        [XmlAttribute("FilesystemGUID")]
        [Bindable(true)]
        public Guid FilesystemGUID
        {
            get { return GetColumnValue<Guid>(Columns.FilesystemGUID); }
            set { SetColumnValue(Columns.FilesystemGUID, value); }
        }

        [XmlAttribute("BackupPath")]
        [Bindable(true)]
        public string BackupPath
        {
            get { return GetColumnValue<string>(Columns.BackupPath); }
            set { SetColumnValue(Columns.BackupPath, value); }
        }

        [XmlAttribute("StudyInstanceUid")]
        [Bindable(true)]
        public string StudyInstanceUid
        {
            get { return GetColumnValue<string>(Columns.StudyInstanceUid); }
            set { SetColumnValue(Columns.StudyInstanceUid, value); }
        }

        [XmlAttribute("AccessionNumber")]
        [Bindable(true)]
        public string AccessionNumber
        {
            get { return GetColumnValue<string>(Columns.AccessionNumber); }
            set { SetColumnValue(Columns.AccessionNumber, value); }
        }

        [XmlAttribute("PatientId")]
        [Bindable(true)]
        public string PatientId
        {
            get { return GetColumnValue<string>(Columns.PatientId); }
            set { SetColumnValue(Columns.PatientId, value); }
        }

        [XmlAttribute("PatientsName")]
        [Bindable(true)]
        public string PatientsName
        {
            get { return GetColumnValue<string>(Columns.PatientsName); }
            set { SetColumnValue(Columns.PatientsName, value); }
        }

        [XmlAttribute("StudyId")]
        [Bindable(true)]
        public string StudyId
        {
            get { return GetColumnValue<string>(Columns.StudyId); }
            set { SetColumnValue(Columns.StudyId, value); }
        }

        [XmlAttribute("StudyDescription")]
        [Bindable(true)]
        public string StudyDescription
        {
            get { return GetColumnValue<string>(Columns.StudyDescription); }
            set { SetColumnValue(Columns.StudyDescription, value); }
        }

        [XmlAttribute("StudyDate")]
        [Bindable(true)]
        public string StudyDate
        {
            get { return GetColumnValue<string>(Columns.StudyDate); }
            set { SetColumnValue(Columns.StudyDate, value); }
        }

        [XmlAttribute("StudyTime")]
        [Bindable(true)]
        public string StudyTime
        {
            get { return GetColumnValue<string>(Columns.StudyTime); }
            set { SetColumnValue(Columns.StudyTime, value); }
        }

        [XmlAttribute("ArchiveInfo")]
        [Bindable(true)]
        public string ArchiveInfo
        {
            get { return GetColumnValue<string>(Columns.ArchiveInfo); }
            set { SetColumnValue(Columns.ArchiveInfo, value); }
        }

        [XmlAttribute("ExtendedInfo")]
        [Bindable(true)]
        public string ExtendedInfo
        {
            get { return GetColumnValue<string>(Columns.ExtendedInfo); }
            set { SetColumnValue(Columns.ExtendedInfo, value); }
        }

        #endregion

        #region ForeignKey Properties

        /// <summary>
        /// Returns a Filesystem ActiveRecord object related to this StudyDeleteRecord
        /// 
        /// </summary>
        public Filesystem Filesystem
        {
            get { return Filesystem.FetchByID(FilesystemGUID); }
            set { SetColumnValue("FilesystemGUID", value.Guid); }
        }

        #endregion

        #region ObjectDataSource support

        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        public static void Insert(Guid varGuid, DateTime varTimestamp, string varReason, string varServerPartitionAE,
                                  Guid varFilesystemGUID, string varBackupPath, string varStudyInstanceUid,
                                  string varAccessionNumber, string varPatientId, string varPatientsName,
                                  string varStudyId, string varStudyDescription, string varStudyDate,
                                  string varStudyTime, string varArchiveInfo, string varExtendedInfo)
        {
            var item = new StudyDeleteRecord();

            item.Guid = varGuid;

            item.Timestamp = varTimestamp;

            item.Reason = varReason;

            item.ServerPartitionAE = varServerPartitionAE;

            item.FilesystemGUID = varFilesystemGUID;

            item.BackupPath = varBackupPath;

            item.StudyInstanceUid = varStudyInstanceUid;

            item.AccessionNumber = varAccessionNumber;

            item.PatientId = varPatientId;

            item.PatientsName = varPatientsName;

            item.StudyId = varStudyId;

            item.StudyDescription = varStudyDescription;

            item.StudyDate = varStudyDate;

            item.StudyTime = varStudyTime;

            item.ArchiveInfo = varArchiveInfo;

            item.ExtendedInfo = varExtendedInfo;


            if (HttpContext.Current != null)
                item.Save(HttpContext.Current.User.Identity.Name);
            else
                item.Save(Thread.CurrentPrincipal.Identity.Name);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        public static void Update(Guid varGuid, DateTime varTimestamp, string varReason, string varServerPartitionAE,
                                  Guid varFilesystemGUID, string varBackupPath, string varStudyInstanceUid,
                                  string varAccessionNumber, string varPatientId, string varPatientsName,
                                  string varStudyId, string varStudyDescription, string varStudyDate,
                                  string varStudyTime, string varArchiveInfo, string varExtendedInfo)
        {
            var item = new StudyDeleteRecord();

            item.Guid = varGuid;

            item.Timestamp = varTimestamp;

            item.Reason = varReason;

            item.ServerPartitionAE = varServerPartitionAE;

            item.FilesystemGUID = varFilesystemGUID;

            item.BackupPath = varBackupPath;

            item.StudyInstanceUid = varStudyInstanceUid;

            item.AccessionNumber = varAccessionNumber;

            item.PatientId = varPatientId;

            item.PatientsName = varPatientsName;

            item.StudyId = varStudyId;

            item.StudyDescription = varStudyDescription;

            item.StudyDate = varStudyDate;

            item.StudyTime = varStudyTime;

            item.ArchiveInfo = varArchiveInfo;

            item.ExtendedInfo = varExtendedInfo;

            item.IsNew = false;
            if (HttpContext.Current != null)
                item.Save(HttpContext.Current.User.Identity.Name);
            else
                item.Save(Thread.CurrentPrincipal.Identity.Name);
        }

        #endregion

        #region Typed Columns

        public static TableSchema.TableColumn GuidColumn
        {
            get { return Schema.Columns[0]; }
        }


        public static TableSchema.TableColumn TimestampColumn
        {
            get { return Schema.Columns[1]; }
        }


        public static TableSchema.TableColumn ReasonColumn
        {
            get { return Schema.Columns[2]; }
        }


        public static TableSchema.TableColumn ServerPartitionAEColumn
        {
            get { return Schema.Columns[3]; }
        }


        public static TableSchema.TableColumn FilesystemGUIDColumn
        {
            get { return Schema.Columns[4]; }
        }


        public static TableSchema.TableColumn BackupPathColumn
        {
            get { return Schema.Columns[5]; }
        }


        public static TableSchema.TableColumn StudyInstanceUidColumn
        {
            get { return Schema.Columns[6]; }
        }


        public static TableSchema.TableColumn AccessionNumberColumn
        {
            get { return Schema.Columns[7]; }
        }


        public static TableSchema.TableColumn PatientIdColumn
        {
            get { return Schema.Columns[8]; }
        }


        public static TableSchema.TableColumn PatientsNameColumn
        {
            get { return Schema.Columns[9]; }
        }


        public static TableSchema.TableColumn StudyIdColumn
        {
            get { return Schema.Columns[10]; }
        }


        public static TableSchema.TableColumn StudyDescriptionColumn
        {
            get { return Schema.Columns[11]; }
        }


        public static TableSchema.TableColumn StudyDateColumn
        {
            get { return Schema.Columns[12]; }
        }


        public static TableSchema.TableColumn StudyTimeColumn
        {
            get { return Schema.Columns[13]; }
        }


        public static TableSchema.TableColumn ArchiveInfoColumn
        {
            get { return Schema.Columns[14]; }
        }


        public static TableSchema.TableColumn ExtendedInfoColumn
        {
            get { return Schema.Columns[15]; }
        }

        #endregion

        #region Columns Struct

        public struct Columns
        {
            public static string Guid = @"GUID";
            public static string Timestamp = @"Timestamp";
            public static string Reason = @"Reason";
            public static string ServerPartitionAE = @"ServerPartitionAE";
            public static string FilesystemGUID = @"FilesystemGUID";
            public static string BackupPath = @"BackupPath";
            public static string StudyInstanceUid = @"StudyInstanceUid";
            public static string AccessionNumber = @"AccessionNumber";
            public static string PatientId = @"PatientId";
            public static string PatientsName = @"PatientsName";
            public static string StudyId = @"StudyId";
            public static string StudyDescription = @"StudyDescription";
            public static string StudyDate = @"StudyDate";
            public static string StudyTime = @"StudyTime";
            public static string ArchiveInfo = @"ArchiveInfo";
            public static string ExtendedInfo = @"ExtendedInfo";
        }

        #endregion

        #region Update PK Collections

        #endregion

        #region Deep Save

        #endregion

        //no ManyToMany tables defined (0)
    }
}