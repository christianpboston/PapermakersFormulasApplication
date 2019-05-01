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
    public partial class sizingAndCapacity : ContentPage
    {
        public sizingAndCapacity()
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
                label1.Text = "BD stock consistency";
                label2.Text = "Tonnes required";
                label4.IsVisible = false;
                label3.IsVisible = false;
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Stock consistency";
                label2.Text = "Tons required";
                label4.IsVisible = false;
                label3.IsVisible = false;
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
            if (string.IsNullOrWhiteSpace(stockConsistency.Text) || string.IsNullOrWhiteSpace(tonsRequired.Text))
            {
                label3.Text = "Please enter all values.";
                label3.IsVisible = true;
            }
            else if (Double.TryParse(stockConsistency.Text, out num) && Double.TryParse(tonsRequired.Text, out num))
            {
                var consistency = double.Parse(stockConsistency.Text, CultureInfo.InvariantCulture);
                var tons = double.Parse(tonsRequired.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var volume = 3200.0 * tons / consistency;
                    var gallons = 3200.0 * tons / consistency / 7.4805;
                    label3.Text = "Volume required: " + String.Format("{0:n4}", volume) + " ft\u00B3";
                    label3.IsVisible = true;
                    label4.Text = "US gallons required: " + String.Format("{0:n4}", gallons);
                    label4.IsVisible = true;
                }
                else
                {
                    var volume = tons * 100.0 / consistency;
                    label3.Text = "Volume required: " + String.Format("{0:n4}", volume) + " m\u00B3";
                    label3.IsVisible = true;
                }
            }
            else
            {
                label3.Text = "Please enter a valid number.";
                label3.IsVisible = true;
            }
        }
    }
}