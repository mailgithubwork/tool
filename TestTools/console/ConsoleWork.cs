using System;
using com.tool;

namespace TestTools.console
{
    public class ConsoleWork
    {
        private const string EXIT = "Exit";
        private const string CANCEL = "Cancel";
        
        private bool _isExit;

        private readonly FacadeTool _ft;

        public ConsoleWork()
        {
            _ft = new FacadeTool(DebugLog);
            _isExit = false;
        }
        
        public void Start(string[] args)
        {
            if (!CheckParams(args))
            {
                return;
            }

            try
            {
                _ft.Start(
                    args[0],
                    args[1],
                    (args.Length >= 3)? args[2] : ""
                    );
            }
            catch (Exception e)
            {
                DebugLog(e.Message);
            }
            
            
            while (!_isExit)
            {
                Console.Write("> ");
                ProcessCommand((Console.ReadLine()));
            }
        }

        private bool CheckParams(string[] args)
        {
            if (args.Length < 2)
            {
                DebugLog("Params is invalid.");
                Console.ReadKey();
                return false;
            }

            return true;
        }

        private void ProcessCommand(string command)
        {
            //Console.Clear();
            if (string.IsNullOrEmpty(command))
            {
                PrintHelp();
                return;
            }

            if (command.ToLower().Equals(EXIT.ToLower()))
            {
                _isExit = true;
                return;
            }

            if (command.ToLower().Equals(CANCEL.ToLower()))
            {
                _ft.Stop();
                return;
                
            }
            //PrintHelp();
        }

        private void PrintHelp()
        {
            DebugLog(EXIT);
            DebugLog(CANCEL);
        }

        private void DebugLog(string message)
        {
            Console.WriteLine(message);
        }
    }
}