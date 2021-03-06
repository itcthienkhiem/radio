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
    /// Strongly-typed collection for the StudyStorage class.
    /// </summary>
    [Serializable]
    public class StudyStorageCollection : ActiveList<StudyStorage, StudyStorageCollection>
    {
        /// <summary>
        /// Filters an existing collection based on the set criteria. This is an in-memory filter
        /// Thanks to developingchris for this!
        /// </summary>
        /// <returns>StudyStorageCollection</returns>
        public StudyStorageCollection Filter()
        {
            for (int i = Count - 1; i > -1; i--)
            {
                StudyStorage o = this[i];
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
    /// This is an ActiveRecord class which wraps the StudyStorage table.
    /// </summary>
    [Serializable]
    public class StudyStorage : ActiveRecord<StudyStorage>, IActiveRecord
    {
        #region .ctors and Default Settings

        public StudyStorage()
        {
            SetSQLProps();
            InitSetDefaults();
            MarkNew();
        }

        public StudyStorage(bool useDatabaseDefaults)
        {
            SetSQLProps();
            if (useDatabaseDefaults)
                ForceDefaults();
            MarkNew();
        }

        public StudyStorage(object keyID)
        {
            SetSQLProps();
            InitSetDefaults();
            LoadByKey(keyID);
        }

        public StudyStorage(string columnName, object columnValue)
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
                var schema = new TableSchema.Table("StudyStorage", TableType.Table, DataService.GetInstance("ORM"));
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

                var colvarServerPartitionGUID = new TableSchema.TableColumn(schema);
                colvarServerPartitionGUID.ColumnName = "ServerPartitionGUID";
                colvarServerPartitionGUID.DataType = DbType.Guid;
                colvarServerPartitionGUID.MaxLength = 0;
                colvarServerPartitionGUID.AutoIncrement = false;
                colvarServerPartitionGUID.IsNullable = false;
                colvarServerPartitionGUID.IsPrimaryKey = false;
                colvarServerPartitionGUID.IsForeignKey = true;
                colvarServerPartitionGUID.IsReadOnly = false;
                colvarServerPartitionGUID.DefaultSetting = @"";

                colvarServerPartitionGUID.ForeignKeyTableName = "ServerPartition";
                schema.Columns.Add(colvarServerPartitionGUID);

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

                var colvarInsertTime = new TableSchema.TableColumn(schema);
                colvarInsertTime.ColumnName = "InsertTime";
                colvarInsertTime.DataType = DbType.DateTime;
                colvarInsertTime.MaxLength = 0;
                colvarInsertTime.AutoIncrement = false;
                colvarInsertTime.IsNullable = false;
                colvarInsertTime.IsPrimaryKey = false;
                colvarInsertTime.IsForeignKey = false;
                colvarInsertTime.IsReadOnly = false;

                colvarInsertTime.DefaultSetting = @"(getdate())";
                colvarInsertTime.ForeignKeyTableName = "";
                schema.Columns.Add(colvarInsertTime);

                var colvarLastAccessedTime = new TableSchema.TableColumn(schema);
                colvarLastAccessedTime.ColumnName = "LastAccessedTime";
                colvarLastAccessedTime.DataType = DbType.DateTime;
                colvarLastAccessedTime.MaxLength = 0;
                colvarLastAccessedTime.AutoIncrement = false;
                colvarLastAccessedTime.IsNullable = false;
                colvarLastAccessedTime.IsPrimaryKey = false;
                colvarLastAccessedTime.IsForeignKey = false;
                colvarLastAccessedTime.IsReadOnly = false;

                colvarLastAccessedTime.DefaultSetting = @"(getdate())";
                colvarLastAccessedTime.ForeignKeyTableName = "";
                schema.Columns.Add(colvarLastAccessedTime);

                var colvarWriteLock = new TableSchema.TableColumn(schema);
                colvarWriteLock.ColumnName = "WriteLock";
                colvarWriteLock.DataType = DbType.Boolean;
                colvarWriteLock.MaxLength = 0;
                colvarWriteLock.AutoIncrement = false;
                colvarWriteLock.IsNullable = false;
                colvarWriteLock.IsPrimaryKey = false;
                colvarWriteLock.IsForeignKey = false;
                colvarWriteLock.IsReadOnly = false;

                colvarWriteLock.DefaultSetting = @"((0))";
                colvarWriteLock.ForeignKeyTableName = "";
                schema.Columns.Add(colvarWriteLock);

                var colvarReadLock = new TableSchema.TableColumn(schema);
                colvarReadLock.ColumnName = "ReadLock";
                colvarReadLock.DataType = DbType.Int16;
                colvarReadLock.MaxLength = 0;
                colvarReadLock.AutoIncrement = false;
                colvarReadLock.IsNullable = false;
                colvarReadLock.IsPrimaryKey = false;
                colvarReadLock.IsForeignKey = false;
                colvarReadLock.IsReadOnly = false;

                colvarReadLock.DefaultSetting = @"((0))";
                colvarReadLock.ForeignKeyTableName = "";
                schema.Columns.Add(colvarReadLock);

                var colvarStudyStatusEnum = new TableSchema.TableColumn(schema);
                colvarStudyStatusEnum.ColumnName = "StudyStatusEnum";
                colvarStudyStatusEnum.DataType = DbType.Int16;
                colvarStudyStatusEnum.MaxLength = 0;
                colvarStudyStatusEnum.AutoIncrement = false;
                colvarStudyStatusEnum.IsNullable = false;
                colvarStudyStatusEnum.IsPrimaryKey = false;
                colvarStudyStatusEnum.IsForeignKey = true;
                colvarStudyStatusEnum.IsReadOnly = false;
                colvarStudyStatusEnum.DefaultSetting = @"";

                colvarStudyStatusEnum.ForeignKeyTableName = "StudyStatusEnum";
                schema.Columns.Add(colvarStudyStatusEnum);

                var colvarQueueStudyStateEnum = new TableSchema.TableColumn(schema);
                colvarQueueStudyStateEnum.ColumnName = "QueueStudyStateEnum";
                colvarQueueStudyStateEnum.DataType = DbType.Int16;
                colvarQueueStudyStateEnum.MaxLength = 0;
                colvarQueueStudyStateEnum.AutoIncrement = false;
                colvarQueueStudyStateEnum.IsNullable = false;
                colvarQueueStudyStateEnum.IsPrimaryKey = false;
                colvarQueueStudyStateEnum.IsForeignKey = true;
                colvarQueueStudyStateEnum.IsReadOnly = false;
                colvarQueueStudyStateEnum.DefaultSetting = @"";

                colvarQueueStudyStateEnum.ForeignKeyTableName = "QueueStudyStateEnum";
                schema.Columns.Add(colvarQueueStudyStateEnum);

                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("StudyStorage", schema);
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

        [XmlAttribute("ServerPartitionGUID")]
        [Bindable(true)]
        public Guid ServerPartitionGUID
        {
            get { return GetColumnValue<Guid>(Columns.ServerPartitionGUID); }
            set { SetColumnValue(Columns.ServerPartitionGUID, value); }
        }

        [XmlAttribute("StudyInstanceUid")]
        [Bindable(true)]
        public string StudyInstanceUid
        {
            get { return GetColumnValue<string>(Columns.StudyInstanceUid); }
            set { SetColumnValue(Columns.StudyInstanceUid, value); }
        }

        [XmlAttribute("InsertTime")]
        [Bindable(true)]
        public DateTime InsertTime
        {
            get { return GetColumnValue<DateTime>(Columns.InsertTime); }
            set { SetColumnValue(Columns.InsertTime, value); }
        }

        [XmlAttribute("LastAccessedTime")]
        [Bindable(true)]
        public DateTime LastAccessedTime
        {
            get { return GetColumnValue<DateTime>(Columns.LastAccessedTime); }
            set { SetColumnValue(Columns.LastAccessedTime, value); }
        }

        [XmlAttribute("WriteLock")]
        [Bindable(true)]
        public bool WriteLock
        {
            get { return GetColumnValue<bool>(Columns.WriteLock); }
            set { SetColumnValue(Columns.WriteLock, value); }
        }

        [XmlAttribute("ReadLock")]
        [Bindable(true)]
        public short ReadLock
        {
            get { return GetColumnValue<short>(Columns.ReadLock); }
            set { SetColumnValue(Columns.ReadLock, value); }
        }

        [XmlAttribute("StudyStatusEnum")]
        [Bindable(true)]
        public short StudyStatusEnum
        {
            get { return GetColumnValue<short>(Columns.StudyStatusEnum); }
            set { SetColumnValue(Columns.StudyStatusEnum, value); }
        }

        [XmlAttribute("QueueStudyStateEnum")]
        [Bindable(true)]
        public short QueueStudyStateEnum
        {
            get { return GetColumnValue<short>(Columns.QueueStudyStateEnum); }
            set { SetColumnValue(Columns.QueueStudyStateEnum, value); }
        }

        #endregion

        #region PrimaryKey Methods		

        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);

            SetPKValues();
        }


        public ArchiveQueueCollection ArchiveQueueRecords()
        {
            return new ArchiveQueueCollection().Where(ArchiveQueue.Columns.StudyStorageGUID, Guid).Load();
        }

        public ArchiveStudyStorageCollection ArchiveStudyStorageRecords()
        {
            return new ArchiveStudyStorageCollection().Where(ArchiveStudyStorage.Columns.StudyStorageGUID, Guid).Load();
        }

        public FilesystemQueueCollection FilesystemQueueRecords()
        {
            return new FilesystemQueueCollection().Where(FilesystemQueue.Columns.StudyStorageGUID, Guid).Load();
        }

        public FilesystemStudyStorageCollection FilesystemStudyStorageRecords()
        {
            return
                new FilesystemStudyStorageCollection().Where(FilesystemStudyStorage.Columns.StudyStorageGUID, Guid).Load
                    ();
        }

        public RestoreQueueCollection RestoreQueueRecords()
        {
            return new RestoreQueueCollection().Where(RestoreQueue.Columns.StudyStorageGUID, Guid).Load();
        }

        public StudyCollection StudyRecords()
        {
            return new StudyCollection().Where(Study.Columns.StudyStorageGUID, Guid).Load();
        }

        public StudyHistoryCollection StudyHistoryRecords()
        {
            return new StudyHistoryCollection().Where(StudyHistory.Columns.DestStudyStorageGUID, Guid).Load();
        }

        public StudyHistoryCollection StudyHistoryRecordsFromStudyStorage()
        {
            return new StudyHistoryCollection().Where(StudyHistory.Columns.StudyStorageGUID, Guid).Load();
        }

        public StudyIntegrityQueueCollection StudyIntegrityQueueRecords()
        {
            return new StudyIntegrityQueueCollection().Where(StudyIntegrityQueue.Columns.StudyStorageGUID, Guid).Load();
        }

        public WorkQueueCollection WorkQueueRecords()
        {
            return new WorkQueueCollection().Where(WorkQueue.Columns.StudyStorageGUID, Guid).Load();
        }

        #endregion

        #region ForeignKey Properties

        /// <summary>
        /// Returns a QueueStudyStateEnum ActiveRecord object related to this StudyStorage
        /// 
        /// </summary>
        public QueueStudyStateEnum QueueStudyStateEnumRecord
        {
            get { return Models.QueueStudyStateEnum.FetchByID(QueueStudyStateEnum); }
            set { SetColumnValue("QueueStudyStateEnum", value.EnumX); }
        }


        /// <summary>
        /// Returns a ServerPartition ActiveRecord object related to this StudyStorage
        /// 
        /// </summary>
        public ServerPartition ServerPartition
        {
            get { return ServerPartition.FetchByID(ServerPartitionGUID); }
            set { SetColumnValue("ServerPartitionGUID", value.Guid); }
        }


        /// <summary>
        /// Returns a StudyStatusEnum ActiveRecord object related to this StudyStorage
        /// 
        /// </summary>
        public StudyStatusEnum StudyStatusEnumRecord
        {
            get { return Models.StudyStatusEnum.FetchByID(StudyStatusEnum); }
            set { SetColumnValue("StudyStatusEnum", value.EnumX); }
        }

        #endregion

        #region ObjectDataSource support

        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        public static void Insert(Guid varGuid, Guid varServerPartitionGUID, string varStudyInstanceUid,
                                  DateTime varInsertTime, DateTime varLastAccessedTime, bool varWriteLock,
                                  short varReadLock, short varStudyStatusEnum, short varQueueStudyStateEnum)
        {
            var item = new StudyStorage();

            item.Guid = varGuid;

            item.ServerPartitionGUID = varServerPartitionGUID;

            item.StudyInstanceUid = varStudyInstanceUid;

            item.InsertTime = varInsertTime;

            item.LastAccessedTime = varLastAccessedTime;

            item.WriteLock = varWriteLock;

            item.ReadLock = varReadLock;

            item.StudyStatusEnum = varStudyStatusEnum;

            item.QueueStudyStateEnum = varQueueStudyStateEnum;


            if (HttpContext.Current != null)
                item.Save(HttpContext.Current.User.Identity.Name);
            else
                item.Save(Thread.CurrentPrincipal.Identity.Name);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        public static void Update(Guid varGuid, Guid varServerPartitionGUID, string varStudyInstanceUid,
                                  DateTime varInsertTime, DateTime varLastAccessedTime, bool varWriteLock,
                                  short varReadLock, short varStudyStatusEnum, short varQueueStudyStateEnum)
        {
            var item = new StudyStorage();

            item.Guid = varGuid;

            item.ServerPartitionGUID = varServerPartitionGUID;

            item.StudyInstanceUid = varStudyInstanceUid;

            item.InsertTime = varInsertTime;

            item.LastAccessedTime = varLastAccessedTime;

            item.WriteLock = varWriteLock;

            item.ReadLock = varReadLock;

            item.StudyStatusEnum = varStudyStatusEnum;

            item.QueueStudyStateEnum = varQueueStudyStateEnum;

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


        public static TableSchema.TableColumn ServerPartitionGUIDColumn
        {
            get { return Schema.Columns[1]; }
        }


        public static TableSchema.TableColumn StudyInstanceUidColumn
        {
            get { return Schema.Columns[2]; }
        }


        public static TableSchema.TableColumn InsertTimeColumn
        {
            get { return Schema.Columns[3]; }
        }


        public static TableSchema.TableColumn LastAccessedTimeColumn
        {
            get { return Schema.Columns[4]; }
        }


        public static TableSchema.TableColumn WriteLockColumn
        {
            get { return Schema.Columns[5]; }
        }


        public static TableSchema.TableColumn ReadLockColumn
        {
            get { return Schema.Columns[6]; }
        }


        public static TableSchema.TableColumn StudyStatusEnumColumn
        {
            get { return Schema.Columns[7]; }
        }


        public static TableSchema.TableColumn QueueStudyStateEnumColumn
        {
            get { return Schema.Columns[8]; }
        }

        #endregion

        #region Columns Struct

        public struct Columns
        {
            public static string Guid = @"GUID";
            public static string ServerPartitionGUID = @"ServerPartitionGUID";
            public static string StudyInstanceUid = @"StudyInstanceUid";
            public static string InsertTime = @"InsertTime";
            public static string LastAccessedTime = @"LastAccessedTime";
            public static string WriteLock = @"WriteLock";
            public static string ReadLock = @"ReadLock";
            public static string StudyStatusEnum = @"StudyStatusEnum";
            public static string QueueStudyStateEnum = @"QueueStudyStateEnum";
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