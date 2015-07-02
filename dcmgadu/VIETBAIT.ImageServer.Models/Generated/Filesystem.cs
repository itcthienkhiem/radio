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
    /// Strongly-typed collection for the Filesystem class.
    /// </summary>
    [Serializable]
    public class FilesystemCollection : ActiveList<Filesystem, FilesystemCollection>
    {
        /// <summary>
        /// Filters an existing collection based on the set criteria. This is an in-memory filter
        /// Thanks to developingchris for this!
        /// </summary>
        /// <returns>FilesystemCollection</returns>
        public FilesystemCollection Filter()
        {
            for (int i = Count - 1; i > -1; i--)
            {
                Filesystem o = this[i];
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
    /// This is an ActiveRecord class which wraps the Filesystem table.
    /// </summary>
    [Serializable]
    public class Filesystem : ActiveRecord<Filesystem>, IActiveRecord
    {
        #region .ctors and Default Settings

        public Filesystem()
        {
            SetSQLProps();
            InitSetDefaults();
            MarkNew();
        }

        public Filesystem(bool useDatabaseDefaults)
        {
            SetSQLProps();
            if (useDatabaseDefaults)
                ForceDefaults();
            MarkNew();
        }

        public Filesystem(object keyID)
        {
            SetSQLProps();
            InitSetDefaults();
            LoadByKey(keyID);
        }

        public Filesystem(string columnName, object columnValue)
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
                var schema = new TableSchema.Table("Filesystem", TableType.Table, DataService.GetInstance("ORM"));
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

                var colvarFilesystemPath = new TableSchema.TableColumn(schema);
                colvarFilesystemPath.ColumnName = "FilesystemPath";
                colvarFilesystemPath.DataType = DbType.String;
                colvarFilesystemPath.MaxLength = 256;
                colvarFilesystemPath.AutoIncrement = false;
                colvarFilesystemPath.IsNullable = false;
                colvarFilesystemPath.IsPrimaryKey = false;
                colvarFilesystemPath.IsForeignKey = false;
                colvarFilesystemPath.IsReadOnly = false;
                colvarFilesystemPath.DefaultSetting = @"";
                colvarFilesystemPath.ForeignKeyTableName = "";
                schema.Columns.Add(colvarFilesystemPath);

                var colvarEnabled = new TableSchema.TableColumn(schema);
                colvarEnabled.ColumnName = "Enabled";
                colvarEnabled.DataType = DbType.Boolean;
                colvarEnabled.MaxLength = 0;
                colvarEnabled.AutoIncrement = false;
                colvarEnabled.IsNullable = false;
                colvarEnabled.IsPrimaryKey = false;
                colvarEnabled.IsForeignKey = false;
                colvarEnabled.IsReadOnly = false;
                colvarEnabled.DefaultSetting = @"";
                colvarEnabled.ForeignKeyTableName = "";
                schema.Columns.Add(colvarEnabled);

                var colvarReadOnlyX = new TableSchema.TableColumn(schema);
                colvarReadOnlyX.ColumnName = "ReadOnly";
                colvarReadOnlyX.DataType = DbType.Boolean;
                colvarReadOnlyX.MaxLength = 0;
                colvarReadOnlyX.AutoIncrement = false;
                colvarReadOnlyX.IsNullable = false;
                colvarReadOnlyX.IsPrimaryKey = false;
                colvarReadOnlyX.IsForeignKey = false;
                colvarReadOnlyX.IsReadOnly = false;
                colvarReadOnlyX.DefaultSetting = @"";
                colvarReadOnlyX.ForeignKeyTableName = "";
                schema.Columns.Add(colvarReadOnlyX);

                var colvarWriteOnlyX = new TableSchema.TableColumn(schema);
                colvarWriteOnlyX.ColumnName = "WriteOnly";
                colvarWriteOnlyX.DataType = DbType.Boolean;
                colvarWriteOnlyX.MaxLength = 0;
                colvarWriteOnlyX.AutoIncrement = false;
                colvarWriteOnlyX.IsNullable = false;
                colvarWriteOnlyX.IsPrimaryKey = false;
                colvarWriteOnlyX.IsForeignKey = false;
                colvarWriteOnlyX.IsReadOnly = false;
                colvarWriteOnlyX.DefaultSetting = @"";
                colvarWriteOnlyX.ForeignKeyTableName = "";
                schema.Columns.Add(colvarWriteOnlyX);

                var colvarDescription = new TableSchema.TableColumn(schema);
                colvarDescription.ColumnName = "Description";
                colvarDescription.DataType = DbType.String;
                colvarDescription.MaxLength = 128;
                colvarDescription.AutoIncrement = false;
                colvarDescription.IsNullable = true;
                colvarDescription.IsPrimaryKey = false;
                colvarDescription.IsForeignKey = false;
                colvarDescription.IsReadOnly = false;
                colvarDescription.DefaultSetting = @"";
                colvarDescription.ForeignKeyTableName = "";
                schema.Columns.Add(colvarDescription);

                var colvarFilesystemTierEnum = new TableSchema.TableColumn(schema);
                colvarFilesystemTierEnum.ColumnName = "FilesystemTierEnum";
                colvarFilesystemTierEnum.DataType = DbType.Int16;
                colvarFilesystemTierEnum.MaxLength = 0;
                colvarFilesystemTierEnum.AutoIncrement = false;
                colvarFilesystemTierEnum.IsNullable = false;
                colvarFilesystemTierEnum.IsPrimaryKey = false;
                colvarFilesystemTierEnum.IsForeignKey = true;
                colvarFilesystemTierEnum.IsReadOnly = false;
                colvarFilesystemTierEnum.DefaultSetting = @"";

                colvarFilesystemTierEnum.ForeignKeyTableName = "FilesystemTierEnum";
                schema.Columns.Add(colvarFilesystemTierEnum);

                var colvarLowWatermark = new TableSchema.TableColumn(schema);
                colvarLowWatermark.ColumnName = "LowWatermark";
                colvarLowWatermark.DataType = DbType.Decimal;
                colvarLowWatermark.MaxLength = 0;
                colvarLowWatermark.AutoIncrement = false;
                colvarLowWatermark.IsNullable = false;
                colvarLowWatermark.IsPrimaryKey = false;
                colvarLowWatermark.IsForeignKey = false;
                colvarLowWatermark.IsReadOnly = false;

                colvarLowWatermark.DefaultSetting = @"((80.00))";
                colvarLowWatermark.ForeignKeyTableName = "";
                schema.Columns.Add(colvarLowWatermark);

                var colvarHighWatermark = new TableSchema.TableColumn(schema);
                colvarHighWatermark.ColumnName = "HighWatermark";
                colvarHighWatermark.DataType = DbType.Decimal;
                colvarHighWatermark.MaxLength = 0;
                colvarHighWatermark.AutoIncrement = false;
                colvarHighWatermark.IsNullable = false;
                colvarHighWatermark.IsPrimaryKey = false;
                colvarHighWatermark.IsForeignKey = false;
                colvarHighWatermark.IsReadOnly = false;

                colvarHighWatermark.DefaultSetting = @"((90.00))";
                colvarHighWatermark.ForeignKeyTableName = "";
                schema.Columns.Add(colvarHighWatermark);

                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("Filesystem", schema);
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

        [XmlAttribute("FilesystemPath")]
        [Bindable(true)]
        public string FilesystemPath
        {
            get { return GetColumnValue<string>(Columns.FilesystemPath); }
            set { SetColumnValue(Columns.FilesystemPath, value); }
        }

        [XmlAttribute("Enabled")]
        [Bindable(true)]
        public bool Enabled
        {
            get { return GetColumnValue<bool>(Columns.Enabled); }
            set { SetColumnValue(Columns.Enabled, value); }
        }

        [XmlAttribute("ReadOnlyX")]
        [Bindable(true)]
        public bool ReadOnlyX
        {
            get { return GetColumnValue<bool>(Columns.ReadOnlyX); }
            set { SetColumnValue(Columns.ReadOnlyX, value); }
        }

        [XmlAttribute("WriteOnlyX")]
        [Bindable(true)]
        public bool WriteOnlyX
        {
            get { return GetColumnValue<bool>(Columns.WriteOnlyX); }
            set { SetColumnValue(Columns.WriteOnlyX, value); }
        }

        [XmlAttribute("Description")]
        [Bindable(true)]
        public string Description
        {
            get { return GetColumnValue<string>(Columns.Description); }
            set { SetColumnValue(Columns.Description, value); }
        }

        [XmlAttribute("FilesystemTierEnum")]
        [Bindable(true)]
        public short FilesystemTierEnum
        {
            get { return GetColumnValue<short>(Columns.FilesystemTierEnum); }
            set { SetColumnValue(Columns.FilesystemTierEnum, value); }
        }

        [XmlAttribute("LowWatermark")]
        [Bindable(true)]
        public decimal LowWatermark
        {
            get { return GetColumnValue<decimal>(Columns.LowWatermark); }
            set { SetColumnValue(Columns.LowWatermark, value); }
        }

        [XmlAttribute("HighWatermark")]
        [Bindable(true)]
        public decimal HighWatermark
        {
            get { return GetColumnValue<decimal>(Columns.HighWatermark); }
            set { SetColumnValue(Columns.HighWatermark, value); }
        }

        #endregion

        #region PrimaryKey Methods		

        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);

            SetPKValues();
        }


        public FilesystemQueueCollection FilesystemQueueRecords()
        {
            return new FilesystemQueueCollection().Where(FilesystemQueue.Columns.FilesystemGUID, Guid).Load();
        }

        public FilesystemStudyStorageCollection FilesystemStudyStorageRecords()
        {
            return
                new FilesystemStudyStorageCollection().Where(FilesystemStudyStorage.Columns.FilesystemGUID, Guid).Load();
        }

        public ServiceLockCollection ServiceLockRecords()
        {
            return new ServiceLockCollection().Where(ServiceLock.Columns.FilesystemGUID, Guid).Load();
        }

        public StudyDeleteRecordCollection StudyDeleteRecordRecords()
        {
            return new StudyDeleteRecordCollection().Where(StudyDeleteRecord.Columns.FilesystemGUID, Guid).Load();
        }

        #endregion

        #region ForeignKey Properties

        /// <summary>
        /// Returns a FilesystemTierEnum ActiveRecord object related to this Filesystem
        /// 
        /// </summary>
        public FilesystemTierEnum FilesystemTierEnumRecord
        {
            get { return Models.FilesystemTierEnum.FetchByID(FilesystemTierEnum); }
            set { SetColumnValue("FilesystemTierEnum", value.EnumX); }
        }

        #endregion

        #region ObjectDataSource support

        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        public static void Insert(Guid varGuid, string varFilesystemPath, bool varEnabled, bool varReadOnlyX,
                                  bool varWriteOnlyX, string varDescription, short varFilesystemTierEnum,
                                  decimal varLowWatermark, decimal varHighWatermark)
        {
            var item = new Filesystem();

            item.Guid = varGuid;

            item.FilesystemPath = varFilesystemPath;

            item.Enabled = varEnabled;

            item.ReadOnlyX = varReadOnlyX;

            item.WriteOnlyX = varWriteOnlyX;

            item.Description = varDescription;

            item.FilesystemTierEnum = varFilesystemTierEnum;

            item.LowWatermark = varLowWatermark;

            item.HighWatermark = varHighWatermark;


            if (HttpContext.Current != null)
                item.Save(HttpContext.Current.User.Identity.Name);
            else
                item.Save(Thread.CurrentPrincipal.Identity.Name);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        public static void Update(Guid varGuid, string varFilesystemPath, bool varEnabled, bool varReadOnlyX,
                                  bool varWriteOnlyX, string varDescription, short varFilesystemTierEnum,
                                  decimal varLowWatermark, decimal varHighWatermark)
        {
            var item = new Filesystem();

            item.Guid = varGuid;

            item.FilesystemPath = varFilesystemPath;

            item.Enabled = varEnabled;

            item.ReadOnlyX = varReadOnlyX;

            item.WriteOnlyX = varWriteOnlyX;

            item.Description = varDescription;

            item.FilesystemTierEnum = varFilesystemTierEnum;

            item.LowWatermark = varLowWatermark;

            item.HighWatermark = varHighWatermark;

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


        public static TableSchema.TableColumn FilesystemPathColumn
        {
            get { return Schema.Columns[1]; }
        }


        public static TableSchema.TableColumn EnabledColumn
        {
            get { return Schema.Columns[2]; }
        }


        public static TableSchema.TableColumn ReadOnlyXColumn
        {
            get { return Schema.Columns[3]; }
        }


        public static TableSchema.TableColumn WriteOnlyXColumn
        {
            get { return Schema.Columns[4]; }
        }


        public static TableSchema.TableColumn DescriptionColumn
        {
            get { return Schema.Columns[5]; }
        }


        public static TableSchema.TableColumn FilesystemTierEnumColumn
        {
            get { return Schema.Columns[6]; }
        }


        public static TableSchema.TableColumn LowWatermarkColumn
        {
            get { return Schema.Columns[7]; }
        }


        public static TableSchema.TableColumn HighWatermarkColumn
        {
            get { return Schema.Columns[8]; }
        }

        #endregion

        #region Columns Struct

        public struct Columns
        {
            public static string Guid = @"GUID";
            public static string FilesystemPath = @"FilesystemPath";
            public static string Enabled = @"Enabled";
            public static string ReadOnlyX = @"ReadOnly";
            public static string WriteOnlyX = @"WriteOnly";
            public static string Description = @"Description";
            public static string FilesystemTierEnum = @"FilesystemTierEnum";
            public static string LowWatermark = @"LowWatermark";
            public static string HighWatermark = @"HighWatermark";
        }

        #endregion

        #region Update PK Collections

        public void SetPKValues()
        {
        }

        #endregion

        #region Deep Save

        public void DeepSave()
        {
            Save();
        }

        #endregion

        //no ManyToMany tables defined (0)
    }
}