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
    /// Strongly-typed collection for the DevicePreferredTransferSyntax class.
    /// </summary>
    [Serializable]
    public class DevicePreferredTransferSyntaxCollection :
        ActiveList<DevicePreferredTransferSyntax, DevicePreferredTransferSyntaxCollection>
    {
        /// <summary>
        /// Filters an existing collection based on the set criteria. This is an in-memory filter
        /// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DevicePreferredTransferSyntaxCollection</returns>
        public DevicePreferredTransferSyntaxCollection Filter()
        {
            for (int i = Count - 1; i > -1; i--)
            {
                DevicePreferredTransferSyntax o = this[i];
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
    /// This is an ActiveRecord class which wraps the DevicePreferredTransferSyntax table.
    /// </summary>
    [Serializable]
    public class DevicePreferredTransferSyntax : ActiveRecord<DevicePreferredTransferSyntax>, IActiveRecord
    {
        #region .ctors and Default Settings

        public DevicePreferredTransferSyntax()
        {
            SetSQLProps();
            InitSetDefaults();
            MarkNew();
        }

        public DevicePreferredTransferSyntax(bool useDatabaseDefaults)
        {
            SetSQLProps();
            if (useDatabaseDefaults)
                ForceDefaults();
            MarkNew();
        }

        public DevicePreferredTransferSyntax(object keyID)
        {
            SetSQLProps();
            InitSetDefaults();
            LoadByKey(keyID);
        }

        public DevicePreferredTransferSyntax(string columnName, object columnValue)
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
                var schema = new TableSchema.Table("DevicePreferredTransferSyntax", TableType.Table,
                                                   DataService.GetInstance("ORM"));
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

                var colvarDeviceGUID = new TableSchema.TableColumn(schema);
                colvarDeviceGUID.ColumnName = "DeviceGUID";
                colvarDeviceGUID.DataType = DbType.Guid;
                colvarDeviceGUID.MaxLength = 0;
                colvarDeviceGUID.AutoIncrement = false;
                colvarDeviceGUID.IsNullable = false;
                colvarDeviceGUID.IsPrimaryKey = false;
                colvarDeviceGUID.IsForeignKey = true;
                colvarDeviceGUID.IsReadOnly = false;
                colvarDeviceGUID.DefaultSetting = @"";

                colvarDeviceGUID.ForeignKeyTableName = "Device";
                schema.Columns.Add(colvarDeviceGUID);

                var colvarServerSopClassGUID = new TableSchema.TableColumn(schema);
                colvarServerSopClassGUID.ColumnName = "ServerSopClassGUID";
                colvarServerSopClassGUID.DataType = DbType.Guid;
                colvarServerSopClassGUID.MaxLength = 0;
                colvarServerSopClassGUID.AutoIncrement = false;
                colvarServerSopClassGUID.IsNullable = false;
                colvarServerSopClassGUID.IsPrimaryKey = false;
                colvarServerSopClassGUID.IsForeignKey = true;
                colvarServerSopClassGUID.IsReadOnly = false;
                colvarServerSopClassGUID.DefaultSetting = @"";

                colvarServerSopClassGUID.ForeignKeyTableName = "ServerSopClass";
                schema.Columns.Add(colvarServerSopClassGUID);

                var colvarServerTransferSyntaxGUID = new TableSchema.TableColumn(schema);
                colvarServerTransferSyntaxGUID.ColumnName = "ServerTransferSyntaxGUID";
                colvarServerTransferSyntaxGUID.DataType = DbType.Guid;
                colvarServerTransferSyntaxGUID.MaxLength = 0;
                colvarServerTransferSyntaxGUID.AutoIncrement = false;
                colvarServerTransferSyntaxGUID.IsNullable = false;
                colvarServerTransferSyntaxGUID.IsPrimaryKey = false;
                colvarServerTransferSyntaxGUID.IsForeignKey = true;
                colvarServerTransferSyntaxGUID.IsReadOnly = false;
                colvarServerTransferSyntaxGUID.DefaultSetting = @"";

                colvarServerTransferSyntaxGUID.ForeignKeyTableName = "ServerTransferSyntax";
                schema.Columns.Add(colvarServerTransferSyntaxGUID);

                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("DevicePreferredTransferSyntax", schema);
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

        [XmlAttribute("DeviceGUID")]
        [Bindable(true)]
        public Guid DeviceGUID
        {
            get { return GetColumnValue<Guid>(Columns.DeviceGUID); }
            set { SetColumnValue(Columns.DeviceGUID, value); }
        }

        [XmlAttribute("ServerSopClassGUID")]
        [Bindable(true)]
        public Guid ServerSopClassGUID
        {
            get { return GetColumnValue<Guid>(Columns.ServerSopClassGUID); }
            set { SetColumnValue(Columns.ServerSopClassGUID, value); }
        }

        [XmlAttribute("ServerTransferSyntaxGUID")]
        [Bindable(true)]
        public Guid ServerTransferSyntaxGUID
        {
            get { return GetColumnValue<Guid>(Columns.ServerTransferSyntaxGUID); }
            set { SetColumnValue(Columns.ServerTransferSyntaxGUID, value); }
        }

        #endregion

        #region ForeignKey Properties

        /// <summary>
        /// Returns a Device ActiveRecord object related to this DevicePreferredTransferSyntax
        /// 
        /// </summary>
        public Device Device
        {
            get { return Device.FetchByID(DeviceGUID); }
            set { SetColumnValue("DeviceGUID", value.Guid); }
        }


        /// <summary>
        /// Returns a ServerTransferSyntax ActiveRecord object related to this DevicePreferredTransferSyntax
        /// 
        /// </summary>
        public ServerTransferSyntax ServerTransferSyntax
        {
            get { return ServerTransferSyntax.FetchByID(ServerTransferSyntaxGUID); }
            set { SetColumnValue("ServerTransferSyntaxGUID", value.Guid); }
        }


        /// <summary>
        /// Returns a ServerSopClass ActiveRecord object related to this DevicePreferredTransferSyntax
        /// 
        /// </summary>
        public ServerSopClass ServerSopClass
        {
            get { return ServerSopClass.FetchByID(ServerSopClassGUID); }
            set { SetColumnValue("ServerSopClassGUID", value.Guid); }
        }

        #endregion

        #region ObjectDataSource support

        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        public static void Insert(Guid varGuid, Guid varDeviceGUID, Guid varServerSopClassGUID,
                                  Guid varServerTransferSyntaxGUID)
        {
            var item = new DevicePreferredTransferSyntax();

            item.Guid = varGuid;

            item.DeviceGUID = varDeviceGUID;

            item.ServerSopClassGUID = varServerSopClassGUID;

            item.ServerTransferSyntaxGUID = varServerTransferSyntaxGUID;


            if (HttpContext.Current != null)
                item.Save(HttpContext.Current.User.Identity.Name);
            else
                item.Save(Thread.CurrentPrincipal.Identity.Name);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        public static void Update(Guid varGuid, Guid varDeviceGUID, Guid varServerSopClassGUID,
                                  Guid varServerTransferSyntaxGUID)
        {
            var item = new DevicePreferredTransferSyntax();

            item.Guid = varGuid;

            item.DeviceGUID = varDeviceGUID;

            item.ServerSopClassGUID = varServerSopClassGUID;

            item.ServerTransferSyntaxGUID = varServerTransferSyntaxGUID;

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


        public static TableSchema.TableColumn DeviceGUIDColumn
        {
            get { return Schema.Columns[1]; }
        }


        public static TableSchema.TableColumn ServerSopClassGUIDColumn
        {
            get { return Schema.Columns[2]; }
        }


        public static TableSchema.TableColumn ServerTransferSyntaxGUIDColumn
        {
            get { return Schema.Columns[3]; }
        }

        #endregion

        #region Columns Struct

        public struct Columns
        {
            public static string Guid = @"GUID";
            public static string DeviceGUID = @"DeviceGUID";
            public static string ServerSopClassGUID = @"ServerSopClassGUID";
            public static string ServerTransferSyntaxGUID = @"ServerTransferSyntaxGUID";
        }

        #endregion

        #region Update PK Collections

        #endregion

        #region Deep Save

        #endregion

        //no ManyToMany tables defined (0)
    }
}