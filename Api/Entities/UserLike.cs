namespace Api.Entities
{
    public class UserLike
    {
        public AppUsers SourceUser{ get; set; }
        public int SourceUesrId { get; set; }

        public AppUsers LikedUser { get; set; }
        public int LikedUserId { get; set; }
    }
}