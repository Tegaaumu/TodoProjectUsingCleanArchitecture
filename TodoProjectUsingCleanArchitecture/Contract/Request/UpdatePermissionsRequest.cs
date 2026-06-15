namespace TodoProjectUsingCleanArchitecture.Contract.Request
{
    // Request schema for updating/assigning user permissions.
    public class UpdatePermissionsRequest
    {
        // Target user ID whose permissions are being modified.
        public Guid UserId { get; set; }

        // CanCreate: True if the user should be allowed to add items.
        public bool CanCreate { get; set; }

        // CanEdit: True if the user should be allowed to update items.
        public bool CanEdit { get; set; }

        // CanDelete: True if the user should be allowed to delete items.
        public bool CanDelete { get; set; }

        // CanAssign: True if the user should be allowed to delegate/assign permissions to others.
        public bool CanAssign { get; set; }
    }
}
