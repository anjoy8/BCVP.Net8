namespace BCVP.Net8.Common
{
    public class AOPLogInfo
    {
        /// <summary>
        /// 请求时间
        /// </summary>
        public string RequestTime { get; set; } = string.Empty;
        /// <summary>
        /// 操作人员
        /// </summary>
        public string OpUserName { get; set; } = string.Empty;
        /// <summary>
        /// 请求方法名
        /// </summary>
        public string RequestMethodName { get; set; } = string.Empty;
        /// <summary>
        /// 请求参数名
        /// </summary>
        public string RequestParamsName { get; set; } = string.Empty;
        /// <summary>
        /// 请求参数数据JSON
        /// </summary>
        public string RequestParamsData { get; set; } = string.Empty;
        /// <summary>
        /// 请求响应间隔时间
        /// </summary>
        public string ResponseIntervalTime { get; set; } = string.Empty;
        /// <summary>
        /// 响应时间
        /// </summary>
        public string ResponseTime { get; set; } = string.Empty;
        /// <summary>
        /// 响应结果
        /// </summary>
        public string ResponseJsonData { get; set; } = string.Empty;
    }
}
