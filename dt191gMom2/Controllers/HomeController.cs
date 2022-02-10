using Microsoft.AspNetCore.Mvc;
using dt191gMoment2.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace dt191gMoment2.Controllers {

    // DT191G Moment 2, Alice Fagerberg
    //Home controller

    public class HomeController : Controller {

        //Start
        [HttpGet("/")]
        public IActionResult Index(){

            // Läser in fil till lista
            var JsonStr = System.IO.File.ReadAllText("schedule.json");
            var JsonObj = JsonConvert.DeserializeObject<List<ScheduleModel>>(JsonStr);

            // Hämtar sessioner
            string? s2 = HttpContext.Session.GetString("recent");
            string? add1 = HttpContext.Session.GetString("added");
            
            //Kontroll view över sessioner i variabler
            if(add1 == null){
                ViewBag.text = s2;
            }
            
            ViewBag.newcourse = add1;
            if(add1 != null){
                Thread.Sleep(3000);
                HttpContext.Session.Remove("added"); 
                
            }

            // Returnera lista med data
            return View(JsonObj);
        }

        //Om
        [HttpGet("/om")]
        public IActionResult About(){
            // Läser in fil till lista
            var JsonStr = System.IO.File.ReadAllText("schedule.json");
            var JsonObj = JsonConvert.DeserializeObject<List<ScheduleModel>>(JsonStr);
            // Returnera lista med data
            return View(JsonObj);
        }

        //Sida för att lägga till träningstillfälle
        [HttpGet("/laggtill")]
        public IActionResult AddWorkout(){

            // Hämtar sessionsdata till sträng och sätter i viewData
            string? s3 = HttpContext.Session.GetString("recent");
            ViewData["Message"] = s3;

            return View();

            
        }

        
        [HttpPost("/laggtill")] //För post-metod på lägg till sidan
        public IActionResult AddWorkout(ScheduleModel model){
            
            string? s4 = HttpContext.Session.GetString("recent");
            ViewData["Message"] = s4;

            if(ModelState.IsValid){
            //Läs in
            var JsonStr = System.IO.File.ReadAllText("schedule.json");
            var JsonObj = JsonConvert.DeserializeObject<List<ScheduleModel>>(JsonStr);

            string s1;
            string added;
                
            //Lägg till
            if(JsonObj != null) {
                   
                   JsonObj.Add(model);

                   // Variabler med träningsinfo
                   s1 = $"Träningstillfälle som du nyligen lagt till - {model.WorkoutName}, {model.DayOfWeek}ar kl {model.Time}";
                   added = $"Träningstillfället {model.WorkoutName} har lagts till på {model.DayOfWeek}ar kl {model.Time}";
                   //Sparar variabler i session
                   HttpContext.Session.SetString("recent", s1);//test1
                   HttpContext.Session.SetString("added", added);
                    

                   
               }

               //Konvertera till json sträng och spara
               
               System.IO.File.WriteAllText("schedule.json", JsonConvert.SerializeObject(JsonObj, Formatting.Indented));

               // rensa input fält och redirecta till startsidan 
               ModelState.Clear();
               
               return Redirect("/"); 
              
            }
            
            return View();
            
        }
    }
}