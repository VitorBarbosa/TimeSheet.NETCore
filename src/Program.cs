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

        public static void Main(string[] args) {
            new Program().Initialize();

            //var culture = CultureInfo.CurrentCulture;
            //var totalTime = new TimeSpan(08, 00, 00);
            //bool result;
            //TimeSpan timeIn1TimeSpan, timeOut1TimeSpan, timeIn2TimeSpan;

            //Console.WriteLine("TimeSheet");
            //Console.WriteLine();
            //do {
            //    Console.WriteLine("Check In (format 14:14)");
            //    var timeIn1 = Console.ReadLine();
            //    result = Time.ParseTimeSpan(timeIn1, out timeIn1TimeSpan);
            //    if (!result) {
            //        Console.WriteLine("Error reading time");
            //    }
            //} while (!result);
            //do {
            //    Console.WriteLine("Check Out (format 14:14)");
            //    var timeOut1 = Console.ReadLine();
            //    result = Time.ParseTimeSpan(timeOut1, out timeOut1TimeSpan);
            //    if (!result) {
            //        Console.WriteLine("Error reading time");
            //    }
            //} while (!result);
            //do {
            //    Console.WriteLine("Check In (format 14:14)");
            //    var timeIn2 = Console.ReadLine();
            //    result = Time.ParseTimeSpan(timeIn2, out timeIn2TimeSpan);
            //    if (!result) {
            //        Console.WriteLine("Error reading time");
            //    }
            //} while (!result);
            //// show expected time out
            //var time2leave = timeIn1TimeSpan.Add(totalTime).Add(timeIn2TimeSpan.Subtract(timeOut1TimeSpan));
            //Console.WriteLine($"Time to leave: {time2leave}");
        }

        public void Initialize() {
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
            Console.WriteLine("help!!");
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
    }

    public class CommandAttribute : Attribute {

    }
}
