using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WidaUserDirectory.Models;

namespace WidaUserDirectory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: Users
        public IActionResult Index()
        {
            IEnumerable<UserModel>? users = null;

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                //HTTP GET
                var responseTask = client.GetAsync("users");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<UserModel>>();
                    readTask.Wait();

                    users = readTask.Result;
                }
                else
                {
                    users = Enumerable.Empty<UserModel>();

                    ModelState.AddModelError(string.Empty, "Server error");
                }
            }

            return View(users);
        }

        public IActionResult UserDetail(int id)
        {
            UserDetailModel userDetail = new UserDetailModel();

            // get user info
            userDetail.User = GetUser(id);
            // get user todo list
            userDetail.ToDo = GetToDos(id);

            return View(userDetail);
        }

        public UserModel GetUser (int id)
        {
            UserModel user = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                //HTTP GET
                var responseTask = client.GetAsync("users/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UserModel>();
                    readTask.Wait();

                    user = readTask.Result;
                }
            }

            return user;
        }

        public IEnumerable<ToDo> GetToDos (int id)
        {
            IEnumerable<ToDo> toDos = null;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                //HTTP GET
                var responseTask = client.GetAsync("users/" + id.ToString() + "/todos");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var todoReadTask = result.Content.ReadAsAsync<IList<ToDo>>();
                    todoReadTask.Wait();

                    toDos = todoReadTask.Result;
                }
            }

            return toDos;
        }

        [HttpPost]
        public ActionResult userDetail(UserModel user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<UserModel>("user", user);
                putTask.Wait();

                var result = putTask.Result;
                //if (result.IsSuccessStatusCode)
                //{

                    return RedirectToAction("Index");
                //}
            }
            return View(user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}