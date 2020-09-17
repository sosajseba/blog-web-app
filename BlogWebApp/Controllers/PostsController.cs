using System;
using System.Collections.Generic;
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
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Posts/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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
    }
}
