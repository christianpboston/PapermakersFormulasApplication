using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaperScience
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeadboxFreeJetLength : ContentPage
    {
        public HeadboxFreeJetLength()
        {
            InitializeComponent();
            
            if (App.switched)
            {
                units.IsToggled = true;
            }
            else
            {
                units.IsToggled = false;
            }
        }

        /*
        * Switch changed
        */
        private void Units_Toggled(object sender, ToggledEventArgs e)
        {
            bool isToggled = e.Value;

            if (isToggled)
            {
                s.Text = "SI";
                label1.Text = "Initial jet velocity (m/min)";
                label2.Text = "Jet angle (°)";
                label3.Text = "Height of apron tip to wire (m)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Initial jet velocity (ft/min)";
                label2.Text = "Jet angle (°)";
                label3.Text = "Height of apron tip to wire (in.)";
                App.switched = false;
                answer.IsVisible = false;
            }
        }

        /*
         * Calculate button pressed
         */
        private void Button_Clicked(object sender, EventArgs e)
        {
            answer.IsVisible = true;
            double num;
            if (string.IsNullOrWhiteSpace(v.Text) || string.IsNullOrWhiteSpace(A.Text) || string.IsNullOrWhiteSpace(h.Text))
            {
                label5.Text = "Please enter all values.";
            }
            else if (Double.TryParse(v.Text, out num) && Double.TryParse(A.Text, out num) && Double.TryParse(h.Text, out num))
            {
                var velocity = double.Parse(v.Text, CultureInfo.InvariantCulture);
                var angle = double.Parse(A.Text, CultureInfo.InvariantCulture);
                angle = Math.PI * angle / 180.0;
                var height = double.Parse(h.Text, CultureInfo.InvariantCulture);

                var cosA = Math.Cos(angle);
                var sinA = Math.Sin(angle);
                var velSquared = Math.Pow(velocity, 2);


                if (!App.switched)
                {
                    var jetLength = ((velocity * Math.Cos(angle)) / 9652.5) * (Math.Sqrt((Math.Pow(velocity, 2) * Math.Pow(Math.Sin(angle), 2)) + (19304.0 * height)) - (velocity * Math.Sin(angle)));
                    label5.Text = "Jet length: " + String.Format("{0:n4}", jetLength) + " in.";
                }
                else
                {
                    var part1 = ((velocity * cosA) / 35305.0);
                    var part2 = velSquared * Math.Pow(sinA, 2) + (70610.0 * height);
                    var part3 = velocity * sinA;
                    var jetLength = part1 * (Math.Sqrt(part2) - part3);
                    label5.Text = "Jet length: " + String.Format("{0:n4}", jetLength) + " m";
                }
            }
            else
            {
                label5.Text = "Please enter a valid number.";
            }
        }
    }
}