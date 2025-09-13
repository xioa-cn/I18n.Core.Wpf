using I18n.Core.Core;
using System;

namespace I18n.Core.Example
{
    public class I18nManager
    {
        private static readonly Lazy<I18nCore> _instance = new Lazy<I18nCore>(
            () => new I18nCore());
        public static I18nCore Instance => _instance.Value;
    }
}
