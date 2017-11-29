using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectManagement.Models;
using System.Web.Mvc;

namespace ProjectManagement.Controllers.User
{
    public class MainPageController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();
        // GET: MainPage
        public ActionResult Index()
        {
            return View(db.QuanLyLiches.ToList());
        }
    }
}