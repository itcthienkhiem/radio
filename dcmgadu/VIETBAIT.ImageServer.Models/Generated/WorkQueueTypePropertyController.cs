using System;
using System.ComponentModel;
using System.Threading;
using System.Web;
using SubSonic;

// <auto-generated />

namespace VIETBAIT.ImageServer.Models
{
    /// <summary>
    /// Controller class for WorkQueueTypeProperties
    /// </summary>
    [DataObject]
    public class WorkQueueTypePropertyController
    {
        // Preload our schema..
        private WorkQueueTypeProperty thisSchemaLoad = new WorkQueueTypeProperty();
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
        public WorkQueueTypePropertyCollection FetchAll()
        {
            var coll = new WorkQueueTypePropertyCollection();
            var qry = new Query(WorkQueueTypeProperty.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public WorkQueueTypePropertyCollection FetchByID(object Guid)
        {
            WorkQueueTypePropertyCollection coll = new WorkQueueTypePropertyCollection().Where("GUID", Guid).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public WorkQueueTypePropertyCollection FetchByQuery(Query qry)
        {
            var coll = new WorkQueueTypePropertyCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Guid)
        {
            return (WorkQueueTypeProperty.Delete(Guid) == 1);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Guid)
        {
            return (WorkQueueTypeProperty.Destroy(Guid) == 1);
        }


        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(Guid Guid, short WorkQueueTypeEnum, short WorkQueuePriorityEnum, bool MemoryLimited,
                           bool AlertFailedWorkQueue, int MaxFailureCount, int ProcessDelaySeconds,
                           int FailureDelaySeconds, int DeleteDelaySeconds, int PostponeDelaySeconds,
                           int ExpireDelaySeconds, int MaxBatchSize, short? QueueStudyStateEnum,
                           short? QueueStudyStateOrder, bool ReadLock, bool WriteLock)
        {
            var item = new WorkQueueTypeProperty();

            item.Guid = Guid;

            item.WorkQueueTypeEnum = WorkQueueTypeEnum;

            item.WorkQueuePriorityEnum = WorkQueuePriorityEnum;

            item.MemoryLimited = MemoryLimited;

            item.AlertFailedWorkQueue = AlertFailedWorkQueue;

            item.MaxFailureCount = MaxFailureCount;

            item.ProcessDelaySeconds = ProcessDelaySeconds;

            item.FailureDelaySeconds = FailureDelaySeconds;

            item.DeleteDelaySeconds = DeleteDelaySeconds;

            item.PostponeDelaySeconds = PostponeDelaySeconds;

            item.ExpireDelaySeconds = ExpireDelaySeconds;

            item.MaxBatchSize = MaxBatchSize;

            item.QueueStudyStateEnum = QueueStudyStateEnum;

            item.QueueStudyStateOrder = QueueStudyStateOrder;

            item.ReadLock = ReadLock;

            item.WriteLock = WriteLock;


            item.Save(UserName);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Guid Guid, short WorkQueueTypeEnum, short WorkQueuePriorityEnum, bool MemoryLimited,
                           bool AlertFailedWorkQueue, int MaxFailureCount, int ProcessDelaySeconds,
                           int FailureDelaySeconds, int DeleteDelaySeconds, int PostponeDelaySeconds,
                           int ExpireDelaySeconds, int MaxBatchSize, short? QueueStudyStateEnum,
                           short? QueueStudyStateOrder, bool ReadLock, bool WriteLock)
        {
            var item = new WorkQueueTypeProperty();
            item.MarkOld();
            item.IsLoaded = true;

            item.Guid = Guid;

            item.WorkQueueTypeEnum = WorkQueueTypeEnum;

            item.WorkQueuePriorityEnum = WorkQueuePriorityEnum;

            item.MemoryLimited = MemoryLimited;

            item.AlertFailedWorkQueue = AlertFailedWorkQueue;

            item.MaxFailureCount = MaxFailureCount;

            item.ProcessDelaySeconds = ProcessDelaySeconds;

            item.FailureDelaySeconds = FailureDelaySeconds;

            item.DeleteDelaySeconds = DeleteDelaySeconds;

            item.PostponeDelaySeconds = PostponeDelaySeconds;

            item.ExpireDelaySeconds = ExpireDelaySeconds;

            item.MaxBatchSize = MaxBatchSize;

            item.QueueStudyStateEnum = QueueStudyStateEnum;

            item.QueueStudyStateOrder = QueueStudyStateOrder;

            item.ReadLock = ReadLock;

            item.WriteLock = WriteLock;

            item.Save(UserName);
        }
    }
}