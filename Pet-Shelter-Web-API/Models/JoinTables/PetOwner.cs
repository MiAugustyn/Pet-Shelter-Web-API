namespace PetShelterWebAPI.Models.JoinTables
{
    public class PetOwner
    {
        public int PetId { get; set; }
        public int OwnerId { get; set; }
        public Pet Pet { get; set; }
        public Owner Owner { get; set; }
    }
}
