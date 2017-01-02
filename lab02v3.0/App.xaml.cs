using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;

namespace lab02v3._0
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static List<CultureInfo> m_Languages = new List<CultureInfo>();
        public static List<CultureInfo> Languages
        {
            get { return m_Languages; }
        }
        public App()
        {
            m_Languages.Clear();
            m_Languages.Add(new CultureInfo("ru-RU"));//нейтральная культура
            m_Languages.Add(new CultureInfo("en-US"));
        }

        public static event EventHandler LanguageChanged;

        public static CultureInfo Language
        {
            get { return System.Threading.Thread.CurrentThread.CurrentUICulture; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                //меняем язык приожения
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;
                //создаем ResourceDictionary для новой культуры
                ResourceDictionary dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case "en-US":
                        dict.Source = new Uri(String.Format("Res/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri("Res/lang.xaml", UriKind.Relative);
                        break;
                }
                //находим старую ResourceDictionary и удаляем его и добавляем новую
                ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.StartsWith("Res/lang.")
                                              select d).First();
                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }
                //вызываем евент для оповещения всех окон
                LanguageChanged(Application.Current, new EventArgs());
            }

        }

        private void Application_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            Language = lab02v3._0.Properties.Settings.Default.DefaultLanguage;
        }

        private void App_LanguageChanged(Object sender, EventArgs e)
        {
            lab02v3._0.Properties.Settings.Default.DefaultLanguage = Language;
            lab02v3._0.Properties.Settings.Default.Save();
        }
    }
}
