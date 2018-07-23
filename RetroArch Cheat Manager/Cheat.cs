using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace RetroArch_Cheat_Manager
{
    class Cheat
    {
        private string name;    // This is the name of the Cheat as it will appear in RetroArch
        private string code;    // List of codes used by this cheat, taken directly from user input (Not Always Correct!)
        private bool enable;    // This is the status of the cheat when the file is loaded (Enabled/Disabled)
        private int id;         // Unique id used to order Code in the cheat file

        public string Name { get => name; set => name = value; }
        public string Code { get => code; set => code = value; }
        public bool Enable { get => enable; set => enable = value; }
        public int Id { get => id; set => id = value; }
        
        public Cheat(string name, string code, bool enable, int id)
        {
            this.name = name;
            this.code = code;
            this.enable = enable;
            this.id = id;
        }

        public Cheat(string name, string code, bool enable)
        {
            this.name = name;
            this.code = code;
            this.enable = enable;
            this.id = -1;
        }
        
        public Cheat()
        {
            this.name = "null";
            this.code = "null";
            this.enable = false;
            this.id = -1;
        }
        
        /*
         * The proper ToString override, returns a string representing a single cheat entry
         * for the cheat file used by RetroArch. Because the user inputted code(s) may contain
         * garbage, we attempt to remove everything but the code itself, and replace every space
         * with a '+' character. Each code ends with a '+' for convenience, at this time this
         * does not appear to cause any issues with RetroArch.
         * 
         * TODO: Investigate ending Code with a '+' char to determine potential issues.
         */
        public override string ToString()
        {
            StringBuilder cheat = new StringBuilder();
            
            code = code.Replace("\r\n", " ");
            code = code.Replace("\n\r", " ");
            code = code.Replace("\r", " ");
            code = code.Replace("\n", " ");
            code = code.Trim().Replace(" ", "+");

            cheat.Append("cheat" + this.id + "_desc = \"" + this.name + "\"\n");
            cheat.Append("cheat" + this.id + "_code = \"" + this.code + "\"\n");
            cheat.Append("cheat" + this.id + "_enable = " + this.enable + "\n\n");

            return cheat.ToString();
        }

        /*
         * So, I didn't know that you could combine multiple Code into one line
         * by simply inserting a '+' between every space and newline...
         * So don't do this, but I already did it, so it's still here.
         * 
         * Note: If you do this, you'll need to track 'id+Code.length' as your
         * actual cheat id for each code in the file. This means you need to be very
         * careful with assigning ids initially. To resolve this issue, always assign
         * a new id as the previous cheat's 'id+Code.length+1'.
         */
        public string ToStringOld()
        {
            StringBuilder cheat = new StringBuilder();

            for (int i = 0; i < this.Code.Length; i++)
            {
                cheat.Append("cheat" + (this.id + i) + "_desc = \"" + this.name + "\"\n");
                cheat.Append("cheat" + (this.id + i) + "_code = \"" + this.Code[i] + "\"\n");
                cheat.Append("cheat" + (this.id + i) + "_enable = " + this.enable + "\n\n");
            }
            return cheat.ToString();
        }
    }
}
