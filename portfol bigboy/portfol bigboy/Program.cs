using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
//the "Program" folder handles all class-related, non functional stuff. I could've put it in Form1.cs but kept it here for organizational purposes
namespace portfol_bigboy
{
    class Program
    {
        //initialize player class
        public class player
        {
            public string name;
            public int maxhealth;
            public int str;
            public int luck;
            public int currenthealth;
            public player(string gname, int ghlt, int gstr, int glck)
            {
                name = gname;
                maxhealth = ghlt;
                str = gstr;
                luck = glck;
                currenthealth = maxhealth;
            }
        }
        //put player in list for easier access
        public static List<player> plejr = new List<player>();
        //create enemy class
        public class enemy
        {
            public string name;
            public int health;
            public int damage;
            public string sprite;
            public enemy(string gsprite, string gname, int ghealth, int gdamage)
            {
                sprite = gsprite;
                name = gname;
                health = ghealth;
                damage = gdamage;
            }
        }
        //enemy list to denote which enemy is fightable at the given time
        public static List<enemy> active = new List<enemy>();
        /// <summary> 
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Default starter enemy, monster
            create("٩(̾●̮̮̃̾•̃̾)۶", "Monster", 10, 2);
            //idk what this does i didn't put it here but I'm too scared to delete it
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static void create(string sprite, string name, int health, int damage)
        {
            //function to create enemy and put in list
            enemy nme = new enemy(sprite, name, health, damage);
            active.Add(nme);
        }
        
        public static void createChar(string name, int hlt, int str, int lck)
        {
            //same as above but player
            player plyr = new player(name, hlt + 10, str, lck);
            plejr.Add(plyr);
        }

    }
}
