using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using H5pro.Models;

namespace H5pro.Controllers
{
    public class SearchController : Controller
    {
        DataClassDataContext db = new DataClassDataContext();

        // GET: Search
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(SearchHandler sh)
        {
            int len = 0;
            List<ResultHandler> results = new List<ResultHandler>();
            List<User> users = new List<User>();
            if (sh.userName != null)
            {
                users = db.Users.Where(u => u.Username.Contains(sh.userName)).ToList();
            }
            else
            {
                int minAge = 18;
                int maxAge = 99;

                if (sh.minAge > minAge)
                {
                    minAge = sh.minAge;
                }
                if (sh.maxAge < maxAge)
                {
                    maxAge = sh.maxAge;
                }

                users = db.Users.ToList();
                users = users.Where(u => u.Age.CompareTo(minAge - 1).Equals(1)).ToList();
                users = users.Where(u => u.Age.CompareTo(maxAge + 1).Equals(-1)).ToList();

                User user = users[0];

                int test = user.Age.CompareTo(minAge);

                if (!(sh.male == sh.female == sh.other))
                {
                    if (!sh.male)
                    {
                        users.RemoveAll(u => u.Gender == 0);
                    }
                    if (!sh.female)
                    {
                        users.RemoveAll(u => u.Gender == 1);
                    }
                    if (!sh.other)
                    {
                        users.RemoveAll(u => u.Gender == 2);
                    }
                }
            }
            len = users.Count();
            if (len > 0)
            {
                List<string> genders = new List<string>(new string[] { "Male", "Female", "Other" });
                List<string> commits = new List<string>(new string[] { "Just watch", "Watch and talk", "Netflix and chill", "Something serious" });
                List<string> service = new List<string>(new string[] { "Netflix", "HBO", "Other" });
                for (int i = 0; i < len; i++)
                {
                    MatchCriteria match = db.MatchCriterias.Where(m => m.MCID.Equals(users[i].MCID)).FirstOrDefault();
                    List<MCShowRelation> relations = db.MCShowRelations.Where(r => r.Equals(users[i].MCID)).ToList();
                    string shows = "";
                    if (relations != null)
                    {
                        for (int j = 0; (j < 3) && (j < relations.Count()); j++)
                        {
                            Show show = db.Shows.Where(s => s.ShowID.Equals(relations[j].ShowID)).FirstOrDefault();
                            shows = shows + ", " + show;
                        }
                    }

                    ResultHandler result = new ResultHandler();
                    result.userID = users[i].UserID;
                    result.userName = users[i].Username;
                    result.age = users[i].Age;
                    result.gender = genders[users[i].Gender];
                    if(match != null)
                    {
                        if (match.Commitment != null)
                        {
                            int k = (int)match.Commitment;
                            result.commitment = commits[k];
                        }
                        if (match.StreamingServices != null)
                        {
                            int k = (int)match.StreamingServices;
                            result.streamingServices = service[k];
                        }
                    }
                    result.shows = shows;
                    results.Add(result);
                }
            }
            return View("SearchResult", results);
        }

        public ActionResult SearchResult(List<ResultHandler> result)
        {
            return View(result);
        }


    }
}