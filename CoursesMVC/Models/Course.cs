using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace CoursesMVC.Models
{
    //This is the Model in MVC, the data and the format for creating a model of Course type. 
    public class Course
    {
        //Properties
        [Key]
        public int Id { get; set; }
        
        [DataType(DataType.Date), DisplayName("Start date")]
        public DateOnly StartDate { get; set; }

        [DataType(DataType.Date), DisplayName("End date")]
        public DateOnly EndDate { get; set; }

        [Required, StringLength(255, ErrorMessage = "max length = 255 characters")]
        public string Description { get; set; }

        [Required, StringLength(50, ErrorMessage = "max length = 155 characters")]
        public string Title { get; set; }

    }
}
