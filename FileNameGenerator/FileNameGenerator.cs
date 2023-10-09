
namespace System
{
    public static class FileNameGenerator
    {
        private const string DefaultPrefix = "file";

        private const string DefaultSuffix = ".txt";

        private const string DefaultDateTimeFormat = "yyyyMMddHHmmss";

        private const string DefaultPrefixSeparator = "_";

        private const string DefaultTemplate = "{prefix}{separator}{dateTime}{suffix}";

        //
        // 摘要:
        //     文件名生成器
        //     按照{前缀名}{分隔符}{日期时间格式字符串}{后缀}的方式生成，可分别传入对应内容或传入指定的文件模板，传入指定的文件模板时将按照对应的格式生成文件名
        //     模板格式必须为xxx{dateTime:xxx}xxx，其中，{dateTime:xxx}中的xxx为日期时间格式字符串
        //
        // 参数:
        //   dateTime:
        //     时间
        //
        //   prefix:
        //     文件名前缀
        //
        //   dateTimeFormat:
        //     日期时间格式
        //
        //   suffix:
        //     后缀
        //
        //   prefixSeparator:
        //     前缀分隔符
        //
        //   template:
        //     文件名模板
        //
        // 言论：
        //     当传入模板时，此时传入其他参数，只有日期时间可以生效。
        public static string Generate(DateTime? dateTime = null, string prefix = null, string dateTimeFormat = null, string suffix = null, string prefixSeparator = null, string template = null)
        {
            if (prefix == null)
            {
                prefix = "file";
            }

            if (suffix == null)
            {
                suffix = ".txt";
            }

            if (dateTimeFormat == null)
            {
                dateTimeFormat = "yyyyMMddHHmmss";
            }

            if (prefixSeparator == null)
            {
                prefixSeparator = "_";
            }

            if (!dateTime.HasValue)
            {
                dateTime = DateTime.Now;
            }

            string newValue = dateTime.Value.ToString(dateTimeFormat);
            if (template == null)
            {
                template = "{prefix}{separator}{dateTime}{suffix}";
                string text = template.Replace("{prefix}", prefix).Replace("{suffix}", suffix).Replace("{dateTime}", newValue);
                if (prefixSeparator != null)
                {
                    text = text.Replace("{separator}", prefixSeparator);
                }

                return text;
            }

            return GenerateFileNameFromTemplate(dateTime.Value, template);
        }

        private static string GeneraFileNameFromDefault(DateTime dateTime)
        {
            return "{prefix}{separator}{dateTime}{suffix}".Replace("{prefix}", "file").Replace("{suffix}", ".txt").Replace("{separator}", "_")
                .Replace("{dateTime}", dateTime.ToString("yyyyMMddHHmmss"));
        }

        private static string GenerateFileNameFromTemplate(DateTime dateTime, string template)
        {
            int num = template.IndexOf("{dateTime:");
            if (num < 0)
            {
                return GeneraFileNameFromDefault(dateTime);
            }

            int num2 = template.IndexOf("}", num);
            if (num2 < 0)
            {
                return GeneraFileNameFromDefault(dateTime);
            }

            string text = template.Substring(num + "{dateTime:".Length, num2 - num - "{dateTime:".Length);
            string newValue = dateTime.ToString(string.IsNullOrEmpty(text) ? "yyyyMMddHHmmss" : text);
            string oldValue = template.Substring(num, num2 - num + 1);
            return template.Replace(oldValue, newValue);
        }
    }
}
