// Copyright 2015 Dannier Sierra. All rights reserved. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerCommandLine.Registrations
{
    internal partial class CommandRegistrar : ICommandRegistrar
    {
        public void Register(ICommandContainer container)
        {
            container.Register(Command.For("help").WithDescription("Provides Help information for commands.").AddAction((args) =>
            {
                if (args == null || args.Length == 0)
                {
                    Console.WriteLine("For more information on a specific command, type HELP command-name");
                    var maxCommandLength = container.Commands.Max(x => x.Key.Length);
                    var marginLeft = maxCommandLength + 5;
                    var rigthPartPos = marginLeft % Console.BufferWidth;
                    var chunkSize = Console.BufferWidth - rigthPartPos;
                    var displayFormat = "{0,-" + marginLeft + "}";
                    foreach (var item in container.Commands.OrderBy(x => x.Key))
                    {
                        var leftPart = string.Format(displayFormat, item.Key.ToUpper());
                        Console.Write(leftPart);

                        if (item.Description.Length > chunkSize)
                        {
                            var chunks = new List<string>();
                            chunks.AddRange(Enumerable.Range(0, item.Description.Length / chunkSize).Select(i => item.Description.Substring(i * chunkSize, chunkSize)));
                            chunks.Add(item.Description.Length % chunkSize > 0 ? item.Description.Substring((item.Description.Length / chunkSize) * chunkSize, item.Description.Length - ((item.Description.Length / chunkSize) * chunkSize)) : string.Empty);
                            foreach (var chunk in chunks)
                            {
                                var rigthPart = string.Format("{0,-" + (rigthPartPos - Console.CursorLeft) + "}{1}",
                                    string.Empty, chunk);
                                if (chunk.Length == chunkSize)
                                    Console.Write(rigthPart);
                                else
                                    Console.WriteLine(rigthPart);
                            }
                        }
                        else
                        {
                            Console.WriteLine(item.Description);
                        }
                    }
                }
                else
                {
                    var command = container.Commands.FirstOrDefault(x => string.Compare(x.Key, args[0], StringComparison.InvariantCultureIgnoreCase) == 0);
                    if (command == null)
                        Console.WriteLine("Command '{0}' not found.", args[0]);
                    else if (!string.IsNullOrEmpty(command.Help))
                        Console.WriteLine(command.Help);
                }
            }));

            container.Register(Command.For("exit").WithDescription("Exits power shell.").WithHelp("Enter EXIT to close the program").AddAction(() =>
            {
                Environment.Exit(0);
            }));
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
