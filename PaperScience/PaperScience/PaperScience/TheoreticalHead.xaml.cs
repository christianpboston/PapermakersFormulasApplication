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
    public partial class TheoreticalHead : ContentPage
    {
        public TheoreticalHead()
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
                label1.Text = "Spouting velocity (m/min)";
                label2.IsVisible = false;
                label3.IsVisible = false;
                label4.IsVisible = false;
                label5.IsVisible = false;
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Spouting velocity (ft/min)";
                label2.IsVisible = false;
                label3.IsVisible = false;
                label4.IsVisible = false;
                label5.IsVisible = false;
                App.switched = false;
                answer.IsVisible = false;
            }
        }

        /*
         * Calculate button pressed
         */
        private void Button_Clicked(object sender, EventArgs e)
        {
            answer.BorderColor = Color.White;
            answer.IsVisible = true;
            double num;
            if (string.IsNullOrWhiteSpace(spoutingVelocity.Text))
            {
                label2.Text = "Please enter a value.";
                label2.IsVisible = true;
            }
            else if (Double.TryParse(spoutingVelocity.Text, out num))
            {
                var velocity = double.Parse(spoutingVelocity.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var firstHead = Math.Pow(velocity / 100.0, 2) / 1.9304;
                    var secondHead = Math.Pow(velocity / 100.0, 2) / 23.165;
                    var thirdHead = Math.Pow(velocity / 100.0, 2) / 26.196;
                    var fourthHead = Math.Pow(velocity / 100.0, 2) / 53.336;

                    label2.Text = "Head: " + String.Format("{0:n4}", firstHead) + " in. of H20";
                    label2.IsVisible = true;
                    label3.Text = "Head: " + String.Format("{0:n4}", secondHead) + " ft of H20";
                    label3.IsVisible = true;
                    label4.Text = "Head: " + String.Format("{0:n4}", thirdHead) + " in. of Hg";
                    label4.IsVisible = true;
                    label5.Text = "Head: " + String.Format("{0:n4}", fourthHead) + " psig";
                    label5.IsVisible = true;
                }
                else
                {
                    var head = Math.Pow(velocity, 2) / 70610.0;
                    label2.Text = "Head: " + String.Format("{0:n4}", head) + " m of H20";
                    label2.IsVisible = true;
                }
            }
            else
            {
                label2.Text = "Please enter a valid number.";
                label2.IsVisible = true;
            }
        }
    }
}