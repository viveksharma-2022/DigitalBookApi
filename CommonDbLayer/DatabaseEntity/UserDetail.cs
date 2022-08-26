using System;
using System.Collections.Generic;

namespace CommonDbLayer.DatabaseEntity
{
    public partial class UserDetail
    {
        public long UserId { get; set; }
        public string? UserEmail { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? UserType { get; set; }
    }
}
