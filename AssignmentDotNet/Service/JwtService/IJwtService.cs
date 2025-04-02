namespace AssignmentDotNet.Service.JwtService
{
    public interface IJwtService
    {
        string GenerateToken(string username);
    }
}
