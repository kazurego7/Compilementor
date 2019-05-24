using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MicroBatchFramework;

namespace Compilementor {
    class Program {
        static async Task Main (string[] args) {
            await BatchHost.CreateDefaultBuilder ().RunBatchEngineAsync<Mentor> (args);
        }
    }

    public class Mentor : BatchBase {
        public void MentorBuild () {
            var dotnetBuildProc = Process.Start (new ProcessStartInfo { FileName = "dotnet", Arguments = "build", RedirectStandardOutput = true, RedirectStandardError = true });
            dotnetBuildProc.WaitForExit ();
            var buildResult = dotnetBuildProc.StandardOutput.ReadToEnd ();
            var buildErrorLine = buildResult.Split ('\n').TakeLast (4).First ();
            var buildErrorNum = int.Parse (buildErrorLine.Trim () [0].ToString ());

            var mentorColor = ConsoleColor.White;
            var mentorSay = "";
            if (buildErrorNum == 0) {
                mentorColor = ConsoleColor.Green;
                mentorSay = "ビルド成功 おめでとーーーーー！！！！！";
            } else {
                mentorColor = ConsoleColor.Cyan;
                mentorSay = "ビルド失敗 また頑張ろう．．．．．．";
            }

            Console.WriteLine (buildResult);
            Console.ForegroundColor = mentorColor;
            Console.WriteLine (mentorSay);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}