using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCEventCalendar
{
    public partial class User
    {
        public int UserID { get; set; }
        [DisplayName("User Name")]
        [Required(ErrorMessage = "This filed is reqired")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This filed is reqired")]

        public string Password { get; set; }

        public string LoginErrorMessage { get; set; }
    }
}