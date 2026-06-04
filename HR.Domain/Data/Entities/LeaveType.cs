using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HR.Domain.Data.Entities
{
    public class LeaveType : BaseEntity
    {
        public string Name { get; set; }
        public int MaxDaysPerYear { get; set; } = 0;
        public bool IsPaid { get; set; } = false;
        public bool CarryOverAllowed { get; set; } = false;
        public bool RequiresApproval { get; set; } = false;
        //---------------------------One To Many Relationship----------------------------//
        public List<Leave> Leaves { get; set; } = new List<Leave>();
        //-------------------------------------------------------------------------------//
    }
}
