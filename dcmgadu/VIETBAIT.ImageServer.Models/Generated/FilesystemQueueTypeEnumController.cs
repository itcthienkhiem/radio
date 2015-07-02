using System;
using System.ComponentModel;
using System.Threading;
using System.Web;
using SubSonic;

// <auto-generated />

namespace VIETBAIT.ImageServer.Models
{
    /// <summary>
    /// Controller class for FilesystemQueueTypeEnum
    /// </summary>
    [DataObject]
    public class FilesystemQueueTypeEnumController
    {
        // Preload our schema..
        private FilesystemQueueTypeEnum thisSchemaLoad = new FilesystemQueueTypeEnum();
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
        public FilesystemQueueTypeEnumCollection FetchAll()
        {
            var coll = new FilesystemQueueTypeEnumCollection();
            var qry = new Query(FilesystemQueueTypeEnum.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public FilesystemQueueTypeEnumCollection FetchByID(object EnumX)
        {
            FilesystemQueueTypeEnumCollection coll = new FilesystemQueueTypeEnumCollection().Where("Enum", EnumX).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public FilesystemQueueTypeEnumCollection FetchByQuery(Query qry)
        {
            var coll = new FilesystemQueueTypeEnumCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object EnumX)
        {
            return (FilesystemQueueTypeEnum.Delete(EnumX) == 1);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object EnumX)
        {
            return (FilesystemQueueTypeEnum.Destroy(EnumX) == 1);
        }


        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(Guid Guid, short EnumX, string Lookup, string Description, string LongDescription)
        {
            var item = new FilesystemQueueTypeEnum();

            item.Guid = Guid;

            item.EnumX = EnumX;

            item.Lookup = Lookup;

            item.Description = Description;

            item.LongDescription = LongDescription;


            item.Save(UserName);
        }

        /// <summary>
        /// Updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Guid Guid, short EnumX, string Lookup, string Description, string LongDescription)
        {
            var item = new FilesystemQueueTypeEnum();
            item.MarkOld();
            item.IsLoaded = true;

            item.Guid = Guid;

            item.EnumX = EnumX;

            item.Lookup = Lookup;

            item.Description = Description;

            item.LongDescription = LongDescription;

            item.Save(UserName);
        }
    }
}