using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;

namespace PS_TEMA3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void ChangeLanguage(string langCode)
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (langCode)
            {
                case "en":
                    dict.Source = new Uri("Languages/Resources.xaml", UriKind.Relative);
                    break;
                case "ro":
                    dict.Source = new Uri("Languages/Resources.ro.xaml", UriKind.Relative);
                    break;
                case "es":
                    dict.Source = new Uri("Languages/Resources.es.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("Languages/Resources.xaml", UriKind.Relative);
                    break;
            }

            // Remove the old resource dictionary
            ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                          where d.Source != null && d.Source.OriginalString.StartsWith("Languages/Resources.")
                                          select d).FirstOrDefault();

            if (oldDict != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);
            }

            // Add the new resource dictionary
            Application.Current.Resources.MergedDictionaries.Add(dict);

            // Optional: Change the culture for formatting
            CultureInfo.CurrentCulture = new CultureInfo(langCode);
            CultureInfo.CurrentUICulture = new CultureInfo(langCode);
        }
    }

}
