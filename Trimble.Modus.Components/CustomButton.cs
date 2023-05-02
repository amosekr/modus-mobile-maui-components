﻿
using Microsoft.Maui.Controls.Shapes;


namespace Trimble.Modus.Components
{

    public class CustomButton : ContentView
    {
        private readonly Label _titleLabel;
        private readonly Image _iconImage;
        private readonly TapGestureRecognizer _tapGestureRecognizer;
        private bool imageSet = false;
        private bool isTextSet = false;
        private Border frame;
        private StackLayout stackLayout;


        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomButton), propertyChanged: OnTitleChanged);


        public static readonly BindableProperty IconSourceProperty =
            BindableProperty.Create(nameof(IconSource), typeof(ImageSource), typeof(CustomButton), propertyChanged: OnIconSourceChanged);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(Command), typeof(CustomButton), null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomButton), null);

        public static readonly BindableProperty SizeProperty =
            BindableProperty.Create(nameof(Size), typeof(ButtonSize), typeof(CustomButton), propertyChanged: OnSizeChanged);

        public static readonly BindableProperty IsFloatingButtonProperty =
            BindableProperty.Create(nameof(IsFloatingButton), typeof(bool), typeof(CustomButton), false, propertyChanged: OnIsFloatingButtonChanged);

        public static readonly BindableProperty IsDisabledProperty =
            BindableProperty.Create(nameof(IsDisabled), typeof(bool), typeof(CustomButton), false, propertyChanged: OnIsDisabledChanged);

        public static readonly BindableProperty TemplateProperty =
            BindableProperty.Create(nameof(Template), typeof(ControlTemplate), typeof(CustomButton));

      
        public ControlTemplate Template
        {
            get => (ControlTemplate)GetValue(ControlTemplateProperty);
            set => SetValue(ControlTemplateProperty, value);
        }

        public bool IsDisabled
        {
            get { return (bool)GetValue(IsDisabledProperty); }
            set { SetValue(IsDisabledProperty, value); }
        }

        public bool IsFloatingButton
        {
            get { return (bool)GetValue(IsFloatingButtonProperty); }
            set { SetValue(IsFloatingButtonProperty, value); }
        }
        public ButtonSize Size
        {
            get => (ButtonSize)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public ImageSource IconSource
        {
            get => (ImageSource)GetValue(IconSourceProperty);
            set => SetValue(IconSourceProperty, value);
        }

        public Command Command
        {
            get => (Command)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public CustomButton()
        {

            _titleLabel = new Label
            {
                TextColor = Colors.White,
            };

            _iconImage = new Image
            {
                WidthRequest = 24,
                HeightRequest = 24,

            };
            HorizontalOptions = LayoutOptions.Start;
            setDefault(this);

            stackLayout = new StackLayout();
            _iconImage.SetBinding(Image.SourceProperty, new Binding(nameof(IconSource), source: this));
            _titleLabel.VerticalOptions = LayoutOptions.Center;
            stackLayout.Orientation = StackOrientation.Horizontal;
            _titleLabel.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
            stackLayout.Children.Add(_iconImage);
            stackLayout.Children.Add(_titleLabel);
            frame = new Border
            {
                Padding = 0,

                Content = stackLayout,
                BackgroundColor = Color.FromArgb("#0063a3"),
                HorizontalOptions = LayoutOptions.Start,
                StrokeShape = new Rectangle
                {
                    RadiusX = 4,
                    RadiusY = 4
                }
            };



            Content = frame;
            

            GestureRecognizers.Add(_tapGestureRecognizer = new TapGestureRecognizer());
            _tapGestureRecognizer.Tapped += OnTapped;

            Console.WriteLine($"Size Custom: {Size}");


        }

        private void setDefault(CustomButton customButton)
        {
            customButton._titleLabel.FontSize = 16;
            if (customButton.imageSet)
            {
                customButton._titleLabel.Padding = new Thickness(0, 16, 24, 16);
                customButton._iconImage.Margin = new Thickness(16, 16, 8, 16);
                customButton._iconImage.IsVisible = true;
            }
            else
            {
                customButton._titleLabel.Padding = new Thickness(24, 16, 24, 16);
                customButton._iconImage.IsVisible = false;
            }
        }
        private void setFloatingButton(CustomButton customButton)
        {
            customButton._titleLabel.FontSize = 16;
            if (customButton.imageSet && !customButton.isTextSet)
            {

                customButton._iconImage.HorizontalOptions = LayoutOptions.Center;
                customButton._iconImage.IsVisible = true;
                customButton._titleLabel.IsVisible = false;
                customButton._iconImage.Margin = new Thickness(16);

            }
            if (customButton.isTextSet && !customButton.imageSet)
            {
                customButton._titleLabel.Padding = new Thickness(24, 16, 24, 16);
                customButton._iconImage.IsVisible = false;
                customButton._titleLabel.IsVisible = true;
            }
            if (customButton.imageSet && customButton.isTextSet)
            {
                customButton._titleLabel.Padding = new Thickness(0, 16, 24, 16);
                customButton._iconImage.Margin = new Thickness(16, 16, 8, 16);
                customButton._iconImage.IsVisible = true;
            }

        }

        private void OnTapped(object sender, EventArgs e)
        {
            Command?.Execute(CommandParameter);
            frame.BackgroundColor = Color.FromArgb("#0E416C");

            this.Dispatcher.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                frame.BackgroundColor = Color.FromArgb("#0063a3");
                return false;
            });
        }
        private void UpdateButtonStyle()
        {

            var hasText = isTextSet;
            var hasIcon = imageSet;

            if (IsFloatingButton)
            {
                frame.StrokeShape = new Rectangle
                {
                    RadiusX = 50,
                    RadiusY = 50
                };
                frame.Shadow = new Shadow
                {
                    Radius = 50,
                    Opacity = 100

                };
                setFloatingButton(this);
                Content = frame;
            }
        }

        private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomButton customButton)
            {
                customButton._titleLabel.Text = (string)newValue;
                customButton.isTextSet = true;
            }
        }

        private static void OnIconSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {

            if (bindable is CustomButton customButton)
            {

                customButton._iconImage.Source = (ImageSource)newValue;

                if (newValue != null)
                {

                    customButton.imageSet = true;


                }
                else
                {

                    customButton.imageSet = false;
                }
            }
        }

        private static void OnSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomButton customButton && !customButton.IsFloatingButton)
            {
                var size = (ButtonSize)newValue;
                Console.WriteLine($"Size OnSize: {size}");

                switch (size)
                {
                    case ButtonSize.XSmall:
                        customButton._titleLabel.FontSize = 12;
                        if (customButton.imageSet)
                        {
                            customButton._titleLabel.Padding = new Thickness(0, 8, 12, 8);
                            customButton._iconImage.Margin = new Thickness(8, 8, 8, 8);
                            customButton._iconImage.IsVisible = true;

                        }
                        else
                        {
                            customButton._titleLabel.Padding = new Thickness(12, 8, 12, 8);
                            customButton._iconImage.IsVisible = false;

                        }

                        break;
                    case ButtonSize.Small:
                        customButton._titleLabel.FontSize = 14;
                        if (customButton.imageSet)
                        {
                            customButton._titleLabel.Padding = new Thickness(0, 8, 16, 8);
                            customButton._iconImage.Margin = new Thickness(12, 8, 8, 8);
                            customButton._iconImage.IsVisible = true;


                        }
                        else
                        {
                            customButton._titleLabel.Padding = new Thickness(16, 8, 16, 8);
                            customButton._iconImage.IsVisible = false;

                            break;
                        }


                        break;

                    case ButtonSize.Default:
                    case ButtonSize.Large:
                        customButton._titleLabel.FontSize = 16;
                        if (customButton.imageSet)
                        {
                            customButton._titleLabel.Padding = new Thickness(0, 16, 24, 16);
                            customButton._iconImage.Margin = new Thickness(16, 16, 8, 16);
                            customButton._iconImage.IsVisible = true;
                        }
                        else
                        {
                            customButton._titleLabel.Padding = new Thickness(24, 16, 24, 16);
                            customButton._iconImage.IsVisible = false;

                        }



                        break;

                }

            }
        }

        private static void OnIsDisabledChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (CustomButton)bindable;

            if ((bool)newValue)
            {
                button.frame.BackgroundColor = Color.FromArgb("CEDEEB");

                button._iconImage.Opacity = 0.5;
                button.GestureRecognizers.Clear();

                button.GestureRecognizers.Clear();
            }
            else
            {

                button.frame.BackgroundColor = Color.FromArgb("#0063a3");
                button._titleLabel.TextColor = Colors.White;
                button._iconImage.Opacity = 1;
                button.GestureRecognizers.Add(button._tapGestureRecognizer);
            }
        }



        private static void OnIsFloatingButtonChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (CustomButton)bindable;
            button.UpdateButtonStyle();
        }


     
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > 0 && height > 0)
            {
                
                var handler = this.Handler as IViewHandler;

                
                var measuredSize = handler?.GetDesiredSize(double.PositiveInfinity, double.PositiveInfinity);

                
                if (measuredSize != null)
                {
                    this.WidthRequest = frame.Width;
                    this.HeightRequest = frame.Height;
                }
            }
        }



    }
}