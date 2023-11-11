using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zero_Hunger_Industry_Standard.Dtos
{
    public class UserDto
    {

        public long intUserID { get; set; }
        public long strUserType { get; set; }
        public long strUserName { get; set; }
        public bool IsActive { get; set; }
        public long intNgoID { get; set; }
        public string strUserMail { get; set; }
        public string strPassword { get; set; }
    }
    public class LogDto
    {
        public int intAutoId { get; set; }
        public Nullable<long> intRestrurantId { get; set; }
        public Nullable<long> intRowid { get; set; }
        public Nullable<long> IntUserID { get; set; }
    }
}