using Microsoft.Extensions.Caching.Memory;
using UKParliament.CodeTest.Services.Services.Interfaces;

namespace UKParliament.CodeTest.Services.Services;

public class AvatarService(HttpClient client, IMemoryCache cache) : IAvatarService
{
    public async Task<Stream> GetAvatar(string seed)
    {
        //I'm using https://api.dicebear.com/9.x/personas/ for the fake avatars.
        var avatarBytes = await cache.GetOrCreateAsync(
            seed,
            async c =>
            {
                using var stream = await client.GetStreamAsync(
                    $"?size=150&mouth=smile&eyes=open&seed={seed}"
                );
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        );

        return new MemoryStream(avatarBytes);
    }
}
