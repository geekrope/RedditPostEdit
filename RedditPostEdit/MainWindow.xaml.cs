using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RedditPostEdit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string UpvTemplate = "${UPVOTES}";
        string AuthorTemplate = "${AUTHOR_NAME}";
        string CommsTemplate = "${COMMENTS_COUNT}";
        string PostTemplate = "${TEXT}";
        Uri pageUri = new Uri(@"C:\Users\Admin\source\repos\redditPostSyle\redditPostSyle/thread.html");
        string editingPage = @"C:\Users\Admin\source\repos\RedditPostEdit\RedditPostEdit\PreviewPage.html";
        string HtmlContent = "";
        string TimeTemplate = "${POST_TIME}";
        public void ParsePage()
        {
            HtmlContent = System.IO.File.ReadAllText(pageUri.AbsolutePath);
        }
        public string GetRichTextBoxText(RichTextBox richTextBox)
        {
            if (richTextBox != null)
            {
                return new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
            }
            else
            {
                return "";
            }
        }
        public void ReplaceAll()
        {
            ParsePage();
            var upvC = GetRichTextBoxText(UpvotesCount);
            var author = GetRichTextBoxText(PostedBy);
            var comms = GetRichTextBoxText(CommentsCount);
            var post = GetRichTextBoxText(Text);
            var time = GetRichTextBoxText(Time);
            HtmlContent = HtmlContent.Replace(UpvTemplate, upvC);
            HtmlContent = HtmlContent.Replace(AuthorTemplate, author);
            HtmlContent = HtmlContent.Replace(CommsTemplate, comms);
            HtmlContent = HtmlContent.Replace(PostTemplate, post);
            HtmlContent = HtmlContent.Replace(TimeTemplate, time);
        }
        public void CreateDocument()
        {
            System.IO.File.WriteAllText(editingPage, HtmlContent);            
        }
        public MainWindow()
        {
            InitializeComponent();
            ParsePage();
        }

        private void Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReplaceAll();
            CreateDocument();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(editingPage);
        }
    }
}
