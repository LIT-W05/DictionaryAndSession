using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CharacterCounter.Models;

namespace CharacterCounter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string text)
        {
            HomePageViewModel viewModel = new HomePageViewModel();
            if (!String.IsNullOrEmpty(text))
            {
                viewModel.Text = text;
                viewModel.CharacterCounts = CountCharacters(text);
            }
            return View(viewModel);
        }

        private Dictionary<char, int> CountCharacters(string text)
        {
            Dictionary<char, int> counts = new Dictionary<char, int>();
            foreach (char c in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
            {
                counts.Add(c, 0);
            }

            foreach (char c in text.ToUpper())
            {
                if (counts.ContainsKey(c))
                {
                    counts[c]++;
                }
            }

            return counts;
        }
    }
}