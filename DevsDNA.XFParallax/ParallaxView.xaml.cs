namespace DevsDNA.XFParallax
{
    using Xamarin.Forms;

    public partial class ParallaxView : ContentView
    {
        private double lastScroll = 0;

        public static readonly BindableProperty HeaderContentProperty = BindableProperty.Create(nameof(HeaderContent), typeof(ContentView), typeof(ParallaxView), coerceValue: HeaderContentCoerceValue);
        public static readonly BindableProperty HeaderScrollSpeedProperty = BindableProperty.Create(nameof(HeaderScrollSpeed), typeof(int), typeof(ParallaxView), 2);
        public static readonly BindableProperty BodyContentProperty = BindableProperty.Create(nameof(BodyContent), typeof(ContentView), typeof(ParallaxView), coerceValue: BodyContentCoerceValue);
        public static readonly BindableProperty BodyMarginProperty = BindableProperty.Create(nameof(BodyMargin), typeof(Thickness), typeof(ParallaxView), new Thickness(0), coerceValue: BodyMarginCoerceValue);

        public ParallaxView()
        {
            InitializeComponent();
            ParentScroll.Scrolled += ParentScroll_Scrolled;
        }

        public ContentView HeaderContent
        {
            get { return (ContentView)GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }

        public int HeaderScrollSpeed
        {
            get { return (int)GetValue(HeaderScrollSpeedProperty); }
            set { SetValue(HeaderScrollSpeedProperty, value); }
        }

        public ContentView BodyContent
        {
            get { return (ContentView)GetValue(BodyContentProperty); }
            set { SetValue(BodyContentProperty, value); }
        }

        public Thickness BodyMargin
        {
            get { return (Thickness)GetValue(BodyMarginProperty); }
            set { SetValue(BodyMarginProperty, value); }
        }

        /// <summary>
        /// Important to call this method from Page OnDissapearing to remove event handlers and avoid memory leaks.
        /// </summary>
        public void DestroyParallaxView()
        {
            ParentScroll.Scrolled -= ParentScroll_Scrolled;
        }

        private static object HeaderContentCoerceValue(BindableObject bindableObject, object value)
        {
            if (bindableObject != null && value != null && value is ContentView)
            {
                ParallaxView instance = (ParallaxView)bindableObject;

                instance.Header.Content = (ContentView)value;
            }
            return value;
        }

        private static object BodyContentCoerceValue(BindableObject bindableObject, object value)
        {
            if (bindableObject != null && value != null && value is ContentView)
            {
                ParallaxView instance = (ParallaxView)bindableObject;

                instance.Body.Content = (ContentView)value;

                if (instance.BodyMargin != null)
                {
                    instance.Body.Margin = instance.BodyMargin;
                }
            }
            return value;
        }

        private static object BodyMarginCoerceValue(BindableObject bindableObject, object value)
        {
            if (bindableObject != null && value != null && value is Thickness)
            {
                ParallaxView instance = (ParallaxView)bindableObject;

                if (instance.Body != null)
                {
                    instance.Body.Margin = instance.BodyMargin;
                }
            }
            return value;
        }

        private void ParentScroll_Scrolled(object sender, ScrolledEventArgs e)
        {
            if (lastScroll == 0)
                lastScroll = e.ScrollY;
            else
            {
                CalculateHeaderTranslation(e);
            }
        }

        private void CalculateHeaderTranslation(ScrolledEventArgs e)
        {
            double translation = 0;

            if (lastScroll < e.ScrollY)
            {
                translation = 0 - ((e.ScrollY / HeaderScrollSpeed));
                if (translation > 0)
                    translation = 0;
            }
            else
            {
                translation = 0 + ((e.ScrollY / HeaderScrollSpeed));
                if (translation > 0)
                    translation = 0;
            }
            Header.TranslateTo(Header.TranslationX, translation);
            lastScroll = e.ScrollY;
        }
    }
}
