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
    /// Strongly-typed collection for the ServerRule class.
    /// </summary>
    [Serializable]
    public class ServerRuleCollection : ActiveList<ServerRule, ServerRuleCollection>
    {
        /// <summary>
        /// Filters an existing collection based on the set criteria. This is an in-memory filter
        /// Thanks to developingchris for this!
        /// </summary>
        /// <returns>ServerRuleCollection</returns>
        public ServerRuleCollection Filter()
        {
            for (int i = Count - 1; i > -1; i--)
            {
                ServerRule o = this[i];
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
    /// This is an ActiveRecord class which wraps the ServerRule table.
    /// </summary>
    [Serializable]
    public class ServerRule : ActiveRecord<ServerRule>, IActiveRecord
    {
        #region .ctors and Default Settings

        public ServerRule()
        {
            SetSQLProps();
            InitSetDefaults();
            MarkNew();
        }

        public ServerRule(bool useDatabaseDefaults)
        {
            SetSQLProps();
            if (useDatabaseDefaults)
                ForceDefaults();
            MarkNew();
        }

        public ServerRule(object keyID)
        {
            SetSQLProps();
            InitSetDefaults();
            LoadByKey(keyID);
        }

        public ServerRule(string columnName, object columnValue)
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
                var schema = new TableSchema.Table("ServerRule", TableType.Table, DataService.GetInstance("ORM"));
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

                var colvarRuleName = new TableSchema.TableColumn(schema);
                colvarRuleName.ColumnName = "RuleName";
                colvarRuleName.DataType = DbType.String;
                colvarRuleName.MaxLength = 128;
                colvarRuleName.AutoIncrement = false;
                colvarRuleName.IsNullable = false;
                colvarRuleName.IsPrimaryKey = false;
                colvarRuleName.IsForeignKey = false;
                colvarRuleName.IsReadOnly = false;
                colvarRuleName.DefaultSetting = @"";
                colvarRuleName.ForeignKeyTableName = "";
                schema.Columns.Add(colvarRuleName);

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

                var colvarServerRuleTypeEnum = new TableSchema.TableColumn(schema);
                colvarServerRuleTypeEnum.ColumnName = "ServerRuleTypeEnum";
                colvarServerRuleTypeEnum.DataType = DbType.Int16;
                colvarServerRuleTypeEnum.MaxLength = 0;
                colvarServerRuleTypeEnum.AutoIncrement = false;
                colvarServerRuleTypeEnum.IsNullable = false;
                colvarServerRuleTypeEnum.IsPrimaryKey = false;
                colvarServerRuleTypeEnum.IsForeignKey = true;
                colvarServerRuleTypeEnum.IsReadOnly = false;
                colvarServerRuleTypeEnum.DefaultSetting = @"";

                colvarServerRuleTypeEnum.ForeignKeyTableName = "ServerRuleTypeEnum";
                schema.Columns.Add(colvarServerRuleTypeEnum);

                var colvarServerRuleApplyTimeEnum = new TableSchema.TableColumn(schema);
                colvarServerRuleApplyTimeEnum.ColumnName = "ServerRuleApplyTimeEnum";
                colvarServerRuleApplyTimeEnum.DataType = DbType.Int16;
                colvarServerRuleApplyTimeEnum.MaxLength = 0;
                colvarServerRuleApplyTimeEnum.AutoIncrement = false;
                colvarServerRuleApplyTimeEnum.IsNullable = false;
                colvarServerRuleApplyTimeEnum.IsPrimaryKey = false;
                colvarServerRuleApplyTimeEnum.IsForeignKey = true;
                colvarServerRuleApplyTimeEnum.IsReadOnly = false;
                colvarServerRuleApplyTimeEnum.DefaultSetting = @"";

                colvarServerRuleApplyTimeEnum.ForeignKeyTableName = "ServerRuleApplyTimeEnum";
                schema.Columns.Add(colvarServerRuleApplyTimeEnum);

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

                var colvarDefaultRule = new TableSchema.TableColumn(schema);
                colvarDefaultRule.ColumnName = "DefaultRule";
                colvarDefaultRule.DataType = DbType.Boolean;
                colvarDefaultRule.MaxLength = 0;
                colvarDefaultRule.AutoIncrement = false;
                colvarDefaultRule.IsNullable = false;
                colvarDefaultRule.IsPrimaryKey = false;
                colvarDefaultRule.IsForeignKey = false;
                colvarDefaultRule.IsReadOnly = false;
                colvarDefaultRule.DefaultSetting = @"";
                colvarDefaultRule.ForeignKeyTableName = "";
                schema.Columns.Add(colvarDefaultRule);

                var colvarExemptRule = new TableSchema.TableColumn(schema);
                colvarExemptRule.ColumnName = "ExemptRule";
                colvarExemptRule.DataType = DbType.Boolean;
                colvarExemptRule.MaxLength = 0;
                colvarExemptRule.AutoIncrement = false;
                colvarExemptRule.IsNullable = false;
                colvarExemptRule.IsPrimaryKey = false;
                colvarExemptRule.IsForeignKey = false;
                colvarExemptRule.IsReadOnly = false;

                colvarExemptRule.DefaultSetting = @"((0))";
                colvarExemptRule.ForeignKeyTableName = "";
                schema.Columns.Add(colvarExemptRule);

                var colvarRuleXml = new TableSchema.TableColumn(schema);
                colvarRuleXml.ColumnName = "RuleXml";
                colvarRuleXml.DataType = DbType.AnsiString;
                colvarRuleXml.MaxLength = -1;
                colvarRuleXml.AutoIncrement = false;
                colvarRuleXml.IsNullable = false;
                colvarRuleXml.IsPrimaryKey = false;
                colvarRuleXml.IsForeignKey = false;
                colvarRuleXml.IsReadOnly = false;
                colvarRuleXml.DefaultSetting = @"";
                colvarRuleXml.ForeignKeyTableName = "";
                schema.Columns.Add(colvarRuleXml);

                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["ORM"].AddSchema("ServerRule", schema);
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

        [XmlAttribute("RuleName")]
        [Bindable(true)]
        public string RuleName
        {
            get { return GetColumnValue<string>(Columns.RuleName); }
            set { SetColumnValue(Columns.RuleName, value); }
        }

        [XmlAttribute("ServerPartitionGUID")]
        [Bindable(true)]
        public Guid ServerPartitionGUID
        {
            get { return GetColumnValue<Guid>(Columns.ServerPartitionGUID); }
            set { SetColumnValue(Columns.ServerPartitionGUID, value); }
        }

        [XmlAttribute("ServerRuleTypeEnum")]
        [Bindable(true)]
        public short ServerRuleTypeEnum
        {
            get { return GetColumnValue<short>(Columns.ServerRuleTypeEnum); }
            set { SetColumnValue(Columns.ServerRuleTypeEnum, value); }
        }

        [XmlAttribute("ServerRuleApplyTimeEnum")]
        [Bindable(true)]
        public short ServerRuleApplyTimeEnum
        {
            get { return GetColumnValue<short>(Columns.ServerRuleApplyTimeEnum); }
            set { SetColumnValue(Columns.ServerRuleApplyTimeEnum, value); }
        }

        [XmlAttribute("Enabled")]
        [Bindable(true)]
        public bool Enabled
        {
            get { return GetColumnValue<bool>(Columns.Enabled); }
            set { SetColumnValue(Columns.Enabled, value); }
        }

        [XmlAttribute("DefaultRule")]
        [Bindable(true)]
        public bool DefaultRule
        {
            get { return GetColumnValue<bool>(Columns.DefaultRule); }
            set { SetColumnValue(Columns.DefaultRule, value); }
        }

        [XmlAttribute("ExemptRule")]
        [Bindable(true)]
        public bool ExemptRule
        {
            get { return GetColumnValue<bool>(Columns.ExemptRule); }
            set { SetColumnValue(Columns.ExemptRule, value); }
        }

        [XmlAttribute("RuleXml")]
        [Bindable(true)]
        public string RuleXml
        {
            get { return GetColumnValue<string>(Columns.RuleXml); }
            set { SetColumnValue(Columns.RuleXml, value); }
        }

        #endregion

        #region ForeignKey Properties

        /// <summary>
        /// Returns a ServerPartition ActiveRecord object related to this ServerRule
        /// 
        /// </summary>
        public ServerPartition ServerPartition
        {
            get { return ServerPartition.FetchByID(ServerPartitionGUID); }
            set { SetColumnValue("ServerPartitionGUID", value.Guid); }
        }


        /// <summary>
        /// Returns a ServerRuleApplyTimeEnum ActiveRecord object related to this ServerRule
        /// 
        /// </summary>
        public ServerRuleApplyTimeEnum ServerRuleApplyTimeEnumRecord
        {
            get { return Models.ServerRuleApplyTimeEnum.FetchByID(ServerRuleApplyTimeEnum); }
            set { SetColumnValue("ServerRuleApplyTimeEnum", value.EnumX); }
        }


        /// <summary>
        /// Returns a ServerRuleTypeEnum ActiveRecord object related to this ServerRule
        /// 
        /// </summary>
        public ServerRuleTypeEnum ServerRuleTypeEnumRecord
        {
            get { return Models.ServerRuleTypeEnum.FetchByID(ServerRuleTypeEnum); }
            set { SetColumnValue("ServerRuleTypeEnum", value.EnumX); }
        }

        #endregion

        #region ObjectDataSource support

        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        public static void Insert(Guid varGuid, string varRuleName, Guid varServerPartitionGUID,
                                  short varServerRuleTypeEnum, short varServerRuleApplyTimeEnum, bool varEnabled,
                                  bool varDefaultRule, bool varExemptRule, string varRuleXml)
        {
            var item = new ServerRule();

            item.Guid = varGuid;

            item.RuleName = varRuleName;

            item.ServerPartitionGUID = varServerPartitionGUID;

            item.ServerRuleTypeEnum = varServerRuleTypeEnum;

            item.ServerRuleApplyTimeEnum = varServerRuleApplyTimeEnum;

            item.Enabled = varEnabled;

            item.DefaultRule = varDefaultRule;

            item.ExemptRule = varExemptRule;

            item.RuleXml = varRuleXml;


            if (HttpContext.Current != null)
                item.Save(HttpContext.Current.User.Identity.Name);
            else
                item.Save(Thread.CurrentPrincipal.Identity.Name);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        public static void Update(Guid varGuid, string varRuleName, Guid varServerPartitionGUID,
                                  short varServerRuleTypeEnum, short varServerRuleApplyTimeEnum, bool varEnabled,
                                  bool varDefaultRule, bool varExemptRule, string varRuleXml)
        {
            var item = new ServerRule();

            item.Guid = varGuid;

            item.RuleName = varRuleName;

            item.ServerPartitionGUID = varServerPartitionGUID;

            item.ServerRuleTypeEnum = varServerRuleTypeEnum;

            item.ServerRuleApplyTimeEnum = varServerRuleApplyTimeEnum;

            item.Enabled = varEnabled;

            item.DefaultRule = varDefaultRule;

            item.ExemptRule = varExemptRule;

            item.RuleXml = varRuleXml;

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


        public static TableSchema.TableColumn RuleNameColumn
        {
            get { return Schema.Columns[1]; }
        }


        public static TableSchema.TableColumn ServerPartitionGUIDColumn
        {
            get { return Schema.Columns[2]; }
        }


        public static TableSchema.TableColumn ServerRuleTypeEnumColumn
        {
            get { return Schema.Columns[3]; }
        }


        public static TableSchema.TableColumn ServerRuleApplyTimeEnumColumn
        {
            get { return Schema.Columns[4]; }
        }


        public static TableSchema.TableColumn EnabledColumn
        {
            get { return Schema.Columns[5]; }
        }


        public static TableSchema.TableColumn DefaultRuleColumn
        {
            get { return Schema.Columns[6]; }
        }


        public static TableSchema.TableColumn ExemptRuleColumn
        {
            get { return Schema.Columns[7]; }
        }


        public static TableSchema.TableColumn RuleXmlColumn
        {
            get { return Schema.Columns[8]; }
        }

        #endregion

        #region Columns Struct

        public struct Columns
        {
            public static string Guid = @"GUID";
            public static string RuleName = @"RuleName";
            public static string ServerPartitionGUID = @"ServerPartitionGUID";
            public static string ServerRuleTypeEnum = @"ServerRuleTypeEnum";
            public static string ServerRuleApplyTimeEnum = @"ServerRuleApplyTimeEnum";
            public static string Enabled = @"Enabled";
            public static string DefaultRule = @"DefaultRule";
            public static string ExemptRule = @"ExemptRule";
            public static string RuleXml = @"RuleXml";
        }

        #endregion

        #region Update PK Collections

        #endregion

        #region Deep Save

        #endregion

        //no ManyToMany tables defined (0)
    }
}