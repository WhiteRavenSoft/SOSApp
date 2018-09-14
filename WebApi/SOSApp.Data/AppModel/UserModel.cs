using System;

namespace SOSApp.Data.AppModel
{
    public class UserModel
    {
        public int ID { get; set; }
        public string MobileID { get; set; }
        public int? RoleId { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Birthdate { get; set; }
        public string Address { get; set; }
        public double? Lon { get; set; }
        public double? Lat { get; set; }
        public bool Active { get; set; }
    }

    public class UserMobileModel
    {
        public string PlayerID { get; set; }
        public string Address { get; set; }
    }

    public class UserLogin
    {
        public int ID { get; set; }
        public string MobileID { get; set; }
        public string Password { get; set; }
    }

    public class UserChangePasswordModel
    {
        public string Password { get; set; }
    }
}
