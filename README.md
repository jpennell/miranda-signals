# Miranda: Signals

Signals for Unity3D, based on:

* [Miranda](https://github.com/mestevens/miranda)

## Basic Usage

### Add dependency

```xml
<dependency>
    <groupId>com.jamespennell.injection</groupId>
    <artifactId>miranda-signals</artifactId>
    <version>0.0.1-SNAPSHOT</version>
    <type>dll</type>
</dependency>
```

### Create a signal

```c#
using Jamespennell.Injection.Extensions.Signals.Impl;

public class HelloSignal : Signal { }
```

### Create a command

```c#
using Jamespennell.Injection.Extensions.Signals.Api;

public class HelloCommand : ICommand
{
    #region Methods

    public void Execute()
    {
        Console.WriteLine("Hello, World!");
    }

    #endregion
}
```

### Configure signal to command mapping

```c#
using Jamespennell.Injection.Extensions.Signals.Impl;

public class HelloSignalContext : SignalContext
{
    #region Methods

    public override void MapBindings()
    {
        this.Bind<HelloSignal>().To<HelloCommand>();
    }

    #endregion
}
```

### Dispatch a signal

```c#
using Mestevens.Injection.Core;

Context context = Miranda.Init(new HelloSignalContext());
HelloSignal helloSignal = context.Get<HelloSignal>();
helloSignal.Dispatch();
//Prints "Hello, World!"
```

## Basic Usage