using System;
using System.ComponentModel;
using System.Threading;
using System.Web;
using SubSonic;

// <auto-generated />

namespace VIETBAIT.ImageServer.Models
{
    /// <summary>
    /// Controller class for WorkQueueUid
    /// </summary>
    [DataObject]
    public class WorkQueueUidController
    {
        // Preload our schema..
        private WorkQueueUid thisSchemaLoad = new WorkQueueUid();
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
        public WorkQueueUidCollection FetchAll()
        {
            var coll = new WorkQueueUidCollection();
            var qry = new Query(WorkQueueUid.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public WorkQueueUidCollection FetchByID(object Guid)
        {
            WorkQueueUidCollection coll = new WorkQueueUidCollection().Where("GUID", Guid).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public WorkQueueUidCollection FetchByQuery(Query qry)
        {
            var coll = new WorkQueueUidCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Guid)
        {
            return (WorkQueueUid.Delete(Guid) == 1);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Guid)
        {
            return (WorkQueueUid.Destroy(Guid) == 1);
        }


        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(Guid Guid, Guid WorkQueueGUID, string SeriesInstanceUid, string SopInstanceUid, bool Failed,
                           bool Duplicate, string Extension, short FailureCount, string GroupID, string RelativePath)
        {
            var item = new WorkQueueUid();

            item.Guid = Guid;

            item.WorkQueueGUID = WorkQueueGUID;

            item.SeriesInstanceUid = SeriesInstanceUid;

            item.SopInstanceUid = SopInstanceUid;

            item.Failed = Failed;

            item.Duplicate = Duplicate;

            item.Extension = Extension;

            item.FailureCount = FailureCount;

            item.GroupID = GroupID;

            item.RelativePath = RelativePath;


            item.Save(UserName);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Guid Guid, Guid WorkQueueGUID, string SeriesInstanceUid, string SopInstanceUid, bool Failed,
                           bool Duplicate, string Extension, short FailureCount, string GroupID, string RelativePath)
        {
            var item = new WorkQueueUid();
            item.MarkOld();
            item.IsLoaded = true;

            item.Guid = Guid;

            item.WorkQueueGUID = WorkQueueGUID;

            item.SeriesInstanceUid = SeriesInstanceUid;

            item.SopInstanceUid = SopInstanceUid;

            item.Failed = Failed;

            item.Duplicate = Duplicate;

            item.Extension = Extension;

            item.FailureCount = FailureCount;

            item.GroupID = GroupID;

            item.RelativePath = RelativePath;

            item.Save(UserName);
        }
    }
}