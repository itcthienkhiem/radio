using System;
using System.ComponentModel;
using System.Threading;
using System.Web;
using SubSonic;

// <auto-generated />

namespace VIETBAIT.ImageServer.Models
{
    /// <summary>
    /// Controller class for AlertLevelEnum
    /// </summary>
    [DataObject]
    public class AlertLevelEnumController
    {
        // Preload our schema..
        private AlertLevelEnum thisSchemaLoad = new AlertLevelEnum();
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
        public AlertLevelEnumCollection FetchAll()
        {
            var coll = new AlertLevelEnumCollection();
            var qry = new Query(AlertLevelEnum.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public AlertLevelEnumCollection FetchByID(object EnumX)
        {
            AlertLevelEnumCollection coll = new AlertLevelEnumCollection().Where("Enum", EnumX).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public AlertLevelEnumCollection FetchByQuery(Query qry)
        {
            var coll = new AlertLevelEnumCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object EnumX)
        {
            return (AlertLevelEnum.Delete(EnumX) == 1);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object EnumX)
        {
            return (AlertLevelEnum.Destroy(EnumX) == 1);
        }


        /// <summary>
        /// Inserts a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(Guid Guid, short EnumX, string Lookup, string Description, string LongDescription)
        {
            var item = new AlertLevelEnum();

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
            var item = new AlertLevelEnum();
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