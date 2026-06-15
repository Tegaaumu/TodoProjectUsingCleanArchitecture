using System;

namespace TodoProjectUsingCleanArchitecture.Application.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // --- Authorization Permissions ---
        // CanCreate: Determines if this user can add/create new items.
        public bool CanCreate { get; set; } = false;

        // CanEdit: Determines if this user can edit/update existing items.
        public bool CanEdit { get; set; } = false;

        // CanDelete: Determines if this user can delete items.
        public bool CanDelete { get; set; } = false;

        // CanAssign: Determines if this user can give/manage authorization permissions for other users.
        public bool CanAssign { get; set; } = false;
    }
}
