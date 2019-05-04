using SteamKit2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGOBot
{
    public static class Utils
    {
        public enum RankName
        {
            NotRanked = 0,
            SilverI = 1,
            SilverII = 2,
            SilverIII = 3,
            SilverIV = 4,
            SilverElite = 5,
            SilverEliteMaster = 6,
            GoldNovaI = 7,
            GoldNovaII = 8,
            GoldNovaIII = 9,
            GoldNovaMaster = 10,
            MasterGuardianI = 11,
            MasterGuardianII = 12,
            MasterGuardianElite = 13,
            DistinguishedMasterGuardian = 14,
            LegendaryEagle = 15,
            LegendaryEagleMaster = 16,
            SupremeMasterFirstClass = 17,
            TheGlobalElite = 18
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string PlayerAccountIDFormat(uint accountID)
        {
            var id = new SteamID();
            id.AccountID = accountID;
            return id.ToString();
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return null;
            }
        }

        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/9f4270bf-5784-4f83-a0c4-29742b1cb9d2/deleting-all-files-from-a-directory?forum=csharpgeneral
        public static void DeletingFiles(string path)
        {
            DirectoryInfo dInfo = new DirectoryInfo(path);

            //delete files:
            foreach (System.IO.FileInfo file in dInfo.GetFiles())
                file.Delete();
            //delete directories in this directory:
            foreach (System.IO.DirectoryInfo subDirectory in dInfo.GetDirectories())
                dInfo.Delete(true);
        }
    }
}
