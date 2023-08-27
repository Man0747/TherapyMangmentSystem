using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TherapyMangmentSystem.Models;
using TherapyMangmentSystem.Services;

namespace TherapyMangmentSystem.Controllers
{
    //[Authorize(Roles = "patient")]
    //[Authorize(Roles = "admin")]


    public class PatientController : Controller
    {
        [Authorize(Roles = "admin,patient")]
        public ActionResult Index()
        {
            PatientOPS patientops = new PatientOPS();
            ModelState.Clear();
            return View(patientops.GetPatients());
        }
        // 2. *************ADD NEW THERAPIST ******************
        // GET: Therapist/Create
        [Authorize(Roles = "admin,patient")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Therapist/Create
        [HttpPost]
        [Authorize(Roles = "admin,patient")]
        public ActionResult Create(PatientModel patientmodel)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                patientmodel.Email = HttpContext.Session.GetString("Login");
                    PatientOPS patientops = new PatientOPS();
                    if (patientops.AddPatient(patientmodel))
                    {
                        ViewBag.Message = "Patient Details Added Successfully";
                        ModelState.Clear();
                    }
                //}
                return View();
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            PatientOPS patientops = new PatientOPS();
            PatientModel patientmodel = patientops.GetPatient(id);

            return View(patientmodel);

        }
        
        //POST: Student/Edit/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(PatientModel patientmodel)
        {
            try
            {
                PatientOPS patientops = new PatientOPS();
                patientops.UpdatePatient(patientmodel);
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
        //GET: TherapistController/Delete/5     
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                PatientOPS patientops = new PatientOPS();
                if (patientops.DeletePatient(id))
                {
                    ViewBag.AlertMsg = "Patient Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }

}