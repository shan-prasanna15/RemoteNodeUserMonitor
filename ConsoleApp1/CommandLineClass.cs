using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BackendProcessor
{
    public class CommandLineClass
    {
        string path = "cmd.exe";
        string arguments = "/C quser";
        List<string> outputLines = new List<string>();
        List<UserDetail> userdetails = new List<UserDetail>();

        public CommandLineClass()
        {
            Console.WriteLine("Initialized with default values \n path : {0} \n arguments: {1}", path, arguments);
        }
        public CommandLineClass(string path, string arguments)
        {
            this.path = path;
            this.arguments = arguments;
        }

        public void Run()
        {
            RunCommandline();
            GetUserDetails();
            ShowUserDetails();
        }

        public void RunCommandline()
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {                        
                        FileName = path,
                        Arguments = arguments,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                process.Start();

                while (!process.StandardOutput.EndOfStream)
                {
                    var line = process.StandardOutput.ReadLine();
                    outputLines.Add(line);
                    Console.WriteLine(line);
                }
                
                process.WaitForExit();
            }            
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }            
        }

        public void GetUserDetails()
        {
            foreach (string line in outputLines)
            {                
                string[] lineSplitResult = UtilityClass.SplitMultiSpaceWithRegex(line);
                string[] spaceReducedLIne = UtilityClass.CleanStringArray(lineSplitResult);
                if (!spaceReducedLIne[0].Equals("USERNAME", StringComparison.OrdinalIgnoreCase))
                {
                    UserDetail ud = new UserDetail();
                    ud.UserName = spaceReducedLIne[0];
                    ud.SessionName = spaceReducedLIne[1];
                    ud.Id = int.Parse(spaceReducedLIne[2]);
                    ud.State = spaceReducedLIne[3];
                    ud.IdleTime = spaceReducedLIne[4];
                    ud.LogonTime = spaceReducedLIne[5];
                    userdetails.Add(ud);
                }
            }
        }

        public void ShowUserDetails()
        {
            foreach(UserDetail user in userdetails)
            {
                Console.WriteLine( "Name : {0} \nSessionNaem : {1}\nId : {2} \nState : {3}\nLogon Time : {4}",
                                                    user.UserName,
                                                    user.SessionName,
                                                    user.Id,
                                                    user.State,
                                                    user.LogonTime);
            }
        }
    }
}
