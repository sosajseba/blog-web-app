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
                PostList oPosts = new PostList();
                return View(oPosts.GetPosts());
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
                PostList oPosts = new PostList();
                return View(oPosts.GetDetails(id));
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
                if (ModelState.IsValid)
                {
                    PostList oPosts = new PostList();
                    oPosts.CreatePost(model);                    
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                PostList oPosts = new PostList();
                return View(oPosts.GetPostForEdit(id));
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
                if (ModelState.IsValid)
                {
                    PostList oPosts = new PostList();
                    oPosts.EditPost(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                PostList oPosts = new PostList();
                oPosts.DeletePost(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return RedirectToAction("NotFound", "Error");
            }
        }
        public ActionResult CategoryList()
        {
            try
            {
                CategoryList oCat = new CategoryList();
                return PartialView(oCat.GetPosts());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
