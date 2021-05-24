namespace Api.Helpers
{
    public class LikesParams:PageinationParams   
    {
        public int UserId { get; set; }

        public string predicate { get; set; }
        
    }
}