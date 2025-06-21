using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSigmoind.BussinesLayer.AI.Interface {
    public interface IMentorGPT {
        Task<String> MentorResponse()
    }
}
