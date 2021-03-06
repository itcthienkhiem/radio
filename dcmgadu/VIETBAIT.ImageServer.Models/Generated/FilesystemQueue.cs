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
    /// Strongly-typed collection for the FilesystemQueue class.
    /// </summary>
    [Serializable]
    public class FilesystemQueueCollection : ActiveList<FilesystemQueue, FilesystemQueueCollection>
    {
        /// <summary>
        /// Filters an existing collection based on the set criteria. This is an in-memory filter
        /// Thanks to developingchris for this!
        /// </summary>
        /// <returns>FilesystemQueueCollection</returns>
        public FilesystemQueueCollection Filter()
        {
            for (int i = Count - 1; i > -1; i--)
            {
                FilesystemQueue o = this[i];
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
    /// This is an ActiveRecord class which wraps the FilesystemQueue table.
    /// </summary>
    [Serializable]
    public class FilesystemQueue : ActiveRecord<FilesystemQueue>, IActiveRecord
    {
        #region .ctors and Default Settings

        public FilesystemQueue()
        {
            SetSQLProps();
            InitSetDefaults();
            MarkNew();
        }

        public FilesystemQueue(bool useDatabaseDefaults)
        {
            SetSQLProps();
            if (useDatabaseDefaults)
                ForceDefaults();
            MarkNew();
        }

        public FilesystemQueue(object keyID)
        {
            SetSQLProps();
            InitSetDefaults();
            LoadByKey(keyID);
        }

        public FilesystemQueue(string columnName, object columnValue)
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
                var schema = new TableSchema.Table("FilesystemQueue", TableType.Table, DataService.GetInstance("ORM"));
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

                var colvarFilesystemQueueTypeEnum = new TableSchema.TableColumn(schema);
                colvarFilesystemQueueTypeEnum.ColumnName = "FilesystemQueueTypeEnum";
                colvarFilesystemQueueTypeEnum.DataType = DbType.Int16;
                colvarFilesystemQueueTypeEnum.MaxLength = 0;
                colvarFilesystemQueueTypeEnum.AutoIncrement = false;
                colvarFilesystemQueueTypeEnum.IsNullable = false;
                colvarFilesystemQueueTypeEnum.IsPrimaryKey = false;
                colvarFilesystemQueueTypeEnum.IsForeignKey = true;
                colvarFilesystemQueueTypeEnum.IsReadOnly = false;
                colvarFilesystemQueueTypeEnum.DefaultSetting = @"";

                colvarFilesystemQueueTypeEnum.ForeignKeyTableName = "FilesystemQueueTypeEnum";
                schema.Columns.Add(colvarFilesystemQueueTypeEnum);

                var colvarStudyStorageGUID = new TableSchema.TableColumn(schema);
                colvarStudyStorageGUID.ColumnName = "StudyStorageGUID";
                colvarStudyStorageGUID.DataType = DbType.Guid;
                colvarStudyStorageGUID.MaxLength = 0;
                colvarStudyStorageGUID.AutoIncrement = false;
                colvarStudyStorageGUID.IsNullable = false;
                colvarStudyStorageGUID.IsPrimaryKey = false;
                colvarStudyStorageGUID.IsForeignKey = true;
                colvarStudyStorageGUID.IsReadOnly = false;
                colvarStudyStorageGUID.DefaultSetting = @"";

                colvarStudyStorageGUID.ForeignKeyTableName = "StudyStorage";
                schema.Columns.Add(colvarStudyStorageGUID);

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

                var colvarScheduledTime = new TableSchema.TableColumn(schema);
                colvarScheduledTime.ColumnName = "ScheduledTime";
                colvarScheduledTime.DataType = DbType.DateTime;
                colvarScheduledTime.MaxLength = 0;
                colvarScheduledTime.AutoIncrement = false;
                colvarScheduledTime.IsNullable = false;
                colvarScheduledTime.IsPrimaryKey = false;
                colvarScheduledTime.IsForeignKey = false;
                colvarScheduledTime.IsReadOnly = false;
                colvarScheduledTime.DefaultSetting = @"";
                colvarScheduledTime.ForeignKeyTableName = "";
                schema.Columns.Add(colvarScheduledTime);

                var colvarSeriesInstanceUid = new TableSchema.TableColumn(schema);
                colvarSeriesInstanceUid.ColumnName = "SeriesInstanceUid";
                colvarSeriesInstanceUid.DataType = DbType.AnsiString;
                colvarSeriesInstanceUid.MaxLength = 64;
                colvarSeriesInstanceUid.AutoIncrement = false;
                colvarSeriesInstanceUid.IsNullable = true;
                colvarSeriesInstanceUid.IsPrimaryKey = false;
                colvarSeriesInstanceUid.IsForeignKey = false;
                colvarSeriesInstanceUid.IsReadOnly = false;
                colvarSeriesInstanceUid.DefaultSetting = @"";
                colvarSeriesInstanceUid.ForeignKeyTableName = "";
                schema.Columns.Add(colvarSeriesInstanceUid);

                var colvarQueueXml = new TableSchema.TableColumn(schema);
                colvarQueueXml.ColumnName = "QueueXml";
                colvarQueueXml.DataType = DbType.AnsiString;
                colvarQueueXml.MaxLength = -1;
                colvarQueueXml.AutoIncrement = false;
                colvarQueueXml.IsNullable = true;
                colvarQueueXml.IsPrimaryKey = false;
                colvarQueueXml.IsForeignKey = false;
                colvarQueueXml.IsReadOnly = false;
                colvarQueueXml.DefaultSetting = @"";
                colvarQueueXml.ForeignKeyTableName = "";
                schema.Columns.Add(colvarQueueXml);

                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("FilesystemQueue", schema);
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

        [XmlAttribute("FilesystemQueueTypeEnum")]
        [Bindable(true)]
        public short FilesystemQueueTypeEnum
        {
            get { return GetColumnValue<short>(Columns.FilesystemQueueTypeEnum); }
            set { SetColumnValue(Columns.FilesystemQueueTypeEnum, value); }
        }

        [XmlAttribute("StudyStorageGUID")]
        [Bindable(true)]
        public Guid StudyStorageGUID
        {
            get { return GetColumnValue<Guid>(Columns.StudyStorageGUID); }
            set { SetColumnValue(Columns.StudyStorageGUID, value); }
        }

        [XmlAttribute("FilesystemGUID")]
        [Bindable(true)]
        public Guid FilesystemGUID
        {
            get { return GetColumnValue<Guid>(Columns.FilesystemGUID); }
            set { SetColumnValue(Columns.FilesystemGUID, value); }
        }

        [XmlAttribute("ScheduledTime")]
        [Bindable(true)]
        public DateTime ScheduledTime
        {
            get { return GetColumnValue<DateTime>(Columns.ScheduledTime); }
            set { SetColumnValue(Columns.ScheduledTime, value); }
        }

        [XmlAttribute("SeriesInstanceUid")]
        [Bindable(true)]
        public string SeriesInstanceUid
        {
            get { return GetColumnValue<string>(Columns.SeriesInstanceUid); }
            set { SetColumnValue(Columns.SeriesInstanceUid, value); }
        }

        [XmlAttribute("QueueXml")]
        [Bindable(true)]
        public string QueueXml
        {
            get { return GetColumnValue<string>(Columns.QueueXml); }
            set { SetColumnValue(Columns.QueueXml, value); }
        }

        #endregion

        #region ForeignKey Properties

        /// <summary>
        /// Returns a Filesystem ActiveRecord object related to this FilesystemQueue
        /// 
        /// </summary>
        public Filesystem Filesystem
        {
            get { return Filesystem.FetchByID(FilesystemGUID); }
            set { SetColumnValue("FilesystemGUID", value.Guid); }
        }


        /// <summary>
        /// Returns a FilesystemQueueTypeEnum ActiveRecord object related to this FilesystemQueue
        /// 
        /// </summary>
        public FilesystemQueueTypeEnum FilesystemQueueTypeEnumRecord
        {
            get { return Models.FilesystemQueueTypeEnum.FetchByID(FilesystemQueueTypeEnum); }
            set { SetColumnValue("FilesystemQueueTypeEnum", value.EnumX); }
        }


        /// <summary>
        /// Returns a StudyStorage ActiveRecord object related to this FilesystemQueue
        /// 
        /// </summary>
        public StudyStorage StudyStorage
        {
            get { return StudyStorage.FetchByID(StudyStorageGUID); }
            set { SetColumnValue("StudyStorageGUID", value.Guid); }
        }

        #endregion

        #region ObjectDataSource support

        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        public static void Insert(Guid varGuid, short varFilesystemQueueTypeEnum, Guid varStudyStorageGUID,
                                  Guid varFilesystemGUID, DateTime varScheduledTime, string varSeriesInstanceUid,
                                  string varQueueXml)
        {
            var item = new FilesystemQueue();

            item.Guid = varGuid;

            item.FilesystemQueueTypeEnum = varFilesystemQueueTypeEnum;

            item.StudyStorageGUID = varStudyStorageGUID;

            item.FilesystemGUID = varFilesystemGUID;

            item.ScheduledTime = varScheduledTime;

            item.SeriesInstanceUid = varSeriesInstanceUid;

            item.QueueXml = varQueueXml;


            if (HttpContext.Current != null)
                item.Save(HttpContext.Current.User.Identity.Name);
            else
                item.Save(Thread.CurrentPrincipal.Identity.Name);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        public static void Update(Guid varGuid, short varFilesystemQueueTypeEnum, Guid varStudyStorageGUID,
                                  Guid varFilesystemGUID, DateTime varScheduledTime, string varSeriesInstanceUid,
                                  string varQueueXml)
        {
            var item = new FilesystemQueue();

            item.Guid = varGuid;

            item.FilesystemQueueTypeEnum = varFilesystemQueueTypeEnum;

            item.StudyStorageGUID = varStudyStorageGUID;

            item.FilesystemGUID = varFilesystemGUID;

            item.ScheduledTime = varScheduledTime;

            item.SeriesInstanceUid = varSeriesInstanceUid;

            item.QueueXml = varQueueXml;

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


        public static TableSchema.TableColumn FilesystemQueueTypeEnumColumn
        {
            get { return Schema.Columns[1]; }
        }


        public static TableSchema.TableColumn StudyStorageGUIDColumn
        {
            get { return Schema.Columns[2]; }
        }


        public static TableSchema.TableColumn FilesystemGUIDColumn
        {
            get { return Schema.Columns[3]; }
        }


        public static TableSchema.TableColumn ScheduledTimeColumn
        {
            get { return Schema.Columns[4]; }
        }


        public static TableSchema.TableColumn SeriesInstanceUidColumn
        {
            get { return Schema.Columns[5]; }
        }


        public static TableSchema.TableColumn QueueXmlColumn
        {
            get { return Schema.Columns[6]; }
        }

        #endregion

        #region Columns Struct

        public struct Columns
        {
            public static string Guid = @"GUID";
            public static string FilesystemQueueTypeEnum = @"FilesystemQueueTypeEnum";
            public static string StudyStorageGUID = @"StudyStorageGUID";
            public static string FilesystemGUID = @"FilesystemGUID";
            public static string ScheduledTime = @"ScheduledTime";
            public static string SeriesInstanceUid = @"SeriesInstanceUid";
            public static string QueueXml = @"QueueXml";
        }

        #endregion

        #region Update PK Collections

        #endregion

        #region Deep Save

        #endregion

        //no ManyToMany tables defined (0)
    }
}