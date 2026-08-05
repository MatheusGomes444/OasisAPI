using System.Security.Cryptography;

namespace OasisApi.Domain
{
    // Guid.CreateVersion7() só existe a partir do .NET 9; este projeto está no .NET 8.
    // Implementação conforme RFC 9562 (48 bits de timestamp Unix em ms + 74 bits aleatórios).
    public static class UuidV7
    {
        public static Guid NewGuid()
        {
            Span<byte> bytes = stackalloc byte[16];

            long unixMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            bytes[0] = (byte)(unixMs >> 40);
            bytes[1] = (byte)(unixMs >> 32);
            bytes[2] = (byte)(unixMs >> 24);
            bytes[3] = (byte)(unixMs >> 16);
            bytes[4] = (byte)(unixMs >> 8);
            bytes[5] = (byte)unixMs;

            RandomNumberGenerator.Fill(bytes[6..]);

            bytes[6] = (byte)(0x70 | (bytes[6] & 0x0F)); // versão 7
            bytes[8] = (byte)(0x80 | (bytes[8] & 0x3F)); // variante RFC 4122/9562

            return new Guid(bytes, bigEndian: true);
        }
    }
}
