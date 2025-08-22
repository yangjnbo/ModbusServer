using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Modbus;
using Modbus.Core;
using Modbus.Protocol;

namespace ModbusServer;

public class Program
{
    public static void Main()
    {
        // 创建Modbus TCP服务器实例，监听502端口
        var server = new ModbusTcpServer(port: 502, unitid: 1);

        // 注册寄存器写事件
        server.WriteRegisterEven += (sender, e) =>
        {
            Console.WriteLine($"写寄存器事件: 类型={(RegisterType)(e.Type)} 地址={e.index}, 数量={e.count}");
        };

        // 启动服务器
        server.Start();
        
        if (!server.IsRunning)
        {
            Console.WriteLine("服务器启动失败");
            return;
        }

        Console.WriteLine("Modbus TCP服务器已启动, 监听端口: 502");
        Console.WriteLine("按任意键停止服务器...");

        // 等待用户输入停止服务器
        Console.ReadKey();

        // 停止服务器
        server.Stop();
        Console.WriteLine("服务器已停止");
    }
}
