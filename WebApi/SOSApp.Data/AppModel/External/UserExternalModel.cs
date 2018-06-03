using WhiteRaven.Core.Enum;

namespace WhiteRaven.Data.AppModel
{
    public class UserExternalModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string lastname { get; set; }
        public string family_name { get; set; }
        public string given_name { get; set; }
        public string picture { get; set; }
        public AuthMode authMode { get; set; }
    }
}
