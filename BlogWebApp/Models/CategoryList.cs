using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogWebApp.Models
{
    public class CategoryList
    {
        public IEnumerable<Categorys> GetPosts()
        {
            try
            {
                using (BlogEntities db = new BlogEntities())
                {
                    var oCategory = (from c in db.category
                                     select new Categorys()
                                     {
                                         Id = c.id,
                                         Name = c.name
                                     });

                    return oCategory.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}