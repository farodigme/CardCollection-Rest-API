﻿namespace Collectors_Corner_Backend.Models.DataBase
{
    public class User
    {
        public int Id { get; set; }
        public string? Nickname { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
