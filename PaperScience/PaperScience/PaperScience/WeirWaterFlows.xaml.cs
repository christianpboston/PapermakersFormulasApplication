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
    public partial class WeirWaterFlows : ContentPage
    {
        public WeirWaterFlows()
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
                label1.Text = "Length of weir opening (m)";
                label2.Text = "Head on weir (m)";
                label4.Text = "Notes: L = (4 to 8) * H\r\nH is measured ~2m back of weir opening\r\na >= 3*H";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Length of weir opening (ft)";
                label2.Text = "Head on weir (ft)";
                label4.Text = "Notes: L = (4 to 8) * H\r\nH is measured ~6ft back of weir opening\r\na >= 3*H";
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
                var length = double.Parse(L.Text, CultureInfo.InvariantCulture);
                var head = double.Parse(H.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var f = 3.33 * (length - 0.2 * head) * Math.Pow(head, 1.5);
                    label3.Text = "Flow: " + String.Format("{0:n4}", f) + " ft\u00B3/s";
                }
                else
                {
                    var f = 1.837 * (length - 0.2 * head) * Math.Pow(head, 1.5);
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