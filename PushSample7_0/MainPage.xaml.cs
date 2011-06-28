using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace PushSample7_0
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void SendUriToMe(object sender, System.Windows.RoutedEventArgs e)
        {
            EmailComposeTask ect = new EmailComposeTask();
            ect.Body = UriText.Text;
            ect.Subject = "Sample WP7 Notification Uri";

            ect.Show();
        }
    }
}
