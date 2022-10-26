﻿using System;

namespace Masky
{
    public class ArgParser
    {
        public string ca { get; set; }
        public string template { get; set; }
        public string output_file { get; set; }
        public string debug_file { get; set; }
        public bool current_user { get; set; }
        public bool output_console { get; set; }
        public string[] unparsed_args { get; set; }

        public ArgParser(string[] unparsed_args)
        {
            this.unparsed_args = unparsed_args;
            this.ca = "";
            this.template = "User";
            this.output_file = "./Moose_result.txt";
            this.debug_file = "./Moose_debug.txt";
            this.current_user = false;
            this.output_console = false;
        }

        public bool Parse()
        {
            if (this.unparsed_args.Length == 0)
            {
                Console.WriteLine("/ca:'caFQDN\\caName' (/outputconsole) (/template:User) (/currentUser) (/output:./output.txt) (/debug:./debug.txt)");
                return false;
            }

            foreach (string arg in this.unparsed_args)
            {
                string cur_arg = string.Empty;
                string cur_val = string.Empty;
                int index = arg.IndexOf(":");
                if (index > 0) {
                    cur_arg = arg.Substring(1, index - 1);
                    cur_val = arg.Substring(index + 1);
                }
                if (arg.ToLower() == "/currentuser")
                    this.current_user = true;
                else if (arg.ToLower() == "/outputconsole")
                    this.output_console = true;
                else if (cur_arg != string.Empty && cur_val != string.Empty)
                    this.Set_argument(cur_arg, cur_val);
            }
            if (this.ca == "")
            {
                Console.WriteLine("[-] No CA - /ca:'CAfqdn\\CAname'");
                return false;
            }
            return true;
        }

        void Set_argument(string arg, string val)
        {
            if (arg.ToLower() == "ca")
                this.ca = val;
            else if (arg.ToLower() == "template")
                this.template = val;
            else if (arg.ToLower() == "output")
                this.output_file = val;
            else if (arg.ToLower() == "debug")
                this.debug_file = val;
        }

    }
}
