using Xamarin.Forms;

namespace Joy
{
    public partial class JoyPage : ContentPage
    {
        public JoyPage()
        {
            InitializeComponent();

            var game = new
                //HueAndFPSDemo();
                FlappyBird();
            gameView.Load(game);
        }
    }
}
