using System;
using System.ComponentModel;
using System.Threading;
using System.Web;
using SubSonic;

// <auto-generated />

namespace VIETBAIT.ImageServer.Models
{
    /// <summary>
    /// Controller class for StudyHistory
    /// </summary>
    [DataObject]
    public class StudyHistoryController
    {
        // Preload our schema..
        private StudyHistory thisSchemaLoad = new StudyHistory();
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
        public StudyHistoryCollection FetchAll()
        {
            var coll = new StudyHistoryCollection();
            var qry = new Query(StudyHistory.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public StudyHistoryCollection FetchByID(object Guid)
        {
            StudyHistoryCollection coll = new StudyHistoryCollection().Where("GUID", Guid).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public StudyHistoryCollection FetchByQuery(Query qry)
        {
            var coll = new StudyHistoryCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Guid)
        {
            return (StudyHistory.Delete(Guid) == 1);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Guid)
        {
            return (StudyHistory.Destroy(Guid) == 1);
        }


        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(Guid Guid, DateTime InsertTime, Guid StudyStorageGUID, Guid? DestStudyStorageGUID,
                           short StudyHistoryTypeEnum, string StudyData, string ChangeDescription)
        {
            var item = new StudyHistory();

            item.Guid = Guid;

            item.InsertTime = InsertTime;

            item.StudyStorageGUID = StudyStorageGUID;

            item.DestStudyStorageGUID = DestStudyStorageGUID;

            item.StudyHistoryTypeEnum = StudyHistoryTypeEnum;

            item.StudyData = StudyData;

            item.ChangeDescription = ChangeDescription;


            item.Save(UserName);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Guid Guid, DateTime InsertTime, Guid StudyStorageGUID, Guid? DestStudyStorageGUID,
                           short StudyHistoryTypeEnum, string StudyData, string ChangeDescription)
        {
            var item = new StudyHistory();
            item.MarkOld();
            item.IsLoaded = true;

            item.Guid = Guid;

            item.InsertTime = InsertTime;

            item.StudyStorageGUID = StudyStorageGUID;

            item.DestStudyStorageGUID = DestStudyStorageGUID;

            item.StudyHistoryTypeEnum = StudyHistoryTypeEnum;

            item.StudyData = StudyData;

            item.ChangeDescription = ChangeDescription;

            item.Save(UserName);
        }
    }
}