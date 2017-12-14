using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
//using SRVTextToImage;
using ProjectManagement.App_Start.Classes;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers.User
{
    public class UserController : Controller
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();
        //
        // GET: /User/
        public ActionResult Index()
        {
            
            if (UserManager.Authenticated)
            {
                //string checkValid = Session["userNameXaGan"].ToString();
                if (App_Start.Classes.UserManager.Authenticated)
                {
                    return RedirectToAction("Logon", "User");
                }

                HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null) authCookie.Expires = DateTime.Now.AddDays(-1);
                System.Web.Security.FormsAuthentication.SignOut();
                Session.Abandon();
                Session.Clear();
                Session.RemoveAll();

                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")] // This is for output cache false
        public ActionResult Login()
        {
            //if (UserManager.Authenticated)
            //{
            //    HttpCookie authCookie =
            //        System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            //    if (authCookie != null) authCookie.Expires = DateTime.Now.AddDays(-1);
            //    System.Web.Security.FormsAuthentication.SignOut();
            //    Session.Abandon();
            //    Session.Clear();
            //    Session.RemoveAll();
            //}
            HttpCookie authCookie =
                    System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null) authCookie.Expires = DateTime.Now.AddDays(-1);
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return View();
        }
        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")] // This is for output cache false
        public ActionResult Login([Bind(Include = "Username,Password")] TaiKhoan user, string captchaText)
        {
            if (ModelState.IsValid)
            {
                string ps = Security.EncryptSha1(Security.EncryptMd5(user.Password).ToLower());
                var login = from u in db.TaiKhoans
                            where u.Username == user.Username && u.Password == ps
                            select u;
                if (login.Any())
                {
                    string ss = Security.EncryptSha1(Security.EncryptMd5(login.Single().Username + "#" + login.Single().Password).ToLower());
                    this.Session.Timeout = 60;

                    string data = Security.EncryptStringCbc(login.Single().Username + ";" + login.Single().TaiKhoanId, "ProjectManagement");
                    HttpCookie authCookie = FormsAuthentication.GetAuthCookie(data, false);
                    var ticket = FormsAuthentication.Decrypt(authCookie.Value);

                    if (ticket != null)
                    {
                        var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name,
                                                                      ticket.IssueDate, ticket.Expiration,
                                                                      ticket.IsPersistent, "");

                        //Update the authCookie's Value to use the encrypted version of newTicket. 
                        authCookie.Value = FormsAuthentication.Encrypt(newTicket);
                    }
                    authCookie.Expires = DateTime.Now.AddMinutes(60);
                    Response.Cookies.Add(authCookie);

                    var tk = db.TaiKhoans.Where(n => n.Username == user.Username).SingleOrDefault();
                    
                    //if (tk.SinhVienId != null)
                    //{
                    //    ViewBag.SinhVienId = tk.SinhVienId;
                    //    return RedirectToAction("Index", "MainPage");
                    //}

                    if(tk.GiangVienId != null)
                    {
                        ViewBag.GiangVienId = tk.GiangVienId;
                        return RedirectToAction("Index", "MainPage", new { GVId = tk.GiangVienId });
                    }
                    if(tk.SinhVienId != null)
                    {
                        ViewBag.SinhVienId = tk.SinhVienId;
                        return RedirectToAction("Index", "MainPage", new { SVId = tk.SinhVienId });
                    }

                    
                }
                else
                {
                    ModelState.AddModelError("error", "Không tồn tại tài khoản hoặc Tài khoản chưa kích hoạt!");
                }
            }

            return View();
        }
        public ActionResult Logout()
        {
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null) authCookie.Expires = DateTime.Now.AddDays(-1);
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Login");
        }

        public ActionResult KhongXoa()
        {
            return View();
        }
        public ActionResult Logon()
        {
            if (!UserManager.Authenticated)
            {
                return RedirectToAction("Login");
            }
            //decimal aCount = db.ARTICLEs.Count();
            //ViewBag.NumberOfArticles = aCount;
            //var listArticesLatest = db.ARTICLEs.Take(20).OrderByDescending(a => a.ARTICLE_CREATEDATE).ToList();
            //ViewBag.listArticesLatest = listArticesLatest;
            return View();
        }
    }
}