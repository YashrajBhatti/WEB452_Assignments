using Microsoft.AspNetCore.Mvc;
using MvcBike.Models;

namespace MvcBike.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome(string name = "John", int num = 1)
        {
            ViewData["Message"] = "Hi " + name;
            ViewData["Number"] = num;
            return View();
        }

        private static List<TodoItem> todoItems = new List<TodoItem>();
        
        public IActionResult Todolist()
        {
            return View(todoItems);
        }

        
        [HttpPost]
        public IActionResult Todolist(int id,string title, string description)
        {
            var newItem = new TodoItem
            {
                Id = id,
                Title = title,
                Description = description
            };
            todoItems.Add(newItem); 

            return View(todoItems);
        }
    }
}
