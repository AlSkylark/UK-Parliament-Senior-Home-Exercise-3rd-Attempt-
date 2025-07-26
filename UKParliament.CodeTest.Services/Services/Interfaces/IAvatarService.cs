namespace UKParliament.CodeTest.Services.Services.Interfaces;

public interface IAvatarService
{
    Task<Stream> GetAvatar(string seed);
}
