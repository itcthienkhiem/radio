using System;
using System.ComponentModel;
using System.Threading;
using System.Web;
using SubSonic;

// <auto-generated />

namespace VIETBAIT.ImageServer.Models
{
    /// <summary>
    /// Controller class for WorkQueue
    /// </summary>
    [DataObject]
    public class WorkQueueController
    {
        // Preload our schema..
        private WorkQueue thisSchemaLoad = new WorkQueue();
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
        public WorkQueueCollection FetchAll()
        {
            var coll = new WorkQueueCollection();
            var qry = new Query(WorkQueue.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public WorkQueueCollection FetchByID(object Guid)
        {
            WorkQueueCollection coll = new WorkQueueCollection().Where("GUID", Guid).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public WorkQueueCollection FetchByQuery(Query qry)
        {
            var coll = new WorkQueueCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Guid)
        {
            return (WorkQueue.Delete(Guid) == 1);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Guid)
        {
            return (WorkQueue.Destroy(Guid) == 1);
        }


        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(Guid Guid, Guid ServerPartitionGUID, Guid StudyStorageGUID, Guid? DeviceGUID,
                           Guid? StudyHistoryGUID, short WorkQueueTypeEnum, short WorkQueueStatusEnum,
                           short WorkQueuePriorityEnum, string ProcessorID, string GroupID, DateTime? ExpirationTime,
                           DateTime ScheduledTime, DateTime InsertTime, DateTime? LastUpdatedTime, int FailureCount,
                           string FailureDescription, string Data)
        {
            var item = new WorkQueue();

            item.Guid = Guid;

            item.ServerPartitionGUID = ServerPartitionGUID;

            item.StudyStorageGUID = StudyStorageGUID;

            item.DeviceGUID = DeviceGUID;

            item.StudyHistoryGUID = StudyHistoryGUID;

            item.WorkQueueTypeEnum = WorkQueueTypeEnum;

            item.WorkQueueStatusEnum = WorkQueueStatusEnum;

            item.WorkQueuePriorityEnum = WorkQueuePriorityEnum;

            item.ProcessorID = ProcessorID;

            item.GroupID = GroupID;

            item.ExpirationTime = ExpirationTime;

            item.ScheduledTime = ScheduledTime;

            item.InsertTime = InsertTime;

            item.LastUpdatedTime = LastUpdatedTime;

            item.FailureCount = FailureCount;

            item.FailureDescription = FailureDescription;

            item.Data = Data;


            item.Save(UserName);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Guid Guid, Guid ServerPartitionGUID, Guid StudyStorageGUID, Guid? DeviceGUID,
                           Guid? StudyHistoryGUID, short WorkQueueTypeEnum, short WorkQueueStatusEnum,
                           short WorkQueuePriorityEnum, string ProcessorID, string GroupID, DateTime? ExpirationTime,
                           DateTime ScheduledTime, DateTime InsertTime, DateTime? LastUpdatedTime, int FailureCount,
                           string FailureDescription, string Data)
        {
            var item = new WorkQueue();
            item.MarkOld();
            item.IsLoaded = true;

            item.Guid = Guid;

            item.ServerPartitionGUID = ServerPartitionGUID;

            item.StudyStorageGUID = StudyStorageGUID;

            item.DeviceGUID = DeviceGUID;

            item.StudyHistoryGUID = StudyHistoryGUID;

            item.WorkQueueTypeEnum = WorkQueueTypeEnum;

            item.WorkQueueStatusEnum = WorkQueueStatusEnum;

            item.WorkQueuePriorityEnum = WorkQueuePriorityEnum;

            item.ProcessorID = ProcessorID;

            item.GroupID = GroupID;

            item.ExpirationTime = ExpirationTime;

            item.ScheduledTime = ScheduledTime;

            item.InsertTime = InsertTime;

            item.LastUpdatedTime = LastUpdatedTime;

            item.FailureCount = FailureCount;

            item.FailureDescription = FailureDescription;

            item.Data = Data;

            item.Save(UserName);
        }
    }
}