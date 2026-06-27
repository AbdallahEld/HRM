using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Application.Account.DTOs
{
    public class RegistrationResult
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
