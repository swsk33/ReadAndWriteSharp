using System;

namespace MainTest
{
    [Serializable]
    class Model
    {
        private int code;

        private string name;

        public void SetCode(int code)
        {
            this.code = code;
        }

        public int GetCode()
        {
            return code;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }
    }
}
