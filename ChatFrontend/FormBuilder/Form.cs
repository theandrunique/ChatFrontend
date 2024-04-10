using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChatFrontend.FormBuilder
{
    public class Form
    {
        private static FontFamily defaultFontFamily = new FontFamily("Open Sans");
        private static SolidColorBrush defaultInputFieldForeground = new SolidColorBrush(Color.FromRgb(238, 238, 238));


        SolidColorBrush defaultFocusBorderBrush = new SolidColorBrush(Color.FromRgb(36, 15, 90));
        Thickness defaultFocusBorderThickness = new Thickness(3);

        SolidColorBrush defaultBorderBrush = new SolidColorBrush(Color.FromRgb(26, 26, 26));
        Thickness defaultBorderThickness = new Thickness(1);

        SolidColorBrush defaultBorderBackground = new SolidColorBrush(Color.FromRgb(59, 59, 59));

        SolidColorBrush defaultPlaceholderForeground = new SolidColorBrush(Color.FromRgb(238, 238, 238));

        SolidColorBrush defaultHeaderForeground = new SolidColorBrush(Color.FromRgb(143, 62, 194));
        double defaultMaxWidth = 350;
        double defaultFontSize = 18;
        double defaultCornerRadius = 5;

        private StackPanel stackPanelForm = new StackPanel();
        public Form() { }
        public void AddHeader(string headerText,
            Thickness margin,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center,
            double fontSize = 30,
            SolidColorBrush foreground = null,
            FontFamily fontFamily = null)
        {
            if (foreground == null) foreground = defaultHeaderForeground;
            if (fontFamily == null) fontFamily = defaultFontFamily;

            TextBox header = new TextBox();
            header.Text = headerText;
            header.Background = Brushes.Transparent;
            header.IsEnabled = false;
            header.HorizontalContentAlignment = horizontalAlignment;
            header.BorderThickness = new Thickness(0);
            header.FontSize = fontSize;
            header.Margin = margin;
            header.Foreground = foreground;
            header.FontWeight = FontWeights.Bold;
            header.FontFamily = fontFamily;

            stackPanelForm.Children.Add(header);
        }
        public void AddInputField(string placeholderText)
        {
            InputField field = new InputField();

            field.Configure(defaultFontFamily, defaultInputFieldForeground, 
                defaultBorderBackground, defaultBorderBrush, 
                defaultBorderThickness, defaultFocusBorderBrush, 
                defaultFocusBorderThickness, defaultMaxWidth, defaultFontSize, defaultCornerRadius);

            field.AddPlaceholder(placeholderText, defaultPlaceholderForeground);
            stackPanelForm.Children.Add(field.Build());
        }
        public StackPanel Build() { return stackPanelForm; }
    }
}
