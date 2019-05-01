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
    public partial class WeirWaterFlowsV : ContentPage
    {
        public WeirWaterFlowsV()
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
                label1.Text = "Width of weir opening (m)";
                label2.Text = "Head on weir at apex (m)";
                label4.Text = "Notes: L at H distance above notch\r\nH is measured above notch\r\na >= 3/4L";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "SI";
                label1.Text = "Width of weir opening (ft)";
                label2.Text = "Head on weir at apex (ft)";
                label4.Text = "Notes: L at H distance above notch\r\nH is measured above notch\r\na >= 3/4L";
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
            if (string.IsNullOrWhiteSpace(L.Text) || string.IsNullOrWhiteSpace(H.Text))
            {
                label3.Text = "Please enter all values.";
            }
            else if (Double.TryParse(L.Text, out num) && Double.TryParse(H.Text, out num))
            {
                var width = double.Parse(L.Text, CultureInfo.InvariantCulture);
                var head = double.Parse(H.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var root = 2.0 * 32.174 * head;
                    var f = 0.57 * (4.0 / 15.0) * width * head * Math.Sqrt(root);
                    label3.Text = "Flow: " + String.Format("{0:n4}", f) + " ft\u00B3/s";
                }
                else
                {
                    var root = 2.0 * 9.81 * head;
                    var f = 0.57 * (4.0 / 15.0) * width * head * Math.Sqrt(root);
                    label3.Text = "Flow: " + String.Format("{0:n4}", f) + " m\u00B3/s";
                }
            }
            else
            {
                label3.Text = "Please enter a valid number.";
            }
        }
    }
}