using System.Security.Cryptography;
using System.Text;

namespace LinkShortener.Services;

public class LinkGenerator : ILinkGenerator
{
    public string GenerateLink(string link)
    {
        byte[] bytes = new byte[6];
        for (int i = 0; i < 6; i++)
        {
            byte upper = (byte)Random.Shared.Next(65, 91);
            byte lower = (byte)Random.Shared.Next(97, 123);
            byte number = (byte)Random.Shared.Next(48, 58);
            int choose = Random.Shared.Next(1, 4);
            switch(choose)
            {
                case 1:
                    bytes[i] = upper;
                    break;
                case 2:
                    bytes[i] = lower;
                    break;
                case 3:
                    bytes[i] = number;
                    break;
                default:
                    break;
            }
        }

        return Encoding.UTF8.GetString(bytes);
    }
}
