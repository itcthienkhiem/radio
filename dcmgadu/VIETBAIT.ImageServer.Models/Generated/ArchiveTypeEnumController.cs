using System;
using System.ComponentModel;
using System.Threading;
using System.Web;
using SubSonic;

// <auto-generated />

namespace VIETBAIT.ImageServer.Models
{
    /// <summary>
    /// Controller class for ArchiveTypeEnum
    /// </summary>
    [DataObject]
    public class ArchiveTypeEnumController
    {
        // Preload our schema..
        private ArchiveTypeEnum thisSchemaLoad = new ArchiveTypeEnum();
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
        public ArchiveTypeEnumCollection FetchAll()
        {
            var coll = new ArchiveTypeEnumCollection();
            var qry = new Query(ArchiveTypeEnum.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public ArchiveTypeEnumCollection FetchByID(object EnumX)
        {
            ArchiveTypeEnumCollection coll = new ArchiveTypeEnumCollection().Where("Enum", EnumX).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public ArchiveTypeEnumCollection FetchByQuery(Query qry)
        {
            var coll = new ArchiveTypeEnumCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object EnumX)
        {
            return (ArchiveTypeEnum.Delete(EnumX) == 1);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object EnumX)
        {
            return (ArchiveTypeEnum.Destroy(EnumX) == 1);
        }


        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(Guid Guid, short EnumX, string Lookup, string Description, string LongDescription)
        {
            var item = new ArchiveTypeEnum();

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
            var item = new ArchiveTypeEnum();
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