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


        //SolidColorBrush defaultFocusBorderBrush = new SolidColorBrush(Color.FromRgb(145, 87, 182));
        SolidColorBrush defaultFocusBorderBrush = new SolidColorBrush(Color.FromRgb(26, 35, 168));
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
        public InputField AddInputField(string placeholderText, bool withErrorField = true, double width = 350, double maxWidth = 350, double minWidth = 100,
            double fontSize = 18, bool acceptReturn = false, int maxLines = 1, TextWrapping textWrapping = TextWrapping.Wrap)
        {
            InputField field = new InputField();

            field.Configure(defaultFontFamily, defaultInputFieldForeground, 
                defaultBorderBackground, defaultBorderBrush, 
                defaultBorderThickness, defaultFocusBorderBrush, 
                defaultFocusBorderThickness, width, maxWidth, minWidth, fontSize, defaultCornerRadius, acceptReturn, maxLines, textWrapping);

            field.AddPlaceholder(placeholderText, defaultPlaceholderForeground);
            if (withErrorField)
            {
                field.BuildErrorField(defaultMaxWidth);
            }
            stackPanelForm.Children.Add(field.Build());
            return field;
        }
        public PasswordField AddPasswordInputField(string placeholderText)
        {
            PasswordField field = new PasswordField();

            field.Configure(defaultFontFamily, defaultInputFieldForeground,
                defaultBorderBackground, defaultBorderBrush,
                defaultBorderThickness, defaultFocusBorderBrush,
                defaultFocusBorderThickness, defaultMaxWidth, defaultFontSize, defaultCornerRadius);

            field.AddPlaceholder(placeholderText, defaultPlaceholderForeground);
            field.BuildErrorField(defaultMaxWidth);
            stackPanelForm.Children.Add(field.Build());
            return field;
        }
        public StackPanel Build() { return stackPanelForm; }
    }
}
