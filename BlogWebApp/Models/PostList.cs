using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace BlogWebApp.Models
{
    public class PostList
    {
        public IEnumerable<Posts> GetPosts()
        {
            try
            {
                using (BlogEntities db = new BlogEntities())
                {
                    var oPosts = (from p in db.post
                                  join c in db.category
                                  on p.id_category equals c.id
                                  orderby p.creation_date descending
                                  select new Posts()
                                  {
                                      Id = p.id,
                                      Title = p.title,
                                      Image = p.image,
                                      Category = c.name,
                                      CreationDate = p.creation_date,
                                      IsActive = p.is_active
                                  });

                    return oPosts.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Posts> GetDetails(int id)
        {
            try
            {
                using (BlogEntities db = new BlogEntities())
                {
                    var oPosts = (from p in db.post
                                  join c in db.category
                                  on p.id_category equals c.id
                                  where p.id == id
                                  select new Posts()
                                  {
                                      Id = p.id,
                                      Title = p.title,
                                      Content = p.post_content,
                                      Image = p.image,
                                      Category = c.name,
                                      CreationDate = p.creation_date
                                  });

                    if (oPosts.Count() == 1)
                    {
                        return oPosts.ToList();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void CreatePost(CreatePost model)
        {
            try
            {
                using (BlogEntities db = new BlogEntities())
                {
                    var oPost = new post();
                    oPost.title = model.Title;
                    oPost.post_content = model.Content;
                    oPost.image = model.Image;
                    oPost.id_category = model.Category.id;
                    oPost.creation_date = model.CreationDate;
                    oPost.is_active = true;

                    db.post.Add(oPost);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CreatePost GetPostForEdit(int id)
        {
            CreatePost model = new CreatePost();

            try
            {
                using (BlogEntities db = new BlogEntities())
                {
                    var oPost = db.post.Find(id);

                    if (oPost == null)
                    {
                        throw new Exception();
                    }

                    var oCat = db.category.Find(oPost.id_category);
                    model.Title = oPost.title;
                    model.Content = oPost.post_content;
                    model.Image = oPost.image;
                    model.Category = oCat;
                    model.CreationDate = oPost.creation_date;
                }

                return model;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void EditPost(CreatePost model)
        {
            try
            {
                using (BlogEntities db = new BlogEntities())
                {
                    var oPost = db.post.Find(model.Id);
                    oPost.title = model.Title;
                    oPost.post_content = model.Content;
                    oPost.image = model.Image;
                    oPost.id_category = model.Category.id;

                    db.Entry(oPost).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeletePost(int id)
        {
            try
            {
                using (BlogEntities db = new BlogEntities())
                {
                    var oPost = db.post.Find(id);

                    if (oPost == null)
                    {
                        throw new Exception();
                    }

                    oPost.is_active = false;

                    db.Entry(oPost).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}