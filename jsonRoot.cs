public class Gcs
    {
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string accessKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string secretKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string api { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string path { get; set; }
    }

    public class Local
    {
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string accessKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string secretKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string api { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string path { get; set; }
    }

    public class Minioxzx
    {
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string accessKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string secretKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string api { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string path { get; set; }
    }

    public class Play
    {
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string accessKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string secretKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string api { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string path { get; set; }
    }

    public class S3
    {
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string accessKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string secretKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string api { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string path { get; set; }
    }

    public class Aliases
    {
        /// <summary>
        /// 
        /// </summary>
        public Gcs gcs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Local local { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Minioxzx minioxzx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Play play { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public S3 s3 { get; set; }
    }

    public class jsonRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Aliases aliases { get; set; }
    }

