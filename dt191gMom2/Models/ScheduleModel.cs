using System.ComponentModel.DataAnnotations;

namespace dt191gMoment2.Models {
    //DT191G Moment 2, Alice Fagerberg
    //Model för obj i träningsschema
    public class ScheduleModel {
        //Properties med inställningar
        [Required(ErrorMessage = "Ange ett namn på ditt träningstillfälle")]
        [Display(Name ="Vad ska du träna?")]
        public string? WorkoutName { get; set; }

        [Required(ErrorMessage = "Ditt tillfälle måste utföras på en veckodag")]
        public string? DayOfWeek { get; set; }

        [Required(ErrorMessage = "Du måste välja en tidpunkt")]
        [Display(Name ="Välj vilken tid på dygnet")]
        public string? Time { get; set; }

        [Range(1,5, ErrorMessage ="Du måste fylla i en svårighetsgrad mellan 1-5")]
        [Display(Name ="Svårighets grad, välj från 1 till 5")]
        public int DegreeDifficulty { get; set; }
    

    }
}