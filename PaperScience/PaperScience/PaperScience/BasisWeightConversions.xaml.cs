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
    public partial class BasisWeightConversions : ContentPage
    {
        public BasisWeightConversions()
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
                label1.Text = "Offset (g/m\u00B2)";
                label2.Text = "Bond (g/m\u00B2)";
                label3.Text = "Liner (g/m\u00B2)";
                label4.Text = "News and Tissue (g/m\u00B2)";
                label5.Text = "Market Pulp (g/m\u00B2)";
                s.Text = "SI";
                App.switched = true;
                errorFrame.IsVisible = false;
                hideLabels();
            }
            else
            {
                label1.Text = "Offset (lb/3300 ft\u00B2)";
                label2.Text = "Bond (lb/1300 ft\u00B2)";
                label3.Text = "Liner (lb/1000 ft\u00B2)";
                label4.Text = "News and Tissue (lb/3000 ft\u00B2)";
                label5.Text = "Market Pulp (lb/2880 ft\u00B2)";
                s.Text = "US";
                App.switched = false;
                errorFrame.IsVisible = false;
                hideLabels();
            }
        }

        /*
        * Hides additional answer labels
        */
        private void hideLabels()
        {
            answer1Frame.IsVisible = false;
            answer2Frame.IsVisible = false;
            answer3Frame.IsVisible = false;
            answer4Frame.IsVisible = false;
            answer5Frame.IsVisible = false;
        }


        /*
         * Calculate button pressed
         */
        private void Button_Clicked(object sender, EventArgs e)
        {
            double num;
            hideLabels();
            if (string.IsNullOrWhiteSpace(e1.Text) && string.IsNullOrWhiteSpace(e2.Text) && string.IsNullOrWhiteSpace(e3.Text) && string.IsNullOrWhiteSpace(e4.Text) && string.IsNullOrWhiteSpace(e5.Text))
            {
                error.Text = "Please enter at least one conversion.";
                errorFrame.IsVisible = true;
                hideLabels();
            }
            if (!App.switched) // US
            {
                if (!string.IsNullOrWhiteSpace(e1.Text)) // offset
                {
                    if (!Double.TryParse(e1.Text, out num))
                    {
                        al1.Text = "Please enter a valid number.";
                        answer1Frame.IsVisible = true;
                    }
                    else
                    {
                        var offset = double.Parse(e1.Text, CultureInfo.InvariantCulture);
                        var conversion = offset * 1.48;
                        al1.Text = "Offset: " + String.Format("{0:n4}", conversion) + " g/m\u00B2";
                        answer1Frame.IsVisible = true;
                    }
                }
                if (!string.IsNullOrWhiteSpace(e2.Text)) // bond
                {
                    if (!Double.TryParse(e2.Text, out num))
                    {
                        al2.Text = "Please enter a valid number.";
                        answer2Frame.IsVisible = true;
                    }
                    else
                    {
                        var bond = double.Parse(e2.Text, CultureInfo.InvariantCulture);
                        var conversion = bond * 3.76;
                        al2.Text = "Bond: " + String.Format("{0:n4}", conversion) + " g/m\u00B2";
                        answer2Frame.IsVisible = true;
                    }
                }
                if (!string.IsNullOrWhiteSpace(e3.Text)) // liner
                {
                    if (!Double.TryParse(e3.Text, out num))
                    {
                        al3.Text = "Please enter a valid number.";
                        answer3Frame.IsVisible = true;
                    }
                    else
                    {
                        var liner = double.Parse(e3.Text, CultureInfo.InvariantCulture);
                        var conversion = liner * 4.89;
                        al3.Text = "Liner: " + String.Format("{0:n4}", conversion) + " g/m\u00B2";
                        answer3Frame.IsVisible = true;
                    }
                }
                if (!string.IsNullOrWhiteSpace(e4.Text)) // news and tissue
                {
                    if (!Double.TryParse(e4.Text, out num))
                    {
                        al4.Text = "Please enter a valid number.";
                        answer4Frame.IsVisible = true;
                    }
                    else
                    {
                        var newsAndTissue = double.Parse(e4.Text, CultureInfo.InvariantCulture);
                        var conversion = newsAndTissue * 1.63;
                        al4.Text = "News and Tissue: " + String.Format("{0:n4}", conversion) + " g/m\u00B2";
                        answer4Frame.IsVisible = true;
                    }
                }
                if (!string.IsNullOrWhiteSpace(e5.Text)) // market pulp
                {
                    if (!Double.TryParse(e5.Text, out num))
                    {
                        al5.Text = "Please enter a valid number.";
                        answer5Frame.IsVisible = true;
                    }
                    else
                    {
                        var marketPulp = double.Parse(e5.Text, CultureInfo.InvariantCulture);
                        var conversion = marketPulp * 1.70;
                        al5.Text = "Market Pulp: " + String.Format("{0:n4}", conversion) + " g/m\u00B2";
                        answer5Frame.IsVisible = true;
                    }
                }
            }
            else // SI
            {
                if (!string.IsNullOrWhiteSpace(e1.Text)) // offset
                {
                    if (!Double.TryParse(e1.Text, out num))
                    {
                        al1.Text = "Please enter a valid number.";
                        answer1Frame.IsVisible = true;
                    }
                    else
                    {
                        var offset = double.Parse(e1.Text, CultureInfo.InvariantCulture);
                        var conversion = offset / 1.48;
                        al1.Text = "Offset: " + String.Format("{0:n4}", conversion) + " lb/3300 ft\u00B2";
                        answer1Frame.IsVisible = true;
                    }
                }
                if (!string.IsNullOrWhiteSpace(e2.Text)) // bond
                {
                    if (!Double.TryParse(e2.Text, out num))
                    {
                        al2.Text = "Please enter a valid number.";
                        answer2Frame.IsVisible = true;
                    }
                    else
                    {
                        var bond = double.Parse(e2.Text, CultureInfo.InvariantCulture);
                        var conversion = bond / 3.76;
                        al2.Text = "Bond: " + String.Format("{0:n4}", conversion) + " lb/1300 ft\u00B2";
                        answer2Frame.IsVisible = true;
                    }
                }
                if (!string.IsNullOrWhiteSpace(e3.Text)) // liner
                {
                    if (!Double.TryParse(e3.Text, out num))
                    {
                        al3.Text = "Please enter a valid number.";
                        answer3Frame.IsVisible = true;
                    }
                    else
                    {
                        var liner = double.Parse(e3.Text, CultureInfo.InvariantCulture);
                        var conversion = liner / 4.89;
                        al3.Text = "Liner: " + String.Format("{0:n4}", conversion) + " lb/1000 ft\u00B2";
                        answer3Frame.IsVisible = true;
                    }
                }
                if (!string.IsNullOrWhiteSpace(e4.Text)) // news and tissue
                {
                    if (!Double.TryParse(e4.Text, out num))
                    {
                        al4.Text = "Please enter a valid number.";
                        answer4Frame.IsVisible = true;
                    }
                    else
                    {
                        var newsAndTissue = double.Parse(e4.Text, CultureInfo.InvariantCulture);
                        var conversion = newsAndTissue / 1.63;
                        al4.Text = "News and Tissue: " + String.Format("{0:n4}", conversion) + " lb/3000 ft\u00B2";
                        answer4Frame.IsVisible = true;
                    }
                }
                if (!string.IsNullOrWhiteSpace(e5.Text)) // market pulp
                {
                    if (!Double.TryParse(e5.Text, out num))
                    {
                        al5.Text = "Please enter a valid number.";
                        answer5Frame.IsVisible = true;
                    }
                    else
                    {
                        var marketPulp = double.Parse(e5.Text, CultureInfo.InvariantCulture);
                        var conversion = marketPulp / 1.70;
                        al5.Text = "Market Pulp: " + String.Format("{0:n4}", conversion) + " lb/2880 ft\u00B2";
                        answer5Frame.IsVisible = true;
                    }
                }
            }
        }
    }
}