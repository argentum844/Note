using System;
using Note.WebAPI.Settings;

namespace Note.WebAPI.Settings
{
    public static class NoteSettingsReader
    {
        public static NoteSettings Read(IConfiguration configuration)
        {
            // здесь будет чтение настроек приложения из конфига
            return new NoteSettings();
        }
    }
}