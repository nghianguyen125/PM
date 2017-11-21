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

namespace ProjectManagement.Controllers.Admin
{
    public class AdminController : BaseController
    {
        private ProjectManagementEntities db = new ProjectManagementEntities();
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            if (UserManager.Authenticated)
            {
                //string checkValid = Session["userNameXaGan"].ToString();
                if (App_Start.Classes.UserManager.Authenticated)
                {
                    return RedirectToAction("Logon", "Admin");
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
            if (UserManager.Authenticated)
            {
                HttpCookie authCookie =
                    System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null) authCookie.Expires = DateTime.Now.AddDays(-1);
                System.Web.Security.FormsAuthentication.SignOut();
                Session.Abandon();
                Session.Clear();
                Session.RemoveAll();
            }
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
                    Session["userNameXaGan"] = ss;
                    this.Session.Timeout = 60;

                    string data = Security.EncryptStringCbc(login.Single().Username + ";" + login.Single().TaiKhoanId, "dtuxagan");
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

                    return RedirectToAction("Index", "Admin");
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
        // This action for get Captcha Image
        //[HttpGet]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")] // This is for output cache false
        //public FileResult GetCaptchaImage()
        //{
        //    CaptchaRandomImage CI = new CaptchaRandomImage();
        //    this.Session["CaptchaImageText"] = CI.GetRandomString(5); // here 5 means I want to get 5 char long captcha
        //    //CI.GenerateImage(this.Session["CaptchaImageText"].ToString(), 300, 75);
        //    // Or We can use another one for get custom color Captcha Image 
        //    CI.GenerateImage(this.Session["CaptchaImageText"].ToString(), 300, 75, Color.DarkGray, Color.White);
        //    MemoryStream stream = new MemoryStream();
        //    CI.Image.Save(stream, ImageFormat.Png);
        //    stream.Seek(0, SeekOrigin.Begin);
        //    return new FileStreamResult(stream, "image/png");
        //}
        
        //Sample ASPX C# LineChart Class
        public Bitmap b;
        public string Title = "Default Title";
        public ArrayList chartValues = new ArrayList();
        public float Xorigin = 0, Yorigin = 0;
        public float ScaleX, ScaleY;
        public float Xdivs = 2, Ydivs = 2;
        private int Width, Height;
        private Graphics g;
        //private Page p;

        struct datapoint
        {
            public float x;
            public float y;
            public bool valid;
        }

        //initialize
        public void LineChart(int myWidth, int myHeight)//, Page myPage
        {
            Width = myWidth; Height = myHeight;
            ScaleX = myWidth; ScaleY = myHeight;
            b = new Bitmap(myWidth, myHeight);
            g = Graphics.FromImage(b);
            //p = myPage;
        }

        public void AddValue(int x, int y)
        {
            datapoint myPoint;
            myPoint.x = x;
            myPoint.y = y;
            myPoint.valid = true;
            chartValues.Add(myPoint);
        }
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")] // This is for output cache false
        public FileResult Draw()
        {
            LineChart(640, 480);
            Title = "Hit counter Chart";
            Xorigin = 0; ScaleX = 500; Xdivs = 5;
            Yorigin = 0; ScaleY = 1000; Ydivs = 5;
            AddValue(50, 50);
            AddValue(100, 100);
            AddValue(200, 150);
            AddValue(450, 450);

            int i;
            float x, y, x0, y0;
            string myLabel;
            Pen blackPen = new Pen(Color.Black, 1);
            Brush blackBrush = new SolidBrush(Color.Black);
            Font axesFont = new Font("arial", 10);

            //first establish working area
            //p.Response.ContentType = "image/jpeg";
            g.FillRectangle(new
            SolidBrush(Color.LightYellow), 0, 0, Width, Height);
            int ChartInset = 50;
            int ChartWidth = Width - (2 * ChartInset);
            int ChartHeight = Height - (2 * ChartInset);
            g.DrawRectangle(new Pen(Color.Black, 1), ChartInset, ChartInset, ChartWidth, ChartHeight);

            //must draw all text items before doing the rotate below
            g.DrawString(Title, new Font("arial", 14), blackBrush, Width / 3, 10);
            //draw X axis labels
            for (i = 0; i <= Xdivs; i++)
            {
                x = ChartInset + (i * ChartWidth) / Xdivs;
                y = ChartHeight + ChartInset;
                myLabel = (Xorigin + (ScaleX * i / Xdivs)).ToString();
                g.DrawString(myLabel, axesFont, blackBrush, x - 4, y + 10);
                g.DrawLine(blackPen, x, y + 2, x, y - 2);
            }
            //draw Y axis labels
            for (i = 0; i <= Ydivs; i++)
            {
                x = ChartInset;
                y = ChartHeight + ChartInset - (i * ChartHeight / Ydivs);
                myLabel = (Yorigin + (ScaleY * i / Ydivs)).ToString();
                g.DrawString(myLabel, axesFont, blackBrush, 5, y - 6);
                g.DrawLine(blackPen, x + 2, y, x - 2, y);
            }

            //transform drawing coords to lower-left (0,0)
            g.RotateTransform(180);
            g.TranslateTransform(0, -Height);
            g.TranslateTransform(-ChartInset, ChartInset);
            g.ScaleTransform(-1, 1);

            //draw chart data
            datapoint prevPoint = new datapoint();
            prevPoint.valid = false;
            foreach (datapoint myPoint in chartValues)
            {
                if (prevPoint.valid == true)
                {
                    x0 = ChartWidth * (prevPoint.x - Xorigin) / ScaleX;
                    y0 = ChartHeight * (prevPoint.y - Yorigin) / ScaleY;
                    x = ChartWidth * (myPoint.x - Xorigin) / ScaleX;
                    y = ChartHeight * (myPoint.y - Yorigin) / ScaleY;
                    g.DrawLine(blackPen, x0, y0, x, y);
                    g.FillEllipse(blackBrush, x0 - 2, y0 - 2, 4, 4);
                    g.FillEllipse(blackBrush, x - 2, y - 2, 4, 4);
                }
                prevPoint = myPoint;
            }

            //finally send graphics to browser
            //b.Save(p.Response.OutputStream, ImageFormat.Jpeg);
            MemoryStream stream = new MemoryStream();
            b.Save(stream, ImageFormat.Jpeg);
            stream.Seek(0, SeekOrigin.Begin);
            var vvv = new FileStreamResult(stream, "image/jpeg");
            return vvv;
        }

    }
}