using System.Collections.Generic;

namespace ProjectSigmoind.Domain.Models {
    public class PromntModel {
        public string UserPromnt { get; set; }

        public string Response { get; set; } 

        public Dictionary<string, string> Links { get; set; } 
    }
}
