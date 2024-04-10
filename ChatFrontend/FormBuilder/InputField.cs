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
        private FontFamily fontFamily;

        private SolidColorBrush foreground;

        Grid field;
        Border border;
        TextBlock placeholder;
        TextBox textBox;

        SolidColorBrush focusColor;
        Thickness focusThickness;

        SolidColorBrush unfocusColor;
        Thickness unfocusThickness;

        public InputField()
        { }
        public void Configure(FontFamily fontFamily, SolidColorBrush foreground, SolidColorBrush background,
            SolidColorBrush unfocusColor, Thickness unfocusThickness, 
            SolidColorBrush focusColor, Thickness focusThickness, double maxWidth, double fontSize,
            double cornerRadius)
        {
            this.foreground = foreground;
            this.focusThickness = focusThickness;
            this.focusColor = focusColor;
            this.unfocusColor = unfocusColor;
            this.unfocusThickness = unfocusThickness;
            this.fontFamily = fontFamily;

            field = new Grid();
            border = CreateBorder(cornerRadius, background, maxWidth);
            textBox = new TextBox();
            textBox.FontSize = fontSize;
        }
        public Grid Build()
        {
            textBox.Background = Brushes.Transparent;
            textBox.BorderThickness = new Thickness(0);
            textBox.FontFamily = fontFamily;
            textBox.Foreground = foreground;

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

        public Border CreateBorder(double cornerRadius, SolidColorBrush background, double maxWidth)
        {
            Border border = new Border();

            border.BorderThickness = unfocusThickness;
            border.CornerRadius = new CornerRadius(cornerRadius);
            border.Background = background;
            border.BorderBrush = unfocusColor;
            border.Margin = new Thickness(5);
            border.Padding = new Thickness(5);
            border.MaxWidth = maxWidth;

            return border;
        }
        public void AddPlaceholder(string placeholderText, SolidColorBrush placeholderForeground)
        {
            TextBlock placeholder = new TextBlock();
            placeholder.Text = placeholderText;
            placeholder.FontSize = textBox.FontSize;
            placeholder.FontFamily = fontFamily;
            placeholder.Foreground = placeholderForeground;
            placeholder.VerticalAlignment = VerticalAlignment.Center;
            placeholder.HorizontalAlignment = HorizontalAlignment.Left;
            placeholder.Margin = new Thickness(1, 0, 0, 0);
            placeholder.Opacity = 0.5;
            Panel.SetZIndex(placeholder, -1);

            this.placeholder = placeholder;
        }
    }
}
