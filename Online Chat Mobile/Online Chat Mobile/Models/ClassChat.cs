using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Online_Chat_Mobile.Models
{
    public class ClassChat
    {
        #region Variable
        public string Name { get; set; }

        public string Date { get; set; }
        public string Message { get; set; }

        #endregion

        /// <summary>
        /// Create StackLayout Message
        /// </summary>
        /// <param name="layoutOptions">Position Right or Left</param>
        /// <param name="colorBack">Color back</param>
        /// <param name="colorFront">Color front</param>
        /// <returns>return StackLayout</returns>
        public StackLayout StackLayoutChat(LayoutOptions layoutOptions, Color colorBack, Color colorFront)
        {
            var stack = new StackLayout{HorizontalOptions = layoutOptions, BackgroundColor = colorBack, WidthRequest = 200 };
            var position = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label
                    {
                        Text = Name,
                        FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                        TextColor = colorFront, 
                        Margin = 5
                    },
                    new Label
                    {
                        Text = Date,
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                        TextColor = colorFront,
                        Margin = 5
                    }
                }
            }; 
            stack.Children.Add(new Label
            {
                Text = Message,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = colorFront,
                Margin = 5
            });
            stack.Children.Add(position);
            

            return stack;
        }
    }
}
