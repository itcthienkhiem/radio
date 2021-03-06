using System;
using System.ComponentModel;
using System.Threading;
using System.Web;
using SubSonic;

// <auto-generated />

namespace VIETBAIT.ImageServer.Models
{
    /// <summary>
    /// Controller class for ApplicationLog
    /// </summary>
    [DataObject]
    public class ApplicationLogController
    {
        // Preload our schema..
        private ApplicationLog thisSchemaLoad = new ApplicationLog();
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
        public ApplicationLogCollection FetchAll()
        {
            var coll = new ApplicationLogCollection();
            var qry = new Query(ApplicationLog.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public ApplicationLogCollection FetchByID(object Guid)
        {
            ApplicationLogCollection coll = new ApplicationLogCollection().Where("GUID", Guid).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public ApplicationLogCollection FetchByQuery(Query qry)
        {
            var coll = new ApplicationLogCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Guid)
        {
            return (ApplicationLog.Delete(Guid) == 1);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Guid)
        {
            return (ApplicationLog.Destroy(Guid) == 1);
        }


        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(Guid Guid, string Host, DateTime Timestamp, string LogLevel, string Thread, string Message,
                           string Exception)
        {
            var item = new ApplicationLog();

            item.Guid = Guid;

            item.Host = Host;

            item.Timestamp = Timestamp;

            item.LogLevel = LogLevel;

            item.Thread = Thread;

            item.Message = Message;

            item.Exception = Exception;


            item.Save(UserName);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Guid Guid, string Host, DateTime Timestamp, string LogLevel, string Thread, string Message,
                           string Exception)
        {
            var item = new ApplicationLog();
            item.MarkOld();
            item.IsLoaded = true;

            item.Guid = Guid;

            item.Host = Host;

            item.Timestamp = Timestamp;

            item.LogLevel = LogLevel;

            item.Thread = Thread;

            item.Message = Message;

            item.Exception = Exception;

            item.Save(UserName);
        }
    }
}