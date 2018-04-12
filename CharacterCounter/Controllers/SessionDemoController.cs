using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CharacterCounter.Controllers
{
    public static class MySession
    {
        private static Dictionary<string, Dictionary<string, object>> _sessions 
             = new Dictionary<string, Dictionary<string, object>>();


        public static Dictionary<string, object> GetSession(string cookieValue)
        {
            return _sessions[cookieValue];
        }

        public static void AddSession(string key, Dictionary<string, object> session)
        {
            _sessions.Add(key, session);
        }

    }

    public class MyBaseController : Controller
    {
        private static Random random = new Random();
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Dictionary<string, object> GetSession()
        {
            if (Request.Cookies["custom-session"] != null)
            {
                return MySession.GetSession(Request.Cookies["custom-session"].Value);
            }

            Dictionary<string, object> session = new Dictionary<string, object>();
            string junk = RandomString(10);
            Response.Cookies.Add(new HttpCookie("custom-session", junk));
            
            MySession.AddSession(junk, session);
            return session;
        }
    }

    public class SessionDemoController : MyBaseController
    {
        public ActionResult Index()
        {
            int number = 0;
            Dictionary<string, object> session = GetSession();
            if (session.ContainsKey("number"))
            {
                number = (int)session["number"];
            }
            number++;
            session["number"] = number;
            return View(number);
        }
    }
}