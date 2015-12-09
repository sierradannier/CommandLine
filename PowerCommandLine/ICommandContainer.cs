// Copyright 2015 Dannier Sierra. All rights reserved. See License.md in the project root for license information.

using System.Collections.Generic;

namespace PowerCommandLine
{
    public interface ICommandContainer
    {
        IList<ICommandRegistration> Commands { get; }
        ICommandContainer Register(params ICommandRegistration[] registrations);
    }
}
