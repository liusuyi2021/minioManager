using CloudManager;
using log4cxx;
using System;
using System.Diagnostics;
using System.Text;

public class CmdHelper
{
    private static string CmdPath = @"C:\Windows\System32\cmd.exe";

    /// <summary>
    /// 同步执行cmd命令
    /// 多命令请使用批处理命令连接符：
    /// <![CDATA[
    /// &:同时执行两个命令
    /// |:将上一个命令的输出,作为下一个命令的输入
    /// &&：当&&前的命令成功时,才执行&&后的命令
    /// ||：当||前的命令失败时,才执行||后的命令]]>
    /// 其他请百度
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="output"></param>
    public static string RunCmd(string cmd)
    {
        try
        {
            cmd = cmd.Trim().TrimEnd('&') + "&exit";//说明：不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时，会处于假死状态
            using (Process p = new Process())
            {
                p.StartInfo.FileName = CmdPath;
                p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                p.Start();//启动程序

                //向cmd窗口写入命令
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.AutoFlush = true;

                //获取cmd窗口的输出信息
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();//等待程序执行完退出进程
                p.Close();
                return output;
            }
        }
        catch (Exception ex)
        {
            LogHelper.Error(ex.Message);
            return string.Empty;
        }
    }


    public static string outputstring = "";
    public static Process process = null;
    public static string encode = "gb2312";
  
    //初始化Process，只需要初始化一次
    public static void ProcessInit()
    {
        process = new Process();
        //启动Windows的cmd控制台
        process.StartInfo.FileName = CmdPath;
        //启动进程时不使用 shell
        process.StartInfo.UseShellExecute = false;
        //设置标准重定向输入
        process.StartInfo.RedirectStandardInput = true;
        //设置标准重定向输出
        process.StartInfo.RedirectStandardOutput = true;
        //设置标准重定向错误输出
        process.StartInfo.RedirectStandardError = true;
        //设置不显示cmd控制台窗体
        process.StartInfo.CreateNoWindow = true;
        //设置编码
        process.StartInfo.StandardErrorEncoding = Encoding.GetEncoding("gb2312");
        process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding("gb2312");
        //隐藏窗体
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        //设置回调函数，异步读取指令回复
        process.OutputDataReceived += new DataReceivedEventHandler(ProcessOutputHandler);
        process.ErrorDataReceived += new DataReceivedEventHandler(ProcessErrorHandler);
    }
    //设置回调，读取指令的错误信息
    public static void ProcessErrorHandler(object sendingProcess, DataReceivedEventArgs outLine)
    {
        try
        {
            if (!string.IsNullOrEmpty(outLine.Data))
            {
                outputstring = outLine.Data;
                byte[] bytestring = Encoding.Default.GetBytes(outputstring);
                string outs = Encoding.GetEncoding(encode).GetString(bytestring);
                CloudManagerForm.f.showMessage(outs);
                LogHelper.Info("cmd显示：" + outputstring);
            }
        }
        catch (Exception ex)
        {
            LogHelper.Error(ex.Message);
        }
    }
    //设置回调，读取指令的返回值
    public static void ProcessOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
    {
        try
        {
            if(outLine.Data.Contains("Microsoft"))
            {
                return;
            }
            if (!string.IsNullOrEmpty(outLine.Data))
            {         
                string outputstring =outLine.Data;
                byte[] bytestring = Encoding.Default.GetBytes(outputstring);
                string outs = Encoding.GetEncoding(encode).GetString(bytestring);
                CloudManagerForm.f.showMessage(outs);
                LogHelper.Info("cmd显示：" + outputstring);
            }
        }
        catch (Exception ex)
        {
            LogHelper.Error(ex.Message);
        }
    }

    //指令发送函数，tclCommand为需要执行的cmd指令
    public static void ExecuteTclCommand(string tclCommand)
    {
        outputstring = "";
        process.StandardInput.WriteLine(tclCommand);
        process.StandardInput.AutoFlush = true;
    }


}
