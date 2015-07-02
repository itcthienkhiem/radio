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
    /// Strongly-typed collection for the WorkQueue class.
    /// </summary>
    [Serializable]
    public class WorkQueueCollection : ActiveList<WorkQueue, WorkQueueCollection>
    {
        /// <summary>
        /// Filters an existing collection based on the set criteria. This is an in-memory filter
        /// Thanks to developingchris for this!
        /// </summary>
        /// <returns>WorkQueueCollection</returns>
        public WorkQueueCollection Filter()
        {
            for (int i = Count - 1; i > -1; i--)
            {
                WorkQueue o = this[i];
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
    /// This is an ActiveRecord class which wraps the WorkQueue table.
    /// </summary>
    [Serializable]
    public class WorkQueue : ActiveRecord<WorkQueue>, IActiveRecord
    {
        #region .ctors and Default Settings

        public WorkQueue()
        {
            SetSQLProps();
            InitSetDefaults();
            MarkNew();
        }

        public WorkQueue(bool useDatabaseDefaults)
        {
            SetSQLProps();
            if (useDatabaseDefaults)
                ForceDefaults();
            MarkNew();
        }

        public WorkQueue(object keyID)
        {
            SetSQLProps();
            InitSetDefaults();
            LoadByKey(keyID);
        }

        public WorkQueue(string columnName, object columnValue)
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
                var schema = new TableSchema.Table("WorkQueue", TableType.Table, DataService.GetInstance("ORM"));
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

                var colvarDeviceGUID = new TableSchema.TableColumn(schema);
                colvarDeviceGUID.ColumnName = "DeviceGUID";
                colvarDeviceGUID.DataType = DbType.Guid;
                colvarDeviceGUID.MaxLength = 0;
                colvarDeviceGUID.AutoIncrement = false;
                colvarDeviceGUID.IsNullable = true;
                colvarDeviceGUID.IsPrimaryKey = false;
                colvarDeviceGUID.IsForeignKey = true;
                colvarDeviceGUID.IsReadOnly = false;
                colvarDeviceGUID.DefaultSetting = @"";

                colvarDeviceGUID.ForeignKeyTableName = "Device";
                schema.Columns.Add(colvarDeviceGUID);

                var colvarStudyHistoryGUID = new TableSchema.TableColumn(schema);
                colvarStudyHistoryGUID.ColumnName = "StudyHistoryGUID";
                colvarStudyHistoryGUID.DataType = DbType.Guid;
                colvarStudyHistoryGUID.MaxLength = 0;
                colvarStudyHistoryGUID.AutoIncrement = false;
                colvarStudyHistoryGUID.IsNullable = true;
                colvarStudyHistoryGUID.IsPrimaryKey = false;
                colvarStudyHistoryGUID.IsForeignKey = true;
                colvarStudyHistoryGUID.IsReadOnly = false;
                colvarStudyHistoryGUID.DefaultSetting = @"";

                colvarStudyHistoryGUID.ForeignKeyTableName = "StudyHistory";
                schema.Columns.Add(colvarStudyHistoryGUID);

                var colvarWorkQueueTypeEnum = new TableSchema.TableColumn(schema);
                colvarWorkQueueTypeEnum.ColumnName = "WorkQueueTypeEnum";
                colvarWorkQueueTypeEnum.DataType = DbType.Int16;
                colvarWorkQueueTypeEnum.MaxLength = 0;
                colvarWorkQueueTypeEnum.AutoIncrement = false;
                colvarWorkQueueTypeEnum.IsNullable = false;
                colvarWorkQueueTypeEnum.IsPrimaryKey = false;
                colvarWorkQueueTypeEnum.IsForeignKey = true;
                colvarWorkQueueTypeEnum.IsReadOnly = false;
                colvarWorkQueueTypeEnum.DefaultSetting = @"";

                colvarWorkQueueTypeEnum.ForeignKeyTableName = "WorkQueueTypeEnum";
                schema.Columns.Add(colvarWorkQueueTypeEnum);

                var colvarWorkQueueStatusEnum = new TableSchema.TableColumn(schema);
                colvarWorkQueueStatusEnum.ColumnName = "WorkQueueStatusEnum";
                colvarWorkQueueStatusEnum.DataType = DbType.Int16;
                colvarWorkQueueStatusEnum.MaxLength = 0;
                colvarWorkQueueStatusEnum.AutoIncrement = false;
                colvarWorkQueueStatusEnum.IsNullable = false;
                colvarWorkQueueStatusEnum.IsPrimaryKey = false;
                colvarWorkQueueStatusEnum.IsForeignKey = true;
                colvarWorkQueueStatusEnum.IsReadOnly = false;
                colvarWorkQueueStatusEnum.DefaultSetting = @"";

                colvarWorkQueueStatusEnum.ForeignKeyTableName = "WorkQueueStatusEnum";
                schema.Columns.Add(colvarWorkQueueStatusEnum);

                var colvarWorkQueuePriorityEnum = new TableSchema.TableColumn(schema);
                colvarWorkQueuePriorityEnum.ColumnName = "WorkQueuePriorityEnum";
                colvarWorkQueuePriorityEnum.DataType = DbType.Int16;
                colvarWorkQueuePriorityEnum.MaxLength = 0;
                colvarWorkQueuePriorityEnum.AutoIncrement = false;
                colvarWorkQueuePriorityEnum.IsNullable = false;
                colvarWorkQueuePriorityEnum.IsPrimaryKey = false;
                colvarWorkQueuePriorityEnum.IsForeignKey = true;
                colvarWorkQueuePriorityEnum.IsReadOnly = false;

                colvarWorkQueuePriorityEnum.DefaultSetting = @"((200))";

                colvarWorkQueuePriorityEnum.ForeignKeyTableName = "WorkQueuePriorityEnum";
                schema.Columns.Add(colvarWorkQueuePriorityEnum);

                var colvarProcessorID = new TableSchema.TableColumn(schema);
                colvarProcessorID.ColumnName = "ProcessorID";
                colvarProcessorID.DataType = DbType.AnsiString;
                colvarProcessorID.MaxLength = 128;
                colvarProcessorID.AutoIncrement = false;
                colvarProcessorID.IsNullable = true;
                colvarProcessorID.IsPrimaryKey = false;
                colvarProcessorID.IsForeignKey = false;
                colvarProcessorID.IsReadOnly = false;
                colvarProcessorID.DefaultSetting = @"";
                colvarProcessorID.ForeignKeyTableName = "";
                schema.Columns.Add(colvarProcessorID);

                var colvarGroupID = new TableSchema.TableColumn(schema);
                colvarGroupID.ColumnName = "GroupID";
                colvarGroupID.DataType = DbType.AnsiString;
                colvarGroupID.MaxLength = 64;
                colvarGroupID.AutoIncrement = false;
                colvarGroupID.IsNullable = true;
                colvarGroupID.IsPrimaryKey = false;
                colvarGroupID.IsForeignKey = false;
                colvarGroupID.IsReadOnly = false;
                colvarGroupID.DefaultSetting = @"";
                colvarGroupID.ForeignKeyTableName = "";
                schema.Columns.Add(colvarGroupID);

                var colvarExpirationTime = new TableSchema.TableColumn(schema);
                colvarExpirationTime.ColumnName = "ExpirationTime";
                colvarExpirationTime.DataType = DbType.DateTime;
                colvarExpirationTime.MaxLength = 0;
                colvarExpirationTime.AutoIncrement = false;
                colvarExpirationTime.IsNullable = true;
                colvarExpirationTime.IsPrimaryKey = false;
                colvarExpirationTime.IsForeignKey = false;
                colvarExpirationTime.IsReadOnly = false;
                colvarExpirationTime.DefaultSetting = @"";
                colvarExpirationTime.ForeignKeyTableName = "";
                schema.Columns.Add(colvarExpirationTime);

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

                var colvarLastUpdatedTime = new TableSchema.TableColumn(schema);
                colvarLastUpdatedTime.ColumnName = "LastUpdatedTime";
                colvarLastUpdatedTime.DataType = DbType.DateTime;
                colvarLastUpdatedTime.MaxLength = 0;
                colvarLastUpdatedTime.AutoIncrement = false;
                colvarLastUpdatedTime.IsNullable = true;
                colvarLastUpdatedTime.IsPrimaryKey = false;
                colvarLastUpdatedTime.IsForeignKey = false;
                colvarLastUpdatedTime.IsReadOnly = false;
                colvarLastUpdatedTime.DefaultSetting = @"";
                colvarLastUpdatedTime.ForeignKeyTableName = "";
                schema.Columns.Add(colvarLastUpdatedTime);

                var colvarFailureCount = new TableSchema.TableColumn(schema);
                colvarFailureCount.ColumnName = "FailureCount";
                colvarFailureCount.DataType = DbType.Int32;
                colvarFailureCount.MaxLength = 0;
                colvarFailureCount.AutoIncrement = false;
                colvarFailureCount.IsNullable = false;
                colvarFailureCount.IsPrimaryKey = false;
                colvarFailureCount.IsForeignKey = false;
                colvarFailureCount.IsReadOnly = false;

                colvarFailureCount.DefaultSetting = @"((0))";
                colvarFailureCount.ForeignKeyTableName = "";
                schema.Columns.Add(colvarFailureCount);

                var colvarFailureDescription = new TableSchema.TableColumn(schema);
                colvarFailureDescription.ColumnName = "FailureDescription";
                colvarFailureDescription.DataType = DbType.String;
                colvarFailureDescription.MaxLength = 512;
                colvarFailureDescription.AutoIncrement = false;
                colvarFailureDescription.IsNullable = true;
                colvarFailureDescription.IsPrimaryKey = false;
                colvarFailureDescription.IsForeignKey = false;
                colvarFailureDescription.IsReadOnly = false;
                colvarFailureDescription.DefaultSetting = @"";
                colvarFailureDescription.ForeignKeyTableName = "";
                schema.Columns.Add(colvarFailureDescription);

                var colvarData = new TableSchema.TableColumn(schema);
                colvarData.ColumnName = "Data";
                colvarData.DataType = DbType.AnsiString;
                colvarData.MaxLength = -1;
                colvarData.AutoIncrement = false;
                colvarData.IsNullable = true;
                colvarData.IsPrimaryKey = false;
                colvarData.IsForeignKey = false;
                colvarData.IsReadOnly = false;
                colvarData.DefaultSetting = @"";
                colvarData.ForeignKeyTableName = "";
                schema.Columns.Add(colvarData);

                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("WorkQueue", schema);
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

        [XmlAttribute("StudyStorageGUID")]
        [Bindable(true)]
        public Guid StudyStorageGUID
        {
            get { return GetColumnValue<Guid>(Columns.StudyStorageGUID); }
            set { SetColumnValue(Columns.StudyStorageGUID, value); }
        }

        [XmlAttribute("DeviceGUID")]
        [Bindable(true)]
        public Guid? DeviceGUID
        {
            get { return GetColumnValue<Guid?>(Columns.DeviceGUID); }
            set { SetColumnValue(Columns.DeviceGUID, value); }
        }

        [XmlAttribute("StudyHistoryGUID")]
        [Bindable(true)]
        public Guid? StudyHistoryGUID
        {
            get { return GetColumnValue<Guid?>(Columns.StudyHistoryGUID); }
            set { SetColumnValue(Columns.StudyHistoryGUID, value); }
        }

        [XmlAttribute("WorkQueueTypeEnum")]
        [Bindable(true)]
        public short WorkQueueTypeEnum
        {
            get { return GetColumnValue<short>(Columns.WorkQueueTypeEnum); }
            set { SetColumnValue(Columns.WorkQueueTypeEnum, value); }
        }

        [XmlAttribute("WorkQueueStatusEnum")]
        [Bindable(true)]
        public short WorkQueueStatusEnum
        {
            get { return GetColumnValue<short>(Columns.WorkQueueStatusEnum); }
            set { SetColumnValue(Columns.WorkQueueStatusEnum, value); }
        }

        [XmlAttribute("WorkQueuePriorityEnum")]
        [Bindable(true)]
        public short WorkQueuePriorityEnum
        {
            get { return GetColumnValue<short>(Columns.WorkQueuePriorityEnum); }
            set { SetColumnValue(Columns.WorkQueuePriorityEnum, value); }
        }

        [XmlAttribute("ProcessorID")]
        [Bindable(true)]
        public string ProcessorID
        {
            get { return GetColumnValue<string>(Columns.ProcessorID); }
            set { SetColumnValue(Columns.ProcessorID, value); }
        }

        [XmlAttribute("GroupID")]
        [Bindable(true)]
        public string GroupID
        {
            get { return GetColumnValue<string>(Columns.GroupID); }
            set { SetColumnValue(Columns.GroupID, value); }
        }

        [XmlAttribute("ExpirationTime")]
        [Bindable(true)]
        public DateTime? ExpirationTime
        {
            get { return GetColumnValue<DateTime?>(Columns.ExpirationTime); }
            set { SetColumnValue(Columns.ExpirationTime, value); }
        }

        [XmlAttribute("ScheduledTime")]
        [Bindable(true)]
        public DateTime ScheduledTime
        {
            get { return GetColumnValue<DateTime>(Columns.ScheduledTime); }
            set { SetColumnValue(Columns.ScheduledTime, value); }
        }

        [XmlAttribute("InsertTime")]
        [Bindable(true)]
        public DateTime InsertTime
        {
            get { return GetColumnValue<DateTime>(Columns.InsertTime); }
            set { SetColumnValue(Columns.InsertTime, value); }
        }

        [XmlAttribute("LastUpdatedTime")]
        [Bindable(true)]
        public DateTime? LastUpdatedTime
        {
            get { return GetColumnValue<DateTime?>(Columns.LastUpdatedTime); }
            set { SetColumnValue(Columns.LastUpdatedTime, value); }
        }

        [XmlAttribute("FailureCount")]
        [Bindable(true)]
        public int FailureCount
        {
            get { return GetColumnValue<int>(Columns.FailureCount); }
            set { SetColumnValue(Columns.FailureCount, value); }
        }

        [XmlAttribute("FailureDescription")]
        [Bindable(true)]
        public string FailureDescription
        {
            get { return GetColumnValue<string>(Columns.FailureDescription); }
            set { SetColumnValue(Columns.FailureDescription, value); }
        }

        [XmlAttribute("Data")]
        [Bindable(true)]
        public string Data
        {
            get { return GetColumnValue<string>(Columns.Data); }
            set { SetColumnValue(Columns.Data, value); }
        }

        #endregion

        #region PrimaryKey Methods		

        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);

            SetPKValues();
        }


        public WorkQueueUidCollection WorkQueueUidRecords()
        {
            return new WorkQueueUidCollection().Where(WorkQueueUid.Columns.WorkQueueGUID, Guid).Load();
        }

        #endregion

        #region ForeignKey Properties

        /// <summary>
        /// Returns a Device ActiveRecord object related to this WorkQueue
        /// 
        /// </summary>
        public Device Device
        {
            get { return Device.FetchByID(DeviceGUID); }
            set { SetColumnValue("DeviceGUID", value.Guid); }
        }


        /// <summary>
        /// Returns a ServerPartition ActiveRecord object related to this WorkQueue
        /// 
        /// </summary>
        public ServerPartition ServerPartition
        {
            get { return ServerPartition.FetchByID(ServerPartitionGUID); }
            set { SetColumnValue("ServerPartitionGUID", value.Guid); }
        }


        /// <summary>
        /// Returns a StudyHistory ActiveRecord object related to this WorkQueue
        /// 
        /// </summary>
        public StudyHistory StudyHistory
        {
            get { return StudyHistory.FetchByID(StudyHistoryGUID); }
            set { SetColumnValue("StudyHistoryGUID", value.Guid); }
        }


        /// <summary>
        /// Returns a StudyStorage ActiveRecord object related to this WorkQueue
        /// 
        /// </summary>
        public StudyStorage StudyStorage
        {
            get { return StudyStorage.FetchByID(StudyStorageGUID); }
            set { SetColumnValue("StudyStorageGUID", value.Guid); }
        }


        /// <summary>
        /// Returns a WorkQueuePriorityEnum ActiveRecord object related to this WorkQueue
        /// 
        /// </summary>
        public WorkQueuePriorityEnum WorkQueuePriorityEnumRecord
        {
            get { return Models.WorkQueuePriorityEnum.FetchByID(WorkQueuePriorityEnum); }
            set { SetColumnValue("WorkQueuePriorityEnum", value.EnumX); }
        }


        /// <summary>
        /// Returns a WorkQueueStatusEnum ActiveRecord object related to this WorkQueue
        /// 
        /// </summary>
        public WorkQueueStatusEnum WorkQueueStatusEnumRecord
        {
            get { return Models.WorkQueueStatusEnum.FetchByID(WorkQueueStatusEnum); }
            set { SetColumnValue("WorkQueueStatusEnum", value.EnumX); }
        }


        /// <summary>
        /// Returns a WorkQueueTypeEnum ActiveRecord object related to this WorkQueue
        /// 
        /// </summary>
        public WorkQueueTypeEnum WorkQueueTypeEnumRecord
        {
            get { return Models.WorkQueueTypeEnum.FetchByID(WorkQueueTypeEnum); }
            set { SetColumnValue("WorkQueueTypeEnum", value.EnumX); }
        }

        #endregion

        #region ObjectDataSource support

        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        public static void Insert(Guid varGuid, Guid varServerPartitionGUID, Guid varStudyStorageGUID,
                                  Guid? varDeviceGUID, Guid? varStudyHistoryGUID, short varWorkQueueTypeEnum,
                                  short varWorkQueueStatusEnum, short varWorkQueuePriorityEnum, string varProcessorID,
                                  string varGroupID, DateTime? varExpirationTime, DateTime varScheduledTime,
                                  DateTime varInsertTime, DateTime? varLastUpdatedTime, int varFailureCount,
                                  string varFailureDescription, string varData)
        {
            var item = new WorkQueue();

            item.Guid = varGuid;

            item.ServerPartitionGUID = varServerPartitionGUID;

            item.StudyStorageGUID = varStudyStorageGUID;

            item.DeviceGUID = varDeviceGUID;

            item.StudyHistoryGUID = varStudyHistoryGUID;

            item.WorkQueueTypeEnum = varWorkQueueTypeEnum;

            item.WorkQueueStatusEnum = varWorkQueueStatusEnum;

            item.WorkQueuePriorityEnum = varWorkQueuePriorityEnum;

            item.ProcessorID = varProcessorID;

            item.GroupID = varGroupID;

            item.ExpirationTime = varExpirationTime;

            item.ScheduledTime = varScheduledTime;

            item.InsertTime = varInsertTime;

            item.LastUpdatedTime = varLastUpdatedTime;

            item.FailureCount = varFailureCount;

            item.FailureDescription = varFailureDescription;

            item.Data = varData;


            if (HttpContext.Current != null)
                item.Save(HttpContext.Current.User.Identity.Name);
            else
                item.Save(Thread.CurrentPrincipal.Identity.Name);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        public static void Update(Guid varGuid, Guid varServerPartitionGUID, Guid varStudyStorageGUID,
                                  Guid? varDeviceGUID, Guid? varStudyHistoryGUID, short varWorkQueueTypeEnum,
                                  short varWorkQueueStatusEnum, short varWorkQueuePriorityEnum, string varProcessorID,
                                  string varGroupID, DateTime? varExpirationTime, DateTime varScheduledTime,
                                  DateTime varInsertTime, DateTime? varLastUpdatedTime, int varFailureCount,
                                  string varFailureDescription, string varData)
        {
            var item = new WorkQueue();

            item.Guid = varGuid;

            item.ServerPartitionGUID = varServerPartitionGUID;

            item.StudyStorageGUID = varStudyStorageGUID;

            item.DeviceGUID = varDeviceGUID;

            item.StudyHistoryGUID = varStudyHistoryGUID;

            item.WorkQueueTypeEnum = varWorkQueueTypeEnum;

            item.WorkQueueStatusEnum = varWorkQueueStatusEnum;

            item.WorkQueuePriorityEnum = varWorkQueuePriorityEnum;

            item.ProcessorID = varProcessorID;

            item.GroupID = varGroupID;

            item.ExpirationTime = varExpirationTime;

            item.ScheduledTime = varScheduledTime;

            item.InsertTime = varInsertTime;

            item.LastUpdatedTime = varLastUpdatedTime;

            item.FailureCount = varFailureCount;

            item.FailureDescription = varFailureDescription;

            item.Data = varData;

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


        public static TableSchema.TableColumn StudyStorageGUIDColumn
        {
            get { return Schema.Columns[2]; }
        }


        public static TableSchema.TableColumn DeviceGUIDColumn
        {
            get { return Schema.Columns[3]; }
        }


        public static TableSchema.TableColumn StudyHistoryGUIDColumn
        {
            get { return Schema.Columns[4]; }
        }


        public static TableSchema.TableColumn WorkQueueTypeEnumColumn
        {
            get { return Schema.Columns[5]; }
        }


        public static TableSchema.TableColumn WorkQueueStatusEnumColumn
        {
            get { return Schema.Columns[6]; }
        }


        public static TableSchema.TableColumn WorkQueuePriorityEnumColumn
        {
            get { return Schema.Columns[7]; }
        }


        public static TableSchema.TableColumn ProcessorIDColumn
        {
            get { return Schema.Columns[8]; }
        }


        public static TableSchema.TableColumn GroupIDColumn
        {
            get { return Schema.Columns[9]; }
        }


        public static TableSchema.TableColumn ExpirationTimeColumn
        {
            get { return Schema.Columns[10]; }
        }


        public static TableSchema.TableColumn ScheduledTimeColumn
        {
            get { return Schema.Columns[11]; }
        }


        public static TableSchema.TableColumn InsertTimeColumn
        {
            get { return Schema.Columns[12]; }
        }


        public static TableSchema.TableColumn LastUpdatedTimeColumn
        {
            get { return Schema.Columns[13]; }
        }


        public static TableSchema.TableColumn FailureCountColumn
        {
            get { return Schema.Columns[14]; }
        }


        public static TableSchema.TableColumn FailureDescriptionColumn
        {
            get { return Schema.Columns[15]; }
        }


        public static TableSchema.TableColumn DataColumn
        {
            get { return Schema.Columns[16]; }
        }

        #endregion

        #region Columns Struct

        public struct Columns
        {
            public static string Guid = @"GUID";
            public static string ServerPartitionGUID = @"ServerPartitionGUID";
            public static string StudyStorageGUID = @"StudyStorageGUID";
            public static string DeviceGUID = @"DeviceGUID";
            public static string StudyHistoryGUID = @"StudyHistoryGUID";
            public static string WorkQueueTypeEnum = @"WorkQueueTypeEnum";
            public static string WorkQueueStatusEnum = @"WorkQueueStatusEnum";
            public static string WorkQueuePriorityEnum = @"WorkQueuePriorityEnum";
            public static string ProcessorID = @"ProcessorID";
            public static string GroupID = @"GroupID";
            public static string ExpirationTime = @"ExpirationTime";
            public static string ScheduledTime = @"ScheduledTime";
            public static string InsertTime = @"InsertTime";
            public static string LastUpdatedTime = @"LastUpdatedTime";
            public static string FailureCount = @"FailureCount";
            public static string FailureDescription = @"FailureDescription";
            public static string Data = @"Data";
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