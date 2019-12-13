using System;

namespace ScrapingFunction.Helpers
{
    public class DataLengthHelper
    {
        public string GetDataSize(float dataLength)
        {
            string dataSize;
            if (dataLength >= 1048576)
            {
                dataSize = String.Format("{0:0.00}", dataLength / 1048576) + " MB";
            }
            else if (dataLength >= 1024)
            {
                dataSize = String.Format("{0:0.00}", dataLength / 1024) + " KB";
            }
            else
            {
                dataSize = dataLength + " B";
            }

            return dataSize;
        }
    }
}
