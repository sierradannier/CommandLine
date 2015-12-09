// Copyright 2015 Dannier Sierra. All rights reserved. See License.md in the project root for license information.

namespace PowerCommandLine
{
    public interface ICommandRegistrar
    {
        void Register(ICommandContainer container);

        int Order { get; }
    }
}
