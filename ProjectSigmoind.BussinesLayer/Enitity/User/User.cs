using ProjectSigmoind.BussinesLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSigmoind.Domain.Entitys;

namespace ProjectSigmoind.BussinesLayer.Enitity.User {
    public class User : IUserInterface{
        private readonly UserEntity _user;

        public User(UserEntity user) { 
            _user = user;
        }

        // Action
        public string userActionPromnt(User userPromnt) {
            return "NullForNow"; // Need realization
        }

    }
}
