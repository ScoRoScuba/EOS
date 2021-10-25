namespace EOS2.Model
{
    public class OrganizationRoleUser : IEntity
    {
        public virtual OrganizationRole Role { get; set; }

        public int UserId { get; set; }

        public int Id { get; set; }
    }
}
