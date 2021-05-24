namespace Api.Helpers
{
    public class UserParams:PageinationParams
    {
      

        public string  CurrentUsername { get; set; }
        public string Gender { get; set; }

        public int MaxAge { get; set; } =150;
        public int MinAge { get; set; }=18;

        public string orderBy { get; set; }="lastActive";

    }
}