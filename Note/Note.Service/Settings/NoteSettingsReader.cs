using System;
using Note.Service.Settings;

namespace Note.Service.Settings
{
    public class NoteSettingsReader
    {
        public static NoteSettings Read(IConfiguration configuration)
        {
            // здесь будет чтение настроек приложения из конфига
            return new NoteSettings();
        }
    }
}