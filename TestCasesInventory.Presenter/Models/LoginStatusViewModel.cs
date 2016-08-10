using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasesInventory.Presenter.Models
{
    public class LoginStatusViewModel
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ProfilePictureURL { get; set; }
        public bool IsProfilePictureExisted { get; set; }
    }
}
