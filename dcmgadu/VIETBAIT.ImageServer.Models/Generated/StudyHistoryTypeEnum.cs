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
    /// Strongly-typed collection for the StudyHistoryTypeEnum class.
    /// </summary>
    [Serializable]
    public class StudyHistoryTypeEnumCollection : ActiveList<StudyHistoryTypeEnum, StudyHistoryTypeEnumCollection>
    {
        /// <summary>
        /// Filters an existing collection based on the set criteria. This is an in-memory filter
        /// Thanks to developingchris for this!
        /// </summary>
        /// <returns>StudyHistoryTypeEnumCollection</returns>
        public StudyHistoryTypeEnumCollection Filter()
        {
            for (int i = Count - 1; i > -1; i--)
            {
                StudyHistoryTypeEnum o = this[i];
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
    /// This is an ActiveRecord class which wraps the StudyHistoryTypeEnum table.
    /// </summary>
    [Serializable]
    public class StudyHistoryTypeEnum : ActiveRecord<StudyHistoryTypeEnum>, IActiveRecord
    {
        #region .ctors and Default Settings

        public StudyHistoryTypeEnum()
        {
            SetSQLProps();
            InitSetDefaults();
            MarkNew();
        }

        public StudyHistoryTypeEnum(bool useDatabaseDefaults)
        {
            SetSQLProps();
            if (useDatabaseDefaults)
                ForceDefaults();
            MarkNew();
        }

        public StudyHistoryTypeEnum(object keyID)
        {
            SetSQLProps();
            InitSetDefaults();
            LoadByKey(keyID);
        }

        public StudyHistoryTypeEnum(string columnName, object columnValue)
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
                var schema = new TableSchema.Table("StudyHistoryTypeEnum", TableType.Table,
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
                colvarGuid.IsPrimaryKey = false;
                colvarGuid.IsForeignKey = false;
                colvarGuid.IsReadOnly = false;

                colvarGuid.DefaultSetting = @"(newid())";
                colvarGuid.ForeignKeyTableName = "";
                schema.Columns.Add(colvarGuid);

                var colvarEnumX = new TableSchema.TableColumn(schema);
                colvarEnumX.ColumnName = "Enum";
                colvarEnumX.DataType = DbType.Int16;
                colvarEnumX.MaxLength = 0;
                colvarEnumX.AutoIncrement = false;
                colvarEnumX.IsNullable = false;
                colvarEnumX.IsPrimaryKey = true;
                colvarEnumX.IsForeignKey = false;
                colvarEnumX.IsReadOnly = false;
                colvarEnumX.DefaultSetting = @"";
                colvarEnumX.ForeignKeyTableName = "";
                schema.Columns.Add(colvarEnumX);

                var colvarLookup = new TableSchema.TableColumn(schema);
                colvarLookup.ColumnName = "Lookup";
                colvarLookup.DataType = DbType.AnsiString;
                colvarLookup.MaxLength = 32;
                colvarLookup.AutoIncrement = false;
                colvarLookup.IsNullable = false;
                colvarLookup.IsPrimaryKey = false;
                colvarLookup.IsForeignKey = false;
                colvarLookup.IsReadOnly = false;
                colvarLookup.DefaultSetting = @"";
                colvarLookup.ForeignKeyTableName = "";
                schema.Columns.Add(colvarLookup);

                var colvarDescription = new TableSchema.TableColumn(schema);
                colvarDescription.ColumnName = "Description";
                colvarDescription.DataType = DbType.String;
                colvarDescription.MaxLength = 32;
                colvarDescription.AutoIncrement = false;
                colvarDescription.IsNullable = false;
                colvarDescription.IsPrimaryKey = false;
                colvarDescription.IsForeignKey = false;
                colvarDescription.IsReadOnly = false;
                colvarDescription.DefaultSetting = @"";
                colvarDescription.ForeignKeyTableName = "";
                schema.Columns.Add(colvarDescription);

                var colvarLongDescription = new TableSchema.TableColumn(schema);
                colvarLongDescription.ColumnName = "LongDescription";
                colvarLongDescription.DataType = DbType.String;
                colvarLongDescription.MaxLength = 512;
                colvarLongDescription.AutoIncrement = false;
                colvarLongDescription.IsNullable = false;
                colvarLongDescription.IsPrimaryKey = false;
                colvarLongDescription.IsForeignKey = false;
                colvarLongDescription.IsReadOnly = false;
                colvarLongDescription.DefaultSetting = @"";
                colvarLongDescription.ForeignKeyTableName = "";
                schema.Columns.Add(colvarLongDescription);

                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("StudyHistoryTypeEnum", schema);
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

        [XmlAttribute("EnumX")]
        [Bindable(true)]
        public short EnumX
        {
            get { return GetColumnValue<short>(Columns.EnumX); }
            set { SetColumnValue(Columns.EnumX, value); }
        }

        [XmlAttribute("Lookup")]
        [Bindable(true)]
        public string Lookup
        {
            get { return GetColumnValue<string>(Columns.Lookup); }
            set { SetColumnValue(Columns.Lookup, value); }
        }

        [XmlAttribute("Description")]
        [Bindable(true)]
        public string Description
        {
            get { return GetColumnValue<string>(Columns.Description); }
            set { SetColumnValue(Columns.Description, value); }
        }

        [XmlAttribute("LongDescription")]
        [Bindable(true)]
        public string LongDescription
        {
            get { return GetColumnValue<string>(Columns.LongDescription); }
            set { SetColumnValue(Columns.LongDescription, value); }
        }

        #endregion

        #region PrimaryKey Methods		

        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);

            SetPKValues();
        }


        public StudyHistoryCollection StudyHistoryRecords()
        {
            return new StudyHistoryCollection().Where(StudyHistory.Columns.StudyHistoryTypeEnum, EnumX).Load();
        }

        #endregion

        #region ObjectDataSource support

        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        public static void Insert(Guid varGuid, short varEnumX, string varLookup, string varDescription,
                                  string varLongDescription)
        {
            var item = new StudyHistoryTypeEnum();

            item.Guid = varGuid;

            item.EnumX = varEnumX;

            item.Lookup = varLookup;

            item.Description = varDescription;

            item.LongDescription = varLongDescription;


            if (HttpContext.Current != null)
                item.Save(HttpContext.Current.User.Identity.Name);
            else
                item.Save(Thread.CurrentPrincipal.Identity.Name);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        public static void Update(Guid varGuid, short varEnumX, string varLookup, string varDescription,
                                  string varLongDescription)
        {
            var item = new StudyHistoryTypeEnum();

            item.Guid = varGuid;

            item.EnumX = varEnumX;

            item.Lookup = varLookup;

            item.Description = varDescription;

            item.LongDescription = varLongDescription;

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


        public static TableSchema.TableColumn EnumXColumn
        {
            get { return Schema.Columns[1]; }
        }


        public static TableSchema.TableColumn LookupColumn
        {
            get { return Schema.Columns[2]; }
        }


        public static TableSchema.TableColumn DescriptionColumn
        {
            get { return Schema.Columns[3]; }
        }


        public static TableSchema.TableColumn LongDescriptionColumn
        {
            get { return Schema.Columns[4]; }
        }

        #endregion

        #region Columns Struct

        public struct Columns
        {
            public static string Guid = @"GUID";
            public static string EnumX = @"Enum";
            public static string Lookup = @"Lookup";
            public static string Description = @"Description";
            public static string LongDescription = @"LongDescription";
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

        //no foreign key tables defined (0)


        //no ManyToMany tables defined (0)
    }
}