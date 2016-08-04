using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasesInventory.Presenter.Common
{
    public class ValidationMessages
    {
        public const string ErrorMessageForPasswordProperty = "The {0} must be a string with a maximum length of {1}";
        public const string ErrorMessageForConFirmPasswordProperty = "The password and confirmation password do not match.";
        public const string ErrorMessageForDisplayNameProperty = "The {0} must be a string with a maximum length of {1}";
        public const string ErrorMessageForTeamNameProperty = "The {0} must be a string with a maximum length of {1}";
        public const string ErrorMessageForTestSuiteTitleProperty = "The {0} must be a string with a maximum length of {1}";
    }
}
