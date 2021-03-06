# Commands

Commands can be C# classes, lambda expressions, or CLI programs.
They can be queued, scheduled, or run from the command line.

## Create a Command

The InEngine.Core package is required to create a C# class command. 
Install it in a Visual Studio project.

**Package Manager**
```bash
Install-Package InEngine.Core
```

**Nuget CLI**
```bash
nuget install InEgine.Core
```

**.NET CLI**
```bash
dotnet add package InEngine.Core
```

**Paket CLI**
```bash
paket add InEngine.Core
```

To create a class command, extend the **InEngine.Core.AbstractCommand** class.
Minimally, the Run method should be overridden.

```c#
using System;
using InEngine.Core;

namespace MyCommandPlugin
{
    public class MyCommand : AbstractCommand
    {
        public override void Run()
        {
            Console.WriteLine("Hello, world!");
        }
    }
}
```

## Run a Command

Create a class that extends **InEngine.Core.AbstractPlugin** in the same assembly as the command class.
Add a VerbOptions attribute, from the **CommandLine** namespace, that defines the name of the command. 

This class registers a command in the MyPlugin assembly called "mycommand":

```c#
using CommandLine;
using CommandLine.Text;
using InEngine.Core;

namespace MyCommandPlugin
{
    public class MyPlugin : AbstractPlugin
    {
        [VerbOption("mycommand", HelpText="My example command.")]
        public MyCommand MyCommand { get; set; }
    }
}
```

Download the InEngine binary distribution, from the [GitHub Releases](https://github.com/InEngine-NET/InEngine.NET/releases) page, that matches the version of the InEngine.Core package you included.

Copy your project's DLLs into the Plugins subdirectory included in the binary distribution. 
Add your plugin to the "Plugins" list in [appsettings.config](configuration) at the root of the binary distribution.

Run your command:

```bash
inengine.exe mycommand
```

### Writing Output

The **InEngine.Core.AbstractCommand** class provides some helper functions to output text to the console, for example:

```c#
public override void Run()
{
    Line("Display some information");
}
```

All of these commands append a newline to the end of the specified text:

```c#
Line("This is some text");                  // Text color is white
Info("Something good happened");            // Text color is green
Warning("Something not so good happened");  // Text color is yellow
Error("Something bad happened");            // Text color is red
```

These commands are similar, but they do not append a newline:

```c#
Text("This is some text");                      // Text color is white
InfoText("Something good happened");            // Text color is green
WarningText("Something not so good happened");  // Text color is yellow
ErrorText("Something bad happened");            // Text color is red
```

You can also display newlines:
 
```c#
Newline();      // 1 newline
Newline(5);     // 5 newlines
Newline(10);    // 10 newlines
```

The methods can be chained together:

```c#
InfoText("You have this many things: ")
    .Line("23")
    .NewLine(2)
    .InfoText("You have this many other things: ")
    .Line("34")
    .NewLine(2); 
```

### Progress Bar

The **InEngine.Core.AbstractCommand** class provides a ProgressBar property to show command progress in a terminal.
This is how it is used:

```c#
public override void Run()
{
    // Define the ticks (aka steps) for the command...
    var maxTicks = 100000;
    SetProgressBarMaxTicks(maxTicks);

    // Do some work...
    for (var i = 0; i <= maxTicks;i++)
    {
        // Update the command's progress
        UpdateProgress(i);
    }
}
```

### Running non-.NET Commands

It is not necessary to create C# classes to utilize InEngine.NET.
Arbitrary external programs can be run, with an optional argument list, by leveraging the InEngine.Core plugin's **exec** command.

For example, create a python script called **helloworld.py**, make it executable, and add this to it:

```python
#!/usr/bin/env python

print 'Hello, world!'
```

Whitelist the helloworld.py script in the [appsettings.json](configuration) file:

```json
{
  "InEngine": {
    // ...
    "ExecWhitelist": {
      "helloworld": "/path/to/helloworld.py"
    }
    // ...
  }
}
```

Now execute it with the **exec** command:

```bash
inengine exec --executable="helloworld"
```

If an external executable requires arguments, use the **--args** argument:

```bash
inengine exec --executable="foo" --args="--version"
```

## View Commands

Run inengine.exe with no arguments to see a list of commands:

```bash
inengine.exe
```

![InEngine Command List](images/commands.png)


!!! note "InEngine.Core is a Plugin"
    The **InEngine.Core** library is itself a plugin that contains queueing, scheduling, and other commands. 


## View a Command's Help Text

Run the command with the -h or --help arguments to see help text.

This command prints the publish command's help text, from the core plugin:

```bash
inengine.exe queue:publish -h
```

The **InEngine.Core** plugin's command to clear the InEngine.NET queues produces this help message. 

```text
InEngine 3.x
Copyright © 2017 Ethan Hann

  --plugin            Required. The name of a command plugin file, e.g. 
                      InEngine.Core

  --command           A command name, e.g. echo

  --class             A command class, e.g. 
                      InEngine.Core.Commands.AlwaysSucceed. Takes precedence 
                      over --command if both are specified.

  --args              An optional list of arguments to publish with the 
                      command.

  --secondary         (Default: False) Publish the command to the secondary 
                      queue.
```

