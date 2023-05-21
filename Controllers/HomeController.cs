using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ViewResult Index()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                PostRepository.posts = PostRepository.GetPosts();
                return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
            }
            return View("~/Views/Home/LoginForm.cshtml");
        }

        public ViewResult About()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                PostRepository.posts = PostRepository.GetPosts();
                return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
            }
            else
                return View("~/Views/Home/About.cshtml");
        }

        public ViewResult Login ()
        {
            return View("~/Views/Home/LoginForm.cshtml");
        }

        [HttpPost]
        public ViewResult Login(string email, string password)
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                PostRepository.posts = PostRepository.GetPosts();
                return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
            }
            else
            {
                if (string.IsNullOrEmpty(email) == false && string.IsNullOrEmpty(password) == false)
                {

                    var obj = UserRepository.validUser(email, password);
                    User u = obj.Item2;
                    if (obj.Item1)
                    {
                        HttpContext.Session.SetInt32("UserId", u.Id);
                        HttpContext.Session.SetString("UserName", u.Username);
                        HttpContext.Session.SetString("UserEmail", u.Email);
                        HttpContext.Session.SetString("UserPassword", u.Password);
                        HttpContext.Session.SetString("UserProfilePicture", u.pictureFileName);
                        PostRepository.posts = PostRepository.GetPosts();
                        return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Account Don't Exist...");
                        return View("~/Views/Home/LoginForm.cshtml");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please Enter Correct Data");
                    return View("~/Views/Home/LoginForm.cshtml");
                }
            }
        }

        [HttpGet]
        public ViewResult SignupForm()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                PostRepository.posts = PostRepository.GetPosts();
                return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
            }
            else
                return View("~/Views/Home/SiqnupForm.cshtml");
        }


        [HttpPost]
        public ViewResult SignupForm(User u)
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                PostRepository.posts = PostRepository.GetPosts();
                return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (UserRepository.registerUser(u))
                    {
                        //PostRepository.posts = DBHandler.GetPosts();
                        //return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
                        ModelState.AddModelError(string.Empty, "Registred Successfully...");
                        return View("~/Views/Home/LoginForm.cshtml");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Registeration Failed...");
                        return View("~/Views/Home/SiqnupForm.cshtml");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please Enter Correct Data");
                    return View("~/Views/Home/SiqnupForm.cshtml");
                }
            }
        }









        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}