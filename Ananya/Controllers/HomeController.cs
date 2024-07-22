using Ananya.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ananya.Controllers
{
    public class HomeController : Controller
    {
 
        //show data from database
        private readonly NTPCBD2Entities _context;

        public HomeController()
        {
            _context = new NTPCBD2Entities();
        }
        


        //LOGIN PAGE

        NTPCBD2Entities db = new NTPCBD2Entities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Tbl_user log)
        {

            var user = db.Tbl_user.FirstOrDefault(x => x.Empno == log.Empno && x.Password == log.Password);

            if (user != null)
            {
                int role = (int)user.Role;
                string name = user.Name;
                if (role == 1)
                {
                    // Redirect to page for role 1
                    return RedirectToAction("IdeaSubmitAdmin", new { name = name });
                }
                else if (role == 0)
                {
                    // Redirect to page for role 0
                    return RedirectToAction("IdeaSubmitUser", new { name = name });
                }
            }
            ModelState.AddModelError("", "Incorrect Employee Number or Password. Try again."); // Add error message
            return View("Login");



        }

        //IDEA PORTAL
        public ActionResult IdeaPortal()
        {
            ViewBag.saveresult = "";
            return View();
        }

        [HttpPost]
        public ActionResult IdeaPortal(Tbl_idea data, HttpPostedFileBase pdfFile)
        {
            using (NTPCBD2Entities entities = new NTPCBD2Entities())
            {
                //save pdf 

                try
                {
                    if (pdfFile != null && pdfFile.ContentLength > 0)
                    {
                        // Generate a unique filename (you can use GUID or other methods)
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(pdfFile.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/Uploads"), fileName);

                        // Save the file to the server
                        pdfFile.SaveAs(filePath);

                        // Set the file path in your Tbl_idea model property (assuming you have a property for the PDF path)
                        data.Attachment = filePath;
                    }


                    entities.Tbl_idea.Add(data);
                    entities.SaveChanges();
                    ViewBag.saveresult = "data has been saved successfully";
                    ModelState.Clear();
                }
                catch (DbEntityValidationException ex)
                {
                    // Iterate through the validation errors to get more details
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            // Here you can access specific validation errors
                            var propertyName = validationError.PropertyName;
                            var errorMessage = validationError.ErrorMessage;
                            // Handle or log the errors as needed
                            ModelState.AddModelError(propertyName, errorMessage);
                        }
                    }
                }
                return View(new Tbl_idea());
            }
        }

        public ActionResult JqueryTable()
        {
            return View();
        }



        public ActionResult Index()
        {
            return View();
        }

        

        public ActionResult Contact()
        {
            
            
            return View();
        }
        

        public ActionResult IdeaSubmitUser(string name)
        {

            ViewBag.Name = name;
            var customers = _context.Tbl_idea.ToList();
            return View(customers);
            
        }

        public ActionResult IdeaSubmitAdmin(string name)
        {
            ViewBag.Name = name;
            var customers = _context.Tbl_idea.ToList();
            return View(customers);
        }
        public ActionResult Project1()
        {

            return View();
        }
        public ActionResult Project2()
        {

            return View();
        }
        public ActionResult Project3()
        {

            return View();
        }
        public ActionResult Project4()
        {

            return View();
        }
        public ActionResult Project5()
        {

            return View();
        }
        public ActionResult Project6()
        {

            return View();
        }
        public ActionResult Project7()
        {

            return View();
        }
        public ActionResult Project8()
        {

            return View();
        }

        public ActionResult Report()
           
        {

            return View();
        }


        

    }
}