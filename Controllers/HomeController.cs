using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Webtask.Data;
using Webtask.Models;


namespace Webtask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _ctx;

        public HomeController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        // get Method in Post


        public IActionResult Addperson()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Addperson(Home home)
        {
            if (!ModelState.IsValid)
            {
                return View();


            }
            try
            {
                _ctx.ListTable.Add(home);
                _ctx.SaveChanges();
                TempData["Msg"] = "Added Sucessfylly";
                return RedirectToAction("Addperson");
            }
            catch (Exception ex)
            {

                TempData["msg"] = "Could Not Added!!";
                return View();
            }
        }

        public IActionResult Displayperson(int page = 1, int pagesize=5)
        {
            //var person = _ctx.ListTable.ToList();

            var totalItems = _ctx.ListTable.OrderBy(p=>p.Name).Skip((page - 1) * pagesize).Take(pagesize).ToList();
            // Total item count
                   
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)_ctx.ListTable.Count() / pagesize);
            return View(totalItems);



        }



        public IActionResult UpdatePerson(int id)
        {
            var person = _ctx.ListTable.Find(id);
            return View(person);
        }
        [HttpPost]
        public IActionResult UpdatePerson(Home home)
        {
            if (!ModelState.IsValid)
            {
                return View();


            }
            try
            {
                _ctx.ListTable.Update(home);
                _ctx.SaveChanges();
                TempData["Msg"] = "Added Sucessfylly";
                return RedirectToAction("DisplayPerson");
            }
            catch (Exception ex)
            {

                TempData["msg"] = "Could Not Added!!";
                return View();
            }

        }
        public IActionResult DeletePerson(int id)
        {
            var person = _ctx.ListTable.Find(id);
            return View(person);
        }
        [HttpPost]

        public IActionResult DeletePerson(Home del)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            try
            {
                _ctx.ListTable.Remove(del);
                _ctx.SaveChanges();
                TempData["msg"] = "Delete Sucessfully";
                return RedirectToAction("DisplayPerson");

            }
            catch
            {
                TempData["msg"] = "Could not Delete";
                return View();
            }
        }

        // //

        public IActionResult User()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult User(string Name, string EmailAddress)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(EmailAddress))
            {
                TempData["msg"] = " EmailAddress and Password are required.";
                return View();
            }

            try
            {
                // Assuming your database context is _ctx and user data is stored in a table called Users
                var users = _ctx.ListTable.FirstOrDefault(u => u.EmailAddress == EmailAddress);

                if (users == null)
                {
                    TempData["msg"] = "Invalid Username.";
                    return View();
                }
                if (users.Name != Name) // Replace with hashed password comparison if applicable
                {
                    TempData["msg"] = "Invalid Name.";
                    return View();
                }
                else
                {
                    TempData["msg"] = "Login Successful!";
                    return RedirectToAction("DisplayPerson"); // Redirect to the desired page
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "An error occurred: " + ex.Message;
                return View();
            }

        }
    }
}









