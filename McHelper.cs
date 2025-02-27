using log4cxx;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CloudManager
{
    class McHelper
    {
        /// <summary>
        /// 初始化主机
        /// </summary>
        public static void McInit(string minioApiIp, string minioApiPort, string miniouser, string miniopassword)
        {
            CmdHelper.ProcessInit();
            string jsonfile = Environment.GetEnvironmentVariable("USERPROFILE") + @"\mc\config.json";
            if (!File.Exists(jsonfile))
            {
                string cmd = @"mc config host ls";
                string outPut = CmdHelper.RunCmd(cmd);
                LogHelper.Info(outPut);
            }
            string api = "http://" + minioApiIp + ":" + minioApiPort;
            string value = tools.GetJsonFile(jsonfile);
            jsonRoot root = JsonConvert.DeserializeObject<jsonRoot>(value);
            if (root.aliases.minioxzx == null)
            {
                LogHelper.Info("客户端未检测到主机，尝试添加主机" + api);
                string cmd = @"mc config host add minioxzx " + api + " " + miniouser + " " + miniopassword;
                string outPut = CmdHelper.RunCmd(cmd);
                if (outPut.Contains("successfully."))
                {
                    LogHelper.Info("客户端添加主机成功!");
                }
                else
                {
                    LogHelper.Info("客户端添加主机失败,请检查主机是否存在或在线" + api);
                }
            }
            else
            {
                string minioAPI = root.aliases.minioxzx.url;
                if (!api.Equals(minioAPI))
                {
                    LogHelper.Info("客户端检测到主机" + minioAPI + "，但主机信息与配置不匹配，尝试修改主机为" + api);
                    string cmd = @"mc config host remove minioxzx";
                    string outPut = CmdHelper.RunCmd(cmd);
                    if (outPut.Contains("successfully."))
                    {
                        string cmd1 = @"mc config host add minioxzx " + api + " " + miniouser + " " + miniopassword;
                        string outPut1 = CmdHelper.RunCmd(cmd1);
                        if (outPut1.Contains("successfully."))
                        {
                            LogHelper.Info("客户端修改主机成功!");
                        }
                        else
                        {
                            LogHelper.Info("客户端修改主机失败,请检查主机是否存在或在线" + api);
                        }
                    }
                }
                else
                {
                    LogHelper.Info("获取主机" + api + "成功!");
                }
            }
        }
        //输入原命令
        public static void InputCmd(string cmd)
        {
            try
            {
                CmdHelper.ExecuteTclCommand(cmd);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 判断服务器是否存在
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public static bool ExsiteServer(string serverName)
        {
            bool exsit = false;
            string jsonfile = Environment.GetEnvironmentVariable("USERPROFILE")+ @"\mc\config.json";
            if (!File.Exists(jsonfile))
            {
                string cmd = @"mc config host ls";
                string outPut = CmdHelper.RunCmd(cmd);
                LogHelper.Info(outPut);
            }
            string json = tools.GetJsonFile(jsonfile);
            //解析
            Newtonsoft.Json.Linq.JObject resultObject = Newtonsoft.Json.Linq.JObject.Parse(json);
            //转换成列表（取得Content）
            List<Newtonsoft.Json.Linq.JToken> listJToken = resultObject["aliases"].Children().ToList();
            //遍历
            foreach (var item in listJToken)
            {
                //转成键值对格式
                var temp_item = (Newtonsoft.Json.Linq.JProperty)item;
                if (temp_item.Name.Equals(serverName))
                {
                    exsit = true;
                }
            }

            return exsit;
        }
        /// <summary>
        /// 查询所有服务器
        /// </summary>
        public static void GetServers()
        {
            try
            {
                string cmd = @"mc config host ls";
                CmdHelper.ExecuteTclCommand(cmd);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 查询集群信息
        /// </summary>
        public static void GetClusterInfo(string serverName)
        {
            try
            {
                string cmd = @"mc admin info "+ serverName;
                CmdHelper.ExecuteTclCommand(cmd);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 增加服务器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void AddServer(string name, string server, string port, string username, string password)
        {
            try
            {
                if (!ExsiteServer(name))
                {
                    string cmd = @"mc config host add " + name + " http://" + server + ":" + port + " " + username + " " + password;
                    CmdHelper.ExecuteTclCommand(cmd);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("服务器已存在");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 删除服务器
        /// </summary>
        /// <param name="name"></param>
        public static void RemoveServer(string name)
        {
            try
            {
                if (ExsiteServer(name))
                {
                    string cmd = @"mc config host remove " + name;
                    CmdHelper.ExecuteTclCommand(cmd);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("服务器不存在");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 查询服务器里的所有桶
        /// </summary>
        /// <param name="serverName"></param>
        public static void GetListBuckets(string serverName)
        {
            try
            {
                if (ExsiteServer(serverName))
                {
                    string cmd = @"mc ls " + serverName;
                    CmdHelper.ExecuteTclCommand(cmd);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("服务器不存在");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 添加桶
        /// </summary>
        /// <param name="bucketName"></param>
        public static void addBucket(string serverName, string bucketName)
        {
            try
            {
                if (ExsiteServer(serverName))
                {
                    string cmd = @"mc mb " + serverName + @"/" + bucketName;
                    CmdHelper.ExecuteTclCommand(cmd);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("服务器不存在");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 删除桶
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="bucketName"></param>
        public static void delBucket(string serverName, string bucketName)
        {
            try
            {
                if (ExsiteServer(serverName))
                {
                    string cmd = @"mc rb --force " + serverName + @"/" + bucketName;
                    CmdHelper.ExecuteTclCommand(cmd);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("服务器不存在");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 上传本地文件到minio桶中
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="bucketName"></param>
        /// <param name="obj"></param>
        public static void addObject(string serverName, string bucketName, string obj)
        {
            try
            {
                if (ExsiteServer(serverName))
                {
                    string cmd = @"mc cp " + obj + " " + serverName + @"/" + bucketName;
                    CmdHelper.ExecuteTclCommand(cmd);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("服务器不存在");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 查询指定桶
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="bucketName"></param>
        public static void getBucket(string serverName, string bucketName)
        {
            try
            {
                if (ExsiteServer(serverName))
                {
                    string cmd = @"mc ls " + serverName + @"/" + bucketName;
                    CmdHelper.ExecuteTclCommand(cmd);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("服务器不存在");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 下载对象到本地
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="bucketName"></param>
        /// <param name="obj"></param>
        public static void downloadObject(string serverName, string bucketName, string obj, string filePaht)
        {
            try
            {
                if (ExsiteServer(serverName))
                {
                    string cmd = @"mc cp " + serverName + @"/" + bucketName + @"/" + obj + " " + filePaht;
                    CmdHelper.ExecuteTclCommand(cmd);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("服务器不存在");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 查询服务器存储状态
        /// </summary>
        /// <param name="serverName"></param>
        public static void getServerDu(string serverName)
        {
            try
            {
                if (ExsiteServer(serverName))
                {
                    string cmd = @"mc du " + serverName;
                    CmdHelper.ExecuteTclCommand(cmd);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("服务器不存在");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 查询桶存储状态
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="bucketName"></param>
        public static void getBucketDu(string serverName, string bucketName)
        {
            try
            {
                if (ExsiteServer(serverName))
                {
                    string cmd = @"mc du " + serverName + @"/" + bucketName;
                    CmdHelper.ExecuteTclCommand(cmd);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("服务器不存在");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 查询对象分享的url期限4小时
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        public static void getShareUrl(string serverName, string bucketName, string objectName)
        {
            try
            {
                if (ExsiteServer(serverName))
                {
                    string cmd = @"mc share download --expire 4h  " + serverName + @"/" + bucketName + @"/" + objectName;
                    CmdHelper.ExecuteTclCommand(cmd);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("服务器不存在");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 查询对象分享的url期限4小时
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        public static void getUploadUrl(string serverName, string bucketName, string objectName)
        {
            try
            {
                if (ExsiteServer(serverName))
                {
                    string cmd = @"mc share upload --expire 4h  " + serverName + @"/" + bucketName + @"/" + objectName;
                    CmdHelper.ExecuteTclCommand(cmd);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("服务器不存在");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        public static void delObject(string serverName, string bucketName, string objectName)
        {
            try
            {
                if (ExsiteServer(serverName))
                {
                    string cmd = @"mc rm --force " + serverName + @"/" + bucketName + @"/" + objectName;
                    CmdHelper.ExecuteTclCommand(cmd);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("服务器不存在");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
