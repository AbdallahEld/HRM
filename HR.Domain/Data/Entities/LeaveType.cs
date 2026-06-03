using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HR.Domain.Data.Entities
{
    public class LeaveType : BaseEntity
    {
        public string Name { get; set; }
        public int MaxDaysPerYear { get; set; }
        public bool IsPaid { get; set; }
        public bool CarryOverAllowed { get; set; }
        public bool RequiresApproval { get; set; }
        //---------------------------One To Many Relationship----------------------------//
        public List<Leave> Leaves { get; set; } = new List<Leave>();
        //-------------------------------------------------------------------------------//
    }
}
