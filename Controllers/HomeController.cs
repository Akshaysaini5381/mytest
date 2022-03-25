using mytest.db_context;
using mytest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace mytest.Controllers
{
    public class HomeController : Controller
    {
        mydbEntities eobj = new mydbEntities();
        [HttpGet]
        public ActionResult Login()
        {
         
            return View();
        }
        public ActionResult singout()
        {
            return RedirectToAction("Login", "Home");
           
        }
        [HttpPost]
        public ActionResult Login(Login_Class obdj)
        {


            mydbEntities dobj = new mydbEntities();
            var UserRes = dobj.login1.Where(m => m.Email == obdj.Email).FirstOrDefault();
            if (UserRes == null)
            {
                TempData["Invalid"] = "Email not found";
            }
            else
            {
                if (UserRes.Email == obdj.Email && UserRes.Password == obdj.Password)
                {
                    FormsAuthentication.SetAuthCookie(UserRes.Email, false);
                    Session["UserName"] = UserRes.Name;
                    return RedirectToAction("Index", "Home");
                    
                }
                else
                {
                    TempData["Wrong"] = "Wrong Password";
                    return View();
                }

            }
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Table()
        {
            
            var sh = eobj.tables.ToList();


            return View(sh);
        }

        [HttpGet]
        public ActionResult Form()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Form(Class1 cobj)
        {

            table tobj = new table();

            tobj.id = cobj.id;
            tobj.name = cobj.name;
            tobj.email = cobj.email;
            if (cobj.id == 0)
            {
                eobj.tables.Add(tobj);
                eobj.SaveChanges();
            }
            else {

                eobj.Entry(cobj).State = System.Data.Entity.EntityState.Modified;
                eobj.SaveChanges();
            }


            return RedirectToAction("Table");
        }

        public ActionResult Delete(int id)
        {
            var del = eobj.tables.Where(m => m.id == id).First();
            eobj.tables.Remove(del);
            eobj.SaveChanges();

            return RedirectToAction("Table");
        }


        public ActionResult Edit(int id)
        {
            var editi = eobj.tables.Where(m => m.id == id).First();
            Class1 cobj = new Class1();
            table tobj = new table();
            tobj.id = editi.id;
            tobj.name = editi.name;
            tobj.email = editi.email;

            return View("Form" , tobj);
        }
    }




}