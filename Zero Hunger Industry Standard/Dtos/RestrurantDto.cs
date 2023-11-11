using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zero_Hunger_Industry_Standard.Dtos
{
    public class RestrurantDto
    {
        public long intRestrurantId { get; set; }
        public string strRestrurantName { get; set; }
        public string strRestrurantType { get; set; }
        public bool IsActive { get; set; }
        public List<RestrurantFoodDto> restrurantFoods { get; set; }
    }
    public class RestrurantFoodDto
    {
        public string strRestrurantName { get; set; }
        public string strRestrurantType { get; set; }
        public long intRowId { get; set; }
        public string strFood { get; set; }
        public string StrFoodType { get; set; }
        public long intRestrurantId { get; set; }
        public Nullable<long> intEmployeeId { get; set; }
        public Nullable<bool> IsCollectReqest { get; set; }
        public Nullable<System.DateTime> DteCollectionTimeLeft { get; set; }
        public Nullable<bool> IsActive { get; set; }


    }
    

    
}