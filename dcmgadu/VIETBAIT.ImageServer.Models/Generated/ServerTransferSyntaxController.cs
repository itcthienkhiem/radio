using System;
using System.ComponentModel;
using System.Threading;
using System.Web;
using SubSonic;

// <auto-generated />

namespace VIETBAIT.ImageServer.Models
{
    /// <summary>
    /// Controller class for ServerTransferSyntax
    /// </summary>
    [DataObject]
    public class ServerTransferSyntaxController
    {
        // Preload our schema..
        private ServerTransferSyntax thisSchemaLoad = new ServerTransferSyntax();
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
        public ServerTransferSyntaxCollection FetchAll()
        {
            var coll = new ServerTransferSyntaxCollection();
            var qry = new Query(ServerTransferSyntax.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public ServerTransferSyntaxCollection FetchByID(object Guid)
        {
            ServerTransferSyntaxCollection coll = new ServerTransferSyntaxCollection().Where("GUID", Guid).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public ServerTransferSyntaxCollection FetchByQuery(Query qry)
        {
            var coll = new ServerTransferSyntaxCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Guid)
        {
            return (ServerTransferSyntax.Delete(Guid) == 1);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Guid)
        {
            return (ServerTransferSyntax.Destroy(Guid) == 1);
        }


        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(Guid Guid, string Uid, string Description, bool Lossless)
        {
            var item = new ServerTransferSyntax();

            item.Guid = Guid;

            item.Uid = Uid;

            item.Description = Description;

            item.Lossless = Lossless;


            item.Save(UserName);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Guid Guid, string Uid, string Description, bool Lossless)
        {
            var item = new ServerTransferSyntax();
            item.MarkOld();
            item.IsLoaded = true;

            item.Guid = Guid;

            item.Uid = Uid;

            item.Description = Description;

            item.Lossless = Lossless;

            item.Save(UserName);
        }
    }
}