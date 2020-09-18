using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogWebApp.Models;

namespace BlogWebApp.Controllers
{
    public class PostsController : Controller
    {
        // GET: Posts
        public ActionResult Index()
        {
            try
            {
                using (BlogDBEntities db = new BlogDBEntities())
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
                                      CreationDate = p.creation_date
                                  });

                    return View(oPosts.ToList());
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Posts/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                using (BlogDBEntities db = new BlogDBEntities())
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
                        return View(oPosts.ToList());
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return RedirectToAction("NotFound", "Error");
            }
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        public ActionResult Create(CreatePost model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    using (BlogDBEntities db = new BlogDBEntities())
                    {
                        var oPost = new post();
                        oPost.title = model.Title;
                        oPost.post_content = model.Content;
                        oPost.image = model.Image;
                        oPost.id_category = model.Category.id;
                        oPost.creation_date = model.CreationDate;

                        db.post.Add(oPost);
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index");

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
                throw new Exception(dbEx.Message);
            }
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int id)
        {
            CreatePost model = new CreatePost();

            try
            {
                using (BlogDBEntities db = new BlogDBEntities())
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

                return View(model);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return RedirectToAction("NotFound", "Error");
            }
        }

        // POST: Posts/Edit/5
        [HttpPost]
        public ActionResult Edit(CreatePost model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    using (BlogDBEntities db = new BlogDBEntities())
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

                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // GET: Posts/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                using (BlogDBEntities db = new BlogDBEntities())
                {
                    var oPost = db.post.Find(id);

                    if (oPost == null)
                    {
                        throw new Exception();
                    }

                    db.post.Remove(oPost);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return RedirectToAction("NotFound", "Error");
            }
        }

        // POST: Posts/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CategoryList()
        {
            try
            {
                using (BlogDBEntities db = new BlogDBEntities())
                {
                    var oCategory = (from c in db.category
                                     select new Categorys()
                                     {
                                         Id = c.id,
                                         Name = c.name
                                     });

                    return PartialView(oCategory.ToList());
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
