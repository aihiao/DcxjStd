using System.Text;
using System.Diagnostics;

public class AssertHelper
{
    /// <summary>
    /// 不应该是true, 如果是true就报错
    /// </summary>
    /// <param name="tf"></param>
    /// <returns></returns>
    public static bool AssetTrue(bool tf, string message = null)
    {
        return Check(!tf, message);
    }

    /// <summary>
    /// 不应该是false，如果是false报错
    /// </summary>
    /// <param name="tf"></param>
    /// <returns></returns>
    public static bool AssetFalse(bool tf, string message = null)
    {
        return Check(tf, message);
    }

    /// <summary>
	/// 条件不成立打印提示并返回true
	/// </summary>
	/// <param name="condation">判断的条件</param>
	/// <param name="systemName">报错的系统名</param>
	/// <param name="errorMsg">提示信息</param>
	/// <returns></returns>
	public static bool AssertFalse(bool condation, string systemName, string errorMsg)
    {
        if (!condation)
        {
            LoggerManager.Instance.ErrorColor("{0} 出错 {1}", Defines.Fuchsia, systemName, errorMsg);
        }
        return !condation;
    }

    /// <summary>
    /// 为空打印错误并返回true
    /// </summary>
    /// <param name="obj">执行空判断的对象</param>
    /// <param name="systemName">报错的系统名</param>
    /// <param name="errorMsg">提示信息</param>
    /// <returns></returns>
    public static bool AssertNull(object obj, string systemName, string errorMsg)
    {
        if (obj.IsNull())
        {
            LoggerManager.Instance.ErrorColor("{0} 空指针 {1}", Defines.Fuchsia, systemName, errorMsg);
        }
        return obj.IsNull();
    }

    public static bool Check(bool condation)
    {
        return Check(condation, "");
    }

    public static bool Check(bool condation, string errorMsg)
    {
        if (!condation)
        {
            if (string.IsNullOrEmpty(errorMsg))
            {
                StringBuilder sb = new StringBuilder();

                // Output stack
                sb.AppendLine("Stack:");
                StackTrace stackTrace = new StackTrace();
                StackFrame[] stackFrames = stackTrace.GetFrames();
                foreach (var stackFrame in stackFrames)
                {
                    sb.AppendFormat("FileName = {0} MethodName = {1} LineNumber = {2}", stackFrame.GetFileName(), stackFrame.GetMethod().Name, stackFrame.GetFileLineNumber());
                    sb.AppendLine();
                }

                errorMsg = (sb.ToString());
            }

            LoggerManager.Instance.Error(errorMsg);
        }

        return condation;
    }

}