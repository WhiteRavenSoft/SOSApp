using Microsoft.Owin.Security;
using Newtonsoft.Json;
using SOSApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SOSApp.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var getTokenUrl = ConfigurationManager.AppSettings["Api.Base.Login"];

                using (HttpClient httpClient = new HttpClient())
                {
                    HttpContent content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", "basic"),
                        new KeyValuePair<string, string>("email", model.Email),
                        new KeyValuePair<string, string>("password", model.Password)
                    });

                    HttpResponseMessage result = httpClient.PostAsync(getTokenUrl, content).Result;

                    string resultContent = result.Content.ReadAsStringAsync().Result;

                    var token = JsonConvert.DeserializeObject<TokenModel>(resultContent);

                    AuthenticationProperties options = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddSeconds(int.Parse(token.expires_in))
                    };

                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Email, model.Email),
                        new Claim(ClaimTypes.GivenName, token.given_name),
                        new Claim("AcessToken", string.Format("Bearer {0}", token.access_token)),
                    };

                    Session.Add("access_token", token.access_token);
                    Session.Add("given_name", token.given_name);

                    var identity = new ClaimsIdentity(claims, "ApplicationCookie");

                    Request.GetOwinContext().Authentication.SignIn(options, identity);

                }
            }

            if (returnUrl == string.Empty || returnUrl == null)
                return RedirectToAction("Index", "News");

            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "News");
        }

        public ActionResult Logout()
        {
            Session.Remove("access_token");
            Request.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "News");
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
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

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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