using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Media;
namespace hang
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        string[] fileWords;
        

        int wordIndex;
        string randomWord;
        string randomWord2;
        SoundPlayer sound;
        int wrongGuesses = 0;
        int wrongGuesses1 = 0;
        int wrongGuesses2 = 0;
        int score = 80;
        ToolTip t1 = new ToolTip();


        void rightmessage()
        {
            tabControl1.SelectedTab = tabPage7;
            textBox3.Text = "CONGRATULATION!";
            textBox2.Text = "WORD: " + randomWord + "\r\nSCORE: " + score;
            pictureBox11.Image = Image.FromFile("celebrate.gif");
            sound = new SoundPlayer("firework.wav");
            sound.Play();
        }
        //One Player Button
        private void button1_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        //Two Players Button
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage9;
        }

        //Easy Button
        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        //Medium Button
        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
        }

        //Hard Button
        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;
        }

        //Easy Categories
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //To Get Random Word
            fileWords = File.ReadAllLines(comboBox1.Text + " easy.txt");
            Random random = new Random();
            wordIndex = random.Next(0, fileWords.Length - 1);

            //To Show "-"
            randomWord = fileWords[wordIndex];
            //MessageBox.Show("Random word is: " + randomWord);

            for (int i = 0; i < randomWord.Length; i++)
                if (randomWord[i] == ' ')
                    textBox1.Text += ' ';
                else
                    textBox1.Text += '-';
        }

        //Medium Categories
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //To Get Random Word
            fileWords = File.ReadAllLines(comboBox2.Text + " medium.txt");
            Random random = new Random();
            wordIndex = random.Next(0, fileWords.Length - 1);

            //To Show "-"
            randomWord = fileWords[wordIndex];
            //MessageBox.Show("Random word is: " + randomWord);

            for (int i = 0; i < randomWord.Length; i++)
                if (randomWord[i] == ' ')
                    textBox1.Text += ' ';
                else
                    textBox1.Text += '-';
        }

        //Hard Categories
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //To Get Random Word
            fileWords = File.ReadAllLines(comboBox3.Text + " hard.txt");
            Random random = new Random();
            wordIndex = random.Next(0, fileWords.Length - 1);

            //To Show "-"
            randomWord = fileWords[wordIndex];
            //MessageBox.Show("Random word is: " + randomWord);

            for (int i = 0; i < randomWord.Length; i++)
                if (randomWord[i] == ' ')
                    textBox1.Text += ' ';
                else
                    textBox1.Text += '-';
        }

        //ABC Buttons
        private void LettersButtons(object sender, EventArgs e)
        {
            SoundPlayer buttonsound = new SoundPlayer("butttonsound.wav");
            buttonsound.Play();
            Button b = (Button)sender;
            b.Enabled = false;
            string test;
            //To Check Whether Your Guess is Right or Wrong
            test = textBox1.Text;
            for (int i = 0; i < randomWord.Length; i++)
            {
                if (b.Text[0] == randomWord[i])
                {
                    textBox1.Text = textBox1.Text.Remove(i, 1).Insert(i, b.Text);

                    //If Right
                    if (textBox1.Text == randomWord)
                    {
                        rightmessage();

                    }
                }
            }

            //Wrong Guess
            if (test == textBox1.Text)
            {
                wrongGuesses++;
                score -= 10;
                label8.Text = "SCORE: " + score;

                switch (wrongGuesses)
                {
                    case 1:
                        pictureBox5.Image = Image.FromFile("hang1.jpg");
                        break;
                    case 2:
                        pictureBox5.Image = Image.FromFile("hang2.jpg");
                        break;
                    case 3:
                        pictureBox5.Image = Image.FromFile("hang3.jpg");
                        break;
                    case 4:
                        pictureBox5.Image = Image.FromFile("hang4.jpg");
                        break;
                    case 5:
                        pictureBox5.Image = Image.FromFile("hang5.jpg");
                        break;
                    case 6:
                        pictureBox5.Image = Image.FromFile("hang6.jpg");
                        break;
                    case 7:
                        pictureBox5.Image = Image.FromFile("hang7.jpg");
                        break;
                }
            }

            //If Wrong
            if (wrongGuesses >= 7)
            {
                tabControl1.SelectedTab = tabPage7;
                textBox3.Text = "WRONG!";
                textBox2.Text = "WORD: " + randomWord + "\r\nSCORE: " + score;
                pictureBox11.Image = Image.FromFile("wrong!.gif");
                sound = new SoundPlayer("Hanging.wav");
                sound.Play();

            }
        }

        //Start Game Button
        private void startGame_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage6;
        }

        //Next Button Puzzle
        private void button35_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        //Hint Button
        private void button36_Click(object sender, EventArgs e)
        {
            SoundPlayer buttonsound = new SoundPlayer("butttonsound.wav");
            buttonsound.Play();
            Button b = (Button)sender;
            b.Enabled = false;
            Random random = new Random();
            int hint;
        Found:
            hint = random.Next(randomWord.Length);


            if (textBox1.Text[hint] == '-')
            {
                textBox1.Text = textBox1.Text.Remove(hint, 1).Insert(hint, randomWord[hint].ToString());
                score -= 10;
                if (textBox1.Text == randomWord)
                {
                    rightmessage();
                }
            }
            else
                goto Found;


        }

        //Hint Tip
        private void button36_MouseHover(object sender, EventArgs e)
        {
            t1.Show("Using the hint will make your score decreased by 10 \n You have only one hint", button36);
        }

        //Player One Letters
        private void Player1_Letters(object sender, EventArgs e)
        {

            SoundPlayer buttonsound = new SoundPlayer("butttonsound.wav");
            buttonsound.Play();
                Button b = (Button)sender;
                b.Enabled = false;
                string test;

                //To Check Whether Your Guess is Right or Wrong
                test = textBox6.Text;
                for (int i = 0; i < randomWord.Length; i++)
                {
                    if (b.Text[0] == randomWord[i])
                    {
                        textBox6.Text = textBox6.Text.Remove(i, 1).Insert(i, b.Text);

                        //If Right
                        if (textBox6.Text == randomWord)
                        {
                            tabControl1.SelectedTab = tabPage7;
                            textBox3.Text = "CONGRATULATION! "+" " +" Player1 Win";
                            textBox2.Text = "1st WORD: " + randomWord + "   "+"2nd WORD: "+randomWord+" "+" WINNER SCORE: "+ score;
                            pictureBox11.Image = Image.FromFile("celebrate.gif");
                            sound = new SoundPlayer("firework.wav");
                            sound.Play();
                           
                          
                        }
                    }
                }

                //Wrong Guess
                if (test == textBox6.Text)
                {
                    wrongGuesses1++;
                    switch (wrongGuesses1)
                    {
                        case 1:
                            pictureBox9.Image = Image.FromFile("hang1.jpg");
                            break;
                        case 2:
                            pictureBox9.Image = Image.FromFile("hang2.jpg");
                            break;
                        case 3:
                            pictureBox9.Image = Image.FromFile("hang3.jpg");
                            break;
                        case 4:
                            pictureBox9.Image = Image.FromFile("hang4.jpg");
                            break;
                        case 5:
                            pictureBox9.Image = Image.FromFile("hang5.jpg");
                            break;
                        case 6:
                            pictureBox9.Image = Image.FromFile("hang6.jpg");
                            break;
                        case 7:
                            pictureBox9.Image = Image.FromFile("hang7.jpg");
                            break;
                    }
                    //MessageBox.Show("WRONG GUESS \n PLAYER 2 TURN'S NOW");
                   // Thread.Sleep(500);
                    tabControl1.SelectedTab = tabPage11;

                }
            }

        //Player Two Letters
        private void Player2_Letters(object sender, EventArgs e)
        {
            SoundPlayer buttonsound = new SoundPlayer("butttonsound.wav");
            buttonsound.Play();
            Button b = (Button)sender;
            b.Enabled = false;
            string test;

            //To Check Whether Your Guess is Right or Wrong
            test = textBox4.Text;
            for (int i = 0; i < randomWord2.Length; i++)
            {
                if (b.Text[0] == randomWord2[i])
                {
                    textBox4.Text = textBox4.Text.Remove(i, 1).Insert(i, b.Text);
                    //If Right
                    if (textBox4.Text == randomWord2)
                    {
                        tabControl1.SelectedTab = tabPage7;
                        textBox3.Text = "CONGRATULATION! " + "\r\nPlayer2 Win";
                        textBox2.Text = "1st WORD: " + randomWord + "\r\n2nd WORD: " + randomWord2 + "\r\n WINNER SCORE: " + score;
                        pictureBox11.Image = Image.FromFile("celebrate.gif");
                        sound = new SoundPlayer("firework.wav");
                        sound.Play();
                       
                    }
                }
            }

            //Wrong Guess
            if (test == textBox4.Text)
            {
                wrongGuesses2++;
                switch (wrongGuesses2)
                {
                    case 1:
                        pictureBox10.Image = Image.FromFile("hang1.jpg");
                        break;
                    case 2:
                        pictureBox10.Image = Image.FromFile("hang2.jpg");
                        break;
                    case 3:
                        pictureBox10.Image = Image.FromFile("hang3.jpg");
                        break;
                    case 4:
                        pictureBox10.Image = Image.FromFile("hang4.jpg");
                        break;
                    case 5:
                        pictureBox10.Image = Image.FromFile("hang5.jpg");
                        break;
                    case 6:
                        pictureBox10.Image = Image.FromFile("hang6.jpg");
                        break;
                    case 7:
                        pictureBox10.Image = Image.FromFile("hang7.jpg");
                        break;
                }
                if (wrongGuesses2 < 7)
                {
                    tabControl1.SelectedTab = tabPage8;
                }
                else if (wrongGuesses2 == 7)
                {
                    tabControl1.SelectedTab = tabPage7;
                    textBox3.Text = "DRAW!";
                    textBox2.Text = "1st Word: " + randomWord +"\r\n2nd Word: "+randomWord2;
                    pictureBox11.Image = Image.FromFile("unnamed.gif");
                }

            }
        }
       
        //Easy (multiple) Button
        private void button93_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage10;
        }

        //Start Game (multiple) Button
        private void button94_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage8;
        }

        //Easy (multiple) Categories
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            fileWords = File.ReadAllLines(comboBox4.Text + " easy.txt");
            Random random = new Random();
            //To Get Random Word for P1;
            wordIndex = random.Next(0, fileWords.Length - 1);

            //To Show "-"
            randomWord = fileWords[wordIndex];
          

            for (int i = 0; i < randomWord.Length; i++)
                if (randomWord[i] == ' ')
                    textBox6.Text += ' ';
                else
                    textBox6.Text += '-';
          //To Get Random Word for P2;
            wordIndex = random.Next(0, fileWords.Length - 1);

            //To Show "-"
            randomWord2 = fileWords[wordIndex];
           

            for (int i = 0; i < randomWord2.Length; i++)
                if (randomWord2[i] == ' ')
                    textBox4.Text += ' ';
                else
                    textBox4.Text += '-';
        }
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            fileWords = File.ReadAllLines(comboBox5.Text + " hard.txt");
            Random random = new Random();
            //To Get Random Word for P1;
            wordIndex = random.Next(0, fileWords.Length - 1);

            //To Show "-"
            randomWord = fileWords[wordIndex];


            for (int i = 0; i < randomWord.Length; i++)
                if (randomWord[i] == ' ')
                    textBox6.Text += ' ';
                else
                    textBox6.Text += '-';
            //To Get Random Word for P2;
            wordIndex = random.Next(0, fileWords.Length - 1);

            //To Show "-"
            randomWord2 = fileWords[wordIndex];


            for (int i = 0; i < randomWord2.Length; i++)
                if (randomWord2[i] == ' ')
                    textBox4.Text += ' ';
                else
                    textBox4.Text += '-';
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            fileWords = File.ReadAllLines(comboBox6.Text + " medium.txt");
            Random random = new Random();
            //To Get Random Word for P1;
            wordIndex = random.Next(0, fileWords.Length - 1);

            //To Show "-"
            randomWord = fileWords[wordIndex];


            for (int i = 0; i < randomWord.Length; i++)
                if (randomWord[i] == ' ')
                    textBox6.Text += ' ';
                else
                    textBox6.Text += '-';
            //To Get Random Word for P2;
            wordIndex = random.Next(0, fileWords.Length - 1);

            //To Show "-"
            randomWord2 = fileWords[wordIndex];

            for (int i = 0; i < randomWord2.Length; i++)
                if (randomWord2[i] == ' ')
                    textBox4.Text += ' ';
                else
                    textBox4.Text += '-';
        }


        //Player One Hint Button
        private void button37_Click(object sender, EventArgs e)
        {
            SoundPlayer buttonsound = new SoundPlayer("butttonsound.wav");
            buttonsound.Play();
            Button b = (Button)sender;
            b.Enabled = false;
            Random random = new Random();
            int hint;
        Found:
            hint = random.Next(randomWord.Length);


            if (textBox6.Text[hint] == '-')
            {
                textBox6.Text = textBox6.Text.Remove(hint, 1).Insert(hint, randomWord[hint].ToString());
                
                if (textBox6.Text == randomWord)
                {
                    tabControl1.SelectedTab = tabPage7;
                    textBox3.Text = "CONGRATULATION! " + "\r\nPlayer1 Win";
                    textBox2.Text = "1st WORD: " + randomWord + "\r\n2nd WORD: " + randomWord2 + "\r\nWINNER SCORE: " + score;
                    pictureBox11.Image = Image.FromFile("celebrate.gif");
                    sound = new SoundPlayer("firework.wav");
                    sound.Play();
                    
                   

                }
            }
            else
                goto Found;
        }

        //Player Two Hint Button
        private void button(object sender, EventArgs e)
        {
            SoundPlayer buttonsound = new SoundPlayer("butttonsound.wav");
            buttonsound.Play();
            Button b = (Button)sender;
            b.Enabled = false;
            Random random = new Random();
            int hint;
        Found:
            hint = random.Next(randomWord2.Length);


            if (textBox4.Text[hint] == '-')
            {
                textBox4.Text = textBox4.Text.Remove(hint, 1).Insert(hint, randomWord2[hint].ToString());

                if (textBox4.Text == randomWord2)
                {
                    tabControl1.SelectedTab = tabPage7;
                    textBox3.Text = "CONGRATULATION! " + "\r\nPlayer2 Win";
                    textBox2.Text = "1st WORD: " + randomWord + "\r\n2nd WORD: " + randomWord2 + "\r\nWINNER SCORE: " + score;
                    pictureBox11.Image = Image.FromFile("celebrate.gif");
                    sound = new SoundPlayer("firework.wav");
                    sound.Play();
                  
                }
            }
            else
                goto Found;
        }

        private void button92_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage12;
        }

        private void button91_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage13;
        }

    
        private void button95_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage8;

        }

        private void button96_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage8;

        }

        private void back(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void back2(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage9;
        }

        private void button103_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

      

        
        }
    }

