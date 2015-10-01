using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using IPCalc;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IPCalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            fillCombobox();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void fillCombobox()
        {
            for(int i = 1; i < 33; i++)
            {
                CBcidr.Items.Add(i.ToString());
            }
            CBcidr.SelectedItem = "24";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            byte octet;
            if (!byte.TryParse(textbox.Text, out octet))
            {
                textbox.Text = "";
            }

            if (firstOctet.Text != "" && secondOctet.Text != "" && thirdOctet.Text != "" && fourthOctet.Text != "")
            {
                calculate();
            }

        }

        private byte[] text2byte()
        {
            byte[] octets = new byte[4];
            octets[0] = byte.Parse(firstOctet.Text);
            octets[1] = byte.Parse(secondOctet.Text);
            octets[2] = byte.Parse(thirdOctet.Text);
            octets[3] = byte.Parse(fourthOctet.Text);
            return octets;
        }

        private void calculate()
        {
            byte[] octets = text2byte();
            InternetProtocolAddress ip = new InternetProtocolAddress(octets[0], octets[1], octets[2], octets[3]);
            IPCalculation ipc = new IPCalculation(ip, byte.Parse(CBcidr.SelectedItem.ToString()));
            updateDecimalLabels(ipc);
            updateBinaryLabels(ipc);
        }

        private void updateDecimalLabels(IPCalculation ipc)
        {
            tbNetmask.Text = ipc.getNetmask().ToString(); ;
            tbNetwork.Text = ipc.getNetworkAddress().ToString() ;
            tbAddresses.Text = ipc.getHostnumber().ToString();
            tbHostSize.Text = ipc.getHostBits().ToString()+" Bits" ;
            tbNetworkSize.Text = ipc.getNetworkBits().ToString()+ " Bits";
            tbStartHost.Text = ipc.getfirstAddress().ToString();
            tbEndHost.Text = ipc.getLastAddress().ToString();
            tbBroadcast.Text= ipc.getBroadcastAddress().ToString();        
        }

        private void updateBinaryLabels(IPCalculation ipc)
        {
            tbNetmaskBinary.Text = ipc.getNetmask().ToBinaryString();
            tbNetworkBinary.Text = ipc.getNetworkAddress().ToBinaryString();
            tbStartHostBinary.Text = ipc.getfirstAddress().ToBinaryString();
            tbEndHostBinary.Text = ipc.getLastAddress().ToBinaryString();
            tbBroadcastBinary.Text = ipc.getBroadcastAddress().ToBinaryString();
        }

        private void CBcidr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (firstOctet.Text != "" && secondOctet.Text != "" && thirdOctet.Text != "" && fourthOctet.Text != "")
            {
                calculate();
            }
        }
    }
}
