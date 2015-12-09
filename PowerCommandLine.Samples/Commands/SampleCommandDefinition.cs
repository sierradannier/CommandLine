// Copyright 2015 Dannier Sierra. All rights reserved. See License.md in the project root for license information.

using System;
using System.Threading;

namespace PowerCommandLine.Samples.Commands
{
    public class SampleCommandDefinition : ICommand
    {
        public void ExecuteCommand()
        {
            Console.WriteLine("Sample command execution by class");
            for (int i = 0; i <= 100; ++i)
            {
                Console.Write("\rSample procesing...{0}%   ", i);
                if(i == 100)
                    Console.WriteLine();
                else
                    Thread.Sleep(20);
            }
            Console.WriteLine("Done!");
        }
    }
}
