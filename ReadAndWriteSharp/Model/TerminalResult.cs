namespace Swsk33.ReadAndWriteSharp.Model
{
    /// <summary>
    /// 终端实用类的输出结果
    /// </summary>
    public class TerminalResult
    {
        /// <summary>
        /// 标准输出
        /// </summary>
        private string StandardOutput;

        /// <summary>
        /// 标准错误
        /// </summary>
        private string ErrorOutput;

        /// <summary>
        /// 命令是否执行完成
        /// </summary>
        private bool Finished = false;

        public string GetStandardOutput()
        {
            return StandardOutput;
        }

        public void SetStandardOutput(string standardOutput)
        {
            this.StandardOutput = standardOutput;
        }

        public string GetErrorOutput()
        {
            return ErrorOutput;
        }

        public void SetErrorOutput(string errorOutput)
        {
            this.ErrorOutput = errorOutput;
        }

        public bool IsFinished()
        {
            return Finished;
        }

        public void SetFinished(bool Finished)
        {
            this.Finished = Finished;
        }
    }
}