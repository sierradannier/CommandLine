// Copyright 2015 Dannier Sierra. All rights reserved. See License.md in the project root for license information.

using System;

namespace PowerCommandLine
{
    public interface ICommandRegistration
    {
        string Key { get; }

        string Description { get; }

        string Help { get; }

        ICommandRegistration ImplementedBy<T>(T instance) where T : ICommand;

        ICommandRegistration AddAction(Action action);

        ICommandRegistration AddAction(Action<string[]> action);

        ICommandRegistration WithDescription(string description);

        ICommandRegistration WithHelp(string help);

        void Execute(string[] args);
        void Execute();
    }
}
