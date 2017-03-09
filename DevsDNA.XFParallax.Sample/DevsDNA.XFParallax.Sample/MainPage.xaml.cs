namespace DevsDNA.XFParallax.Sample
{
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MainParallax.DestroyParallaxView();
        }
    }
}
