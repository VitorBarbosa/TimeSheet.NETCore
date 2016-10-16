using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using TimeSheet.src.util;

namespace TimeSheet.src {
    public class Program : Attribute {

        private Dictionary<string, MethodInfo> commands;
        public Time time { get; private set;}

        public static void Main(string[] args) {
            new Program().Initialize();
        }

        public void Initialize() {
            time = new Time();
            commands = new Dictionary<string, MethodInfo>();

            var methods = this.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            var methodsList = new List<MethodInfo>(methods);
            var methodListFiltered = methodsList.FindAll(m => new List<Attribute>(m.GetCustomAttributes(typeof(CommandAttribute), true)).Count > 0);

            foreach (var item in methodListFiltered) {
                commands.Add(item.Name.ToUpper(), item);
            }


            string command = "";
            while ((command = Console.ReadLine()) != null) {
                onCommandEntered(command);
            }

        }

        private void onCommandEntered(string command) {

            var match = Regex.Match(command, @"^(?<cmd>\S+)(\s(?<args>.*))?$");
            if (!match.Success) {
                return;
            }

            var commandName = match.Groups["cmd"].Value;
            var args = match.Groups["args"].Value.Split(' ');

            if (!commands.ContainsKey(commandName.ToUpper())) {
                Console.Write("Comando invalido! Para ajuda digite help\n");
                return;
            }

            var commandMethod = commands[commandName.ToUpper()];
            try {
                commandMethod.Invoke(this, new object[] { args });
            } catch (Exception ex) {
               
            }
        }
        
        /// <summary>
        /// exibe os comandos disponivels
        /// </summary>
        /// <param name="args"></param>
        [Command]
        private void help(string[] args) {
            Console.WriteLine("timein [00:00]\t\tAdd a new time in");
            Console.WriteLine("timeout [00:00]\t\tAdd a new time out");
            Console.WriteLine("timetoleave\t\tShow time to leave");
            Console.WriteLine("print\t\t\tShow current list of times in and out");
        }

        /// <summary>
        /// retorna a hora atual
        /// </summary>
        /// <param name="args"></param>
        [Command]
        private void now(string[] args) {
            Console.WriteLine(DateTime.Now.ToString());
        }

        /// <summary>
        /// tstargs a b c d
        /// </summary>
        /// <param name="args"></param>
        [Command]
        private void tstargs(string[] args) {
            Console.WriteLine(String.Join(", ",args));
        }

        [Command]
        private void timeIn(string[] args) {
            TimeSpan timeInTimeSpan;
            var result = Time.ParseTimeSpan(args[0], out timeInTimeSpan);
            if (!result) {
                Console.WriteLine("Error reading time");
            } else {
                time.TimesIn.Add(timeInTimeSpan);
            }
        }

        [Command]
        private void timeOut(string[] args) {
            TimeSpan timeOutTimeSpan;
            var result = Time.ParseTimeSpan(args[0], out timeOutTimeSpan);
            if (!result) {
                Console.WriteLine("Error reading time");
            } else {
                time.TimesOut.Add(timeOutTimeSpan);
            }
        }

        [Command]
        private void timeToLeave(string[] args) {
            Console.WriteLine($"Time to leave: {time.CalculateTimeOut()}");
        }

        /// <summary>
		/// Shows current lists of times in and out
		/// </summary>
		/// <param name="args"></param>
        [Command]
        private void print(string[] args) {
            Console.WriteLine($"Times in: {string.Join(", ", time.TimesIn)}");
            Console.WriteLine($"Times out: {string.Join(", ", time.TimesOut)}");
        }
    }

    public class CommandAttribute : Attribute {

    }
}
