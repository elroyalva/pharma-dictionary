
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Diagnostics;
using System.Xml.Linq;
using Pharmacy_Dictionary.ViewModels;

namespace Pharmacy_Dictionary
{
    public partial class ResultsPage : PhoneApplicationPage
    {
        public ResultsPage()
        {
            InitializeComponent();
        }

        public string aaa="";
        public string abc;
        public string xyz;

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.TryGetValue("term", out aaa))
            {
                Debug.WriteLine(aaa);
                abc = aaa;
            }

            if (NavigationContext.QueryString.TryGetValue("search", out aaa))
            {
                Debug.WriteLine(aaa);
                xyz = aaa;
            }
            WebClient wcXML = new WebClient();

            if (xyz == "med")
            {
                wcXML.DownloadStringCompleted += c_DownloadStringCompleted;
                wcXML.DownloadStringAsync(new Uri("http://www.dictionaryapi.com/api/v1/references/medical/xml/" + abc + "?key=235089d7-eb18-47f6-9ab8-226685fc7d98"));
            }
            else
            {
                wcXML.DownloadStringCompleted += d_DownloadStringCompleted;
                wcXML.DownloadStringAsync(new Uri("http://www.dictionaryapi.com/api/v1/references/collegiate/xml/" + abc + "?key=cd7437db-289f-4f31-80db-6a99bfd7a796"));
            }
        }

    private void d_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
    {
        var s = e.Result;
        //Debug.WriteLine(""+e.Result);
        var rssElement = XElement.Parse(s);


        var items = from item in rssElement.Descendants("entry")
                    select new entry_list
                    {
                        id = item.Attribute("id").Value,
                        //fl = item.Element("fl").Value,
                        def = item.Element("def").Element("dt").Value
                    };


        //    MessageBox.Show("No results");
        result.DataContext = items;
        //                result.EmptyContent = "No matching items found, go back and check your term";
        if (result.ItemCount == 0)
        {
            result.EmptyContent = "No matching items found, \ngo back and check your search term";
            MessageBox.Show("No results");
            //sugg.DataContext = items;
        }
    } 
        
    private void c_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //MainDataViewModel m;
            var s = e.Result;
            //Debug.WriteLine(""+e.Result);
            var rssElement = XElement.Parse(s);

            
                var items = from item in rssElement.Descendants("entry")
                            select new entry_list
                            {
                                id = item.Attribute("id").Value,
                                //fl = item.Element("fl").Value,
                                def = item.Element("def").Element("sensb").Element("sens").Element("dt").Value  
                            };

                
                //    MessageBox.Show("No results");
                result.DataContext = items;
//                result.EmptyContent = "No matching items found, go back and check your term";
                if (result.ItemCount == 0)
                {
                    result.EmptyContent = "No matching items found, \ngo back and check your search term";
                    MessageBox.Show("No results");
                    //sugg.DataContext = items;
                }
            //else
            //{
            //    var ite = from item in rssElement.Descendants("entry")
            //              select new entry_list
            //              {
            //                  suggestion = item.Attribute("id").Value
            //              };

            //    ContentPanel.DataContext = ite;
            //}
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/About.xaml", UriKind.RelativeOrAbsolute)); 
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {

        }

        private void Navtomed(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
