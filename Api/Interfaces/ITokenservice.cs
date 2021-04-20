using Api.Entities;

namespace Api.Interfaces
{
    public interface ITokenservice
    {
        string CreateToken(AppUsers users);
    }
}