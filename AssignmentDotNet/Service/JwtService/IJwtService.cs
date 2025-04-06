using AssignmentDotNet.Model;

namespace AssignmentDotNet.Service.JwtService
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
