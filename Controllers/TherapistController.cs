using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TherapyMangmentSystem.Models;
using TherapyMangmentSystem.Services;



namespace TherapyMangmentSystem.Controllers
{
    

    public class TherapistController : Controller
    {   // 1. *************GET THERAPIST ******************
        // GET: Therapist
        [Authorize(Roles = "admin,therapist")]
        
        public ActionResult Index()
        {
            TherapistOPS therapistops = new TherapistOPS();
            ModelState.Clear();
            return View(therapistops.GetTherapists());
        }
        // 2. *************ADD NEW THERAPIST ******************
        // GET: Therapist/Create
        //[Authorize(Roles = "admin,therapist")]
        [Authorize(Roles = "admin,therapist")]
        public ActionResult Create()
        {

            TherapistOPS therapistops = new TherapistOPS();
            List<TherapistSpecialityModel> therapistspecialitymodel = therapistops.AddSpecialityInListBox();
            List<TherapistPracticeModel> therapistpracticemodel = therapistops.AddPracticeInListBox();
            ViewBag.TherapistSpecialities = therapistspecialitymodel;
            ViewBag.TherapistPractices = therapistpracticemodel;
            ViewBag.Therapist = new TherapistLoginModel();
            return View();
        }

        
        // POST: Therapist/Create
        [HttpPost]
        [Authorize(Roles = "admin,therapist")]
        public ActionResult Create(TherapistModel therapistmodel)
        {
            try
            {
                //if (ModelState.IsValid)
                {
                    therapistmodel.Email = HttpContext.Session.GetString("Login");
                    TherapistOPS therapistops = new TherapistOPS();
                    if (therapistops.AddTherapist(therapistmodel))
                    {
                        ViewBag.Message = "Therapist Details Added Successfully";
                        ModelState.Clear();
                    }
                    List<TherapistSpecialityModel> therapistspecialitymodel = therapistops.AddSpecialityInListBox();
                    List<TherapistPracticeModel> therapistpracticemodel = therapistops.AddPracticeInListBox();
                    ViewBag.TherapistSpecialities = therapistspecialitymodel;
                    ViewBag.TherapistPractices = therapistpracticemodel;
                    //}
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        //GET: Student/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            TherapistOPS therapistops = new TherapistOPS();
            TherapistLoginModel therapistloginmodel = therapistops.GetTherapist(id);
            
            List<TherapistSpecialityModel> therapistspecialitymodel = therapistops.AddSpecialityInListBox();
            List<TherapistPracticeModel> therapistpracticemodel = therapistops.AddPracticeInListBox();
            ViewBag.TherapistSpecialities = therapistspecialitymodel;
            ViewBag.TherapistPractices = therapistpracticemodel;
            ViewBag.Therapist = new TherapistLoginModel();
            return View(therapistloginmodel);

        }

        //POST: Student/Edit/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(TherapistLoginModel therapistloginmodel)
        {
            try
            {
                TherapistOPS therapistops = new TherapistOPS();
                therapistops.UpdateTherapist(therapistloginmodel);
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
                TherapistOPS therapistOPS = new TherapistOPS();
                if (therapistOPS.DeleteTherapist(id))
                {
                    ViewBag.AlertMsg = "Student Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin,therapist")]
        // In your controller action
        public ActionResult Schedule()
        {
            //TherapistOPS therapistops = new TherapistOPS();
            //var scheduleList = therapistops.GetScheduleTherapist();
            //return View(scheduleList);
            return View();
        }


        // POST: Therapist/Create
        [HttpPost]
        [Authorize(Roles = "admin,therapist")]
        [HttpPost]
        public ActionResult Schedule([FromBody] SlotDataModel slotDataModel)
        {
            try
            {   
                slotDataModel.Therapist_Id= Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
                TherapistOPS therapistops = new TherapistOPS();
                if (therapistops.AddSchedule(slotDataModel))
                {
                    ViewBag.Message = "Therapist Details Added Successfully";
                    ModelState.Clear();
                }
                //Date selectedDate = slotDataModel.Date;
                //int slotDuration = slotDataModel.SlotDuration;
                //bool isHoliday = slotDataModel.IsHoliday;
                //List<string> selectedSlots = slotDataModel.Slots;


                //if (ModelState.IsValid)
                //{

                //scheduleviewmodel.Therapist_Id = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
                //TherapistOPS therapistops = new TherapistOPS();
                //if (therapistops.ScheduleTherapist(scheduleviewmodel))
                //{
                //    ViewBag.Message = "Therapist Details Added Successfully";
                //    ModelState.Clear();
                //}
                //}
                return RedirectToAction("Schedule", "Therapist");
                return View();

            }
            catch
            {
                return View();
            }
        }
    }
}
