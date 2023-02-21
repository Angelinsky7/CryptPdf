using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine.Text;
using CommandLine;

namespace CryptPdf {
    public class Options {

        [Option('i', "input", Required = true, HelpText = "Input PDF File.")]
        public String Input { get; set; } = default!;

        [Option('o', "output", Required = true, HelpText = "Output PDF File.")]
        public String Output { get; set; } = default!;

        [Option('p', "password", Required = false, HelpText = "New Password for the Output PDF File.")]
        public String Password { get; set; } = default!;

        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public Boolean Verbose { get; set; }

    }
}
