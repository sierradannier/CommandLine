// Copyright 2015 Dannier Sierra. All rights reserved. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PowerCommandLine.Helpers;

namespace PowerCommandLine
{
    public class CommandEngine
    {
        private static ICommandContainer _commandContainer;
        public static ICommandContainer CommandContainer
        {
            get
            {
                if (_commandContainer == null)
                    _commandContainer = new CommandContainer();
                return _commandContainer;
            }
        }
        private static ITypeFinder _typeFinder;
        internal static ITypeFinder TypeFinder
        {
            get
            {
                if (_typeFinder == null)
                    _typeFinder = new AppDomainTypeFinder();
                return _typeFinder;
            }
        }

        public static void Initialize()
        {
            Console.WriteLine("PowerCommandLine shell version: {0}", ApplicationInfo.CurrentVersion);
            RegisterCommands();
            StartInteractive();
        }

        static void RegisterCommands()
        {
            var crTypes = TypeFinder.FindClassesOfType<ICommandRegistrar>();
            var crInstances = crTypes.Select(crType => (ICommandRegistrar) Activator.CreateInstance(crType)).ToList();
            crInstances = crInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var cr in crInstances)
                cr.Register(CommandContainer);
        }

        static void StartInteractive()
        {
            var thread = new Thread(new ThreadStart(ConsoleListener));
            thread.Start();
        }

        static void ConsoleListener()
        {
            var line = string.Empty;
            while (true)
            {
                if (Console.CursorLeft > 0)
                    Console.WriteLine();
                Console.Write("> ");
                line = Console.ReadLine();
                ParseCommand(line);
            }
        }

        public static void ParseCommand(string line)
        {
            line = line.Trim();
            if (string.IsNullOrEmpty(line)) return;
            var sPos = line.IndexOf(' ');
            var command = line;
            var args = string.Empty;
            if (sPos > 0)
            {
                command = line.Substring(0, sPos);
                args = line.Substring(sPos + 1, line.Length - sPos - 1);
            }
            var match = false;
            foreach (var item in CommandContainer.Commands)
            {
                if (string.Compare(item.Key, command, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    match = true;
                    item.Execute(args.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
            if (!match)
                Console.WriteLine("'{0}' is not recognized as an internal command", command);
        }
    }
}
