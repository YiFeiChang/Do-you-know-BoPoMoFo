using Newtonsoft.Json;
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

namespace 你到底懂不懂注音符號阿
{
    public class Question
    {
        public string Answer { get; set; }
        public string Options { get; set; }
    }
    public sealed partial class MainPage : Page
    {
        private Question currentQuestion;
        private int currentIndex = 0;
        private string currentDirectoryPath;
        private List<Question> questions = new List<Question>();
        public MainPage()
        {
            this.InitializeComponent();
            currentDirectoryPath = Directory.GetCurrentDirectory();
            List<Question> questionArray;
            using (TextReader textReader = new StreamReader(File.OpenRead(Path.Combine(currentDirectoryPath, "questions.json"))))
            {
                questionArray = JsonConvert.DeserializeObject<List<Question>>(textReader.ReadToEnd());
            }
            questionArray = questionArray.OrderBy(e => e.Answer.Length).Select(e => e).ToList();
            while (questionArray.Where(e => e.Answer.Length == 2).ToList().Count > 0)
            {
                Random random = new Random();
                int index = random.Next(0, questionArray.Where(e => e.Answer.Length == 2).ToList().Count - 1);
                questions.Add(questionArray[index]);
                questionArray.RemoveAt(index);
            }
            while (questionArray.Where(e => e.Answer.Length == 3).ToList().Count > 0)
            {
                Random random = new Random();
                int index = random.Next(0, questionArray.Where(e => e.Answer.Length == 3).ToList().Count - 1);
                questions.Add(questionArray[index]);
                questionArray.RemoveAt(index);
            }
            while (questionArray.Where(e => e.Answer.Length == 4).ToList().Count > 0)
            {
                Random random = new Random();
                int index = random.Next(0, questionArray.Where(e => e.Answer.Length == 4).ToList().Count - 1);
                questions.Add(questionArray[index]);
                questionArray.RemoveAt(index);
            }


            currentQuestion = questions[currentIndex];
            List<char> s = currentQuestion.Options.ToList();
            List<char> s2 = new List<char>();
            while (s.Count > 0)
            {
                Random random = new Random();
                int index = random.Next(0, s.Count - 1);
                s2.Add(s[index]);
                s.RemoveAt(index);
            }
            Question_TextBlock.Text = "";
            foreach (char ss in s2)
            {
                Question_TextBlock.Text += ss;
            }
        }
        private void Button01_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                currentQuestion = questions[currentIndex];
                List<char> s = currentQuestion.Options.ToList();
                List<char> s2 = new List<char>();
                while (s.Count > 0)
                {
                    Random random = new Random();
                    int index = random.Next(0, s.Count - 1);
                    s2.Add(s[index]);
                    s.RemoveAt(index);
                }
                Question_TextBlock.Text = "";
                foreach (char ss in s2)
                {
                    Question_TextBlock.Text += ss;
                }
            }
        }
        private void Button02_Click(object sender, RoutedEventArgs e)
        {
            switch (Button02.Content.ToString())
            {
                case "隱藏":
                    Question_TextBlock.Text = Question_TextBlock.Tag.ToString();
                    Button02.Content = "看答案";
                    break;
                default:
                    Question_TextBlock.Tag = Question_TextBlock.Text;
                    Question_TextBlock.Text = currentQuestion.Answer + "(" + currentQuestion.Options + ")";
                    Button02.Content = "隱藏";
                    break;
            }
        }
        private void Button03_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex < questions.Count - 1)
            {
                currentIndex++;
                currentQuestion = questions[currentIndex];
                List<char> s = currentQuestion.Options.ToList();
                List<char> s2 = new List<char>();
                while (s.Count > 0)
                {
                    Random random = new Random();
                    int index = random.Next(0, s.Count - 1);
                    s2.Add(s[index]);
                    s.RemoveAt(index);
                }
                Question_TextBlock.Text = "";
                foreach (char ss in s2)
                {
                    Question_TextBlock.Text += ss;
                }
            }
        }
    }
}
