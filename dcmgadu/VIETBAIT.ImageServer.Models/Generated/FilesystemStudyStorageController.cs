using System;
using System.ComponentModel;
using System.Threading;
using System.Web;
using SubSonic;

// <auto-generated />

namespace VIETBAIT.ImageServer.Models
{
    /// <summary>
    /// Controller class for FilesystemStudyStorage
    /// </summary>
    [DataObject]
    public class FilesystemStudyStorageController
    {
        // Preload our schema..
        private FilesystemStudyStorage thisSchemaLoad = new FilesystemStudyStorage();
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
        public FilesystemStudyStorageCollection FetchAll()
        {
            var coll = new FilesystemStudyStorageCollection();
            var qry = new Query(FilesystemStudyStorage.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public FilesystemStudyStorageCollection FetchByID(object Guid)
        {
            FilesystemStudyStorageCollection coll = new FilesystemStudyStorageCollection().Where("GUID", Guid).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public FilesystemStudyStorageCollection FetchByQuery(Query qry)
        {
            var coll = new FilesystemStudyStorageCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Guid)
        {
            return (FilesystemStudyStorage.Delete(Guid) == 1);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Guid)
        {
            return (FilesystemStudyStorage.Destroy(Guid) == 1);
        }


        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(Guid Guid, Guid StudyStorageGUID, Guid FilesystemGUID, Guid ServerTransferSyntaxGUID,
                           string StudyFolder)
        {
            var item = new FilesystemStudyStorage();

            item.Guid = Guid;

            item.StudyStorageGUID = StudyStorageGUID;

            item.FilesystemGUID = FilesystemGUID;

            item.ServerTransferSyntaxGUID = ServerTransferSyntaxGUID;

            item.StudyFolder = StudyFolder;


            item.Save(UserName);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Guid Guid, Guid StudyStorageGUID, Guid FilesystemGUID, Guid ServerTransferSyntaxGUID,
                           string StudyFolder)
        {
            var item = new FilesystemStudyStorage();
            item.MarkOld();
            item.IsLoaded = true;

            item.Guid = Guid;

            item.StudyStorageGUID = StudyStorageGUID;

            item.FilesystemGUID = FilesystemGUID;

            item.ServerTransferSyntaxGUID = ServerTransferSyntaxGUID;

            item.StudyFolder = StudyFolder;

            item.Save(UserName);
        }
    }
}