using System;
using System.ComponentModel;
using System.Threading;
using System.Web;
using SubSonic;

// <auto-generated />

namespace VIETBAIT.ImageServer.Models
{
    /// <summary>
    /// Controller class for Device
    /// </summary>
    [DataObject]
    public class DeviceController
    {
        // Preload our schema..
        private Device thisSchemaLoad = new Device();
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
        public DeviceCollection FetchAll()
        {
            var coll = new DeviceCollection();
            var qry = new Query(Device.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DeviceCollection FetchByID(object Guid)
        {
            DeviceCollection coll = new DeviceCollection().Where("GUID", Guid).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DeviceCollection FetchByQuery(Query qry)
        {
            var coll = new DeviceCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Guid)
        {
            return (Device.Delete(Guid) == 1);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Guid)
        {
            return (Device.Destroy(Guid) == 1);
        }


        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(Guid Guid, Guid ServerPartitionGUID, string AeTitle, string IpAddress, int Port,
                           string Description, bool Dhcp, bool Enabled, bool AllowStorage, bool AcceptKOPR,
                           bool AllowRetrieve, bool AllowQuery, bool AllowAutoRoute, short ThrottleMaxConnections,
                           DateTime LastAccessedTime, short DeviceTypeEnum)
        {
            var item = new Device();

            item.Guid = Guid;

            item.ServerPartitionGUID = ServerPartitionGUID;

            item.AeTitle = AeTitle;

            item.IpAddress = IpAddress;

            item.Port = Port;

            item.Description = Description;

            item.Dhcp = Dhcp;

            item.Enabled = Enabled;

            item.AllowStorage = AllowStorage;

            item.AcceptKOPR = AcceptKOPR;

            item.AllowRetrieve = AllowRetrieve;

            item.AllowQuery = AllowQuery;

            item.AllowAutoRoute = AllowAutoRoute;

            item.ThrottleMaxConnections = ThrottleMaxConnections;

            item.LastAccessedTime = LastAccessedTime;

            item.DeviceTypeEnum = DeviceTypeEnum;


            item.Save(UserName);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Guid Guid, Guid ServerPartitionGUID, string AeTitle, string IpAddress, int Port,
                           string Description, bool Dhcp, bool Enabled, bool AllowStorage, bool AcceptKOPR,
                           bool AllowRetrieve, bool AllowQuery, bool AllowAutoRoute, short ThrottleMaxConnections,
                           DateTime LastAccessedTime, short DeviceTypeEnum)
        {
            var item = new Device();
            item.MarkOld();
            item.IsLoaded = true;

            item.Guid = Guid;

            item.ServerPartitionGUID = ServerPartitionGUID;

            item.AeTitle = AeTitle;

            item.IpAddress = IpAddress;

            item.Port = Port;

            item.Description = Description;

            item.Dhcp = Dhcp;

            item.Enabled = Enabled;

            item.AllowStorage = AllowStorage;

            item.AcceptKOPR = AcceptKOPR;

            item.AllowRetrieve = AllowRetrieve;

            item.AllowQuery = AllowQuery;

            item.AllowAutoRoute = AllowAutoRoute;

            item.ThrottleMaxConnections = ThrottleMaxConnections;

            item.LastAccessedTime = LastAccessedTime;

            item.DeviceTypeEnum = DeviceTypeEnum;

            item.Save(UserName);
        }
    }
}