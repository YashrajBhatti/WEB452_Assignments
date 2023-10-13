using Microsoft.AspNetCore.Mvc;
using WEB452_Assignments.Models; // Include the necessary model

namespace MyAssignmentApp.Controllers
{
    public class SampleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Todolist()
        {
            return View();
        }
    }
}
