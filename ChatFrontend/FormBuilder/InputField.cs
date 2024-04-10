using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;

namespace ChatFrontend.FormBuilder
{
    public class InputField
    {
        private static FontFamily defaultFontFamily = new FontFamily("Open Sans");
        private static SolidColorBrush defaultForeground = new SolidColorBrush(Color.FromRgb(238, 238, 238));

        Grid field;
        Border border;
        TextBlock placeholder;
        TextBox textBox;

        SolidColorBrush focusColor = new SolidColorBrush(Color.FromRgb(36, 15, 90));
        Thickness focusThickness = new Thickness(3);

        SolidColorBrush unfocusColor = new SolidColorBrush(Color.FromRgb(26, 26, 26));
        Thickness unfocusThickness = new Thickness(1);

        public InputField()
        {
            field = new Grid();
            border = CreateBorder();
            textBox = new TextBox();
        }
        public Grid Build()
        {
            textBox.FontSize = 18;
            textBox.Background = Brushes.Transparent;
            textBox.BorderThickness = new Thickness(0);
            textBox.FontFamily = defaultFontFamily;
            textBox.Foreground = defaultForeground;

            Grid gridWithPlaceholder = new Grid();
            gridWithPlaceholder.Children.Add(textBox);

            if (placeholder != null)
            {
                gridWithPlaceholder.Children.Add(placeholder);
                textBox.SelectionChanged += textBox_SelectionChanged;
            }

            textBox.GotFocus += textBox_GotFocus;
            textBox.LostFocus += textBox_LostFocus;

            border.Child = gridWithPlaceholder;

            field.Children.Add(border);

            return field;
        }
        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            border.BorderBrush = focusColor;
            border.BorderThickness = focusThickness;
            Keyboard.Focus(border);
        }
        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            border.BorderBrush = unfocusColor;
            border.BorderThickness = unfocusThickness;
        }

        private void textBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length == 0)
            {
                placeholder.Visibility = Visibility.Visible;
            }
            else
            {
                placeholder.Visibility = Visibility.Collapsed;
            }
        }


        private static SolidColorBrush defaultBorderBackground = new SolidColorBrush(Color.FromRgb(59, 59, 59));
        private static SolidColorBrush defaultBorderBrush = new SolidColorBrush(Color.FromRgb(26, 26, 26));

        public static Border CreateBorder(double borderThickness = 1, double cornerRadius = 5,
            SolidColorBrush borderBrush = null, SolidColorBrush background = null,
            double padding = 5, double margin = 5, int maxWidth = 350)
        {
            if (borderBrush == null) borderBrush = defaultBorderBrush;
            if (background == null) background = defaultBorderBackground;

            Border border = new Border();

            border.BorderThickness = new Thickness(borderThickness);
            border.CornerRadius = new CornerRadius(cornerRadius);
            border.Background = background;
            border.BorderBrush = borderBrush;
            border.Margin = new Thickness(margin);
            border.Padding = new Thickness(padding);
            border.MaxWidth = maxWidth;

            return border;
        }
        private static SolidColorBrush defaultPlaceholderForeground = new SolidColorBrush(Color.FromRgb(238, 238, 238));
        public void AddPlaceholder(string placeholderText, double fontSize = 18,
            FontFamily fontFamily = null,
            SolidColorBrush foreground = null)
        {
            if (fontFamily == null) fontFamily = defaultFontFamily;
            if (foreground == null) foreground = defaultPlaceholderForeground;

            TextBlock placeholder = new TextBlock();
            placeholder.Text = placeholderText;
            placeholder.FontSize = fontSize;
            placeholder.FontFamily = fontFamily;
            placeholder.Foreground = foreground;
            placeholder.VerticalAlignment = VerticalAlignment.Center;
            placeholder.HorizontalAlignment = HorizontalAlignment.Left;
            placeholder.Margin = new Thickness(1, 0, 0, 0);
            placeholder.Opacity = 0.5;
            Panel.SetZIndex(placeholder, -1);

            this.placeholder = placeholder;
        }
    }
}
