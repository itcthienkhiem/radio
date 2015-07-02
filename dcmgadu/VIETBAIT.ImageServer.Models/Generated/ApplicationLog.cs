using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Web;
using System.Xml.Serialization;
using SubSonic;

// <auto-generated />

namespace VIETBAIT.ImageServer.Models
{
    /// <summary>
    /// Strongly-typed collection for the ApplicationLog class.
    /// </summary>
    [Serializable]
    public class ApplicationLogCollection : ActiveList<ApplicationLog, ApplicationLogCollection>
    {
        /// <summary>
        /// Filters an existing collection based on the set criteria. This is an in-memory filter
        /// Thanks to developingchris for this!
        /// </summary>
        /// <returns>ApplicationLogCollection</returns>
        public ApplicationLogCollection Filter()
        {
            for (int i = Count - 1; i > -1; i--)
            {
                ApplicationLog o = this[i];
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
    /// This is an ActiveRecord class which wraps the ApplicationLog table.
    /// </summary>
    [Serializable]
    public class ApplicationLog : ActiveRecord<ApplicationLog>, IActiveRecord
    {
        #region .ctors and Default Settings

        public ApplicationLog()
        {
            SetSQLProps();
            InitSetDefaults();
            MarkNew();
        }

        public ApplicationLog(bool useDatabaseDefaults)
        {
            SetSQLProps();
            if (useDatabaseDefaults)
                ForceDefaults();
            MarkNew();
        }

        public ApplicationLog(object keyID)
        {
            SetSQLProps();
            InitSetDefaults();
            LoadByKey(keyID);
        }

        public ApplicationLog(string columnName, object columnValue)
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
                var schema = new TableSchema.Table("ApplicationLog", TableType.Table, DataService.GetInstance("ORM"));
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

                var colvarHost = new TableSchema.TableColumn(schema);
                colvarHost.ColumnName = "Host";
                colvarHost.DataType = DbType.AnsiString;
                colvarHost.MaxLength = 50;
                colvarHost.AutoIncrement = false;
                colvarHost.IsNullable = false;
                colvarHost.IsPrimaryKey = false;
                colvarHost.IsForeignKey = false;
                colvarHost.IsReadOnly = false;
                colvarHost.DefaultSetting = @"";
                colvarHost.ForeignKeyTableName = "";
                schema.Columns.Add(colvarHost);

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

                var colvarLogLevel = new TableSchema.TableColumn(schema);
                colvarLogLevel.ColumnName = "LogLevel";
                colvarLogLevel.DataType = DbType.AnsiString;
                colvarLogLevel.MaxLength = 5;
                colvarLogLevel.AutoIncrement = false;
                colvarLogLevel.IsNullable = false;
                colvarLogLevel.IsPrimaryKey = false;
                colvarLogLevel.IsForeignKey = false;
                colvarLogLevel.IsReadOnly = false;
                colvarLogLevel.DefaultSetting = @"";
                colvarLogLevel.ForeignKeyTableName = "";
                schema.Columns.Add(colvarLogLevel);

                var colvarThread = new TableSchema.TableColumn(schema);
                colvarThread.ColumnName = "Thread";
                colvarThread.DataType = DbType.AnsiString;
                colvarThread.MaxLength = 50;
                colvarThread.AutoIncrement = false;
                colvarThread.IsNullable = false;
                colvarThread.IsPrimaryKey = false;
                colvarThread.IsForeignKey = false;
                colvarThread.IsReadOnly = false;
                colvarThread.DefaultSetting = @"";
                colvarThread.ForeignKeyTableName = "";
                schema.Columns.Add(colvarThread);

                var colvarMessage = new TableSchema.TableColumn(schema);
                colvarMessage.ColumnName = "Message";
                colvarMessage.DataType = DbType.AnsiString;
                colvarMessage.MaxLength = 3000;
                colvarMessage.AutoIncrement = false;
                colvarMessage.IsNullable = false;
                colvarMessage.IsPrimaryKey = false;
                colvarMessage.IsForeignKey = false;
                colvarMessage.IsReadOnly = false;
                colvarMessage.DefaultSetting = @"";
                colvarMessage.ForeignKeyTableName = "";
                schema.Columns.Add(colvarMessage);

                var colvarException = new TableSchema.TableColumn(schema);
                colvarException.ColumnName = "Exception";
                colvarException.DataType = DbType.AnsiString;
                colvarException.MaxLength = 2000;
                colvarException.AutoIncrement = false;
                colvarException.IsNullable = true;
                colvarException.IsPrimaryKey = false;
                colvarException.IsForeignKey = false;
                colvarException.IsReadOnly = false;
                colvarException.DefaultSetting = @"";
                colvarException.ForeignKeyTableName = "";
                schema.Columns.Add(colvarException);

                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("ApplicationLog", schema);
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

        [XmlAttribute("Host")]
        [Bindable(true)]
        public string Host
        {
            get { return GetColumnValue<string>(Columns.Host); }
            set { SetColumnValue(Columns.Host, value); }
        }

        [XmlAttribute("Timestamp")]
        [Bindable(true)]
        public DateTime Timestamp
        {
            get { return GetColumnValue<DateTime>(Columns.Timestamp); }
            set { SetColumnValue(Columns.Timestamp, value); }
        }

        [XmlAttribute("LogLevel")]
        [Bindable(true)]
        public string LogLevel
        {
            get { return GetColumnValue<string>(Columns.LogLevel); }
            set { SetColumnValue(Columns.LogLevel, value); }
        }

        [XmlAttribute("Thread")]
        [Bindable(true)]
        public string Thread
        {
            get { return GetColumnValue<string>(Columns.Thread); }
            set { SetColumnValue(Columns.Thread, value); }
        }

        [XmlAttribute("Message")]
        [Bindable(true)]
        public string Message
        {
            get { return GetColumnValue<string>(Columns.Message); }
            set { SetColumnValue(Columns.Message, value); }
        }

        [XmlAttribute("Exception")]
        [Bindable(true)]
        public string Exception
        {
            get { return GetColumnValue<string>(Columns.Exception); }
            set { SetColumnValue(Columns.Exception, value); }
        }

        #endregion

        #region ObjectDataSource support

        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        public static void Insert(Guid varGuid, string varHost, DateTime varTimestamp, string varLogLevel,
                                  string varThread, string varMessage, string varException)
        {
            var item = new ApplicationLog();

            item.Guid = varGuid;

            item.Host = varHost;

            item.Timestamp = varTimestamp;

            item.LogLevel = varLogLevel;

            item.Thread = varThread;

            item.Message = varMessage;

            item.Exception = varException;


            if (HttpContext.Current != null)
                item.Save(HttpContext.Current.User.Identity.Name);
            else
                item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        public static void Update(Guid varGuid, string varHost, DateTime varTimestamp, string varLogLevel,
                                  string varThread, string varMessage, string varException)
        {
            var item = new ApplicationLog();

            item.Guid = varGuid;

            item.Host = varHost;

            item.Timestamp = varTimestamp;

            item.LogLevel = varLogLevel;

            item.Thread = varThread;

            item.Message = varMessage;

            item.Exception = varException;

            item.IsNew = false;
            if (HttpContext.Current != null)
                item.Save(HttpContext.Current.User.Identity.Name);
            else
                item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
        }

        #endregion

        #region Typed Columns

        public static TableSchema.TableColumn GuidColumn
        {
            get { return Schema.Columns[0]; }
        }


        public static TableSchema.TableColumn HostColumn
        {
            get { return Schema.Columns[1]; }
        }


        public static TableSchema.TableColumn TimestampColumn
        {
            get { return Schema.Columns[2]; }
        }


        public static TableSchema.TableColumn LogLevelColumn
        {
            get { return Schema.Columns[3]; }
        }


        public static TableSchema.TableColumn ThreadColumn
        {
            get { return Schema.Columns[4]; }
        }


        public static TableSchema.TableColumn MessageColumn
        {
            get { return Schema.Columns[5]; }
        }


        public static TableSchema.TableColumn ExceptionColumn
        {
            get { return Schema.Columns[6]; }
        }

        #endregion

        #region Columns Struct

        public struct Columns
        {
            public static string Guid = @"GUID";
            public static string Host = @"Host";
            public static string Timestamp = @"Timestamp";
            public static string LogLevel = @"LogLevel";
            public static string Thread = @"Thread";
            public static string Message = @"Message";
            public static string Exception = @"Exception";
        }

        #endregion

        #region Update PK Collections

        #endregion

        #region Deep Save

        #endregion

        //no foreign key tables defined (0)


        //no ManyToMany tables defined (0)
    }
}