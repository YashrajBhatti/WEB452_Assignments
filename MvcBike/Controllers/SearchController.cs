using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcBike.Models;


public class SearchController : Controller
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