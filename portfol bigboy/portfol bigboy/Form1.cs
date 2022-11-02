using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace portfol_bigboy
{
    
    public partial class Form1 : Form
    {
        //do the funny random thingy
        public static System.Random random = new System.Random();
        //points that the player can level up with
        public int points;
        //initialize to the first room in the game
        public decimal room = 1;
        public Form1()
        {
            InitializeComponent();
        }

        //character creator "Stats" calculations
        private void NumericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            calculate();
            if (Convert.ToInt32(label6.Text) < 0)
            {
                numericUpDown3.Value--;
            }
        }
        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            calculate();
            if (Convert.ToInt32(label6.Text) < 0)
            {
                numericUpDown1.Value--;
            }
        }
        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            calculate();
            if (Convert.ToInt32(label6.Text) < 0)
            { 
                numericUpDown2.Value--;
            }
        }
        private void calculate()
        {
            //don't allow the player to allocate more than 10 points to their initial stats
            int grug;
            int grg1 = Convert.ToInt32(numericUpDown1.Value);
            int grg2 = Convert.ToInt32(numericUpDown2.Value);
            int grg3 = Convert.ToInt32(numericUpDown3.Value);
            grug = 10 - grg1 - grg2 - grg3;
            label6.Text = grug.ToString();
        }
        //Close character creator menu and open combat / stats menu
        private void Button3_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            { //if player did not enter a name, put this in 
                textBox1.Text = "Nameless";
            }
            Program.createChar(textBox1.Text, Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown3.Value));
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            groupBox3.Visible = true;
            label1.Text = "٩(̾●̮̮̃̾•̃̾)۶";
            refresh();
        }
        //Player presses attack button
        private void Button1_Click(object sender, EventArgs e)
        {
            //Allow "pray" move
            button2.Enabled = true;
            //do player damage calculations
            int atk = getDMG();
            label9.Text = "you did " + Convert.ToString(atk) + " damage";
            Program.active[0].health-= atk;
            //check if enemy is ded 
            if (Program.active[0].health < 1)
            {
                //only allow button presses that move the player along
                label2.Text = "you win";
                button7.Visible = true;
                button1.Visible = false;
                button2.Visible = false;
                label1.Visible = false;
                Program.active.Clear();
            }
        }
        
        public int getDMG()
        {
            //first allow enemy to attack 
            nmeattack();
            //then calculate player damage from str count
            return Program.plejr[0].str + 1;
        }
        public void nmeattack()
        {
            //get generated enemy atk value and subtract that from player health
            int numbre = random.Next(Program.active[0].damage);
            Program.plejr[0].currenthealth -= numbre;
            if(Program.plejr[0].currenthealth < 1)
            {
                //die
                groupBox2.Visible = false;
                groupBox3.Visible = false;
                groupBox4.Visible = true;
            }
            label10.Text = Program.active[0].name + " did " + numbre;
            refresh();
        }
        public void refresh()
        {
            //update labels with new values
            label16.Text = "Room: " + Convert.ToString(room);
            label11.Text = Program.plejr[0].name;
            label12.Text = "Hp: " + Convert.ToString(Program.plejr[0].currenthealth) + "/" + Convert.ToString(Program.plejr[0].maxhealth);
            label13.Text = "strength: " + Convert.ToString(Program.plejr[0].str);
            label14.Text = "Luck: " + Convert.ToString(Program.plejr[0].luck);
            label15.Text = Convert.ToString(points);
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            //Heal self, can't do it again until you attack
            button2.Enabled = false;
            int numbre = random.Next(Program.plejr[0].luck);
            //can't heal above max health
            if (Program.plejr[0].currenthealth + numbre >= Program.plejr[0].maxhealth)
            {
                Program.plejr[0].currenthealth = Program.plejr[0].maxhealth;
            }
            else
            {
                Program.plejr[0].currenthealth += numbre;
            }
            label10.Text = Program.plejr[0].name + " healed " + numbre;
            refresh();
        }
        private void Button7_Click(object sender, EventArgs e)
        {
            //transition to level up screen
            button7.Visible = false;
            //POST-SUBMISSION COMMENT: I should've explained this better before, but the player can only level up if they took damage during the fight. 
            if (Program.plejr[0].currenthealth != Program.plejr[0].maxhealth)
            {
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
            }
            //allow player to increase stats by half the room number
            points += Convert.ToInt32(Math.Ceiling(room / 2));
            label15.Text = Convert.ToString(points);
            button8.Visible = true;
            //move to next room and heal to full
            room++;
            Program.plejr[0].currenthealth = Program.plejr[0].maxhealth;
            refresh();

            label9.Text = "";
            label10.Text = "";
            groupBox2.Text = "You enter the next room";
            label2.Text = "You walk into the room.\nYou feel your strength return to you.\nYou can also feel that you can \nimbue further power into yourself...";

        }
        //handles stat increases
        private void Button4_Click(object sender, EventArgs e)
        {
            Program.plejr[0].maxhealth++;
            Program.plejr[0].currenthealth = Program.plejr[0].maxhealth;
            points--;
            czech();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Program.plejr[0].str++;
            points--;
            czech();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Program.plejr[0].luck++;
            points--;
            czech();
        }
        //checks if player is out of points
        public void czech()
        {
            if (points == 0)
            {
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
            }

            refresh();
        }
        //random generation of new enemies that scale with room 
        private void Button8_Click(object sender, EventArgs e)
        {
            int numbre = random.Next(1, 5);
            int factor = Convert.ToInt32(room);
            if (numbre == 1)
            {
                Program.create(") _ _ __/°°¬", "croc", 10 + factor, 3 + factor);
            } else if (numbre == 2)
            {
                Program.create("ᕙ༼ ,,ԾܫԾ,, ༽ᕗ", "strongman", 3 + factor, 8 + factor);
            } else if (numbre == 3)
            {
                Program.create("(╯°□°)--︻╦╤─ - - - ", "gunguy", 5 + factor, 7 + factor);
            } else
            {
            Program.create("><>", "fishy", 1, 2);
            }
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            drawfight();
        }

        public void drawfight()
        {
            //show generated enemy in fight room and all that
            label1.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            button8.Visible = false;
            label1.Text = Program.active[0].sprite;
            label2.Text = Program.active[0].name;
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            //press the die button
            Application.Restart();
        }
    }
}
