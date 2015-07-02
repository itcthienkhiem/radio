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
    /// Strongly-typed collection for the StudyHistory class.
    /// </summary>
    [Serializable]
    public class StudyHistoryCollection : ActiveList<StudyHistory, StudyHistoryCollection>
    {
        /// <summary>
        /// Filters an existing collection based on the set criteria. This is an in-memory filter
        /// Thanks to developingchris for this!
        /// </summary>
        /// <returns>StudyHistoryCollection</returns>
        public StudyHistoryCollection Filter()
        {
            for (int i = Count - 1; i > -1; i--)
            {
                StudyHistory o = this[i];
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
    /// This is an ActiveRecord class which wraps the StudyHistory table.
    /// </summary>
    [Serializable]
    public class StudyHistory : ActiveRecord<StudyHistory>, IActiveRecord
    {
        #region .ctors and Default Settings

        public StudyHistory()
        {
            SetSQLProps();
            InitSetDefaults();
            MarkNew();
        }

        public StudyHistory(bool useDatabaseDefaults)
        {
            SetSQLProps();
            if (useDatabaseDefaults)
                ForceDefaults();
            MarkNew();
        }

        public StudyHistory(object keyID)
        {
            SetSQLProps();
            InitSetDefaults();
            LoadByKey(keyID);
        }

        public StudyHistory(string columnName, object columnValue)
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
                var schema = new TableSchema.Table("StudyHistory", TableType.Table, DataService.GetInstance("ORM"));
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

                var colvarDestStudyStorageGUID = new TableSchema.TableColumn(schema);
                colvarDestStudyStorageGUID.ColumnName = "DestStudyStorageGUID";
                colvarDestStudyStorageGUID.DataType = DbType.Guid;
                colvarDestStudyStorageGUID.MaxLength = 0;
                colvarDestStudyStorageGUID.AutoIncrement = false;
                colvarDestStudyStorageGUID.IsNullable = true;
                colvarDestStudyStorageGUID.IsPrimaryKey = false;
                colvarDestStudyStorageGUID.IsForeignKey = true;
                colvarDestStudyStorageGUID.IsReadOnly = false;
                colvarDestStudyStorageGUID.DefaultSetting = @"";

                colvarDestStudyStorageGUID.ForeignKeyTableName = "StudyStorage";
                schema.Columns.Add(colvarDestStudyStorageGUID);

                var colvarStudyHistoryTypeEnum = new TableSchema.TableColumn(schema);
                colvarStudyHistoryTypeEnum.ColumnName = "StudyHistoryTypeEnum";
                colvarStudyHistoryTypeEnum.DataType = DbType.Int16;
                colvarStudyHistoryTypeEnum.MaxLength = 0;
                colvarStudyHistoryTypeEnum.AutoIncrement = false;
                colvarStudyHistoryTypeEnum.IsNullable = false;
                colvarStudyHistoryTypeEnum.IsPrimaryKey = false;
                colvarStudyHistoryTypeEnum.IsForeignKey = true;
                colvarStudyHistoryTypeEnum.IsReadOnly = false;
                colvarStudyHistoryTypeEnum.DefaultSetting = @"";

                colvarStudyHistoryTypeEnum.ForeignKeyTableName = "StudyHistoryTypeEnum";
                schema.Columns.Add(colvarStudyHistoryTypeEnum);

                var colvarStudyData = new TableSchema.TableColumn(schema);
                colvarStudyData.ColumnName = "StudyData";
                colvarStudyData.DataType = DbType.AnsiString;
                colvarStudyData.MaxLength = -1;
                colvarStudyData.AutoIncrement = false;
                colvarStudyData.IsNullable = false;
                colvarStudyData.IsPrimaryKey = false;
                colvarStudyData.IsForeignKey = false;
                colvarStudyData.IsReadOnly = false;
                colvarStudyData.DefaultSetting = @"";
                colvarStudyData.ForeignKeyTableName = "";
                schema.Columns.Add(colvarStudyData);

                var colvarChangeDescription = new TableSchema.TableColumn(schema);
                colvarChangeDescription.ColumnName = "ChangeDescription";
                colvarChangeDescription.DataType = DbType.AnsiString;
                colvarChangeDescription.MaxLength = -1;
                colvarChangeDescription.AutoIncrement = false;
                colvarChangeDescription.IsNullable = true;
                colvarChangeDescription.IsPrimaryKey = false;
                colvarChangeDescription.IsForeignKey = false;
                colvarChangeDescription.IsReadOnly = false;
                colvarChangeDescription.DefaultSetting = @"";
                colvarChangeDescription.ForeignKeyTableName = "";
                schema.Columns.Add(colvarChangeDescription);

                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("StudyHistory", schema);
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

        [XmlAttribute("InsertTime")]
        [Bindable(true)]
        public DateTime InsertTime
        {
            get { return GetColumnValue<DateTime>(Columns.InsertTime); }
            set { SetColumnValue(Columns.InsertTime, value); }
        }

        [XmlAttribute("StudyStorageGUID")]
        [Bindable(true)]
        public Guid StudyStorageGUID
        {
            get { return GetColumnValue<Guid>(Columns.StudyStorageGUID); }
            set { SetColumnValue(Columns.StudyStorageGUID, value); }
        }

        [XmlAttribute("DestStudyStorageGUID")]
        [Bindable(true)]
        public Guid? DestStudyStorageGUID
        {
            get { return GetColumnValue<Guid?>(Columns.DestStudyStorageGUID); }
            set { SetColumnValue(Columns.DestStudyStorageGUID, value); }
        }

        [XmlAttribute("StudyHistoryTypeEnum")]
        [Bindable(true)]
        public short StudyHistoryTypeEnum
        {
            get { return GetColumnValue<short>(Columns.StudyHistoryTypeEnum); }
            set { SetColumnValue(Columns.StudyHistoryTypeEnum, value); }
        }

        [XmlAttribute("StudyData")]
        [Bindable(true)]
        public string StudyData
        {
            get { return GetColumnValue<string>(Columns.StudyData); }
            set { SetColumnValue(Columns.StudyData, value); }
        }

        [XmlAttribute("ChangeDescription")]
        [Bindable(true)]
        public string ChangeDescription
        {
            get { return GetColumnValue<string>(Columns.ChangeDescription); }
            set { SetColumnValue(Columns.ChangeDescription, value); }
        }

        #endregion

        #region PrimaryKey Methods		

        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);

            SetPKValues();
        }


        public WorkQueueCollection WorkQueueRecords()
        {
            return new WorkQueueCollection().Where(WorkQueue.Columns.StudyHistoryGUID, Guid).Load();
        }

        #endregion

        #region ForeignKey Properties

        /// <summary>
        /// Returns a StudyHistoryTypeEnum ActiveRecord object related to this StudyHistory
        /// 
        /// </summary>
        public StudyHistoryTypeEnum StudyHistoryTypeEnumRecord
        {
            get { return Models.StudyHistoryTypeEnum.FetchByID(StudyHistoryTypeEnum); }
            set { SetColumnValue("StudyHistoryTypeEnum", value.EnumX); }
        }


        /// <summary>
        /// Returns a StudyStorage ActiveRecord object related to this StudyHistory
        /// 
        /// </summary>
        public StudyStorage StudyStorage
        {
            get { return StudyStorage.FetchByID(DestStudyStorageGUID); }
            set { SetColumnValue("DestStudyStorageGUID", value.Guid); }
        }


        /// <summary>
        /// Returns a StudyStorage ActiveRecord object related to this StudyHistory
        /// 
        /// </summary>
        public StudyStorage StudyStorageToStudyStorageGUID
        {
            get { return StudyStorage.FetchByID(StudyStorageGUID); }
            set { SetColumnValue("StudyStorageGUID", value.Guid); }
        }

        #endregion

        #region ObjectDataSource support

        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        public static void Insert(Guid varGuid, DateTime varInsertTime, Guid varStudyStorageGUID,
                                  Guid? varDestStudyStorageGUID, short varStudyHistoryTypeEnum, string varStudyData,
                                  string varChangeDescription)
        {
            var item = new StudyHistory();

            item.Guid = varGuid;

            item.InsertTime = varInsertTime;

            item.StudyStorageGUID = varStudyStorageGUID;

            item.DestStudyStorageGUID = varDestStudyStorageGUID;

            item.StudyHistoryTypeEnum = varStudyHistoryTypeEnum;

            item.StudyData = varStudyData;

            item.ChangeDescription = varChangeDescription;


            if (HttpContext.Current != null)
                item.Save(HttpContext.Current.User.Identity.Name);
            else
                item.Save(Thread.CurrentPrincipal.Identity.Name);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        public static void Update(Guid varGuid, DateTime varInsertTime, Guid varStudyStorageGUID,
                                  Guid? varDestStudyStorageGUID, short varStudyHistoryTypeEnum, string varStudyData,
                                  string varChangeDescription)
        {
            var item = new StudyHistory();

            item.Guid = varGuid;

            item.InsertTime = varInsertTime;

            item.StudyStorageGUID = varStudyStorageGUID;

            item.DestStudyStorageGUID = varDestStudyStorageGUID;

            item.StudyHistoryTypeEnum = varStudyHistoryTypeEnum;

            item.StudyData = varStudyData;

            item.ChangeDescription = varChangeDescription;

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


        public static TableSchema.TableColumn InsertTimeColumn
        {
            get { return Schema.Columns[1]; }
        }


        public static TableSchema.TableColumn StudyStorageGUIDColumn
        {
            get { return Schema.Columns[2]; }
        }


        public static TableSchema.TableColumn DestStudyStorageGUIDColumn
        {
            get { return Schema.Columns[3]; }
        }


        public static TableSchema.TableColumn StudyHistoryTypeEnumColumn
        {
            get { return Schema.Columns[4]; }
        }


        public static TableSchema.TableColumn StudyDataColumn
        {
            get { return Schema.Columns[5]; }
        }


        public static TableSchema.TableColumn ChangeDescriptionColumn
        {
            get { return Schema.Columns[6]; }
        }

        #endregion

        #region Columns Struct

        public struct Columns
        {
            public static string Guid = @"GUID";
            public static string InsertTime = @"InsertTime";
            public static string StudyStorageGUID = @"StudyStorageGUID";
            public static string DestStudyStorageGUID = @"DestStudyStorageGUID";
            public static string StudyHistoryTypeEnum = @"StudyHistoryTypeEnum";
            public static string StudyData = @"StudyData";
            public static string ChangeDescription = @"ChangeDescription";
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