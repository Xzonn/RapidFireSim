using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System.Diagnostics;

namespace RapidFireSim;

public class Program
{
    const string defaultCommand = "A,20,,20";

    static void Main(string[] args)
    {
        var version = typeof(Program).Assembly.GetName().Version ?? throw new Exception("无法获取版本号。");
        Console.WriteLine($"连发手柄模拟\n作者：Xzonn 版本：{version.Major}.{version.Minor}.{version.Build}\nhttps://github.com/Xzonn/RapidFireSim\n");
        var fullCommand = defaultCommand;
        if (args.Length == 1)
        {
            fullCommand = args[0];
            if (fullCommand == "-h" || fullCommand == "--help")
            {
                ShowHelp();
                Environment.Exit(0);
            }
        }
        else if (args.Length > 1)
        {
            Console.WriteLine("参数过多。");
            ShowHelp();
            Environment.Exit(-1);
        }
        else if (File.Exists("command.txt"))
        {
            fullCommand = File.ReadAllText("command.txt").Trim();
        }
        var commandList = fullCommand.Split(',');
        if (commandList.Length % 2 == 1) { throw new Exception("参数个数必须为偶数。"); }
        var states = new List<State>();
        for (int i = 0; i < commandList.Length; i += 2)
        {
            var command = commandList[i];
            ushort state = 0;
            byte lt = 0, rt = 0;
            var ms = int.Parse(commandList[i + 1]);
            for (int j = 0; j < command.Length; j++)
            {
                var c = command[j];
                if (c == 'A')
                {
                    state |= Xbox360Button.A.Value;
                }
                else if (c == 'B')
                {
                    if (j < command.Length - 1 && command[j + 1] == 'C')
                    {
                        state |= Xbox360Button.Back.Value;
                        j += 1;
                    }
                    else
                    {
                        state |= Xbox360Button.B.Value;
                    }
                }
                else if (c == 'X')
                {
                    state |= Xbox360Button.X.Value;
                }
                else if (c == 'Y')
                {
                    state |= Xbox360Button.Y.Value;
                }
                else if (c == 'U')
                {
                    state |= Xbox360Button.Up.Value;
                }
                else if (c == 'D')
                {
                    state |= Xbox360Button.Down.Value;
                }
                else if (c == 'L')
                {
                    if (j < command.Length - 1 && command[j + 1] == 'T')
                    {
                        lt = 255;
                        state |= Xbox360Button.LeftThumb.Value;
                        j += 1;
                    }
                    else if (j < command.Length - 1 && command[j + 1] == 'B')
                    {
                        state |= Xbox360Button.LeftShoulder.Value;
                        j += 1;
                    }
                    else
                    {
                        state |= Xbox360Button.Left.Value;
                    }
                }
                else if (c == 'R')
                {
                    if (j < command.Length - 1 && command[j + 1] == 'T')
                    {
                        rt = 255;
                        state |= Xbox360Button.RightThumb.Value;
                        j += 1;
                    }
                    else if (j < command.Length - 1 && command[j + 1] == 'B')
                    {
                        state |= Xbox360Button.RightShoulder.Value;
                        j += 1;
                    }
                    else
                    {
                        state |= Xbox360Button.Right.Value;
                    }
                }
                else if (c == 'S')
                {
                    state |= Xbox360Button.Start.Value;
                }
                else if (c == 'G')
                {
                    state |= Xbox360Button.Guide.Value;
                }
            }
            states.Add(new State()
            {
                command = command,
                state = state,
                lt = lt,
                rt = rt,
                ms = ms,
            });
        }
        using var client = new ViGEmClient();
        var controller = client.CreateXbox360Controller();
        var timestamp = DateTime.Now.Ticks / 10000;
        controller.Connect();
        Console.WriteLine("控制器已连接。请按“Ctrl+C”或关闭窗口停止运行。");
        while (true)
        {
            foreach (var state in states)
            {
                var nextTimestamp = timestamp + state.ms;
                controller.ButtonState = state.state;
                controller.LeftTrigger = state.lt;
                controller.RightTrigger = state.rt;
                controller.SubmitReport();
                Console.Write($"\r{new string(' ', Console.WindowWidth)}\r{state.command}");
                var now = DateTime.Now.Ticks / 10000;
                if (now < nextTimestamp)
                {
                    Thread.Sleep((int)(nextTimestamp - now));
                }
                timestamp = nextTimestamp;
            }
        }
    }

    static void ShowHelp()
    {
        var currentName = Process.GetCurrentProcess().ProcessName;
        Console.WriteLine($"使用方法：.\\{currentName} [命令]");
        Console.WriteLine($"示例：.\\{currentName} A,20,,20");
        Console.WriteLine($"也可将命令保存为“command.txt”。");
    }
}

internal struct State
{
    public string command = "";
    public ushort state = 0;
    public byte lt = 0;
    public byte rt = 0;
    public int ms = 1000;

    public State() { }
}