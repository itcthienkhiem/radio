using System;
using System.ComponentModel;
using System.Threading;
using System.Web;
using SubSonic;

// <auto-generated />

namespace VIETBAIT.ImageServer.Models
{
    /// <summary>
    /// Controller class for PartitionArchive
    /// </summary>
    [DataObject]
    public class PartitionArchiveController
    {
        // Preload our schema..
        private PartitionArchive thisSchemaLoad = new PartitionArchive();
        private string userName = String.Empty;

        protected string UserName
        {
            get
            {
                if (userName.Length == 0)
                {
                    if (HttpContext.Current != null)
                    {
                        userName = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        userName = Thread.CurrentPrincipal.Identity.Name;
                    }
                }
                return userName;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public PartitionArchiveCollection FetchAll()
        {
            var coll = new PartitionArchiveCollection();
            var qry = new Query(PartitionArchive.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public PartitionArchiveCollection FetchByID(object Guid)
        {
            PartitionArchiveCollection coll = new PartitionArchiveCollection().Where("GUID", Guid).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public PartitionArchiveCollection FetchByQuery(Query qry)
        {
            var coll = new PartitionArchiveCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Guid)
        {
            return (PartitionArchive.Delete(Guid) == 1);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Guid)
        {
            return (PartitionArchive.Destroy(Guid) == 1);
        }


        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(Guid Guid, Guid ServerPartitionGUID, short ArchiveTypeEnum, string Description, bool Enabled,
                           bool ReadOnlyX, int ArchiveDelayHours, string ConfigurationXml)
        {
            var item = new PartitionArchive();

            item.Guid = Guid;

            item.ServerPartitionGUID = ServerPartitionGUID;

            item.ArchiveTypeEnum = ArchiveTypeEnum;

            item.Description = Description;

            item.Enabled = Enabled;

            item.ReadOnlyX = ReadOnlyX;

            item.ArchiveDelayHours = ArchiveDelayHours;

            item.ConfigurationXml = ConfigurationXml;


            item.Save(UserName);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Guid Guid, Guid ServerPartitionGUID, short ArchiveTypeEnum, string Description, bool Enabled,
                           bool ReadOnlyX, int ArchiveDelayHours, string ConfigurationXml)
        {
            var item = new PartitionArchive();
            item.MarkOld();
            item.IsLoaded = true;

            item.Guid = Guid;

            item.ServerPartitionGUID = ServerPartitionGUID;

            item.ArchiveTypeEnum = ArchiveTypeEnum;

            item.Description = Description;

            item.Enabled = Enabled;

            item.ReadOnlyX = ReadOnlyX;

            item.ArchiveDelayHours = ArchiveDelayHours;

            item.ConfigurationXml = ConfigurationXml;

            item.Save(UserName);
        }
    }
}