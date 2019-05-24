using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MicroBatchFramework;
using Microsoft.Extensions.Logging;

namespace Compilementor {
    class Program {
        static async Task Main (string[] args) {
            await BatchHost.CreateDefaultBuilder ().RunBatchEngineAsync<Mentor> (args);
        }
    }

    public class Mentor : BatchBase {
        public void MyBuild () {
            var proc = Process.Start (new ProcessStartInfo { FileName = "dotnet", Arguments = "build", RedirectStandardOutput = true });
            proc.WaitForExit ();
            var errorAll = proc.StandardOutput.ReadToEnd ();
            proc.Close ();
            var errorLine = errorAll.Split ('\n').TakeLast (4).First ();
            var errorNum = int.Parse (errorLine.Trim () [0].ToString ());
            Console.WriteLine (errorAll);
            if (errorNum == 0) {
                Console.WriteLine ("おめでとーーーー！！！！！！！！");
            } else {
                Console.WriteLine ("残念．．．．．．");
            }
        }
    }
}