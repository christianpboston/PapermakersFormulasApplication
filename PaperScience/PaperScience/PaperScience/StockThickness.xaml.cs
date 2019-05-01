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
    public partial class StockThickness : ContentPage
    {
        public StockThickness()
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
                label1.Text = "Basis weight (g/m\u00B2)";
                label2.Text = "Consistency (%)";
                label3.Text = "Retention (%)";
                label4.Text = "Jet/wire ratio (j/w)";
                label5.IsVisible = false;
                input5.IsVisible = false;
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Basis weight (lbs)";
                label2.Text = "Ream size (ft\u00B2)";
                label3.Text = "Consistency (%)";
                label4.Text = "Retention (%)";
                label5.Text = "Jet/wire ratio (j/w)";
                label5.IsVisible = true;
                input5.IsVisible = true;
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
            if (App.switched)
            {
                if (string.IsNullOrWhiteSpace(bw.Text) || string.IsNullOrWhiteSpace(input2.Text) || string.IsNullOrWhiteSpace(input3.Text) || string.IsNullOrWhiteSpace(input4.Text))
                {
                    label6.Text = "Please enter all values.";
                }
                else if (Double.TryParse(bw.Text, out num) && Double.TryParse(input2.Text, out num) && Double.TryParse(input3.Text, out num) && Double.TryParse(input4.Text, out num))
                {
                    var weight = double.Parse(bw.Text, CultureInfo.InvariantCulture);
                    var consistency = double.Parse(input2.Text, CultureInfo.InvariantCulture) / 100.0; ;
                    var retention = double.Parse(input3.Text, CultureInfo.InvariantCulture) / 100.0;
                    var ratio = double.Parse(input4.Text, CultureInfo.InvariantCulture);

                    var top = weight / 10000.0;
                    var bottom = consistency * retention * ratio;

                    var thick = top / bottom;
                    label6.Text = "Stock thickness: " + String.Format("{0:n4}", thick) + " cm";
                }
                else
                {
                    label6.Text = "Please enter a valid number.";
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(bw.Text) || string.IsNullOrWhiteSpace(input2.Text) || string.IsNullOrWhiteSpace(input3.Text) || string.IsNullOrWhiteSpace(input4.Text)
                || string.IsNullOrWhiteSpace(input5.Text))
                {
                    label6.Text = "Please enter all values.";
                }
                else if (Double.TryParse(bw.Text, out num) && Double.TryParse(input2.Text, out num) && Double.TryParse(input3.Text, out num) && Double.TryParse(input4.Text, out num)
                    && Double.TryParse(input5.Text, out num))
                {
                    var weight = double.Parse(bw.Text, CultureInfo.InvariantCulture);
                    var reamSize = double.Parse(input2.Text, CultureInfo.InvariantCulture);
                    var consistency = double.Parse(input3.Text, CultureInfo.InvariantCulture) / 100.0;
                    var retention = double.Parse(input4.Text, CultureInfo.InvariantCulture) / 100.0;
                    var ratio = double.Parse(input5.Text, CultureInfo.InvariantCulture);

                    var top = weight * 0.1925;
                    var bottom = consistency * retention * reamSize * ratio;

                    var thick = top / bottom;
                    label6.Text = "Stock thickness: " + String.Format("{0:n4}", thick) + " in.";
                }
                else
                {
                    label6.Text = "Please enter a valid number.";
                }
            }
        }
    }
}